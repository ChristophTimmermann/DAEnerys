using Assimp;

namespace DAEnerys
{
    public class HWEngineShape : HWMesh
    {
        new public HWJoint Parent;

        public override string FormattedName
        {
            get
            {
                return "ETSH[" + Name + "]";
            }
        }

        public int EngineShapeListItemIndex;

        public HWEngineShape(Mesh assimpMesh, HWJoint parent, string name) : base(assimpMesh)
        {
            Parent = parent;
            Name = name;

            HWScene.EngineShapes.Add(this);
            Program.main.AddEngineShape(this);
        }
    }
}
