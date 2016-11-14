using OpenTK;
using System.Collections.Generic;
using System.IO;

namespace DAEnerys
{
    public class EditorIcon : EditorMesh
    {
        public static Assimp.Mesh Mesh;
        public Vector3 Position;

        public float Size = 10;

        public override int VertexCount { get { return Mesh.VertexCount; } }
        public override int IndiceCount { get { return Indices.Length; } }

        public static HWTexture LightbulbTexture = new HWTexture(Path.Combine(Program.EXECUTABLE_PATH, @"resources/lightbulb.tga"), true, true);

        public EditorIcon(Vector3 position, HWTexture texture) : base()
        {
            this.Position = position;

            this.Material = new EditorMaterial("iconMaterial", new Vector3(1), new Vector3(1));
            this.Material.DiffuseTexture = texture;
            Visible = false;

            Vertices = GetVertices();
            Normals = GetNormals();
            Indices = GetIndices();
            Colors = GetColorData();
            TextureCoords = GetTextureCoords();
        }

        public override Vector3[] GetVertices()
        {
            List<Vector3> verticesList = new List<Vector3>();
            foreach (Assimp.Vector3D vertex in Mesh.Vertices)
            {
                verticesList.Add(new Vector3(vertex.X, vertex.Y, vertex.Z));
            }
            return verticesList.ToArray();
        }

        public override Vector3[] GetNormals()
        {
            List<Vector3> normalsList = new List<Vector3>();
            foreach (Assimp.Vector3D normal in Mesh.Normals)
            {
                normalsList.Add(new Vector3(normal.X, normal.Y, normal.Z));
            }
            return normalsList.ToArray();
        }

        public override int[] GetIndices(int offset = 0)
        {
            int[] indices = Mesh.GetIndices();

            if (offset != 0)
            {
                for (int i = 0; i < indices.Length; i++)
                {
                    indices[i] += offset;
                }
            }

            return indices;
        }

        public override Vector3[] GetColorData()
        {
            return new Vector3[VertexCount];
        }

        public override Vector2[] GetTextureCoords()
        {
            if (Mesh.TextureCoordinateChannelCount > 0)
            {
                List<Vector2> coords = new List<Vector2>();

                foreach (Assimp.Vector3D coord in Mesh.TextureCoordinateChannels[0])
                {
                    coords.Add(new Vector2(coord.X, coord.Y));
                }

                return coords.ToArray();
            }
            else
            {
                return new Vector2[VertexCount];
            }
        }

        /// <summary>
        /// Calculates the model matrix from transforms
        /// </summary>
        public override void CalculateModelMatrix()
        {
            ModelMatrix = Renderer.View.Inverted().ClearTranslation() * Matrix4.CreateScale(Size) * Matrix4.CreateTranslation(Position);
        }
    }
}
