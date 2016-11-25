using OpenTK;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace DAEnerys
{
    public static class ObjExporter
    {
        public static void ExportToFile(string path, List<HWMesh> meshes)
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

                for(int i = 0; i < mesh.IndexCount; i += 3)
                    file.AppendLine("f " + (mesh.GetIndices(indexOffset)[i] + 1) + "/" + (mesh.GetIndices(indexOffset)[i] + 1) + " " + (mesh.GetIndices(indexOffset)[i + 1] + 1) + "/" + (mesh.GetIndices(indexOffset)[i + 1] + 1) + " " + (mesh.GetIndices(indexOffset)[i + 2] + 1) + "/" + (mesh.GetIndices(indexOffset)[i + 2] + 1));

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
            foreach(HWMesh mesh in meshes)
            {
                if (mesh.Material == null || mesh.Material.DiffusePath == string.Empty)
                    continue;

                file.AppendLine("newmtl " + mesh.Material.Name);
                file.AppendLine("\tmap_Kd " + mesh.Material.DiffusePath);
            }

            file.AppendLine("# <EOF>");
            File.WriteAllText(Path.ChangeExtension(path, "mtl"), file.ToString());
            #endregion
        }
    }
}
