using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAEnerys
{
    public class HWEngineShape
    {
        public HWMesh Mesh;
        public HWJoint Parent;
        public string Name;

        public int EngineShapeListItemIndex;

        public HWEngineShape(HWMesh mesh, HWJoint parent, string name)
        {
            Mesh = mesh;
            Parent = parent;
            Name = name;

            HWScene.EngineShapes.Add(this);
            Program.main.AddEngineShape(this);
        }
    }
}
