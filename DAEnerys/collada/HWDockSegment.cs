using OpenTK;
using System.Collections.Generic;
using System.Drawing;

namespace DAEnerys
{
    public class HWDockSegment
    {
        public HWNode Node;
        public int ID;
        public float Tolerance;
        public float Speed;
        public List<DockSegmentFlag> Flags;
        public HWDockpath Dockpath;
        public EditorIcosphere Icosphere;
        public EditorIcosphere ToleranceIcosphere;

        public HWDockSegment(HWNode node, int id, float tolerance, float speed, List<DockSegmentFlag> flags)
        {
            Node = node;
            ID = id;
            Tolerance = tolerance;
            Speed = speed;
            Flags = flags;

            Dockpath = node.Parent.Dockpath;
            Dockpath.Segments.Add(this);

            Icosphere = new EditorIcosphere(Node, new Vector3(1, 0, 0));
            Icosphere.Scale = new Vector3(5, 5, 5);

            ToleranceIcosphere = new EditorIcosphere(node, new Vector3(1, 1, 0));
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
