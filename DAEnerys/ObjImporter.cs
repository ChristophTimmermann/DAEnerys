using Assimp;
using Assimp.Configs;
using System;
using System.Collections.Generic;

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

            //Combine meshes with the same material
            Dictionary<Material, Mesh> materialMeshes = new Dictionary<Material, Mesh>();
            foreach (Mesh mesh in obj.Meshes)
            {
                Material material = obj.Materials[mesh.MaterialIndex];
                Mesh materialMesh = null;

                if (!materialMeshes.ContainsKey(material))
                {
                    materialMesh = new Mesh(PrimitiveType.Triangle);
                    materialMesh.MaterialIndex = mesh.MaterialIndex;
                    materialMesh.Name = mesh.Name;
                    materialMeshes.Add(material, materialMesh);
                }
                else
                    materialMesh = materialMeshes[material];

                List<int> indices = new List<int>();
                if (materialMesh.FaceCount > 0)
                    indices.AddRange(materialMesh.GetIndices());

                int[] newIndices = mesh.GetIndices();

                if (mesh.FaceCount > 0)
                {
                    for (int i = 0; i < newIndices.Length; i++)
                        newIndices[i] += materialMesh.VertexCount;
                    indices.AddRange(newIndices);
                }

                materialMesh.Vertices.AddRange(mesh.Vertices);
                materialMesh.Normals.AddRange(mesh.Normals);
                materialMesh.TextureCoordinateChannels[0].AddRange(mesh.TextureCoordinateChannels[0]);

                materialMesh.SetIndices(indices.ToArray(), 3);
            }

            List<Mesh> optimizedMeshes = new List<Mesh>();
            foreach (Mesh mesh in materialMeshes.Values)
                optimizedMeshes.Add(mesh);

            return optimizedMeshes.ToArray();
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
