using System;
using System.Collections.Generic;

namespace DAEnerys
{
    public class HWShipMesh
    {
        public static List<HWShipMesh> ShipMeshes = new List<HWShipMesh>();

        private HWJoint parent;
        public HWJoint Parent
        {
            get { return parent; }
            set
            {
                parent = value;
                foreach (HWShipMeshLOD mesh in Meshes)
                {
                    mesh.Parent = value;
                }
            }
        }

        private string name;
        public string Name
        {
            get { return name; } set { name = value; foreach (HWShipMeshLOD mesh in Meshes) mesh.Name = value; }
        }

        public List<ShipMeshTag> Tags = new List<ShipMeshTag>();

        public List<HWShipMeshLOD> Meshes = new List<HWShipMeshLOD>();
        public List<HWShipMeshLOD>[] LODMeshes = new List<HWShipMeshLOD>[4];

        public HWShipMesh(HWJoint parent, string name, List<ShipMeshTag> tags)
        {
            for(int i = 0; i < LODMeshes.Length; i++)
                LODMeshes[i] = new List<HWShipMeshLOD>();

            Parent = parent;
            Name = name;
            Tags = tags;

            ShipMeshes.Add(this);
            Program.main.AddShipMesh(this);
        }

        public void AddLODMesh(HWShipMeshLOD lodMesh)
        {
            Meshes.Add(lodMesh);

            LODMeshes[lodMesh.LOD].Add(lodMesh);
        }

        public static HWShipMesh GetByName(string name)
        {
            foreach (HWShipMesh shipMesh in ShipMeshes)
                if (shipMesh.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                    return shipMesh;

            return null;
        }

        public void Destroy()
        {
            ShipMeshes.Remove(this);
            Program.main.RemoveShipMesh(this);
            HWShipMeshLOD[] lodMeshes = Meshes.ToArray();
            for (int i = 0; i < lodMeshes.Length; i++)
                lodMeshes[i].Destroy();
            Parent = null;
        }
    }

    public enum ShipMeshTag
    {
        DoScar = 1,
    }
}
