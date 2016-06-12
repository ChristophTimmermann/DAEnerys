using Assimp;
using Assimp.Configs;
using OpenTK;
using System;
using System.Collections.Generic;
using System.IO;

namespace HomeworldDAEEditor
{
    static class EditorScene
    {
        public static List<EditorMesh> meshes = new List<EditorMesh>();
        public static List<EditorMaterial> materials = new List<EditorMaterial>();
        public static List<EditorIcon> icons = new List<EditorIcon>();

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

            Scene joint = importer.ImportFile(Path.Combine(Program.EXECUTABLE_PATH, @"resources/joint.ply"), PostProcessPreset.TargetRealTimeMaximumQuality);
            EditorJoint.Mesh = joint.Meshes[0];

            Scene icosphere = importer.ImportFile(Path.Combine(Program.EXECUTABLE_PATH, @"resources/icosphere.obj"), PostProcessPreset.TargetRealTimeMaximumQuality);
            EditorIcosphere.Mesh = icosphere.Meshes[0];

            Scene icon = importer.ImportFile(Path.Combine(Program.EXECUTABLE_PATH, @"resources/icon.obj"), PostProcessPreset.TargetRealTimeMaximumQuality);
            EditorIcon.Mesh = icon.Meshes[0];

            importer.Dispose();
            #endregion
        }

        public static void Clear()
        {
            meshes.Clear();
            materials.Clear();
            icons.Clear();
        }
    }
}
