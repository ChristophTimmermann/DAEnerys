using Assimp;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DAEnerys
{
    public class HWJoint : HWNode
    {
        public static List<HWJoint> Joints = new List<HWJoint>();

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
        public object ComboItemEngineGlowParent;
        public object ComboItemEngineShapeParent;

        public HWJoint(Node assimpNode, HWNode parent, string name) : base(assimpNode, parent)
        {
            Name = name;

            if (!IsUnderAnyRootNode())
            {
                new Problem(ProblemTypes.ERROR, "The joint \"" + Name + "\" is not under any \"ROOT_LOD[X]\" node.");
                return;
            }

            Joints.Add(this);

            HWJoint parentJoint = parent as HWJoint;
            Program.main.AddJoint(this, parentJoint);

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
