using Assimp;
using Assimp.Configs;
using System;

namespace DAEnerys
{
    public class ObjImporter
    {
        public static Mesh[] ImportFromFile(string path)
        {
            AssimpContext importer = new AssimpContext();
            NormalSmoothingAngleConfig config = new NormalSmoothingAngleConfig(80.0f);
            importer.SetConfig(config);
            LogStream logStream = new LogStream(delegate (string msg, string userData)
            {
                Console.WriteLine(msg);
            });
            logStream.Attach();

            Scene obj = importer.ImportFile(path, ~(PostProcessSteps.CalculateTangentSpace | PostProcessSteps.GenerateNormals) & (PostProcessPreset.TargetRealTimeFast));
            importer.Dispose();

            return obj.Meshes.ToArray();
        }
    }
}
