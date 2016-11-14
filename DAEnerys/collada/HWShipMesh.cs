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
                        mesh.Mesh.Parent.Parent = value.Node;
                        mesh.CalculateBoundingBox();
                    }
                else
                    foreach (HWShipMeshLOD mesh in Meshes)
                    {
                        mesh.Mesh.Parent.Parent = HWNode.Roots[mesh.LOD];
                        mesh.CalculateBoundingBox();
                    }
            }
        }
        public string Name;

        public ObservableCollection<ShipMeshTag> Tags = new ObservableCollection<ShipMeshTag>();

        public List<HWShipMeshLOD> Meshes = new List<HWShipMeshLOD>();
        public List<HWShipMeshLOD> LOD0Meshes = new List<HWShipMeshLOD>();
        public List<HWShipMeshLOD> LOD1Meshes = new List<HWShipMeshLOD>();
        public List<HWShipMeshLOD> LOD2Meshes = new List<HWShipMeshLOD>();
        public List<HWShipMeshLOD> LOD3Meshes = new List<HWShipMeshLOD>();

        public object ShipMeshListItem;

        public HWShipMesh(HWJoint parent, string name, ObservableCollection<ShipMeshTag> tags)
        {
            Parent = parent;
            Name = name;
            Tags = tags;
            Tags.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(TagsChanged);

            HWScene.ShipMeshes.Add(this);
            Program.main.AddShipMesh(this);
        }

        public void AddLODMesh(HWShipMeshLOD lodMesh)
        {
            Meshes.Add(lodMesh);

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
                case 3:
                    LOD3Meshes.Add(lodMesh);
                    break;
            }
        }

        private void TagsChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            bool doScar = false;
            if (Tags.Contains(ShipMeshTag.DOSCAR))
                doScar = true;

            foreach (HWShipMeshLOD mesh in Meshes)
                mesh.Mesh.DoScars = doScar;
        }
    }

    public enum ShipMeshTag
    {
        DOSCAR = 1,
    }
}
