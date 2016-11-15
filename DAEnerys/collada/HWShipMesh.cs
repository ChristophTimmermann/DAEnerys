using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DAEnerys
{
    public class HWShipMesh
    {
        private HWJoint parent;
        public HWJoint Parent
        {
            get { return parent; }
            set
            {
                parent = value;
                if (value != null)
                    foreach (HWShipMeshLOD mesh in Meshes)
                    {
                        mesh.Parent.Parent = value.Node;
                        mesh.CalculateBoundingBox();
                    }
                else
                    foreach (HWShipMeshLOD mesh in Meshes)
                    {
                        mesh.Parent.Parent = HWNode.Roots[mesh.LOD];
                        mesh.CalculateBoundingBox();
                    }
            }
        }
        public string Name;

        public List<ShipMeshTag> Tags = new List<ShipMeshTag>();

        public List<HWShipMeshLOD> Meshes = new List<HWShipMeshLOD>();
        public List<HWShipMeshLOD>[] LODMeshes = new List<HWShipMeshLOD>[4];

        public object ShipMeshListItem;

        public HWShipMesh(HWJoint parent, string name, List<ShipMeshTag> tags)
        {
            for(int i = 0; i < LODMeshes.Length; i++)
                LODMeshes[i] = new List<HWShipMeshLOD>();

            Parent = parent;
            Name = name;
            Tags = tags;

            HWScene.ShipMeshes.Add(this);
            Program.main.AddShipMesh(this);
        }

        public void AddLODMesh(HWShipMeshLOD lodMesh)
        {
            Meshes.Add(lodMesh);

            LODMeshes[lodMesh.LOD].Add(lodMesh);
        }
    }

    public enum ShipMeshTag
    {
        DOSCAR = 1,
    }
}
