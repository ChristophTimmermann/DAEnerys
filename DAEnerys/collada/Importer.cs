using Assimp;
using Assimp.Configs;
using OpenTK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;

namespace DAEnerys
{
    public static class Importer
    {
        public static string ColladaPath;
        public static Scene Collada;

        private static Dictionary<Node, HWJoint> nodeJoints = new Dictionary<Node, HWJoint>();
        private static Dictionary<Node, HWDockpath> nodeDockpaths = new Dictionary<Node, HWDockpath>();

        private static Node[] lodNodes;
        private static Node colNode;
        private static Node infoNode;
        private static Node holdDockNode;

        private static bool goblinWarningShown;

        public static void ImportFromFile(string path)
        {
            nodeJoints.Clear();
            nodeDockpaths.Clear();

            lodNodes = new Node[4];
            colNode = null;
            infoNode = null;
            holdDockNode = null;

            goblinWarningShown = false;

            #region Import
            AssimpContext importer = new AssimpContext();
            NormalSmoothingAngleConfig config = new NormalSmoothingAngleConfig(80.0f);
            importer.SetConfig(config);

            LogStream logStream = new LogStream(delegate (string msg, string userData)
            {
                Console.WriteLine(msg);
            });
            logStream.Attach();

            string fileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), path);
            ColladaPath = Path.GetDirectoryName(path);

            //Blender Homeworld Toolkit fix
            string fixedColladaPath = FixCollada(fileName);

            Collada = importer.ImportFile(fixedColladaPath, ~(PostProcessSteps.CalculateTangentSpace | PostProcessSteps.GenerateNormals) & (PostProcessPreset.TargetRealTimeFast));
            importer.Dispose();

            //Manual parsing
            LoadTextures(fixedColladaPath);

            //File.Delete(fixedColladaPath);
            #endregion

            Program.Camera.Zoom = 0; //Set camera zoom to 0 for bounding box calculations to set it

            LoadMaterials();

            ParseNode(Collada.RootNode);

            foreach (HWShipMesh shipMesh in HWShipMesh.ShipMeshes) //Set all LOD0 meshes visible by default
            {
                foreach (HWShipMeshLOD shipMeshLOD in shipMesh.LODMeshes[0])
                {
                    shipMeshLOD.Visible = true;
                }
            }

            foreach (HWEngineGlow engineGlow in HWEngineGlow.EngineGlows) //Set all LOD0 engine glows visible by default
            {
                foreach (HWEngineGlowLOD engineGlowLOD in engineGlow.LODMeshes[0])
                {
                    engineGlowLOD.Visible = true;
                }
            }

            HWScene.CalibrateSettings();
            HWScene.CheckForProblems();
            HWEngineGlow.UpdateEngineStrength();
            Program.main.UpdateProblems();

            Renderer.InvalidateMeshData();
            Renderer.InvalidateView();
            Renderer.Invalidate();
            logStream.Detach();
        }

        public static Matrix4 GetAssimpNodeTransform(Node assimpNode)
        {
            Matrix4 transform = new Matrix4(assimpNode.Transform.A1, assimpNode.Transform.B1, assimpNode.Transform.C1, assimpNode.Transform.D1, assimpNode.Transform.A2, assimpNode.Transform.B2, assimpNode.Transform.C2, assimpNode.Transform.D2, assimpNode.Transform.A3, assimpNode.Transform.B3, assimpNode.Transform.C3, assimpNode.Transform.D3, assimpNode.Transform.A4, assimpNode.Transform.B4, assimpNode.Transform.C4, assimpNode.Transform.D4);
            transform = transform.ClearScale(); //Ignore scale
            return transform;
        }
        private static Matrix4 GetAssimpNodeAbsoluteTransform(Node assimpNode)
        {
            Matrix4 transform = GetAssimpNodeTransform(assimpNode);
            Node assimpParent = assimpNode.Parent;

            //Get the list of all transforms to apply to this joint
            List<Matrix4> parentTransforms = new List<Matrix4>();
            while (assimpParent != null)
            {
                //Don't add joint transforms to this
                if (nodeJoints.ContainsKey(assimpParent) || assimpParent == Collada.RootNode)
                {
                    assimpParent = assimpParent.Parent;
                    continue;
                }

                Matrix4 parentTransform = GetAssimpNodeTransform(assimpParent);
                if (lodNodes.Contains(assimpParent) || assimpParent == colNode) //If ROOT_XXX
                    parentTransform = parentTransform.ClearTranslation(); //Ignore root translation

                parentTransforms.Add(parentTransform);
                assimpParent = assimpParent.Parent;
            }
            parentTransforms.Reverse();

            //Actually apply the transforms
            foreach (Matrix4 parentTranslation in parentTransforms)
                transform *= parentTranslation;

            return transform;
        }
        private static bool IsAssimpNodeDescendantOf(Node assimpNode, Node assimpParent)
        {
            Node nodeChecking = assimpNode.Parent;
            while (nodeChecking != null)
            {
                if (nodeChecking == assimpParent)
                    return true;

                nodeChecking = nodeChecking.Parent;
            }

            return false;
        }
        private static bool IsAssimpNodeUnderAnyRootNode(Node assimpNode)
        {
            for (int i = 0; i < 3; i++)
                if (IsAssimpNodeDescendantOf(assimpNode, lodNodes[i]))
                    return true;

            return false;
        }
        private static HWJoint GetNextJointParent(Node assimpNode)
        {
            Node assimpParent = assimpNode.Parent;
            HWJoint jointParent = HWJoint.Root;

            while (assimpParent != null)
            {
                if (nodeJoints.ContainsKey(assimpParent))
                {
                    jointParent = nodeJoints[assimpParent];
                    break;
                }

                assimpParent = assimpParent.Parent;
            }

            return jointParent;
        }

        public static void ParseNode(Node assimpNode)
        {
            bool failed = false;

            if (assimpNode.Name.StartsWith("ROOT_LOD")) //If node is a root LOD node
            {
                string[] split = assimpNode.Name.Split('[');

                if (split.Length > 1)
                {
                    string lodString = split[1];
                    lodString = lodString.Remove(lodString.Length - 1);

                    int lod = 0;
                    bool success = int.TryParse(lodString, out lod);

                    if (success)
                    {
                        if (lodNodes[lod] != null)
                            new Problem(ProblemTypes.ERROR, "There are multiple \"ROOT_LOD[" + lod + "]\" nodes.");

                        lodNodes[lod] = assimpNode;

                        if (lod == 0)
                        {
                            Matrix4 transform = GetAssimpNodeAbsoluteTransform(assimpNode);
                            transform.ClearTranslation();
                            OpenTK.Quaternion rot = transform.ExtractRotation();
                            transform *= Matrix4.CreateFromQuaternion(rot.Inverted());
                            HWJoint.Root.RelativeWorldMatrix = transform;
                            HWJoint.Root.CalculateWorldMatrix();

                            nodeJoints.Add(assimpNode, HWJoint.Root);
                        }
                    }
                }
            }
            else if (assimpNode.Name.StartsWith("ROOT_COL")) //If node is a root COL node
            {
                if (colNode != null)
                    new Problem(ProblemTypes.ERROR, "There are multiple \"ROOT_COL\" nodes.");

                colNode = assimpNode;
            }
            else if (assimpNode.Name.StartsWith("ROOT_INFO")) //If node is a root INFO node
            {
                if (infoNode != null)
                    new Problem(ProblemTypes.ERROR, "There are multiple \"ROOT_INFO\" nodes.");

                infoNode = assimpNode;
            }
            else if (assimpNode.Name == "HOLD_DOCK") //If node is the holder for dockpaths
            {
                if (holdDockNode != null)
                    new Problem(ProblemTypes.ERROR, "There are multiple \"HOLD_DOCK\" nodes.");

                holdDockNode = assimpNode;
            }
            else if (assimpNode.Name.StartsWith("JNT")) //If node is a joint
            {
                if (!IsAssimpNodeUnderAnyRootNode(assimpNode))
                {
                    new Problem(ProblemTypes.ERROR, "The joint \"" + assimpNode.Name + "\" is not under any \"ROOT_LOD[X]\" node.");
                    failed = true;
                }

                if (!failed)
                {
                    HWJoint parentJoint = GetNextJointParent(assimpNode);

                    string[] splitted = assimpNode.Name.Split('[');
                    int end = splitted[1].IndexOf(']');
                    string jointName = splitted[1].Substring(0, end);

                    HWJoint newJoint = new HWJoint(jointName, parentJoint, GetAssimpNodeAbsoluteTransform(assimpNode));
                    nodeJoints.Add(assimpNode, newJoint);
                }
            }
            else if (assimpNode.Name.StartsWith("MARK")) //If node is a marker
            {
                HWJoint parentJoint = GetNextJointParent(assimpNode);

                string[] splitted = assimpNode.Name.Split('[');
                int end = splitted[1].IndexOf(']');
                string markerName = splitted[1].Substring(0, end);

                new HWMarker(markerName, parentJoint, GetAssimpNodeAbsoluteTransform(assimpNode));
            }
            #region Dockpath
            else if (assimpNode.Name.StartsWith("DOCK")) //If node is a dockpath
            {
                if (assimpNode.Parent != holdDockNode)
                {
                    new Problem(ProblemTypes.ERROR, "The dockpath \"" + assimpNode.Name + "\" is not under the HOLD_DOCK node.");
                    failed = true;
                }

                if (!failed)
                {
                    HWJoint parentJoint = GetNextJointParent(assimpNode);

                    string pathName = "";
                    string[] families = new string[0];
                    string[] links = new string[0];
                    List<DockpathFlag> flags = new List<DockpathFlag>();

                    string[] splitted = assimpNode.Name.Split('[');
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
                                if (linksString.Trim().Length > 0)
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

                    HWDockpath newDockpath = new HWDockpath(parentJoint, pathName, families, links, flags);
                    nodeDockpaths.Add(assimpNode, newDockpath);
                }
            }
            #endregion

            #region DockSegment
            else if (assimpNode.Name.StartsWith("SEG")) //If node is a docksegment
            {
                HWDockpath dockpath = null;
                if (nodeDockpaths.ContainsKey(assimpNode.Parent))
                    dockpath = nodeDockpaths[assimpNode.Parent];

                //Check if segment is child of dockpath
                if (dockpath == null)
                {
                    new Problem(ProblemTypes.WARNING, "Dockpath segment \"" + assimpNode.Name + "\" is not a child of a dockpath.");
                    failed = true;
                }

                if (!failed)
                {
                    int id = -1;
                    float tolerance = 0;
                    float speed = 0;
                    List<DockSegmentFlag> flags = new List<DockSegmentFlag>();

                    string[] splitted = assimpNode.Name.Split('_');
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
                                    new Problem(ProblemTypes.WARNING, "Unknown flag \"" + flag + "\" in dockpath segment \"" + assimpNode.Name + "\".");
                            }
                        }
                    }

                    new HWDockSegment(dockpath, GetAssimpNodeAbsoluteTransform(assimpNode), id, tolerance, speed, flags);
                }
            }
            #endregion

            #region NavLight
            else if (assimpNode.Name.StartsWith("NAVL")) //If node is a navlight
            {
                HWJoint parentJoint = GetNextJointParent(assimpNode);

                string lightName = "";
                string type = "default";
                float size = 0;
                float phase = 0;
                float frequency = 0;
                Vector3 color = Vector3.One;
                float distance = 0;
                List<NavLightFlag> flags = new List<NavLightFlag>();

                string[] splitted = assimpNode.Name.Split('[');
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
                foreach (HWNavLightStyle style in HWData.NavLightStyles)
                {
                    if (style.Name == type)
                    {
                        navLightStyle = style;
                        break;
                    }
                }

                if (navLightStyle == null)
                {
                    new Problem(ProblemTypes.WARNING, "Navlight style \"" + type + "\" not found. Skipping navlight \"" + lightName + "\".");
                    failed = true;
                }

                if(!failed)
                    new HWNavLight(lightName, parentJoint, GetAssimpNodeTransform(assimpNode), navLightStyle, size, phase, frequency, color, distance, flags);
            }
            #endregion
            else if (assimpNode.Name.StartsWith("MULT"))
            {
                Matrix4 transform = GetAssimpNodeAbsoluteTransform(assimpNode);
                HWJoint parentJoint = GetNextJointParent(assimpNode);

                foreach (int meshIndex in assimpNode.MeshIndices)
                    ParseShipMesh(Collada.Meshes[meshIndex], assimpNode, parentJoint);
            }
            else if (assimpNode.Name.StartsWith("GOBG")) //deprecated
            {
                if (!goblinWarningShown)
                {
                    new Problem(ProblemTypes.ERROR, "Goblins detected, remove them or your game will crash.");
                    goblinWarningShown = true;
                }
            }
            else if (assimpNode.Name.StartsWith("COL"))
            {
                Matrix4 transform = GetAssimpNodeAbsoluteTransform(assimpNode);
                HWJoint parentJoint = GetNextJointParent(assimpNode);

                foreach (int meshIndex in assimpNode.MeshIndices)
                    ParseCollisionMesh(Collada.Meshes[meshIndex], assimpNode, parentJoint);
            }
            else if (assimpNode.Name.StartsWith("GLOW"))
            {
                Matrix4 transform = GetAssimpNodeAbsoluteTransform(assimpNode);
                HWJoint parentJoint = GetNextJointParent(assimpNode);

                foreach (int meshIndex in assimpNode.MeshIndices)
                    ParseEngineGlow(Collada.Meshes[meshIndex], assimpNode, parentJoint);
            }
            else if (assimpNode.Name.StartsWith("ETSH"))
            {
                Matrix4 transform = GetAssimpNodeAbsoluteTransform(assimpNode);
                HWJoint parentJoint = GetNextJointParent(assimpNode);

                foreach (int meshIndex in assimpNode.MeshIndices)
                    ParseEngineShape(Collada.Meshes[meshIndex], assimpNode, parentJoint);
            }

            foreach (Node childNode in assimpNode.Children)
                ParseNode(childNode);
        }

        private static void ParseShipMesh(Mesh assimpMesh, Node assimpNode, HWJoint parentJoint)
        {
            string name = "";
            int lod = 0;
            List<ShipMeshTag> tags = new List<ShipMeshTag>();

            string[] splitted = assimpMesh.Name.Split('[');
            int end = -1;
            for (int i = 0; i < splitted.Length; i++)
            {
                if (i != 0)
                {
                    end = splitted[i].IndexOf(']');
                    if (splitted[i - 1].EndsWith("MULT")) //Name
                    {
                        name = splitted[i].Substring(0, end);
                    }
                    else if (splitted[i - 1].EndsWith("LOD")) //Level of detail
                    {
                        bool success = int.TryParse(splitted[i].Substring(0, end), out lod);
                        if (!success)
                        {
                            new Problem(ProblemTypes.ERROR, "Failed to parse LOD of ship mesh \"" + assimpMesh.Name + "\".");
                            return;
                        }
                    }
                    else if (splitted[i - 1].EndsWith("TAGS")) //Tags
                    {
                        string tagsString = splitted[i].Substring(0, end);
                        string[] tagsStrings = tagsString.Split(' ');

                        foreach (string tag in tagsStrings)
                        {
                            tags.Add((ShipMeshTag)Enum.Parse(typeof(ShipMeshTag), tag.ToUpper()));
                        }
                    }
                }
            }

            if (name == "")
            {
                new Problem(ProblemTypes.ERROR, "Failed to parse name of ship mesh \"" + assimpMesh.Name + "\".");
                return;
            }

            if (!IsAssimpNodeDescendantOf(assimpNode, lodNodes[lod]))
            {
                new Problem(ProblemTypes.WARNING, "Ship mesh \"" + assimpMesh.Name + "\" is marked with LOD " + lod + ", but is not under \"ROOT_LOD[" + lod + "]\".");
                return;
            }

            HWShipMesh newShipMesh = null;
            foreach (HWShipMesh shipMesh in HWShipMesh.ShipMeshes)
            {
                if (shipMesh.Name == name)
                {
                    newShipMesh = shipMesh;
                    break;
                }
            }

            if (newShipMesh == null) //If a ship mesh does not already exist with that name
                newShipMesh = new HWShipMesh(parentJoint, name, tags);
            else if (lod == 0)
                newShipMesh.Parent = parentJoint;

            HWMaterial material = null;
            if (HWMaterial.Materials[assimpMesh.MaterialIndex] != null)
                if (HWMaterial.Materials[assimpMesh.MaterialIndex].Valid)
                    material = HWMaterial.Materials[assimpMesh.MaterialIndex];

            if (material == null)
                material = new HWMaterial();

            HWShipMeshLOD newLOD = new HWShipMeshLOD(ParseAssimpMesh(assimpMesh), GetAssimpNodeAbsoluteTransform(assimpNode), material, newShipMesh, lod);
        }
        private static void ParseCollisionMesh(Mesh assimpMesh, Node assimpNode, HWJoint parentJoint)
        {
            string name = "";

            string[] splitted = assimpMesh.Name.Split('[');
            int end = -1;
            for (int i = 0; i < splitted.Length; i++)
            {
                if (i != 0)
                {
                    end = splitted[i].IndexOf(']');
                    if (splitted[i - 1].EndsWith("COL")) //Name
                    {
                        name = splitted[i].Substring(0, end);
                    }
                }
            }

            if (name == "")
            {
                new Problem(ProblemTypes.ERROR, "Failed to parse name of collision mesh \"" + assimpMesh.Name + "\".");
                return;
            }

            HWCollisionMesh newCollisionMesh = new HWCollisionMesh(ParseAssimpMesh(assimpMesh), GetAssimpNodeTransform(assimpNode), parentJoint, name);
        }
        private static void ParseEngineGlow(Mesh assimpMesh, Node assimpNode, HWJoint parentJoint)
        {
            string name = "";
            int lod = -1;

            string[] splitted = assimpMesh.Name.Split('[');
            int end = -1;
            for (int i = 0; i < splitted.Length; i++)
            {
                if (i != 0)
                {
                    end = splitted[i].IndexOf(']');
                    if (splitted[i - 1].EndsWith("GLOW")) //Name
                    {
                        name = splitted[i].Substring(0, end);
                    }
                    else if (splitted[i - 1].EndsWith("LOD")) //Level of detail
                    {
                        bool success = int.TryParse(splitted[i].Substring(0, end), out lod);
                        if (!success)
                        {
                            new Problem(ProblemTypes.ERROR, "Failed to parse LOD of engine glow \"" + assimpMesh.Name + "\".");
                        }
                    }
                }
            }

            if (name == "")
            {
                new Problem(ProblemTypes.ERROR, "Failed to parse name of engine glow \"" + assimpMesh.Name + "\".");
                return;
            }

            if (!IsAssimpNodeDescendantOf(assimpNode, lodNodes[lod]))
            {
                new Problem(ProblemTypes.WARNING, "Engine glow \"" + assimpMesh.Name + "\" is marked with LOD " + lod + ", but is not under \"ROOT_LOD[" + lod + "]\".");
                return;
            }

            HWEngineGlow newGlowMesh = null;
            foreach (HWEngineGlow glowMesh in HWEngineGlow.EngineGlows)
            {
                if (glowMesh.Name == name)
                {
                    newGlowMesh = glowMesh;
                    break;
                }
            }

            if (newGlowMesh == null) //If a glow mesh does not already exist with that name
                newGlowMesh = new HWEngineGlow(parentJoint, name);
            else if(lod == 0)
                newGlowMesh.Parent = parentJoint;

            HWEngineGlowLOD newLOD = new HWEngineGlowLOD(ParseAssimpMesh(assimpMesh), GetAssimpNodeTransform(assimpNode), newGlowMesh, lod);
        }
        private static void ParseEngineShape(Mesh assimpMesh, Node assimpNode, HWJoint parentJoint)
        {
            string name = "";

            string[] splitted = assimpMesh.Name.Split('[');
            int end = -1;
            for (int i = 0; i < splitted.Length; i++)
            {
                if (i != 0)
                {
                    end = splitted[i].IndexOf(']');
                    if (splitted[i - 1].EndsWith("ETSH")) //Name
                    {
                        name = splitted[i].Substring(0, end);
                    }
                }
            }

            if (name == "")
            {
                new Problem(ProblemTypes.ERROR, "Failed to parse name of engine shape \"" + assimpMesh.Name + "\".");
                return;
            }

            HWEngineShape newEngineShape = new HWEngineShape(ParseAssimpMesh(assimpMesh), GetAssimpNodeTransform(assimpNode), parentJoint, name);
        }

        public static MeshData ParseAssimpMesh(Mesh assimpMesh)
        {
            List<Vertex> vertexList = new List<Vertex>();

            for (int i = 0; i < assimpMesh.VertexCount; ++i)
            {
                Vertex vertex = new Vertex();
                vertex.Position = new Vector3(assimpMesh.Vertices[i].X, assimpMesh.Vertices[i].Y, assimpMesh.Vertices[i].Z);
                if (assimpMesh.Normals.Count - 1 >= i)
                    vertex.Normal = new Vector3(assimpMesh.Normals[i].X, assimpMesh.Normals[i].Y, assimpMesh.Normals[i].Z);
                vertex.Color = Vector3.One; //Meh

                if (assimpMesh.TextureCoordinateChannelCount > 0)
                    vertex.UV0 = new Vector2(assimpMesh.TextureCoordinateChannels[0][i].X, assimpMesh.TextureCoordinateChannels[0][i].Y);

                if(assimpMesh.TextureCoordinateChannelCount > 1)
                    vertex.UV1 = new Vector2(assimpMesh.TextureCoordinateChannels[1][i].X, assimpMesh.TextureCoordinateChannels[1][i].Y);

                if (assimpMesh.HasTangentBasis)
                {
                    vertex.Tangent = new Vector3(assimpMesh.Tangents[i].X, assimpMesh.Tangents[i].Y, assimpMesh.Tangents[i].Z);
                    vertex.BiTangent = new Vector3(assimpMesh.BiTangents[i].X, assimpMesh.BiTangents[i].Y, assimpMesh.BiTangents[i].Z);
                }
                vertexList.Add(vertex);
            }

            return new MeshData(vertexList.ToArray(), assimpMesh.GetIndices(), assimpMesh.TextureCoordinateChannelCount);
        }

        private static void LoadMaterials()
        {
            foreach (Material material in Collada.Materials)
            {
                Log.WriteLine("Trying to parse material \"" + material.Name + "\".");

                HWMaterial newMaterial = new HWMaterial();

                newMaterial.Name = material.Name;

                if (!newMaterial.Name.StartsWith("MAT["))
                    newMaterial.Valid = false;

                newMaterial.DiffusePath = material.TextureDiffuse.FilePath;

                newMaterial.Parse();
            }
        }

        private static void LoadTextures(string file)
        {
            XmlReader reader = XmlReader.Create(file);

            while (reader.Read())
            {
                if (reader.Name == "image")
                {
                    string name = reader.GetAttribute("name");
                    string path = null;
                    if (name != null)
                    {
                        while (reader.Read())
                        {
                            if (reader.Name == "init_from")
                            {
                                reader.MoveToElement();
                                path = reader.ReadElementContentAsString();
                                break;
                            }
                        }
                    }

                    if (name != null && path != null)
                    {
                        Log.WriteLine("Trying to parse texture \"" + name + "\".");
                        new HWImage(name, path);
                    }
                }

                //Check for problems with texture names in diffuse slots (Crashes HODOR without any information)
                if (reader.Name == "diffuse")
                {
                    reader.ReadToDescendant("texture");
                    string name = "";

                    if (reader.Name == "texture")
                    {
                        name = reader.GetAttribute("texture").Replace("-image", "");
                    }

                    if (name.Length > 0)
                    {
                        if (!name.StartsWith("IMG[")) //Not a very good check (I guess)...
                            new Problem(ProblemTypes.ERROR, "Diffuse texture \"" + name + "\" has the wrong name format. This will most likely crash HODOR.");
                    }
                }
            }

            reader.Dispose();
        }

        private static string FixCollada(string path)
        {
            XDocument doc = XDocument.Load(path);
            XNamespace ns = doc.Root.GetDefaultNamespace();

            foreach (XElement element in doc.Descendants())
            {
                if (element.Name == ns + "color")
                    element.SetValue(element.Value + "1.0");
            }

            File.WriteAllText("colladaBlenderFix.dae", doc.ToString());
            return "colladaBlenderFix.dae";
        }
    }
}
