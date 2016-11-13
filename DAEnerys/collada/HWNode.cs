using OpenTK;
using System.Collections.Generic;
using Assimp;
using System.Windows.Forms;
using System.Linq;
using System;

namespace DAEnerys
{
    public class HWNode
    {
        public static HWNode[] Roots = new HWNode[6];
        public static HWNode RootLOD0;
        public static HWNode RootLOD1;
        public static HWNode RootLOD2;
        public static HWNode RootLOD3;
        public static HWNode RootINFO;
        public static HWNode RootCOL;

        public List<HWNode> Children = new List<HWNode>();
        public List<HWMesh> Meshes = new List<HWMesh>();

        private HWNode parent;
        public HWNode Parent { get { return parent; } set { if(parent != null) parent.Children.Remove(this); parent = value; if(parent != null) parent.Children.Add(this); CalculateWorldMatrix(); Renderer.InvalidateView(); Renderer.Invalidate(); } }
        public string Name;
        public Matrix4 WorldMatrix = Matrix4.Identity;
        public Matrix4 RelativeWorldMatrix = Matrix4.Identity;

        public Vector3 AbsolutePosition { get { return Vector3.TransformPosition(Vector3.Zero, WorldMatrix); } }
        public OpenTK.Quaternion AbsoluteRotation;
        public Vector3 AbsoluteScale { get { return WorldMatrix.ExtractScale(); } }

        public Vector3 RelativePosition { get { return Vector3.TransformPosition(Vector3.Zero, RelativeWorldMatrix); } }
        public Vector3 RelativeRotation
        {
            get
            {
                Vector3 rotation = Vector3.Zero;
                float angle = 0;
                RelativeWorldMatrix.ExtractRotation().ToAxisAngle(out rotation, out angle);
                return rotation * angle;
            }
        }

        public HWJoint Joint;
        public HWMarker Marker;
        public HWDockpath Dockpath;
        public HWDockSegment DockSegment;
        public HWNavLight NavLight;

        private Node node;

        public HWNode(Node node, HWNode parent)
        {
            HWScene.Nodes.Add(this);
            this.node = node;
            Name = node.Name;

            WorldMatrix = new Matrix4(node.Transform.A1, node.Transform.B1, node.Transform.C1, node.Transform.D1, node.Transform.A2, node.Transform.B2, node.Transform.C2, node.Transform.D2, node.Transform.A3, node.Transform.B3, node.Transform.C3, node.Transform.D3, node.Transform.A4, node.Transform.B4, node.Transform.C4, node.Transform.D4);
            RelativeWorldMatrix = WorldMatrix;

            this.Parent = parent;

            if (Name.StartsWith("ROOT_LOD")) //If node is a root LOD node
            {
                string[] split = Name.Split('[');

                if (split.Length > 1)
                {
                    string lodString = split[1];
                    lodString = lodString.Remove(lodString.Length - 1);

                    int lod = 0;
                    bool success = int.TryParse(lodString, out lod);

                    if (success)
                    {
                        Roots[lod] = this;

                        switch (lod)
                        {
                            case 0:
                                RootLOD0 = this;
                                break;
                            case 1:
                                RootLOD1 = this;
                                break;
                            case 2:
                                RootLOD2 = this;
                                break;
                            case 3:
                                RootLOD3 = this;
                                break;
                        }
                    }
                }
            }

            if (Name.StartsWith("ROOT_COL")) //If node is a root COL node
            {
                Roots[4] = this;
                RootCOL = this;
            }

            if (Name.StartsWith("ROOT_INFO")) //If node is a root INFO node
            {
                Roots[5] = this;
                RootINFO = this;
            }

            if (Name.StartsWith("JNT")) //If node is a joint
            {
                string jointName = Name.Split('[')[1];
                jointName = jointName.Remove(jointName.Length - 1);
                Joint = new HWJoint(this, parent.Joint, jointName);
            }
            else if (Name.StartsWith("MARK")) //If node is a marker
            {
                string markerName = Name.Split('[')[1];
                markerName = markerName.Remove(markerName.Length - 1);
                Marker = new HWMarker(this, markerName);
            }
            #region Dockpath
            else if (Name.StartsWith("DOCK")) //If node is a dockpath
            {
                string pathName = "";
                string[] families = new string[0];
                string[] links = new string[0];
                List<DockpathFlag> flags = new List<DockpathFlag>();

                string[] splitted = Name.Split('[');
                int end = -1;

                for (int i = 0; i < splitted.Length; i++)
                {
                    if (i != 0)
                    {
                        end = splitted[i].IndexOf(']');
                        if (splitted[i - 1].EndsWith("DOCK")) //Name
                        {
                            pathName = splitted[i].Substring(0, end);
                        }
                        else if (splitted[i - 1].EndsWith("Fam")) //Families
                        {
                            string familiesString = splitted[i].Substring(0, end);
                            families = familiesString.Replace(" ", "").Split(',');
                        }
                        else if (splitted[i - 1].EndsWith("Link")) //Links
                        {
                            string linksString = splitted[i].Substring(0, end);

                            //Don't load empty links
                            if(linksString.Trim().Length > 0)
                                links = linksString.Replace(" ", "").Split(',');
                        }
                        else if (splitted[i - 1].EndsWith("Flags")) //Flags
                        {
                            string flagsString = splitted[i].Substring(0, end);
                            string[] flagsStrings = flagsString.Split(' ');

                            foreach (string flag in flagsStrings)
                            {
                                if (flag.Length > 0) //If FLAGS is not empty
                                {
                                    DockpathFlag newFlag;
                                    bool success = Enum.TryParse(flag.ToUpper(), out newFlag);

                                    //Check if flag is valid
                                    if (success)
                                        flags.Add(newFlag);
                                    else
                                        new Problem(ProblemTypes.WARNING, "Unknown dockpath flag \"" + flag + "\" on dockpath \"" + pathName + "\".");
                                }
                            }
                        }
                    }
                }
                Dockpath = new HWDockpath(this, pathName, families, links, flags);
            }
            #endregion

            #region DockSegment
            else if (Name.StartsWith("SEG")) //If node is a docksegment
            {
                //Check if segment is child of dockpath
                if (this.Parent.Dockpath != null)
                {
                    int id = -1;
                    float tolerance = 0;
                    float speed = 0;
                    List<DockSegmentFlag> flags = new List<DockSegmentFlag>();

                    string[] splitted = Name.Split('_');
                    int start = -1;
                    int end = -1;
                    foreach (string split in splitted)
                    {
                        if (split.StartsWith("SEG")) //ID
                        {
                            start = split.IndexOf('[') + 1;
                            end = split.IndexOf(']');
                            id = int.Parse(split.Substring(start, end - start));
                        }
                        else if (split.StartsWith("Tol")) //Tolerance
                        {
                            start = split.IndexOf('[') + 1;
                            end = split.IndexOf(']');
                            tolerance = float.Parse(split.Substring(start, end - start), System.Globalization.CultureInfo.InvariantCulture);
                        }
                        else if (split.StartsWith("Spd")) //Speed
                        {
                            start = split.IndexOf('[') + 1;
                            end = split.IndexOf(']');
                            speed = float.Parse(split.Substring(start, end - start), System.Globalization.CultureInfo.InvariantCulture);
                        }
                        else if (split.StartsWith("Flags")) //Flags
                        {
                            start = split.IndexOf('[') + 1;
                            end = split.IndexOf(']');
                            string flagsString = split.Substring(start, end - start);
                            string[] flagsStrings = flagsString.Split(' ');

                            foreach (string flag in flagsStrings)
                            {
                                DockSegmentFlag newFlag;
                                bool success = Enum.TryParse(flag.ToUpper(), out newFlag);

                                //Check if flag is valid
                                if (success)
                                    flags.Add(newFlag);
                                else
                                    new Problem(ProblemTypes.WARNING, "Unknown dockpath segment flag \"" + flag + "\" in dockpath \"" + this.Parent.Dockpath.Name + "\".");
                            }
                        }
                    }
                    DockSegment = new HWDockSegment(this, id, tolerance, speed, flags);
                }
                else
                    new Problem(ProblemTypes.WARNING, "Dockpath segment \"" + Name + "\" is not a child of a dockpath.");
            }
            #endregion

            #region NavLight
            else if (Name.StartsWith("NAVL")) //If node is a navlight
            {
                string lightName = "";
                string type = "default";
                float size = 0;
                float phase = 0;
                float frequency = 0;
                Vector3 color = Vector3.One;
                float distance = 0;
                List<NavLightFlag> flags = new List<NavLightFlag>();

                string[] splitted = Name.Split('[');
                int end = -1;

                for (int i = 0; i < splitted.Length; i++)
                {
                    if (i != 0)
                    {
                        end = splitted[i].IndexOf(']');
                        if (splitted[i - 1].EndsWith("NAVL")) //Name
                        {
                            lightName = splitted[i].Substring(0, end);
                        }
                        else if (splitted[i - 1].EndsWith("Type")) //Type
                        {
                            type = splitted[i].Substring(0, end);
                        }
                        else if (splitted[i - 1].EndsWith("Sz")) //Size
                        {
                            size = float.Parse(splitted[i].Substring(0, end), System.Globalization.CultureInfo.InvariantCulture);
                        }
                        else if (splitted[i - 1].EndsWith("Ph")) //Phase
                        {
                            phase = float.Parse(splitted[i].Substring(0, end), System.Globalization.CultureInfo.InvariantCulture);
                        }
                        else if (splitted[i - 1].EndsWith("Fr")) //Frequency
                        {
                            frequency = float.Parse(splitted[i].Substring(0, end), System.Globalization.CultureInfo.InvariantCulture);
                        }
                        else if (splitted[i - 1].EndsWith("Col")) //Color
                        {
                            string[] channels = splitted[i].Substring(0, end).Split(',');
                            float red = float.Parse(channels[0], System.Globalization.CultureInfo.InvariantCulture);
                            float green = float.Parse(channels[1], System.Globalization.CultureInfo.InvariantCulture);
                            float blue = float.Parse(channels[2], System.Globalization.CultureInfo.InvariantCulture);
                            color = new Vector3(red, green, blue);
                        }
                        else if (splitted[i - 1].EndsWith("Dist")) //Distance
                        {
                            distance = float.Parse(splitted[i].Substring(0, end), System.Globalization.CultureInfo.InvariantCulture);
                        }
                        else if (splitted[i - 1].EndsWith("Flags")) //Flags
                        {
                            string flagsString = splitted[i].Substring(0, end);
                            string[] flagsStrings = flagsString.Split(' ');

                            foreach (string flag in flagsStrings)
                            {
                                if (flag.Length > 0) //If FLAGS is not empty
                                    flags.Add((NavLightFlag)Enum.Parse(typeof(NavLightFlag), flag.ToUpper()));
                            }
                        }
                    }
                }

                HWNavLightStyle navLightStyle = null;
                //Check if navlight style is valid
                foreach(HWNavLightStyle style in HWData.NavLightStyles)
                {
                    if(style.Name == type)
                    {
                        navLightStyle = style;
                        break;
                    }
                }

                if(navLightStyle != null)
                    NavLight = new HWNavLight(this, lightName, navLightStyle, size, phase, frequency, color, distance, flags);
                else
                    new Problem(ProblemTypes.WARNING, "Navlight style \"" + type + "\" not found. Skipping navlight \"" + lightName + "\".");
            }
            #endregion

            //Add meshes
            foreach (int mesh in node.MeshIndices)
            {
                this.AddMesh(HWScene.Meshes[mesh]);
            }

            //Add children
            foreach(Node childNode in node.Children)
            {
                HWNode newChild = new HWNode(childNode, this);
            }
        }

        public void AddMesh(HWMesh mesh)
        {
            mesh.Parent = this;
            Meshes.Add(mesh);
        }

        /// <summary>
        /// Calculates the model matrix from transforms
        /// </summary>
        public void CalculateWorldMatrix()
        {
            WorldMatrix = RelativeWorldMatrix;

            if (Parent != null)
                WorldMatrix *= Parent.WorldMatrix;

            AbsoluteRotation = WorldMatrix.ExtractRotation();

            if (Name.StartsWith("ROOT_")) //Ignore root positions
                WorldMatrix = WorldMatrix.ClearTranslation();

            if (Name.StartsWith("ROOT_LOD")) //Fix Homeworld rotation
                WorldMatrix *= Matrix4.CreateFromQuaternion(AbsoluteRotation.Inverted());

            WorldMatrix = WorldMatrix.ClearScale(); //Ignore scale
        }
    }
}
