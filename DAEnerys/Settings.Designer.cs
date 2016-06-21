namespace DAEnerys
{
    partial class Settings
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
            this.labelJointSize = new System.Windows.Forms.Label();
            this.labelZoomSpeed = new System.Windows.Forms.Label();
            this.labelMarkerSize = new System.Windows.Forms.Label();
            this.labelFarClip = new System.Windows.Forms.Label();
            this.numericFarClip = new System.Windows.Forms.NumericUpDown();
            this.numericZoomSpeed = new System.Windows.Forms.NumericUpDown();
            this.groupCamera = new System.Windows.Forms.GroupBox();
            this.labelFOV = new System.Windows.Forms.Label();
            this.numericFOV = new System.Windows.Forms.NumericUpDown();
            this.labelNearClip = new System.Windows.Forms.Label();
            this.numericNearClip = new System.Windows.Forms.NumericUpDown();
            this.groupEditor = new System.Windows.Forms.GroupBox();
            this.labelIconSize = new System.Windows.Forms.Label();
            this.numericIconSize = new System.Windows.Forms.NumericUpDown();
            this.numericMarkerSize = new System.Windows.Forms.NumericUpDown();
            this.numericJointSize = new System.Windows.Forms.NumericUpDown();
            this.groupLighting = new System.Windows.Forms.GroupBox();
            this.buttonBackgroundColor = new System.Windows.Forms.Button();
            this.labelBackgroundColor = new System.Windows.Forms.Label();
            this.buttonAmbientColor = new System.Windows.Forms.Button();
            this.labelAmbientColor = new System.Windows.Forms.Label();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.groupDataPaths = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonRemoveDataPath = new System.Windows.Forms.Button();
            this.buttonAddDataPath = new System.Windows.Forms.Button();
            this.listDataPaths = new System.Windows.Forms.ListBox();
            this.addDataPathDialog = new System.Windows.Forms.OpenFileDialog();
            this.groupRendering = new System.Windows.Forms.GroupBox();
            this.checkVSync = new System.Windows.Forms.CheckBox();
            this.checkRenderOnTop = new System.Windows.Forms.CheckBox();
            this.labelFSAASamples = new System.Windows.Forms.Label();
            this.comboFSAASamples = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericFarClip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericZoomSpeed)).BeginInit();
            this.groupCamera.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericFOV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericNearClip)).BeginInit();
            this.groupEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericIconSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMarkerSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericJointSize)).BeginInit();
            this.groupLighting.SuspendLayout();
            this.groupDataPaths.SuspendLayout();
            this.groupRendering.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelJointSize
            // 
            this.labelJointSize.AutoSize = true;
            this.labelJointSize.Location = new System.Drawing.Point(8, 21);
            this.labelJointSize.Name = "labelJointSize";
            this.labelJointSize.Size = new System.Drawing.Size(50, 13);
            this.labelJointSize.TabIndex = 0;
            this.labelJointSize.Text = "Joint size";
            // 
            // labelZoomSpeed
            // 
            this.labelZoomSpeed.AutoSize = true;
            this.labelZoomSpeed.Location = new System.Drawing.Point(8, 21);
            this.labelZoomSpeed.Name = "labelZoomSpeed";
            this.labelZoomSpeed.Size = new System.Drawing.Size(66, 13);
            this.labelZoomSpeed.TabIndex = 3;
            this.labelZoomSpeed.Text = "Zoom speed";
            // 
            // labelMarkerSize
            // 
            this.labelMarkerSize.AutoSize = true;
            this.labelMarkerSize.Location = new System.Drawing.Point(8, 47);
            this.labelMarkerSize.Name = "labelMarkerSize";
            this.labelMarkerSize.Size = new System.Drawing.Size(61, 13);
            this.labelMarkerSize.TabIndex = 7;
            this.labelMarkerSize.Text = "Marker size";
            // 
            // labelFarClip
            // 
            this.labelFarClip.AutoSize = true;
            this.labelFarClip.Location = new System.Drawing.Point(8, 47);
            this.labelFarClip.Name = "labelFarClip";
            this.labelFarClip.Size = new System.Drawing.Size(41, 13);
            this.labelFarClip.TabIndex = 10;
            this.labelFarClip.Text = "Far clip";
            // 
            // numericFarClip
            // 
            this.numericFarClip.DecimalPlaces = 4;
            this.numericFarClip.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericFarClip.Location = new System.Drawing.Point(105, 45);
            this.numericFarClip.Maximum = new decimal(new int[] {
            658067456,
            1164,
            0,
            0});
            this.numericFarClip.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.numericFarClip.Name = "numericFarClip";
            this.numericFarClip.Size = new System.Drawing.Size(218, 20);
            this.numericFarClip.TabIndex = 11;
            this.numericFarClip.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericFarClip.ValueChanged += new System.EventHandler(this.numericClipDistance_ValueChanged);
            // 
            // numericZoomSpeed
            // 
            this.numericZoomSpeed.DecimalPlaces = 1;
            this.numericZoomSpeed.Location = new System.Drawing.Point(105, 19);
            this.numericZoomSpeed.Maximum = new decimal(new int[] {
            658067456,
            1164,
            0,
            0});
            this.numericZoomSpeed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            524288});
            this.numericZoomSpeed.Name = "numericZoomSpeed";
            this.numericZoomSpeed.Size = new System.Drawing.Size(218, 20);
            this.numericZoomSpeed.TabIndex = 12;
            this.numericZoomSpeed.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericZoomSpeed.ValueChanged += new System.EventHandler(this.numericZoomSpeed_ValueChanged);
            // 
            // groupCamera
            // 
            this.groupCamera.AutoSize = true;
            this.groupCamera.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupCamera.Controls.Add(this.labelFOV);
            this.groupCamera.Controls.Add(this.numericFOV);
            this.groupCamera.Controls.Add(this.labelNearClip);
            this.groupCamera.Controls.Add(this.numericNearClip);
            this.groupCamera.Controls.Add(this.labelFarClip);
            this.groupCamera.Controls.Add(this.labelZoomSpeed);
            this.groupCamera.Controls.Add(this.numericZoomSpeed);
            this.groupCamera.Controls.Add(this.numericFarClip);
            this.groupCamera.Location = new System.Drawing.Point(13, 13);
            this.groupCamera.Name = "groupCamera";
            this.groupCamera.Size = new System.Drawing.Size(329, 136);
            this.groupCamera.TabIndex = 15;
            this.groupCamera.TabStop = false;
            this.groupCamera.Text = "Camera";
            // 
            // labelFOV
            // 
            this.labelFOV.AutoSize = true;
            this.labelFOV.Location = new System.Drawing.Point(8, 99);
            this.labelFOV.Name = "labelFOV";
            this.labelFOV.Size = new System.Drawing.Size(66, 13);
            this.labelFOV.TabIndex = 16;
            this.labelFOV.Text = "Field of view";
            // 
            // numericFOV
            // 
            this.numericFOV.Location = new System.Drawing.Point(105, 97);
            this.numericFOV.Maximum = new decimal(new int[] {
            179,
            0,
            0,
            0});
            this.numericFOV.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericFOV.Name = "numericFOV";
            this.numericFOV.Size = new System.Drawing.Size(218, 20);
            this.numericFOV.TabIndex = 15;
            this.numericFOV.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numericFOV.ValueChanged += new System.EventHandler(this.numericFOV_ValueChanged);
            // 
            // labelNearClip
            // 
            this.labelNearClip.AutoSize = true;
            this.labelNearClip.Location = new System.Drawing.Point(8, 73);
            this.labelNearClip.Name = "labelNearClip";
            this.labelNearClip.Size = new System.Drawing.Size(49, 13);
            this.labelNearClip.TabIndex = 14;
            this.labelNearClip.Text = "Near clip";
            // 
            // numericNearClip
            // 
            this.numericNearClip.DecimalPlaces = 4;
            this.numericNearClip.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericNearClip.Location = new System.Drawing.Point(105, 71);
            this.numericNearClip.Maximum = new decimal(new int[] {
            658067456,
            1164,
            0,
            0});
            this.numericNearClip.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.numericNearClip.Name = "numericNearClip";
            this.numericNearClip.Size = new System.Drawing.Size(218, 20);
            this.numericNearClip.TabIndex = 13;
            this.numericNearClip.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericNearClip.ValueChanged += new System.EventHandler(this.numericNearClip_ValueChanged);
            // 
            // groupEditor
            // 
            this.groupEditor.AutoSize = true;
            this.groupEditor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupEditor.Controls.Add(this.labelIconSize);
            this.groupEditor.Controls.Add(this.numericIconSize);
            this.groupEditor.Controls.Add(this.numericMarkerSize);
            this.groupEditor.Controls.Add(this.numericJointSize);
            this.groupEditor.Controls.Add(this.labelMarkerSize);
            this.groupEditor.Controls.Add(this.labelJointSize);
            this.groupEditor.Location = new System.Drawing.Point(13, 155);
            this.groupEditor.Name = "groupEditor";
            this.groupEditor.Size = new System.Drawing.Size(329, 110);
            this.groupEditor.TabIndex = 16;
            this.groupEditor.TabStop = false;
            this.groupEditor.Text = "Editor";
            // 
            // labelIconSize
            // 
            this.labelIconSize.AutoSize = true;
            this.labelIconSize.Location = new System.Drawing.Point(8, 73);
            this.labelIconSize.Name = "labelIconSize";
            this.labelIconSize.Size = new System.Drawing.Size(49, 13);
            this.labelIconSize.TabIndex = 19;
            this.labelIconSize.Text = "Icon size";
            // 
            // numericIconSize
            // 
            this.numericIconSize.DecimalPlaces = 3;
            this.numericIconSize.Location = new System.Drawing.Point(75, 71);
            this.numericIconSize.Maximum = new decimal(new int[] {
            658067456,
            1164,
            0,
            0});
            this.numericIconSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericIconSize.Name = "numericIconSize";
            this.numericIconSize.Size = new System.Drawing.Size(248, 20);
            this.numericIconSize.TabIndex = 18;
            this.numericIconSize.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericIconSize.ValueChanged += new System.EventHandler(this.numericIconSize_ValueChanged);
            // 
            // numericMarkerSize
            // 
            this.numericMarkerSize.DecimalPlaces = 3;
            this.numericMarkerSize.Location = new System.Drawing.Point(75, 45);
            this.numericMarkerSize.Maximum = new decimal(new int[] {
            658067456,
            1164,
            0,
            0});
            this.numericMarkerSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            524288});
            this.numericMarkerSize.Name = "numericMarkerSize";
            this.numericMarkerSize.Size = new System.Drawing.Size(248, 20);
            this.numericMarkerSize.TabIndex = 16;
            this.numericMarkerSize.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericMarkerSize.ValueChanged += new System.EventHandler(this.numericMarkerSize_ValueChanged);
            // 
            // numericJointSize
            // 
            this.numericJointSize.DecimalPlaces = 1;
            this.numericJointSize.Location = new System.Drawing.Point(75, 19);
            this.numericJointSize.Maximum = new decimal(new int[] {
            658067456,
            1164,
            0,
            0});
            this.numericJointSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            524288});
            this.numericJointSize.Name = "numericJointSize";
            this.numericJointSize.Size = new System.Drawing.Size(248, 20);
            this.numericJointSize.TabIndex = 15;
            this.numericJointSize.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericJointSize.ValueChanged += new System.EventHandler(this.numericJointSize_ValueChanged);
            // 
            // groupLighting
            // 
            this.groupLighting.AutoSize = true;
            this.groupLighting.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupLighting.Controls.Add(this.buttonBackgroundColor);
            this.groupLighting.Controls.Add(this.labelBackgroundColor);
            this.groupLighting.Controls.Add(this.buttonAmbientColor);
            this.groupLighting.Controls.Add(this.labelAmbientColor);
            this.groupLighting.Location = new System.Drawing.Point(13, 382);
            this.groupLighting.Name = "groupLighting";
            this.groupLighting.Size = new System.Drawing.Size(329, 84);
            this.groupLighting.TabIndex = 17;
            this.groupLighting.TabStop = false;
            this.groupLighting.Text = "Lighting";
            // 
            // buttonBackgroundColor
            // 
            this.buttonBackgroundColor.BackColor = System.Drawing.Color.Red;
            this.buttonBackgroundColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBackgroundColor.Location = new System.Drawing.Point(105, 45);
            this.buttonBackgroundColor.Name = "buttonBackgroundColor";
            this.buttonBackgroundColor.Size = new System.Drawing.Size(218, 20);
            this.buttonBackgroundColor.TabIndex = 4;
            this.buttonBackgroundColor.UseVisualStyleBackColor = false;
            this.buttonBackgroundColor.Click += new System.EventHandler(this.buttonBackgroundColor_Click);
            // 
            // labelBackgroundColor
            // 
            this.labelBackgroundColor.AutoSize = true;
            this.labelBackgroundColor.Location = new System.Drawing.Point(8, 49);
            this.labelBackgroundColor.Name = "labelBackgroundColor";
            this.labelBackgroundColor.Size = new System.Drawing.Size(91, 13);
            this.labelBackgroundColor.TabIndex = 3;
            this.labelBackgroundColor.Text = "Background color";
            // 
            // buttonAmbientColor
            // 
            this.buttonAmbientColor.BackColor = System.Drawing.Color.Red;
            this.buttonAmbientColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAmbientColor.Location = new System.Drawing.Point(105, 19);
            this.buttonAmbientColor.Name = "buttonAmbientColor";
            this.buttonAmbientColor.Size = new System.Drawing.Size(218, 20);
            this.buttonAmbientColor.TabIndex = 2;
            this.buttonAmbientColor.UseVisualStyleBackColor = false;
            this.buttonAmbientColor.Click += new System.EventHandler(this.buttonAmbientColor_Click);
            // 
            // labelAmbientColor
            // 
            this.labelAmbientColor.AutoSize = true;
            this.labelAmbientColor.Location = new System.Drawing.Point(8, 23);
            this.labelAmbientColor.Name = "labelAmbientColor";
            this.labelAmbientColor.Size = new System.Drawing.Size(71, 13);
            this.labelAmbientColor.TabIndex = 0;
            this.labelAmbientColor.Text = "Ambient color";
            // 
            // colorDialog
            // 
            this.colorDialog.AnyColor = true;
            this.colorDialog.Color = System.Drawing.Color.Gray;
            this.colorDialog.SolidColorOnly = true;
            // 
            // groupDataPaths
            // 
            this.groupDataPaths.AutoSize = true;
            this.groupDataPaths.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupDataPaths.Controls.Add(this.label1);
            this.groupDataPaths.Controls.Add(this.buttonRemoveDataPath);
            this.groupDataPaths.Controls.Add(this.buttonAddDataPath);
            this.groupDataPaths.Controls.Add(this.listDataPaths);
            this.groupDataPaths.Location = new System.Drawing.Point(13, 472);
            this.groupDataPaths.Name = "groupDataPaths";
            this.groupDataPaths.Size = new System.Drawing.Size(329, 188);
            this.groupDataPaths.TabIndex = 19;
            this.groupDataPaths.TabStop = false;
            this.groupDataPaths.Text = "Data paths";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 146);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(317, 26);
            this.label1.TabIndex = 25;
            this.label1.Text = "The order of the paths matter. Files in lower paths will overwrite files in the p" +
    "aths above them.";
            // 
            // buttonRemoveDataPath
            // 
            this.buttonRemoveDataPath.Location = new System.Drawing.Point(168, 120);
            this.buttonRemoveDataPath.Name = "buttonRemoveDataPath";
            this.buttonRemoveDataPath.Size = new System.Drawing.Size(155, 23);
            this.buttonRemoveDataPath.TabIndex = 24;
            this.buttonRemoveDataPath.Text = "Remove";
            this.buttonRemoveDataPath.UseVisualStyleBackColor = true;
            this.buttonRemoveDataPath.Click += new System.EventHandler(this.buttonRemoveDataPath_Click);
            // 
            // buttonAddDataPath
            // 
            this.buttonAddDataPath.Location = new System.Drawing.Point(6, 120);
            this.buttonAddDataPath.Name = "buttonAddDataPath";
            this.buttonAddDataPath.Size = new System.Drawing.Size(156, 23);
            this.buttonAddDataPath.TabIndex = 23;
            this.buttonAddDataPath.Text = "Add";
            this.buttonAddDataPath.UseVisualStyleBackColor = true;
            this.buttonAddDataPath.Click += new System.EventHandler(this.buttonAddDataPath_Click);
            // 
            // listDataPaths
            // 
            this.listDataPaths.FormattingEnabled = true;
            this.listDataPaths.HorizontalScrollbar = true;
            this.listDataPaths.Location = new System.Drawing.Point(6, 19);
            this.listDataPaths.Name = "listDataPaths";
            this.listDataPaths.Size = new System.Drawing.Size(317, 95);
            this.listDataPaths.TabIndex = 22;
            // 
            // addDataPathDialog
            // 
            this.addDataPathDialog.FileName = "keeper.txt";
            this.addDataPathDialog.Filter = "Data roots|keeper.txt";
            this.addDataPathDialog.Title = "Select keeper.txt in data root folder";
            // 
            // groupRendering
            // 
            this.groupRendering.AutoSize = true;
            this.groupRendering.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupRendering.Controls.Add(this.checkVSync);
            this.groupRendering.Controls.Add(this.checkRenderOnTop);
            this.groupRendering.Controls.Add(this.labelFSAASamples);
            this.groupRendering.Controls.Add(this.comboFSAASamples);
            this.groupRendering.Location = new System.Drawing.Point(13, 271);
            this.groupRendering.Name = "groupRendering";
            this.groupRendering.Size = new System.Drawing.Size(328, 105);
            this.groupRendering.TabIndex = 18;
            this.groupRendering.TabStop = false;
            this.groupRendering.Text = "Rendering";
            // 
            // checkVSync
            // 
            this.checkVSync.AutoSize = true;
            this.checkVSync.Checked = true;
            this.checkVSync.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkVSync.Location = new System.Drawing.Point(11, 69);
            this.checkVSync.Name = "checkVSync";
            this.checkVSync.Size = new System.Drawing.Size(172, 17);
            this.checkVSync.TabIndex = 23;
            this.checkVSync.Text = "Enable vertical synchronization";
            this.checkVSync.UseVisualStyleBackColor = true;
            this.checkVSync.CheckedChanged += new System.EventHandler(this.checkVSync_CheckedChanged);
            // 
            // checkRenderOnTop
            // 
            this.checkRenderOnTop.AutoSize = true;
            this.checkRenderOnTop.Checked = true;
            this.checkRenderOnTop.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkRenderOnTop.Location = new System.Drawing.Point(11, 46);
            this.checkRenderOnTop.Name = "checkRenderOnTop";
            this.checkRenderOnTop.Size = new System.Drawing.Size(151, 17);
            this.checkRenderOnTop.TabIndex = 21;
            this.checkRenderOnTop.Text = "Draw visualizations in front";
            this.checkRenderOnTop.UseVisualStyleBackColor = true;
            this.checkRenderOnTop.CheckedChanged += new System.EventHandler(this.checkRenderOnTop_CheckedChanged);
            // 
            // labelFSAASamples
            // 
            this.labelFSAASamples.AutoSize = true;
            this.labelFSAASamples.Location = new System.Drawing.Point(7, 22);
            this.labelFSAASamples.Name = "labelFSAASamples";
            this.labelFSAASamples.Size = new System.Drawing.Size(92, 13);
            this.labelFSAASamples.TabIndex = 20;
            this.labelFSAASamples.Text = "FSAA anti-aliasing";
            // 
            // comboFSAASamples
            // 
            this.comboFSAASamples.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboFSAASamples.FormattingEnabled = true;
            this.comboFSAASamples.Items.AddRange(new object[] {
            "0 samples",
            "2 samples",
            "4 samples"});
            this.comboFSAASamples.Location = new System.Drawing.Point(104, 19);
            this.comboFSAASamples.Name = "comboFSAASamples";
            this.comboFSAASamples.Size = new System.Drawing.Size(218, 21);
            this.comboFSAASamples.TabIndex = 19;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(394, 689);
            this.Controls.Add(this.groupRendering);
            this.Controls.Add(this.groupDataPaths);
            this.Controls.Add(this.groupLighting);
            this.Controls.Add(this.groupEditor);
            this.Controls.Add(this.groupCamera);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.Text = "Settings";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.numericFarClip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericZoomSpeed)).EndInit();
            this.groupCamera.ResumeLayout(false);
            this.groupCamera.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericFOV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericNearClip)).EndInit();
            this.groupEditor.ResumeLayout(false);
            this.groupEditor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericIconSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMarkerSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericJointSize)).EndInit();
            this.groupLighting.ResumeLayout(false);
            this.groupLighting.PerformLayout();
            this.groupDataPaths.ResumeLayout(false);
            this.groupRendering.ResumeLayout(false);
            this.groupRendering.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelJointSize;
        private System.Windows.Forms.Label labelZoomSpeed;
        private System.Windows.Forms.Label labelMarkerSize;
        private System.Windows.Forms.Label labelFarClip;
        private System.Windows.Forms.NumericUpDown numericFarClip;
        private System.Windows.Forms.NumericUpDown numericZoomSpeed;
        private System.Windows.Forms.GroupBox groupCamera;
        private System.Windows.Forms.GroupBox groupEditor;
        private System.Windows.Forms.NumericUpDown numericMarkerSize;
        private System.Windows.Forms.NumericUpDown numericJointSize;
        private System.Windows.Forms.Label labelNearClip;
        private System.Windows.Forms.NumericUpDown numericNearClip;
        private System.Windows.Forms.GroupBox groupLighting;
        private System.Windows.Forms.Label labelAmbientColor;
        private System.Windows.Forms.Button buttonAmbientColor;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.Label labelFOV;
        private System.Windows.Forms.NumericUpDown numericFOV;
        private System.Windows.Forms.Button buttonBackgroundColor;
        private System.Windows.Forms.Label labelBackgroundColor;
        private System.Windows.Forms.GroupBox groupDataPaths;
        private System.Windows.Forms.Button buttonRemoveDataPath;
        private System.Windows.Forms.Button buttonAddDataPath;
        private System.Windows.Forms.ListBox listDataPaths;
        private System.Windows.Forms.OpenFileDialog addDataPathDialog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericIconSize;
        private System.Windows.Forms.Label labelIconSize;
        private System.Windows.Forms.GroupBox groupRendering;
        private System.Windows.Forms.CheckBox checkVSync;
        private System.Windows.Forms.CheckBox checkRenderOnTop;
        private System.Windows.Forms.Label labelFSAASamples;
        private System.Windows.Forms.ComboBox comboFSAASamples;
    }
}