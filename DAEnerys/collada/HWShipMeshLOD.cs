using OpenTK;

namespace DAEnerys
{
    public class HWShipMeshLOD
    {
        public HWShipMesh ShipMesh;

        public HWMesh Mesh;
        public int LOD;

        public HWShipMeshLOD(HWShipMesh shipMesh, HWMesh mesh, int lod)
        {
            ShipMesh = shipMesh;
            Mesh = mesh;
            LOD = lod;
            Mesh.LOD = lod;

            ShipMesh.AddLODMesh(this);

            if (shipMesh.Tags.Contains(ShipMeshTag.DOSCAR))
                Mesh.DoScars = true;

            if (lod == 0)
                CalculateBoundingBox();
        }

        public void Destroy()
        {
            Mesh.Destroy();
            Mesh = null;

            ShipMesh.Meshes.Remove(this);
            switch (LOD)
            {
                case 0:
                    ShipMesh.LOD0Meshes.Remove(this);
                    break;
                case 1:
                    ShipMesh.LOD1Meshes.Remove(this);
                    break;
                case 2:
                    ShipMesh.LOD2Meshes.Remove(this);
                    break;
                case 3:
                    ShipMesh.LOD3Meshes.Remove(this);
                    break;
            }
            ShipMesh = null;
        }
        
        public void CalculateBoundingBox()
        {
            Vector3 min = Vector3.Zero;
            Vector3 max = Vector3.Zero;

            foreach(Vector3 vertex in Mesh.GetVertices())
            {
                Vector3 computedVertex = Vector3.Add(vertex * Mesh.Parent.AbsoluteScale, Mesh.Parent.AbsolutePosition);
                //Vector3 computedVertex = Vector3.Add(vertex, Mesh.Parent.AbsolutePosition);
                if (computedVertex.X < min.X)
                    min.X = computedVertex.X;
                if (computedVertex.Y < min.Y)
                    min.Y = computedVertex.Y;
                if (computedVertex.Z < min.Z)
                    min.Z = computedVertex.Z;

                if (computedVertex.X > max.X)
                    max.X = computedVertex.X;
                if (computedVertex.Y > max.Y)
                    max.Y = computedVertex.Y;
                if (computedVertex.Z > max.Z)
                    max.Z = computedVertex.Z;
            }

            if (min.X < HWScene.Min.X)
            {
                HWScene.Min.X = min.X;
                HWScene.BiggestMesh = this;
            }
            if (min.Y < HWScene.Min.Y)
            {
                HWScene.Min.Y = min.Y;
                HWScene.BiggestMesh = this;
            }
            if (min.Z < HWScene.Min.Z)
            {
                HWScene.Min.Z = min.Z;
                HWScene.BiggestMesh = this;
            }

            if (max.X > HWScene.Max.X)
            {
                HWScene.Max.X = max.X;
                HWScene.BiggestMesh = this;
            }
            if (max.Y > HWScene.Max.Y)
            {
                HWScene.Max.Y = max.Y;
                HWScene.BiggestMesh = this;
            }
            if (max.Z > HWScene.Max.Z)
            {
                HWScene.Max.Z = max.Z;
                HWScene.BiggestMesh = this;
            }
        }
    }
}
