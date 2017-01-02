using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAEnerys
{
    public partial class JointTemplateWindow : Form
    {
        private JointType type = JointType.WEAPON;
        public JointTemplateWindow()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.main.jointsTree.Focus();
        }

        private void radioType_CheckedChanged(object sender, EventArgs e)
        {
            if (radioTypeWeapon.Checked)
                type = JointType.WEAPON;
            else if (radioTypeTurret.Checked)
                type = JointType.TURRET;
            else if (radioTypeHardpoint.Checked)
                type = JointType.HARDPOINT;
            else if (radioTypeCapturePoint.Checked)
                type = JointType.CAPTUREPOINT;
            else if (radioTypeRepairPoint.Checked)
                type = JointType.REPAIRPOINT;
            else if (radioTypeSalvagePoint.Checked)
                type = JointType.SALVAGEPOINT;

            if (type == JointType.WEAPON || type == JointType.TURRET || type == JointType.HARDPOINT)
                boxJointTemplateName.Enabled = true;
            else
                boxJointTemplateName.Enabled = false;

            CheckForEmptyName();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            HWJoint parent = Program.main.SelectedJoint;
            if (Program.main.SelectedJoint == null)
                parent = HWJoint.Root;

            string name = boxJointTemplateName.Text;
            string prefix = string.Empty;
            if (type == JointType.CAPTUREPOINT)
                prefix = "CapturePoint";
            else if (type == JointType.REPAIRPOINT)
                prefix = "RepairPoint";
            else if (type == JointType.SALVAGEPOINT)
                prefix = "SalvagePoint";

            if (prefix != string.Empty)
            {
                int indexOffset = 1;
                name = prefix + indexOffset;
                while (HWJoint.GetByName(name) != null)
                {
                    indexOffset++;
                    name = prefix + indexOffset;
                }
            }

            bool success = Program.main.AddJointTemplate(type, name, parent);

            if (!success)
            {
                MessageBox.Show("One or more joint names that will be added already exist. Please choose another name.", "Error while adding joint template", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.Hide();
            Program.main.jointsTree.Focus();
        }

        private void CheckForEmptyName()
        {
            if (type == JointType.WEAPON || type == JointType.TURRET || type == JointType.HARDPOINT)
                if (boxJointTemplateName.Text.Length == 0)
                    buttonAdd.Enabled = false;
                else
                    buttonAdd.Enabled = true;
            else
                buttonAdd.Enabled = true;
        }

        private void boxJointTemplateName_TextChanged(object sender, EventArgs e)
        {
            CheckForEmptyName();
        }
    }
}
