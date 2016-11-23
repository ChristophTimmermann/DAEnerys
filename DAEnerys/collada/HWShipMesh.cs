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
                        mesh.Parent.Parent = value;
                        mesh.CalculateBoundingBox();
                    }
                else
                    foreach (HWShipMeshLOD mesh in Meshes)
                    {
                        mesh.Parent.Parent = HWNode.RootLODs[mesh.LOD];
                        mesh.CalculateBoundingBox();
                    }
            }
        }
        public string Name;

        public List<ShipMeshTag> Tags = new List<ShipMeshTag>();

        public List<HWShipMeshLOD> Meshes = new List<HWShipMeshLOD>();
        public List<HWShipMeshLOD>[] LODMeshes = new List<HWShipMeshLOD>[4];

        public object ListItem;

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

        public void Destroy()
        {
            HWScene.ShipMeshes.Remove(this);
            Program.main.RemoveShipMesh(this);
            HWShipMeshLOD[] lodMeshes = Meshes.ToArray();
            for (int i = 0; i < lodMeshes.Length; i++)
                lodMeshes[i].Destroy();
            Parent = null;
        }
    }

    public enum ShipMeshTag
    {
        DOSCAR = 1,
    }
}
