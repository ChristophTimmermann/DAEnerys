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

            this.PreviewCube.SetData(GenerateBoundingCube());

            float maxDistance = float.MinValue;
            maxDistance = Math.Max(maxDistance, Math.Abs(BoundsMin.X));
            maxDistance = Math.Max(maxDistance, Math.Abs(BoundsMin.Y));
            maxDistance = Math.Max(maxDistance, Math.Abs(BoundsMin.Z));

            maxDistance = Math.Max(maxDistance, Math.Abs(BoundsMax.X));
            maxDistance = Math.Max(maxDistance, Math.Abs(BoundsMax.Y));
            maxDistance = Math.Max(maxDistance, Math.Abs(BoundsMax.Z));

            this.PreviewSphere.LocalScale = new Vector3(maxDistance);

            this.PreviewSphere.Wireframe = true;
            this.PreviewCube.Wireframe = true;

            CollisionMeshes.Add(this);
            Program.main.AddCollisionMesh(this);
        }

        public override void CalculateBoundingBox()
        {
            base.CalculateBoundingBox();

            if(this.PreviewCube != null)
            {
                this.PreviewCube.SetData(GenerateBoundingCube());
            }
        }

        public override void Destroy()
        {
            Program.main.RemoveCollisionMesh(this);
            CollisionMeshes.Remove(this);
            Parent = null;

            this.PreviewCube.Destroy();
            this.PreviewSphere.Destroy();

            base.Destroy();
        }
    }
}
