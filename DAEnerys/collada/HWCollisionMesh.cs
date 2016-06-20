using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAEnerys
{
    public class HWCollisionMesh
    {
        public HWMesh Mesh;
        public HWJoint Parent;
        public string Name;

        public int CollisionMeshListItemIndex;

        public HWCollisionMesh(HWMesh mesh, HWJoint parent, string name)
        {
            Mesh = mesh;
            Parent = parent;
            Name = name;

            HWScene.CollisionMeshes.Add(this);
            Program.main.AddCollisionMesh(this);
        }
    }
}
