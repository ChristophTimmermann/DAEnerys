using OpenTK;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DAEnerys
{
    public class HWJoint : HWElement
    {
        public static List<HWJoint> Joints = new List<HWJoint>();
        public static HWJoint Root;

        public Matrix4 AnimationMatrix = Matrix4.Identity;
        public HWAnimationChannel PositionChannel = new HWAnimationChannel();
        public HWAnimationChannel RotationChannel = new HWAnimationChannel();
        public HWAnimationChannel ScalingChannel = new HWAnimationChannel();

        public List<HWElement> Children = new List<HWElement>();
        public List<HWMesh> Meshes = new List<HWMesh>();

        public override HWElement Parent
        {
            get { return parent; }
            set
            {
                if (parent != null)
                {
                    HWJoint parentJoint = parent as HWJoint;

                    if(parentJoint != null)
                        parentJoint.Children.Remove(this);
                }
                parent = value;
                if (parent != null)
                {
                    HWJoint parentJoint = parent as HWJoint;

                    if (parentJoint != null)
                        parentJoint.Children.Add(this);
                }
                CalculateWorldMatrix();
                Renderer.InvalidateView();
                Renderer.Invalidate();
            }
        }

        public EditorJoint EditorJoint;

        public override string FormattedName
        {
            get
            {
                return "JNT[" + Name + "]";
            }
        }

        public TreeNode TreeNode;
        public object ComboItem;

        public HWJoint(string name, HWJoint parent, Matrix4 transform) : base(name, parent, transform)
        {
            Joints.Add(this);

            Program.main.AddJoint(this, parent);

            //Visualization
            EditorJoint = new EditorJoint(this);
        }

        public override void Destroy()
        {
            Program.main.RemoveJoint(this);
            Joints.Remove(this);
            EditorJoint.Destroy();
            EditorJoint = null;

            base.Destroy();
        }

        public static HWJoint GetByName(string name)
        {
            foreach(HWJoint joint in Joints)
                if (joint.Name == name)
                    return joint;

            return null;
        }

        public override void CalculateWorldMatrix()
        {
            if (AnimationMatrix == Matrix4.Identity)
                WorldMatrix = RelativeWorldMatrix;
            else
                WorldMatrix = AnimationMatrix;

            if (Parent != null)
                WorldMatrix *= Parent.WorldMatrix;

            AbsoluteRotation = WorldMatrix.ExtractRotation();
        }
    }
}
