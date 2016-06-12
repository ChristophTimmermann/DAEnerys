using Assimp;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworldDAEEditor
{
    public abstract class EditorMesh
    {
        public EditorMaterial Material = new EditorMaterial();

        public Vector3 Scale = Vector3.One;
        public bool Visible = false;
        public bool Shaded = false;
        public bool NeverDrawInFront = false;
        public bool BlackIsTransparent = false;
        public bool Wireframe = false;
        public bool DrawAboveShip = false;

        public Matrix4 ModelMatrix = Matrix4.Identity;
        public Matrix4 ViewProjectionMatrix = Matrix4.Identity;
        public Matrix4 ModelViewProjectionMatrix = Matrix4.Identity;

        public abstract int VertexCount { get; }
        public abstract int IndiceCount { get; }

        public EditorMesh()
        {
            EditorScene.meshes.Add(this);
        }

        public abstract Vector3[] GetVertices();
        public abstract Vector3[] GetNormals();
        public abstract int[] GetIndices(int offset = 0);
        public abstract Vector3[] GetColorData();
        public abstract Vector2[] GetTextureCoords();
        public abstract void CalculateModelMatrix();
    }
}
