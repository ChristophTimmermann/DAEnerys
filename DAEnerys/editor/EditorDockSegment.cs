using Assimp;
using OpenTK;
using System.Collections.Generic;

namespace DAEnerys
{
    public class EditorDockSegment : EditorMesh
    {
        private static float size = 5;
        public static float Size { get { return size; } set { size = value; foreach (HWDockSegment segment in HWDockSegment.DockSegments) segment.EditorDockSegment.LocalScale = new Vector3(value); } }

        public static MeshData Data;
        public Vector3 Color { get { return Icosphere.Color; } set { Icosphere.Color = value; } }
        public override bool Visible { get { return base.Visible; } set { base.Visible = value; Icosphere.Visible = value; } }

        private EditorIcosphere Icosphere;

        public EditorDockSegment(HWDockSegment dockSegment, Vector3 color) : base(Data, dockSegment, null)
        {
            LocalScale = new Vector3(Size);

            Icosphere = new EditorIcosphere(this, color);
            VertexColored = true;
        }

        public override void Destroy()
        {
            Icosphere.Destroy();

            base.Destroy();
        }
    }
}
