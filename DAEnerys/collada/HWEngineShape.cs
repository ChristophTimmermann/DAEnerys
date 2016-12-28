using OpenTK;
using System.Collections.Generic;

namespace DAEnerys
{
    public class HWEngineShape : HWMesh
    {
        public static List<HWEngineShape> EngineShapes = new List<HWEngineShape>();

        public override string FormattedName
        {
            get
            {
                return "ETSH[" + Name + "]";
            }
        }

        public int EngineShapeListItemIndex;

        public HWEngineShape(MeshData data, Matrix4 transform, HWJoint parent, string name) : base(data, parent, transform, HWMaterial.DefaultMaterial)
        {
            Parent = parent;
            Name = name;

            EngineShapes.Add(this);
            Program.main.AddEngineShape(this);
        }
    }
}
