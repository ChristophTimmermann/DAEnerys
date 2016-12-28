using OpenTK;
using System.Collections.Generic;
using System.Globalization;

namespace DAEnerys
{
    public class HWDockSegment : HWElement
    {
        public static List<HWDockSegment> DockSegments = new List<HWDockSegment>();

        public int ID;
        public float Tolerance;
        public float Speed;
        public List<DockSegmentFlag> Flags;

        public override string FormattedName
        {
            get
            {
                string tol = "_Tol[" + Tolerance.ToString(CultureInfo.InvariantCulture) + "]";
                string speed = "_Spd[" + Speed.ToString(CultureInfo.InvariantCulture) + "]";

                string flags = "";
                if (Flags.Count > 0)
                {
                    flags = "_Flags[";
                    for (int i = 0; i < Flags.Count; i++)
                    {
                        flags += Flags[i];
                        if (i < Flags.Count - 1)
                            flags += " ";
                    }
                    flags += "]";
                }

                return "SEG[" + ID + "]" + tol + speed + flags;
            }
        }

        public HWDockpath Dockpath;
        public EditorIcosphere Icosphere;
        public EditorIcosphere ToleranceIcosphere;

        public HWDockSegment(HWDockpath dockpath, Matrix4 transform, int id, float tolerance, float speed, List<DockSegmentFlag> flags) : base("", HWJoint.Root, transform)
        {
            Dockpath = dockpath;
            ID = id;
            Tolerance = tolerance;
            Speed = speed;
            Flags = flags;

            Dockpath.Segments.Add(this);

            Icosphere = new EditorIcosphere(this, new Vector3(1, 0, 0));
            Icosphere.LocalScale = new Vector3(5);

            ToleranceIcosphere = new EditorIcosphere(this, new Vector3(1, 1, 0));
            ToleranceIcosphere.LocalScale = new Vector3(Tolerance);
            ToleranceIcosphere.Wireframe = true;
            ToleranceIcosphere.Visible = false;

            DockSegments.Add(this);

            Dockpath.SetupVisualization();
        }
    }

    public enum DockSegmentFlag
    {
        UseRot = 1,
        Player = 2,
        Queue = 3,
        Close = 4,
        ClearRes = 5,
        Check = 6,
        UnFocus = 7,
        Clip = 8,
    }
}
