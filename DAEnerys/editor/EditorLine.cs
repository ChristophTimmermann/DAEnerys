using OpenTK;
using System.Drawing;
using System.Collections.Generic;

namespace DAEnerys
{
    public class EditorLine : EditorMesh
    {
        public static List<EditorLine> EditorLines = new List<EditorLine>();

        private Vector3 startColor = Vector3.One;
        public Vector3 StartColor { get { return startColor; } set { startColor = value; UpdateData(); } }
        private Vector3 endColor = Vector3.One;
        public Vector3 EndColor { get { return endColor; } set { endColor = value; UpdateData(); } }

        private Vector3 start = Vector3.Zero;
        public Vector3 Start { get { return start; } set { start = value; UpdateData(); } }
        private Vector3 end = new Vector3(10, 10, 10);
        public Vector3 End { get { return end; } set { end = value; UpdateData(); } }

        public EditorLine(Vector3 start, Vector3 end, Vector3 startColor, Vector3 endColor, Element parent) : base(new MeshData(), parent, null)
        {
            this.StartColor = startColor;
            this.EndColor = endColor;

            this.Start = start;
            this.End = end;

            this.VertexColored = true;

            EditorLines.Add(this);
        }

        public override void Destroy()
        {
            EditorLines.Remove(this);

            base.Destroy();
        }

        private void UpdateData()
        {
            Vertex[] vertices = new Vertex[2];
            vertices[0] = new Vertex();
            vertices[0].Position = start;
            vertices[0].Color = startColor;

            vertices[1] = new Vertex();
            vertices[1].Position = end;
            vertices[1].Color = endColor;

            int[] indices = { 0, 1 };

            MeshData data = new MeshData(vertices, indices, 0);
            SetData(data);
        }
    }
}
