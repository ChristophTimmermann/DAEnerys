using OpenTK;
using System.Collections.Generic;

namespace DAEnerys
{
    public abstract class Element
    {
        public static List<Element> Elements = new List<Element>();

        protected Element parent;
        public virtual Element Parent
        {
            get { return parent; }
            set
            {
                if (parent != null)
                    parent.Children.Remove(this);

                parent = value;

                if (value != null)
                    value.Children.Add(this);

                Invalidate();

                Renderer.InvalidateView();
                Renderer.Invalidate();
            }
        }

        public Matrix4 GlobalWorldMatrix = Matrix4.Identity;
        public Matrix4 LocalWorldMatrix = Matrix4.Identity;

        public Vector3 GlobalPosition { get; protected set; } = Vector3.Zero;
        public Quaternion GlobalRotation { get; protected set; } = Quaternion.Identity;
        public Vector3 GlobalScale { get; protected set; } = Vector3.One;

        protected Vector3 localPosition = Vector3.Zero;
        public virtual Vector3 LocalPosition
        {
            get { return localPosition; }
            set
            {
                localPosition = value;
                Invalidate();
            }
        }
        protected Quaternion localRotation = Quaternion.Identity;
        public Quaternion LocalRotation
        {
            get
            {
                return localRotation;
            }
            set
            {
                localRotation = value;
                Invalidate();
            }
        }
        protected Vector3 localScale = Vector3.One;
        public Vector3 LocalScale { get { return localScale; } set { localScale = value; Invalidate(); } }

        protected bool invalid;

        public List<Element> Children = new List<Element>();

        public Element(Element parent, Matrix4 transform)
        {
            LocalPosition = transform.ExtractTranslation();
            LocalRotation = transform.ExtractRotation();
            LocalScale = transform.ExtractScale();

            this.Parent = parent;

            CalculateWorldMatrix();

            Elements.Add(this);
        }

        public virtual void Destroy()
        {
            Element[] children = Children.ToArray();
            if (this.Parent != null)
            {
                foreach (Element child in children)
                    child.Parent = this.Parent;
            }
            else
            {
                foreach (Element child in children)
                    child.Parent = null;
            }

            this.Parent = null;

            Children.Clear();

            Elements.Remove(this);
        }

        public void Invalidate()
        {
            invalid = true;

            foreach (Element child in Children)
                child.Invalidate();
        }

        public static void UpdateInvalids()
        {
            foreach (Element element in Elements)
            {
                if (element.invalid)
                    if (element.Parent != null)
                    {
                        if (element.Parent.invalid == false)
                            element.CalculateWorldMatrix();
                    }
                    else
                        element.CalculateWorldMatrix();
            }
        }

        public virtual void CalculateWorldMatrix()
        {
            invalid = false;

            LocalWorldMatrix = Matrix4.CreateFromQuaternion(localRotation);
            LocalWorldMatrix *= Matrix4.CreateScale(localScale);
            LocalWorldMatrix *= Matrix4.CreateTranslation(localPosition);

            GlobalWorldMatrix = LocalWorldMatrix;

            if(this.Parent != null)
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
