using OpenTK;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DAEnerys
{
    public class HWJoint : HWElement
    {
        public static List<HWJoint> Joints = new List<HWJoint>();
        public static HWJoint Root;

        public List<HWElement> Children = new List<HWElement>();
        public List<HWMesh> Meshes = new List<HWMesh>();

        public override HWJoint Parent
        {
            get { return parent; }
            set
            {
                if (parent != null)
                    parent.Children.Remove(this);
                parent = value;
                if (parent != null)
                    parent.Children.Add(this);
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
        public object ComboItemShipMeshParent;
        public object ComboItemCollisionMeshParent;
        public object ComboItemEngineGlowParent;
        public object ComboItemEngineShapeParent;

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
    }
}
