using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeworldDAEEditor
{
    public class HWJoint
    {
        public EditorJoint EditorJoint;
        public HWNode Node;

        public string Name;
        public HWJoint Parent;
        public List<HWJoint> Children = new List<HWJoint>();

        public TreeNode TreeNode;
        public object ComboItemShipMeshParent;
        public object ComboItemGoblinParent;

        public HWJoint(HWNode node, HWJoint parent, string name)
        {
            Parent = parent;
            Node = node;
            Name = name;

            HWScene.Joints.Add(this);
            Program.main.AddJoint(this, parent);

            //Visualization
            EditorJoint = new EditorJoint(this);
        }
    }
}
