using System.Collections.Generic;
using OpenTK;
using System.Drawing;

namespace HomeworldDAEEditor
{
    public class HWDockpath
    {
        public HWNode Node;
        public string Name;
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
                EditorLine line = new EditorLine(Segments[i].Node.AbsolutePosition, Segments[i + 1].Node.AbsolutePosition, Color.Red, Color.Red);
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
