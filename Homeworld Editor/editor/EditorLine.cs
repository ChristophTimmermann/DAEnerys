using Assimp;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworldDAEEditor
{
    public class EditorLine : EditorMesh
    {
        public static new Vector3 Scale = Vector3.One;

        public Color StartColor;
        public Color EndColor;

        public Vector3 Start;
        public Vector3 End;

        public override int VertexCount { get { return 2; } }
        public override int IndiceCount { get { return 2; } }

        public EditorLine(Vector3 start, Vector3 end, Color startColor, Color endColor) : base()
        {
            this.StartColor = startColor;
            this.EndColor = endColor;

            this.Start = start;
            this.End = end;
        }

        public override Vector3[] GetVertices()
        {
            return new Vector3[] { Start, End };
        }

        public override Vector3[] GetNormals()
        {
            return new Vector3[0];
        }

        public override int[] GetIndices(int offset = 0)
        {
            int[] indices = { 0, 1 };

            if (offset != 0)
            {
                for (int i = 0; i < indices.Length; i++)
                {
                    indices[i] += offset;
                }
            }

            return indices;
        }

        public override Vector3[] GetColorData()
        {
            Vector3[] colorData = { new Vector3(StartColor.R, StartColor.G, StartColor.B), new Vector3(EndColor.R, EndColor.G, EndColor.B) };
            return colorData;
        }

        public override Vector2[] GetTextureCoords()
        {
            return new Vector2[0];
        }

        /// <summary>
        /// Calculates the model matrix from transforms
        /// </summary>
        public override void CalculateModelMatrix()
        {
            ModelMatrix = Matrix4.CreateScale(Scale);
        }
    }
}
