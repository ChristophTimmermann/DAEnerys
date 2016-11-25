using OpenTK;
using System.Collections.Generic;

namespace DAEnerys
{
    public class HWCollisionMesh : HWMesh
    {
        public static List<HWCollisionMesh> CollisionMeshes = new List<HWCollisionMesh>();

        public override string FormattedName
        {
            get
            {
                return "COL[" + Name + "]";
            }
        }

        public int CollisionMeshListItemIndex;

        public HWCollisionMesh(MeshData data, Matrix4 transform, HWJoint parent, string name) : base(data, transform, new HWMaterial())
        {
            this.Parent = parent;
            this.Name = name;

            CollisionMeshes.Add(this);
            Program.main.AddCollisionMesh(this);
        }
    }
}
