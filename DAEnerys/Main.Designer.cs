using OpenTK.Graphics;

namespace DAEnerys
{
    partial class Main
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.buttonOpen = new System.Windows.Forms.ToolStripButton();
            this.buttonSettings = new System.Windows.Forms.ToolStripButton();
            this.buttonHotkeys = new System.Windows.Forms.ToolStripButton();
            this.buttonAbout = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.openColladaDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveColladaDialog = new System.Windows.Forms.SaveFileDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabShipMeshes = new System.Windows.Forms.TabPage();
            this.groupShipMeshLODs = new System.Windows.Forms.GroupBox();
            this.listShipMeshLODs = new System.Windows.Forms.CheckedListBox();
            this.panelShipMesh = new System.Windows.Forms.Panel();
            this.checkShipMeshDoScar = new System.Windows.Forms.CheckBox();
            this.listShipMeshes = new System.Windows.Forms.ListBox();
            this.labelShipMeshParent = new System.Windows.Forms.Label();
            this.comboShipMeshParent = new System.Windows.Forms.ComboBox();
            this.tabMaterials = new System.Windows.Forms.TabPage();
            this.groupThrusterStrength = new System.Windows.Forms.GroupBox();
            this.trackBarThrusterStrength = new System.Windows.Forms.TrackBar();
            this.groupMaterialTextures = new System.Windows.Forms.GroupBox();
            this.listMaterialTextures = new System.Windows.Forms.ListBox();
            this.panelMaterialsList = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.comboMaterialFormat = new System.Windows.Forms.ComboBox();
            this.boxMaterialShader = new System.Windows.Forms.TextBox();
            this.listMaterials = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabCollisionMeshes = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.listCollisionMeshes = new System.Windows.Forms.CheckedListBox();
            this.labelCollisionMeshParent = new System.Windows.Forms.Label();
            this.comboCollisionMeshParent = new System.Windows.Forms.ComboBox();
            this.tabJoints = new System.Windows.Forms.TabPage();
            this.jointsTree = new System.Windows.Forms.TreeView();
            this.tabMarkers = new System.Windows.Forms.TabPage();
            this.splitTabMarkers = new System.Windows.Forms.SplitContainer();
            this.listBoxMarkers = new System.Windows.Forms.ListBox();
            this.checkboxDrawMarkers = new System.Windows.Forms.CheckBox();
            this.tabDockpaths = new System.Windows.Forms.TabPage();
            this.groupDockpathFlags = new System.Windows.Forms.GroupBox();
            this.checkDockpathAjar = new System.Windows.Forms.CheckBox();
            this.checkDockpathLatch = new System.Windows.Forms.CheckBox();
            this.checkDockpathAnim = new System.Windows.Forms.CheckBox();
            this.checkDockpathExit = new System.Windows.Forms.CheckBox();
            this.groupDockpathSegments = new System.Windows.Forms.GroupBox();
            this.groupDockpathSegmentFlags = new System.Windows.Forms.GroupBox();
            this.checkDockpathSegmentFlagClip = new System.Windows.Forms.CheckBox();
            this.checkDockpathSegmentFlagUnfocus = new System.Windows.Forms.CheckBox();
            this.checkDockpathSegmentFlagCheck = new System.Windows.Forms.CheckBox();
            this.checkDockpathSegmentFlagClearRes = new System.Windows.Forms.CheckBox();
            this.checkDockpathSegmentFlagClose = new System.Windows.Forms.CheckBox();
            this.checkDockpathSegmentFlagUseRot = new System.Windows.Forms.CheckBox();
            this.checkDockpathSegmentFlagPlayer = new System.Windows.Forms.CheckBox();
            this.checkDockpathSegmentFlagQueue = new System.Windows.Forms.CheckBox();
            this.labelDockpathSegmentSpeed = new System.Windows.Forms.Label();
            this.labelDockpathSegmentTolerance = new System.Windows.Forms.Label();
            this.boxDockpathSegmentSpeed = new System.Windows.Forms.TextBox();
            this.boxDockpathSegmentTolerance = new System.Windows.Forms.TextBox();
            this.trackBarDockpathSegments = new System.Windows.Forms.TrackBar();
            this.groupDockpathLinks = new System.Windows.Forms.GroupBox();
            this.listDockpathLinks = new System.Windows.Forms.ListBox();
            this.groupDockpathFamilies = new System.Windows.Forms.GroupBox();
            this.listDockpathFamilies = new System.Windows.Forms.ListBox();
            this.panelDockpathList = new System.Windows.Forms.Panel();
            this.dockpathList = new System.Windows.Forms.CheckedListBox();
            this.tabNavLights = new System.Windows.Forms.TabPage();
            this.groupNavLightPreview = new System.Windows.Forms.GroupBox();
            this.checkNavLightDrawRadius = new System.Windows.Forms.CheckBox();
            this.groupNavLightParameters = new System.Windows.Forms.GroupBox();
            this.comboNavLightType = new System.Windows.Forms.ComboBox();
            this.labelNavLightDistance = new System.Windows.Forms.Label();
            this.numericNavLightDistance = new System.Windows.Forms.NumericUpDown();
            this.labelNavLightColor = new System.Windows.Forms.Label();
            this.buttonNavLightColor = new System.Windows.Forms.Button();
            this.labelNavLightFrequency = new System.Windows.Forms.Label();
            this.numericNavLightFrequency = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numericNavLightPhase = new System.Windows.Forms.NumericUpDown();
            this.labelNavLightSize = new System.Windows.Forms.Label();
            this.numericNavLightSize = new System.Windows.Forms.NumericUpDown();
            this.labelNavLightType = new System.Windows.Forms.Label();
            this.groupNavLightFlags = new System.Windows.Forms.GroupBox();
            this.checkNavLightFlagHighEnd = new System.Windows.Forms.CheckBox();
            this.checkNavLightFlagSprite = new System.Windows.Forms.CheckBox();
            this.panelNavLightList = new System.Windows.Forms.Panel();
            this.navLightList = new System.Windows.Forms.CheckedListBox();
            this.tabEngineGlows = new System.Windows.Forms.TabPage();
            this.groupEngineGlowLODs = new System.Windows.Forms.GroupBox();
            this.listEngineGlowLODs = new System.Windows.Forms.CheckedListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.listEngineGlows = new System.Windows.Forms.ListBox();
            this.labelEngineGlowParent = new System.Windows.Forms.Label();
            this.comboEngineGlowParent = new System.Windows.Forms.ComboBox();
            this.tabEngineShapes = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.listEngineShapes = new System.Windows.Forms.CheckedListBox();
            this.labelEngineShapeParent = new System.Windows.Forms.Label();
            this.comboEngineShapeParent = new System.Windows.Forms.ComboBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.gridProblems = new System.Windows.Forms.DataGridView();
            this.columnProblems = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.comboPerspectiveOrtho = new System.Windows.Forms.ComboBox();
            this.labelFPS = new System.Windows.Forms.Label();
            this.buttonProblems = new System.Windows.Forms.Button();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabShipMeshes.SuspendLayout();
            this.groupShipMeshLODs.SuspendLayout();
            this.panelShipMesh.SuspendLayout();
            this.tabMaterials.SuspendLayout();
            this.groupThrusterStrength.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarThrusterStrength)).BeginInit();
            this.groupMaterialTextures.SuspendLayout();
            this.panelMaterialsList.SuspendLayout();
            this.tabCollisionMeshes.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabJoints.SuspendLayout();
            this.tabMarkers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitTabMarkers)).BeginInit();
            this.splitTabMarkers.Panel1.SuspendLayout();
            this.splitTabMarkers.Panel2.SuspendLayout();
            this.splitTabMarkers.SuspendLayout();
            this.tabDockpaths.SuspendLayout();
            this.groupDockpathFlags.SuspendLayout();
            this.groupDockpathSegments.SuspendLayout();
            this.groupDockpathSegmentFlags.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDockpathSegments)).BeginInit();
            this.groupDockpathLinks.SuspendLayout();
            this.groupDockpathFamilies.SuspendLayout();
            this.panelDockpathList.SuspendLayout();
            this.tabNavLights.SuspendLayout();
            this.groupNavLightPreview.SuspendLayout();
            this.groupNavLightParameters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericNavLightDistance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericNavLightFrequency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericNavLightPhase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericNavLightSize)).BeginInit();
            this.groupNavLightFlags.SuspendLayout();
            this.panelNavLightList.SuspendLayout();
            this.tabEngineGlows.SuspendLayout();
            this.groupEngineGlowLODs.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabEngineShapes.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridProblems)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonOpen,
            this.buttonSettings,
            this.buttonHotkeys,
            this.buttonAbout,
            this.toolStripButton1});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1212, 25);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip";
            // 
            // buttonOpen
            // 
            this.buttonOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonOpen.Image = ((System.Drawing.Image)(resources.GetObject("buttonOpen.Image")));
            this.buttonOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(23, 22);
            this.buttonOpen.Text = "Open";
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // buttonSettings
            // 
            this.buttonSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonSettings.Image = ((System.Drawing.Image)(resources.GetObject("buttonSettings.Image")));
            this.buttonSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Size = new System.Drawing.Size(23, 22);
            this.buttonSettings.Text = "Settings";
            this.buttonSettings.Click += new System.EventHandler(this.buttonSettings_Click);
            // 
            // buttonHotkeys
            // 
            this.buttonHotkeys.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonHotkeys.Image = ((System.Drawing.Image)(resources.GetObject("buttonHotkeys.Image")));
            this.buttonHotkeys.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonHotkeys.Name = "buttonHotkeys";
            this.buttonHotkeys.Size = new System.Drawing.Size(23, 22);
            this.buttonHotkeys.Text = "Hotkeys";
            this.buttonHotkeys.Click += new System.EventHandler(this.buttonHotkeys_Click);
            // 
            // buttonAbout
            // 
            this.buttonAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonAbout.Image = ((System.Drawing.Image)(resources.GetObject("buttonAbout.Image")));
            this.buttonAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonAbout.Name = "buttonAbout";
            this.buttonAbout.Size = new System.Drawing.Size(23, 22);
            this.buttonAbout.Text = "About";
            this.buttonAbout.Click += new System.EventHandler(this.buttonAbout_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // openColladaDialog
            // 
            this.openColladaDialog.Filter = "COLLADA-Files|*.dae|All files|*.*";
            // 
            // saveColladaDialog
            // 
            this.saveColladaDialog.DefaultExt = "dae";
            this.saveColladaDialog.Filter = "COLLADA-Files|*.dae|All files|*.*";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl);
            this.splitContainer1.Panel1MinSize = 250;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1212, 716);
            this.splitContainer1.SplitterDistance = 250;
            this.splitContainer1.TabIndex = 4;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabShipMeshes);
            this.tabControl.Controls.Add(this.tabMaterials);
            this.tabControl.Controls.Add(this.tabCollisionMeshes);
            this.tabControl.Controls.Add(this.tabJoints);
            this.tabControl.Controls.Add(this.tabMarkers);
            this.tabControl.Controls.Add(this.tabDockpaths);
            this.tabControl.Controls.Add(this.tabNavLights);
            this.tabControl.Controls.Add(this.tabEngineGlows);
            this.tabControl.Controls.Add(this.tabEngineShapes);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(250, 716);
            this.tabControl.TabIndex = 0;
            // 
            // tabShipMeshes
            // 
            this.tabShipMeshes.Controls.Add(this.groupShipMeshLODs);
            this.tabShipMeshes.Controls.Add(this.panelShipMesh);
            this.tabShipMeshes.Location = new System.Drawing.Point(4, 58);
            this.tabShipMeshes.Name = "tabShipMeshes";
            this.tabShipMeshes.Padding = new System.Windows.Forms.Padding(3);
            this.tabShipMeshes.Size = new System.Drawing.Size(242, 654);
            this.tabShipMeshes.TabIndex = 0;
            this.tabShipMeshes.Text = "Ship Meshes";
            this.tabShipMeshes.UseVisualStyleBackColor = true;
            // 
            // groupShipMeshLODs
            // 
            this.groupShipMeshLODs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupShipMeshLODs.Controls.Add(this.listShipMeshLODs);
            this.groupShipMeshLODs.Location = new System.Drawing.Point(8, 471);
            this.groupShipMeshLODs.Name = "groupShipMeshLODs";
            this.groupShipMeshLODs.Size = new System.Drawing.Size(228, 217);
            this.groupShipMeshLODs.TabIndex = 1;
            this.groupShipMeshLODs.TabStop = false;
            this.groupShipMeshLODs.Text = "Level of detail(s)";
            // 
            // listShipMeshLODs
            // 
            this.listShipMeshLODs.CheckOnClick = true;
            this.listShipMeshLODs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listShipMeshLODs.FormattingEnabled = true;
            this.listShipMeshLODs.Location = new System.Drawing.Point(3, 16);
            this.listShipMeshLODs.Name = "listShipMeshLODs";
            this.listShipMeshLODs.Size = new System.Drawing.Size(222, 198);
            this.listShipMeshLODs.TabIndex = 0;
            this.listShipMeshLODs.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listShipMeshLODs_ItemCheck);
            // 
            // panelShipMesh
            // 
            this.panelShipMesh.Controls.Add(this.checkShipMeshDoScar);
            this.panelShipMesh.Controls.Add(this.listShipMeshes);
            this.panelShipMesh.Controls.Add(this.labelShipMeshParent);
            this.panelShipMesh.Controls.Add(this.comboShipMeshParent);
            this.panelShipMesh.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelShipMesh.Location = new System.Drawing.Point(3, 3);
            this.panelShipMesh.Name = "panelShipMesh";
            this.panelShipMesh.Size = new System.Drawing.Size(236, 462);
            this.panelShipMesh.TabIndex = 0;
            // 
            // checkShipMeshDoScar
            // 
            this.checkShipMeshDoScar.AutoSize = true;
            this.checkShipMeshDoScar.Enabled = false;
            this.checkShipMeshDoScar.Location = new System.Drawing.Point(6, 439);
            this.checkShipMeshDoScar.Name = "checkShipMeshDoScar";
            this.checkShipMeshDoScar.Size = new System.Drawing.Size(79, 17);
            this.checkShipMeshDoScar.TabIndex = 11;
            this.checkShipMeshDoScar.Text = "Allow scars";
            this.checkShipMeshDoScar.UseVisualStyleBackColor = true;
            // 
            // listShipMeshes
            // 
            this.listShipMeshes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listShipMeshes.FormattingEnabled = true;
            this.listShipMeshes.Location = new System.Drawing.Point(3, 3);
            this.listShipMeshes.Name = "listShipMeshes";
            this.listShipMeshes.Size = new System.Drawing.Size(230, 407);
            this.listShipMeshes.TabIndex = 10;
            this.listShipMeshes.SelectedIndexChanged += new System.EventHandler(this.listShipMeshes_SelectedIndexChanged);
            // 
            // labelShipMeshParent
            // 
            this.labelShipMeshParent.AutoSize = true;
            this.labelShipMeshParent.Location = new System.Drawing.Point(3, 415);
            this.labelShipMeshParent.Name = "labelShipMeshParent";
            this.labelShipMeshParent.Size = new System.Drawing.Size(44, 13);
            this.labelShipMeshParent.TabIndex = 9;
            this.labelShipMeshParent.Text = "Parent: ";
            // 
            // comboShipMeshParent
            // 
            this.comboShipMeshParent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboShipMeshParent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboShipMeshParent.Enabled = false;
            this.comboShipMeshParent.FormattingEnabled = true;
            this.comboShipMeshParent.Location = new System.Drawing.Point(53, 412);
            this.comboShipMeshParent.Name = "comboShipMeshParent";
            this.comboShipMeshParent.Size = new System.Drawing.Size(180, 21);
            this.comboShipMeshParent.TabIndex = 8;
            // 
            // tabMaterials
            // 
            this.tabMaterials.Controls.Add(this.groupThrusterStrength);
            this.tabMaterials.Controls.Add(this.groupMaterialTextures);
            this.tabMaterials.Controls.Add(this.panelMaterialsList);
            this.tabMaterials.Location = new System.Drawing.Point(4, 58);
            this.tabMaterials.Name = "tabMaterials";
            this.tabMaterials.Padding = new System.Windows.Forms.Padding(3);
            this.tabMaterials.Size = new System.Drawing.Size(242, 691);
            this.tabMaterials.TabIndex = 6;
            this.tabMaterials.Text = "Materials";
            this.tabMaterials.UseVisualStyleBackColor = true;
            // 
            // groupThrusterStrength
            // 
            this.groupThrusterStrength.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupThrusterStrength.Controls.Add(this.trackBarThrusterStrength);
            this.groupThrusterStrength.Location = new System.Drawing.Point(8, 640);
            this.groupThrusterStrength.Name = "groupThrusterStrength";
            this.groupThrusterStrength.Size = new System.Drawing.Size(228, 46);
            this.groupThrusterStrength.TabIndex = 2;
            this.groupThrusterStrength.TabStop = false;
            this.groupThrusterStrength.Text = "Thruster strength";
            // 
            // trackBarThrusterStrength
            // 
            this.trackBarThrusterStrength.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.trackBarThrusterStrength.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBarThrusterStrength.Location = new System.Drawing.Point(3, 16);
            this.trackBarThrusterStrength.Maximum = 100;
            this.trackBarThrusterStrength.Name = "trackBarThrusterStrength";
            this.trackBarThrusterStrength.Size = new System.Drawing.Size(222, 27);
            this.trackBarThrusterStrength.TabIndex = 0;
            this.trackBarThrusterStrength.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarThrusterStrength.Value = 100;
            this.trackBarThrusterStrength.Scroll += new System.EventHandler(this.trackBarThrusterStrength_Scroll);
            // 
            // groupMaterialTextures
            // 
            this.groupMaterialTextures.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupMaterialTextures.Controls.Add(this.listMaterialTextures);
            this.groupMaterialTextures.Location = new System.Drawing.Point(8, 384);
            this.groupMaterialTextures.Name = "groupMaterialTextures";
            this.groupMaterialTextures.Size = new System.Drawing.Size(228, 253);
            this.groupMaterialTextures.TabIndex = 1;
            this.groupMaterialTextures.TabStop = false;
            this.groupMaterialTextures.Text = "Textures";
            // 
            // listMaterialTextures
            // 
            this.listMaterialTextures.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listMaterialTextures.FormattingEnabled = true;
            this.listMaterialTextures.Location = new System.Drawing.Point(3, 16);
            this.listMaterialTextures.Name = "listMaterialTextures";
            this.listMaterialTextures.Size = new System.Drawing.Size(222, 234);
            this.listMaterialTextures.TabIndex = 0;
            // 
            // panelMaterialsList
            // 
            this.panelMaterialsList.AutoSize = true;
            this.panelMaterialsList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelMaterialsList.Controls.Add(this.label3);
            this.panelMaterialsList.Controls.Add(this.comboMaterialFormat);
            this.panelMaterialsList.Controls.Add(this.boxMaterialShader);
            this.panelMaterialsList.Controls.Add(this.listMaterials);
            this.panelMaterialsList.Controls.Add(this.label2);
            this.panelMaterialsList.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMaterialsList.Location = new System.Drawing.Point(3, 3);
            this.panelMaterialsList.Name = "panelMaterialsList";
            this.panelMaterialsList.Size = new System.Drawing.Size(236, 375);
            this.panelMaterialsList.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 354);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Format:";
            // 
            // comboMaterialFormat
            // 
            this.comboMaterialFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboMaterialFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.comboMaterialFormat.Enabled = false;
            this.comboMaterialFormat.FormattingEnabled = true;
            this.comboMaterialFormat.Location = new System.Drawing.Point(55, 351);
            this.comboMaterialFormat.Name = "comboMaterialFormat";
            this.comboMaterialFormat.Size = new System.Drawing.Size(178, 21);
            this.comboMaterialFormat.TabIndex = 12;
            // 
            // boxMaterialShader
            // 
            this.boxMaterialShader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boxMaterialShader.Location = new System.Drawing.Point(55, 325);
            this.boxMaterialShader.Name = "boxMaterialShader";
            this.boxMaterialShader.ReadOnly = true;
            this.boxMaterialShader.Size = new System.Drawing.Size(178, 20);
            this.boxMaterialShader.TabIndex = 11;
            // 
            // listMaterials
            // 
            this.listMaterials.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listMaterials.FormattingEnabled = true;
            this.listMaterials.Location = new System.Drawing.Point(3, 3);
            this.listMaterials.Name = "listMaterials";
            this.listMaterials.Size = new System.Drawing.Size(230, 316);
            this.listMaterials.TabIndex = 10;
            this.listMaterials.SelectedIndexChanged += new System.EventHandler(this.listMaterials_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 328);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Shader:";
            // 
            // tabCollisionMeshes
            // 
            this.tabCollisionMeshes.Controls.Add(this.panel1);
            this.tabCollisionMeshes.Location = new System.Drawing.Point(4, 58);
            this.tabCollisionMeshes.Name = "tabCollisionMeshes";
            this.tabCollisionMeshes.Padding = new System.Windows.Forms.Padding(3);
            this.tabCollisionMeshes.Size = new System.Drawing.Size(242, 691);
            this.tabCollisionMeshes.TabIndex = 5;
            this.tabCollisionMeshes.Text = "Collision Meshes";
            this.tabCollisionMeshes.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.listCollisionMeshes);
            this.panel1.Controls.Add(this.labelCollisionMeshParent);
            this.panel1.Controls.Add(this.comboCollisionMeshParent);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(236, 443);
            this.panel1.TabIndex = 0;
            // 
            // listCollisionMeshes
            // 
            this.listCollisionMeshes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listCollisionMeshes.FormattingEnabled = true;
            this.listCollisionMeshes.Location = new System.Drawing.Point(3, 3);
            this.listCollisionMeshes.Name = "listCollisionMeshes";
            this.listCollisionMeshes.Size = new System.Drawing.Size(230, 394);
            this.listCollisionMeshes.TabIndex = 12;
            this.listCollisionMeshes.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listCollisionMeshes_ItemCheck);
            this.listCollisionMeshes.SelectedIndexChanged += new System.EventHandler(this.listCollisionMeshes_SelectedIndexChanged);
            // 
            // labelCollisionMeshParent
            // 
            this.labelCollisionMeshParent.AutoSize = true;
            this.labelCollisionMeshParent.Location = new System.Drawing.Point(3, 415);
            this.labelCollisionMeshParent.Name = "labelCollisionMeshParent";
            this.labelCollisionMeshParent.Size = new System.Drawing.Size(44, 13);
            this.labelCollisionMeshParent.TabIndex = 9;
            this.labelCollisionMeshParent.Text = "Parent: ";
            // 
            // comboCollisionMeshParent
            // 
            this.comboCollisionMeshParent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboCollisionMeshParent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.comboCollisionMeshParent.Enabled = false;
            this.comboCollisionMeshParent.FormattingEnabled = true;
            this.comboCollisionMeshParent.Location = new System.Drawing.Point(56, 412);
            this.comboCollisionMeshParent.Name = "comboCollisionMeshParent";
            this.comboCollisionMeshParent.Size = new System.Drawing.Size(177, 21);
            this.comboCollisionMeshParent.TabIndex = 8;
            // 
            // tabJoints
            // 
            this.tabJoints.Controls.Add(this.jointsTree);
            this.tabJoints.Location = new System.Drawing.Point(4, 58);
            this.tabJoints.Name = "tabJoints";
            this.tabJoints.Padding = new System.Windows.Forms.Padding(3);
            this.tabJoints.Size = new System.Drawing.Size(242, 691);
            this.tabJoints.TabIndex = 1;
            this.tabJoints.Text = "Joints";
            this.tabJoints.UseVisualStyleBackColor = true;
            // 
            // jointsTree
            // 
            this.jointsTree.CheckBoxes = true;
            this.jointsTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jointsTree.Location = new System.Drawing.Point(3, 3);
            this.jointsTree.Name = "jointsTree";
            this.jointsTree.Size = new System.Drawing.Size(236, 685);
            this.jointsTree.TabIndex = 0;
            this.jointsTree.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.jointsTree_AfterCheck);
            // 
            // tabMarkers
            // 
            this.tabMarkers.Controls.Add(this.splitTabMarkers);
            this.tabMarkers.Location = new System.Drawing.Point(4, 58);
            this.tabMarkers.Name = "tabMarkers";
            this.tabMarkers.Size = new System.Drawing.Size(242, 691);
            this.tabMarkers.TabIndex = 2;
            this.tabMarkers.Text = "Markers";
            this.tabMarkers.UseVisualStyleBackColor = true;
            // 
            // splitTabMarkers
            // 
            this.splitTabMarkers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitTabMarkers.Location = new System.Drawing.Point(0, 0);
            this.splitTabMarkers.Name = "splitTabMarkers";
            this.splitTabMarkers.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitTabMarkers.Panel1
            // 
            this.splitTabMarkers.Panel1.Controls.Add(this.listBoxMarkers);
            // 
            // splitTabMarkers.Panel2
            // 
            this.splitTabMarkers.Panel2.Controls.Add(this.checkboxDrawMarkers);
            this.splitTabMarkers.Size = new System.Drawing.Size(242, 691);
            this.splitTabMarkers.SplitterDistance = 649;
            this.splitTabMarkers.TabIndex = 0;
            // 
            // listBoxMarkers
            // 
            this.listBoxMarkers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxMarkers.FormattingEnabled = true;
            this.listBoxMarkers.Location = new System.Drawing.Point(0, 0);
            this.listBoxMarkers.Name = "listBoxMarkers";
            this.listBoxMarkers.Size = new System.Drawing.Size(242, 649);
            this.listBoxMarkers.TabIndex = 2;
            // 
            // checkboxDrawMarkers
            // 
            this.checkboxDrawMarkers.AutoSize = true;
            this.checkboxDrawMarkers.Location = new System.Drawing.Point(4, 4);
            this.checkboxDrawMarkers.Name = "checkboxDrawMarkers";
            this.checkboxDrawMarkers.Size = new System.Drawing.Size(91, 17);
            this.checkboxDrawMarkers.TabIndex = 0;
            this.checkboxDrawMarkers.Text = "Draw markers";
            this.checkboxDrawMarkers.UseVisualStyleBackColor = true;
            this.checkboxDrawMarkers.CheckedChanged += new System.EventHandler(this.checkboxDrawMarkers_CheckedChanged);
            // 
            // tabDockpaths
            // 
            this.tabDockpaths.AutoScroll = true;
            this.tabDockpaths.Controls.Add(this.groupDockpathFlags);
            this.tabDockpaths.Controls.Add(this.groupDockpathSegments);
            this.tabDockpaths.Controls.Add(this.groupDockpathLinks);
            this.tabDockpaths.Controls.Add(this.groupDockpathFamilies);
            this.tabDockpaths.Controls.Add(this.panelDockpathList);
            this.tabDockpaths.Location = new System.Drawing.Point(4, 58);
            this.tabDockpaths.Name = "tabDockpaths";
            this.tabDockpaths.Size = new System.Drawing.Size(242, 691);
            this.tabDockpaths.TabIndex = 3;
            this.tabDockpaths.Text = "Dockpaths";
            this.tabDockpaths.UseVisualStyleBackColor = true;
            // 
            // groupDockpathFlags
            // 
            this.groupDockpathFlags.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupDockpathFlags.Controls.Add(this.checkDockpathAjar);
            this.groupDockpathFlags.Controls.Add(this.checkDockpathLatch);
            this.groupDockpathFlags.Controls.Add(this.checkDockpathAnim);
            this.groupDockpathFlags.Controls.Add(this.checkDockpathExit);
            this.groupDockpathFlags.Location = new System.Drawing.Point(4, 167);
            this.groupDockpathFlags.Name = "groupDockpathFlags";
            this.groupDockpathFlags.Size = new System.Drawing.Size(235, 64);
            this.groupDockpathFlags.TabIndex = 12;
            this.groupDockpathFlags.TabStop = false;
            this.groupDockpathFlags.Text = "Flags";
            // 
            // checkDockpathAjar
            // 
            this.checkDockpathAjar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkDockpathAjar.AutoSize = true;
            this.checkDockpathAjar.Enabled = false;
            this.checkDockpathAjar.Location = new System.Drawing.Point(176, 42);
            this.checkDockpathAjar.Name = "checkDockpathAjar";
            this.checkDockpathAjar.Size = new System.Drawing.Size(44, 17);
            this.checkDockpathAjar.TabIndex = 11;
            this.checkDockpathAjar.Text = "Ajar";
            this.checkDockpathAjar.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.checkDockpathAjar.UseVisualStyleBackColor = true;
            // 
            // checkDockpathLatch
            // 
            this.checkDockpathLatch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkDockpathLatch.AutoSize = true;
            this.checkDockpathLatch.Enabled = false;
            this.checkDockpathLatch.Location = new System.Drawing.Point(176, 19);
            this.checkDockpathLatch.Name = "checkDockpathLatch";
            this.checkDockpathLatch.Size = new System.Drawing.Size(53, 17);
            this.checkDockpathLatch.TabIndex = 10;
            this.checkDockpathLatch.Text = "Latch";
            this.checkDockpathLatch.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.checkDockpathLatch.UseVisualStyleBackColor = true;
            // 
            // checkDockpathAnim
            // 
            this.checkDockpathAnim.AutoSize = true;
            this.checkDockpathAnim.Enabled = false;
            this.checkDockpathAnim.Location = new System.Drawing.Point(6, 42);
            this.checkDockpathAnim.Name = "checkDockpathAnim";
            this.checkDockpathAnim.Size = new System.Drawing.Size(72, 17);
            this.checkDockpathAnim.TabIndex = 9;
            this.checkDockpathAnim.Text = "Animation";
            this.checkDockpathAnim.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkDockpathAnim.UseVisualStyleBackColor = true;
            // 
            // checkDockpathExit
            // 
            this.checkDockpathExit.AutoSize = true;
            this.checkDockpathExit.Enabled = false;
            this.checkDockpathExit.Location = new System.Drawing.Point(6, 19);
            this.checkDockpathExit.Name = "checkDockpathExit";
            this.checkDockpathExit.Size = new System.Drawing.Size(43, 17);
            this.checkDockpathExit.TabIndex = 8;
            this.checkDockpathExit.Text = "Exit";
            this.checkDockpathExit.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkDockpathExit.UseVisualStyleBackColor = true;
            // 
            // groupDockpathSegments
            // 
            this.groupDockpathSegments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupDockpathSegments.Controls.Add(this.groupDockpathSegmentFlags);
            this.groupDockpathSegments.Controls.Add(this.labelDockpathSegmentSpeed);
            this.groupDockpathSegments.Controls.Add(this.labelDockpathSegmentTolerance);
            this.groupDockpathSegments.Controls.Add(this.boxDockpathSegmentSpeed);
            this.groupDockpathSegments.Controls.Add(this.boxDockpathSegmentTolerance);
            this.groupDockpathSegments.Controls.Add(this.trackBarDockpathSegments);
            this.groupDockpathSegments.Location = new System.Drawing.Point(4, 451);
            this.groupDockpathSegments.Name = "groupDockpathSegments";
            this.groupDockpathSegments.Size = new System.Drawing.Size(235, 237);
            this.groupDockpathSegments.TabIndex = 3;
            this.groupDockpathSegments.TabStop = false;
            this.groupDockpathSegments.Text = "Segments";
            // 
            // groupDockpathSegmentFlags
            // 
            this.groupDockpathSegmentFlags.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupDockpathSegmentFlags.Controls.Add(this.checkDockpathSegmentFlagClip);
            this.groupDockpathSegmentFlags.Controls.Add(this.checkDockpathSegmentFlagUnfocus);
            this.groupDockpathSegmentFlags.Controls.Add(this.checkDockpathSegmentFlagCheck);
            this.groupDockpathSegmentFlags.Controls.Add(this.checkDockpathSegmentFlagClearRes);
            this.groupDockpathSegmentFlags.Controls.Add(this.checkDockpathSegmentFlagClose);
            this.groupDockpathSegmentFlags.Controls.Add(this.checkDockpathSegmentFlagUseRot);
            this.groupDockpathSegmentFlags.Controls.Add(this.checkDockpathSegmentFlagPlayer);
            this.groupDockpathSegmentFlags.Controls.Add(this.checkDockpathSegmentFlagQueue);
            this.groupDockpathSegmentFlags.Location = new System.Drawing.Point(3, 122);
            this.groupDockpathSegmentFlags.Name = "groupDockpathSegmentFlags";
            this.groupDockpathSegmentFlags.Size = new System.Drawing.Size(226, 111);
            this.groupDockpathSegmentFlags.TabIndex = 11;
            this.groupDockpathSegmentFlags.TabStop = false;
            this.groupDockpathSegmentFlags.Text = "Flags";
            // 
            // checkDockpathSegmentFlagClip
            // 
            this.checkDockpathSegmentFlagClip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkDockpathSegmentFlagClip.AutoSize = true;
            this.checkDockpathSegmentFlagClip.Enabled = false;
            this.checkDockpathSegmentFlagClip.Location = new System.Drawing.Point(119, 89);
            this.checkDockpathSegmentFlagClip.Name = "checkDockpathSegmentFlagClip";
            this.checkDockpathSegmentFlagClip.Size = new System.Drawing.Size(72, 17);
            this.checkDockpathSegmentFlagClip.TabIndex = 27;
            this.checkDockpathSegmentFlagClip.Text = "Clip plane";
            this.checkDockpathSegmentFlagClip.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.checkDockpathSegmentFlagClip.UseVisualStyleBackColor = true;
            // 
            // checkDockpathSegmentFlagUnfocus
            // 
            this.checkDockpathSegmentFlagUnfocus.AutoSize = true;
            this.checkDockpathSegmentFlagUnfocus.Enabled = false;
            this.checkDockpathSegmentFlagUnfocus.Location = new System.Drawing.Point(6, 88);
            this.checkDockpathSegmentFlagUnfocus.Name = "checkDockpathSegmentFlagUnfocus";
            this.checkDockpathSegmentFlagUnfocus.Size = new System.Drawing.Size(78, 17);
            this.checkDockpathSegmentFlagUnfocus.TabIndex = 26;
            this.checkDockpathSegmentFlagUnfocus.Text = "Drop focus";
            this.checkDockpathSegmentFlagUnfocus.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkDockpathSegmentFlagUnfocus.UseVisualStyleBackColor = true;
            // 
            // checkDockpathSegmentFlagCheck
            // 
            this.checkDockpathSegmentFlagCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkDockpathSegmentFlagCheck.AutoSize = true;
            this.checkDockpathSegmentFlagCheck.Enabled = false;
            this.checkDockpathSegmentFlagCheck.Location = new System.Drawing.Point(119, 66);
            this.checkDockpathSegmentFlagCheck.Name = "checkDockpathSegmentFlagCheck";
            this.checkDockpathSegmentFlagCheck.Size = new System.Drawing.Size(95, 17);
            this.checkDockpathSegmentFlagCheck.TabIndex = 25;
            this.checkDockpathSegmentFlagCheck.Text = "Check rotation";
            this.checkDockpathSegmentFlagCheck.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.checkDockpathSegmentFlagCheck.UseVisualStyleBackColor = true;
            // 
            // checkDockpathSegmentFlagClearRes
            // 
            this.checkDockpathSegmentFlagClearRes.AutoSize = true;
            this.checkDockpathSegmentFlagClearRes.Enabled = false;
            this.checkDockpathSegmentFlagClearRes.Location = new System.Drawing.Point(6, 65);
            this.checkDockpathSegmentFlagClearRes.Name = "checkDockpathSegmentFlagClearRes";
            this.checkDockpathSegmentFlagClearRes.Size = new System.Drawing.Size(105, 17);
            this.checkDockpathSegmentFlagClearRes.TabIndex = 24;
            this.checkDockpathSegmentFlagClearRes.Text = "Clear reservation";
            this.checkDockpathSegmentFlagClearRes.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkDockpathSegmentFlagClearRes.UseVisualStyleBackColor = true;
            // 
            // checkDockpathSegmentFlagClose
            // 
            this.checkDockpathSegmentFlagClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkDockpathSegmentFlagClose.AutoSize = true;
            this.checkDockpathSegmentFlagClose.Enabled = false;
            this.checkDockpathSegmentFlagClose.Location = new System.Drawing.Point(119, 42);
            this.checkDockpathSegmentFlagClose.Name = "checkDockpathSegmentFlagClose";
            this.checkDockpathSegmentFlagClose.Size = new System.Drawing.Size(102, 17);
            this.checkDockpathSegmentFlagClose.TabIndex = 23;
            this.checkDockpathSegmentFlagClose.Text = "Close behaviour";
            this.checkDockpathSegmentFlagClose.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.checkDockpathSegmentFlagClose.UseVisualStyleBackColor = true;
            // 
            // checkDockpathSegmentFlagUseRot
            // 
            this.checkDockpathSegmentFlagUseRot.AutoSize = true;
            this.checkDockpathSegmentFlagUseRot.Enabled = false;
            this.checkDockpathSegmentFlagUseRot.Location = new System.Drawing.Point(6, 19);
            this.checkDockpathSegmentFlagUseRot.Name = "checkDockpathSegmentFlagUseRot";
            this.checkDockpathSegmentFlagUseRot.Size = new System.Drawing.Size(83, 17);
            this.checkDockpathSegmentFlagUseRot.TabIndex = 20;
            this.checkDockpathSegmentFlagUseRot.Text = "Use rotation";
            this.checkDockpathSegmentFlagUseRot.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkDockpathSegmentFlagUseRot.UseVisualStyleBackColor = true;
            // 
            // checkDockpathSegmentFlagPlayer
            // 
            this.checkDockpathSegmentFlagPlayer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkDockpathSegmentFlagPlayer.AutoSize = true;
            this.checkDockpathSegmentFlagPlayer.Enabled = false;
            this.checkDockpathSegmentFlagPlayer.Location = new System.Drawing.Point(119, 19);
            this.checkDockpathSegmentFlagPlayer.Name = "checkDockpathSegmentFlagPlayer";
            this.checkDockpathSegmentFlagPlayer.Size = new System.Drawing.Size(101, 17);
            this.checkDockpathSegmentFlagPlayer.TabIndex = 22;
            this.checkDockpathSegmentFlagPlayer.Text = "Player in control";
            this.checkDockpathSegmentFlagPlayer.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.checkDockpathSegmentFlagPlayer.UseVisualStyleBackColor = true;
            // 
            // checkDockpathSegmentFlagQueue
            // 
            this.checkDockpathSegmentFlagQueue.AutoSize = true;
            this.checkDockpathSegmentFlagQueue.Enabled = false;
            this.checkDockpathSegmentFlagQueue.Location = new System.Drawing.Point(6, 42);
            this.checkDockpathSegmentFlagQueue.Name = "checkDockpathSegmentFlagQueue";
            this.checkDockpathSegmentFlagQueue.Size = new System.Drawing.Size(84, 17);
            this.checkDockpathSegmentFlagQueue.TabIndex = 21;
            this.checkDockpathSegmentFlagQueue.Text = "Queue point";
            this.checkDockpathSegmentFlagQueue.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkDockpathSegmentFlagQueue.UseVisualStyleBackColor = true;
            // 
            // labelDockpathSegmentSpeed
            // 
            this.labelDockpathSegmentSpeed.AutoSize = true;
            this.labelDockpathSegmentSpeed.Location = new System.Drawing.Point(6, 99);
            this.labelDockpathSegmentSpeed.Name = "labelDockpathSegmentSpeed";
            this.labelDockpathSegmentSpeed.Size = new System.Drawing.Size(41, 13);
            this.labelDockpathSegmentSpeed.TabIndex = 10;
            this.labelDockpathSegmentSpeed.Text = "Speed:";
            // 
            // labelDockpathSegmentTolerance
            // 
            this.labelDockpathSegmentTolerance.AutoSize = true;
            this.labelDockpathSegmentTolerance.Location = new System.Drawing.Point(6, 73);
            this.labelDockpathSegmentTolerance.Name = "labelDockpathSegmentTolerance";
            this.labelDockpathSegmentTolerance.Size = new System.Drawing.Size(61, 13);
            this.labelDockpathSegmentTolerance.TabIndex = 9;
            this.labelDockpathSegmentTolerance.Text = "Tolerance: ";
            // 
            // boxDockpathSegmentSpeed
            // 
            this.boxDockpathSegmentSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boxDockpathSegmentSpeed.Location = new System.Drawing.Point(77, 96);
            this.boxDockpathSegmentSpeed.Name = "boxDockpathSegmentSpeed";
            this.boxDockpathSegmentSpeed.ReadOnly = true;
            this.boxDockpathSegmentSpeed.Size = new System.Drawing.Size(155, 20);
            this.boxDockpathSegmentSpeed.TabIndex = 8;
            // 
            // boxDockpathSegmentTolerance
            // 
            this.boxDockpathSegmentTolerance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boxDockpathSegmentTolerance.Location = new System.Drawing.Point(77, 70);
            this.boxDockpathSegmentTolerance.Name = "boxDockpathSegmentTolerance";
            this.boxDockpathSegmentTolerance.ReadOnly = true;
            this.boxDockpathSegmentTolerance.Size = new System.Drawing.Size(155, 20);
            this.boxDockpathSegmentTolerance.TabIndex = 7;
            // 
            // trackBarDockpathSegments
            // 
            this.trackBarDockpathSegments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarDockpathSegments.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.trackBarDockpathSegments.Location = new System.Drawing.Point(9, 19);
            this.trackBarDockpathSegments.Name = "trackBarDockpathSegments";
            this.trackBarDockpathSegments.Size = new System.Drawing.Size(229, 45);
            this.trackBarDockpathSegments.TabIndex = 3;
            this.trackBarDockpathSegments.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBarDockpathSegments.Scroll += new System.EventHandler(this.trackBarDockpathSegments_Scroll);
            // 
            // groupDockpathLinks
            // 
            this.groupDockpathLinks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupDockpathLinks.Controls.Add(this.listDockpathLinks);
            this.groupDockpathLinks.Location = new System.Drawing.Point(4, 344);
            this.groupDockpathLinks.Name = "groupDockpathLinks";
            this.groupDockpathLinks.Size = new System.Drawing.Size(235, 101);
            this.groupDockpathLinks.TabIndex = 2;
            this.groupDockpathLinks.TabStop = false;
            this.groupDockpathLinks.Text = "Links";
            // 
            // listDockpathLinks
            // 
            this.listDockpathLinks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listDockpathLinks.Enabled = false;
            this.listDockpathLinks.FormattingEnabled = true;
            this.listDockpathLinks.Location = new System.Drawing.Point(3, 16);
            this.listDockpathLinks.Name = "listDockpathLinks";
            this.listDockpathLinks.Size = new System.Drawing.Size(229, 82);
            this.listDockpathLinks.TabIndex = 11;
            // 
            // groupDockpathFamilies
            // 
            this.groupDockpathFamilies.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupDockpathFamilies.Controls.Add(this.listDockpathFamilies);
            this.groupDockpathFamilies.Location = new System.Drawing.Point(4, 237);
            this.groupDockpathFamilies.Name = "groupDockpathFamilies";
            this.groupDockpathFamilies.Size = new System.Drawing.Size(235, 101);
            this.groupDockpathFamilies.TabIndex = 1;
            this.groupDockpathFamilies.TabStop = false;
            this.groupDockpathFamilies.Text = "Families";
            // 
            // listDockpathFamilies
            // 
            this.listDockpathFamilies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listDockpathFamilies.Enabled = false;
            this.listDockpathFamilies.FormattingEnabled = true;
            this.listDockpathFamilies.Location = new System.Drawing.Point(3, 16);
            this.listDockpathFamilies.Name = "listDockpathFamilies";
            this.listDockpathFamilies.Size = new System.Drawing.Size(229, 82);
            this.listDockpathFamilies.TabIndex = 11;
            // 
            // panelDockpathList
            // 
            this.panelDockpathList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDockpathList.Controls.Add(this.dockpathList);
            this.panelDockpathList.Location = new System.Drawing.Point(4, 4);
            this.panelDockpathList.Name = "panelDockpathList";
            this.panelDockpathList.Size = new System.Drawing.Size(235, 157);
            this.panelDockpathList.TabIndex = 0;
            // 
            // dockpathList
            // 
            this.dockpathList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockpathList.FormattingEnabled = true;
            this.dockpathList.Location = new System.Drawing.Point(0, 0);
            this.dockpathList.Name = "dockpathList";
            this.dockpathList.Size = new System.Drawing.Size(235, 157);
            this.dockpathList.TabIndex = 7;
            this.dockpathList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.dockpathList_ItemCheck);
            this.dockpathList.SelectedIndexChanged += new System.EventHandler(this.dockpathList_SelectedIndexChanged);
            // 
            // tabNavLights
            // 
            this.tabNavLights.AutoScroll = true;
            this.tabNavLights.Controls.Add(this.groupNavLightPreview);
            this.tabNavLights.Controls.Add(this.groupNavLightParameters);
            this.tabNavLights.Controls.Add(this.groupNavLightFlags);
            this.tabNavLights.Controls.Add(this.panelNavLightList);
            this.tabNavLights.Location = new System.Drawing.Point(4, 58);
            this.tabNavLights.Name = "tabNavLights";
            this.tabNavLights.Size = new System.Drawing.Size(242, 691);
            this.tabNavLights.TabIndex = 7;
            this.tabNavLights.Text = "NavLights";
            this.tabNavLights.UseVisualStyleBackColor = true;
            // 
            // groupNavLightPreview
            // 
            this.groupNavLightPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupNavLightPreview.Controls.Add(this.checkNavLightDrawRadius);
            this.groupNavLightPreview.Location = new System.Drawing.Point(8, 642);
            this.groupNavLightPreview.Name = "groupNavLightPreview";
            this.groupNavLightPreview.Size = new System.Drawing.Size(226, 41);
            this.groupNavLightPreview.TabIndex = 14;
            this.groupNavLightPreview.TabStop = false;
            this.groupNavLightPreview.Text = "Preview";
            // 
            // checkNavLightDrawRadius
            // 
            this.checkNavLightDrawRadius.AutoSize = true;
            this.checkNavLightDrawRadius.Location = new System.Drawing.Point(7, 20);
            this.checkNavLightDrawRadius.Name = "checkNavLightDrawRadius";
            this.checkNavLightDrawRadius.Size = new System.Drawing.Size(136, 17);
            this.checkNavLightDrawRadius.TabIndex = 0;
            this.checkNavLightDrawRadius.Text = "Draw illumination radius";
            this.checkNavLightDrawRadius.UseVisualStyleBackColor = true;
            this.checkNavLightDrawRadius.CheckedChanged += new System.EventHandler(this.checkNavLightDrawRadius_CheckedChanged);
            // 
            // groupNavLightParameters
            // 
            this.groupNavLightParameters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupNavLightParameters.Controls.Add(this.comboNavLightType);
            this.groupNavLightParameters.Controls.Add(this.labelNavLightDistance);
            this.groupNavLightParameters.Controls.Add(this.numericNavLightDistance);
            this.groupNavLightParameters.Controls.Add(this.labelNavLightColor);
            this.groupNavLightParameters.Controls.Add(this.buttonNavLightColor);
            this.groupNavLightParameters.Controls.Add(this.labelNavLightFrequency);
            this.groupNavLightParameters.Controls.Add(this.numericNavLightFrequency);
            this.groupNavLightParameters.Controls.Add(this.label4);
            this.groupNavLightParameters.Controls.Add(this.numericNavLightPhase);
            this.groupNavLightParameters.Controls.Add(this.labelNavLightSize);
            this.groupNavLightParameters.Controls.Add(this.numericNavLightSize);
            this.groupNavLightParameters.Controls.Add(this.labelNavLightType);
            this.groupNavLightParameters.Location = new System.Drawing.Point(8, 402);
            this.groupNavLightParameters.Name = "groupNavLightParameters";
            this.groupNavLightParameters.Size = new System.Drawing.Size(227, 185);
            this.groupNavLightParameters.TabIndex = 13;
            this.groupNavLightParameters.TabStop = false;
            this.groupNavLightParameters.Text = "Parameters";
            // 
            // comboNavLightType
            // 
            this.comboNavLightType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboNavLightType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboNavLightType.Enabled = false;
            this.comboNavLightType.FormattingEnabled = true;
            this.comboNavLightType.Location = new System.Drawing.Point(72, 19);
            this.comboNavLightType.Name = "comboNavLightType";
            this.comboNavLightType.Size = new System.Drawing.Size(149, 21);
            this.comboNavLightType.TabIndex = 24;
            // 
            // labelNavLightDistance
            // 
            this.labelNavLightDistance.AutoSize = true;
            this.labelNavLightDistance.Location = new System.Drawing.Point(6, 153);
            this.labelNavLightDistance.Name = "labelNavLightDistance";
            this.labelNavLightDistance.Size = new System.Drawing.Size(52, 13);
            this.labelNavLightDistance.TabIndex = 23;
            this.labelNavLightDistance.Text = "Distance:";
            // 
            // numericNavLightDistance
            // 
            this.numericNavLightDistance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericNavLightDistance.DecimalPlaces = 2;
            this.numericNavLightDistance.Enabled = false;
            this.numericNavLightDistance.Location = new System.Drawing.Point(72, 149);
            this.numericNavLightDistance.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericNavLightDistance.Name = "numericNavLightDistance";
            this.numericNavLightDistance.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericNavLightDistance.Size = new System.Drawing.Size(149, 20);
            this.numericNavLightDistance.TabIndex = 22;
            this.numericNavLightDistance.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            // 
            // labelNavLightColor
            // 
            this.labelNavLightColor.AutoSize = true;
            this.labelNavLightColor.Location = new System.Drawing.Point(6, 127);
            this.labelNavLightColor.Name = "labelNavLightColor";
            this.labelNavLightColor.Size = new System.Drawing.Size(34, 13);
            this.labelNavLightColor.TabIndex = 21;
            this.labelNavLightColor.Text = "Color:";
            // 
            // buttonNavLightColor
            // 
            this.buttonNavLightColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNavLightColor.BackColor = System.Drawing.Color.White;
            this.buttonNavLightColor.Enabled = false;
            this.buttonNavLightColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonNavLightColor.Location = new System.Drawing.Point(72, 123);
            this.buttonNavLightColor.Name = "buttonNavLightColor";
            this.buttonNavLightColor.Size = new System.Drawing.Size(149, 20);
            this.buttonNavLightColor.TabIndex = 20;
            this.buttonNavLightColor.UseVisualStyleBackColor = false;
            // 
            // labelNavLightFrequency
            // 
            this.labelNavLightFrequency.AutoSize = true;
            this.labelNavLightFrequency.Location = new System.Drawing.Point(6, 99);
            this.labelNavLightFrequency.Name = "labelNavLightFrequency";
            this.labelNavLightFrequency.Size = new System.Drawing.Size(60, 13);
            this.labelNavLightFrequency.TabIndex = 19;
            this.labelNavLightFrequency.Text = "Frequency:";
            // 
            // numericNavLightFrequency
            // 
            this.numericNavLightFrequency.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericNavLightFrequency.DecimalPlaces = 2;
            this.numericNavLightFrequency.Enabled = false;
            this.numericNavLightFrequency.Location = new System.Drawing.Point(72, 97);
            this.numericNavLightFrequency.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericNavLightFrequency.Name = "numericNavLightFrequency";
            this.numericNavLightFrequency.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericNavLightFrequency.Size = new System.Drawing.Size(149, 20);
            this.numericNavLightFrequency.TabIndex = 18;
            this.numericNavLightFrequency.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Phase:";
            // 
            // numericNavLightPhase
            // 
            this.numericNavLightPhase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericNavLightPhase.DecimalPlaces = 2;
            this.numericNavLightPhase.Enabled = false;
            this.numericNavLightPhase.Location = new System.Drawing.Point(72, 71);
            this.numericNavLightPhase.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericNavLightPhase.Name = "numericNavLightPhase";
            this.numericNavLightPhase.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericNavLightPhase.Size = new System.Drawing.Size(149, 20);
            this.numericNavLightPhase.TabIndex = 16;
            this.numericNavLightPhase.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            // 
            // labelNavLightSize
            // 
            this.labelNavLightSize.AutoSize = true;
            this.labelNavLightSize.Location = new System.Drawing.Point(6, 47);
            this.labelNavLightSize.Name = "labelNavLightSize";
            this.labelNavLightSize.Size = new System.Drawing.Size(30, 13);
            this.labelNavLightSize.TabIndex = 15;
            this.labelNavLightSize.Text = "Size:";
            // 
            // numericNavLightSize
            // 
            this.numericNavLightSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericNavLightSize.DecimalPlaces = 2;
            this.numericNavLightSize.Enabled = false;
            this.numericNavLightSize.Location = new System.Drawing.Point(72, 45);
            this.numericNavLightSize.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericNavLightSize.Name = "numericNavLightSize";
            this.numericNavLightSize.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericNavLightSize.Size = new System.Drawing.Size(149, 20);
            this.numericNavLightSize.TabIndex = 14;
            this.numericNavLightSize.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            // 
            // labelNavLightType
            // 
            this.labelNavLightType.AutoSize = true;
            this.labelNavLightType.Location = new System.Drawing.Point(6, 22);
            this.labelNavLightType.Name = "labelNavLightType";
            this.labelNavLightType.Size = new System.Drawing.Size(34, 13);
            this.labelNavLightType.TabIndex = 12;
            this.labelNavLightType.Text = "Type:";
            // 
            // groupNavLightFlags
            // 
            this.groupNavLightFlags.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupNavLightFlags.Controls.Add(this.checkNavLightFlagHighEnd);
            this.groupNavLightFlags.Controls.Add(this.checkNavLightFlagSprite);
            this.groupNavLightFlags.Location = new System.Drawing.Point(8, 593);
            this.groupNavLightFlags.Name = "groupNavLightFlags";
            this.groupNavLightFlags.Size = new System.Drawing.Size(227, 43);
            this.groupNavLightFlags.TabIndex = 12;
            this.groupNavLightFlags.TabStop = false;
            this.groupNavLightFlags.Text = "Flags";
            // 
            // checkNavLightFlagHighEnd
            // 
            this.checkNavLightFlagHighEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkNavLightFlagHighEnd.AutoSize = true;
            this.checkNavLightFlagHighEnd.Enabled = false;
            this.checkNavLightFlagHighEnd.Location = new System.Drawing.Point(152, 19);
            this.checkNavLightFlagHighEnd.Name = "checkNavLightFlagHighEnd";
            this.checkNavLightFlagHighEnd.Size = new System.Drawing.Size(69, 17);
            this.checkNavLightFlagHighEnd.TabIndex = 10;
            this.checkNavLightFlagHighEnd.Text = "High-end";
            this.checkNavLightFlagHighEnd.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.checkNavLightFlagHighEnd.UseVisualStyleBackColor = true;
            // 
            // checkNavLightFlagSprite
            // 
            this.checkNavLightFlagSprite.AutoSize = true;
            this.checkNavLightFlagSprite.Enabled = false;
            this.checkNavLightFlagSprite.Location = new System.Drawing.Point(6, 19);
            this.checkNavLightFlagSprite.Name = "checkNavLightFlagSprite";
            this.checkNavLightFlagSprite.Size = new System.Drawing.Size(53, 17);
            this.checkNavLightFlagSprite.TabIndex = 8;
            this.checkNavLightFlagSprite.Text = "Sprite";
            this.checkNavLightFlagSprite.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkNavLightFlagSprite.UseVisualStyleBackColor = true;
            // 
            // panelNavLightList
            // 
            this.panelNavLightList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelNavLightList.Controls.Add(this.navLightList);
            this.panelNavLightList.Location = new System.Drawing.Point(4, 4);
            this.panelNavLightList.Name = "panelNavLightList";
            this.panelNavLightList.Size = new System.Drawing.Size(235, 392);
            this.panelNavLightList.TabIndex = 0;
            // 
            // navLightList
            // 
            this.navLightList.CheckOnClick = true;
            this.navLightList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navLightList.FormattingEnabled = true;
            this.navLightList.Location = new System.Drawing.Point(0, 0);
            this.navLightList.Name = "navLightList";
            this.navLightList.Size = new System.Drawing.Size(235, 392);
            this.navLightList.TabIndex = 7;
            this.navLightList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.navLightList_ItemCheck);
            this.navLightList.SelectedIndexChanged += new System.EventHandler(this.navLightList_SelectedIndexChanged);
            // 
            // tabEngineGlows
            // 
            this.tabEngineGlows.Controls.Add(this.groupEngineGlowLODs);
            this.tabEngineGlows.Controls.Add(this.panel2);
            this.tabEngineGlows.Location = new System.Drawing.Point(4, 58);
            this.tabEngineGlows.Name = "tabEngineGlows";
            this.tabEngineGlows.Padding = new System.Windows.Forms.Padding(3);
            this.tabEngineGlows.Size = new System.Drawing.Size(242, 691);
            this.tabEngineGlows.TabIndex = 8;
            this.tabEngineGlows.Text = "Engine Glows";
            this.tabEngineGlows.UseVisualStyleBackColor = true;
            // 
            // groupEngineGlowLODs
            // 
            this.groupEngineGlowLODs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupEngineGlowLODs.Controls.Add(this.listEngineGlowLODs);
            this.groupEngineGlowLODs.Location = new System.Drawing.Point(8, 471);
            this.groupEngineGlowLODs.Name = "groupEngineGlowLODs";
            this.groupEngineGlowLODs.Size = new System.Drawing.Size(228, 219);
            this.groupEngineGlowLODs.TabIndex = 1;
            this.groupEngineGlowLODs.TabStop = false;
            this.groupEngineGlowLODs.Text = "Level of detail(s)";
            // 
            // listEngineGlowLODs
            // 
            this.listEngineGlowLODs.CheckOnClick = true;
            this.listEngineGlowLODs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listEngineGlowLODs.FormattingEnabled = true;
            this.listEngineGlowLODs.Location = new System.Drawing.Point(3, 16);
            this.listEngineGlowLODs.Name = "listEngineGlowLODs";
            this.listEngineGlowLODs.Size = new System.Drawing.Size(222, 200);
            this.listEngineGlowLODs.TabIndex = 0;
            this.listEngineGlowLODs.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listEngineGlowLODs_ItemCheck);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.listEngineGlows);
            this.panel2.Controls.Add(this.labelEngineGlowParent);
            this.panel2.Controls.Add(this.comboEngineGlowParent);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(236, 462);
            this.panel2.TabIndex = 0;
            // 
            // listEngineGlows
            // 
            this.listEngineGlows.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listEngineGlows.FormattingEnabled = true;
            this.listEngineGlows.Location = new System.Drawing.Point(3, 3);
            this.listEngineGlows.Name = "listEngineGlows";
            this.listEngineGlows.Size = new System.Drawing.Size(230, 420);
            this.listEngineGlows.TabIndex = 10;
            this.listEngineGlows.SelectedIndexChanged += new System.EventHandler(this.listEngineGlows_SelectedIndexChanged);
            // 
            // labelEngineGlowParent
            // 
            this.labelEngineGlowParent.AutoSize = true;
            this.labelEngineGlowParent.Location = new System.Drawing.Point(5, 441);
            this.labelEngineGlowParent.Name = "labelEngineGlowParent";
            this.labelEngineGlowParent.Size = new System.Drawing.Size(44, 13);
            this.labelEngineGlowParent.TabIndex = 9;
            this.labelEngineGlowParent.Text = "Parent: ";
            // 
            // comboEngineGlowParent
            // 
            this.comboEngineGlowParent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboEngineGlowParent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboEngineGlowParent.Enabled = false;
            this.comboEngineGlowParent.FormattingEnabled = true;
            this.comboEngineGlowParent.Location = new System.Drawing.Point(53, 438);
            this.comboEngineGlowParent.Name = "comboEngineGlowParent";
            this.comboEngineGlowParent.Size = new System.Drawing.Size(180, 21);
            this.comboEngineGlowParent.TabIndex = 8;
            // 
            // tabEngineShapes
            // 
            this.tabEngineShapes.Controls.Add(this.panel3);
            this.tabEngineShapes.Location = new System.Drawing.Point(4, 58);
            this.tabEngineShapes.Name = "tabEngineShapes";
            this.tabEngineShapes.Padding = new System.Windows.Forms.Padding(3);
            this.tabEngineShapes.Size = new System.Drawing.Size(242, 691);
            this.tabEngineShapes.TabIndex = 9;
            this.tabEngineShapes.Text = "Engine Shapes";
            this.tabEngineShapes.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.listEngineShapes);
            this.panel3.Controls.Add(this.labelEngineShapeParent);
            this.panel3.Controls.Add(this.comboEngineShapeParent);
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(236, 443);
            this.panel3.TabIndex = 0;
            // 
            // listEngineShapes
            // 
            this.listEngineShapes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listEngineShapes.FormattingEnabled = true;
            this.listEngineShapes.Location = new System.Drawing.Point(3, 3);
            this.listEngineShapes.Name = "listEngineShapes";
            this.listEngineShapes.Size = new System.Drawing.Size(230, 394);
            this.listEngineShapes.TabIndex = 12;
            this.listEngineShapes.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listEngineShapes_ItemCheck);
            this.listEngineShapes.SelectedIndexChanged += new System.EventHandler(this.listEngineShapes_SelectedIndexChanged);
            // 
            // labelEngineShapeParent
            // 
            this.labelEngineShapeParent.AutoSize = true;
            this.labelEngineShapeParent.Location = new System.Drawing.Point(3, 415);
            this.labelEngineShapeParent.Name = "labelEngineShapeParent";
            this.labelEngineShapeParent.Size = new System.Drawing.Size(44, 13);
            this.labelEngineShapeParent.TabIndex = 9;
            this.labelEngineShapeParent.Text = "Parent: ";
            // 
            // comboEngineShapeParent
            // 
            this.comboEngineShapeParent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboEngineShapeParent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.comboEngineShapeParent.Enabled = false;
            this.comboEngineShapeParent.FormattingEnabled = true;
            this.comboEngineShapeParent.Location = new System.Drawing.Point(56, 412);
            this.comboEngineShapeParent.Name = "comboEngineShapeParent";
            this.comboEngineShapeParent.Size = new System.Drawing.Size(177, 21);
            this.comboEngineShapeParent.TabIndex = 8;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.gridProblems);
            this.splitContainer2.Size = new System.Drawing.Size(958, 716);
            this.splitContainer2.SplitterDistance = 680;
            this.splitContainer2.TabIndex = 0;
            // 
            // gridProblems
            // 
            this.gridProblems.AllowUserToAddRows = false;
            this.gridProblems.AllowUserToDeleteRows = false;
            this.gridProblems.AllowUserToResizeColumns = false;
            this.gridProblems.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridProblems.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridProblems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridProblems.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridProblems.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.gridProblems.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.gridProblems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gridProblems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnProblems});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridProblems.DefaultCellStyle = dataGridViewCellStyle3;
            this.gridProblems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridProblems.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridProblems.Location = new System.Drawing.Point(0, 0);
            this.gridProblems.MultiSelect = false;
            this.gridProblems.Name = "gridProblems";
            this.gridProblems.ReadOnly = true;
            this.gridProblems.RowHeadersVisible = false;
            this.gridProblems.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridProblems.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.gridProblems.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.gridProblems.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridProblems.RowTemplate.Height = 500;
            this.gridProblems.RowTemplate.ReadOnly = true;
            this.gridProblems.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.gridProblems.Size = new System.Drawing.Size(274, 716);
            this.gridProblems.TabIndex = 0;
            this.gridProblems.SelectionChanged += new System.EventHandler(this.gridProblems_SelectionChanged);
            // 
            // columnProblems
            // 
            this.columnProblems.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.columnProblems.DefaultCellStyle = dataGridViewCellStyle2;
            this.columnProblems.HeaderText = "Problems";
            this.columnProblems.Name = "columnProblems";
            this.columnProblems.ReadOnly = true;
            this.columnProblems.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.columnProblems.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // comboPerspectiveOrtho
            // 
            this.comboPerspectiveOrtho.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboPerspectiveOrtho.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboPerspectiveOrtho.FormattingEnabled = true;
            this.comboPerspectiveOrtho.ItemHeight = 13;
            this.comboPerspectiveOrtho.Items.AddRange(new object[] {
            "Perspective",
            "Orthographic"});
            this.comboPerspectiveOrtho.Location = new System.Drawing.Point(1057, 2);
            this.comboPerspectiveOrtho.MaxDropDownItems = 1;
            this.comboPerspectiveOrtho.Name = "comboPerspectiveOrtho";
            this.comboPerspectiveOrtho.Size = new System.Drawing.Size(105, 21);
            this.comboPerspectiveOrtho.TabIndex = 5;
            this.comboPerspectiveOrtho.SelectedIndexChanged += new System.EventHandler(this.comboPerspectiveOrtho_SelectedIndexChanged);
            // 
            // labelFPS
            // 
            this.labelFPS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelFPS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.labelFPS.Location = new System.Drawing.Point(927, 2);
            this.labelFPS.Name = "labelFPS";
            this.labelFPS.Size = new System.Drawing.Size(124, 20);
            this.labelFPS.TabIndex = 7;
            this.labelFPS.Text = "0 FPS";
            this.labelFPS.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // buttonProblems
            // 
            this.buttonProblems.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonProblems.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.buttonProblems.FlatAppearance.BorderSize = 0;
            this.buttonProblems.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonProblems.Image = global::DAEnerys.Properties.Resources.flagWhite;
            this.buttonProblems.Location = new System.Drawing.Point(1168, 0);
            this.buttonProblems.Name = "buttonProblems";
            this.buttonProblems.Size = new System.Drawing.Size(44, 25);
            this.buttonProblems.TabIndex = 8;
            this.buttonProblems.UseVisualStyleBackColor = false;
            this.buttonProblems.Click += new System.EventHandler(this.buttonProblems_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1212, 741);
            this.Controls.Add(this.buttonProblems);
            this.Controls.Add(this.labelFPS);
            this.Controls.Add(this.comboPerspectiveOrtho);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.Name = "Main";
            this.Text = "DAEnerys";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabShipMeshes.ResumeLayout(false);
            this.groupShipMeshLODs.ResumeLayout(false);
            this.panelShipMesh.ResumeLayout(false);
            this.panelShipMesh.PerformLayout();
            this.tabMaterials.ResumeLayout(false);
            this.tabMaterials.PerformLayout();
            this.groupThrusterStrength.ResumeLayout(false);
            this.groupThrusterStrength.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarThrusterStrength)).EndInit();
            this.groupMaterialTextures.ResumeLayout(false);
            this.panelMaterialsList.ResumeLayout(false);
            this.panelMaterialsList.PerformLayout();
            this.tabCollisionMeshes.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabJoints.ResumeLayout(false);
            this.tabMarkers.ResumeLayout(false);
            this.splitTabMarkers.Panel1.ResumeLayout(false);
            this.splitTabMarkers.Panel2.ResumeLayout(false);
            this.splitTabMarkers.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitTabMarkers)).EndInit();
            this.splitTabMarkers.ResumeLayout(false);
            this.tabDockpaths.ResumeLayout(false);
            this.groupDockpathFlags.ResumeLayout(false);
            this.groupDockpathFlags.PerformLayout();
            this.groupDockpathSegments.ResumeLayout(false);
            this.groupDockpathSegments.PerformLayout();
            this.groupDockpathSegmentFlags.ResumeLayout(false);
            this.groupDockpathSegmentFlags.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDockpathSegments)).EndInit();
            this.groupDockpathLinks.ResumeLayout(false);
            this.groupDockpathFamilies.ResumeLayout(false);
            this.panelDockpathList.ResumeLayout(false);
            this.tabNavLights.ResumeLayout(false);
            this.groupNavLightPreview.ResumeLayout(false);
            this.groupNavLightPreview.PerformLayout();
            this.groupNavLightParameters.ResumeLayout(false);
            this.groupNavLightParameters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericNavLightDistance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericNavLightFrequency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericNavLightPhase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericNavLightSize)).EndInit();
            this.groupNavLightFlags.ResumeLayout(false);
            this.groupNavLightFlags.PerformLayout();
            this.panelNavLightList.ResumeLayout(false);
            this.tabEngineGlows.ResumeLayout(false);
            this.groupEngineGlowLODs.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabEngineShapes.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridProblems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton buttonOpen;
        private System.Windows.Forms.OpenFileDialog openColladaDialog;
        private System.Windows.Forms.SaveFileDialog saveColladaDialog;
        private System.Windows.Forms.ToolStripButton buttonSettings;
        private System.Windows.Forms.ToolStripButton buttonAbout;
        private System.Windows.Forms.ComboBox comboPerspectiveOrtho;
        private System.Windows.Forms.Label labelFPS;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabShipMeshes;
        private System.Windows.Forms.GroupBox groupShipMeshLODs;
        private System.Windows.Forms.CheckedListBox listShipMeshLODs;
        private System.Windows.Forms.Panel panelShipMesh;
        private System.Windows.Forms.CheckBox checkShipMeshDoScar;
        private System.Windows.Forms.ListBox listShipMeshes;
        private System.Windows.Forms.Label labelShipMeshParent;
        private System.Windows.Forms.ComboBox comboShipMeshParent;
        private System.Windows.Forms.TabPage tabMaterials;
        private System.Windows.Forms.GroupBox groupThrusterStrength;
        private System.Windows.Forms.TrackBar trackBarThrusterStrength;
        private System.Windows.Forms.GroupBox groupMaterialTextures;
        private System.Windows.Forms.ListBox listMaterialTextures;
        private System.Windows.Forms.Panel panelMaterialsList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboMaterialFormat;
        private System.Windows.Forms.TextBox boxMaterialShader;
        private System.Windows.Forms.ListBox listMaterials;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabCollisionMeshes;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckedListBox listCollisionMeshes;
        private System.Windows.Forms.Label labelCollisionMeshParent;
        private System.Windows.Forms.ComboBox comboCollisionMeshParent;
        private System.Windows.Forms.TabPage tabJoints;
        private System.Windows.Forms.TreeView jointsTree;
        private System.Windows.Forms.TabPage tabMarkers;
        private System.Windows.Forms.SplitContainer splitTabMarkers;
        private System.Windows.Forms.ListBox listBoxMarkers;
        private System.Windows.Forms.CheckBox checkboxDrawMarkers;
        private System.Windows.Forms.TabPage tabDockpaths;
        private System.Windows.Forms.GroupBox groupDockpathFlags;
        private System.Windows.Forms.CheckBox checkDockpathAjar;
        private System.Windows.Forms.CheckBox checkDockpathLatch;
        private System.Windows.Forms.CheckBox checkDockpathAnim;
        private System.Windows.Forms.CheckBox checkDockpathExit;
        private System.Windows.Forms.GroupBox groupDockpathSegments;
        private System.Windows.Forms.GroupBox groupDockpathSegmentFlags;
        private System.Windows.Forms.CheckBox checkDockpathSegmentFlagClip;
        private System.Windows.Forms.CheckBox checkDockpathSegmentFlagUnfocus;
        private System.Windows.Forms.CheckBox checkDockpathSegmentFlagCheck;
        private System.Windows.Forms.CheckBox checkDockpathSegmentFlagClearRes;
        private System.Windows.Forms.CheckBox checkDockpathSegmentFlagClose;
        private System.Windows.Forms.CheckBox checkDockpathSegmentFlagUseRot;
        private System.Windows.Forms.CheckBox checkDockpathSegmentFlagPlayer;
        private System.Windows.Forms.CheckBox checkDockpathSegmentFlagQueue;
        private System.Windows.Forms.Label labelDockpathSegmentSpeed;
        private System.Windows.Forms.Label labelDockpathSegmentTolerance;
        private System.Windows.Forms.TextBox boxDockpathSegmentSpeed;
        private System.Windows.Forms.TextBox boxDockpathSegmentTolerance;
        private System.Windows.Forms.TrackBar trackBarDockpathSegments;
        private System.Windows.Forms.GroupBox groupDockpathLinks;
        private System.Windows.Forms.ListBox listDockpathLinks;
        private System.Windows.Forms.GroupBox groupDockpathFamilies;
        private System.Windows.Forms.ListBox listDockpathFamilies;
        private System.Windows.Forms.Panel panelDockpathList;
        private System.Windows.Forms.CheckedListBox dockpathList;
        private System.Windows.Forms.TabPage tabNavLights;
        private System.Windows.Forms.GroupBox groupNavLightPreview;
        private System.Windows.Forms.CheckBox checkNavLightDrawRadius;
        private System.Windows.Forms.GroupBox groupNavLightParameters;
        private System.Windows.Forms.ComboBox comboNavLightType;
        private System.Windows.Forms.Label labelNavLightDistance;
        private System.Windows.Forms.NumericUpDown numericNavLightDistance;
        private System.Windows.Forms.Label labelNavLightColor;
        private System.Windows.Forms.Button buttonNavLightColor;
        private System.Windows.Forms.Label labelNavLightFrequency;
        private System.Windows.Forms.NumericUpDown numericNavLightFrequency;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericNavLightPhase;
        private System.Windows.Forms.Label labelNavLightSize;
        private System.Windows.Forms.NumericUpDown numericNavLightSize;
        private System.Windows.Forms.Label labelNavLightType;
        private System.Windows.Forms.GroupBox groupNavLightFlags;
        private System.Windows.Forms.CheckBox checkNavLightFlagHighEnd;
        private System.Windows.Forms.CheckBox checkNavLightFlagSprite;
        private System.Windows.Forms.Panel panelNavLightList;
        private System.Windows.Forms.CheckedListBox navLightList;
        private System.Windows.Forms.TabPage tabEngineGlows;
        private System.Windows.Forms.GroupBox groupEngineGlowLODs;
        private System.Windows.Forms.CheckedListBox listEngineGlowLODs;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListBox listEngineGlows;
        private System.Windows.Forms.Label labelEngineGlowParent;
        private System.Windows.Forms.ComboBox comboEngineGlowParent;
        private System.Windows.Forms.ToolStripButton buttonHotkeys;
        private System.Windows.Forms.TabPage tabEngineShapes;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckedListBox listEngineShapes;
        private System.Windows.Forms.Label labelEngineShapeParent;
        private System.Windows.Forms.ComboBox comboEngineShapeParent;
        private System.Windows.Forms.Button buttonProblems;
        public System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView gridProblems;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnProblems;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}

