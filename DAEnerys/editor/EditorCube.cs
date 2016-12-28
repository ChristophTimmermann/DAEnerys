using Assimp;
using OpenTK;
using System.Collections.Generic;

namespace DAEnerys
{
    public class EditorCube : EditorMesh
    {
        public static MeshData Data;

        private Vector3 color;
        public Vector3 Color { get { return color; } set { color = value; Material.DiffuseColor = value; } }

        public EditorCube(Element parent, Vector3 color) : base(Data, parent, new GenericMaterial(color))
        {
            this.Color = color;
        }
    }
}
