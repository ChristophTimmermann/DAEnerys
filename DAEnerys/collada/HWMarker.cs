using OpenTK;
using System.Collections.Generic;
using System.Drawing;

namespace DAEnerys
{
    public class HWMarker : HWElement
    {
        public static List<HWMarker> Markers = new List<HWMarker>();

        public EditorLine[] Lines = new EditorLine[3];

        public override string FormattedName
        {
            get
            {
                return "MARK[" + Name + "]";
            }
        }

        private static float markerSize = 1;
        public static float MarkerSize
        {
            get { return markerSize; }
            set { markerSize = value; SetMarkerSize(); }
        }

        public HWMarker(string name, HWJoint parent, Matrix4 transform) : base(name, parent, transform)
        {
            Markers.Add(this);
            Program.main.AddMarker(this);
            float realSize = markerSize;

            Lines[0] = new EditorLine(AbsolutePosition + new Vector3(0, -realSize, 0), AbsolutePosition + new Vector3(0, realSize, 0), Color.Red, Color.Red);
            Lines[1] = new EditorLine(AbsolutePosition + new Vector3(-realSize, 0, 0), AbsolutePosition + new Vector3(realSize, 0, 0), Color.Red, Color.Red);
            Lines[2] = new EditorLine(AbsolutePosition + new Vector3(0, 0, -realSize), AbsolutePosition + new Vector3(0, 0, realSize), Color.Red, Color.Red);
        }

        public static void SetMarkerSize()
        {
            float realSize = markerSize;

            foreach(HWMarker marker in Markers)
            {
                marker.Lines[0].Start = marker.AbsolutePosition + new Vector3(0, -realSize, 0);
                marker.Lines[0].End = marker.AbsolutePosition + new Vector3(0, realSize, 0);

                marker.Lines[1].Start = marker.AbsolutePosition + new Vector3(-realSize, 0, 0);
                marker.Lines[1].End = marker.AbsolutePosition + new Vector3(realSize, 0, 0);

                marker.Lines[2].Start = marker.AbsolutePosition + new Vector3(0, 0, -realSize);
                marker.Lines[2].End = marker.AbsolutePosition + new Vector3(0, 0, realSize);
            }
        }
    }
}
