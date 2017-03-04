using OpenTK;
using System.Collections.Generic;
using System;

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

        public HWEngineShape(MeshData data, Vector3 pos, Vector3 rot, Vector3 scale, HWJoint parent, string name) : base(data, parent, pos, rot, scale, HWMaterial.DefaultMaterial)
        {
            Parent = parent;
            Name = name;

            EngineShapes.Add(this);
            Program.main.AddEngineShape(this);
        }

        public static HWEngineShape GetByName(string name)
        {
            foreach (HWEngineShape engineShape in EngineShapes)
                if (engineShape.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                    return engineShape;

            return null;
        }

        public override void Destroy()
        {
            Program.main.RemoveEngineShape(this);
            EngineShapes.Remove(this);
            Parent = null;

            base.Destroy();
        }
    }
}
