using OpenTK;
using System.Collections.Generic;

namespace DAEnerys
{
    public class Vertex
    {
        public Vector3 Position;
        public Vector3 Normal;
        public Vector3 Color;
        public Vector2 UV0;
        public Vector2 UV1;
        public Vector3 Tangent;
        public Vector3 BiTangent;

        public Vertex() { }
        public Vertex(Vertex vtx)
        {
            this.Position = vtx.Position;
            this.Normal = vtx.Normal;
            this.Color = vtx.Color;
            this.UV0 = vtx.UV0;
            this.UV1 = vtx.UV1;
            this.Tangent = vtx.Tangent;
            this.BiTangent = vtx.BiTangent;
        }
    }

    //This is used to move mesh data around
    public class MeshData
    {
        public Vertex[] Vertices;
        public int[] Indices;
        public int UVCount;

        public MeshData()
        {
            Vertices = new Vertex[0];
            Indices = new int[0];
            UVCount = 1;
        }
        public MeshData(Vertex[] vertices, int[] indices, int uvCount)
        {
            Vertices = vertices;
            Indices = indices;
            UVCount = uvCount;
        }
    }

    public abstract class GenericMesh
    {
        private bool visible = false;
        public virtual bool Visible { get { return visible; } set { visible = value; } }

        public bool Shaded = true;
        public bool Translucent = false;
        public bool VertexColored = true;
        public Vector3 Scale = Vector3.One;

        public Matrix4 ModelMatrix = Matrix4.Identity;
        public Matrix4 ModelViewProjectionMatrix = Matrix4.Identity;

        public Vertex[] VertexList;
        private int[] indices;

        public Vector3[] Vertices
        {
            get
            {
                List<Vector3> vertices = new List<Vector3>();
                foreach (Vertex vertex in VertexList)
                    vertices.Add(vertex.Position);
                return vertices.ToArray();
            }
        }
        public Vector3[] Normals
        {
            get
            {
                List<Vector3> normals = new List<Vector3>();
                foreach (Vertex vertex in VertexList)
                    normals.Add(vertex.Normal);
                return normals.ToArray();
            }
        }
        public Vector3[] Colors
        {
            get
            {
                List<Vector3> colors = new List<Vector3>();
                foreach (Vertex vertex in VertexList)
                    colors.Add(vertex.Color);
                return colors.ToArray();
            }
        }
        public Vector2[] UV0
        {
            get
            {
                List<Vector2> uv0 = new List<Vector2>();
                foreach (Vertex vertex in VertexList)
                    uv0.Add(vertex.UV0);
                return uv0.ToArray();
            }
        }
        public Vector2[] UV1
        {
            get
            {
                List<Vector2> uv1 = new List<Vector2>();
                foreach (Vertex vertex in VertexList)
                    uv1.Add(vertex.UV1);
                return uv1.ToArray();
            }
        }
        public Vector3[] Tangents
        {
            get
            {
                List<Vector3> tangents = new List<Vector3>();
                foreach (Vertex vertex in VertexList)
                    tangents.Add(vertex.Tangent);
                return tangents.ToArray();
            }
        }
        public Vector3[] BiTangents
        {
            get
            {
                List<Vector3> biTangents = new List<Vector3>();
                foreach (Vertex vertex in VertexList)
                    biTangents.Add(vertex.BiTangent);
                return biTangents.ToArray();
            }
        }
        public int[] Indices
        {
            get
            {
                return indices;
            }
        }

        public int VertexCount { get { return VertexList.Length; } }
        public int IndexCount { get { return indices.Length; } }
        public int FaceCount { get { return IndexCount / 3; } }

        public int UVCount { get; private set; } = 0;

        private GenericMaterial material = new GenericMaterial();
        public GenericMaterial Material { get { return material; } set { material = value; Renderer.Invalidate(); } }

        public int[] GetIndices(int offset = 0)
        {
            int[] offsetIndices = new int[indices.Length];
            indices.CopyTo(offsetIndices, 0);

            if (offset > 0)
            {
                for (int i = 0; i < indices.Length; i++)
                    offsetIndices[i] += offset;
            }

            return offsetIndices;
        }

        public void SetData(MeshData data)
        {
            this.VertexList = data.Vertices;
            this.indices = data.Indices;
            this.UVCount = data.UVCount;

            if (Normals.Length == 0)
                RecalculateNormals();

            Renderer.InvalidateMeshData();
            Renderer.Invalidate();
        }

        private void RecalculateNormals()
        {
            // See if we have anything to do.
            if (VertexCount == 0 || IndexCount == 0)
                return;

            Vector3[] normals = new Vector3[Vertices.Length];
            for (int i = 0; i < IndexCount; i += 3)
            {
                int ind1 = Indices[i + 0];
                int ind2 = Indices[i + 1];
                int ind3 = Indices[i + 2];
                Vector3 v1 = Vertices[ind1];
                Vector3 v2 = Vertices[ind2];
                Vector3 v3 = Vertices[ind3];
                Vector3 v4 = Vector3.Cross(v2 - v1, v3 - v1);
                normals[ind1] += v4;
                normals[ind2] += v4;
                normals[ind3] += v4;
            }
            for (int i = 0; i < normals.Length; ++i)
            {
                normals[i].Normalize();
                Normals[i] = normals[i];
            }
        }

        private void RecalculateTangents()
        {
            // float tolerance = 0.01f;
            foreach (Vertex vtx in VertexList)
            {
                vtx.Tangent = new Vector3();
                vtx.BiTangent = new Vector3();
            }
            int[] altv = new int[Vertices.Length];
            int[] handedness = new int[Vertices.Length];
            for (int i = 0; i < Vertices.Length; ++i)
            {
                altv[i] = -1;
                handedness[i] = 0;
            }
            for (int i = 0; i < IndexCount; i += 3)
            {
                Vector3 fT, fB;
                int i1 = Indices[i + 0];
                int i2 = Indices[i + 1];
                int i3 = Indices[i + 2];
                int fH, N;
                _CalcFaceTangents(out fT, out fB, out fH, i1, i2, i3);
                _UpdateVertTangents(altv, ref handedness, fT, fB, fH, i1, out N);
                Indices[i1] = N;
                _UpdateVertTangents(altv, ref handedness, fT, fB, fH, i2, out N);
                Indices[i2] = N;
                _UpdateVertTangents(altv, ref handedness, fT, fB, fH, i3, out N);
                Indices[i3] = N;
            }
            for (int i = 0; i < Vertices.Length; ++i)
                _NormaliseVertTangents(handedness[i], i);
        }

        private void _CalcFaceTangents(
            out Vector3 tangent, out Vector3 bitangent, out int hand,
            int i1, int i2, int i3)
        {
            Vector3 X, Y, Z, S, T;
            Vector3 norm, temp, v1, v2, v3;
            float R, dp;

            X.X = Vertices[i2].X - Vertices[i1].X;
            X.Y = Vertices[i3].X - Vertices[i1].X;

            Y.X = Vertices[i2].Y - Vertices[i1].Y;
            Y.Y = Vertices[i3].Y - Vertices[i1].Y;

            Z.X = Vertices[i2].Z - Vertices[i1].Z;
            Z.Y = Vertices[i3].Z - Vertices[i1].Z;

            S.X = UV0[i2].X - UV0[i1].X;
            S.Y = Vertices[i3].X - UV0[i1].X;

            T.X = UV0[i2].Y - UV0[i1].Y;
            T.Y = UV0[i3].Y - UV0[i1].Y;

            R = S.X * T.Y - S.Y * T.X;

            if (R != 0)
                R = 1f / R;

            tangent.X = (T.Y * X.X - T.X * X.Y) * R;
            tangent.Y = (T.Y * Y.X - T.X * Y.Y) * R;
            tangent.Z = (T.Y * Z.X - T.X * Z.Y) * R;

            if (tangent.X == 0 && tangent.Y == 0 && tangent.Z == 0)
            {
                // no actual texture variation so use vertex vector to help prevent things breaking
                tangent.X = X.X;
                tangent.Y = Y.X;
                tangent.Z = Z.X;
                // Zero tangent found, aligning with vector between first and second vertex
            }

            tangent.Normalize();

            bitangent.X = (S.X * X.Y - S.Y * X.X) * R;
            bitangent.Y = (S.X * Y.Y - S.Y * Y.X) * R;
            bitangent.Z = (S.X * Z.Y - S.Y * Z.X) * R;

            if (bitangent.X == 0 && bitangent.Y == 0 && bitangent.Z == 0)
            {
                // no actual texture variation so use vertex vector to help prevent things breaking
                bitangent.X = X.Y;
                bitangent.Y = Y.Y;
                bitangent.Z = Z.Y;
                // Zero bitangent found, aligning with vector between first and third vertex
            }
            bitangent.Normalize();

            // calculate face normal
            v1 = Vertices[i1];
            v2 = Vertices[i2];
            v3 = Vertices[i3];
            norm = Vector3.Cross(v2 - v1, v3 - v1);
            norm.Normalize();

            // calculate face handedness
            temp = Vector3.Cross(norm, tangent);
            dp = Vector3.Dot(temp, bitangent);
            if (dp < 0)
                hand = -1;
            else
                hand = 1;
        }

        private void _UpdateVertTangents(int[] altv, ref int[] handedness, Vector3 fT, Vector3 fB, int fH, int v, out int n)
        {
            n = v;
            Tangents[n] += fT;
            BiTangents[n] += fB;
            //if (handedness[v] != 0 && fH != handedness[v])
            //{
            //    // need to search for or create an alternately handed vertex
            //    n = altv[v];
            //    if (n == -1)
            //    {
            //        // no alternate vertex found, clone current, reset tangents incase they have been set
            //        Vertex[] tempV = Vertices;
            //        Vertices = new Vertex[Vertices.Length + 1];
            //        Array.Copy(tempV, Vertices, tempV.Length);
            //        n = Vertices.Length - 1;
            //        Vertices[n] = new Vertex(Vertices[v]);
            //        Vertices[n].Tangent = new Vector3();
            //        Vertices[n].Binormal = new Vector3();

            //        int[] temp = handedness;
            //        handedness = new int[handedness.Length + 1];
            //        Array.Copy(temp, handedness, temp.Length);
            //        handedness[n] = fH;
            //        altv[v] = n;
            //    }
            //    Vertices[n].Tangent += fT;
            //    Vertices[n].Binormal += fB;
            //}
            //else
            //{
            //    Vertices[v].Tangent += fT;
            //    Vertices[v].Binormal += fB;
            //    handedness[v] = fH;
            //    n = v;
            //}
        }

        private void _NormaliseVertTangents(int handedness, int i)
        {
            // normalise (average) accumulated tangents
            Vector3 nTangent = Vector3.Normalize(Tangents[i]);
            Vector3 nBitangent = Vector3.Normalize(BiTangents[i]);

            Vector3 norm;
            // Gram-Schmidt orthogonalize
            norm.X = Normals[i].X;
            norm.Y = Normals[i].Y;
            norm.Z = Normals[i].Z;
            norm.Normalize(); // Paranoia!

            float dp = Vector3.Dot(norm, nTangent);
            if (dp == 1 || dp == -1)
                Log.WriteLine("Vertex" + i + ": Tangent generation failed, normal and tangent parallel.");
            Vector3 temp = norm * dp;
            temp = nTangent - temp;
            Tangents[i] = Vector3.Normalize(temp);
            BiTangents[i] = Vector3.Cross(norm, Tangents[i]);

            // Flip Bitangent according to local handedness
            temp = Vector3.Cross(norm, nTangent);
            dp = Vector3.Dot(temp, nBitangent);
            if (dp < 0)
                BiTangents[i] *= -1;
        }
    }
}
