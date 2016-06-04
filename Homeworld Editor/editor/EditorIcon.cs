using Assimp;
using OpenTK;
using System;
using System.Collections.Generic;

namespace HomeworldDAEEditor
{
    public class EditorIcon : EditorMesh
    {
        public static Mesh Mesh;
        public HWNode HWNode;

        public static float Size = 10;

        public static EditorMaterial IconMaterial;

        public static HWTexture JointIcon = new HWTexture("iconJoint.png");

        public override int VertexCount { get { return Mesh.VertexCount; } }
        public override int IndiceCount { get { return Mesh.GetIndices().Length; } }

        public EditorIcon(HWJoint joint) : base()
        {
            this.HWNode = joint.Node;
            this.Material = new EditorMaterial();
            this.Material.DiffuseTexture = JointIcon;
            Visible = true;
        }

        public override Vector3[] GetVertices()
        {
            List<Vector3> verticesList = new List<Vector3>();
            foreach (Vector3D vertex in Mesh.Vertices)
            {
                verticesList.Add(new Vector3(vertex.X, vertex.Y, vertex.Z));
            }
            return verticesList.ToArray();
        }

        public override Vector3[] GetNormals()
        {
            List<Vector3> normalsList = new List<Vector3>();
            foreach (Vector3D normal in Mesh.Normals)
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

        public override Vector3[] GetColorData(int offset = 0)
        {
            return new Vector3[0];
        }

        public override Vector2[] GetTextureCoords()
        {
            if (Mesh.TextureCoordinateChannelCount > 0)
            {
                List<Vector2> coords = new List<Vector2>();

                foreach (Vector3D coord in Mesh.TextureCoordinateChannels[0])
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
            float realSize = Size;

            Matrix4 lookAt = Matrix4.LookAt(HWNode.WorldMatrix.ExtractTranslation(), Program.Camera.Position, new Vector3(0, 1, 0));

            Scale = new Vector3(realSize, realSize, realSize);
            ModelMatrix = lookAt;
        }
    }
}
