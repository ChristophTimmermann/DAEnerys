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
        public List<HWShipMeshLOD>[] LODMeshes = new List<HWShipMeshLOD>[4];

        public object ShipMeshListItem;

        public HWShipMesh(HWJoint parent, string name, ObservableCollection<ShipMeshTag> tags)
        {
            for(int i = 0; i < LODMeshes.Length; i++)
                LODMeshes[i] = new List<HWShipMeshLOD>();

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

            LODMeshes[lodMesh.LOD].Add(lodMesh);
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
