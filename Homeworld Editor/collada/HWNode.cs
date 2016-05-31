using OpenTK;
using System.Collections.Generic;
using Assimp;
using System.Windows.Forms;
using System.Linq;
using System;

namespace HomeworldDAEEditor
{
    public class HWNode
    {
        public List<HWNode> children = new List<HWNode>();
        public List<HWMesh> meshes = new List<HWMesh>();

        public HWNode Parent;
        public string Name;
        public Matrix4 WorldMatrix = Matrix4.Identity;

        public Vector3 AbsolutePosition { get { return Vector3.TransformPosition(Vector3.Zero, WorldMatrix); } }
        public Vector3 AbsoluteScale { get { return WorldMatrix.ExtractScale(); } }

        public HWJoint Joint;
        public HWMarker Marker;
        public HWDockpath Dockpath;
        public HWDockSegment DockSegment;

        private Node node;

        public HWNode(Node node, HWNode parent)
        {
            HWScene.Nodes.Add(this);
            this.node = node;
            this.Parent = parent;

            Name = node.Name;

            WorldMatrix = new Matrix4(node.Transform.A1, node.Transform.B1, node.Transform.C1, node.Transform.D1, node.Transform.A2, node.Transform.B2, node.Transform.C2, node.Transform.D2, node.Transform.A3, node.Transform.B3, node.Transform.C3, node.Transform.D3, node.Transform.A4, node.Transform.B4, node.Transform.C4, node.Transform.D4);

            CalculateWorldMatrix();

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
                            links = linksString.Replace(" ", "").Split(',');
                        }
                        else if (splitted[i - 1].EndsWith("Flags")) //Flags
                        {
                            string flagsString = splitted[i].Substring(0, end);
                            string[] flagsStrings = flagsString.Split(' ');

                            foreach (string flag in flagsStrings)
                            {
                                if(flag.Length > 0) //If FLAGS is not empty
                                    flags.Add((DockpathFlag)Enum.Parse(typeof(DockpathFlag), flag.ToUpper()));
                            }
                        }
                    }
                }
                Dockpath = new HWDockpath(this, pathName, families, links, flags);
                #endregion
            }

            #region DockSegment
            else if (Name.StartsWith("SEG")) //If node is a docksegment
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
                            flags.Add((DockSegmentFlag)Enum.Parse(typeof(DockSegmentFlag), flag.ToUpper()));
                        }
                    }
                }
                DockSegment = new HWDockSegment(this, id, tolerance, speed, flags);
                #endregion
            }

            //Add meshes
            foreach (int mesh in node.MeshIndices)
            {
                this.AddMesh(HWScene.Meshes[mesh]);
            }

            //Add children
            foreach(Node childNode in node.Children)
            {
                HWNode newChild = new HWNode(childNode, this);
                children.Add(newChild);
            }
        }

        public void AddMesh(HWMesh mesh)
        {
            mesh.Parent = this;
            meshes.Add(mesh);
        }

        /// <summary>
        /// Calculates the model matrix from transforms
        /// </summary>
        public void CalculateWorldMatrix()
        {
            if(Parent != null)
                WorldMatrix *= Parent.WorldMatrix;

            if(Name.StartsWith("ROOT_")) //Ignore root positions
            {
                WorldMatrix = WorldMatrix.ClearTranslation();
            }
        }
    }
}
