using OpenTK;
using System.Collections.Generic;
using System.Globalization;
using System;

namespace DAEnerys
{
    public class HWDockSegment : HWElement
    {
        public static List<HWDockSegment> DockSegments = new List<HWDockSegment>();

        public int ID;
        private float tolerance;
        public float Tolerance { get { return tolerance; } set { tolerance = value; ToleranceIcosphere.LocalScale = new Vector3(value); } }
        public float Speed;
        public List<DockSegmentFlag> Flags = new List<DockSegmentFlag>();

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
        public EditorDockSegment EditorDockSegment;
        public EditorIcosphere ToleranceIcosphere;

        public HWDockSegment(HWDockpath dockpath, Vector3 pos, Vector3 rot, int id, float tolerance, float speed, DockSegmentFlag[] flags) : base("", HWJoint.Root, pos, rot, Vector3.One)
        {
            Dockpath = dockpath;
            ID = id;
            Speed = speed;
            Flags.AddRange(flags);

            if (id > Dockpath.Segments.Count - 1 || id == -1)
                Dockpath.Segments.Add(this);
            else
                Dockpath.Segments.Insert(id, this);

            EditorDockSegment = new EditorDockSegment(this, new Vector3(1, 0, 0));

            ToleranceIcosphere = new EditorIcosphere(this, new Vector3(1, 1, 0));
            Tolerance = tolerance;
            ToleranceIcosphere.Wireframe = true;
            ToleranceIcosphere.Visible = false;

            DockSegments.Add(this);

            Dockpath.SetupVisualization();
        }

        public override void Destroy()
        {
            DockSegments.Remove(this);
            
            EditorDockSegment.Destroy();
            EditorDockSegment = null;

            ToleranceIcosphere.Destroy();
            ToleranceIcosphere = null;

            Dockpath.Segments.Remove(this);
            Dockpath.SetupVisualization();
            Dockpath = null;

            base.Destroy();
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
