using Assimp;
using Assimp.Configs;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
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
        private static Dictionary<Node, HWEngineBurn> nodeEngineBurns = new Dictionary<Node, HWEngineBurn>();

        private static Node[] lodNodes;
        private static Node colNode;
        private static Node infoNode;
        private static Node holdDockNode;

        private static bool goblinWarningShown;

        public static void ImportFromFile(string path)
        {
            nodeJoints.Clear();
            nodeDockpaths.Clear();
            nodeEngineBurns.Clear();

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
                    string jointName = "";

                    Dictionary<string, string> values = ParseNameParameters(assimpNode.Name, new string[] { "JNT" });
                    foreach (KeyValuePair<string, string> pair in values.ToArray())
                    {
                        switch (pair.Key)
                        {
                            case "JNT":
                                jointName = pair.Value;
                                break;
                        }
                    }

                    if (jointName == "")
                    {
                        new Problem(ProblemTypes.ERROR, "Failed to parse name of joint \"" + assimpNode.Name + "\".");
                        failed = true;
                    }

                    if (!failed)
                    {
                        HWJoint newJoint = new HWJoint(jointName, parentJoint, GetAssimpNodeAbsoluteTransform(assimpNode));
                        nodeJoints.Add(assimpNode, newJoint);
                    }
                }
            }
            else if (assimpNode.Name.StartsWith("MARK")) //If node is a marker
            {
                HWJoint parentJoint = GetNextJointParent(assimpNode);

                string markerName = "";

                Dictionary<string, string> values = ParseNameParameters(assimpNode.Name, new string[] { "MARK" });
                foreach (KeyValuePair<string, string> pair in values.ToArray())
                {
                    switch (pair.Key)
                    {
                        case "MARK":
                            markerName = pair.Value;
                            break;
                    }
                }

                if (markerName == "")
                {
                    new Problem(ProblemTypes.ERROR, "Failed to parse name of marker \"" + assimpNode.Name + "\".");
                    failed = true;
                }

                if(!failed)
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
                    string pathName = "";
                    string[] families = new string[0];
                    string[] links = new string[0];
                    List<DockpathFlag> flags = new List<DockpathFlag>();
                    int animationIndex = 0;

                    bool success = false;
                    Dictionary<string, string> values = ParseNameParameters(assimpNode.Name, new string[] { "DOCK", "Fam", "Link", "Flags", "MAD" });
                    foreach(KeyValuePair<string, string> pair in values.ToArray())
                    {
                        switch (pair.Key)
                        {
                            case "DOCK":
                                pathName = pair.Value;
                                break;
                            case "Fam":
                                families = pair.Value.Replace(" ", "").Split(',');
                                break;
                            case "Link":
                                if (pair.Value.Trim().Length > 0)
                                    links = pair.Value.Replace(" ", "").Split(',');
                                break;
                            case "Flags":
                                string[] flagsStrings = pair.Value.Split(' ');
                                foreach (string flag in flagsStrings)
                                {
                                    if (flag.Length > 0) //If FLAGS is not empty
                                    {
                                        DockpathFlag newFlag;
                                        success = Enum.TryParse(flag, true, out newFlag);

                                        //Check if flag is valid
                                        if (success)
                                            flags.Add(newFlag);
                                        else
                                        {
                                            new Problem(ProblemTypes.WARNING, "Unknown dockpath flag \"" + flag + "\" on dockpath \"" + pathName + "\".");
                                            failed = true;
                                        }
                                    }
                                }
                                break;
                            case "MAD":
                                success = int.TryParse(pair.Value, out animationIndex);
                                if (!success)
                                {
                                    new Problem(ProblemTypes.WARNING, "Failed to parse MAD-index \"" + pair.Value + "\" on dockpath \"" + pathName + "\".");
                                    failed = true;
                                }
                                break;
                        }
                    }

                    if (!failed)
                    {
                        HWDockpath newDockpath = new HWDockpath(pathName, families, links, flags, animationIndex);
                        nodeDockpaths.Add(assimpNode, newDockpath);
                    }
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
                    float tolerance = -1;
                    float speed = -1;
                    List<DockSegmentFlag> flags = new List<DockSegmentFlag>();

                    bool success = false;
                    Dictionary<string, string> values = ParseNameParameters(assimpNode.Name, new string[] { "SEG", "Tol", "Spd", "Flags",  });
                    foreach (KeyValuePair<string, string> pair in values.ToArray())
                    {
                        switch (pair.Key)
                        {
                            case "SEG":
                                int.TryParse(pair.Value, out id);
                                break;
                            case "Tol":
                                float.TryParse(pair.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out tolerance);
                                break;
                            case "Spd":
                                float.TryParse(pair.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out speed);
                                break;
                            case "Flags":
                                string[] flagsStrings = pair.Value.Split(' ');
                                foreach (string flag in flagsStrings)
                                {
                                    if (flag.Length > 0) //If FLAGS is not empty
                                    {
                                        DockSegmentFlag newFlag;
                                        success = Enum.TryParse(flag, true, out newFlag);

                                        //Check if flag is valid
                                        if (success)
                                            flags.Add(newFlag);
                                        else
                                        {
                                            new Problem(ProblemTypes.WARNING, "Unknown flag \"" + flag + "\" in dockpath segment \"" + assimpNode.Name + "\".");
                                            failed = true;
                                        }
                                    }
                                }
                                break;
                        }
                    }

                    if (id == -1)
                    {
                        new Problem(ProblemTypes.ERROR, "Failed to parse ID of dockpath segment \"" + assimpNode.Name + "\".");
                        failed = true;
                    }

                    if (tolerance == -1)
                    {
                        new Problem(ProblemTypes.ERROR, "Failed to parse tolerance of dockpath segment \"" + assimpNode.Name + "\".");
                        failed = true;
                    }

                    if (speed == -1)
                    {
                        new Problem(ProblemTypes.ERROR, "Failed to parse speed of dockpath segment \"" + assimpNode.Name + "\".");
                        failed = true;
                    }

                    if(!failed)
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
                float size = -1;
                float phase = -1;
                float frequency = -1;
                Vector3 color = new Vector3(168, 123, 945);
                float distance = -1;
                List<NavLightFlag> flags = new List<NavLightFlag>();

                bool success = false;
                Dictionary<string, string> values = ParseNameParameters(assimpNode.Name, new string[] { "NAVL", "Type", "Sz", "Ph", "Fr", "Col", "Dist", "Flags" });
                foreach (KeyValuePair<string, string> pair in values.ToArray())
                {
                    switch (pair.Key)
                    {
                        case "NAVL":
                            lightName = pair.Value;
                            break;
                        case "Type":
                            type = pair.Value;
                            break;
                        case "Sz":
                            float.TryParse(pair.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out size);
                            break;
                        case "Ph":
                            float.TryParse(pair.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out phase);
                            break;
                        case "Fr":
                            float.TryParse(pair.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out frequency);
                            break;
                        case "Col":
                            string[] channels = pair.Value.Split(',');
                            if (channels.Length < 3)
                            {
                                failed = true;
                                break;
                            }

                            float red, green, blue = 1;
                            float.TryParse(channels[0], NumberStyles.Float, CultureInfo.InvariantCulture, out red);
                            float.TryParse(channels[1], NumberStyles.Float, CultureInfo.InvariantCulture, out green);
                            float.TryParse(channels[2], NumberStyles.Float, CultureInfo.InvariantCulture, out blue);
                            color = new Vector3(red, green, blue);
                            break;
                        case "Dist":
                            float.TryParse(pair.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out distance);
                            break;
                        case "Flags":
                            string[] flagsStrings = pair.Value.Split(' ');
                            foreach (string flag in flagsStrings)
                            {
                                if (flag.Length > 0) //If FLAGS is not empty
                                {
                                    NavLightFlag newFlag;
                                    success = Enum.TryParse(flag, true, out newFlag);

                                    //Check if flag is valid
                                    if (success)
                                        flags.Add(newFlag);
                                    else
                                    {
                                        new Problem(ProblemTypes.WARNING, "Unknown flag \"" + flag + "\" in navlight \"" + assimpNode.Name + "\".");
                                        failed = true;
                                    }
                                }
                            }
                            break;
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
                if (lightName == "")
                {
                    new Problem(ProblemTypes.ERROR, "Failed to parse name of navlight \"" + assimpNode.Name + "\".");
                    failed = true;
                }
                if (type == "")
                {
                    new Problem(ProblemTypes.ERROR, "Failed to parse type of navlight \"" + assimpNode.Name + "\".");
                    failed = true;
                }
                if (size == -1)
                {
                    new Problem(ProblemTypes.ERROR, "Failed to parse size of navlight \"" + assimpNode.Name + "\".");
                    failed = true;
                }
                if (phase == -1)
                {
                    new Problem(ProblemTypes.ERROR, "Failed to parse phase of navlight \"" + assimpNode.Name + "\".");
                    failed = true;
                }
                if (frequency == -1)
                {
                    new Problem(ProblemTypes.ERROR, "Failed to parse frequency of navlight \"" + assimpNode.Name + "\".");
                    failed = true;
                }
                if (color == new Vector3(168, 123, 945))
                {
                    new Problem(ProblemTypes.ERROR, "Failed to parse color of navlight \"" + assimpNode.Name + "\".");
                    failed = true;
                }
                if (distance == -1)
                {
                    new Problem(ProblemTypes.ERROR, "Failed to parse distance of navlight \"" + assimpNode.Name + "\".");
                    failed = true;
                }

                if (!failed)
                    new HWNavLight(lightName, parentJoint, GetAssimpNodeTransform(assimpNode), navLightStyle, size, phase, frequency, color, distance, flags);
            }
            #endregion
            #region EngineBurn
            else if (assimpNode.Name.StartsWith("BURN"))
            {
                HWJoint parentJoint = GetNextJointParent(assimpNode);

                if (!failed)
                {
                    string burnName = "";

                    Dictionary<string, string> values = ParseNameParameters(assimpNode.Name, new string[] { "BURN" });
                    foreach (KeyValuePair<string, string> pair in values.ToArray())
                    {
                        switch (pair.Key)
                        {
                            case "BURN":
                                burnName = pair.Value;
                                break;
                        }
                    }

                    if (burnName == "")
                    {
                        new Problem(ProblemTypes.ERROR, "Failed to parse name of engine burn \"" + assimpNode.Name + "\".");
                        failed = true;
                    }

                    if (!failed)
                    {
                        HWEngineBurn newBurn = new HWEngineBurn(burnName, parentJoint, GetAssimpNodeTransform(assimpNode));
                        nodeEngineBurns.Add(assimpNode, newBurn);
                    }
                }
            }
            #endregion
            #region EngineFlame
            else if (assimpNode.Name.StartsWith("Flame"))
            {
                HWEngineBurn engineBurn = null;
                if (nodeEngineBurns.ContainsKey(assimpNode.Parent))
                    engineBurn = nodeEngineBurns[assimpNode.Parent];

                //Check if segment is child of engine burn
                if (engineBurn == null)
                {
                    new Problem(ProblemTypes.WARNING, "Engine burn flame \"" + assimpNode.Name + "\" is not a child of an engine burn.");
                    failed = true;
                }

                if (!failed)
                {
                    int spriteIndex = -1;
                    int divIndex = -1;

                    Dictionary<string, string> values = ParseNameParameters(assimpNode.Name, new string[] { "Flame", "Div" });
                    foreach (KeyValuePair<string, string> pair in values.ToArray())
                    {
                        switch (pair.Key)
                        {
                            case "Flame":
                                int.TryParse(pair.Value, out spriteIndex);
                                break;
                            case "Div":
                                int.TryParse(pair.Value, out divIndex);
                                break;
                        }
                    }

                    if(spriteIndex == -1)
                    {
                        new Problem(ProblemTypes.ERROR, "Failed to parse sprite index of engine flame \"" + assimpNode.Name + "\".");
                        failed = true;
                    }
                    if (divIndex == -1)
                    {
                        new Problem(ProblemTypes.ERROR, "Failed to parse division index of engine flame \"" + assimpNode.Name + "\".");
                        failed = true;
                    }

                    if (!failed)
                    {
                        HWEngineFlame newFlame = new HWEngineFlame(engineBurn, GetAssimpNodeAbsoluteTransform(assimpNode), divIndex, spriteIndex);
                    }
                }
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

            bool success = false;
            Dictionary<string, string> values = ParseNameParameters(assimpMesh.Name, new string[] { "MULT", "LOD", "TAGS" });
            foreach (KeyValuePair<string, string> pair in values.ToArray())
            {
                switch (pair.Key)
                {
                    case "MULT":
                        name = pair.Value;
                        break;
                    case "LOD":
                        success = int.TryParse(pair.Value, out lod);
                        if (!success)
                        {
                            new Problem(ProblemTypes.ERROR, "Failed to parse LOD of ship mesh \"" + assimpMesh.Name + "\".");
                            return;
                        }
                        break;
                    case "TAGS":
                        string[] tagsStrings = pair.Value.Split(' ');
                        foreach (string tag in tagsStrings)
                        {
                            tags.Add((ShipMeshTag)Enum.Parse(typeof(ShipMeshTag), tag, true));
                        }
                        break;
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

            Dictionary<string, string> values = ParseNameParameters(assimpMesh.Name, new string[] { "COL" });
            foreach (KeyValuePair<string, string> pair in values.ToArray())
            {
                switch (pair.Key)
                {
                    case "COL":
                        name = pair.Value;
                        break;
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

            bool success = false;
            Dictionary<string, string> values = ParseNameParameters(assimpMesh.Name, new string[] { "GLOW", "LOD" });
            foreach (KeyValuePair<string, string> pair in values.ToArray())
            {
                switch (pair.Key)
                {
                    case "GLOW":
                        name = pair.Value;
                        break;
                    case "LOD":
                        success = int.TryParse(pair.Value, out lod);
                        if (!success)
                        {
                            new Problem(ProblemTypes.ERROR, "Failed to parse LOD of engine glow \"" + assimpMesh.Name + "\".");
                            return;
                        }
                        break;
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

            Dictionary<string, string> values = ParseNameParameters(assimpMesh.Name, new string[] { "ETSH" });
            foreach (KeyValuePair<string, string> pair in values.ToArray())
            {
                switch (pair.Key)
                {
                    case "ETSH":
                        name = pair.Value;
                        break;
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

        public static Dictionary<string, string> ParseNameParameters(string input, string[] parameters)
        {
            if (parameters.Length == 0 || input.Length == 0)
                return new Dictionary<string, string>();

            List<string> filteredParameters = new List<string>();

            //Sort the parameters
            int startIndex = 0;
            while (startIndex < input.Length)
            {
                int lowestIndex = input.Length;
                string lowestParameter = "";
                for (int i = 0; i < parameters.Length; i++)
                {
                    int newIndex = input.IndexOf(parameters[i] + "[", startIndex);
                    if (newIndex != -1 && newIndex < lowestIndex)
                    {
                        lowestIndex = newIndex;
                        lowestParameter = parameters[i];
                    }
                }
                startIndex = lowestIndex + 5;
                if (lowestParameter != string.Empty)
                    if (!filteredParameters.Contains(lowestParameter))
                        filteredParameters.Add(lowestParameter);
                    else
                        new Problem(ProblemTypes.WARNING, "Multiple parameter \"" + lowestParameter + "\" in \"" + input + "\".");
            }

            Dictionary<string, string> result = new Dictionary<string, string>();
            for (int i = 0; i < filteredParameters.Count; i++)
            {
                string pattern = "";

                if(filteredParameters.Count - 1 > i)
                    pattern = @"(?<=" + filteredParameters[i] + @"\[)(.*)(?=\]_" + filteredParameters[i + 1] + ")";
                else
                    pattern = @"(?<=" + filteredParameters[i] + @"\[)(.*)(?=\])";

                Match match = Regex.Match(input, pattern);

                if (match.Success)
                    result.Add(filteredParameters[i], match.Value);
                else
                    new Problem(ProblemTypes.ERROR, "Failed to parse parameter \"" + filteredParameters[i] + "\" of \"" + input + "\".");
            }
            return result;
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
                        HWImage.Parse(name, path);
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
