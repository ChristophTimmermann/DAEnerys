using Assimp;
using Assimp.Configs;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using OpenTK.Graphics.OpenGL;
using System.Drawing.Imaging;
using Utilities;

namespace HomeworldDAEEditor
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
        public static List<HWGoblinMesh> GoblinMeshes = new List<HWGoblinMesh>();
        public static List<HWCollisionMesh> CollisionMeshes = new List<HWCollisionMesh>();
        public static List<HWMaterial> Materials = new List<HWMaterial>();
        public static List<HWNode> Nodes = new List<HWNode>();
        public static List<HWJoint> Joints = new List<HWJoint>();
        public static List<HWMarker> Markers = new List<HWMarker>();
        public static List<HWDockpath> Dockpaths = new List<HWDockpath>();
        public static List<HWDockSegment> DockSegments = new List<HWDockSegment>();

        public static Dictionary<string, int> Textures = new Dictionary<string, int>();

        public static void LoadCollada(string path)
        {
            #region Import
            AssimpContext importer = new AssimpContext();
            NormalSmoothingAngleConfig config = new NormalSmoothingAngleConfig(66.0f);
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

            Collada = importer.ImportFile(fixedColladaPath, PostProcessPreset.TargetRealTimeMaximumQuality);
            importer.Dispose();

            File.Delete(fixedColladaPath);
            #endregion

            Program.Camera.Zoom = 0; //Set camera zoom to 0 for bounding box calculations to set it

            LoadMaterials();
            LoadMeshes();
            RootNode = new HWNode(Collada.RootNode, null);

            foreach(HWMesh mesh in Meshes) //Parse meshes (add them as ship meshes etc.)
            {
                mesh.ParseMesh();
            }

            foreach(HWDockpath dockpath in Dockpaths)
            {
                dockpath.SetupVisualization(); //Create 3D lines and stuff
            }

            foreach (HWShipMesh shipMesh in ShipMeshes) //Set all LOD0 meshes visible by default
            {
                foreach(HWShipMeshLOD shipMeshLOD in shipMesh.LOD0Meshes)
                {
                    shipMeshLOD.Mesh.Visible = true;
                }
            }

            foreach(HWGoblinMesh goblin in GoblinMeshes) //Set all goblin meshes visible by default
            {
                goblin.SetVisible(true);
            }

            CalibrateSettings();

            Renderer.UpdateMeshData();
            Renderer.UpdateView();
            Program.GLControl.Invalidate();
        }

        private static void LoadMeshes()
        {
            foreach(Mesh mesh in Collada.Meshes)
            {
                HWMesh newMesh = new HWMesh(mesh);

                if(mesh.TextureCoordinateChannelCount > 0)
                    newMesh.Material = HWScene.Materials[mesh.MaterialIndex];

                Console.WriteLine("Mesh '" + mesh.Name + "' added.");
            }
        }

        private static void LoadMaterials()
        {
            foreach(Material material in Collada.Materials)
            {
                HWMaterial newMaterial = new HWMaterial();

                newMaterial.Name = material.Name;
                newMaterial.AmbientColor = new Vector3(material.ColorAmbient.R, material.ColorAmbient.G, material.ColorAmbient.B);
                newMaterial.DiffuseColor = new Vector3(material.ColorDiffuse.R, material.ColorDiffuse.G, material.ColorDiffuse.B);
                newMaterial.SpecularColor = new Vector3(material.ColorSpecular.R, material.ColorSpecular.G, material.ColorSpecular.B);
                newMaterial.SpecularExponent = material.ShininessStrength;
                newMaterial.Opacity = material.Opacity;

                newMaterial.DiffuseMap = material.TextureDiffuse.FilePath;
                //TODO: Add more maps

                if (newMaterial.DiffuseMap != null)
                {
                    string mapPath = Path.Combine(HWScene.ColladaPath, newMaterial.DiffuseMap);
                    if (File.Exists(mapPath))
                    {
                        newMaterial.DiffuseTexture = new HWTexture(mapPath);
                        newMaterial.Shader = "textured";
                    }
                }

                Console.WriteLine("Material '" + material.Name + "' added.");
            }
        }

        private static string FixCollada(string path)
        {
            string file = File.ReadAllText(path);

            //Fix emission
            int index = 0;
            int found = file.IndexOf("<color sid=\"emission\">  ");
            while (found != -1)
            {
                int pFrom = found + "<color sid=\"emission\">  ".Length;
                int pTo = file.IndexOf("  </color>", pFrom);
                string emission = file.Substring(pFrom, pTo - pFrom);

                if (emission.Split(' ').Length == 3)
                {
                    file = file.Insert(pTo, " 1.0");
                }
                index = pTo;
                found = file.IndexOf("<color sid=\"emission\">  ", index);
            }

            //Fix diffuse
            index = 0;
            found = file.IndexOf("<color sid=\"diffuse\">  ");
            while (found != -1)
            {
                int pFrom = found + "<color sid=\"diffuse\">  ".Length;
                int pTo = file.IndexOf(" </color>", pFrom);
                string diffuse = file.Substring(pFrom, pTo - pFrom);

                if (diffuse.Split(' ').Length == 3)
                {
                    file = file.Insert(pTo, " 1.0");
                }
                index = pTo;
                found = file.IndexOf("<color sid=\"diffuse\">  ", index);
            }

            //Fix specular
            index = 0;
            found = file.IndexOf("<color sid=\"specular\">  ");
            while (found != -1)
            {
                int pFrom = found + "<color sid=\"specular\">  ".Length;
                int pTo = file.IndexOf(" </color>", pFrom);
                string specular = file.Substring(pFrom, pTo - pFrom);

                if (specular.Split(' ').Length == 3)
                {
                    file = file.Insert(pTo, " 1.0");
                }
                index = pTo;
                found = file.IndexOf("<color sid=\"specular\">  ", index);
            }

            //Fix reflective parameter
            index = 0;
            found = file.IndexOf("<reflective>");
            while (found != -1)
            {
                string subStr = file.Substring(found, 500);

                int pFrom = subStr.IndexOf("<color>  ");
                if (pFrom != -1) //If blender exported file
                {
                    int pTo = subStr.IndexOf(" </color>", pFrom);
                    string reflective = file.Substring(pFrom + found, pTo - pFrom + " </color>".Length);
                    int paramIndex = file.IndexOf(reflective);

                    file = file.Remove(paramIndex, reflective.Length);
                    file = file.Insert(paramIndex, "<color sid=\"reflective\">  0.0 0.0 0.0 1.0</color>");
                }

                found = file.IndexOf("<reflective>", found + 10);
            }

            File.WriteAllText("colladaBlenderFix.dae", file);
            return "colladaBlenderFix.dae";
        }

        private static void CalibrateSettings()
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

            Console.WriteLine("Volume = " + volume);
            Console.WriteLine("Farthest = " + farthest);

            float zoomSpeed = (float)farthest * 10;

            float jointSize = 1;
            if (BiggestMesh != null)
            {
                jointSize = farthest / 35 / BiggestMesh.Mesh.Parent.AbsoluteScale.X;
                jointSize = Math.Max(jointSize, 0.3f);
            }

            float markerSize = farthest / 60;
            markerSize = Math.Max(markerSize, 0.01f);

            float farClip = farthest * 32;
            float nearClip = farthest / 16;

            Program.Camera.Zoom = farthest * 1.2f;
            Program.Camera.ZoomSpeed = zoomSpeed;
            EditorJoint.Size = jointSize;
            HWMarker.MarkerSize = markerSize;
            Renderer.ClipDistance = farClip;
            Renderer.NearClipDistance = nearClip;
        }

        public static void SaveCollada(string path)
        {
            AssimpContext exporter = new AssimpContext();

            /*LogStream logStream = new LogStream(delegate (string msg, string userData) 
            {
                Console.WriteLine(msg);
            });
            logStream.Attach();*/

            Console.WriteLine(exporter.IsExportFormatSupported(".dae"));

            bool success = exporter.ExportFile(Collada, path, ".dae");
            if (success)
                Console.WriteLine("Successfully exported to \"" + path + "\".");
            else
                Console.WriteLine("Failed to export.");

            Console.WriteLine(Assimp.Unmanaged.AssimpLibrary.Instance.GetErrorString());

            exporter.Dispose();
        }

        public static void Clear()
        {
            Min = Vector3.Zero;
            Max = Vector3.Zero;

            Meshes.Clear();
            ShipMeshes.Clear();
            GoblinMeshes.Clear();
            CollisionMeshes.Clear();
            Materials.Clear();
            Nodes.Clear();
            Textures.Clear();
            Joints.Clear();
            Markers.Clear();
            Dockpaths.Clear();
            DockSegments.Clear();
        }
    }
}
