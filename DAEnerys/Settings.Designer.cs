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
            Program.settings = null;
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
            this.buttonTeamColorSwap = new System.Windows.Forms.Button();
            this.buttonStripeColor = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonTeamColor = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.checkDisableLighting = new System.Windows.Forms.CheckBox();
            this.checkVSync = new System.Windows.Forms.CheckBox();
            this.checkRenderOnTop = new System.Windows.Forms.CheckBox();
            this.labelFSAASamples = new System.Windows.Forms.Label();
            this.comboFSAASamples = new System.Windows.Forms.ComboBox();
            this.teamColorButton20 = new DAEnerys.TeamColorButton();
            this.teamColorButton19 = new DAEnerys.TeamColorButton();
            this.teamColorButton18 = new DAEnerys.TeamColorButton();
            this.teamColorButton17 = new DAEnerys.TeamColorButton();
            this.teamColorButton16 = new DAEnerys.TeamColorButton();
            this.teamColorButton15 = new DAEnerys.TeamColorButton();
            this.teamColorButton14 = new DAEnerys.TeamColorButton();
            this.teamColorButton13 = new DAEnerys.TeamColorButton();
            this.teamColorButton12 = new DAEnerys.TeamColorButton();
            this.teamColorButton11 = new DAEnerys.TeamColorButton();
            this.teamColorButtonCustom = new DAEnerys.TeamColorButton();
            this.teamColorButtonDefault = new DAEnerys.TeamColorButton();
            this.teamColorButton10 = new DAEnerys.TeamColorButton();
            this.teamColorButton9 = new DAEnerys.TeamColorButton();
            this.teamColorButton8 = new DAEnerys.TeamColorButton();
            this.teamColorButton7 = new DAEnerys.TeamColorButton();
            this.teamColorButton6 = new DAEnerys.TeamColorButton();
            this.teamColorButton5 = new DAEnerys.TeamColorButton();
            this.teamColorButton4 = new DAEnerys.TeamColorButton();
            this.teamColorButton3 = new DAEnerys.TeamColorButton();
            this.teamColorButton2 = new DAEnerys.TeamColorButton();
            this.teamColorButton1 = new DAEnerys.TeamColorButton();
            this.teamColorButton0 = new DAEnerys.TeamColorButton();
            ((System.ComponentModel.ISupportInitialize)(this.numericFarClip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericZoomSpeed)).BeginInit();
            this.groupCamera.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericFOV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericNearClip)).BeginInit();
            this.groupEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericIconSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMarkerSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericJointSize)).BeginInit();
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
            this.groupCamera.Location = new System.Drawing.Point(8, 8);
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
            this.groupEditor.Location = new System.Drawing.Point(8, 152);
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
            // buttonBackgroundColor
            // 
            this.buttonBackgroundColor.BackColor = System.Drawing.Color.Red;
            this.buttonBackgroundColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBackgroundColor.Location = new System.Drawing.Point(104, 144);
            this.buttonBackgroundColor.Name = "buttonBackgroundColor";
            this.buttonBackgroundColor.Size = new System.Drawing.Size(232, 20);
            this.buttonBackgroundColor.TabIndex = 4;
            this.buttonBackgroundColor.UseVisualStyleBackColor = false;
            this.buttonBackgroundColor.Click += new System.EventHandler(this.buttonBackgroundColor_Click);
            // 
            // labelBackgroundColor
            // 
            this.labelBackgroundColor.AutoSize = true;
            this.labelBackgroundColor.Location = new System.Drawing.Point(7, 148);
            this.labelBackgroundColor.Name = "labelBackgroundColor";
            this.labelBackgroundColor.Size = new System.Drawing.Size(91, 13);
            this.labelBackgroundColor.TabIndex = 3;
            this.labelBackgroundColor.Text = "Background color";
            // 
            // buttonAmbientColor
            // 
            this.buttonAmbientColor.BackColor = System.Drawing.Color.Red;
            this.buttonAmbientColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAmbientColor.Location = new System.Drawing.Point(104, 120);
            this.buttonAmbientColor.Name = "buttonAmbientColor";
            this.buttonAmbientColor.Size = new System.Drawing.Size(232, 20);
            this.buttonAmbientColor.TabIndex = 2;
            this.buttonAmbientColor.UseVisualStyleBackColor = false;
            this.buttonAmbientColor.Click += new System.EventHandler(this.buttonAmbientColor_Click);
            // 
            // labelAmbientColor
            // 
            this.labelAmbientColor.AutoSize = true;
            this.labelAmbientColor.Location = new System.Drawing.Point(7, 124);
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
            this.groupDataPaths.Location = new System.Drawing.Point(8, 272);
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
            this.groupRendering.Controls.Add(this.teamColorButton20);
            this.groupRendering.Controls.Add(this.teamColorButton19);
            this.groupRendering.Controls.Add(this.teamColorButton18);
            this.groupRendering.Controls.Add(this.teamColorButton17);
            this.groupRendering.Controls.Add(this.teamColorButton16);
            this.groupRendering.Controls.Add(this.teamColorButton15);
            this.groupRendering.Controls.Add(this.teamColorButton14);
            this.groupRendering.Controls.Add(this.teamColorButton13);
            this.groupRendering.Controls.Add(this.teamColorButton12);
            this.groupRendering.Controls.Add(this.teamColorButton11);
            this.groupRendering.Controls.Add(this.buttonTeamColorSwap);
            this.groupRendering.Controls.Add(this.teamColorButtonCustom);
            this.groupRendering.Controls.Add(this.teamColorButtonDefault);
            this.groupRendering.Controls.Add(this.teamColorButton10);
            this.groupRendering.Controls.Add(this.teamColorButton9);
            this.groupRendering.Controls.Add(this.teamColorButton8);
            this.groupRendering.Controls.Add(this.teamColorButton7);
            this.groupRendering.Controls.Add(this.teamColorButton6);
            this.groupRendering.Controls.Add(this.teamColorButton5);
            this.groupRendering.Controls.Add(this.teamColorButton4);
            this.groupRendering.Controls.Add(this.teamColorButton3);
            this.groupRendering.Controls.Add(this.teamColorButton2);
            this.groupRendering.Controls.Add(this.teamColorButton1);
            this.groupRendering.Controls.Add(this.buttonStripeColor);
            this.groupRendering.Controls.Add(this.label2);
            this.groupRendering.Controls.Add(this.buttonTeamColor);
            this.groupRendering.Controls.Add(this.teamColorButton0);
            this.groupRendering.Controls.Add(this.label3);
            this.groupRendering.Controls.Add(this.buttonBackgroundColor);
            this.groupRendering.Controls.Add(this.checkDisableLighting);
            this.groupRendering.Controls.Add(this.labelBackgroundColor);
            this.groupRendering.Controls.Add(this.checkVSync);
            this.groupRendering.Controls.Add(this.buttonAmbientColor);
            this.groupRendering.Controls.Add(this.labelAmbientColor);
            this.groupRendering.Controls.Add(this.checkRenderOnTop);
            this.groupRendering.Controls.Add(this.labelFSAASamples);
            this.groupRendering.Controls.Add(this.comboFSAASamples);
            this.groupRendering.Location = new System.Drawing.Point(352, 8);
            this.groupRendering.Name = "groupRendering";
            this.groupRendering.Size = new System.Drawing.Size(343, 303);
            this.groupRendering.TabIndex = 18;
            this.groupRendering.TabStop = false;
            this.groupRendering.Text = "Rendering";
            // 
            // buttonTeamColorSwap
            // 
            this.buttonTeamColorSwap.Location = new System.Drawing.Point(271, 215);
            this.buttonTeamColorSwap.Name = "buttonTeamColorSwap";
            this.buttonTeamColorSwap.Size = new System.Drawing.Size(66, 22);
            this.buttonTeamColorSwap.TabIndex = 113;
            this.buttonTeamColorSwap.Text = "Swap";
            this.buttonTeamColorSwap.UseVisualStyleBackColor = true;
            this.buttonTeamColorSwap.Click += new System.EventHandler(this.buttonTeamColorSwap_Click);
            // 
            // buttonStripeColor
            // 
            this.buttonStripeColor.BackColor = System.Drawing.Color.Red;
            this.buttonStripeColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStripeColor.Location = new System.Drawing.Point(104, 192);
            this.buttonStripeColor.Name = "buttonStripeColor";
            this.buttonStripeColor.Size = new System.Drawing.Size(232, 20);
            this.buttonStripeColor.TabIndex = 29;
            this.buttonStripeColor.UseVisualStyleBackColor = false;
            this.buttonStripeColor.Click += new System.EventHandler(this.buttonStripeColor_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 196);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 28;
            this.label2.Text = "Stripe color";
            // 
            // buttonTeamColor
            // 
            this.buttonTeamColor.BackColor = System.Drawing.Color.Red;
            this.buttonTeamColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonTeamColor.Location = new System.Drawing.Point(104, 168);
            this.buttonTeamColor.Name = "buttonTeamColor";
            this.buttonTeamColor.Size = new System.Drawing.Size(232, 20);
            this.buttonTeamColor.TabIndex = 27;
            this.buttonTeamColor.UseVisualStyleBackColor = false;
            this.buttonTeamColor.Click += new System.EventHandler(this.buttonTeamColor_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 172);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "Team color";
            // 
            // checkDisableLighting
            // 
            this.checkDisableLighting.AutoSize = true;
            this.checkDisableLighting.Location = new System.Drawing.Point(11, 92);
            this.checkDisableLighting.Name = "checkDisableLighting";
            this.checkDisableLighting.Size = new System.Drawing.Size(97, 17);
            this.checkDisableLighting.TabIndex = 25;
            this.checkDisableLighting.Text = "Disable lighting";
            this.checkDisableLighting.UseVisualStyleBackColor = true;
            this.checkDisableLighting.CheckedChanged += new System.EventHandler(this.checkDisableLighting_CheckedChanged);
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
            this.comboFSAASamples.Size = new System.Drawing.Size(232, 21);
            this.comboFSAASamples.TabIndex = 19;
            // 
            // teamColorButton20
            // 
            this.teamColorButton20.BackColor = System.Drawing.Color.DarkGray;
            this.teamColorButton20.Location = new System.Drawing.Point(200, 264);
            this.teamColorButton20.Name = "teamColorButton20";
            this.teamColorButton20.Size = new System.Drawing.Size(20, 20);
            this.teamColorButton20.StripeColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(178)))), ((int)(((byte)(153)))));
            this.teamColorButton20.TabIndex = 123;
            this.teamColorButton20.TeamColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.teamColorButton20.UsePreset = DAEnerys.TeamColorButton.PresetColors.MP7;
            this.teamColorButton20.UseVisualStyleBackColor = false;
            this.teamColorButton20.Click += new System.EventHandler(this.buttonTeamColorPreset_Click);
            // 
            // teamColorButton19
            // 
            this.teamColorButton19.BackColor = System.Drawing.Color.DarkGray;
            this.teamColorButton19.Location = new System.Drawing.Point(176, 264);
            this.teamColorButton19.Name = "teamColorButton19";
            this.teamColorButton19.Size = new System.Drawing.Size(20, 20);
            this.teamColorButton19.StripeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.teamColorButton19.TabIndex = 122;
            this.teamColorButton19.TeamColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(127)))));
            this.teamColorButton19.UsePreset = DAEnerys.TeamColorButton.PresetColors.MP6;
            this.teamColorButton19.UseVisualStyleBackColor = false;
            this.teamColorButton19.Click += new System.EventHandler(this.buttonTeamColorPreset_Click);
            // 
            // teamColorButton18
            // 
            this.teamColorButton18.BackColor = System.Drawing.Color.DarkGray;
            this.teamColorButton18.Location = new System.Drawing.Point(152, 264);
            this.teamColorButton18.Name = "teamColorButton18";
            this.teamColorButton18.Size = new System.Drawing.Size(20, 20);
            this.teamColorButton18.StripeColor = System.Drawing.Color.Yellow;
            this.teamColorButton18.TabIndex = 121;
            this.teamColorButton18.TeamColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(129)))), ((int)(((byte)(182)))));
            this.teamColorButton18.UsePreset = DAEnerys.TeamColorButton.PresetColors.MP5;
            this.teamColorButton18.UseVisualStyleBackColor = false;
            this.teamColorButton18.Click += new System.EventHandler(this.buttonTeamColorPreset_Click);
            // 
            // teamColorButton17
            // 
            this.teamColorButton17.BackColor = System.Drawing.Color.DarkGray;
            this.teamColorButton17.Location = new System.Drawing.Point(128, 264);
            this.teamColorButton17.Name = "teamColorButton17";
            this.teamColorButton17.Size = new System.Drawing.Size(20, 20);
            this.teamColorButton17.StripeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(211)))), ((int)(((byte)(0)))));
            this.teamColorButton17.TabIndex = 120;
            this.teamColorButton17.TeamColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.teamColorButton17.UsePreset = DAEnerys.TeamColorButton.PresetColors.MP4;
            this.teamColorButton17.UseVisualStyleBackColor = false;
            this.teamColorButton17.Click += new System.EventHandler(this.buttonTeamColorPreset_Click);
            // 
            // teamColorButton16
            // 
            this.teamColorButton16.BackColor = System.Drawing.Color.DarkGray;
            this.teamColorButton16.Location = new System.Drawing.Point(104, 264);
            this.teamColorButton16.Name = "teamColorButton16";
            this.teamColorButton16.Size = new System.Drawing.Size(20, 20);
            this.teamColorButton16.StripeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.teamColorButton16.TabIndex = 119;
            this.teamColorButton16.TeamColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(255)))), ((int)(((byte)(233)))));
            this.teamColorButton16.UsePreset = DAEnerys.TeamColorButton.PresetColors.MP3;
            this.teamColorButton16.UseVisualStyleBackColor = false;
            this.teamColorButton16.Click += new System.EventHandler(this.buttonTeamColorPreset_Click);
            // 
            // teamColorButton15
            // 
            this.teamColorButton15.BackColor = System.Drawing.Color.DarkGray;
            this.teamColorButton15.Location = new System.Drawing.Point(80, 264);
            this.teamColorButton15.Name = "teamColorButton15";
            this.teamColorButton15.Size = new System.Drawing.Size(20, 20);
            this.teamColorButton15.StripeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.teamColorButton15.TabIndex = 118;
            this.teamColorButton15.TeamColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(229)))), ((int)(((byte)(7)))));
            this.teamColorButton15.UsePreset = DAEnerys.TeamColorButton.PresetColors.MP2;
            this.teamColorButton15.UseVisualStyleBackColor = false;
            this.teamColorButton15.Click += new System.EventHandler(this.buttonTeamColorPreset_Click);
            // 
            // teamColorButton14
            // 
            this.teamColorButton14.BackColor = System.Drawing.Color.DarkGray;
            this.teamColorButton14.Location = new System.Drawing.Point(248, 240);
            this.teamColorButton14.Name = "teamColorButton14";
            this.teamColorButton14.Size = new System.Drawing.Size(20, 20);
            this.teamColorButton14.StripeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.teamColorButton14.TabIndex = 117;
            this.teamColorButton14.TeamColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(165)))), ((int)(((byte)(111)))));
            this.teamColorButton14.UsePreset = DAEnerys.TeamColorButton.PresetColors.MP1;
            this.teamColorButton14.UseVisualStyleBackColor = false;
            this.teamColorButton14.Click += new System.EventHandler(this.buttonTeamColorPreset_Click);
            // 
            // teamColorButton13
            // 
            this.teamColorButton13.BackColor = System.Drawing.Color.DarkGray;
            this.teamColorButton13.Location = new System.Drawing.Point(224, 240);
            this.teamColorButton13.Name = "teamColorButton13";
            this.teamColorButton13.Size = new System.Drawing.Size(20, 20);
            this.teamColorButton13.StripeColor = System.Drawing.Color.Yellow;
            this.teamColorButton13.TabIndex = 116;
            this.teamColorButton13.TeamColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(160)))));
            this.teamColorButton13.UsePreset = DAEnerys.TeamColorButton.PresetColors.HiigaranElite;
            this.teamColorButton13.UseVisualStyleBackColor = false;
            this.teamColorButton13.Click += new System.EventHandler(this.buttonTeamColorPreset_Click);
            // 
            // teamColorButton12
            // 
            this.teamColorButton12.BackColor = System.Drawing.Color.DarkGray;
            this.teamColorButton12.Location = new System.Drawing.Point(200, 240);
            this.teamColorButton12.Name = "teamColorButton12";
            this.teamColorButton12.Size = new System.Drawing.Size(20, 20);
            this.teamColorButton12.StripeColor = System.Drawing.Color.Red;
            this.teamColorButton12.TabIndex = 115;
            this.teamColorButton12.TeamColor = System.Drawing.Color.Black;
            this.teamColorButton12.UsePreset = DAEnerys.TeamColorButton.PresetColors.KithSoban;
            this.teamColorButton12.UseVisualStyleBackColor = false;
            this.teamColorButton12.Click += new System.EventHandler(this.buttonTeamColorPreset_Click);
            // 
            // teamColorButton11
            // 
            this.teamColorButton11.BackColor = System.Drawing.Color.DarkGray;
            this.teamColorButton11.Location = new System.Drawing.Point(176, 240);
            this.teamColorButton11.Name = "teamColorButton11";
            this.teamColorButton11.Size = new System.Drawing.Size(20, 20);
            this.teamColorButton11.StripeColor = System.Drawing.Color.White;
            this.teamColorButton11.TabIndex = 114;
            this.teamColorButton11.TeamColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(177)))), ((int)(((byte)(142)))));
            this.teamColorButton11.UsePreset = DAEnerys.TeamColorButton.PresetColors.TanisDefense;
            this.teamColorButton11.UseVisualStyleBackColor = false;
            this.teamColorButton11.Click += new System.EventHandler(this.buttonTeamColorPreset_Click);
            // 
            // teamColorButtonCustom
            // 
            this.teamColorButtonCustom.BackColor = System.Drawing.Color.DarkGray;
            this.teamColorButtonCustom.Location = new System.Drawing.Point(248, 264);
            this.teamColorButtonCustom.Name = "teamColorButtonCustom";
            this.teamColorButtonCustom.Size = new System.Drawing.Size(20, 20);
            this.teamColorButtonCustom.StripeColor = System.Drawing.Color.SpringGreen;
            this.teamColorButtonCustom.TabIndex = 112;
            this.teamColorButtonCustom.TeamColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(127)))), ((int)(((byte)(255)))));
            this.teamColorButtonCustom.UsePreset = DAEnerys.TeamColorButton.PresetColors.Custom;
            this.teamColorButtonCustom.UseVisualStyleBackColor = false;
            this.teamColorButtonCustom.Click += new System.EventHandler(this.buttonTeamColorPreset_Click);
            // 
            // teamColorButtonDefault
            // 
            this.teamColorButtonDefault.BackColor = System.Drawing.Color.DarkGray;
            this.teamColorButtonDefault.Location = new System.Drawing.Point(80, 216);
            this.teamColorButtonDefault.Name = "teamColorButtonDefault";
            this.teamColorButtonDefault.Size = new System.Drawing.Size(20, 20);
            this.teamColorButtonDefault.StripeColor = System.Drawing.Color.SpringGreen;
            this.teamColorButtonDefault.TabIndex = 111;
            this.teamColorButtonDefault.TeamColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(127)))), ((int)(((byte)(255)))));
            this.teamColorButtonDefault.UsePreset = DAEnerys.TeamColorButton.PresetColors.Default;
            this.teamColorButtonDefault.UseVisualStyleBackColor = false;
            this.teamColorButtonDefault.Click += new System.EventHandler(this.buttonTeamColorPreset_Click);
            // 
            // teamColorButton10
            // 
            this.teamColorButton10.BackColor = System.Drawing.Color.DarkGray;
            this.teamColorButton10.Location = new System.Drawing.Point(152, 240);
            this.teamColorButton10.Name = "teamColorButton10";
            this.teamColorButton10.Size = new System.Drawing.Size(20, 20);
            this.teamColorButton10.StripeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.teamColorButton10.TabIndex = 109;
            this.teamColorButton10.TeamColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.teamColorButton10.UsePreset = DAEnerys.TeamColorButton.PresetColors.VaygrSP;
            this.teamColorButton10.UseVisualStyleBackColor = false;
            this.teamColorButton10.Click += new System.EventHandler(this.buttonTeamColorPreset_Click);
            // 
            // teamColorButton9
            // 
            this.teamColorButton9.BackColor = System.Drawing.Color.DarkGray;
            this.teamColorButton9.Location = new System.Drawing.Point(128, 240);
            this.teamColorButton9.Name = "teamColorButton9";
            this.teamColorButton9.Size = new System.Drawing.Size(20, 20);
            this.teamColorButton9.StripeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.teamColorButton9.TabIndex = 108;
            this.teamColorButton9.TeamColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(141)))), ((int)(((byte)(170)))));
            this.teamColorButton9.UsePreset = DAEnerys.TeamColorButton.PresetColors.Hiigaran;
            this.teamColorButton9.UseVisualStyleBackColor = false;
            this.teamColorButton9.Click += new System.EventHandler(this.buttonTeamColorPreset_Click);
            // 
            // teamColorButton8
            // 
            this.teamColorButton8.BackColor = System.Drawing.Color.DarkGray;
            this.teamColorButton8.Location = new System.Drawing.Point(104, 240);
            this.teamColorButton8.Name = "teamColorButton8";
            this.teamColorButton8.Size = new System.Drawing.Size(20, 20);
            this.teamColorButton8.StripeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.teamColorButton8.TabIndex = 107;
            this.teamColorButton8.TeamColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.teamColorButton8.UsePreset = DAEnerys.TeamColorButton.PresetColors.KithSjet;
            this.teamColorButton8.UseVisualStyleBackColor = false;
            this.teamColorButton8.Click += new System.EventHandler(this.buttonTeamColorPreset_Click);
            // 
            // teamColorButton7
            // 
            this.teamColorButton7.BackColor = System.Drawing.Color.DarkGray;
            this.teamColorButton7.Location = new System.Drawing.Point(80, 240);
            this.teamColorButton7.Name = "teamColorButton7";
            this.teamColorButton7.Size = new System.Drawing.Size(20, 20);
            this.teamColorButton7.StripeColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(111)))), ((int)(((byte)(29)))));
            this.teamColorButton7.TabIndex = 106;
            this.teamColorButton7.TeamColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(173)))), ((int)(((byte)(83)))));
            this.teamColorButton7.UsePreset = DAEnerys.TeamColorButton.PresetColors.KithNabaal;
            this.teamColorButton7.UseVisualStyleBackColor = false;
            this.teamColorButton7.Click += new System.EventHandler(this.buttonTeamColorPreset_Click);
            // 
            // teamColorButton6
            // 
            this.teamColorButton6.BackColor = System.Drawing.Color.DarkGray;
            this.teamColorButton6.Location = new System.Drawing.Point(248, 216);
            this.teamColorButton6.Name = "teamColorButton6";
            this.teamColorButton6.Size = new System.Drawing.Size(20, 20);
            this.teamColorButton6.StripeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(247)))), ((int)(((byte)(101)))));
            this.teamColorButton6.TabIndex = 105;
            this.teamColorButton6.TeamColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(151)))), ((int)(((byte)(8)))));
            this.teamColorButton6.UsePreset = DAEnerys.TeamColorButton.PresetColors.KithManaan;
            this.teamColorButton6.UseVisualStyleBackColor = false;
            this.teamColorButton6.Click += new System.EventHandler(this.buttonTeamColorPreset_Click);
            // 
            // teamColorButton5
            // 
            this.teamColorButton5.BackColor = System.Drawing.Color.DarkGray;
            this.teamColorButton5.Location = new System.Drawing.Point(224, 216);
            this.teamColorButton5.Name = "teamColorButton5";
            this.teamColorButton5.Size = new System.Drawing.Size(20, 20);
            this.teamColorButton5.StripeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.teamColorButton5.TabIndex = 104;
            this.teamColorButton5.TeamColor = System.Drawing.Color.White;
            this.teamColorButton5.UsePreset = DAEnerys.TeamColorButton.PresetColors.TaiidanLoyalist;
            this.teamColorButton5.UseVisualStyleBackColor = false;
            this.teamColorButton5.Click += new System.EventHandler(this.buttonTeamColorPreset_Click);
            // 
            // teamColorButton4
            // 
            this.teamColorButton4.BackColor = System.Drawing.Color.DarkGray;
            this.teamColorButton4.Location = new System.Drawing.Point(200, 216);
            this.teamColorButton4.Name = "teamColorButton4";
            this.teamColorButton4.Size = new System.Drawing.Size(20, 20);
            this.teamColorButton4.StripeColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(45)))), ((int)(((byte)(20)))));
            this.teamColorButton4.TabIndex = 103;
            this.teamColorButton4.TeamColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(237)))), ((int)(((byte)(37)))));
            this.teamColorButton4.UsePreset = DAEnerys.TeamColorButton.PresetColors.TaiidanImperialist;
            this.teamColorButton4.UseVisualStyleBackColor = false;
            this.teamColorButton4.Click += new System.EventHandler(this.buttonTeamColorPreset_Click);
            // 
            // teamColorButton3
            // 
            this.teamColorButton3.BackColor = System.Drawing.Color.DarkGray;
            this.teamColorButton3.Location = new System.Drawing.Point(176, 216);
            this.teamColorButton3.Name = "teamColorButton3";
            this.teamColorButton3.Size = new System.Drawing.Size(20, 20);
            this.teamColorButton3.StripeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.teamColorButton3.TabIndex = 102;
            this.teamColorButton3.TeamColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.teamColorButton3.UsePreset = DAEnerys.TeamColorButton.PresetColors.Beast;
            this.teamColorButton3.UseVisualStyleBackColor = false;
            this.teamColorButton3.Click += new System.EventHandler(this.buttonTeamColorPreset_Click);
            // 
            // teamColorButton2
            // 
            this.teamColorButton2.BackColor = System.Drawing.Color.DarkGray;
            this.teamColorButton2.Location = new System.Drawing.Point(152, 216);
            this.teamColorButton2.Name = "teamColorButton2";
            this.teamColorButton2.Size = new System.Drawing.Size(20, 20);
            this.teamColorButton2.StripeColor = System.Drawing.Color.White;
            this.teamColorButton2.TabIndex = 101;
            this.teamColorButton2.TeamColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(108)))), ((int)(((byte)(202)))));
            this.teamColorButton2.UsePreset = DAEnerys.TeamColorButton.PresetColors.Somtaaw;
            this.teamColorButton2.UseVisualStyleBackColor = false;
            this.teamColorButton2.Click += new System.EventHandler(this.buttonTeamColorPreset_Click);
            // 
            // teamColorButton1
            // 
            this.teamColorButton1.BackColor = System.Drawing.Color.DarkGray;
            this.teamColorButton1.Location = new System.Drawing.Point(128, 216);
            this.teamColorButton1.Name = "teamColorButton1";
            this.teamColorButton1.Size = new System.Drawing.Size(20, 20);
            this.teamColorButton1.StripeColor = System.Drawing.Color.Red;
            this.teamColorButton1.TabIndex = 100;
            this.teamColorButton1.TeamColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(210)))), ((int)(((byte)(0)))));
            this.teamColorButton1.UsePreset = DAEnerys.TeamColorButton.PresetColors.Taiidan;
            this.teamColorButton1.UseVisualStyleBackColor = false;
            this.teamColorButton1.Click += new System.EventHandler(this.buttonTeamColorPreset_Click);
            // 
            // teamColorButton0
            // 
            this.teamColorButton0.BackColor = System.Drawing.Color.DarkGray;
            this.teamColorButton0.Location = new System.Drawing.Point(104, 216);
            this.teamColorButton0.Name = "teamColorButton0";
            this.teamColorButton0.Size = new System.Drawing.Size(20, 20);
            this.teamColorButton0.StripeColor = System.Drawing.Color.White;
            this.teamColorButton0.TabIndex = 99;
            this.teamColorButton0.TeamColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(159)))), ((int)(((byte)(161)))));
            this.teamColorButton0.UsePreset = DAEnerys.TeamColorButton.PresetColors.Kushan;
            this.teamColorButton0.UseVisualStyleBackColor = false;
            this.teamColorButton0.Click += new System.EventHandler(this.buttonTeamColorPreset_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(701, 469);
            this.Controls.Add(this.groupRendering);
            this.Controls.Add(this.groupDataPaths);
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
        private System.Windows.Forms.CheckBox checkDisableLighting;
        private System.Windows.Forms.Button buttonStripeColor;
        private System.Windows.Forms.Button buttonTeamColor;
        private TeamColorButton teamColorButton0;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private TeamColorButton teamColorButton10;
        private TeamColorButton teamColorButton9;
        private TeamColorButton teamColorButton8;
        private TeamColorButton teamColorButton7;
        private TeamColorButton teamColorButton6;
        private TeamColorButton teamColorButton5;
        private TeamColorButton teamColorButton4;
        private TeamColorButton teamColorButton3;
        private TeamColorButton teamColorButton2;
        private TeamColorButton teamColorButton1;
        private TeamColorButton teamColorButtonCustom;
        private TeamColorButton teamColorButtonDefault;
        private System.Windows.Forms.Button buttonTeamColorSwap;
        private TeamColorButton teamColorButton11;
        private TeamColorButton teamColorButton15;
        private TeamColorButton teamColorButton14;
        private TeamColorButton teamColorButton13;
        private TeamColorButton teamColorButton12;
        private TeamColorButton teamColorButton20;
        private TeamColorButton teamColorButton19;
        private TeamColorButton teamColorButton18;
        private TeamColorButton teamColorButton17;
        private TeamColorButton teamColorButton16;
    }
}