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
            this.gbxPaintStyle = new System.Windows.Forms.GroupBox();
            this.numPaintStyleOffset = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numPaintStyleScale = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numPaintStyleCurve = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.chkHACKPain = new System.Windows.Forms.CheckBox();
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numDeathRatio = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numLifeAlpha = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.numPeakScar = new DAEnerys.LabeledNumericUpDown();
            this.numPeakFren = new DAEnerys.LabeledNumericUpDown();
            this.numPeakPaint = new DAEnerys.LabeledNumericUpDown();
            this.numPeakBase = new DAEnerys.LabeledNumericUpDown();
            this.numPaintDim = new DAEnerys.LabeledNumericUpDown();
            this.numPaintBias = new DAEnerys.LabeledNumericUpDown();
            this.numPaintScale = new DAEnerys.LabeledNumericUpDown();
            this.numPaintCurve = new DAEnerys.LabeledNumericUpDown();
            this.numFrenCurve = new DAEnerys.LabeledNumericUpDown();
            this.numFrenBias = new DAEnerys.LabeledNumericUpDown();
            this.numFrenPower = new DAEnerys.LabeledNumericUpDown();
            this.numReflAddMix = new DAEnerys.LabeledNumericUpDown();
            this.numReflFren = new DAEnerys.LabeledNumericUpDown();
            this.numReflPower = new DAEnerys.LabeledNumericUpDown();
            this.numGlossBias = new DAEnerys.LabeledNumericUpDown();
            this.numGlossScale = new DAEnerys.LabeledNumericUpDown();
            this.numGlossCurve = new DAEnerys.LabeledNumericUpDown();
            this.numSpecFren = new DAEnerys.LabeledNumericUpDown();
            this.numSpecPower = new DAEnerys.LabeledNumericUpDown();
            this.numGlowFren = new DAEnerys.LabeledNumericUpDown();
            this.numGlowPower = new DAEnerys.LabeledNumericUpDown();
            this.numDiffFren = new DAEnerys.LabeledNumericUpDown();
            this.gbxPaintStyle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPaintStyleOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPaintStyleScale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPaintStyleCurve)).BeginInit();
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
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDeathRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLifeAlpha)).BeginInit();
            this.groupBox4.SuspendLayout();
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
            // gbxPaintStyle
            // 
            this.gbxPaintStyle.Controls.Add(this.numPaintStyleOffset);
            this.gbxPaintStyle.Controls.Add(this.label5);
            this.gbxPaintStyle.Controls.Add(this.numPaintStyleScale);
            this.gbxPaintStyle.Controls.Add(this.label4);
            this.gbxPaintStyle.Controls.Add(this.numPaintStyleCurve);
            this.gbxPaintStyle.Controls.Add(this.label1);
            this.gbxPaintStyle.Controls.Add(this.chkHACKPain);
            this.gbxPaintStyle.Location = new System.Drawing.Point(8, 400);
            this.gbxPaintStyle.Name = "gbxPaintStyle";
            this.gbxPaintStyle.Size = new System.Drawing.Size(232, 128);
            this.gbxPaintStyle.TabIndex = 22;
            this.gbxPaintStyle.TabStop = false;
            this.gbxPaintStyle.Text = "Paint Style";
            // 
            // numPaintStyleOffset
            // 
            this.numPaintStyleOffset.DecimalPlaces = 2;
            this.numPaintStyleOffset.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numPaintStyleOffset.Location = new System.Drawing.Point(152, 72);
            this.numPaintStyleOffset.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numPaintStyleOffset.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.numPaintStyleOffset.Name = "numPaintStyleOffset";
            this.numPaintStyleOffset.Size = new System.Drawing.Size(72, 20);
            this.numPaintStyleOffset.TabIndex = 32;
            this.numPaintStyleOffset.ValueChanged += new System.EventHandler(this.numPaintStyleOffset_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 31;
            this.label5.Text = "Paint Offset";
            // 
            // numPaintStyleScale
            // 
            this.numPaintStyleScale.DecimalPlaces = 2;
            this.numPaintStyleScale.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numPaintStyleScale.Location = new System.Drawing.Point(152, 48);
            this.numPaintStyleScale.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numPaintStyleScale.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.numPaintStyleScale.Name = "numPaintStyleScale";
            this.numPaintStyleScale.Size = new System.Drawing.Size(72, 20);
            this.numPaintStyleScale.TabIndex = 30;
            this.numPaintStyleScale.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numPaintStyleScale.ValueChanged += new System.EventHandler(this.numPaintStyleScale_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 29;
            this.label4.Text = "Paint Scale";
            // 
            // numPaintStyleCurve
            // 
            this.numPaintStyleCurve.DecimalPlaces = 2;
            this.numPaintStyleCurve.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numPaintStyleCurve.Location = new System.Drawing.Point(152, 24);
            this.numPaintStyleCurve.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numPaintStyleCurve.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.numPaintStyleCurve.Name = "numPaintStyleCurve";
            this.numPaintStyleCurve.Size = new System.Drawing.Size(72, 20);
            this.numPaintStyleCurve.TabIndex = 28;
            this.numPaintStyleCurve.ValueChanged += new System.EventHandler(this.numPaintStyleCurve_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Paint Curve";
            // 
            // chkHACKPain
            // 
            this.chkHACKPain.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkHACKPain.Location = new System.Drawing.Point(16, 104);
            this.chkHACKPain.Name = "chkHACKPain";
            this.chkHACKPain.Size = new System.Drawing.Size(208, 17);
            this.chkHACKPain.TabIndex = 26;
            this.chkHACKPain.Text = "All I Feel is Pain";
            this.chkHACKPain.UseVisualStyleBackColor = true;
            this.chkHACKPain.CheckedChanged += new System.EventHandler(this.chkHACKPain_CheckedChanged);
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
            this.groupBox3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox3.Controls.Add(this.numSimDelta);
            this.groupBox3.Controls.Add(this.lblSimDelta);
            this.groupBox3.Controls.Add(this.numSimTime);
            this.groupBox3.Controls.Add(this.lblSimTime);
            this.groupBox3.Controls.Add(this.numExecDelta);
            this.groupBox3.Controls.Add(this.lblExecDelta);
            this.groupBox3.Controls.Add(this.numExecTime);
            this.groupBox3.Controls.Add(this.lblExecTime);
            this.groupBox3.Location = new System.Drawing.Point(8, 144);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(232, 56);
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
            this.numSimDelta.Visible = false;
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
            this.lblSimDelta.Visible = false;
            // 
            // numSimTime
            // 
            this.numSimTime.DecimalPlaces = 2;
            this.numSimTime.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numSimTime.Location = new System.Drawing.Point(152, 24);
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
            this.lblSimTime.Location = new System.Drawing.Point(15, 26);
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
            this.numExecDelta.Location = new System.Drawing.Point(152, 72);
            this.numExecDelta.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            65536});
            this.numExecDelta.Name = "numExecDelta";
            this.numExecDelta.Size = new System.Drawing.Size(72, 20);
            this.numExecDelta.TabIndex = 23;
            this.numExecDelta.Visible = false;
            this.numExecDelta.ValueChanged += new System.EventHandler(this.numExecDelta_ValueChanged);
            // 
            // lblExecDelta
            // 
            this.lblExecDelta.AutoSize = true;
            this.lblExecDelta.Enabled = false;
            this.lblExecDelta.Location = new System.Drawing.Point(15, 74);
            this.lblExecDelta.Name = "lblExecDelta";
            this.lblExecDelta.Size = new System.Drawing.Size(59, 13);
            this.lblExecDelta.TabIndex = 22;
            this.lblExecDelta.Text = "Exec Delta";
            this.lblExecDelta.Visible = false;
            // 
            // numExecTime
            // 
            this.numExecTime.DecimalPlaces = 2;
            this.numExecTime.Enabled = false;
            this.numExecTime.Location = new System.Drawing.Point(152, 48);
            this.numExecTime.Maximum = new decimal(new int[] {
            18000,
            0,
            0,
            0});
            this.numExecTime.Name = "numExecTime";
            this.numExecTime.Size = new System.Drawing.Size(72, 20);
            this.numExecTime.TabIndex = 21;
            this.numExecTime.Visible = false;
            this.numExecTime.ValueChanged += new System.EventHandler(this.numExecTime_ValueChanged);
            // 
            // lblExecTime
            // 
            this.lblExecTime.AutoSize = true;
            this.lblExecTime.Enabled = false;
            this.lblExecTime.Location = new System.Drawing.Point(15, 50);
            this.lblExecTime.Name = "lblExecTime";
            this.lblExecTime.Size = new System.Drawing.Size(57, 13);
            this.lblExecTime.TabIndex = 20;
            this.lblExecTime.Text = "Exec Time";
            this.lblExecTime.Visible = false;
            // 
            // gbxSOBParams
            // 
            this.gbxSOBParams.Controls.Add(this.numSOBClip);
            this.gbxSOBParams.Controls.Add(this.lblSOBClip);
            this.gbxSOBParams.Controls.Add(this.numSOBCloak);
            this.gbxSOBParams.Controls.Add(this.lblSOBCloak);
            this.gbxSOBParams.Controls.Add(this.numSOBAlpha);
            this.gbxSOBParams.Controls.Add(this.lblSOBAlpha);
            this.gbxSOBParams.Location = new System.Drawing.Point(8, 208);
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
            this.numClipDist.Location = new System.Drawing.Point(160, 112);
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
            this.lblClipDist.Location = new System.Drawing.Point(15, 114);
            this.lblClipDist.Name = "lblClipDist";
            this.lblClipDist.Size = new System.Drawing.Size(69, 13);
            this.lblClipDist.TabIndex = 26;
            this.lblClipDist.Text = "Clip Distance";
            // 
            // btnEnterHyperspace
            // 
            this.btnEnterHyperspace.Location = new System.Drawing.Point(16, 536);
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
            this.btnExitHyperspace.Location = new System.Drawing.Point(128, 536);
            this.btnExitHyperspace.Name = "btnExitHyperspace";
            this.btnExitHyperspace.Size = new System.Drawing.Size(104, 24);
            this.btnExitHyperspace.TabIndex = 29;
            this.btnExitHyperspace.Text = "Exit Hyperspace";
            this.btnExitHyperspace.UseVisualStyleBackColor = true;
            this.btnExitHyperspace.Visible = false;
            this.btnExitHyperspace.Click += new System.EventHandler(this.btnExitHyperspace_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numDeathRatio);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.numLifeAlpha);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(8, 312);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(232, 80);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Life Params";
            // 
            // numDeathRatio
            // 
            this.numDeathRatio.DecimalPlaces = 2;
            this.numDeathRatio.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numDeathRatio.Location = new System.Drawing.Point(152, 48);
            this.numDeathRatio.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numDeathRatio.Name = "numDeathRatio";
            this.numDeathRatio.Size = new System.Drawing.Size(72, 20);
            this.numDeathRatio.TabIndex = 23;
            this.numDeathRatio.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numDeathRatio.ValueChanged += new System.EventHandler(this.numDeathRatio_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Death Ratio";
            // 
            // numLifeAlpha
            // 
            this.numLifeAlpha.DecimalPlaces = 2;
            this.numLifeAlpha.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numLifeAlpha.Location = new System.Drawing.Point(152, 24);
            this.numLifeAlpha.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numLifeAlpha.Name = "numLifeAlpha";
            this.numLifeAlpha.Size = new System.Drawing.Size(72, 20);
            this.numLifeAlpha.TabIndex = 21;
            this.numLifeAlpha.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numLifeAlpha.ValueChanged += new System.EventHandler(this.numLifeAlpha_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Life Alpha";
            // 
            // groupBox4
            // 
            this.groupBox4.AutoSize = true;
            this.groupBox4.Controls.Add(this.numPeakScar);
            this.groupBox4.Controls.Add(this.numPeakFren);
            this.groupBox4.Controls.Add(this.numPeakPaint);
            this.groupBox4.Controls.Add(this.numPeakBase);
            this.groupBox4.Controls.Add(this.numPaintDim);
            this.groupBox4.Controls.Add(this.numPaintBias);
            this.groupBox4.Controls.Add(this.numPaintScale);
            this.groupBox4.Controls.Add(this.numPaintCurve);
            this.groupBox4.Controls.Add(this.numFrenCurve);
            this.groupBox4.Controls.Add(this.numFrenBias);
            this.groupBox4.Controls.Add(this.numFrenPower);
            this.groupBox4.Controls.Add(this.numReflAddMix);
            this.groupBox4.Controls.Add(this.numReflFren);
            this.groupBox4.Controls.Add(this.numReflPower);
            this.groupBox4.Controls.Add(this.numGlossBias);
            this.groupBox4.Controls.Add(this.numGlossScale);
            this.groupBox4.Controls.Add(this.numGlossCurve);
            this.groupBox4.Controls.Add(this.numSpecFren);
            this.groupBox4.Controls.Add(this.numSpecPower);
            this.groupBox4.Controls.Add(this.numGlowFren);
            this.groupBox4.Controls.Add(this.numGlowPower);
            this.groupBox4.Controls.Add(this.numDiffFren);
            this.groupBox4.Location = new System.Drawing.Point(248, 8);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(8);
            this.groupBox4.Size = new System.Drawing.Size(192, 557);
            this.groupBox4.TabIndex = 31;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Surface Variables";
            // 
            // numPeakScar
            // 
            this.numPeakScar.DecimalPlaces = 2;
            this.numPeakScar.Dock = System.Windows.Forms.DockStyle.Top;
            this.numPeakScar.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numPeakScar.Location = new System.Drawing.Point(8, 525);
            this.numPeakScar.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numPeakScar.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.numPeakScar.Name = "numPeakScar";
            this.numPeakScar.NumericUpDownWidth = 72;
            this.numPeakScar.Size = new System.Drawing.Size(176, 24);
            this.numPeakScar.TabIndex = 28;
            this.numPeakScar.Text = "Scar Peak";
            this.numPeakScar.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numPeakScar.ValueChanged += new System.EventHandler(this.numPeakScar_ValueChanged);
            // 
            // numPeakFren
            // 
            this.numPeakFren.DecimalPlaces = 2;
            this.numPeakFren.Dock = System.Windows.Forms.DockStyle.Top;
            this.numPeakFren.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numPeakFren.Location = new System.Drawing.Point(8, 501);
            this.numPeakFren.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numPeakFren.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.numPeakFren.Name = "numPeakFren";
            this.numPeakFren.NumericUpDownWidth = 72;
            this.numPeakFren.Size = new System.Drawing.Size(176, 24);
            this.numPeakFren.TabIndex = 27;
            this.numPeakFren.Text = "Fresnel Peak";
            this.numPeakFren.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numPeakFren.ValueChanged += new System.EventHandler(this.numPeakFren_ValueChanged);
            // 
            // numPeakPaint
            // 
            this.numPeakPaint.DecimalPlaces = 2;
            this.numPeakPaint.Dock = System.Windows.Forms.DockStyle.Top;
            this.numPeakPaint.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numPeakPaint.Location = new System.Drawing.Point(8, 477);
            this.numPeakPaint.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numPeakPaint.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.numPeakPaint.Name = "numPeakPaint";
            this.numPeakPaint.NumericUpDownWidth = 72;
            this.numPeakPaint.Size = new System.Drawing.Size(176, 24);
            this.numPeakPaint.TabIndex = 26;
            this.numPeakPaint.Text = "Paint Peak";
            this.numPeakPaint.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numPeakPaint.ValueChanged += new System.EventHandler(this.numPeakPaint_ValueChanged);
            // 
            // numPeakBase
            // 
            this.numPeakBase.DecimalPlaces = 2;
            this.numPeakBase.Dock = System.Windows.Forms.DockStyle.Top;
            this.numPeakBase.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numPeakBase.Location = new System.Drawing.Point(8, 453);
            this.numPeakBase.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numPeakBase.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.numPeakBase.Name = "numPeakBase";
            this.numPeakBase.NumericUpDownWidth = 72;
            this.numPeakBase.Size = new System.Drawing.Size(176, 24);
            this.numPeakBase.TabIndex = 25;
            this.numPeakBase.Text = "Base Peak";
            this.numPeakBase.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numPeakBase.ValueChanged += new System.EventHandler(this.numPeakBase_ValueChanged);
            // 
            // numPaintDim
            // 
            this.numPaintDim.DecimalPlaces = 2;
            this.numPaintDim.Dock = System.Windows.Forms.DockStyle.Top;
            this.numPaintDim.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numPaintDim.Location = new System.Drawing.Point(8, 429);
            this.numPaintDim.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numPaintDim.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.numPaintDim.Name = "numPaintDim";
            this.numPaintDim.NumericUpDownWidth = 72;
            this.numPaintDim.Size = new System.Drawing.Size(176, 24);
            this.numPaintDim.TabIndex = 24;
            this.numPaintDim.Text = "Paint Dim";
            this.numPaintDim.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numPaintDim.ValueChanged += new System.EventHandler(this.numPaintDim_ValueChanged);
            // 
            // numPaintBias
            // 
            this.numPaintBias.DecimalPlaces = 2;
            this.numPaintBias.Dock = System.Windows.Forms.DockStyle.Top;
            this.numPaintBias.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numPaintBias.Location = new System.Drawing.Point(8, 405);
            this.numPaintBias.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numPaintBias.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.numPaintBias.Name = "numPaintBias";
            this.numPaintBias.NumericUpDownWidth = 72;
            this.numPaintBias.Size = new System.Drawing.Size(176, 24);
            this.numPaintBias.TabIndex = 23;
            this.numPaintBias.Text = "Paint Offset";
            this.numPaintBias.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numPaintBias.ValueChanged += new System.EventHandler(this.numPaintOffset_ValueChanged);
            // 
            // numPaintScale
            // 
            this.numPaintScale.DecimalPlaces = 2;
            this.numPaintScale.Dock = System.Windows.Forms.DockStyle.Top;
            this.numPaintScale.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numPaintScale.Location = new System.Drawing.Point(8, 381);
            this.numPaintScale.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numPaintScale.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.numPaintScale.Name = "numPaintScale";
            this.numPaintScale.NumericUpDownWidth = 72;
            this.numPaintScale.Size = new System.Drawing.Size(176, 24);
            this.numPaintScale.TabIndex = 22;
            this.numPaintScale.Text = "Paint Scale";
            this.numPaintScale.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numPaintScale.ValueChanged += new System.EventHandler(this.numPaintScale_ValueChanged);
            // 
            // numPaintCurve
            // 
            this.numPaintCurve.DecimalPlaces = 2;
            this.numPaintCurve.Dock = System.Windows.Forms.DockStyle.Top;
            this.numPaintCurve.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numPaintCurve.Location = new System.Drawing.Point(8, 357);
            this.numPaintCurve.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numPaintCurve.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.numPaintCurve.Name = "numPaintCurve";
            this.numPaintCurve.NumericUpDownWidth = 72;
            this.numPaintCurve.Size = new System.Drawing.Size(176, 24);
            this.numPaintCurve.TabIndex = 21;
            this.numPaintCurve.Text = "Paint Curve";
            this.numPaintCurve.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numPaintCurve.ValueChanged += new System.EventHandler(this.numPaintCurve_ValueChanged);
            // 
            // numFrenCurve
            // 
            this.numFrenCurve.DecimalPlaces = 2;
            this.numFrenCurve.Dock = System.Windows.Forms.DockStyle.Top;
            this.numFrenCurve.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numFrenCurve.Location = new System.Drawing.Point(8, 333);
            this.numFrenCurve.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numFrenCurve.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.numFrenCurve.Name = "numFrenCurve";
            this.numFrenCurve.NumericUpDownWidth = 72;
            this.numFrenCurve.Size = new System.Drawing.Size(176, 24);
            this.numFrenCurve.TabIndex = 20;
            this.numFrenCurve.Text = "Fresnel Curve";
            this.numFrenCurve.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numFrenCurve.ValueChanged += new System.EventHandler(this.numFrenCurve_ValueChanged);
            // 
            // numFrenBias
            // 
            this.numFrenBias.DecimalPlaces = 2;
            this.numFrenBias.Dock = System.Windows.Forms.DockStyle.Top;
            this.numFrenBias.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numFrenBias.Location = new System.Drawing.Point(8, 309);
            this.numFrenBias.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numFrenBias.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.numFrenBias.Name = "numFrenBias";
            this.numFrenBias.NumericUpDownWidth = 72;
            this.numFrenBias.Size = new System.Drawing.Size(176, 24);
            this.numFrenBias.TabIndex = 19;
            this.numFrenBias.Text = "Fresnel Offset";
            this.numFrenBias.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numFrenBias.ValueChanged += new System.EventHandler(this.numFrenBias_ValueChanged);
            // 
            // numFrenPower
            // 
            this.numFrenPower.DecimalPlaces = 2;
            this.numFrenPower.Dock = System.Windows.Forms.DockStyle.Top;
            this.numFrenPower.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numFrenPower.Location = new System.Drawing.Point(8, 285);
            this.numFrenPower.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numFrenPower.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.numFrenPower.Name = "numFrenPower";
            this.numFrenPower.NumericUpDownWidth = 72;
            this.numFrenPower.Size = new System.Drawing.Size(176, 24);
            this.numFrenPower.TabIndex = 18;
            this.numFrenPower.Text = "Fresnel Power";
            this.numFrenPower.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numFrenPower.ValueChanged += new System.EventHandler(this.numFrenPower_ValueChanged);
            // 
            // numReflAddMix
            // 
            this.numReflAddMix.DecimalPlaces = 2;
            this.numReflAddMix.Dock = System.Windows.Forms.DockStyle.Top;
            this.numReflAddMix.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numReflAddMix.Location = new System.Drawing.Point(8, 261);
            this.numReflAddMix.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numReflAddMix.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.numReflAddMix.Name = "numReflAddMix";
            this.numReflAddMix.NumericUpDownWidth = 72;
            this.numReflAddMix.Size = new System.Drawing.Size(176, 24);
            this.numReflAddMix.TabIndex = 17;
            this.numReflAddMix.Text = "Reflective Add Mix";
            this.numReflAddMix.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numReflAddMix.ValueChanged += new System.EventHandler(this.numReflAddMix_ValueChanged);
            // 
            // numReflFren
            // 
            this.numReflFren.DecimalPlaces = 2;
            this.numReflFren.Dock = System.Windows.Forms.DockStyle.Top;
            this.numReflFren.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numReflFren.Location = new System.Drawing.Point(8, 237);
            this.numReflFren.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numReflFren.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.numReflFren.Name = "numReflFren";
            this.numReflFren.NumericUpDownWidth = 72;
            this.numReflFren.Size = new System.Drawing.Size(176, 24);
            this.numReflFren.TabIndex = 16;
            this.numReflFren.Text = "Reflective Fresnel";
            this.numReflFren.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numReflFren.ValueChanged += new System.EventHandler(this.numReflFren_ValueChanged);
            // 
            // numReflPower
            // 
            this.numReflPower.DecimalPlaces = 2;
            this.numReflPower.Dock = System.Windows.Forms.DockStyle.Top;
            this.numReflPower.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numReflPower.Location = new System.Drawing.Point(8, 213);
            this.numReflPower.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numReflPower.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.numReflPower.Name = "numReflPower";
            this.numReflPower.NumericUpDownWidth = 72;
            this.numReflPower.Size = new System.Drawing.Size(176, 24);
            this.numReflPower.TabIndex = 15;
            this.numReflPower.Text = "Reflective Power";
            this.numReflPower.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numReflPower.ValueChanged += new System.EventHandler(this.numReflPower_ValueChanged);
            // 
            // numGlossBias
            // 
            this.numGlossBias.DecimalPlaces = 2;
            this.numGlossBias.Dock = System.Windows.Forms.DockStyle.Top;
            this.numGlossBias.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numGlossBias.Location = new System.Drawing.Point(8, 189);
            this.numGlossBias.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numGlossBias.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.numGlossBias.Name = "numGlossBias";
            this.numGlossBias.NumericUpDownWidth = 72;
            this.numGlossBias.Size = new System.Drawing.Size(176, 24);
            this.numGlossBias.TabIndex = 14;
            this.numGlossBias.Text = "Gloss Offset";
            this.numGlossBias.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numGlossBias.ValueChanged += new System.EventHandler(this.numGlossBias_ValueChanged);
            // 
            // numGlossScale
            // 
            this.numGlossScale.DecimalPlaces = 2;
            this.numGlossScale.Dock = System.Windows.Forms.DockStyle.Top;
            this.numGlossScale.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numGlossScale.Location = new System.Drawing.Point(8, 165);
            this.numGlossScale.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numGlossScale.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.numGlossScale.Name = "numGlossScale";
            this.numGlossScale.NumericUpDownWidth = 72;
            this.numGlossScale.Size = new System.Drawing.Size(176, 24);
            this.numGlossScale.TabIndex = 13;
            this.numGlossScale.Text = "Gloss Scale";
            this.numGlossScale.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numGlossScale.ValueChanged += new System.EventHandler(this.numGlossScale_ValueChanged);
            // 
            // numGlossCurve
            // 
            this.numGlossCurve.DecimalPlaces = 2;
            this.numGlossCurve.Dock = System.Windows.Forms.DockStyle.Top;
            this.numGlossCurve.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numGlossCurve.Location = new System.Drawing.Point(8, 141);
            this.numGlossCurve.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numGlossCurve.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.numGlossCurve.Name = "numGlossCurve";
            this.numGlossCurve.NumericUpDownWidth = 72;
            this.numGlossCurve.Size = new System.Drawing.Size(176, 24);
            this.numGlossCurve.TabIndex = 12;
            this.numGlossCurve.Text = "Gloss Curve";
            this.numGlossCurve.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numGlossCurve.ValueChanged += new System.EventHandler(this.numGlossCurve_ValueChanged);
            // 
            // numSpecFren
            // 
            this.numSpecFren.DecimalPlaces = 2;
            this.numSpecFren.Dock = System.Windows.Forms.DockStyle.Top;
            this.numSpecFren.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numSpecFren.Location = new System.Drawing.Point(8, 117);
            this.numSpecFren.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numSpecFren.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.numSpecFren.Name = "numSpecFren";
            this.numSpecFren.NumericUpDownWidth = 72;
            this.numSpecFren.Size = new System.Drawing.Size(176, 24);
            this.numSpecFren.TabIndex = 11;
            this.numSpecFren.Text = "Specular Fresnel";
            this.numSpecFren.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numSpecFren.ValueChanged += new System.EventHandler(this.numSpecFren_ValueChanged);
            // 
            // numSpecPower
            // 
            this.numSpecPower.DecimalPlaces = 2;
            this.numSpecPower.Dock = System.Windows.Forms.DockStyle.Top;
            this.numSpecPower.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numSpecPower.Location = new System.Drawing.Point(8, 93);
            this.numSpecPower.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numSpecPower.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.numSpecPower.Name = "numSpecPower";
            this.numSpecPower.NumericUpDownWidth = 72;
            this.numSpecPower.Size = new System.Drawing.Size(176, 24);
            this.numSpecPower.TabIndex = 10;
            this.numSpecPower.Text = "Specular Power";
            this.numSpecPower.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numSpecPower.ValueChanged += new System.EventHandler(this.numSpecPower_ValueChanged);
            // 
            // numGlowFren
            // 
            this.numGlowFren.DecimalPlaces = 2;
            this.numGlowFren.Dock = System.Windows.Forms.DockStyle.Top;
            this.numGlowFren.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numGlowFren.Location = new System.Drawing.Point(8, 69);
            this.numGlowFren.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numGlowFren.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.numGlowFren.Name = "numGlowFren";
            this.numGlowFren.NumericUpDownWidth = 72;
            this.numGlowFren.Size = new System.Drawing.Size(176, 24);
            this.numGlowFren.TabIndex = 9;
            this.numGlowFren.Text = "Glow Fresnel";
            this.numGlowFren.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numGlowFren.ValueChanged += new System.EventHandler(this.numGlowFren_ValueChanged);
            // 
            // numGlowPower
            // 
            this.numGlowPower.DecimalPlaces = 2;
            this.numGlowPower.Dock = System.Windows.Forms.DockStyle.Top;
            this.numGlowPower.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numGlowPower.Location = new System.Drawing.Point(8, 45);
            this.numGlowPower.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numGlowPower.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.numGlowPower.Name = "numGlowPower";
            this.numGlowPower.NumericUpDownWidth = 72;
            this.numGlowPower.Size = new System.Drawing.Size(176, 24);
            this.numGlowPower.TabIndex = 8;
            this.numGlowPower.Text = "Glow Power";
            this.numGlowPower.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numGlowPower.ValueChanged += new System.EventHandler(this.numGlowPower_ValueChanged);
            // 
            // numDiffFren
            // 
            this.numDiffFren.DecimalPlaces = 2;
            this.numDiffFren.Dock = System.Windows.Forms.DockStyle.Top;
            this.numDiffFren.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numDiffFren.Location = new System.Drawing.Point(8, 21);
            this.numDiffFren.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numDiffFren.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.numDiffFren.Name = "numDiffFren";
            this.numDiffFren.NumericUpDownWidth = 72;
            this.numDiffFren.Size = new System.Drawing.Size(176, 24);
            this.numDiffFren.TabIndex = 7;
            this.numDiffFren.Text = "Diff Fresnel";
            this.numDiffFren.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numDiffFren.ValueChanged += new System.EventHandler(this.numDiffFren_ValueChanged);
            // 
            // ShaderSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(448, 572);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
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
            this.Text = "Shader Settings";
            this.TopMost = true;
            this.gbxPaintStyle.ResumeLayout(false);
            this.gbxPaintStyle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPaintStyleOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPaintStyleScale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPaintStyleCurve)).EndInit();
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
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDeathRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLifeAlpha)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.OpenFileDialog addDataPathDialog;
        private System.Windows.Forms.GroupBox gbxPaintStyle;
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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numDeathRatio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numLifeAlpha;
        private System.Windows.Forms.Label label3;
        private LabeledNumericUpDown numPeakScar;
        private LabeledNumericUpDown numPeakFren;
        private LabeledNumericUpDown numPeakPaint;
        private LabeledNumericUpDown numPeakBase;
        private LabeledNumericUpDown numPaintDim;
        private LabeledNumericUpDown numPaintBias;
        private LabeledNumericUpDown numPaintScale;
        private LabeledNumericUpDown numPaintCurve;
        private LabeledNumericUpDown numFrenCurve;
        private LabeledNumericUpDown numFrenBias;
        private LabeledNumericUpDown numFrenPower;
        private LabeledNumericUpDown numReflAddMix;
        private LabeledNumericUpDown numReflFren;
        private LabeledNumericUpDown numReflPower;
        private LabeledNumericUpDown numDiffFren;
        private LabeledNumericUpDown numGlowPower;
        private LabeledNumericUpDown numGlowFren;
        private LabeledNumericUpDown numSpecPower;
        private LabeledNumericUpDown numSpecFren;
        private LabeledNumericUpDown numGlossCurve;
        private LabeledNumericUpDown numGlossScale;
        private LabeledNumericUpDown numGlossBias;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.NumericUpDown numPaintStyleOffset;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numPaintStyleScale;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numPaintStyleCurve;
        private System.Windows.Forms.Label label1;
    }
}