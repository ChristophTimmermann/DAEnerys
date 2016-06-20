using Assimp;
using OpenTK;
using System;
using System.Collections.Generic;

namespace DAEnerys
{
    public class EditorJoint : EditorMesh
    {
        public static Mesh Mesh;
        public HWNode HWNode;

        public static float Size = 10;

        public static EditorMaterial JointMaterial;

        public override int VertexCount { get { return Mesh.VertexCount; } }
        public override int IndiceCount { get { return Indices.Length; } }

        public EditorJoint(HWJoint joint) : base()
        {
            this.HWNode = joint.Node;
            this.Material = JointMaterial;

            Vertices = GetVertices();
            Normals = GetNormals();
            Indices = GetIndices();
            Colors = GetColorData();
            TextureCoords = GetTextureCoords();
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

            if(offset != 0)
            {
                for(int i = 0; i < indices.Length; i++)
                {
                    indices[i] += offset;
                }
            }

            return indices;
        }

        public override Vector3[] GetColorData()
        {
            Vector3[] colorData = new Vector3[VertexCount];
            for(int i = 0;i < VertexCount; i++)
            {
                colorData[i] = new Vector3(Mesh.VertexColorChannels[0][i].R, Mesh.VertexColorChannels[0][i].G, Mesh.VertexColorChannels[0][i].B);
            }

            return colorData;
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

            Scale = new Vector3(realSize, realSize, realSize);
            ModelMatrix = Matrix4.CreateScale(Scale) * HWNode.WorldMatrix;
        }
    }
}
