using OpenTK;

namespace DAEnerys
{
    public abstract class HWElement
    {
        protected HWElement parent;
        public virtual HWElement Parent
        {
            get { return parent; }
            set
            {
                parent = value;
                CalculateWorldMatrix();
            }
        }

        public string Name;
        public abstract string FormattedName { get; }

        public Matrix4 WorldMatrix = Matrix4.Identity;
        public Matrix4 RelativeWorldMatrix = Matrix4.Identity;

        public Vector3 AbsolutePosition { get { return Vector3.TransformPosition(Vector3.Zero, WorldMatrix); } }
        public OpenTK.Quaternion AbsoluteRotation;
        public Vector3 AbsoluteScale { get { return WorldMatrix.ExtractScale(); } }

        public Vector3 RelativePosition { get { return Vector3.TransformPosition(Vector3.Zero, RelativeWorldMatrix); } }
        public Vector3 RelativeRotation
        {
            get
            {
                Vector3 rotation = Vector3.Zero;
                float angle = 0;
                RelativeWorldMatrix.ExtractRotation().ToAxisAngle(out rotation, out angle);
                return rotation * angle;
            }
        }

        public HWElement(string name, HWElement parent, Matrix4 transform)
        {
            Name = name;

            WorldMatrix = transform;
            RelativeWorldMatrix = transform;

            this.Parent = parent;
        }

        public virtual void Destroy()
        {
            this.Parent = null;
        }

        public virtual void CalculateWorldMatrix()
        {
            WorldMatrix = RelativeWorldMatrix;

            if (Parent != null)
                WorldMatrix *= Parent.WorldMatrix;

            AbsoluteRotation = WorldMatrix.ExtractRotation();
        }
    }
}
