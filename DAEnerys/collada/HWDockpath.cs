using System.Collections.Generic;
using System.Drawing;

namespace DAEnerys
{
    public class HWDockpath
    {
        public string Name;
        public HWNode Node;

        public string FormattedName
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

                return "DOCK[" + Name + "]" + fams + flags + links;
            }
        }

        public string[] Families;
        public string[] Links;
        public List<DockpathFlag> Flags;
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

        public HWDockpath(HWNode node, string name, string[] families, string[] links, List<DockpathFlag> flags)
        {
            Node = node;
            Node.Dockpath = this;

            Name = name;
            Families = families;
            Links = links;
            Flags = flags;

            HWScene.Dockpaths.Add(this);
            Program.main.AddDockpath(this);
        }

        public void SetupVisualization()
        {
            for(int i = 0; i < Segments.Count - 1; i++) //1 line less than segments
            {
                EditorLine line = new EditorLine(Segments[i].AbsolutePosition, Segments[i + 1].AbsolutePosition, Color.Red, Color.Red);
                Lines.Add(line);
            }
        }
    }

    public enum DockpathFlag
    {
        EXIT = 1,
        LATCH = 2,
        ANIM = 3,
        AJAR = 4,
    }
}
