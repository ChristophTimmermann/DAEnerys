using OpenTK;
using System.Collections.Generic;

namespace DAEnerys
{
    public abstract class HWElement : Element
    {
        public static List<HWElement> HWElements = new List<HWElement>();

        public virtual string Name { get; set; }
        public abstract string FormattedName { get; }

        public HWElement(string name, HWElement parent) : this(name, parent, Vector3.Zero, Vector3.Zero, Vector3.One)
        {

        }
        public HWElement(string name, HWElement parent, Vector3 pos, Vector3 rot, Vector3 scale) : base(parent, pos, rot, scale)
        {
            Name = name;

            HWElements.Add(this);
        }

        public override void Destroy()
        {
            base.Destroy();

            HWElements.Remove(this);
        }

        public override void CalculateWorldMatrix()
        {
            invalid = false;

            LocalWorldMatrix = Matrix4.CreateRotationX(LocalRotation.X) * Matrix4.CreateRotationY(LocalRotation.Y) * Matrix4.CreateRotationZ(LocalRotation.Z);
            LocalWorldMatrix *= Matrix4.CreateScale(localScale);
            LocalWorldMatrix *= Matrix4.CreateTranslation(localPosition);

            GlobalWorldMatrix = LocalWorldMatrix;

            if (Parent != null)
                GlobalWorldMatrix *= Parent.GlobalWorldMatrix;

            GlobalPosition = GlobalWorldMatrix.ExtractTranslation();
            GlobalRotation = GlobalWorldMatrix.ExtractRotation();
            GlobalScale = GlobalWorldMatrix.ExtractScale();

            foreach (Element child in Children)
                child.CalculateWorldMatrix();

            Renderer.InvalidateView();
            Renderer.Invalidate();
        }
    }
}
