using OpenTK;
using System.Collections.Generic;
using System.Drawing;

namespace DAEnerys
{
    public class HWDockpath : HWElement
    {
        public static List<HWDockpath> Dockpaths = new List<HWDockpath>();

        public override string FormattedName
        {
            get
            {
                string fams = "";
                if (Families.Length > 0)
                {
                    fams = "_Fam[";
                    for (int i = 0; i < Families.Length; i++)
                    {
                        fams += Families[i];
                        if (i < Families.Length - 1)
                            fams += ", ";
                    }
                    fams += "]";
                }

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

                string links = "";
                if (Links.Length > 0)
                {
                    links = "_Link[";
                    for (int i = 0; i < Links.Length; i++)
                    {
                        links += Links[i];
                        if (i < Links.Length - 1)
                            links += ", ";
                    }
                    links += "]";
                }

                string animIndex = "";
                if (AnimationIndex > 0)
                {
                    animIndex = "_MAD[" + AnimationIndex + "]";
                }

                return "DOCK[" + Name + "]" + fams + flags + links + animIndex;
            }
        }

        public string[] Families;
        public string[] Links;
        public List<DockpathFlag> Flags;
        public int AnimationIndex;
        public List<HWDockSegment> Segments = new List<HWDockSegment>();
        public List<EditorLine> Lines = new List<EditorLine>();

        private bool visible;
        public bool Visible
        {
            get { return visible; }
            set
            {
                visible = value;
                foreach(EditorLine line in Lines)
                {
                    line.Visible = value;
                }

                foreach(HWDockSegment segment in Segments)
                {
                    segment.Icosphere.Visible = value;
                }
            }
        }

        public HWDockpath(string name, string[] families, string[] links, List<DockpathFlag> flags, int animationIndex) : base(name, HWJoint.Root, Matrix4.Identity)
        {
            Name = name;
            Families = families;
            Links = links;
            Flags = flags;
            AnimationIndex = animationIndex;

            Dockpaths.Add(this);
            Program.main.AddDockpath(this);
        }

        public void SetupVisualization()
        {
            foreach (EditorLine line in Lines)
                line.Destroy();

            Lines.Clear();

            for(int i = 0; i < Segments.Count - 1; i++) //1 line less than segments
            {
                EditorLine line = new EditorLine(Segments[i].AbsolutePosition, Segments[i + 1].AbsolutePosition, Color.Red, Color.Red);
                Lines.Add(line);
            }
        }
    }

    public enum DockpathFlag
    {
        Exit = 1,
        Latch = 2,
        Anim = 3,
        Ajar = 4,
    }
}
