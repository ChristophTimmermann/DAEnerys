using Assimp;
using Assimp.Configs;
using OpenTK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;

namespace DAEnerys
{
    static class HWScene
    {
        public static string ColladaPath;
        public static Scene Collada;

        public static HWNode RootNode;

        public static Vector3 Min = Vector3.Zero;
        public static Vector3 Max = Vector3.Zero;
        public static HWShipMeshLOD BiggestMesh = null;

        public static List<HWMesh> Meshes = new List<HWMesh>();
        public static List<HWShipMesh> ShipMeshes = new List<HWShipMesh>();
        public static List<HWCollisionMesh> CollisionMeshes = new List<HWCollisionMesh>();
        public static List<HWEngineGlow> EngineGlows = new List<HWEngineGlow>();
        public static List<HWEngineShape> EngineShapes = new List<HWEngineShape>();
        public static List<HWMaterial> Materials = new List<HWMaterial>();
        public static List<HWImage> Images = new List<HWImage>();
        public static List<HWNode> Nodes = new List<HWNode>();
        public static List<HWMarker> Markers = new List<HWMarker>();
        public static List<HWDockpath> Dockpaths = new List<HWDockpath>();
        public static List<HWDockSegment> DockSegments = new List<HWDockSegment>();
        public static List<HWNavLight> NavLights = new List<HWNavLight>();

        public static Dictionary<string, int> RenderTextures = new Dictionary<string, int>();

        public static void LoadCollada(string path)
        {
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

            ParseNode(Collada.RootNode, null);
  
            foreach(HWDockpath dockpath in Dockpaths)
            {
                dockpath.SetupVisualization(); //Create 3D lines and stuff
            }

            foreach (HWShipMesh shipMesh in ShipMeshes) //Set all LOD0 meshes visible by default
            {
                foreach (HWShipMeshLOD shipMeshLOD in shipMesh.LODMeshes[0])
                {
                    shipMeshLOD.Visible = true;
                }
            }

            foreach(HWNavLight navLight in NavLights)
            {
                Program.main.CheckNavLightVisible(navLight, true); //Set all navlights visible by default
            }

            foreach (HWEngineGlow engineGlow in EngineGlows) //Set all LOD0 engine glows visible by default
            {
                foreach (HWEngineGlowLOD engineGlowLOD in engineGlow.LODMeshes[0])
                {
                    engineGlowLOD.Visible = true;
                }
            }

            CalibrateSettings();
            CheckForProblems();
            HWEngineGlow.UpdateEngineStrength();
            Program.main.UpdateProblems(); 

            Renderer.InvalidateMeshData();
            Renderer.InvalidateView();
            Renderer.Invalidate();
            logStream.Detach();
        }

        private static void ParseNode(Node assimpNode, HWNode parent)
        {
            HWNode newNode = HWNode.ParseNode(assimpNode, parent);

            if(HWScene.RootNode == null)
                if (assimpNode == Collada.RootNode)
                    HWScene.RootNode = newNode;

            foreach(Node assNode in assimpNode.Children)
            {
                ParseNode(assNode, newNode);
            }
        }

        private static void LoadMaterials()
        {
            foreach(Material material in Collada.Materials)
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

            while(reader.Read())
            {
                if(reader.Name == "image")
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

                    if(name != null && path != null)
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

            foreach(XElement element in doc.Descendants())
            {
                if(element.Name == ns + "color")
                    element.SetValue(element.Value + "1.0");
            }

            File.WriteAllText("colladaBlenderFix.dae", doc.ToString());
            return "colladaBlenderFix.dae";
        }

        public static void FindBiggestMesh()
        {
            HWScene.Min = Vector3.Zero;
            HWScene.Max = Vector3.Zero;

            foreach (HWShipMesh shipMesh in ShipMeshes)
                foreach (HWShipMeshLOD lodMesh in shipMesh.LODMeshes[0])
                    lodMesh.CalculateBoundingBox();
        }

        public static void CalibrateSettings(bool setZoom = true)
        {
            float volume = (-Min.X + Max.X) * (-Min.Y + Max.Y) * (-Min.Z + Max.Z);
            List<float> values = new List<float>();
            values.Add(-Min.X); values.Add(-Min.Y); values.Add(-Min.Z);
            values.Add(Max.X); values.Add(Max.Y); values.Add(Max.Z);
            float farthest = 0;
            foreach(float value in values)
            {
                if (value > farthest)
                    farthest = value;
            }

            farthest = Math.Max(0.01f, farthest);

            float jointSize = 1;
            float markerSize = 1;
            if (BiggestMesh != null)
            {
                if (BiggestMesh != null)
                {
                    jointSize = farthest / 60 / BiggestMesh.Parent.AbsoluteScale.X;
                    jointSize = Math.Max(jointSize, 0.3f);

                    markerSize = farthest / 65 / BiggestMesh.Parent.AbsoluteScale.X;
                    markerSize = Math.Max(markerSize, 0.01f);
                }
            }

            float farClip = farthest * 64;
            float nearClip = farthest / 32;

            Program.Camera.MinZoom = farthest / 40;
            Program.Camera.MaxZoom = farthest * 40;
            if(setZoom)
                Program.Camera.Zoom = farthest * 2f;
            Program.Camera.ZoomSpeed = farthest * 10;
            Program.Camera.CalculatedZoom = Program.Camera.Zoom;

            EditorJoint.Size = jointSize;
            HWMarker.MarkerSize = markerSize;
            HWNavLight.IconSize = farthest / 55;
            Program.Camera.ClipDistance = farClip;
            Program.Camera.NearClipDistance = nearClip;
            Renderer.MinClipDistance = Min.Z * 1.2f;
            Renderer.MaxClipDistance = Max.Z * 1.2f;
            Renderer.ClipDistance = Renderer.MaxClipDistance;

            //Update line vertices
            foreach (HWMarker marker in HWScene.Markers)
            {
                foreach (EditorLine line in marker.Lines)
                {
                    line.Vertices = line.GetVertices();
                }
            }
        }

        private static void CheckForProblems()
        {
            #region Dockpaths
            List<string> dockpathNames = new List<string>();

            //Check if there are multiple dockpaths with the same name
            foreach (HWDockpath dockpath in Dockpaths)
            {
                if(!dockpathNames.Contains(dockpath.Name))
                    dockpathNames.Add(dockpath.Name);
                else
                    new Problem(ProblemTypes.WARNING, "There are multiple dockpaths with the same name \"" + dockpath.Name + "\".");
            }

            //Check if there is a non-existent linked path
            foreach (HWDockpath dockpath in Dockpaths)
            {
                foreach(string link in dockpath.Links)
                {
                    if (!dockpathNames.Contains(link))
                        new Problem(ProblemTypes.WARNING, "The dockpath \"" + dockpath.Name + "\" is linked to the non-existent dockpath \"" + link + "\".");
                }
            }
            #endregion

            foreach(HWMaterial material in Materials)
            {
                if(material.Shader == "thruster")
                {
                    if (material.DiffuseOffTexture == null)
                        new Problem(ProblemTypes.WARNING, "Material \"" + material.Name + "\" uses the \"thruster\" shader but has no DIFX texture.");

                    if (material.GlowOffTexture == null)
                        new Problem(ProblemTypes.WARNING, "Material \"" + material.Name + "\" uses the \"thruster\" shader but has no GLOX texture.");

                    if (material.DiffuseTexture == null)
                        new Problem(ProblemTypes.WARNING, "Material \"" + material.Name + "\" uses the \"thruster\" shader but has no DIFF texture.");

                    if (material.GlowTexture == null)
                        new Problem(ProblemTypes.WARNING, "Material \"" + material.Name + "\" uses the \"thruster\" shader but has no GLOW texture.");
                }
            }
        }

        public static void SaveCollada(string path)
        {
            Exporter.ExportToFile(path);
            Log.WriteLine("Successfully exported to \"" + path + "\".");
        }

        public static void Clear()
        {
            Min = Vector3.Zero;
            Max = Vector3.Zero;

            foreach(HWNavLight navLight in NavLights)
            {
                Light.Lights.Remove(navLight.RenderLight);
            }

            Meshes.Clear();
            ShipMeshes.Clear();
            CollisionMeshes.Clear();
            EngineGlows.Clear();
            EngineShapes.Clear();
            Materials.Clear();
            Images.Clear();

            Nodes.Clear();
            HWNode.RootLODs = new HWNode[4];
            HWNode.RootCOL = null;
            HWNode.RootINFO = null;
            HWNode.HoldDock = null;

            RenderTextures.Clear();
            HWJoint.Joints.Clear();
            Markers.Clear();
            Dockpaths.Clear();
            DockSegments.Clear();
            NavLights.Clear();
        }
    }
}
