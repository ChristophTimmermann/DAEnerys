using OpenTK;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Assimp;
using Assimp.Configs;

namespace DAEnerys
{
    public static class ObjExporter
    {
        /*public static void ExportToFile(string path, List<HWMesh> meshes)
        {
            int materialCount = 0;
            foreach (HWMesh mesh in meshes)
                if (mesh.Material != null)
                    if (mesh.Material.DiffusePath != string.Empty)
                        materialCount++;

            #region OBJ-file
            StringBuilder file = new StringBuilder();
            file.AppendLine("# DAEnerys b" + Program.main.BUILD);
            file.AppendLine("# OBJ File: \"" + meshes[0].Name + "\"");
            file.AppendLine("# Created " + DateTime.Now.ToLongDateString() + " at " + DateTime.Now.ToLongTimeString());
            file.AppendLine("#");

            //Materials
            if (materialCount > 0)
            {
                file.AppendLine("# Materials");
                file.AppendLine("mtllib " + Path.ChangeExtension(Path.GetFileName(path), "mtl"));
            }

            //Object
            file.AppendLine("# Objects");
            file.AppendLine("o " + meshes[0].Name);

            //Vertices
            file.AppendLine("# Vertices");
            foreach (HWMesh mesh in meshes)
                foreach (Vector3 vertex in mesh.Vertices)
                    file.AppendLine("v " + vertex.X.ToString(CultureInfo.InvariantCulture) + " " + vertex.Y.ToString(CultureInfo.InvariantCulture) + " " + vertex.Z.ToString(CultureInfo.InvariantCulture));

            //Texture coordinates
            foreach (HWMesh mesh in meshes)
            {
                if (mesh.UVCount > 0)
                {
                    file.AppendLine("# Texture coordinates");
                    foreach (Vector2 uv0 in mesh.UV0)
                        file.AppendLine("vt " + (uv0.X).ToString(CultureInfo.InvariantCulture) + " " + uv0.Y.ToString(CultureInfo.InvariantCulture));
                }
            }

            //Normals
            file.AppendLine("# Normals");
            foreach (HWMesh mesh in meshes)
                foreach (Vector3 normal in mesh.Normals)
                    file.AppendLine("vn " + normal.X.ToString(CultureInfo.InvariantCulture) + " " + normal.Y.ToString(CultureInfo.InvariantCulture) + " " + normal.Z.ToString(CultureInfo.InvariantCulture));


            //Faces
            file.AppendLine("# Faces");

            int indexOffset = 0;
            foreach (HWMesh mesh in meshes)
            {
                if (mesh.Material != null && mesh.Material.DiffusePath != string.Empty)
                    file.AppendLine("usemtl " + mesh.Material.Name);
                else
                    file.AppendLine("usemtl");

                for (int i = 0; i < mesh.IndexCount; i += 3)
                    file.AppendLine("f " + (mesh.Indices[i] + 1 + indexOffset) + "/" + (mesh.Indices[i] + 1 + indexOffset) + " " + (mesh.Indices[i + 1] + 1 + indexOffset) + "/" + (mesh.Indices[i + 1] + 1 + indexOffset) + " " + (mesh.Indices[i + 2] + 1 + indexOffset) + "/" + (mesh.Indices[i + 2] + 1 + indexOffset));

                indexOffset += mesh.VertexCount;
            }

            file.AppendLine("# <EOF>");
            File.WriteAllText(path, file.ToString());
            #endregion

            if (materialCount == 0)
                return;

            #region MTL-file
            file = new StringBuilder();
            file.AppendLine("# DAEnerys b" + Program.main.BUILD);
            file.AppendLine("# MTL File: \"" + meshes[0].Name + "\"");
            file.AppendLine("# Created " + DateTime.Now.ToLongDateString() + " at " + DateTime.Now.ToLongTimeString());
            file.AppendLine("#");

            file.AppendLine("# Materials");
            foreach (HWMesh mesh in meshes)
            {
                if (mesh.Material == null || mesh.Material.DiffusePath == string.Empty)
                    continue;

                file.AppendLine("newmtl " + mesh.Material.Name);
                file.AppendLine("\tmap_Kd " + mesh.Material.DiffusePath);
            }

            file.AppendLine("# <EOF>");
            File.WriteAllText(Path.ChangeExtension(path, "mtl"), file.ToString());
            #endregion
        }*/

        public static void ExportToFile(string path, List<HWMesh> meshes)
        {
            List<HWMaterial> addedMaterials = new List<HWMaterial>();

            Scene scene = new Scene();
            scene.SceneFlags = SceneFlags.NonVerboseFormat;
            scene.RootNode = new Node(meshes[0].Name);

            foreach(HWMesh mesh in meshes)
            {
                Mesh assimpMesh = new Mesh(PrimitiveType.Triangle);

                List<Vector3D> vertices = new List<Vector3D>();
                foreach (Vector3 vertex in mesh.Vertices)
                    vertices.Add(new Vector3D(vertex.X, vertex.Y, vertex.Z));
                assimpMesh.Vertices.AddRange(vertices);

                List<Vector3D> normals = new List<Vector3D>();
                foreach (Vector3 normal in mesh.Normals)
                    normals.Add(new Vector3D(normal.X, normal.Y, normal.Z));
                assimpMesh.Normals.AddRange(normals);

                List<Vector3D> texCoords = new List<Vector3D>();
                foreach (Vector2 texCoord in mesh.UV0)
                    texCoords.Add(new Vector3D(texCoord.X, texCoord.Y, 0));
                assimpMesh.TextureCoordinateChannels[0].AddRange(texCoords);

                assimpMesh.SetIndices(mesh.Indices, 3);

                scene.Meshes.Add(assimpMesh);

                //Node newNode = new Node(mesh.Name, scene.RootNode);
                //scene.RootNode.Children.Add(newNode);
                scene.RootNode.MeshIndices.Add(scene.MeshCount - 1);
                //newNode.MeshIndices.Add(scene.MeshCount - 1);

                if (mesh.Material != null && mesh.Material.DiffusePath != string.Empty)
                {
                    if (!addedMaterials.Contains(mesh.Material))
                    {
                        Material material = new Material();
                        scene.Materials.Add(material);
                        material.Name = mesh.Material.Name;

                        TextureSlot textureSlot = new TextureSlot(mesh.Material.DiffuseTexture.Path, TextureType.Diffuse, 0, TextureMapping.FromUV, 0, 1, TextureOperation.Add, TextureWrapMode.Wrap, TextureWrapMode.Wrap, 0);
                        material.TextureDiffuse = textureSlot;

                        addedMaterials.Add(mesh.Material);
                        assimpMesh.MaterialIndex = addedMaterials.IndexOf(mesh.Material);
                    }
                    else
                    {
                        assimpMesh.MaterialIndex = addedMaterials.IndexOf(mesh.Material);
                    }
                }

                AssimpContext exporter = new AssimpContext();
                NormalSmoothingAngleConfig config = new NormalSmoothingAngleConfig(80.0f);
                exporter.SetConfig(config);

                LogStream logStream = new LogStream(delegate (string msg, string userData)
                {
                    Log.WriteLine(msg);
                });
                logStream.Attach();

                exporter.ExportFile(scene, path, "obj", PostProcessSteps.JoinIdenticalVertices | PostProcessSteps.Triangulate | PostProcessSteps.ValidateDataStructure);
                exporter.Dispose();
            }
        }
    }
}