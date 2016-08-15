namespace DAEnerys
{
    partial class ShaderSettings
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
            Program.ShaderSettings = null;
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.addDataPathDialog = new System.Windows.Forms.OpenFileDialog();
            this.labelPaintCurve = new System.Windows.Forms.Label();
            this.numPaintCurve = new System.Windows.Forms.NumericUpDown();
            this.gbxPaintStyle = new System.Windows.Forms.GroupBox();
            this.chkHACKPain = new System.Windows.Forms.CheckBox();
            this.numPaintOffset = new System.Windows.Forms.NumericUpDown();
            this.labelPaintOffset = new System.Windows.Forms.Label();
            this.numPaintScale = new System.Windows.Forms.NumericUpDown();
            this.labelPaintScale = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numConfigOption = new System.Windows.Forms.NumericUpDown();
            this.btnReloadShaders = new System.Windows.Forms.Button();
            this.cbxConfigOptions = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.numSimDelta = new System.Windows.Forms.NumericUpDown();
            this.lblSimDelta = new System.Windows.Forms.Label();
            this.numSimTime = new System.Windows.Forms.NumericUpDown();
            this.lblSimTime = new System.Windows.Forms.Label();
            this.numExecDelta = new System.Windows.Forms.NumericUpDown();
            this.lblExecDelta = new System.Windows.Forms.Label();
            this.numExecTime = new System.Windows.Forms.NumericUpDown();
            this.lblExecTime = new System.Windows.Forms.Label();
            this.gbxSOBParams = new System.Windows.Forms.GroupBox();
            this.numSOBClip = new System.Windows.Forms.NumericUpDown();
            this.lblSOBClip = new System.Windows.Forms.Label();
            this.numSOBCloak = new System.Windows.Forms.NumericUpDown();
            this.lblSOBCloak = new System.Windows.Forms.Label();
            this.numSOBAlpha = new System.Windows.Forms.NumericUpDown();
            this.lblSOBAlpha = new System.Windows.Forms.Label();
            this.numClipDist = new System.Windows.Forms.NumericUpDown();
            this.lblClipDist = new System.Windows.Forms.Label();
            this.btnEnterHyperspace = new System.Windows.Forms.Button();
            this.btnExitHyperspace = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numPaintCurve)).BeginInit();
            this.gbxPaintStyle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPaintOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPaintScale)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numConfigOption)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSimDelta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSimTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numExecDelta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numExecTime)).BeginInit();
            this.gbxSOBParams.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSOBClip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSOBCloak)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSOBAlpha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numClipDist)).BeginInit();
            this.SuspendLayout();
            // 
            // colorDialog
            // 
            this.colorDialog.AnyColor = true;
            this.colorDialog.Color = System.Drawing.Color.Gray;
            this.colorDialog.SolidColorOnly = true;
            // 
            // addDataPathDialog
            // 
            this.addDataPathDialog.FileName = "keeper.txt";
            this.addDataPathDialog.Filter = "Data roots|keeper.txt";
            this.addDataPathDialog.Title = "Select keeper.txt in data root folder";
            // 
            // labelPaintCurve
            // 
            this.labelPaintCurve.AutoSize = true;
            this.labelPaintCurve.Location = new System.Drawing.Point(15, 26);
            this.labelPaintCurve.Name = "labelPaintCurve";
            this.labelPaintCurve.Size = new System.Drawing.Size(62, 13);
            this.labelPaintCurve.TabIndex = 20;
            this.labelPaintCurve.Text = "Paint Curve";
            // 
            // numPaintCurve
            // 
            this.numPaintCurve.DecimalPlaces = 2;
            this.numPaintCurve.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numPaintCurve.Location = new System.Drawing.Point(152, 24);
            this.numPaintCurve.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numPaintCurve.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            -2147483648});
            this.numPaintCurve.Name = "numPaintCurve";
            this.numPaintCurve.Size = new System.Drawing.Size(72, 20);
            this.numPaintCurve.TabIndex = 21;
            this.numPaintCurve.ValueChanged += new System.EventHandler(this.numPaintCurve_ValueChanged);
            // 
            // gbxPaintStyle
            // 
            this.gbxPaintStyle.Controls.Add(this.chkHACKPain);
            this.gbxPaintStyle.Controls.Add(this.numPaintOffset);
            this.gbxPaintStyle.Controls.Add(this.labelPaintOffset);
            this.gbxPaintStyle.Controls.Add(this.numPaintScale);
            this.gbxPaintStyle.Controls.Add(this.labelPaintScale);
            this.gbxPaintStyle.Controls.Add(this.numPaintCurve);
            this.gbxPaintStyle.Controls.Add(this.labelPaintCurve);
            this.gbxPaintStyle.Location = new System.Drawing.Point(8, 352);
            this.gbxPaintStyle.Name = "gbxPaintStyle";
            this.gbxPaintStyle.Size = new System.Drawing.Size(232, 128);
            this.gbxPaintStyle.TabIndex = 22;
            this.gbxPaintStyle.TabStop = false;
            this.gbxPaintStyle.Text = "Paint Style";
            // 
            // chkHACKPain
            // 
            this.chkHACKPain.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkHACKPain.Location = new System.Drawing.Point(16, 96);
            this.chkHACKPain.Name = "chkHACKPain";
            this.chkHACKPain.Size = new System.Drawing.Size(208, 17);
            this.chkHACKPain.TabIndex = 26;
            this.chkHACKPain.Text = "All I Feel is Pain";
            this.chkHACKPain.UseVisualStyleBackColor = true;
            this.chkHACKPain.CheckedChanged += new System.EventHandler(this.chkHACKPain_CheckedChanged);
            // 
            // numPaintOffset
            // 
            this.numPaintOffset.DecimalPlaces = 2;
            this.numPaintOffset.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numPaintOffset.Location = new System.Drawing.Point(152, 72);
            this.numPaintOffset.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numPaintOffset.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            -2147483648});
            this.numPaintOffset.Name = "numPaintOffset";
            this.numPaintOffset.Size = new System.Drawing.Size(72, 20);
            this.numPaintOffset.TabIndex = 25;
            this.numPaintOffset.ValueChanged += new System.EventHandler(this.numPaintOffset_ValueChanged);
            // 
            // labelPaintOffset
            // 
            this.labelPaintOffset.AutoSize = true;
            this.labelPaintOffset.Location = new System.Drawing.Point(15, 74);
            this.labelPaintOffset.Name = "labelPaintOffset";
            this.labelPaintOffset.Size = new System.Drawing.Size(62, 13);
            this.labelPaintOffset.TabIndex = 24;
            this.labelPaintOffset.Text = "Paint Offset";
            // 
            // numPaintScale
            // 
            this.numPaintScale.DecimalPlaces = 2;
            this.numPaintScale.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numPaintScale.Location = new System.Drawing.Point(152, 48);
            this.numPaintScale.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numPaintScale.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            -2147483648});
            this.numPaintScale.Name = "numPaintScale";
            this.numPaintScale.Size = new System.Drawing.Size(72, 20);
            this.numPaintScale.TabIndex = 23;
            this.numPaintScale.ValueChanged += new System.EventHandler(this.numPaintScale_ValueChanged);
            // 
            // labelPaintScale
            // 
            this.labelPaintScale.AutoSize = true;
            this.labelPaintScale.Location = new System.Drawing.Point(15, 50);
            this.labelPaintScale.Name = "labelPaintScale";
            this.labelPaintScale.Size = new System.Drawing.Size(61, 13);
            this.labelPaintScale.TabIndex = 22;
            this.labelPaintScale.Text = "Paint Scale";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numConfigOption);
            this.groupBox2.Controls.Add(this.btnReloadShaders);
            this.groupBox2.Controls.Add(this.cbxConfigOptions);
            this.groupBox2.Location = new System.Drawing.Point(8, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(232, 96);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Graphics Config";
            // 
            // numConfigOption
            // 
            this.numConfigOption.Location = new System.Drawing.Point(176, 24);
            this.numConfigOption.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numConfigOption.Name = "numConfigOption";
            this.numConfigOption.Size = new System.Drawing.Size(48, 20);
            this.numConfigOption.TabIndex = 2;
            this.numConfigOption.ValueChanged += new System.EventHandler(this.numConfigOption_ValueChanged);
            // 
            // btnReloadShaders
            // 
            this.btnReloadShaders.Location = new System.Drawing.Point(48, 56);
            this.btnReloadShaders.Name = "btnReloadShaders";
            this.btnReloadShaders.Size = new System.Drawing.Size(104, 24);
            this.btnReloadShaders.TabIndex = 1;
            this.btnReloadShaders.Text = "Reload Shaders";
            this.btnReloadShaders.UseVisualStyleBackColor = true;
            this.btnReloadShaders.Click += new System.EventHandler(this.btnReloadShaders_Click);
            // 
            // cbxConfigOptions
            // 
            this.cbxConfigOptions.FormattingEnabled = true;
            this.cbxConfigOptions.Location = new System.Drawing.Point(16, 24);
            this.cbxConfigOptions.Name = "cbxConfigOptions";
            this.cbxConfigOptions.Size = new System.Drawing.Size(152, 21);
            this.cbxConfigOptions.TabIndex = 0;
            this.cbxConfigOptions.SelectedIndexChanged += new System.EventHandler(this.cbxConfigOptions_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.numSimDelta);
            this.groupBox3.Controls.Add(this.lblSimDelta);
            this.groupBox3.Controls.Add(this.numSimTime);
            this.groupBox3.Controls.Add(this.lblSimTime);
            this.groupBox3.Controls.Add(this.numExecDelta);
            this.groupBox3.Controls.Add(this.lblExecDelta);
            this.groupBox3.Controls.Add(this.numExecTime);
            this.groupBox3.Controls.Add(this.lblExecTime);
            this.groupBox3.Location = new System.Drawing.Point(8, 112);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(232, 120);
            this.groupBox3.TabIndex = 24;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Time Variables";
            // 
            // numSimDelta
            // 
            this.numSimDelta.DecimalPlaces = 2;
            this.numSimDelta.Enabled = false;
            this.numSimDelta.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numSimDelta.Location = new System.Drawing.Point(152, 96);
            this.numSimDelta.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numSimDelta.Name = "numSimDelta";
            this.numSimDelta.Size = new System.Drawing.Size(72, 20);
            this.numSimDelta.TabIndex = 27;
            this.numSimDelta.ValueChanged += new System.EventHandler(this.numSimDelta_ValueChanged);
            // 
            // lblSimDelta
            // 
            this.lblSimDelta.AutoSize = true;
            this.lblSimDelta.Enabled = false;
            this.lblSimDelta.Location = new System.Drawing.Point(15, 98);
            this.lblSimDelta.Name = "lblSimDelta";
            this.lblSimDelta.Size = new System.Drawing.Size(52, 13);
            this.lblSimDelta.TabIndex = 26;
            this.lblSimDelta.Text = "Sim Delta";
            // 
            // numSimTime
            // 
            this.numSimTime.DecimalPlaces = 2;
            this.numSimTime.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numSimTime.Location = new System.Drawing.Point(152, 72);
            this.numSimTime.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
            this.numSimTime.Name = "numSimTime";
            this.numSimTime.Size = new System.Drawing.Size(72, 20);
            this.numSimTime.TabIndex = 25;
            this.numSimTime.ValueChanged += new System.EventHandler(this.numSimTime_ValueChanged);
            // 
            // lblSimTime
            // 
            this.lblSimTime.AutoSize = true;
            this.lblSimTime.Location = new System.Drawing.Point(15, 74);
            this.lblSimTime.Name = "lblSimTime";
            this.lblSimTime.Size = new System.Drawing.Size(50, 13);
            this.lblSimTime.TabIndex = 24;
            this.lblSimTime.Text = "Sim Time";
            // 
            // numExecDelta
            // 
            this.numExecDelta.DecimalPlaces = 2;
            this.numExecDelta.Enabled = false;
            this.numExecDelta.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numExecDelta.Location = new System.Drawing.Point(152, 48);
            this.numExecDelta.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            65536});
            this.numExecDelta.Name = "numExecDelta";
            this.numExecDelta.Size = new System.Drawing.Size(72, 20);
            this.numExecDelta.TabIndex = 23;
            this.numExecDelta.ValueChanged += new System.EventHandler(this.numExecDelta_ValueChanged);
            // 
            // lblExecDelta
            // 
            this.lblExecDelta.AutoSize = true;
            this.lblExecDelta.Enabled = false;
            this.lblExecDelta.Location = new System.Drawing.Point(15, 50);
            this.lblExecDelta.Name = "lblExecDelta";
            this.lblExecDelta.Size = new System.Drawing.Size(59, 13);
            this.lblExecDelta.TabIndex = 22;
            this.lblExecDelta.Text = "Exec Delta";
            // 
            // numExecTime
            // 
            this.numExecTime.DecimalPlaces = 2;
            this.numExecTime.Enabled = false;
            this.numExecTime.Location = new System.Drawing.Point(152, 24);
            this.numExecTime.Maximum = new decimal(new int[] {
            18000,
            0,
            0,
            0});
            this.numExecTime.Name = "numExecTime";
            this.numExecTime.Size = new System.Drawing.Size(72, 20);
            this.numExecTime.TabIndex = 21;
            this.numExecTime.ValueChanged += new System.EventHandler(this.numExecTime_ValueChanged);
            // 
            // lblExecTime
            // 
            this.lblExecTime.AutoSize = true;
            this.lblExecTime.Enabled = false;
            this.lblExecTime.Location = new System.Drawing.Point(15, 26);
            this.lblExecTime.Name = "lblExecTime";
            this.lblExecTime.Size = new System.Drawing.Size(57, 13);
            this.lblExecTime.TabIndex = 20;
            this.lblExecTime.Text = "Exec Time";
            // 
            // gbxSOBParams
            // 
            this.gbxSOBParams.Controls.Add(this.numSOBClip);
            this.gbxSOBParams.Controls.Add(this.lblSOBClip);
            this.gbxSOBParams.Controls.Add(this.numSOBCloak);
            this.gbxSOBParams.Controls.Add(this.lblSOBCloak);
            this.gbxSOBParams.Controls.Add(this.numSOBAlpha);
            this.gbxSOBParams.Controls.Add(this.lblSOBAlpha);
            this.gbxSOBParams.Location = new System.Drawing.Point(8, 240);
            this.gbxSOBParams.Name = "gbxSOBParams";
            this.gbxSOBParams.Size = new System.Drawing.Size(232, 100);
            this.gbxSOBParams.TabIndex = 25;
            this.gbxSOBParams.TabStop = false;
            this.gbxSOBParams.Text = "SOB Params";
            // 
            // numSOBClip
            // 
            this.numSOBClip.DecimalPlaces = 2;
            this.numSOBClip.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numSOBClip.Location = new System.Drawing.Point(152, 72);
            this.numSOBClip.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSOBClip.Name = "numSOBClip";
            this.numSOBClip.Size = new System.Drawing.Size(72, 20);
            this.numSOBClip.TabIndex = 25;
            this.numSOBClip.ValueChanged += new System.EventHandler(this.numSOBClip_ValueChanged);
            // 
            // lblSOBClip
            // 
            this.lblSOBClip.AutoSize = true;
            this.lblSOBClip.Location = new System.Drawing.Point(15, 74);
            this.lblSOBClip.Name = "lblSOBClip";
            this.lblSOBClip.Size = new System.Drawing.Size(49, 13);
            this.lblSOBClip.TabIndex = 24;
            this.lblSOBClip.Text = "SOB Clip";
            // 
            // numSOBCloak
            // 
            this.numSOBCloak.DecimalPlaces = 2;
            this.numSOBCloak.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numSOBCloak.Location = new System.Drawing.Point(152, 48);
            this.numSOBCloak.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numSOBCloak.Name = "numSOBCloak";
            this.numSOBCloak.Size = new System.Drawing.Size(72, 20);
            this.numSOBCloak.TabIndex = 23;
            this.numSOBCloak.ValueChanged += new System.EventHandler(this.numSOBCloak_ValueChanged);
            // 
            // lblSOBCloak
            // 
            this.lblSOBCloak.AutoSize = true;
            this.lblSOBCloak.Location = new System.Drawing.Point(15, 50);
            this.lblSOBCloak.Name = "lblSOBCloak";
            this.lblSOBCloak.Size = new System.Drawing.Size(59, 13);
            this.lblSOBCloak.TabIndex = 22;
            this.lblSOBCloak.Text = "SOB Cloak";
            // 
            // numSOBAlpha
            // 
            this.numSOBAlpha.DecimalPlaces = 2;
            this.numSOBAlpha.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numSOBAlpha.Location = new System.Drawing.Point(152, 24);
            this.numSOBAlpha.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSOBAlpha.Name = "numSOBAlpha";
            this.numSOBAlpha.Size = new System.Drawing.Size(72, 20);
            this.numSOBAlpha.TabIndex = 21;
            this.numSOBAlpha.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSOBAlpha.ValueChanged += new System.EventHandler(this.numSOBAlpha_ValueChanged);
            // 
            // lblSOBAlpha
            // 
            this.lblSOBAlpha.AutoSize = true;
            this.lblSOBAlpha.Location = new System.Drawing.Point(15, 26);
            this.lblSOBAlpha.Name = "lblSOBAlpha";
            this.lblSOBAlpha.Size = new System.Drawing.Size(59, 13);
            this.lblSOBAlpha.TabIndex = 20;
            this.lblSOBAlpha.Text = "SOB Alpha";
            // 
            // numClipDist
            // 
            this.numClipDist.DecimalPlaces = 2;
            this.numClipDist.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numClipDist.Location = new System.Drawing.Point(385, 14);
            this.numClipDist.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numClipDist.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
            this.numClipDist.Name = "numClipDist";
            this.numClipDist.Size = new System.Drawing.Size(72, 20);
            this.numClipDist.TabIndex = 27;
            this.numClipDist.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numClipDist.ValueChanged += new System.EventHandler(this.numClipDist_ValueChanged);
            // 
            // lblClipDist
            // 
            this.lblClipDist.AutoSize = true;
            this.lblClipDist.Location = new System.Drawing.Point(248, 16);
            this.lblClipDist.Name = "lblClipDist";
            this.lblClipDist.Size = new System.Drawing.Size(69, 13);
            this.lblClipDist.TabIndex = 26;
            this.lblClipDist.Text = "Clip Distance";
            // 
            // btnEnterHyperspace
            // 
            this.btnEnterHyperspace.Location = new System.Drawing.Point(304, 48);
            this.btnEnterHyperspace.Name = "btnEnterHyperspace";
            this.btnEnterHyperspace.Size = new System.Drawing.Size(104, 24);
            this.btnEnterHyperspace.TabIndex = 28;
            this.btnEnterHyperspace.Text = "Enter Hyperspace";
            this.btnEnterHyperspace.UseVisualStyleBackColor = true;
            this.btnEnterHyperspace.Visible = false;
            this.btnEnterHyperspace.Click += new System.EventHandler(this.btnEnterHyperspace_Click);
            // 
            // btnExitHyperspace
            // 
            this.btnExitHyperspace.Location = new System.Drawing.Point(304, 80);
            this.btnExitHyperspace.Name = "btnExitHyperspace";
            this.btnExitHyperspace.Size = new System.Drawing.Size(104, 24);
            this.btnExitHyperspace.TabIndex = 29;
            this.btnExitHyperspace.Text = "Exit Hyperspace";
            this.btnExitHyperspace.UseVisualStyleBackColor = true;
            this.btnExitHyperspace.Visible = false;
            this.btnExitHyperspace.Click += new System.EventHandler(this.btnExitHyperspace_Click);
            // 
            // ShaderSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(688, 486);
            this.Controls.Add(this.btnExitHyperspace);
            this.Controls.Add(this.btnEnterHyperspace);
            this.Controls.Add(this.numClipDist);
            this.Controls.Add(this.lblClipDist);
            this.Controls.Add(this.gbxSOBParams);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.gbxPaintStyle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ShaderSettings";
            this.Text = "Settings";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.numPaintCurve)).EndInit();
            this.gbxPaintStyle.ResumeLayout(false);
            this.gbxPaintStyle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPaintOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPaintScale)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numConfigOption)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSimDelta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSimTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numExecDelta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numExecTime)).EndInit();
            this.gbxSOBParams.ResumeLayout(false);
            this.gbxSOBParams.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSOBClip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSOBCloak)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSOBAlpha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numClipDist)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.OpenFileDialog addDataPathDialog;
        private System.Windows.Forms.Label labelPaintCurve;
        private System.Windows.Forms.NumericUpDown numPaintCurve;
        private System.Windows.Forms.GroupBox gbxPaintStyle;
        private System.Windows.Forms.NumericUpDown numPaintOffset;
        private System.Windows.Forms.Label labelPaintOffset;
        private System.Windows.Forms.NumericUpDown numPaintScale;
        private System.Windows.Forms.Label labelPaintScale;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown numConfigOption;
        private System.Windows.Forms.Button btnReloadShaders;
        private System.Windows.Forms.ComboBox cbxConfigOptions;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown numSimDelta;
        private System.Windows.Forms.Label lblSimDelta;
        private System.Windows.Forms.NumericUpDown numSimTime;
        private System.Windows.Forms.Label lblSimTime;
        private System.Windows.Forms.NumericUpDown numExecDelta;
        private System.Windows.Forms.Label lblExecDelta;
        private System.Windows.Forms.NumericUpDown numExecTime;
        private System.Windows.Forms.Label lblExecTime;
        private System.Windows.Forms.GroupBox gbxSOBParams;
        private System.Windows.Forms.NumericUpDown numSOBClip;
        private System.Windows.Forms.Label lblSOBClip;
        private System.Windows.Forms.NumericUpDown numSOBCloak;
        private System.Windows.Forms.Label lblSOBCloak;
        private System.Windows.Forms.NumericUpDown numSOBAlpha;
        private System.Windows.Forms.Label lblSOBAlpha;
        private System.Windows.Forms.CheckBox chkHACKPain;
        private System.Windows.Forms.NumericUpDown numClipDist;
        private System.Windows.Forms.Label lblClipDist;
        private System.Windows.Forms.Button btnEnterHyperspace;
        private System.Windows.Forms.Button btnExitHyperspace;
    }
}