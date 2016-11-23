using Assimp;
using OpenTK;
using System;

namespace DAEnerys
{
    public class HWShipMeshLOD : HWMesh
    {
        public HWShipMesh ShipMesh;
        public int LOD;

        public override string FormattedName
        {
            get
            {
                string lod = "_LOD[" + LOD + "]";

                string tags = "";
                if (ShipMesh.Tags.Contains(ShipMeshTag.DOSCAR))
                    tags = "_TAGS[DoScar]";

                return "MULT[" + Name + "]" + lod + tags;
            }
        }

        public HWShipMeshLOD(Mesh assimpMesh, HWShipMesh shipMesh, int lod) : base(assimpMesh)
        {
            ShipMesh = shipMesh;
            LOD = lod;
            Name = shipMesh.Name;
            Parent = shipMesh.Parent;

            if (HWScene.Materials[assimpMesh.MaterialIndex] != null)
                if (HWScene.Materials[assimpMesh.MaterialIndex].Valid)
                    //if (assimpMesh.TextureCoordinateChannelCount > 0) HODOR assigns them anyways
                    Material = HWScene.Materials[assimpMesh.MaterialIndex];

            ShipMesh.AddLODMesh(this);

            if (lod == 0)
                CalculateBoundingBox();
        }

        public override void Destroy()
        {
            Parent.Destroy();
            ShipMesh.Meshes.Remove(this);
            ShipMesh.LODMeshes[LOD].Remove(this);
            ShipMesh = null;

            base.Destroy();
        }
        
        public void CalculateBoundingBox()
        {
            Vector3 min = new Vector3(float.MaxValue);
            Vector3 max = new Vector3(-float.MaxValue);
            CalculateModelMatrix();
            foreach (Vertex vertex in Vertices)
            {
                Vector3 computedVertex = (Matrix4.CreateTranslation(vertex.Position) * ModelMatrix).ExtractTranslation();
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
