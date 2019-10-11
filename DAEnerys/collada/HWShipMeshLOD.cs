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

        public override void CalculateBoundingBox()
        {
            base.CalculateBoundingBox();

            Vector3 transformedMin = Vector3.TransformPosition(BoundsMin, GlobalWorldMatrix);
            Vector3 transformedMax = Vector3.TransformPosition(BoundsMax, GlobalWorldMatrix);

            HWScene.Min.X = Math.Min(HWScene.Min.X, transformedMin.X);
            HWScene.Min.Y = Math.Min(HWScene.Min.Y, transformedMin.Y);
            HWScene.Min.Z = Math.Min(HWScene.Min.Z, transformedMin.Z);

            HWScene.Max.X = Math.Max(HWScene.Max.X, transformedMax.X);
            HWScene.Max.Y = Math.Max(HWScene.Max.Y, transformedMax.Y);
            HWScene.Max.Z = Math.Max(HWScene.Max.Z, transformedMax.Z);

            Renderer.MinClipDistance = HWScene.Min.Z * 1.2f;
            Renderer.MaxClipDistance = HWScene.Max.Z * 1.2f;
            Renderer.ClipDistance = Renderer.MaxClipDistance;
        }
    }
}
