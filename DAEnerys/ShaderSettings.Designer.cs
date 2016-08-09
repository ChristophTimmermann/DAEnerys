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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numPaintOffset = new System.Windows.Forms.NumericUpDown();
            this.labelPaintOffset = new System.Windows.Forms.Label();
            this.numPaintScale = new System.Windows.Forms.NumericUpDown();
            this.labelPaintScale = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbxConfigOptions = new System.Windows.Forms.ComboBox();
            this.btnReloadShaders = new System.Windows.Forms.Button();
            this.numConfigOption = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numPaintCurve)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPaintOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPaintScale)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numConfigOption)).BeginInit();
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
            1,
            0,
            0,
            65536});
            this.numPaintCurve.Location = new System.Drawing.Point(112, 24);
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numPaintOffset);
            this.groupBox1.Controls.Add(this.labelPaintOffset);
            this.groupBox1.Controls.Add(this.numPaintScale);
            this.groupBox1.Controls.Add(this.labelPaintScale);
            this.groupBox1.Controls.Add(this.numPaintCurve);
            this.groupBox1.Controls.Add(this.labelPaintCurve);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 100);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Paint Style";
            // 
            // numPaintOffset
            // 
            this.numPaintOffset.DecimalPlaces = 2;
            this.numPaintOffset.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numPaintOffset.Location = new System.Drawing.Point(112, 72);
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
            1,
            0,
            0,
            65536});
            this.numPaintScale.Location = new System.Drawing.Point(112, 48);
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
            this.groupBox2.Location = new System.Drawing.Point(8, 120);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(248, 96);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Paint Style";
            // 
            // cbxConfigOptions
            // 
            this.cbxConfigOptions.FormattingEnabled = true;
            this.cbxConfigOptions.Location = new System.Drawing.Point(16, 24);
            this.cbxConfigOptions.Name = "cbxConfigOptions";
            this.cbxConfigOptions.Size = new System.Drawing.Size(176, 21);
            this.cbxConfigOptions.TabIndex = 0;
            this.cbxConfigOptions.SelectedIndexChanged += new System.EventHandler(this.cbxConfigOptions_SelectedIndexChanged);
            // 
            // btnReloadShaders
            // 
            this.btnReloadShaders.Location = new System.Drawing.Point(72, 56);
            this.btnReloadShaders.Name = "btnReloadShaders";
            this.btnReloadShaders.Size = new System.Drawing.Size(104, 24);
            this.btnReloadShaders.TabIndex = 1;
            this.btnReloadShaders.Text = "Reload Shaders";
            this.btnReloadShaders.UseVisualStyleBackColor = true;
            this.btnReloadShaders.Click += new System.EventHandler(this.btnReloadShaders_Click);
            // 
            // numConfigOption
            // 
            this.numConfigOption.Location = new System.Drawing.Point(200, 24);
            this.numConfigOption.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numConfigOption.Name = "numConfigOption";
            this.numConfigOption.Size = new System.Drawing.Size(40, 20);
            this.numConfigOption.TabIndex = 2;
            this.numConfigOption.ValueChanged += new System.EventHandler(this.numConfigOption_ValueChanged);
            // 
            // ShaderSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(688, 469);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ShaderSettings";
            this.Text = "Settings";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.numPaintCurve)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPaintOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPaintScale)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numConfigOption)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.OpenFileDialog addDataPathDialog;
        private System.Windows.Forms.Label labelPaintCurve;
        private System.Windows.Forms.NumericUpDown numPaintCurve;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numPaintOffset;
        private System.Windows.Forms.Label labelPaintOffset;
        private System.Windows.Forms.NumericUpDown numPaintScale;
        private System.Windows.Forms.Label labelPaintScale;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown numConfigOption;
        private System.Windows.Forms.Button btnReloadShaders;
        private System.Windows.Forms.ComboBox cbxConfigOptions;
    }
}