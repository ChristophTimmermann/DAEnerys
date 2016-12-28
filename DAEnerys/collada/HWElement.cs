using OpenTK;
using System.Collections.Generic;

namespace DAEnerys
{
    public abstract class HWElement : Element
    {
        public static List<HWElement> HWElements = new List<HWElement>();

        public string Name;
        public abstract string FormattedName { get; }

        public HWElement(string name, HWElement parent, Matrix4 transform) : base(parent, transform)
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

            LocalWorldMatrix = Matrix4.CreateFromQuaternion(localRotation);
            LocalWorldMatrix *= Matrix4.CreateScale(localScale);
            LocalWorldMatrix *= Matrix4.CreateTranslation(localPosition);

            GlobalWorldMatrix = LocalWorldMatrix;

            if (Parent != null)
            {
                HWJoint joint = Parent as HWJoint;

                if (joint == null)
                    GlobalWorldMatrix *= Parent.GlobalWorldMatrix;
                else
                    if(joint.AnimationMatrix != Matrix4.Identity)
                        GlobalWorldMatrix *= joint.AnimationMatrix;
                    else
                        GlobalWorldMatrix *= Parent.GlobalWorldMatrix;
            }

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
