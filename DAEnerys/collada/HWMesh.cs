using OpenTK;
using System.Collections.Generic;

namespace DAEnerys
{
    public abstract class HWMesh : GenericMesh
    {
        public static List<HWMesh> Meshes = new List<HWMesh>();

        new private HWJoint parent;
        new public virtual HWJoint Parent
        {
            get { return parent; }
            set
            {
                if (parent != null)
                    parent.Meshes.Remove(this);
                parent = value;
                if (parent != null)
                    parent.Meshes.Add(this);

                Invalidate();

                Renderer.InvalidateView(); Renderer.Invalidate();
            }
        }

        public string Name;
        public abstract string FormattedName { get; }

        private HWMaterial material = HWMaterial.DefaultMaterial;
        new public HWMaterial Material { get { return material; } set { material = value; Renderer.Invalidate(); } }

        private bool MinMaxSet = false;
        private Vector3 _max, _min;
        public Vector3 Max
        {
            get
            {
                if (!MinMaxSet)
                {
                    _max = -float.MaxValue * Vector3.One;
                    _min = float.MaxValue * Vector3.One;
                    foreach (Vector3 vertex in Vertices)
                    {
                        _max = Vector3.Max(_max, vertex);
                        _min = Vector3.Min(_min, vertex);
                    }
                    MinMaxSet = true;
                }
                return _max;
            }
            private set { }
        }
        public Vector3 Min
        {
            get
            {
                if (!MinMaxSet)
                {
                    _max = -float.MaxValue * Vector3.One;
                    _min = float.MaxValue * Vector3.One;
                    foreach (Vector3 vertex in Vertices)
                    {
                        _max = Vector3.Max(_max, vertex);
                        _min = Vector3.Min(_min, vertex);
                    }
                    MinMaxSet = true;
                }
                return _min;
            }
            private set { }
        }

        public HWMesh(MeshData data, HWJoint parent, Vector3 pos, Vector3 rot, Vector3 scale, HWMaterial material) : base(parent, pos, rot, scale)
        {
            Parent = parent;

            Meshes.Add(this);

            SetData(data);

            Material = material;
        }

        public override void Destroy()
        {
            base.Destroy();

            HWMesh.Meshes.Remove(this);
            Renderer.InvalidateMeshData();
            Renderer.Invalidate();
        }

        public override void CalculateWorldMatrix()
        {
            invalid = false;

            LocalWorldMatrix = Matrix4.CreateRotationX(LocalRotation.X) * Matrix4.CreateRotationY(LocalRotation.Y) * Matrix4.CreateRotationZ(LocalRotation.Z);
            LocalWorldMatrix *= Matrix4.CreateScale(localScale);
            LocalWorldMatrix *= Matrix4.CreateTranslation(localPosition);

            GlobalWorldMatrix = LocalWorldMatrix;

            if (Parent != null)
            {
                HWJoint joint = Parent as HWJoint;

                if (joint == null)
                    GlobalWorldMatrix *= Parent.GlobalWorldMatrix;
                else
                    if (joint.AnimationMatrix != Matrix4.Identity)
                    GlobalWorldMatrix *= joint.AnimationMatrix;
                else
                    GlobalWorldMatrix *= Parent.GlobalWorldMatrix;
            }

            GlobalPosition = GlobalWorldMatrix.ExtractTranslation();
            GlobalRotation = GlobalWorldMatrix.ExtractRotation();
            GlobalScale = GlobalWorldMatrix.ExtractScale();

            foreach (Element child in Children)
                child.CalculateWorldMatrix();

            ModelViewProjectionMatrix = GlobalWorldMatrix * Renderer.ViewProjection;

            Renderer.InvalidateView();
            Renderer.Invalidate();
        }
    }
}
