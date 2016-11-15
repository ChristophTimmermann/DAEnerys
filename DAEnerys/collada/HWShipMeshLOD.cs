using OpenTK;
using System;

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
            ShipMesh.LODMeshes[LOD].Remove(this);
            ShipMesh = null;
        }
        
        public void CalculateBoundingBox()
        {
            Vector3 min = new Vector3(float.MaxValue);
            Vector3 max = new Vector3(-float.MaxValue);
            Mesh.CalculateModelMatrix();
            foreach (Vector3 vertex in Mesh.GetVertices())
            {
                Vector3 computedVertex = (Matrix4.CreateTranslation(vertex) * Mesh.ModelMatrix).ExtractTranslation();
                //Vector3 computedVertex = Vector3.Add(vertex, Mesh.Parent.AbsolutePosition);
                min.X = Math.Min(min.X, computedVertex.X);
                min.Y = Math.Min(min.Y, computedVertex.Y);
                min.Z = Math.Min(min.Z, computedVertex.Z);

                max.X = Math.Max(max.X, computedVertex.X);
                max.Y = Math.Max(max.Y, computedVertex.Y);
                max.Z = Math.Max(max.Z, computedVertex.Z);
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
