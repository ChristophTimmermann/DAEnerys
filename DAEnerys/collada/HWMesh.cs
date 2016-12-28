using OpenTK;
using System.Collections.Generic;

namespace DAEnerys
{
    public abstract class HWMesh : GenericMesh
    {
        public static List<HWMesh> Meshes = new List<HWMesh>();

        public Matrix4 Transform;

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

                CalculateWorldMatrix();
                Renderer.InvalidateView(); Renderer.Invalidate();
            }
        }

        public string Name;
        public abstract string FormattedName { get; }

        private HWMaterial material = new HWMaterial();
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

        public HWMesh(MeshData data, HWJoint parent, Matrix4 transform, HWMaterial material) : base(parent, transform)
        {
            Parent = parent;
            Transform = transform.ClearTranslation();
            //Transform *= Matrix4.CreateFromQuaternion(Transform.ExtractRotation());

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
            base.CalculateWorldMatrix();

            ModelViewProjectionMatrix = GlobalWorldMatrix * Renderer.ViewProjection;
        }
    }
}
