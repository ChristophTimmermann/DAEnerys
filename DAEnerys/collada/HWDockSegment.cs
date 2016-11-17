using Assimp;
using OpenTK;
using System.Collections.Generic;

namespace DAEnerys
{
    public class HWDockSegment : HWNode
    {
        public int ID;
        public float Tolerance;
        public float Speed;
        public List<DockSegmentFlag> Flags;

        public override string FormattedName
        {
            get
            {
                string tol = "_Tol[" + Tolerance + "]";
                string speed = "_Spd[" + Speed + "]";

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

        new public HWDockpath Dockpath;
        public EditorIcosphere Icosphere;
        public EditorIcosphere ToleranceIcosphere;

        public HWDockSegment(Node assimpNode, HWNode parent, int id, float tolerance, float speed, List<DockSegmentFlag> flags) : base(assimpNode, parent)
        {
            if (!IsUnderAnyDockpath())
            {
                new Problem(ProblemTypes.ERROR, "The dockpath segment \"" + Name + "\" is not under any dockpath.");
                return;
            }

            ID = id;
            Tolerance = tolerance;
            Speed = speed;
            Flags = flags;

            Dockpath = parent.Dockpath;
            if (Dockpath == null)
            {
                Problem.Problems.Add(new Problem(ProblemTypes.ERROR, "Dockpath error with node " + assimpNode.Name.ToString()));
            } else
                Dockpath.Segments.Add(this);

            Icosphere = new EditorIcosphere(this, new Vector3(1, 0, 0));
            Icosphere.Scale = new Vector3(5, 5, 5);

            ToleranceIcosphere = new EditorIcosphere(this, new Vector3(1, 1, 0));
            ToleranceIcosphere.Scale = new Vector3(Tolerance);
            ToleranceIcosphere.Wireframe = true;
            ToleranceIcosphere.Visible = false;

            HWScene.DockSegments.Add(this);
        }
    }

    public enum DockSegmentFlag
    {
        USEROT = 1,
        PLAYER = 2,
        QUEUE = 3,
        CLOSE = 4,
        CLEARRES = 5,
        CHECK = 6,
        UNFOCUS = 7,
        CLIP = 8,
    }
}
