using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace DAEnerys
{
    public class HWMarker : HWElement
    {
        public static List<HWMarker> Markers = new List<HWMarker>();

        private static bool displayMarkers = false;
        public static bool DisplayMarkers { get { return displayMarkers; } set { displayMarkers = value; foreach (HWMarker marker in Markers) marker.EditorMarker.Visible = value; } }

        public EditorMarker EditorMarker;

        public override string FormattedName
        {
            get
            {
                return "MARK[" + Name + "]";
            }
        }

        public HWMarker(string name, HWJoint parent) : this(name, parent, Vector3.Zero, Vector3.Zero, Vector3.One)
        {

        }
        public HWMarker(string name, HWJoint parent, Vector3 pos, Vector3 rot, Vector3 scale) : base(name, parent, pos, rot, scale)
        {
            Markers.Add(this);
            Program.main.AddMarker(this);

            //Visualization
            EditorMarker = new EditorMarker(this);
            EditorMarker.Visible = DisplayMarkers;
        }

        public static HWMarker GetByName(string name)
        {
            foreach (HWMarker marker in Markers)
                if (marker.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                    return marker;

            return null;
        }

        public override void Destroy()
        {
            base.Destroy();

            Program.main.RemoveMarker(this);
            Markers.Remove(this);
            EditorMarker.Destroy();
            EditorMarker = null;
        }
    }
}
