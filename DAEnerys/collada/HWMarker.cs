using OpenTK;
using System.Collections.Generic;
using System.Drawing;

namespace DAEnerys
{
    public class HWMarker : HWElement
    {
        public static List<HWMarker> Markers = new List<HWMarker>();

        public EditorMarker EditorMarker;

        public override string FormattedName
        {
            get
            {
                return "MARK[" + Name + "]";
            }
        }

        public HWMarker(string name, HWJoint parent, Matrix4 transform) : base(name, parent, transform)
        {
            Markers.Add(this);
            Program.main.AddMarker(this);

            //Visualization
            EditorMarker = new EditorMarker(this);
        }

        public override void Destroy()
        {
            base.Destroy();

            Markers.Remove(this);
            EditorMarker.Destroy();
            EditorMarker = null;
        }
    }
}
