using Assimp;
using OpenTK;
using System.Collections.Generic;

namespace DAEnerys
{
    public class EditorMarker : EditorMesh
    {
        public static MeshData Data;

        private static float size = 10;
        public static float Size { get { return size; } set { size = value; foreach (HWMarker marker in HWMarker.Markers) marker.EditorMarker.LocalScale = new Vector3(value); } }

        private Vector3 color = new Vector3(1, 1, 0);
        public Vector3 Color { get { return color; } set { color = value; Material.DiffuseColor = value; } }

        public EditorMarker(HWMarker marker) : base(Data, marker, new GenericMaterial(new Vector3(1, 1, 0)))
        {
            LocalScale = new Vector3(Size);
        }
    }
}
