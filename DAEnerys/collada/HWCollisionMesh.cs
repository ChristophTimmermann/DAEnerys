using OpenTK;
using System;
using System.Collections.Generic;

namespace DAEnerys
{
    public class HWCollisionMesh : HWMesh
    {
        public static List<HWCollisionMesh> CollisionMeshes = new List<HWCollisionMesh>();

        public override bool Visible { get { return base.Visible; } set { base.Visible = value; } }

        public EditorCube PreviewCube;
        public EditorIcosphere PreviewSphere;

        public override string FormattedName
        {
            get
            {
                return "COL[" + Parent.Name + "]";
            }
        }

        public int ItemIndex;

        public HWCollisionMesh(MeshData data, Vector3 pos, Vector3 rot, Vector3 scale, HWJoint parent) : base(data, parent, pos, rot, scale, HWMaterial.DefaultMaterial)
        {
            this.Parent = parent;

            this.PreviewCube = new EditorCube(this, new Vector3(1, 0, 0));
            this.PreviewSphere = new EditorIcosphere(this, new Vector3(1, 0, 0));

            CollisionMeshes.Add(this);
            Program.main.AddCollisionMesh(this);

            this.PreviewCube.SetData(MeshData.GenerateBoundingCube(this.BoundsMin, this.BoundsMax));

            float maxDistance = float.MinValue;
            maxDistance = Math.Max(maxDistance, Math.Abs(BoundsMin.X));
            maxDistance = Math.Max(maxDistance, Math.Abs(BoundsMin.Y));
            maxDistance = Math.Max(maxDistance, Math.Abs(BoundsMin.Z));

            maxDistance = Math.Max(maxDistance, Math.Abs(BoundsMax.X));
            maxDistance = Math.Max(maxDistance, Math.Abs(BoundsMax.Y));
            maxDistance = Math.Max(maxDistance, Math.Abs(BoundsMax.Z));

            this.PreviewSphere.LocalScale = new Vector3(maxDistance);
            this.PreviewSphere.LocalPosition = (this.BoundsMax + this.BoundsMin) / 2;

            this.PreviewSphere.Wireframe = true;
            this.PreviewCube.Wireframe = true;

            TargetBoxManager.InvalidateTargetBoxes();
        }

        public override void CalculateBoundingBox()
        {
            base.CalculateBoundingBox();

            if(this.PreviewCube != null)
            {
                this.PreviewCube.SetData(MeshData.GenerateBoundingCube(this.BoundsMin, this.BoundsMax));
            }
        }

        public override void Destroy()
        {
            Program.main.RemoveCollisionMesh(this);
            CollisionMeshes.Remove(this);
            Parent = null;

            this.PreviewCube.Destroy();
            this.PreviewSphere.Destroy();

            TargetBoxManager.InvalidateTargetBoxes();

            base.Destroy();
        }
    }
}
