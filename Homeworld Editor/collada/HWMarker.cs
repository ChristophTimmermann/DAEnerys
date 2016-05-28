using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworldDAEEditor
{
    public class HWMarker
    {
        public HWNode Node;
        public string Name;
        public EditorLine[] Lines = new EditorLine[3];

        private static float markerSize = 1;
        public static float MarkerSize
        {
            get { return markerSize; }
            set { markerSize = value; SetMarkerSize(); }
        }

        public HWMarker(HWNode node, string name)
        {
            Node = node;
            Name = name;
            HWScene.Markers.Add(this);
            Program.main.AddMarker(this);
            float realSize = markerSize;

            Lines[0] = new EditorLine(Node.AbsolutePosition + new Vector3(0, -realSize, 0), Node.AbsolutePosition + new Vector3(0, realSize, 0));
            Lines[1] = new EditorLine(Node.AbsolutePosition + new Vector3(-realSize, 0, 0), Node.AbsolutePosition + new Vector3(realSize, 0, 0));
            Lines[2] = new EditorLine(Node.AbsolutePosition + new Vector3(0, 0, -realSize), Node.AbsolutePosition + new Vector3(0, 0, realSize));
        }

        public static void SetMarkerSize()
        {
            float realSize = markerSize;

            foreach(HWMarker marker in HWScene.Markers)
            {
                marker.Lines[0].Start = marker.Node.AbsolutePosition + new Vector3(0, -realSize, 0);
                marker.Lines[0].End = marker.Node.AbsolutePosition + new Vector3(0, realSize, 0);

                marker.Lines[1].Start = marker.Node.AbsolutePosition + new Vector3(-realSize, 0, 0);
                marker.Lines[1].End = marker.Node.AbsolutePosition + new Vector3(realSize, 0, 0);

                marker.Lines[2].Start = marker.Node.AbsolutePosition + new Vector3(0, 0, -realSize);
                marker.Lines[2].End = marker.Node.AbsolutePosition + new Vector3(0, 0, realSize);
            }
        }
    }
}
