using OpenTK.Graphics;

namespace HomeworldDAEEditor
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
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.buttonOpen = new System.Windows.Forms.ToolStripButton();
            this.buttonSettings = new System.Windows.Forms.ToolStripButton();
            this.buttonAbout = new System.Windows.Forms.ToolStripButton();
            this.openColladaDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveColladaDialog = new System.Windows.Forms.SaveFileDialog();
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
            this.groupMaterialTextures = new System.Windows.Forms.GroupBox();
            this.listMaterialTextures = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.comboMaterialFormat = new System.Windows.Forms.ComboBox();
            this.boxMaterialShader = new System.Windows.Forms.TextBox();
            this.listMaterials = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabGoblins = new System.Windows.Forms.TabPage();
            this.panelGoblins = new System.Windows.Forms.Panel();
            this.listGoblinMeshes = new System.Windows.Forms.CheckedListBox();
            this.checkGoblinDoScar = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboGoblinMeshParent = new System.Windows.Forms.ComboBox();
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.glControl = new OpenTK.GLControl(new GraphicsMode(32, 24, 8, 4));
            this.toolStrip.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabShipMeshes.SuspendLayout();
            this.groupShipMeshLODs.SuspendLayout();
            this.panelShipMesh.SuspendLayout();
            this.tabMaterials.SuspendLayout();
            this.groupMaterialTextures.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabGoblins.SuspendLayout();
            this.panelGoblins.SuspendLayout();
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
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonOpen,
            this.buttonSettings,
            this.buttonAbout});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1123, 25);
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
            // openColladaDialog
            // 
            this.openColladaDialog.Filter = "COLLADA-Files|*.dae|All files|*.*";
            // 
            // saveColladaDialog
            // 
            this.saveColladaDialog.DefaultExt = "dae";
            this.saveColladaDialog.Filter = "COLLADA-Files|*.dae|All files|*.*";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabShipMeshes);
            this.tabControl.Controls.Add(this.tabMaterials);
            this.tabControl.Controls.Add(this.tabGoblins);
            this.tabControl.Controls.Add(this.tabCollisionMeshes);
            this.tabControl.Controls.Add(this.tabJoints);
            this.tabControl.Controls.Add(this.tabMarkers);
            this.tabControl.Controls.Add(this.tabDockpaths);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(254, 758);
            this.tabControl.TabIndex = 0;
            // 
            // tabShipMeshes
            // 
            this.tabShipMeshes.Controls.Add(this.groupShipMeshLODs);
            this.tabShipMeshes.Controls.Add(this.panelShipMesh);
            this.tabShipMeshes.Location = new System.Drawing.Point(4, 40);
            this.tabShipMeshes.Name = "tabShipMeshes";
            this.tabShipMeshes.Padding = new System.Windows.Forms.Padding(3);
            this.tabShipMeshes.Size = new System.Drawing.Size(246, 714);
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
            this.groupShipMeshLODs.Size = new System.Drawing.Size(232, 240);
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
            this.listShipMeshLODs.Size = new System.Drawing.Size(226, 221);
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
            this.panelShipMesh.Size = new System.Drawing.Size(240, 462);
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
            this.listShipMeshes.Size = new System.Drawing.Size(234, 407);
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
            this.comboShipMeshParent.Enabled = false;
            this.comboShipMeshParent.FormattingEnabled = true;
            this.comboShipMeshParent.Location = new System.Drawing.Point(53, 412);
            this.comboShipMeshParent.Name = "comboShipMeshParent";
            this.comboShipMeshParent.Size = new System.Drawing.Size(184, 21);
            this.comboShipMeshParent.TabIndex = 8;
            // 
            // tabMaterials
            // 
            this.tabMaterials.Controls.Add(this.groupMaterialTextures);
            this.tabMaterials.Controls.Add(this.panel2);
            this.tabMaterials.Location = new System.Drawing.Point(4, 40);
            this.tabMaterials.Name = "tabMaterials";
            this.tabMaterials.Padding = new System.Windows.Forms.Padding(3);
            this.tabMaterials.Size = new System.Drawing.Size(246, 714);
            this.tabMaterials.TabIndex = 6;
            this.tabMaterials.Text = "Materials";
            this.tabMaterials.UseVisualStyleBackColor = true;
            // 
            // groupMaterialTextures
            // 
            this.groupMaterialTextures.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupMaterialTextures.Controls.Add(this.listMaterialTextures);
            this.groupMaterialTextures.Location = new System.Drawing.Point(8, 471);
            this.groupMaterialTextures.Name = "groupMaterialTextures";
            this.groupMaterialTextures.Size = new System.Drawing.Size(232, 240);
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
            this.listMaterialTextures.Size = new System.Drawing.Size(226, 221);
            this.listMaterialTextures.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.comboMaterialFormat);
            this.panel2.Controls.Add(this.boxMaterialShader);
            this.panel2.Controls.Add(this.listMaterials);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(240, 462);
            this.panel2.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 441);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Format:";
            // 
            // comboMaterialFormat
            // 
            this.comboMaterialFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboMaterialFormat.Enabled = false;
            this.comboMaterialFormat.FormattingEnabled = true;
            this.comboMaterialFormat.Location = new System.Drawing.Point(53, 438);
            this.comboMaterialFormat.Name = "comboMaterialFormat";
            this.comboMaterialFormat.Size = new System.Drawing.Size(184, 21);
            this.comboMaterialFormat.TabIndex = 12;
            // 
            // boxMaterialShader
            // 
            this.boxMaterialShader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boxMaterialShader.Location = new System.Drawing.Point(53, 412);
            this.boxMaterialShader.Name = "boxMaterialShader";
            this.boxMaterialShader.ReadOnly = true;
            this.boxMaterialShader.Size = new System.Drawing.Size(185, 20);
            this.boxMaterialShader.TabIndex = 11;
            // 
            // listMaterials
            // 
            this.listMaterials.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listMaterials.FormattingEnabled = true;
            this.listMaterials.Location = new System.Drawing.Point(3, 3);
            this.listMaterials.Name = "listMaterials";
            this.listMaterials.Size = new System.Drawing.Size(234, 407);
            this.listMaterials.TabIndex = 10;
            this.listMaterials.SelectedIndexChanged += new System.EventHandler(this.listMaterials_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 415);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Shader:";
            // 
            // tabGoblins
            // 
            this.tabGoblins.Controls.Add(this.panelGoblins);
            this.tabGoblins.Location = new System.Drawing.Point(4, 40);
            this.tabGoblins.Name = "tabGoblins";
            this.tabGoblins.Padding = new System.Windows.Forms.Padding(3);
            this.tabGoblins.Size = new System.Drawing.Size(246, 714);
            this.tabGoblins.TabIndex = 4;
            this.tabGoblins.Text = "Goblins";
            this.tabGoblins.UseVisualStyleBackColor = true;
            // 
            // panelGoblins
            // 
            this.panelGoblins.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelGoblins.Controls.Add(this.listGoblinMeshes);
            this.panelGoblins.Controls.Add(this.checkGoblinDoScar);
            this.panelGoblins.Controls.Add(this.label1);
            this.panelGoblins.Controls.Add(this.comboGoblinMeshParent);
            this.panelGoblins.Location = new System.Drawing.Point(3, 3);
            this.panelGoblins.Name = "panelGoblins";
            this.panelGoblins.Size = new System.Drawing.Size(240, 462);
            this.panelGoblins.TabIndex = 0;
            // 
            // listGoblinMeshes
            // 
            this.listGoblinMeshes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listGoblinMeshes.FormattingEnabled = true;
            this.listGoblinMeshes.Location = new System.Drawing.Point(3, 3);
            this.listGoblinMeshes.Name = "listGoblinMeshes";
            this.listGoblinMeshes.Size = new System.Drawing.Size(234, 394);
            this.listGoblinMeshes.TabIndex = 12;
            this.listGoblinMeshes.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listGoblinMeshes_ItemCheck);
            this.listGoblinMeshes.SelectedIndexChanged += new System.EventHandler(this.listGoblinMeshes_SelectedIndexChanged);
            // 
            // checkGoblinDoScar
            // 
            this.checkGoblinDoScar.AutoSize = true;
            this.checkGoblinDoScar.Enabled = false;
            this.checkGoblinDoScar.Location = new System.Drawing.Point(6, 439);
            this.checkGoblinDoScar.Name = "checkGoblinDoScar";
            this.checkGoblinDoScar.Size = new System.Drawing.Size(79, 17);
            this.checkGoblinDoScar.TabIndex = 11;
            this.checkGoblinDoScar.Text = "Allow scars";
            this.checkGoblinDoScar.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 415);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Parent: ";
            // 
            // comboGoblinMeshParent
            // 
            this.comboGoblinMeshParent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboGoblinMeshParent.Enabled = false;
            this.comboGoblinMeshParent.FormattingEnabled = true;
            this.comboGoblinMeshParent.Location = new System.Drawing.Point(56, 412);
            this.comboGoblinMeshParent.Name = "comboGoblinMeshParent";
            this.comboGoblinMeshParent.Size = new System.Drawing.Size(184, 21);
            this.comboGoblinMeshParent.TabIndex = 8;
            // 
            // tabCollisionMeshes
            // 
            this.tabCollisionMeshes.Controls.Add(this.panel1);
            this.tabCollisionMeshes.Location = new System.Drawing.Point(4, 40);
            this.tabCollisionMeshes.Name = "tabCollisionMeshes";
            this.tabCollisionMeshes.Padding = new System.Windows.Forms.Padding(3);
            this.tabCollisionMeshes.Size = new System.Drawing.Size(246, 714);
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
            this.panel1.Size = new System.Drawing.Size(240, 443);
            this.panel1.TabIndex = 0;
            // 
            // listCollisionMeshes
            // 
            this.listCollisionMeshes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listCollisionMeshes.FormattingEnabled = true;
            this.listCollisionMeshes.Location = new System.Drawing.Point(3, 3);
            this.listCollisionMeshes.Name = "listCollisionMeshes";
            this.listCollisionMeshes.Size = new System.Drawing.Size(234, 394);
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
            this.comboCollisionMeshParent.Enabled = false;
            this.comboCollisionMeshParent.FormattingEnabled = true;
            this.comboCollisionMeshParent.Location = new System.Drawing.Point(56, 412);
            this.comboCollisionMeshParent.Name = "comboCollisionMeshParent";
            this.comboCollisionMeshParent.Size = new System.Drawing.Size(184, 21);
            this.comboCollisionMeshParent.TabIndex = 8;
            // 
            // tabJoints
            // 
            this.tabJoints.Controls.Add(this.jointsTree);
            this.tabJoints.Location = new System.Drawing.Point(4, 40);
            this.tabJoints.Name = "tabJoints";
            this.tabJoints.Padding = new System.Windows.Forms.Padding(3);
            this.tabJoints.Size = new System.Drawing.Size(246, 714);
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
            this.jointsTree.Size = new System.Drawing.Size(240, 708);
            this.jointsTree.TabIndex = 0;
            this.jointsTree.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.jointsTree_AfterCheck);
            // 
            // tabMarkers
            // 
            this.tabMarkers.Controls.Add(this.splitTabMarkers);
            this.tabMarkers.Location = new System.Drawing.Point(4, 40);
            this.tabMarkers.Name = "tabMarkers";
            this.tabMarkers.Size = new System.Drawing.Size(246, 714);
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
            this.splitTabMarkers.Size = new System.Drawing.Size(246, 714);
            this.splitTabMarkers.SplitterDistance = 672;
            this.splitTabMarkers.TabIndex = 0;
            // 
            // listBoxMarkers
            // 
            this.listBoxMarkers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxMarkers.FormattingEnabled = true;
            this.listBoxMarkers.Location = new System.Drawing.Point(0, 0);
            this.listBoxMarkers.Name = "listBoxMarkers";
            this.listBoxMarkers.Size = new System.Drawing.Size(246, 672);
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
            this.tabDockpaths.Location = new System.Drawing.Point(4, 40);
            this.tabDockpaths.Name = "tabDockpaths";
            this.tabDockpaths.Size = new System.Drawing.Size(246, 714);
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
            this.groupDockpathFlags.Location = new System.Drawing.Point(4, 190);
            this.groupDockpathFlags.Name = "groupDockpathFlags";
            this.groupDockpathFlags.Size = new System.Drawing.Size(239, 64);
            this.groupDockpathFlags.TabIndex = 12;
            this.groupDockpathFlags.TabStop = false;
            this.groupDockpathFlags.Text = "Flags";
            // 
            // checkDockpathAjar
            // 
            this.checkDockpathAjar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkDockpathAjar.AutoSize = true;
            this.checkDockpathAjar.Enabled = false;
            this.checkDockpathAjar.Location = new System.Drawing.Point(180, 42);
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
            this.checkDockpathLatch.Location = new System.Drawing.Point(180, 19);
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
            this.groupDockpathSegments.Location = new System.Drawing.Point(4, 474);
            this.groupDockpathSegments.Name = "groupDockpathSegments";
            this.groupDockpathSegments.Size = new System.Drawing.Size(239, 237);
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
            this.groupDockpathSegmentFlags.Size = new System.Drawing.Size(230, 111);
            this.groupDockpathSegmentFlags.TabIndex = 11;
            this.groupDockpathSegmentFlags.TabStop = false;
            this.groupDockpathSegmentFlags.Text = "Flags";
            // 
            // checkDockpathSegmentFlagClip
            // 
            this.checkDockpathSegmentFlagClip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkDockpathSegmentFlagClip.AutoSize = true;
            this.checkDockpathSegmentFlagClip.Enabled = false;
            this.checkDockpathSegmentFlagClip.Location = new System.Drawing.Point(123, 89);
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
            this.checkDockpathSegmentFlagCheck.Location = new System.Drawing.Point(123, 66);
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
            this.checkDockpathSegmentFlagClose.Location = new System.Drawing.Point(123, 42);
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
            this.checkDockpathSegmentFlagPlayer.Location = new System.Drawing.Point(123, 19);
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
            this.boxDockpathSegmentSpeed.Size = new System.Drawing.Size(159, 20);
            this.boxDockpathSegmentSpeed.TabIndex = 8;
            // 
            // boxDockpathSegmentTolerance
            // 
            this.boxDockpathSegmentTolerance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boxDockpathSegmentTolerance.Location = new System.Drawing.Point(77, 70);
            this.boxDockpathSegmentTolerance.Name = "boxDockpathSegmentTolerance";
            this.boxDockpathSegmentTolerance.ReadOnly = true;
            this.boxDockpathSegmentTolerance.Size = new System.Drawing.Size(159, 20);
            this.boxDockpathSegmentTolerance.TabIndex = 7;
            // 
            // trackBarDockpathSegments
            // 
            this.trackBarDockpathSegments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarDockpathSegments.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.trackBarDockpathSegments.Location = new System.Drawing.Point(9, 19);
            this.trackBarDockpathSegments.Name = "trackBarDockpathSegments";
            this.trackBarDockpathSegments.Size = new System.Drawing.Size(233, 45);
            this.trackBarDockpathSegments.TabIndex = 3;
            this.trackBarDockpathSegments.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBarDockpathSegments.Scroll += new System.EventHandler(this.trackBarDockpathSegments_Scroll);
            // 
            // groupDockpathLinks
            // 
            this.groupDockpathLinks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupDockpathLinks.Controls.Add(this.listDockpathLinks);
            this.groupDockpathLinks.Location = new System.Drawing.Point(4, 367);
            this.groupDockpathLinks.Name = "groupDockpathLinks";
            this.groupDockpathLinks.Size = new System.Drawing.Size(239, 101);
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
            this.listDockpathLinks.Size = new System.Drawing.Size(233, 82);
            this.listDockpathLinks.TabIndex = 11;
            // 
            // groupDockpathFamilies
            // 
            this.groupDockpathFamilies.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupDockpathFamilies.Controls.Add(this.listDockpathFamilies);
            this.groupDockpathFamilies.Location = new System.Drawing.Point(4, 260);
            this.groupDockpathFamilies.Name = "groupDockpathFamilies";
            this.groupDockpathFamilies.Size = new System.Drawing.Size(239, 101);
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
            this.listDockpathFamilies.Size = new System.Drawing.Size(233, 82);
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
            this.panelDockpathList.Size = new System.Drawing.Size(239, 180);
            this.panelDockpathList.TabIndex = 0;
            // 
            // dockpathList
            // 
            this.dockpathList.CheckOnClick = true;
            this.dockpathList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockpathList.FormattingEnabled = true;
            this.dockpathList.Location = new System.Drawing.Point(0, 0);
            this.dockpathList.Name = "dockpathList";
            this.dockpathList.Size = new System.Drawing.Size(239, 180);
            this.dockpathList.TabIndex = 7;
            this.dockpathList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.dockpathList_ItemCheck);
            this.dockpathList.SelectedIndexChanged += new System.EventHandler(this.dockpathList_SelectedIndexChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.splitContainer1.Panel2.Controls.Add(this.glControl);
            this.splitContainer1.Size = new System.Drawing.Size(1123, 758);
            this.splitContainer1.SplitterDistance = 254;
            this.splitContainer1.TabIndex = 4;
            // 
            // glControl
            // 
            this.glControl.AutoSize = true;
            this.glControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.glControl.BackColor = System.Drawing.Color.Black;
            this.glControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glControl.Location = new System.Drawing.Point(0, 0);
            this.glControl.Name = "glControl";
            this.glControl.Size = new System.Drawing.Size(865, 758);
            this.glControl.TabIndex = 3;
            this.glControl.VSync = true;
            this.glControl.Paint += new System.Windows.Forms.PaintEventHandler(this.glControl_Render);
            this.glControl.Enter += new System.EventHandler(this.glControl_Enter);
            this.glControl.Leave += new System.EventHandler(this.glControl_Leave);
            this.glControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.glControl_MouseDown);
            this.glControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.glControl_MouseUp);
            this.glControl.Resize += new System.EventHandler(this.glControl_Resize);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1123, 783);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip);
            this.Name = "Main";
            this.Text = "Homeworld DAE Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabShipMeshes.ResumeLayout(false);
            this.groupShipMeshLODs.ResumeLayout(false);
            this.panelShipMesh.ResumeLayout(false);
            this.panelShipMesh.PerformLayout();
            this.tabMaterials.ResumeLayout(false);
            this.groupMaterialTextures.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabGoblins.ResumeLayout(false);
            this.panelGoblins.ResumeLayout(false);
            this.panelGoblins.PerformLayout();
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
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton buttonOpen;
        private System.Windows.Forms.OpenFileDialog openColladaDialog;
        private System.Windows.Forms.SaveFileDialog saveColladaDialog;
        private System.Windows.Forms.ToolStripButton buttonSettings;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabShipMeshes;
        private System.Windows.Forms.GroupBox groupShipMeshLODs;
        private System.Windows.Forms.CheckedListBox listShipMeshLODs;
        private System.Windows.Forms.Panel panelShipMesh;
        private System.Windows.Forms.CheckBox checkShipMeshDoScar;
        private System.Windows.Forms.ListBox listShipMeshes;
        private System.Windows.Forms.Label labelShipMeshParent;
        private System.Windows.Forms.ComboBox comboShipMeshParent;
        private System.Windows.Forms.TabPage tabGoblins;
        private System.Windows.Forms.Panel panelGoblins;
        private System.Windows.Forms.CheckedListBox listGoblinMeshes;
        private System.Windows.Forms.CheckBox checkGoblinDoScar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboGoblinMeshParent;
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
        private System.Windows.Forms.Panel panelDockpathList;
        private System.Windows.Forms.CheckedListBox dockpathList;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupDockpathSegments;
        private System.Windows.Forms.GroupBox groupDockpathLinks;
        private System.Windows.Forms.ListBox listDockpathLinks;
        private System.Windows.Forms.GroupBox groupDockpathFamilies;
        private System.Windows.Forms.ListBox listDockpathFamilies;
        private System.Windows.Forms.GroupBox groupDockpathFlags;
        private System.Windows.Forms.CheckBox checkDockpathAjar;
        private System.Windows.Forms.CheckBox checkDockpathLatch;
        private System.Windows.Forms.CheckBox checkDockpathAnim;
        private System.Windows.Forms.CheckBox checkDockpathExit;
        private System.Windows.Forms.TrackBar trackBarDockpathSegments;
        private System.Windows.Forms.GroupBox groupDockpathSegmentFlags;
        private System.Windows.Forms.Label labelDockpathSegmentSpeed;
        private System.Windows.Forms.Label labelDockpathSegmentTolerance;
        private System.Windows.Forms.TextBox boxDockpathSegmentSpeed;
        private System.Windows.Forms.TextBox boxDockpathSegmentTolerance;
        private System.Windows.Forms.CheckBox checkDockpathSegmentFlagClip;
        private System.Windows.Forms.CheckBox checkDockpathSegmentFlagUnfocus;
        private System.Windows.Forms.CheckBox checkDockpathSegmentFlagCheck;
        private System.Windows.Forms.CheckBox checkDockpathSegmentFlagClearRes;
        private System.Windows.Forms.CheckBox checkDockpathSegmentFlagClose;
        private System.Windows.Forms.CheckBox checkDockpathSegmentFlagUseRot;
        private System.Windows.Forms.CheckBox checkDockpathSegmentFlagPlayer;
        private System.Windows.Forms.CheckBox checkDockpathSegmentFlagQueue;
        private System.Windows.Forms.ToolStripButton buttonAbout;
        private System.Windows.Forms.TabPage tabMaterials;
        private System.Windows.Forms.GroupBox groupMaterialTextures;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox comboMaterialFormat;
        private System.Windows.Forms.TextBox boxMaterialShader;
        private System.Windows.Forms.ListBox listMaterials;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listMaterialTextures;
        private OpenTK.GLControl glControl;
    }
}

