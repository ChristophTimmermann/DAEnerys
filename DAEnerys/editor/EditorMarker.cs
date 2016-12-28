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

        public EditorMarker(HWMarker marker) : base(Data, marker, new GenericMaterial(new Vector3(1, 1, 0)))
        {
            LocalScale = new Vector3(Size);
        }
    }
}
