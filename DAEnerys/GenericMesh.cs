using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Vector3 Binormal;

        public Vertex() { }
        public Vertex(Vertex vtx)
        {
            this.Position = vtx.Position;
            this.Normal = vtx.Normal;
            this.Color = vtx.Color;
            this.UV0 = vtx.UV0;
            this.UV1 = vtx.UV1;
            this.Tangent = vtx.Tangent;
            this.Binormal = vtx.Binormal;
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

        public Matrix4 ModelMatrix;
        public Matrix4 ModelViewProjectionMatrix = Matrix4.Identity;

        public Vertex[] Vertices;
        public int[] Indices;

        private GenericMaterial material = new GenericMaterial();
        public GenericMaterial Material { get { return material; } set { material = value; Renderer.Invalidate(); } }
    }
}
