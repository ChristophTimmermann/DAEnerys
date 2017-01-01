using Assimp;
using Assimp.Configs;
using System;

namespace DAEnerys
{
    public class ObjImporter
    {
        public static Mesh[] ImportMeshesFromFile(string path)
        {
            AssimpContext importer = new AssimpContext();
            NormalSmoothingAngleConfig config = new NormalSmoothingAngleConfig(80.0f);
            importer.SetConfig(config);
            LogStream logStream = new LogStream(delegate (string msg, string userData)
            {
                Console.WriteLine(msg);
            });
            logStream.Attach();

            //Scene obj = importer.ImportFile(path, ~(PostProcessSteps.CalculateTangentSpace | PostProcessSteps.GenerateNormals) & (PostProcessPreset.TargetRealTimeFast));
            Scene obj = importer.ImportFile(path, PostProcessSteps.JoinIdenticalVertices | PostProcessSteps.Triangulate | PostProcessSteps.ValidateDataStructure);
            importer.Dispose();

            return obj.Meshes.ToArray();
        }

        public static Mesh ImportMeshFromFile(string path)
        {
            AssimpContext importer = new AssimpContext();
            NormalSmoothingAngleConfig config = new NormalSmoothingAngleConfig(80.0f);
            importer.SetConfig(config);
            LogStream logStream = new LogStream(delegate (string msg, string userData)
            {
                Console.WriteLine(msg);
            });
            logStream.Attach();

            //Scene obj = importer.ImportFile(path, ~(PostProcessSteps.CalculateTangentSpace | PostProcessSteps.GenerateNormals) & (PostProcessPreset.TargetRealTimeFast));
            Scene obj = importer.ImportFile(path, PostProcessSteps.JoinIdenticalVertices | PostProcessSteps.Triangulate | PostProcessSteps.ValidateDataStructure);
            importer.Dispose();

            return obj.Meshes[0];
        }
    }
}
