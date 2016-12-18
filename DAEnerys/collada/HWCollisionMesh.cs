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
                return "COL[" + Parent.Name + "]";
            }
        }

        public int ItemIndex;

        public HWCollisionMesh(MeshData data, Matrix4 transform, HWJoint parent) : base(data, transform, HWMaterial.DefaultMaterial)
        {
            this.Parent = parent;

            CollisionMeshes.Add(this);
            Program.main.AddCollisionMesh(this);
        }

        public override void Destroy()
        {
            Program.main.RemoveCollisionMesh(this);
            CollisionMeshes.Remove(this);
            Parent = null;

            base.Destroy();
        }
    }
}
