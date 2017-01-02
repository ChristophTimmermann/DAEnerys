namespace DAEnerys
{
    partial class JointTemplateWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.boxJointTemplateName = new System.Windows.Forms.TextBox();
            this.labelJointTemplateName = new System.Windows.Forms.Label();
            this.groupJointTemplateType = new System.Windows.Forms.GroupBox();
            this.radioTypeSalvagePoint = new System.Windows.Forms.RadioButton();
            this.radioTypeRepairPoint = new System.Windows.Forms.RadioButton();
            this.radioTypeCapturePoint = new System.Windows.Forms.RadioButton();
            this.radioTypeHardpoint = new System.Windows.Forms.RadioButton();
            this.radioTypeTurret = new System.Windows.Forms.RadioButton();
            this.radioTypeWeapon = new System.Windows.Forms.RadioButton();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.groupJointTemplateType.SuspendLayout();
            this.SuspendLayout();
            // 
            // boxJointTemplateName
            // 
            this.boxJointTemplateName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boxJointTemplateName.Location = new System.Drawing.Point(56, 12);
            this.boxJointTemplateName.Name = "boxJointTemplateName";
            this.boxJointTemplateName.Size = new System.Drawing.Size(253, 20);
            this.boxJointTemplateName.TabIndex = 0;
            this.boxJointTemplateName.TextChanged += new System.EventHandler(this.boxJointTemplateName_TextChanged);
            // 
            // labelJointTemplateName
            // 
            this.labelJointTemplateName.AutoSize = true;
            this.labelJointTemplateName.Location = new System.Drawing.Point(12, 15);
            this.labelJointTemplateName.Name = "labelJointTemplateName";
            this.labelJointTemplateName.Size = new System.Drawing.Size(38, 13);
            this.labelJointTemplateName.TabIndex = 1;
            this.labelJointTemplateName.Text = "Name:";
            // 
            // groupJointTemplateType
            // 
            this.groupJointTemplateType.AutoSize = true;
            this.groupJointTemplateType.Controls.Add(this.radioTypeSalvagePoint);
            this.groupJointTemplateType.Controls.Add(this.radioTypeRepairPoint);
            this.groupJointTemplateType.Controls.Add(this.radioTypeCapturePoint);
            this.groupJointTemplateType.Controls.Add(this.radioTypeHardpoint);
            this.groupJointTemplateType.Controls.Add(this.radioTypeTurret);
            this.groupJointTemplateType.Controls.Add(this.radioTypeWeapon);
            this.groupJointTemplateType.Location = new System.Drawing.Point(12, 38);
            this.groupJointTemplateType.Name = "groupJointTemplateType";
            this.groupJointTemplateType.Size = new System.Drawing.Size(297, 170);
            this.groupJointTemplateType.TabIndex = 2;
            this.groupJointTemplateType.TabStop = false;
            this.groupJointTemplateType.Text = "Type";
            // 
            // radioTypeSalvagePoint
            // 
            this.radioTypeSalvagePoint.AutoSize = true;
            this.radioTypeSalvagePoint.Location = new System.Drawing.Point(6, 134);
            this.radioTypeSalvagePoint.Name = "radioTypeSalvagePoint";
            this.radioTypeSalvagePoint.Size = new System.Drawing.Size(90, 17);
            this.radioTypeSalvagePoint.TabIndex = 5;
            this.radioTypeSalvagePoint.TabStop = true;
            this.radioTypeSalvagePoint.Text = "Salvage point";
            this.radioTypeSalvagePoint.UseVisualStyleBackColor = true;
            this.radioTypeSalvagePoint.CheckedChanged += new System.EventHandler(this.radioType_CheckedChanged);
            // 
            // radioTypeRepairPoint
            // 
            this.radioTypeRepairPoint.AutoSize = true;
            this.radioTypeRepairPoint.Location = new System.Drawing.Point(6, 111);
            this.radioTypeRepairPoint.Name = "radioTypeRepairPoint";
            this.radioTypeRepairPoint.Size = new System.Drawing.Size(82, 17);
            this.radioTypeRepairPoint.TabIndex = 4;
            this.radioTypeRepairPoint.TabStop = true;
            this.radioTypeRepairPoint.Text = "Repair point";
            this.radioTypeRepairPoint.UseVisualStyleBackColor = true;
            this.radioTypeRepairPoint.CheckedChanged += new System.EventHandler(this.radioType_CheckedChanged);
            // 
            // radioTypeCapturePoint
            // 
            this.radioTypeCapturePoint.AutoSize = true;
            this.radioTypeCapturePoint.Location = new System.Drawing.Point(6, 88);
            this.radioTypeCapturePoint.Name = "radioTypeCapturePoint";
            this.radioTypeCapturePoint.Size = new System.Drawing.Size(88, 17);
            this.radioTypeCapturePoint.TabIndex = 3;
            this.radioTypeCapturePoint.TabStop = true;
            this.radioTypeCapturePoint.Text = "Capture point";
            this.radioTypeCapturePoint.UseVisualStyleBackColor = true;
            this.radioTypeCapturePoint.CheckedChanged += new System.EventHandler(this.radioType_CheckedChanged);
            // 
            // radioTypeHardpoint
            // 
            this.radioTypeHardpoint.AutoSize = true;
            this.radioTypeHardpoint.Location = new System.Drawing.Point(6, 65);
            this.radioTypeHardpoint.Name = "radioTypeHardpoint";
            this.radioTypeHardpoint.Size = new System.Drawing.Size(71, 17);
            this.radioTypeHardpoint.TabIndex = 2;
            this.radioTypeHardpoint.TabStop = true;
            this.radioTypeHardpoint.Text = "Hardpoint";
            this.radioTypeHardpoint.UseVisualStyleBackColor = true;
            this.radioTypeHardpoint.CheckedChanged += new System.EventHandler(this.radioType_CheckedChanged);
            // 
            // radioTypeTurret
            // 
            this.radioTypeTurret.AutoSize = true;
            this.radioTypeTurret.Location = new System.Drawing.Point(6, 42);
            this.radioTypeTurret.Name = "radioTypeTurret";
            this.radioTypeTurret.Size = new System.Drawing.Size(53, 17);
            this.radioTypeTurret.TabIndex = 1;
            this.radioTypeTurret.TabStop = true;
            this.radioTypeTurret.Text = "Turret";
            this.radioTypeTurret.UseVisualStyleBackColor = true;
            this.radioTypeTurret.CheckedChanged += new System.EventHandler(this.radioType_CheckedChanged);
            // 
            // radioTypeWeapon
            // 
            this.radioTypeWeapon.AutoSize = true;
            this.radioTypeWeapon.Checked = true;
            this.radioTypeWeapon.Location = new System.Drawing.Point(6, 19);
            this.radioTypeWeapon.Name = "radioTypeWeapon";
            this.radioTypeWeapon.Size = new System.Drawing.Size(66, 17);
            this.radioTypeWeapon.TabIndex = 0;
            this.radioTypeWeapon.TabStop = true;
            this.radioTypeWeapon.Text = "Weapon";
            this.radioTypeWeapon.UseVisualStyleBackColor = true;
            this.radioTypeWeapon.CheckedChanged += new System.EventHandler(this.radioType_CheckedChanged);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(220, 214);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(89, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdd.Enabled = false;
            this.buttonAdd.Location = new System.Drawing.Point(125, 214);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(89, 23);
            this.buttonAdd.TabIndex = 4;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // JointTemplateWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(321, 246);
            this.ControlBox = false;
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.groupJointTemplateType);
            this.Controls.Add(this.labelJointTemplateName);
            this.Controls.Add(this.boxJointTemplateName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "JointTemplateWindow";
            this.Text = "Add joint template...";
            this.groupJointTemplateType.ResumeLayout(false);
            this.groupJointTemplateType.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox boxJointTemplateName;
        private System.Windows.Forms.Label labelJointTemplateName;
        private System.Windows.Forms.GroupBox groupJointTemplateType;
        private System.Windows.Forms.RadioButton radioTypeWeapon;
        private System.Windows.Forms.RadioButton radioTypeSalvagePoint;
        private System.Windows.Forms.RadioButton radioTypeRepairPoint;
        private System.Windows.Forms.RadioButton radioTypeCapturePoint;
        private System.Windows.Forms.RadioButton radioTypeHardpoint;
        private System.Windows.Forms.RadioButton radioTypeTurret;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonAdd;
    }
}