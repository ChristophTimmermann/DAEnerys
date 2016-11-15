using Assimp;

namespace DAEnerys
{
    public class HWCollisionMesh : HWMesh
    {
        new public HWJoint Parent;

        public override string FormattedName
        {
            get
            {
                return "COL[" + Name + "]";
            }
        }

        public int CollisionMeshListItemIndex;

        public HWCollisionMesh(Mesh assimpMesh, HWJoint parent, string name) : base(assimpMesh)
        {
            this.Parent = parent;
            this.Name = name;

            HWScene.CollisionMeshes.Add(this);
            Program.main.AddCollisionMesh(this);
        }
    }
}
