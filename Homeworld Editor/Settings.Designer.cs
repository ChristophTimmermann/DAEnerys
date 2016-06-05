namespace HomeworldDAEEditor
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
            this.labelNearClip = new System.Windows.Forms.Label();
            this.numericNearClip = new System.Windows.Forms.NumericUpDown();
            this.groupEditor = new System.Windows.Forms.GroupBox();
            this.numericMarkerSize = new System.Windows.Forms.NumericUpDown();
            this.numericJointSize = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericFarClip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericZoomSpeed)).BeginInit();
            this.groupCamera.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericNearClip)).BeginInit();
            this.groupEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericMarkerSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericJointSize)).BeginInit();
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
            this.numericFarClip.Location = new System.Drawing.Point(80, 45);
            this.numericFarClip.Maximum = new decimal(new int[] {
            50000000,
            0,
            0,
            0});
            this.numericFarClip.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.numericFarClip.Name = "numericFarClip";
            this.numericFarClip.Size = new System.Drawing.Size(217, 20);
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
            this.numericZoomSpeed.Location = new System.Drawing.Point(80, 19);
            this.numericZoomSpeed.Maximum = new decimal(new int[] {
            50000000,
            0,
            0,
            0});
            this.numericZoomSpeed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            524288});
            this.numericZoomSpeed.Name = "numericZoomSpeed";
            this.numericZoomSpeed.Size = new System.Drawing.Size(217, 20);
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
            this.groupCamera.Controls.Add(this.labelNearClip);
            this.groupCamera.Controls.Add(this.numericNearClip);
            this.groupCamera.Controls.Add(this.labelFarClip);
            this.groupCamera.Controls.Add(this.labelZoomSpeed);
            this.groupCamera.Controls.Add(this.numericZoomSpeed);
            this.groupCamera.Controls.Add(this.numericFarClip);
            this.groupCamera.Location = new System.Drawing.Point(13, 13);
            this.groupCamera.Name = "groupCamera";
            this.groupCamera.Size = new System.Drawing.Size(303, 110);
            this.groupCamera.TabIndex = 15;
            this.groupCamera.TabStop = false;
            this.groupCamera.Text = "Camera";
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
            this.numericNearClip.Location = new System.Drawing.Point(80, 71);
            this.numericNearClip.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numericNearClip.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.numericNearClip.Name = "numericNearClip";
            this.numericNearClip.Size = new System.Drawing.Size(217, 20);
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
            this.groupEditor.Controls.Add(this.numericMarkerSize);
            this.groupEditor.Controls.Add(this.numericJointSize);
            this.groupEditor.Controls.Add(this.labelMarkerSize);
            this.groupEditor.Controls.Add(this.labelJointSize);
            this.groupEditor.Location = new System.Drawing.Point(13, 129);
            this.groupEditor.Name = "groupEditor";
            this.groupEditor.Size = new System.Drawing.Size(303, 77);
            this.groupEditor.TabIndex = 16;
            this.groupEditor.TabStop = false;
            this.groupEditor.Text = "Editor";
            // 
            // numericMarkerSize
            // 
            this.numericMarkerSize.DecimalPlaces = 3;
            this.numericMarkerSize.Location = new System.Drawing.Point(80, 45);
            this.numericMarkerSize.Maximum = new decimal(new int[] {
            705032704,
            1,
            0,
            0});
            this.numericMarkerSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            524288});
            this.numericMarkerSize.Name = "numericMarkerSize";
            this.numericMarkerSize.Size = new System.Drawing.Size(217, 20);
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
            this.numericJointSize.Location = new System.Drawing.Point(80, 19);
            this.numericJointSize.Maximum = new decimal(new int[] {
            50000000,
            0,
            0,
            0});
            this.numericJointSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            524288});
            this.numericJointSize.Name = "numericJointSize";
            this.numericJointSize.Size = new System.Drawing.Size(217, 20);
            this.numericJointSize.TabIndex = 15;
            this.numericJointSize.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericJointSize.ValueChanged += new System.EventHandler(this.numericJointSize_ValueChanged);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(395, 229);
            this.Controls.Add(this.groupEditor);
            this.Controls.Add(this.groupCamera);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.numericFarClip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericZoomSpeed)).EndInit();
            this.groupCamera.ResumeLayout(false);
            this.groupCamera.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericNearClip)).EndInit();
            this.groupEditor.ResumeLayout(false);
            this.groupEditor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericMarkerSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericJointSize)).EndInit();
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
    }
}