using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAEnerys
{
    public class HWShipMesh
    {
        public HWJoint Parent;
        public string Name;
        public List<ShipMeshTag> Tags = new List<ShipMeshTag>();

        public List<HWShipMeshLOD> LOD0Meshes = new List<HWShipMeshLOD>();
        public List<HWShipMeshLOD> LOD1Meshes = new List<HWShipMeshLOD>();
        public List<HWShipMeshLOD> LOD2Meshes = new List<HWShipMeshLOD>();

        public object ShipMeshListItem;

        public HWShipMesh(HWJoint parent, string name, List<ShipMeshTag> tags)
        {
            Parent = parent;
            Name = name;
            Tags = tags;

            HWScene.ShipMeshes.Add(this);
            Program.main.AddShipMesh(this);
        }

        public void AddLODMesh(HWShipMeshLOD lodMesh)
        {
            switch(lodMesh.LOD)
            {
                case 0:
                    LOD0Meshes.Add(lodMesh);
                    break;
                case 1:
                    LOD1Meshes.Add(lodMesh);
                    break;
                case 2:
                    LOD2Meshes.Add(lodMesh);
                    break;
            }
        }
    }

    public enum ShipMeshTag
    {
        DOSCAR = 1,
    }
}
