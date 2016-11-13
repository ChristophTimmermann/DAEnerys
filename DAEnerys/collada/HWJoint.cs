using System.Collections.Generic;
using System.Windows.Forms;

namespace DAEnerys
{
    public class HWJoint
    {
        public static List<HWJoint> Joints = new List<HWJoint>();

        public EditorJoint EditorJoint;
        public HWNode Node;

        public string Name;
        public HWJoint Parent;
        public List<HWJoint> Children = new List<HWJoint>();

        public TreeNode TreeNode;
        public object ComboItemShipMeshParent;
        public object ComboItemEngineGlowParent;
        public object ComboItemEngineShapeParent;

        public HWJoint(HWNode node, HWJoint parent, string name)
        {
            Parent = parent;
            Node = node;
            Name = name;

            Joints.Add(this);
            Program.main.AddJoint(this, parent);

            //Visualization
            EditorJoint = new EditorJoint(this);
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
