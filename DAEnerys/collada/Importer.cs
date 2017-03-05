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
        public static float Framerate = 30f;

        private static XDocument doc;
        private static XNamespace ns;

        private static List<string> visualSceneInstances = new List<string>();
        private static List<COLLADANode> rootNodes = new List<COLLADANode>();

        private static Dictionary<COLLADANode, HWJoint> nodeJoints = new Dictionary<COLLADANode, HWJoint>();
        private static Dictionary<COLLADANode, HWDockpath> nodeDockpaths = new Dictionary<COLLADANode, HWDockpath>();
        private static Dictionary<COLLADANode, HWEngineBurn> nodeEngineBurns = new Dictionary<COLLADANode, HWEngineBurn>();

        private static Dictionary<HWJoint, string> jointColladaNames = new Dictionary<HWJoint, string>();

        private static List<COLLADAAnimation> parsedAnimations = new List<COLLADAAnimation>();
        private static List<COLLADAJointAnimation> parsedJointAnimations = new List<COLLADAJointAnimation>();

        private static Queue<COLQueueItem> collisionMeshQueue = new Queue<COLQueueItem>();

        private static COLLADANode[] lodNodes;
        private static COLLADANode colNode;
        private static COLLADANode infoNode;
        private static COLLADANode holdDockNode;
        private static COLLADANode holdAnimNode;
        private static COLLADANode holdParamsNode;

        private static bool goblinWarningShown;

        public static void ImportFromFile(string path)
        {
            doc = null;
            ns = null;

            nodeJoints.Clear();
            nodeDockpaths.Clear();
            nodeEngineBurns.Clear();

            Framerate = 30f;

            jointColladaNames.Clear();

            parsedAnimations.Clear();
            parsedJointAnimations.Clear();

            collisionMeshQueue.Clear();

            visualSceneInstances.Clear();
            rootNodes.Clear();

            lodNodes = new COLLADANode[4];
            colNode = null;
            infoNode = null;
            holdDockNode = null;
            holdAnimNode = null;
            holdParamsNode = null;

            goblinWarningShown = false;

            #region Import
            AssimpContext importer = new AssimpContext();
            NormalSmoothingAngleConfig config = new NormalSmoothingAngleConfig(80.0f);
            importer.SetConfig(config);

            LogStream logStream = new LogStream(delegate (string msg, string userData)
            {
                Log.WriteLine(msg);
            });
            logStream.Attach();

            string fileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), path);
            ColladaPath = Path.GetDirectoryName(path);

            //Blender Homeworld Toolkit fix
            string fixedColladaPath = FixCollada(fileName);

            //Collada = importer.ImportFile(fixedColladaPath, ~(PostProcessSteps.CalculateTangentSpace | PostProcessSteps.GenerateNormals) & (PostProcessPreset.TargetRealTimeFast));
            Collada = importer.ImportFile(fixedColladaPath, PostProcessSteps.JoinIdenticalVertices | PostProcessSteps.Triangulate | PostProcessSteps.ValidateDataStructure);
            importer.Dispose();

            //Manual parsing
            ManualParsing(fixedColladaPath);

            //File.Delete(fixedColladaPath);
            #endregion

            Program.Camera.ZoomTarget = 0; //Set camera zoom to 0 for bounding box calculations to set it

            LoadMaterials();

            ManualParseNodes();

            foreach (COLLADANode rootNode in rootNodes)
                ParseNode(rootNode);

            HandleQueues();

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

            LoadAnimationData();

            HWScene.CalibrateSettings();
            HWScene.CheckForProblems();
            HWEngineGlow.UpdateEngineStrength();
            HWAnimation.UpdateAnimatedJoints();
            Program.main.UpdateProblems();

            Renderer.InvalidateMeshData();
            Renderer.InvalidateView();
            Renderer.Invalidate();
            logStream.Detach();
        }

        public static Mesh[] ImportMeshesFromFile(string path)
        {
            AssimpContext importer = new AssimpContext();
            NormalSmoothingAngleConfig config = new NormalSmoothingAngleConfig(80.0f);
            importer.SetConfig(config);
            LogStream logStream = new LogStream(delegate (string msg, string userData)
            {
                Log.WriteLine(msg);
            });
            logStream.Attach();

            Scene dae = importer.ImportFile(path, PostProcessSteps.JoinIdenticalVertices | PostProcessSteps.Triangulate | PostProcessSteps.ValidateDataStructure);
            importer.Dispose();

            return dae.Meshes.ToArray();
        }

        public static Mesh ImportMeshFromFile(string path)
        {
            AssimpContext importer = new AssimpContext();
            NormalSmoothingAngleConfig config = new NormalSmoothingAngleConfig(80.0f);
            importer.SetConfig(config);
            LogStream logStream = new LogStream(delegate (string msg, string userData)
            {
                Log.WriteLine(msg);
            });
            logStream.Attach();

            Scene dae = importer.ImportFile(path, PostProcessSteps.JoinIdenticalVertices | PostProcessSteps.Triangulate | PostProcessSteps.ValidateDataStructure);
            importer.Dispose();

            return dae.Meshes[0];
        }

        public static COLLADATransform GetColladaNodeTransform(COLLADANode colladaNode)
        {
            return colladaNode.Transform;
        }

        private static COLLADATransform GetColladaNodeAbsoluteTransform(COLLADANode colladaNode)
        {
            Matrix4 transform = colladaNode.Transform.GetMatrix();
            COLLADANode colladaParent = colladaNode.Parent;

            //Get the list of all transforms to apply to this joint
            List<Matrix4> parentTransforms = new List<Matrix4>();
            while (colladaParent != null)
            {
                //Don't add joint transforms to this
                if (nodeJoints.ContainsKey(colladaParent) || colladaParent.AssimpNode == Collada.RootNode)
                {
                    colladaParent = colladaParent.Parent;
                    continue;
                }

                Matrix4 parentTransform = colladaParent.Transform.GetMatrix();
                if (lodNodes.Contains(colladaParent) || colladaParent == colNode) //If ROOT_XXX
                    parentTransform = parentTransform.ClearTranslation(); //Ignore root translation

                parentTransforms.Add(parentTransform);
                colladaParent = colladaParent.Parent;
            }
            parentTransforms.Reverse();

            //Actually apply the transforms
            foreach (Matrix4 parentTranslation in parentTransforms)
                transform *= parentTranslation;

            Vector3 pos = transform.ExtractTranslation();
            OpenTK.Quaternion quat = transform.ExtractRotation();
            Vector3 rot = Extensions.Utilities.ToEulerAngles(quat);
            Vector3 scale = transform.ExtractScale();

            return new COLLADATransform(pos, rot, scale);
        }
        private static bool IsColladaNodeDescendantOf(COLLADANode colladaNode, COLLADANode colladaParent)
        {
            COLLADANode nodeChecking = colladaNode.Parent;
            while (nodeChecking != null)
            {
                if (nodeChecking == colladaParent)
                    return true;

                nodeChecking = nodeChecking.Parent;
            }

            return false;
        }
        private static bool IsColladaNodeUnderAnyRootNode(COLLADANode colladaNode)
        {
            for (int i = 0; i < 3; i++)
                if (IsColladaNodeDescendantOf(colladaNode, lodNodes[i]))
                    return true;

            return false;
        }
        private static HWJoint GetNextJointParent(COLLADANode colladaNode)
        {
            COLLADANode colladaParent = colladaNode.Parent;
            HWJoint jointParent = HWJoint.Root;

            while (colladaParent != null)
            {
                if (nodeJoints.ContainsKey(colladaParent))
                {
                    jointParent = nodeJoints[colladaParent];
                    break;
                }

                colladaParent = colladaParent.Parent;
            }

            return jointParent;
        }

        public static void ParseNode(COLLADANode colladaNode)
        {
            bool failed = false;

            //Check for parameters in child nodes
            COLLADANode subParams = null;
            foreach (COLLADANode childNode in colladaNode.Children)
                if (childNode.Name.StartsWith("SUB_PARAMS"))
                {
                    subParams = childNode;
                    break;
                }
            if(subParams != null)
                foreach (COLLADANode paramNode in subParams.Children)
                {
                    string addedParam = paramNode.Name.Substring(0, paramNode.Name.LastIndexOf(']') + 1);
                    colladaNode.Name += "_" + addedParam;
                }

            if (colladaNode.Name.StartsWith("ROOT_LOD")) //If node is a root LOD node
            {
                string[] split = colladaNode.Name.Split('[');

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

                        lodNodes[lod] = colladaNode;

                        if (lod == 0)
                        {
                            COLLADATransform transform = GetColladaNodeTransform(colladaNode);
                            transform.Position = Vector3.Zero;
 
                            //OpenTK.Quaternion rot = transform.ExtractRotation();
                            //transform *= Matrix4.CreateFromQuaternion(rot.Inverted());
                            HWJoint.Root.LocalWorldMatrix = transform.GetMatrix();
                            HWJoint.Root.CalculateWorldMatrix();

                            nodeJoints.Add(colladaNode, HWJoint.Root);
                        }
                    }
                }
            }
            else if (colladaNode.Name.StartsWith("ROOT_COL")) //If node is a root COL node
            {
                if (colNode != null)
                    new Problem(ProblemTypes.ERROR, "There are multiple \"ROOT_COL\" nodes.");

                colNode = colladaNode;
            }
            else if (colladaNode.Name.StartsWith("ROOT_INFO")) //If node is a root INFO node
            {
                if (infoNode != null)
                    new Problem(ProblemTypes.ERROR, "There are multiple \"ROOT_INFO\" nodes.");

                infoNode = colladaNode;
            }
            else if (colladaNode.Name == "HOLD_DOCK") //If node is the holder for dockpaths
            {
                if (holdDockNode != null)
                    new Problem(ProblemTypes.ERROR, "There are multiple \"HOLD_DOCK\" nodes.");

                holdDockNode = colladaNode;
            }
            else if (colladaNode.Name == "HOLD_ANIM") //If node is the holder for animations
            {
                if (holdAnimNode != null)
                    new Problem(ProblemTypes.ERROR, "There are multiple \"HOLD_ANIM\" nodes.");

                holdAnimNode = colladaNode;
            }
            else if (colladaNode.Name == "HOLD_PARAMS") //If node is the holder for parameters
            {
                if (holdParamsNode != null)
                    new Problem(ProblemTypes.ERROR, "There are multiple \"HOLD_PARAMS\" nodes.");

                holdParamsNode = colladaNode;
            }
            else if (colladaNode.Name.StartsWith("JNT")) //If node is a joint
            {
                if (!IsColladaNodeUnderAnyRootNode(colladaNode))
                {
                    new Problem(ProblemTypes.ERROR, "The joint \"" + colladaNode.Name + "\" is not under any \"ROOT_LOD[X]\" node.");
                    failed = true;
                }

                if (!failed)
                {
                    HWJoint parentJoint = GetNextJointParent(colladaNode);
                    string jointName = "";

                    Dictionary<string, string> values = ParseNameParameters(colladaNode.Name, new string[] { "JNT" });
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
                        new Problem(ProblemTypes.ERROR, "Failed to parse name of joint \"" + colladaNode.Name + "\".");
                        failed = true;
                    }

                    if (!failed)
                    {
                        COLLADATransform transform = GetColladaNodeTransform(colladaNode);

                        HWJoint newJoint = new HWJoint(jointName, parentJoint, transform.Position, transform.Rotation, transform.Scale);
                        nodeJoints.Add(colladaNode, newJoint);
                        jointColladaNames.Add(newJoint, colladaNode.Name);
                    }
                }
            }
            else if (colladaNode.Name.StartsWith("MARK")) //If node is a marker
            {
                HWJoint parentJoint = GetNextJointParent(colladaNode);

                string markerName = "";

                Dictionary<string, string> values = ParseNameParameters(colladaNode.Name, new string[] { "MARK" });
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
                    new Problem(ProblemTypes.ERROR, "Failed to parse name of marker \"" + colladaNode.Name + "\".");
                    failed = true;
                }

                if (!failed)
                {
                    COLLADATransform transform = GetColladaNodeTransform(colladaNode);
                    HWMarker newMarker = new HWMarker(markerName, parentJoint, transform.Position, transform.Rotation, transform.Scale);
                }
            }
            #region Dockpath
            else if (colladaNode.Name.StartsWith("DOCK")) //If node is a dockpath
            {
                if (colladaNode.Parent != holdDockNode)
                {
                    new Problem(ProblemTypes.ERROR, "The dockpath \"" + colladaNode.Name + "\" is not under the \"HOLD_DOCK\" node.");
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
                    Dictionary<string, string> values = ParseNameParameters(colladaNode.Name, new string[] { "DOCK", "Fam", "Link", "Flags", "MAD" });
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
                        HWDockpath newDockpath = new HWDockpath(pathName, families, links, flags.ToArray(), animationIndex);
                        nodeDockpaths.Add(colladaNode, newDockpath);
                    }
                }
            }
            #endregion

            #region DockSegment
            else if (colladaNode.Name.StartsWith("SEG")) //If node is a docksegment
            {
                HWDockpath dockpath = null;
                if (nodeDockpaths.ContainsKey(colladaNode.Parent))
                    dockpath = nodeDockpaths[colladaNode.Parent];

                //Check if segment is child of dockpath
                if (dockpath == null)
                {
                    new Problem(ProblemTypes.WARNING, "Dockpath segment \"" + colladaNode.Name + "\" is not a child of a dockpath.");
                    failed = true;
                }

                if (!failed)
                {
                    int id = -1;
                    float tolerance = -1;
                    float speed = -1;
                    List<DockSegmentFlag> flags = new List<DockSegmentFlag>();

                    bool success = false;
                    Dictionary<string, string> values = ParseNameParameters(colladaNode.Name, new string[] { "SEG", "Tol", "Spd", "Flags",  });
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
                                            new Problem(ProblemTypes.WARNING, "Unknown flag \"" + flag + "\" in dockpath segment \"" + colladaNode.Name + "\".");
                                            failed = true;
                                        }
                                    }
                                }
                                break;
                        }
                    }

                    if (id == -1)
                    {
                        new Problem(ProblemTypes.ERROR, "Failed to parse ID of dockpath segment \"" + colladaNode.Name + "\".");
                        failed = true;
                    }

                    if (tolerance == -1)
                    {
                        new Problem(ProblemTypes.ERROR, "Failed to parse tolerance of dockpath segment \"" + colladaNode.Name + "\".");
                        failed = true;
                    }

                    if (speed == -1)
                    {
                        new Problem(ProblemTypes.ERROR, "Failed to parse speed of dockpath segment \"" + colladaNode.Name + "\".");
                        failed = true;
                    }

                    if (!failed)
                    {
                        COLLADATransform transform = GetColladaNodeTransform(colladaNode);
                        new HWDockSegment(dockpath, transform.Position, transform.Rotation, id, tolerance, speed, flags.ToArray());
                    }
                }
            }
            #endregion

            #region Parameter
            else if (colladaNode.Name.StartsWith("MAT")) //If node is a material parameter
            {
                if (colladaNode.Parent != holdParamsNode)
                {
                    new Problem(ProblemTypes.ERROR, "The material parameter \"" + colladaNode.Name + "\" is not under the \"HOLD_PARAMS\" node.");
                    failed = true;
                }

                if (!failed)
                {
                    string materialName = "";
                    string name = "";
                    HWParameter.ParameterType type = HWParameter.ParameterType.RGBA;
                    float[] data = new float[0];

                    bool success = false;
                    Dictionary<string, string> values = ParseNameParameters(colladaNode.Name, new string[] { "MAT", "PARAM", "Type", "Data"});
                    foreach (KeyValuePair<string, string> pair in values.ToArray())
                    {
                        switch (pair.Key)
                        {
                            case "MAT":
                                materialName = pair.Value;
                                break;
                            case "PARAM":
                                name = pair.Value;
                                break;
                            case "Type":
                                HWParameter.ParameterType newType;
                                success = Enum.TryParse(pair.Value, true, out newType);

                                //Check if type is valid
                                if (success)
                                    type = newType;
                                else
                                {
                                    new Problem(ProblemTypes.WARNING, "Unknown material parameter type \"" + type + "\" on parameter \"" + colladaNode.Name + "\".");
                                    failed = true;
                                }
                                break;
                            case "Data":
                                string[] dataStrings = pair.Value.Split(',');
                                List<float> dataList = new List<float>();
                                foreach (string dataString in dataStrings)
                                {
                                    if (dataString.Length > 0) //If FLAGS is not empty
                                    {
                                        float value = 0;
                                        float.TryParse(dataString, NumberStyles.Float, CultureInfo.InvariantCulture, out value);
                                        dataList.Add(value);
                                    }
                                }
                                data = dataList.ToArray();
                                break;
                        }
                    }

                    if (!failed)
                    {
                        HWParameter newParameter = new HWParameter(materialName, name, type, data);
                    }
                }
            }
            #endregion

            #region NavLight
            else if (colladaNode.Name.StartsWith("NAVL")) //If node is a navlight
            {
                HWJoint parentJoint = GetNextJointParent(colladaNode);

                string lightName = "";
                string type = "default";
                float size = 1;
                float phase = 0;
                float frequency = 1;
                Vector3 color = new Vector3(255, 255, 255);
                float distance = 0.001f;
                List<NavLightFlag> flags = new List<NavLightFlag>();
                int sect = 0;

                bool success = false;
                Dictionary<string, string> values = ParseNameParameters(colladaNode.Name, new string[] { "NAVL", "Type", "Sz", "Ph", "Fr", "Col", "Dist", "Flags", "Sect" });
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
                                        new Problem(ProblemTypes.WARNING, "Unknown flag \"" + flag + "\" in navlight \"" + colladaNode.Name + "\".");
                                        //failed = true;
                                    }
                                }
                            }
                            break;
                        case "Sect":
                            int.TryParse(pair.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out sect);
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
                    new Problem(ProblemTypes.ERROR, "Failed to parse name of navlight \"" + colladaNode.Name + "\". Skipping navlight.");
                    failed = true;
                }
                /*if (type == "")
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
                }*/

                if (!failed)
                    new HWNavLight(lightName, parentJoint, colladaNode.Transform.Position, navLightStyle, size, phase, frequency, color, distance, flags, sect);
            }
            #endregion
            #region EngineBurn
            else if (colladaNode.Name.StartsWith("BURN"))
            {
                HWJoint parentJoint = GetNextJointParent(colladaNode);

                if (!failed)
                {
                    string burnName = "";

                    Dictionary<string, string> values = ParseNameParameters(colladaNode.Name, new string[] { "BURN" });
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
                        new Problem(ProblemTypes.ERROR, "Failed to parse name of engine burn \"" + colladaNode.Name + "\".");
                        failed = true;
                    }

                    if (!failed)
                    {
                        HWEngineBurn newBurn = new HWEngineBurn(burnName, parentJoint, colladaNode.Transform.Position, colladaNode.Transform.Rotation, colladaNode.Transform.Scale);
                        nodeEngineBurns.Add(colladaNode, newBurn);
                    }
                }
            }
            #endregion
            #region EngineFlame
            else if (colladaNode.Name.StartsWith("Flame"))
            {
                HWEngineBurn engineBurn = null;
                if (nodeEngineBurns.ContainsKey(colladaNode.Parent))
                    engineBurn = nodeEngineBurns[colladaNode.Parent];

                //Check if segment is child of engine burn
                if (engineBurn == null)
                {
                    new Problem(ProblemTypes.WARNING, "Engine burn flame \"" + colladaNode.Name + "\" is not a child of an engine burn.");
                    failed = true;
                }

                if (!failed)
                {
                    int spriteIndex = -1;
                    int divIndex = -1;

                    Dictionary<string, string> values = ParseNameParameters(colladaNode.Name, new string[] { "Flame", "Div" });
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
                        new Problem(ProblemTypes.ERROR, "Failed to parse sprite index of engine flame \"" + colladaNode.Name + "\".");
                        failed = true;
                    }
                    if (divIndex == -1)
                    {
                        new Problem(ProblemTypes.ERROR, "Failed to parse division index of engine flame \"" + colladaNode.Name + "\".");
                        failed = true;
                    }

                    if (!failed)
                    {
                        COLLADATransform transform = GetColladaNodeTransform(colladaNode);
                        HWEngineFlame newFlame = new HWEngineFlame(engineBurn, transform.Position, transform.Rotation, transform.Scale, divIndex, spriteIndex);
                    }
                }
            }
            #endregion
            else if (colladaNode.Name.StartsWith("MULT"))
            {
                HWJoint parentJoint = GetNextJointParent(colladaNode);

                foreach (int meshIndex in colladaNode.MeshIndices)
                    ParseShipMesh(Collada.Meshes[meshIndex], colladaNode, parentJoint);
            }
            else if (colladaNode.Name.StartsWith("GOBG")) //deprecated
            {
                if (!goblinWarningShown)
                {
                    new Problem(ProblemTypes.ERROR, "Goblins detected, remove them or your game will crash.");
                    goblinWarningShown = true;
                }
            }
            else if (colladaNode.Name.StartsWith("COL"))
            {
                foreach (int meshIndex in colladaNode.MeshIndices)
                {
                    //Because collision meshes have to be parsed after all joints
                    collisionMeshQueue.Enqueue(new COLQueueItem(Collada.Meshes[meshIndex], colladaNode));
                }
            }
            else if (colladaNode.Name.StartsWith("GLOW"))
            {
                HWJoint parentJoint = GetNextJointParent(colladaNode);

                foreach (int meshIndex in colladaNode.MeshIndices)
                    ParseEngineGlow(Collada.Meshes[meshIndex], colladaNode, parentJoint);
            }
            else if (colladaNode.Name.StartsWith("ETSH"))
            {
                HWJoint parentJoint = GetNextJointParent(colladaNode);

                foreach (int meshIndex in colladaNode.MeshIndices)
                    ParseEngineShape(Collada.Meshes[meshIndex], colladaNode, parentJoint);
            }
            else if (colladaNode.Name.StartsWith("ANIM")) //If node is an animation
            {
                if (colladaNode.Parent != holdAnimNode)
                {
                    new Problem(ProblemTypes.ERROR, "The animation \"" + colladaNode.Name + "\" is not under the \"HOLD_ANIM\" node.");
                    failed = true;
                }

                if (!failed)
                {
                    string animName = "";
                    float startTime = 0;
                    int startFrame = 0;
                    float endTime = 0;
                    int endFrame = 0;
                    float loopStartTime = 0;
                    int loopStartFrame = 0;
                    float loopEndTime = 0;
                    int loopEndFrame = 0;
                    AnimationType type = AnimationType.TIME;

                    Dictionary<string, string> values = ParseNameParameters(colladaNode.Name, new string[] { "ANIM", "ST", "STF", "EN", "ENF", "LS", "LSF", "LE", "LEF" });
                    foreach (KeyValuePair<string, string> pair in values.ToArray())
                    {
                        switch (pair.Key)
                        {
                            case "ANIM":
                                animName = pair.Value;
                                break;
                            case "ST":
                                float.TryParse(pair.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out startTime);
                                type = AnimationType.TIME;
                                break;
                            case "STF":
                                int.TryParse(pair.Value, out startFrame);
                                type = AnimationType.FRAME;
                                break;
                            case "EN":
                                float.TryParse(pair.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out endTime);
                                break;
                            case "ENF":
                                int.TryParse(pair.Value, out endFrame);
                                break;
                            case "LS":
                                float.TryParse(pair.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out loopStartTime);
                                break;
                            case "LSF":
                                int.TryParse(pair.Value, out loopStartFrame);
                                break;
                            case "LE":
                                float.TryParse(pair.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out loopEndTime);
                                break;
                            case "LEF":
                                int.TryParse(pair.Value, out loopEndFrame);
                                break;
                        }
                    }

                    if (animName == "")
                    {
                        new Problem(ProblemTypes.ERROR, "Failed to parse name of animation \"" + colladaNode.Name + "\".");
                        failed = true;
                    }

                    if (!failed)
                    {
                        HWAnimation newAnim = new HWAnimation(animName, startTime, startFrame, endTime, endFrame, loopStartTime, loopStartFrame, loopEndTime, loopEndFrame, type);
                    }
                }
            }

            foreach (COLLADANode childNode in colladaNode.Children)
                ParseNode(childNode);
        }

        private static void ParseShipMesh(Mesh assimpMesh, COLLADANode colladaNode, HWJoint parentJoint)
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

            if (!IsColladaNodeDescendantOf(colladaNode, lodNodes[lod]))
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
                material = HWMaterial.DefaultMaterial;

            COLLADATransform transform = GetColladaNodeTransform(colladaNode);
            HWShipMeshLOD newLOD = new HWShipMeshLOD(ParseAssimpMesh(assimpMesh), transform.Position, transform.Rotation, transform.Scale, material, newShipMesh, lod);
        }
        private static void ParseCollisionMesh(Mesh assimpMesh, COLLADANode colladaNode)
        {
            string parent = "";

            Dictionary<string, string> values = ParseNameParameters(assimpMesh.Name, new string[] { "COL" });
            foreach (KeyValuePair<string, string> pair in values.ToArray())
            {
                switch (pair.Key)
                {
                    case "COL":
                        parent = pair.Value;
                        break;
                }
            }

            if (parent == "")
            {
                new Problem(ProblemTypes.ERROR, "Failed to parse parent of collision mesh \"" + assimpMesh.Name + "\".");
                return;
            }

            HWJoint parentJoint = HWJoint.GetByName(parent);
            if (parentJoint == null)
            {
                new Problem(ProblemTypes.ERROR, "Parent joint \"" + parent + "\" of collision mesh  \"" + assimpMesh.Name + "\" does not exist. Resetting to root");
                parentJoint = HWJoint.Root;
            }

            HWCollisionMesh newCollisionMesh = new HWCollisionMesh(ParseAssimpMesh(assimpMesh), colladaNode.Transform.Position, colladaNode.Transform.Rotation, colladaNode.Transform.Scale, parentJoint);
        }
        private static void ParseEngineGlow(Mesh assimpMesh, COLLADANode colladaNode, HWJoint parentJoint)
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

            if (!IsColladaNodeDescendantOf(colladaNode, lodNodes[lod]))
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

            HWEngineGlowLOD newLOD = new HWEngineGlowLOD(ParseAssimpMesh(assimpMesh), colladaNode.Transform.Position, colladaNode.Transform.Rotation, colladaNode.Transform.Scale, newGlowMesh, lod);
        }
        private static void ParseEngineShape(Mesh assimpMesh, COLLADANode colladaNode, HWJoint parentJoint)
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

            HWEngineShape newEngineShape = new HWEngineShape(ParseAssimpMesh(assimpMesh), colladaNode.Transform.Position, colladaNode.Transform.Rotation, colladaNode.Transform.Scale, parentJoint, name);
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
                if (assimpMesh.VertexColorChannelCount > 0)
                    vertex.Color = new Vector3(assimpMesh.VertexColorChannels[0][i].R, assimpMesh.VertexColorChannels[0][i].G, assimpMesh.VertexColorChannels[0][i].B);

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

                string otherPath = string.Empty;
                if(material.TextureDiffuse.FilePath != null)
                    otherPath = material.TextureDiffuse.FilePath;
                otherPath = otherPath.Replace("file://", "");
                otherPath = otherPath.TrimStart(new char[] { '\\', '/' });
                string absolutePath = System.IO.Path.Combine(Importer.ColladaPath, otherPath);
                absolutePath = System.IO.Path.GetFullPath(absolutePath);
                newMaterial.DiffusePath = absolutePath;

                newMaterial.Parse();
            }
        }

        private static void LoadAnimationData()
        {
            if (parsedJointAnimations.Count == 0)
                return;

            foreach (COLLADAJointAnimation jointAnim in parsedJointAnimations)
            {
                HWJoint animJoint = null;
                foreach (HWJoint joint in HWJoint.Joints)
                {
                    if (joint == HWJoint.Root)
                        continue;

                    if (jointColladaNames[joint] == jointAnim.Name)
                    {
                        animJoint = joint;
                        break;
                    }
                }

                if (animJoint == null)
                    continue;

                HWAnimationChannel positionChannel = new HWAnimationChannel();
                HWAnimationChannel rotationChannel = new HWAnimationChannel();
                HWAnimationChannel scalingChannel = new HWAnimationChannel();

                for (int i = 0; i < 3; i++)
                {
                    HWAnimationAxis axis = new HWAnimationAxis();
                    positionChannel.Axes[i] = axis;
                    axis = new HWAnimationAxis();
                    rotationChannel.Axes[i] = axis;
                    axis = new HWAnimationAxis();
                    scalingChannel.Axes[i] = axis;
                }

                COLLADAJointAnimation colladaJointAnim = null;
                foreach (COLLADAJointAnimation colJointAnim in parsedJointAnimations)
                {
                    if (colJointAnim.Name == jointColladaNames[animJoint])
                    {
                        colladaJointAnim = colJointAnim;
                        break;
                    }
                }

                if (colladaJointAnim == null)
                    continue;

                //TRANSLATION
                for (int i = 0; i < 3; i++)
                {
                    if (colladaJointAnim.Translation[i] == null)
                        continue;

                    positionChannel.Axes[i].Times = colladaJointAnim.Translation[i].Times;
                    positionChannel.Axes[i].Values = colladaJointAnim.Translation[i].Values;
                    positionChannel.Axes[i].KeyInterpolationTypes = colladaJointAnim.Translation[i].Interpolations;
                    positionChannel.Axes[i].InTangents = colladaJointAnim.Translation[i].InTangents;
                    positionChannel.Axes[i].OutTangents = colladaJointAnim.Translation[i].OutTangents;
                }

                //ROTATION
                for (int i = 0; i < 3; i++)
                {
                    if (colladaJointAnim.Rotation[i] == null)
                        continue;

                    rotationChannel.Axes[i].Times = colladaJointAnim.Rotation[i].Times;
                    rotationChannel.Axes[i].Values = colladaJointAnim.Rotation[i].Values;
                    rotationChannel.Axes[i].KeyInterpolationTypes = colladaJointAnim.Rotation[i].Interpolations;
                    rotationChannel.Axes[i].InTangents = colladaJointAnim.Rotation[i].InTangents;
                    rotationChannel.Axes[i].OutTangents = colladaJointAnim.Rotation[i].OutTangents;
                }

                //SCALING
                for (int i = 0; i < 3; i++)
                {
                    if (colladaJointAnim.Scaling[i] == null)
                        continue;

                    scalingChannel.Axes[i].Times = colladaJointAnim.Scaling[i].Times;
                    scalingChannel.Axes[i].Values = colladaJointAnim.Scaling[i].Values;
                    scalingChannel.Axes[i].KeyInterpolationTypes = colladaJointAnim.Scaling[i].Interpolations;
                    scalingChannel.Axes[i].InTangents = colladaJointAnim.Scaling[i].InTangents;
                    scalingChannel.Axes[i].OutTangents = colladaJointAnim.Scaling[i].OutTangents;
                }

                animJoint.PositionChannel = positionChannel;
                animJoint.RotationChannel = rotationChannel;
                animJoint.ScalingChannel = scalingChannel;
            }
        }

        private static void ManualParsing(string file)
        {
            #region Textures
            using (XmlReader reader = XmlReader.Create(file))
            {
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

                                    string otherPath = path.Replace("file://", "");
                                    otherPath = otherPath.TrimStart(new char[] { '\\', '/' });
                                    string absolutePath = System.IO.Path.Combine(Importer.ColladaPath, otherPath);
                                    absolutePath = System.IO.Path.GetFullPath(absolutePath);
                                    path = absolutePath;

                                    break;
                                }
                            }
                        }

                        if (name != null && path != null)
                        {
                            #region Suffix checking (HODOR crash)
                            Dictionary<string, string> values = ParseNameParameters(name, new string[] { "IMG", "FMT" });
                            if (values.Values.Count > 0)
                            {
                                string textureName = values.Values.ElementAt(0);
                                string suffix = textureName.Substring(textureName.LastIndexOf('_') + 1);

                                MaterialSuffix parsedSuffix;
                                bool success = Enum.TryParse(suffix, out parsedSuffix);
                                success = Enum.IsDefined(typeof(MaterialSuffix), suffix);
                                if (!success)
                                    new Problem(ProblemTypes.ERROR, "Texture \"" + name + "\" has no (valid) suffix, this will most likely crash HODOR.");
                            }
                            #endregion

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
            }
            #endregion

            #region Scene
            List<XElement> scenes = doc.Descendants(ns + "scene").ToList();
            XElement scene = null;
            if (scenes.Count > 0)
                scene = scenes[0];

            if (scene != null)
            {
                List<XElement> visualSceneInstanceElements = scene.Descendants(ns + "instance_visual_scene").ToList();
                foreach(XElement visualSceneInstance in visualSceneInstanceElements)
                {
                    string url = visualSceneInstance.Attribute("url").Value;
                    if (url.Length > 0)
                    {
                        url = url.Substring(1);
                        visualSceneInstances.Add(url);
                    }
                }
            }
            #endregion

            #region Animations
            List<XElement> animLibraries = doc.Descendants(ns + "library_animations").ToList();
            XElement animLibrary = null;
            if (animLibraries.Count > 0)
                animLibrary = animLibraries[0];

            if (animLibrary != null)
            {
                List<XElement> animationElements = animLibrary.Descendants(ns + "animation").ToList();
                foreach (XElement animationElement in animationElements)
                {
                    COLLADAAnimation animation = new COLLADAAnimation();

                    List<XElement> sourceElements = animationElement.Descendants(ns + "source").ToList();
                    foreach (XElement sourceElement in sourceElements)
                    {
                        COLLADASource source = new COLLADASource();
                        source.ID = sourceElement.Attribute("id").Value;

                        animation.Sources.Add(source);

                        List<XElement> floatArrayElements = sourceElement.Descendants(ns + "float_array").ToList();
                        if (floatArrayElements.Count > 0)
                        {
                            XElement floatArrayElement = floatArrayElements[0];

                            COLLADAFloatArray floatArray = new COLLADAFloatArray();
                            floatArray.ID = floatArrayElement.Attribute("id").Value;
                            int.TryParse(floatArrayElement.Attribute("count").Value, out floatArray.Count);
                            floatArray.Value = floatArrayElement.Value;

                            source.FloatArray = floatArray;
                        }

                        List<XElement> nameArrayElements = sourceElement.Descendants(ns + "Name_array").ToList();
                        if (nameArrayElements.Count > 0)
                        {
                            XElement nameArrayElement = nameArrayElements[0];

                            COLLADANameArray nameArray = new COLLADANameArray();
                            nameArray.ID = nameArrayElement.Attribute("id").Value;
                            int.TryParse(nameArrayElement.Attribute("count").Value, out nameArray.Count);
                            nameArray.Value = nameArrayElement.Value;

                            source.NameArray = nameArray;
                        }
                    }

                    List<XElement> samplerElements = animationElement.Descendants(ns + "sampler").ToList();

                    if (samplerElements.Count > 0)
                    {
                        XElement samplerElement = samplerElements[0];

                        COLLADASampler sampler = new COLLADASampler();
                        sampler.ID = samplerElement.Attribute("id").Value;

                        animation.Sampler = sampler;
                        List<XElement> inputElements = samplerElement.Descendants(ns + "input").ToList();
                        foreach (XElement inputElement in inputElements)
                        {
                            COLLADAInput input = new COLLADAInput();
                            input.Semantic = inputElement.Attribute("semantic").Value;
                            input.Source = inputElement.Attribute("source").Value;

                            sampler.Inputs.Add(input);
                        }
                    }
                    else
                        continue;

                    foreach (COLLADAInput input in animation.Sampler.Inputs)
                    {
                        COLLADASource source = null;
                        foreach (COLLADASource animSource in animation.Sources)
                            if (animSource.ID == input.Source.Remove(0, 1))
                            {
                                source = animSource;
                                break;
                            }

                        if (source == null)
                            continue;

                        if (input.Semantic == "INTERPOLATION")
                        {
                            string[] values = source.NameArray.Value.Trim().Split(' ');
                            foreach (string value in values)
                            {
                                switch (value)
                                {
                                    case "BEZIER":
                                        animation.Interpolations.Add(AnimationInterpolation.BEZIER);
                                        break;
                                    default:
                                        animation.Interpolations.Add(AnimationInterpolation.LINEAR);
                                        break;
                                }
                            }
                        }
                        else if (input.Semantic == "INPUT")
                        {
                            string[] values = source.FloatArray.Value.Trim().Split(' ');
                            for (int i = 0; i < values.Length; i++)
                            {
                                float value = 0;
                                float.TryParse(values[i], NumberStyles.Float, CultureInfo.InvariantCulture, out value);
                                animation.Times.Add(value);
                            }
                        }
                        else if (input.Semantic == "OUTPUT")
                        {
                            string[] values = source.FloatArray.Value.Trim().Split(' ');
                            for (int i = 0; i < values.Length; i++)
                            {
                                float value = 0;
                                float.TryParse(values[i], NumberStyles.Float, CultureInfo.InvariantCulture, out value);
                                animation.Values.Add(value);
                            }
                        }
                        else if (input.Semantic == "IN_TANGENT")
                        {
                            string[] values = source.FloatArray.Value.Trim().Split(' ');
                            for (int i = 0; i < values.Length; i += 2)
                            {
                                float x, y = 0;
                                float.TryParse(values[i], NumberStyles.Float, CultureInfo.InvariantCulture, out x);
                                float.TryParse(values[i + 1], NumberStyles.Float, CultureInfo.InvariantCulture, out y);
                                animation.InTangents.Add(new Vector2(x, y));
                            }
                        }
                        else if (input.Semantic == "OUT_TANGENT")
                        {
                            string[] values = source.FloatArray.Value.Trim().Split(' ');
                            for (int i = 0; i < values.Length; i += 2)
                            {
                                float x, y = 0;
                                float.TryParse(values[i], NumberStyles.Float, CultureInfo.InvariantCulture, out x);
                                float.TryParse(values[i + 1], NumberStyles.Float, CultureInfo.InvariantCulture, out y);
                                animation.OutTangents.Add(new Vector2(x, y));
                            }
                        }
                    }

                    List<XElement> channelElements = animationElement.Descendants(ns + "channel").ToList();

                    if (channelElements.Count > 0)
                    {
                        XElement channelElement = channelElements[0];

                        COLLADAChannel channel = new COLLADAChannel();
                        channel.Target = channelElement.Attribute("target").Value;

                        animation.Channel = channel;

                        int lastSlash = channel.Target.LastIndexOf('/');
                        int lastDot = channel.Target.LastIndexOf('.');
                        string jointTarget = "";
                        string channelTarget = "";
                        string axisTarget = "";

                        jointTarget = channel.Target.Substring(0, lastSlash);

                        if (lastDot != -1)
                        {
                            channelTarget = channel.Target.Substring(lastSlash + 1, lastDot - lastSlash - 1);
                            axisTarget = channel.Target.Substring(lastDot + 1);
                        }
                        else //Matrix
                        {
                            channelTarget = channel.Target.Substring(lastSlash + 1);
                        }

                        COLLADAJointAnimation jointAnimation = null;

                        //Add to joint animation
                        foreach (COLLADAJointAnimation jointAnim in parsedJointAnimations)
                            if (jointAnim.Name == jointTarget)
                            {
                                jointAnimation = jointAnim;
                                break;
                            }

                        if (jointAnimation == null)
                        {
                            jointAnimation = new COLLADAJointAnimation();
                            jointAnimation.Name = jointTarget;
                        }

                        switch (channelTarget)
                        {
                            case "translate":
                                switch (axisTarget)
                                {
                                    case "X":
                                        jointAnimation.Translation[0] = animation;
                                        break;
                                    case "Y":
                                        jointAnimation.Translation[1] = animation;
                                        break;
                                    case "Z":
                                        jointAnimation.Translation[2] = animation;
                                        break;
                                }
                                break;
                            case "rotateX":
                                jointAnimation.Rotation[0] = animation;
                                break;
                            case "rotateY":
                                jointAnimation.Rotation[1] = animation;
                                break;
                            case "rotateZ":
                                jointAnimation.Rotation[2] = animation;
                                break;
                            case "matrix":

                                for (int i = 0; i < 3; i++)
                                {
                                    jointAnimation.Translation[i] = new COLLADAAnimation();
                                    jointAnimation.Translation[i].Times = animation.Times;
                                    jointAnimation.Translation[i].InTangents = animation.InTangents;
                                    jointAnimation.Translation[i].OutTangents = animation.OutTangents;
                                    jointAnimation.Translation[i].Interpolations = animation.Interpolations;

                                    jointAnimation.Rotation[i] = new COLLADAAnimation();
                                    jointAnimation.Rotation[i].Times = animation.Times;
                                    jointAnimation.Rotation[i].InTangents = animation.InTangents;
                                    jointAnimation.Rotation[i].OutTangents = animation.OutTangents;
                                    jointAnimation.Rotation[i].Interpolations = animation.Interpolations;
                                }

                                List<Matrix4> matrices = new List<Matrix4>();
                                for (int i = 0; i < animation.Values.Count; i += 4 * 4)
                                {
                                    if (animation.Values.Count - 1 < i + 15)
                                        break;

                                    Matrix4 matrix = new Matrix4(animation.Values[i], animation.Values[i + 1], animation.Values[i + 2], animation.Values[i + 3], animation.Values[i + 4], animation.Values[i + 5], animation.Values[i + 6], animation.Values[i + 7], animation.Values[i + 8], animation.Values[i + 9], animation.Values[i + 10], animation.Values[i + 11], animation.Values[i + 12], animation.Values[i + 13], animation.Values[i + 14], animation.Values[i + 15]);

                                    Vector3 pos = matrix.ExtractTranslation();
                                    jointAnimation.Translation[0].Values.Add(pos.X);
                                    jointAnimation.Translation[1].Values.Add(pos.Y);
                                    jointAnimation.Translation[2].Values.Add(pos.Z);

                                    OpenTK.Quaternion rot = matrix.ExtractRotation();
                                    Vector3 axis; float angle;
                                    rot.ToAxisAngle(out axis, out angle);
                                    axis *= angle;
                                    jointAnimation.Rotation[0].Values.Add(axis.X);
                                    jointAnimation.Rotation[1].Values.Add(axis.Y);
                                    jointAnimation.Rotation[2].Values.Add(axis.Z);
                                }
                                break;
                        }
                    }
                }
            }
            #endregion

            #region Framerate
            List<XElement> frameRates = doc.Descendants(ns + "frame_rate").ToList();
            XElement frameRate = null;
            if (frameRates.Count > 0)
                frameRate = frameRates[0];

            if (frameRate != null)
                float.TryParse(frameRate.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out Framerate);
            #endregion
        }

        private static void ManualParseNodes()
        {
            #region Nodes
            List<XElement> visualScenesLibraries = doc.Descendants(ns + "library_visual_scenes").ToList();
            XElement visualScenesLibrary = null;
            if (visualScenesLibraries.Count > 0)
                visualScenesLibrary = visualScenesLibraries[0];

            if (visualScenesLibrary != null)
            {
                List<XElement> visualSceneElements = visualScenesLibrary.Descendants(ns + "visual_scene").ToList();
                List<XElement> instancedVisualSceneElements = new List<XElement>();

                foreach (string visualSceneInstance in visualSceneInstances)
                {
                    foreach (XElement visualSceneElement in visualSceneElements)
                    {
                        if (visualSceneElement.Attribute("id").Value == visualSceneInstance)
                        {
                            instancedVisualSceneElements.Add(visualSceneElement);
                        }
                    }
                }

                foreach (XElement visualScene in instancedVisualSceneElements)
                {
                    List<XElement> rootNodeElements = visualScene.Elements(ns + "node").ToList();
                    foreach (XElement rootNode in rootNodeElements)
                        rootNodes.Add(ParseColladaNode(rootNode, null));
                }
            }
            #endregion
        }

        private static COLLADANode ParseColladaNode(XElement nodeElement, COLLADANode parentNode)
        {
            string name = "";
            string id = "";
            string sid = "";

            XAttribute attrib = nodeElement.Attribute("name");
            if(attrib != null)
                name = attrib.Value;
            attrib = nodeElement.Attribute("id");
            if (attrib != null)
                id = attrib.Value;
            attrib = nodeElement.Attribute("sid");
            if (attrib != null)
                sid = attrib.Value;

            Vector3 pos = Vector3.Zero;
            Vector3 rot = Vector3.Zero;
            Vector3 scale = Vector3.One;

            List<int> meshIndices = new List<int>();

            XElement translateElement = nodeElement.Element(ns + "translate");
            if (translateElement != null)
            {
                string value = translateElement.Value;
                string[] split = value.Trim().Split(' ');



                float posX = float.Parse(split[0], NumberStyles.Float, CultureInfo.InvariantCulture);
                float posY = float.Parse(split[1], NumberStyles.Float, CultureInfo.InvariantCulture);
                float posZ = float.Parse(split[2], NumberStyles.Float, CultureInfo.InvariantCulture);
                pos = new Vector3(posX, posY, posZ);
            }

            float x = 0;
            float y = 0;
            float z = 0;
            XElement[] rotateElements = nodeElement.Elements(ns + "rotate").ToArray();
            foreach (XElement rotateElement in rotateElements)
            {
                string rotateSID = "";
                XAttribute sidAttrib = rotateElement.Attribute("sid");
                if(sidAttrib != null)
                    rotateSID = sidAttrib.Value;

                if (rotateSID == "rotateX")
                {
                    string value = rotateElement.Value;
                    string[] split = value.Trim().Split(' ');
                    x = float.Parse(split[3], NumberStyles.Float, CultureInfo.InvariantCulture);
                    x = MathHelper.DegreesToRadians(x);
                }
                else if (rotateSID == "rotateY")
                {
                    string value = rotateElement.Value;
                    string[] split = value.Trim().Split(' ');
                    y = float.Parse(split[3], NumberStyles.Float, CultureInfo.InvariantCulture);
                    y = MathHelper.DegreesToRadians(y);
                }
                else if (rotateSID == "rotateZ")
                {
                    string value = rotateElement.Value;
                    string[] split = value.Trim().Split(' ');
                    z = float.Parse(split[3], NumberStyles.Float, CultureInfo.InvariantCulture);
                    z = MathHelper.DegreesToRadians(z);
                }
                else
                {
                    string value = rotateElement.Value;
                    string[] split = value.Trim().Split(' ');

                    x = float.Parse(split[0], NumberStyles.Float, CultureInfo.InvariantCulture);
                    y = float.Parse(split[1], NumberStyles.Float, CultureInfo.InvariantCulture);
                    z = float.Parse(split[2], NumberStyles.Float, CultureInfo.InvariantCulture);
                    float w = float.Parse(split[3], NumberStyles.Float, CultureInfo.InvariantCulture);

                    OpenTK.Quaternion quat = new OpenTK.Quaternion(x, y, z, w);
                    Vector3 axis; float angle;
                    quat.ToAxisAngle(out axis, out angle);
                    axis *= angle;

                    x = axis.X;
                    y = axis.Y;
                    z = axis.Z;
                }
            }
            rot = new Vector3(x, y, z);

            XElement matrixElement = nodeElement.Element(ns + "matrix");
            if (matrixElement != null)
            {
                string value = matrixElement.Value;
                string[] split = value.Trim().Split(' ');
                float[] values = new float[split.Length];
                for(int i = 0; i < split.Length; i++)
                    values[i] = float.Parse(split[i], NumberStyles.Float, CultureInfo.InvariantCulture);

                Matrix4 matrix = new Matrix4(values[0], values[1], values[2], values[3], values[4], values[5], values[6], values[7], values[8], values[9], values[10], values[11], values[12], values[13], values[14], values[15]);

                //pos = matrix.ExtractTranslation();
                pos = new Vector3(values[3], values[7], values[11]);

                OpenTK.Quaternion quat = matrix.ExtractRotation();
                Vector3 axis;
                float angle;
                quat.ToAxisAngle(out axis, out angle);
                axis *= angle;
                rot = axis;
                rot = Extensions.Utilities.Matrix4ToEuler(matrix);
            }

            XElement scaleElement = nodeElement.Element(ns + "scale");
            if (scaleElement != null)
            {
                if (scaleElement.Attribute("sid").Value == "scale")
                {
                    string value = scaleElement.Value;
                    string[] split = value.Trim().Split(' ');
                    x = float.Parse(split[0], NumberStyles.Float, CultureInfo.InvariantCulture);
                    y = float.Parse(split[1], NumberStyles.Float, CultureInfo.InvariantCulture);
                    z = float.Parse(split[2], NumberStyles.Float, CultureInfo.InvariantCulture);
                    //scale = new Vector3(x, y, z);
                }
            }

            Node assimpNode = Collada.RootNode.FindNode(name);
            if (assimpNode != null)
                meshIndices = assimpNode.MeshIndices;

            /*XElement[] geometryInstanceElements = nodeElement.Elements(ns + "instance_geometry").ToArray();
            foreach (XElement geometryInstanceElement in geometryInstanceElements)
            {
                string url = geometryInstanceElement.Attribute("url").Value;
                List<string> materialTargets = new List<string>();

                XElement bindMaterialElement = geometryInstanceElement.Element(ns + "bind_material");
                if(bindMaterialElement != null)
                {
                    XElement techniqueCommonElement = bindMaterialElement.Element(ns + "technique_common");
                    if (techniqueCommonElement != null)
                    {
                        XElement[] instanceMaterialElements = techniqueCommonElement.Elements(ns + "instance_material").ToArray();
                        foreach (XElement instanceMaterialElement in instanceMaterialElements)
                        {
                            string target = instanceMaterialElement.Attribute("target").Value;
                            if (target == string.Empty)
                                continue;

                            materialTargets.Add(target);
                        }
                    }
                }
            }*/

            COLLADANode newNode = new COLLADANode(name, new COLLADATransform(pos, rot, scale), meshIndices.ToArray(), parentNode, assimpNode);
            XElement[] childElements = nodeElement.Elements(ns + "node").ToArray();
            foreach (XElement childElement in childElements)
                ParseColladaNode(childElement, newNode);

            return newNode;
        }

        private static void HandleQueues()
        {
            while(collisionMeshQueue.Count > 0)
            {
                COLQueueItem item = collisionMeshQueue.Dequeue();
                ParseCollisionMesh(item.AssimpMesh, item.ColladaNode);
            }
        }

        public class COLLADATransform
        {
            public Vector3 Position;
            public Vector3 Rotation;
            public Vector3 Scale;

            public COLLADATransform(Vector3 pos, Vector3 rot, Vector3 scale)
            {
                Position = pos;
                Rotation = rot;
                Scale = scale;
            }

            public Matrix4 GetMatrix()
            {
                Matrix4 matrix = Matrix4.CreateRotationX(Rotation.X) * Matrix4.CreateRotationY(Rotation.Y) * Matrix4.CreateRotationZ(Rotation.Z);
                matrix *= Matrix4.CreateTranslation(Position);
                return matrix;
            }
        }
        public class COLLADANode
        {
            public string Name;
            public COLLADATransform Transform;

            public Node AssimpNode;

            public COLLADANode Parent;
            public List<COLLADANode> Children = new List<COLLADANode>();

            public int[] MeshIndices = new int[0];

            public COLLADANode(string name, COLLADATransform transform, int[] meshIndices, COLLADANode parent, Node assimpNode)
            {
                Name = name;
                Transform = transform;

                AssimpNode = assimpNode;

                MeshIndices = meshIndices;

                Parent = parent;
                if (Parent != null)
                    Parent.Children.Add(this);
            }
        }
        private class COLLADAJointAnimation
        {
            public string Name;

            public COLLADAAnimation[] Translation = new COLLADAAnimation[3];
            public COLLADAAnimation[] Rotation = new COLLADAAnimation[3];
            public COLLADAAnimation[] Scaling = new COLLADAAnimation[3];

            public COLLADAJointAnimation()
            {
                parsedJointAnimations.Add(this);
            }
        }
        private class COLLADAAnimation
        {
            public List<COLLADASource> Sources = new List<COLLADASource>();
            public COLLADASampler Sampler;
            public COLLADAChannel Channel;

            public List<float> Times = new List<float>();
            public List<float> Values = new List<float>();
            public List<AnimationInterpolation> Interpolations = new List<AnimationInterpolation>();
            public List<Vector2> InTangents = new List<Vector2>();
            public List<Vector2> OutTangents = new List<Vector2>();

            public COLLADAAnimation()
            {
                parsedAnimations.Add(this);
            }
        }
        private class COLLADASource
        {
            public string ID;
            public COLLADAFloatArray FloatArray;
            public COLLADANameArray NameArray;
        }
        private class COLLADAChannel
        {
            public string Target;
        }
        private class COLLADAFloatArray
        {
            public string ID;
            public int Count;
            public string Value;
        }
        private class COLLADANameArray
        {
            public string ID;
            public int Count;
            public string Value;
        }
        private class COLLADASampler
        {
            public string ID;
            public List<COLLADAInput> Inputs = new List<COLLADAInput>();
        }
        private class COLLADAInput
        {
            public string Semantic;
            public string Source;
        }

        private class COLQueueItem
        {
            public Mesh AssimpMesh;
            public COLLADANode ColladaNode;

            public COLQueueItem(Mesh assimpMesh, COLLADANode colladaNode)
            {
                AssimpMesh = assimpMesh;
                ColladaNode = colladaNode;
            }
        }

        private static string FixCollada(string path)
        {
            doc = XDocument.Load(path);
            ns = doc.Root.GetDefaultNamespace();

            foreach (XElement element in doc.Descendants())
            {
                if (element.Name == ns + "color")
                    element.SetValue(element.Value + " 1.0");
            }

            File.WriteAllText("colladaBlenderFix.dae", doc.ToString());
            return "colladaBlenderFix.dae";
        }
    }
}
