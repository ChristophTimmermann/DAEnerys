using OpenTK;
using System;
using System.Collections.Generic;

namespace DAEnerys
{
    static class HWScene
    {
        public static Vector3 Min = Vector3.Zero;
        public static Vector3 Max = Vector3.Zero;
        public static HWShipMeshLOD BiggestMesh = null;

        public static Dictionary<string, int> RenderTextures = new Dictionary<string, int>();

        public static void FindBiggestMesh()
        {
            HWScene.Min = Vector3.Zero;
            HWScene.Max = Vector3.Zero;

            foreach (HWShipMesh shipMesh in HWShipMesh.ShipMeshes)
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
                    jointSize = farthest / 60 / HWJoint.Root.AbsoluteScale.X;
                    jointSize = Math.Max(jointSize, 0.3f);

                    markerSize = farthest / 65 / HWJoint.Root.AbsoluteScale.X;
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
            foreach (HWMarker marker in HWMarker.Markers)
            {
                foreach (EditorLine line in marker.Lines)
                {
                    line.Vertices = line.GetVertices();
                }
            }
        }

        public static void CheckForProblems()
        {
            #region Dockpaths
            List<string> dockpathNames = new List<string>();

            //Check if there are multiple dockpaths with the same name
            foreach (HWDockpath dockpath in HWDockpath.Dockpaths)
            {
                if(!dockpathNames.Contains(dockpath.Name))
                    dockpathNames.Add(dockpath.Name);
                else
                    new Problem(ProblemTypes.WARNING, "There are multiple dockpaths with the same name \"" + dockpath.Name + "\".");
            }

            //Check if there is a non-existent linked path
            foreach (HWDockpath dockpath in HWDockpath.Dockpaths)
            {
                foreach(string link in dockpath.Links)
                {
                    if (!dockpathNames.Contains(link))
                        new Problem(ProblemTypes.WARNING, "The dockpath \"" + dockpath.Name + "\" is linked to the non-existent dockpath \"" + link + "\".");
                }
            }
            #endregion

            foreach(HWMaterial material in HWMaterial.Materials)
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
            Importer.ColladaPath = String.Empty;

            Min = Vector3.Zero;
            Max = Vector3.Zero;

            foreach(HWNavLight navLight in HWNavLight.NavLights)
            {
                Light.Lights.Remove(navLight.RenderLight);
            }

            BiggestMesh = null;

            HWMesh.Meshes.Clear();
            HWShipMesh.ShipMeshes.Clear();
            HWCollisionMesh.CollisionMeshes.Clear();
            HWEngineGlow.EngineGlows.Clear();
            HWEngineShape.EngineShapes.Clear();
            HWMaterial.Materials.Clear();
            HWImage.Images.Clear();

            RenderTextures.Clear();
            HWJoint.Joints.Clear();
            HWMarker.Markers.Clear();
            HWDockpath.Dockpaths.Clear();
            HWDockSegment.DockSegments.Clear();
            HWNavLight.NavLights.Clear();
            HWEngineBurn.EngineBurns.Clear();
            HWEngineFlame.EngineFlames.Clear();

            HWJoint.Root = new HWJoint("Root", null, Matrix4.Identity);
            HWJoint.Root.TreeNode.Expand();
        }
    }
}
