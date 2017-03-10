using Assimp;
using Assimp.Configs;
using System;
using System.Collections.Generic;
using System.IO;

namespace DAEnerys
{
    static class EditorScene
    {
        public static List<EditorMesh> meshes = new List<EditorMesh>();
        public static List<EditorIcon> icons = new List<EditorIcon>();
        //public static List<EditorEffect> effects = new List<EditorEffect>();

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
            EditorJoint.Data = Importer.ParseAssimpMesh(joint.Meshes[0]);

            Scene marker = importer.ImportFile(Path.Combine(Program.EXECUTABLE_PATH, @"resources/marker.obj"), PostProcessPreset.TargetRealTimeMaximumQuality);
            EditorMarker.Data = Importer.ParseAssimpMesh(marker.Meshes[0]);

            Scene icosphere = importer.ImportFile(Path.Combine(Program.EXECUTABLE_PATH, @"resources/icosphere.obj"), PostProcessPreset.TargetRealTimeMaximumQuality);
            EditorIcosphere.Data = Importer.ParseAssimpMesh(icosphere.Meshes[0]);

            Scene icon = importer.ImportFile(Path.Combine(Program.EXECUTABLE_PATH, @"resources/icon.obj"), PostProcessPreset.TargetRealTimeMaximumQuality);
            EditorIcon.Data = Importer.ParseAssimpMesh(icon.Meshes[0]);

            //Scene effect = importer.ImportFile(Path.Combine(Program.EXECUTABLE_PATH, @"resources/square.obj"), PostProcessPreset.TargetRealTimeMaximumQuality);
            //EditorEffect.Data = Importer.ParseAssimpMesh(effect.Meshes[0]);

            Scene cube = importer.ImportFile(Path.Combine(Program.EXECUTABLE_PATH, @"resources/cube.obj"), PostProcessPreset.TargetRealTimeMaximumQuality);
            EditorCube.Data = Importer.ParseAssimpMesh(cube.Meshes[0]);

            Scene dockSegment = importer.ImportFile(Path.Combine(Program.EXECUTABLE_PATH, @"resources/dockSegment.ply"), PostProcessPreset.TargetRealTimeMaximumQuality);
            EditorDockSegment.Data = Importer.ParseAssimpMesh(dockSegment.Meshes[0]);

            Scene dockpathPreviewModel = importer.ImportFile(Path.Combine(Program.EXECUTABLE_PATH, @"resources/dockpathPreviewModel.obj"), PostProcessPreset.TargetRealTimeMaximumQuality);
            EditorDockpathPreviewModel.Data = Importer.ParseAssimpMesh(dockpathPreviewModel.Meshes[0]);

            importer.Dispose();
            logStream.Detach();
            #endregion
        }

        public static void Clear()
        {
            meshes.Clear();
            icons.Clear();
        }
    }
}
