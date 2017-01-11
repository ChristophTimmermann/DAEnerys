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
                if (ShipMesh.Tags.Contains(ShipMeshTag.DoScar))
                    tags = "_TAGS[DoScar]";

                return "MULT[" + Name + "]" + lod + tags;
            }
        }

        public HWShipMeshLOD(MeshData data, Vector3 pos, Vector3 rot, Vector3 scale, HWMaterial material, HWShipMesh shipMesh, int lod) : base(data, shipMesh.Parent, pos, rot, scale, material)
        {
            ShipMesh = shipMesh;
            LOD = lod;
            Name = shipMesh.Name;

            ShipMesh.AddLODMesh(this);

            if (lod == 0)
                CalculateBoundingBox();
        }

        public override void Destroy()
        {
            ShipMesh.Meshes.Remove(this);
            ShipMesh.LODMeshes[LOD].Remove(this);
            ShipMesh = null;

            base.Destroy();
        }

        public override void CalculateWorldMatrix()
        {
            base.CalculateWorldMatrix();

            CalculateBoundingBox();
        }

        public void CalculateBoundingBox()
        {
            Vector3 min = new Vector3(float.MaxValue);
            Vector3 max = new Vector3(-float.MaxValue);

            foreach (Vector3 vertex in Vertices)
            {
                Vector3 computedVertex = Vector3.TransformPosition(vertex, GlobalWorldMatrix);
                //Vector3 computedVertex = Vector3.Add(vertex, Parent.AbsolutePosition);
                min.X = Math.Min(min.X, computedVertex.X);
                min.Y = Math.Min(min.Y, computedVertex.Y);
                min.Z = Math.Min(min.Z, computedVertex.Z);

                max.X = Math.Max(max.X, computedVertex.X);
                max.Y = Math.Max(max.Y, computedVertex.Y);
                max.Z = Math.Max(max.Z, computedVertex.Z);
            }

            HWScene.Min.X = Math.Min(HWScene.Min.X, min.X);
            HWScene.Min.Y = Math.Min(HWScene.Min.Y, min.Y);
            HWScene.Min.Z = Math.Min(HWScene.Min.Z, min.Z);

            HWScene.Max.X = Math.Max(HWScene.Max.X, max.X);
            HWScene.Max.Y = Math.Max(HWScene.Max.Y, max.Y);
            HWScene.Max.Z = Math.Max(HWScene.Max.Z, max.Z);

            Renderer.MinClipDistance = HWScene.Min.Z * 1.2f;
            Renderer.MaxClipDistance = HWScene.Max.Z * 1.2f;
            Renderer.ClipDistance = Renderer.MaxClipDistance;
        }
    }
}
