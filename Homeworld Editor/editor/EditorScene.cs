using Assimp;
using Assimp.Configs;
using System;
using System.Collections.Generic;
using System.IO;

namespace HomeworldDAEEditor
{
    static class EditorScene
    {
        public static List<EditorMesh> meshes = new List<EditorMesh>();
        public static List<EditorMaterial> materials = new List<EditorMaterial>();

        public static void Init()
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

            Scene joint = importer.ImportFile("joint.obj", PostProcessPreset.TargetRealTimeMaximumQuality);
            EditorJoint.Mesh = joint.Meshes[0];

            EditorMaterial jointMaterial = new EditorMaterial();
            jointMaterial.Name = joint.Materials[0].Name;
            jointMaterial.DiffuseMap = joint.Materials[0].TextureDiffuse.FilePath;
            jointMaterial.DiffuseTexture = new HWTexture(joint.Materials[0].TextureDiffuse.FilePath);
            EditorJoint.JointMaterial = jointMaterial;

            Scene icosphere = importer.ImportFile("icosphere.obj", PostProcessPreset.TargetRealTimeMaximumQuality);
            EditorIcosphere.Mesh = icosphere.Meshes[0];
            

            EditorMaterial redMaterial = new EditorMaterial();
            redMaterial.Name = "RedMaterial";
            redMaterial.DiffuseMap = "red.tga";
            redMaterial.DiffuseTexture = new HWTexture("red.tga");
            EditorLine.RedMaterial = redMaterial;
            EditorIcosphere.RedMaterial = redMaterial;

            EditorMaterial yellowMaterial = new EditorMaterial();
            yellowMaterial.Name = "YellowMaterial";
            yellowMaterial.DiffuseMap = "yellow.tga";
            yellowMaterial.DiffuseTexture = new HWTexture("yellow.tga");
            EditorLine.YellowMaterial = yellowMaterial;
            EditorIcosphere.YellowMaterial = yellowMaterial;

            importer.Dispose();
            #endregion
        }

        public static void Clear()
        {
            meshes.Clear();
            materials.Clear();
        }
    }
}
