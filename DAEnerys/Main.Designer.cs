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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.buttonNew = new System.Windows.Forms.ToolStripButton();
            this.buttonOpen = new System.Windows.Forms.ToolStripButton();
            this.buttonSave = new System.Windows.Forms.ToolStripButton();
            this.buttonSettings = new System.Windows.Forms.ToolStripButton();
            this.buttonShaderSettings = new System.Windows.Forms.ToolStripButton();
            this.buttonHotkeys = new System.Windows.Forms.ToolStripButton();
            this.buttonCheckForUpdates = new System.Windows.Forms.ToolStripButton();
            this.buttonAbout = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.openColladaDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveColladaDialog = new System.Windows.Forms.SaveFileDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabShipMeshes = new System.Windows.Forms.TabPage();
            this.buttonShipMeshRemove = new System.Windows.Forms.Button();
            this.buttonShipMeshAdd = new System.Windows.Forms.Button();
            this.boxShipMeshName = new System.Windows.Forms.TextBox();
            this.labelShipMeshName = new System.Windows.Forms.Label();
            this.checkShipMeshDoScar = new System.Windows.Forms.CheckBox();
            this.listShipMeshes = new System.Windows.Forms.ListBox();
            this.labelShipMeshParent = new System.Windows.Forms.Label();
            this.comboShipMeshParent = new System.Windows.Forms.ComboBox();
            this.groupShipMeshLODs = new System.Windows.Forms.GroupBox();
            this.buttonShipMeshLODImportDAE = new System.Windows.Forms.Button();
            this.buttonShipMeshLODRemove = new System.Windows.Forms.Button();
            this.buttonShipMeshLODExportDAE = new System.Windows.Forms.Button();
            this.buttonShipMeshLODAdd = new System.Windows.Forms.Button();
            this.groupShipMeshLODMaterials = new System.Windows.Forms.GroupBox();
            this.buttonShipMeshLODImportOBJ = new System.Windows.Forms.Button();
            this.buttonShipMeshLODExportOBJ = new System.Windows.Forms.Button();
            this.listShipMeshLODs = new System.Windows.Forms.CheckedListBox();
            this.tabMaterials = new System.Windows.Forms.TabPage();
            this.groupProgress = new System.Windows.Forms.GroupBox();
            this.trackBarProgress = new System.Windows.Forms.TrackBar();
            this.groupThrusterStrength = new System.Windows.Forms.GroupBox();
            this.trackBarThrusterStrength = new System.Windows.Forms.TrackBar();
            this.boxMaterialName = new System.Windows.Forms.TextBox();
            this.labelMaterialName = new System.Windows.Forms.Label();
            this.buttonMaterialRemove = new System.Windows.Forms.Button();
            this.buttonMaterialAdd = new System.Windows.Forms.Button();
            this.comboMaterialShader = new System.Windows.Forms.ComboBox();
            this.groupMaterialTextures = new System.Windows.Forms.GroupBox();
            this.buttonMaterialTexturesBrowseDIFF = new System.Windows.Forms.Button();
            this.listMaterialTextures = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboMaterialFormat = new System.Windows.Forms.ComboBox();
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
            this.labelDockpathAnimationIndex = new System.Windows.Forms.Label();
            this.numericDockpathAnimationIndex = new System.Windows.Forms.NumericUpDown();
            this.boxDockpathName = new System.Windows.Forms.TextBox();
            this.labelDockpathName = new System.Windows.Forms.Label();
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
            this.tabEngineBurns = new System.Windows.Forms.TabPage();
            this.comboEngineBurnParent = new System.Windows.Forms.ComboBox();
            this.labelEngineBurnParent = new System.Windows.Forms.Label();
            this.boxEngineBurnName = new System.Windows.Forms.TextBox();
            this.labelEngineBurnName = new System.Windows.Forms.Label();
            this.groupEngineBurnFlames = new System.Windows.Forms.GroupBox();
            this.numericEngineBurnSpriteIndex = new System.Windows.Forms.NumericUpDown();
            this.labelEngineBurnFlameSpriteIndex = new System.Windows.Forms.Label();
            this.trackBarEngineBurnFlames = new System.Windows.Forms.TrackBar();
            this.panel4 = new System.Windows.Forms.Panel();
            this.listEngineBurns = new System.Windows.Forms.CheckedListBox();
            this.tabAnimations = new System.Windows.Forms.TabPage();
            this.numericAnimationEndTime = new System.Windows.Forms.NumericUpDown();
            this.numericAnimationStartTime = new System.Windows.Forms.NumericUpDown();
            this.labelAnimationStartTime = new System.Windows.Forms.Label();
            this.buttonAnimationPlay = new System.Windows.Forms.Button();
            this.groupAnimationJoints = new System.Windows.Forms.GroupBox();
            this.buttonAnimationJointRemove = new System.Windows.Forms.Button();
            this.buttonAnimationJointAdd = new System.Windows.Forms.Button();
            this.listAnimationJoints = new System.Windows.Forms.ListBox();
            this.boxAnimationName = new System.Windows.Forms.TextBox();
            this.labelAnimationName = new System.Windows.Forms.Label();
            this.buttonAnimationRemove = new System.Windows.Forms.Button();
            this.buttonAnimationAdd = new System.Windows.Forms.Button();
            this.listAnimations = new System.Windows.Forms.ListBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.gridProblems = new System.Windows.Forms.DataGridView();
            this.columnProblems = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.comboPerspectiveOrtho = new System.Windows.Forms.ComboBox();
            this.labelFPS = new System.Windows.Forms.Label();
            this.buttonProblems = new System.Windows.Forms.Button();
            this.saveObjDialog = new System.Windows.Forms.SaveFileDialog();
            this.openObjDialog = new System.Windows.Forms.OpenFileDialog();
            this.browseMaterialTexturesDIFFDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveColladaMeshDialog = new System.Windows.Forms.SaveFileDialog();
            this.openColladaMeshDialog = new System.Windows.Forms.OpenFileDialog();
            this.labelAnimationEndTime = new System.Windows.Forms.Label();
            this.numericAnimationLoopEndTime = new System.Windows.Forms.NumericUpDown();
            this.numericAnimationLoopStartTime = new System.Windows.Forms.NumericUpDown();
            this.labelAnimationLoopStartTime = new System.Windows.Forms.Label();
            this.labelAnimationLoopEndTime = new System.Windows.Forms.Label();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabShipMeshes.SuspendLayout();
            this.groupShipMeshLODs.SuspendLayout();
            this.tabMaterials.SuspendLayout();
            this.groupProgress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarProgress)).BeginInit();
            this.groupThrusterStrength.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarThrusterStrength)).BeginInit();
            this.groupMaterialTextures.SuspendLayout();
            this.tabCollisionMeshes.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabJoints.SuspendLayout();
            this.tabMarkers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitTabMarkers)).BeginInit();
            this.splitTabMarkers.Panel1.SuspendLayout();
            this.splitTabMarkers.Panel2.SuspendLayout();
            this.splitTabMarkers.SuspendLayout();
            this.tabDockpaths.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericDockpathAnimationIndex)).BeginInit();
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
            this.tabEngineBurns.SuspendLayout();
            this.groupEngineBurnFlames.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericEngineBurnSpriteIndex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarEngineBurnFlames)).BeginInit();
            this.panel4.SuspendLayout();
            this.tabAnimations.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericAnimationEndTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericAnimationStartTime)).BeginInit();
            this.groupAnimationJoints.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridProblems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericAnimationLoopEndTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericAnimationLoopStartTime)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonNew,
            this.buttonOpen,
            this.buttonSave,
            this.buttonSettings,
            this.buttonShaderSettings,
            this.buttonHotkeys,
            this.buttonCheckForUpdates,
            this.buttonAbout,
            this.toolStripButton1});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1284, 25);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip";
            // 
            // buttonNew
            // 
            this.buttonNew.Image = ((System.Drawing.Image)(resources.GetObject("buttonNew.Image")));
            this.buttonNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonNew.Name = "buttonNew";
            this.buttonNew.Size = new System.Drawing.Size(51, 22);
            this.buttonNew.Text = "New";
            this.buttonNew.Click += new System.EventHandler(this.buttonNew_Click);
            // 
            // buttonOpen
            // 
            this.buttonOpen.Image = ((System.Drawing.Image)(resources.GetObject("buttonOpen.Image")));
            this.buttonOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(56, 22);
            this.buttonOpen.Text = "Open";
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Image = ((System.Drawing.Image)(resources.GetObject("buttonSave.Image")));
            this.buttonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(65, 22);
            this.buttonSave.Text = "Save as";
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonSettings
            // 
            this.buttonSettings.Image = ((System.Drawing.Image)(resources.GetObject("buttonSettings.Image")));
            this.buttonSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Size = new System.Drawing.Size(69, 22);
            this.buttonSettings.Text = "Settings";
            this.buttonSettings.Click += new System.EventHandler(this.buttonSettings_Click);
            // 
            // buttonShaderSettings
            // 
            this.buttonShaderSettings.Image = ((System.Drawing.Image)(resources.GetObject("buttonShaderSettings.Image")));
            this.buttonShaderSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonShaderSettings.Name = "buttonShaderSettings";
            this.buttonShaderSettings.Size = new System.Drawing.Size(108, 22);
            this.buttonShaderSettings.Text = "Shader Settings";
            this.buttonShaderSettings.Click += new System.EventHandler(this.buttonShaderSettings_Click);
            // 
            // buttonHotkeys
            // 
            this.buttonHotkeys.Image = ((System.Drawing.Image)(resources.GetObject("buttonHotkeys.Image")));
            this.buttonHotkeys.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonHotkeys.Name = "buttonHotkeys";
            this.buttonHotkeys.Size = new System.Drawing.Size(70, 22);
            this.buttonHotkeys.Text = "Hotkeys";
            this.buttonHotkeys.Click += new System.EventHandler(this.buttonHotkeys_Click);
            // 
            // buttonCheckForUpdates
            // 
            this.buttonCheckForUpdates.Image = ((System.Drawing.Image)(resources.GetObject("buttonCheckForUpdates.Image")));
            this.buttonCheckForUpdates.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonCheckForUpdates.Name = "buttonCheckForUpdates";
            this.buttonCheckForUpdates.Size = new System.Drawing.Size(123, 22);
            this.buttonCheckForUpdates.Text = "Check for updates";
            this.buttonCheckForUpdates.Click += new System.EventHandler(this.buttonCheckForUpdates_Click);
            // 
            // buttonAbout
            // 
            this.buttonAbout.Image = ((System.Drawing.Image)(resources.GetObject("buttonAbout.Image")));
            this.buttonAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonAbout.Name = "buttonAbout";
            this.buttonAbout.Size = new System.Drawing.Size(60, 22);
            this.buttonAbout.Text = "About";
            this.buttonAbout.Click += new System.EventHandler(this.buttonAbout_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(114, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.Visible = false;
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // openColladaDialog
            // 
            this.openColladaDialog.Filter = "COLLADA-Files|*.dae|All files|*.*";
            this.openColladaDialog.Title = "Open DAE-file...";
            // 
            // saveColladaDialog
            // 
            this.saveColladaDialog.DefaultExt = "dae";
            this.saveColladaDialog.Filter = "COLLADA-files|*.dae|All files|*.*";
            this.saveColladaDialog.Title = "Save DAE-file...";
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
            this.splitContainer1.Size = new System.Drawing.Size(1284, 836);
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
            this.tabControl.Controls.Add(this.tabEngineBurns);
            this.tabControl.Controls.Add(this.tabAnimations);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(250, 836);
            this.tabControl.TabIndex = 0;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabShipMeshes
            // 
            this.tabShipMeshes.AutoScroll = true;
            this.tabShipMeshes.Controls.Add(this.buttonShipMeshRemove);
            this.tabShipMeshes.Controls.Add(this.buttonShipMeshAdd);
            this.tabShipMeshes.Controls.Add(this.boxShipMeshName);
            this.tabShipMeshes.Controls.Add(this.labelShipMeshName);
            this.tabShipMeshes.Controls.Add(this.checkShipMeshDoScar);
            this.tabShipMeshes.Controls.Add(this.listShipMeshes);
            this.tabShipMeshes.Controls.Add(this.labelShipMeshParent);
            this.tabShipMeshes.Controls.Add(this.comboShipMeshParent);
            this.tabShipMeshes.Controls.Add(this.groupShipMeshLODs);
            this.tabShipMeshes.Location = new System.Drawing.Point(4, 76);
            this.tabShipMeshes.Name = "tabShipMeshes";
            this.tabShipMeshes.Padding = new System.Windows.Forms.Padding(3);
            this.tabShipMeshes.Size = new System.Drawing.Size(242, 756);
            this.tabShipMeshes.TabIndex = 0;
            this.tabShipMeshes.Text = "Ship Meshes";
            this.tabShipMeshes.UseVisualStyleBackColor = true;
            // 
            // buttonShipMeshRemove
            // 
            this.buttonShipMeshRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonShipMeshRemove.Enabled = false;
            this.buttonShipMeshRemove.Location = new System.Drawing.Point(113, 211);
            this.buttonShipMeshRemove.Name = "buttonShipMeshRemove";
            this.buttonShipMeshRemove.Size = new System.Drawing.Size(106, 23);
            this.buttonShipMeshRemove.TabIndex = 17;
            this.buttonShipMeshRemove.Text = "Remove";
            this.buttonShipMeshRemove.UseVisualStyleBackColor = true;
            this.buttonShipMeshRemove.Click += new System.EventHandler(this.buttonShipMeshRemove_Click);
            // 
            // buttonShipMeshAdd
            // 
            this.buttonShipMeshAdd.Location = new System.Drawing.Point(6, 211);
            this.buttonShipMeshAdd.Name = "buttonShipMeshAdd";
            this.buttonShipMeshAdd.Size = new System.Drawing.Size(98, 23);
            this.buttonShipMeshAdd.TabIndex = 16;
            this.buttonShipMeshAdd.Text = "Add";
            this.buttonShipMeshAdd.UseVisualStyleBackColor = true;
            this.buttonShipMeshAdd.Click += new System.EventHandler(this.buttonShipMeshAdd_Click);
            // 
            // boxShipMeshName
            // 
            this.boxShipMeshName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boxShipMeshName.Enabled = false;
            this.boxShipMeshName.Location = new System.Drawing.Point(56, 240);
            this.boxShipMeshName.Name = "boxShipMeshName";
            this.boxShipMeshName.Size = new System.Drawing.Size(163, 20);
            this.boxShipMeshName.TabIndex = 20;
            this.boxShipMeshName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.boxShipMeshName_KeyPress);
            this.boxShipMeshName.Leave += new System.EventHandler(this.boxShipMeshName_Leave);
            // 
            // labelShipMeshName
            // 
            this.labelShipMeshName.AutoSize = true;
            this.labelShipMeshName.Location = new System.Drawing.Point(6, 243);
            this.labelShipMeshName.Name = "labelShipMeshName";
            this.labelShipMeshName.Size = new System.Drawing.Size(41, 13);
            this.labelShipMeshName.TabIndex = 19;
            this.labelShipMeshName.Text = "Name: ";
            // 
            // checkShipMeshDoScar
            // 
            this.checkShipMeshDoScar.AutoSize = true;
            this.checkShipMeshDoScar.Enabled = false;
            this.checkShipMeshDoScar.Location = new System.Drawing.Point(9, 293);
            this.checkShipMeshDoScar.Name = "checkShipMeshDoScar";
            this.checkShipMeshDoScar.Size = new System.Drawing.Size(79, 17);
            this.checkShipMeshDoScar.TabIndex = 15;
            this.checkShipMeshDoScar.Text = "Allow scars";
            this.checkShipMeshDoScar.UseVisualStyleBackColor = true;
            this.checkShipMeshDoScar.CheckedChanged += new System.EventHandler(this.checkShipMeshDoScar_CheckedChanged);
            // 
            // listShipMeshes
            // 
            this.listShipMeshes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listShipMeshes.FormattingEnabled = true;
            this.listShipMeshes.Location = new System.Drawing.Point(6, 6);
            this.listShipMeshes.Name = "listShipMeshes";
            this.listShipMeshes.Size = new System.Drawing.Size(213, 199);
            this.listShipMeshes.TabIndex = 14;
            this.listShipMeshes.SelectedIndexChanged += new System.EventHandler(this.listShipMeshes_SelectedIndexChanged);
            // 
            // labelShipMeshParent
            // 
            this.labelShipMeshParent.AutoSize = true;
            this.labelShipMeshParent.Location = new System.Drawing.Point(6, 269);
            this.labelShipMeshParent.Name = "labelShipMeshParent";
            this.labelShipMeshParent.Size = new System.Drawing.Size(44, 13);
            this.labelShipMeshParent.TabIndex = 13;
            this.labelShipMeshParent.Text = "Parent: ";
            // 
            // comboShipMeshParent
            // 
            this.comboShipMeshParent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboShipMeshParent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboShipMeshParent.Enabled = false;
            this.comboShipMeshParent.FormattingEnabled = true;
            this.comboShipMeshParent.Location = new System.Drawing.Point(56, 266);
            this.comboShipMeshParent.Name = "comboShipMeshParent";
            this.comboShipMeshParent.Size = new System.Drawing.Size(163, 21);
            this.comboShipMeshParent.TabIndex = 12;
            this.comboShipMeshParent.SelectedIndexChanged += new System.EventHandler(this.comboShipMeshParent_SelectedIndexChanged);
            // 
            // groupShipMeshLODs
            // 
            this.groupShipMeshLODs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupShipMeshLODs.Controls.Add(this.buttonShipMeshLODImportDAE);
            this.groupShipMeshLODs.Controls.Add(this.buttonShipMeshLODRemove);
            this.groupShipMeshLODs.Controls.Add(this.buttonShipMeshLODExportDAE);
            this.groupShipMeshLODs.Controls.Add(this.buttonShipMeshLODAdd);
            this.groupShipMeshLODs.Controls.Add(this.groupShipMeshLODMaterials);
            this.groupShipMeshLODs.Controls.Add(this.buttonShipMeshLODImportOBJ);
            this.groupShipMeshLODs.Controls.Add(this.buttonShipMeshLODExportOBJ);
            this.groupShipMeshLODs.Controls.Add(this.listShipMeshLODs);
            this.groupShipMeshLODs.Location = new System.Drawing.Point(3, 316);
            this.groupShipMeshLODs.Name = "groupShipMeshLODs";
            this.groupShipMeshLODs.Size = new System.Drawing.Size(219, 439);
            this.groupShipMeshLODs.TabIndex = 1;
            this.groupShipMeshLODs.TabStop = false;
            this.groupShipMeshLODs.Text = "Level of detail(s)";
            // 
            // buttonShipMeshLODImportDAE
            // 
            this.buttonShipMeshLODImportDAE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonShipMeshLODImportDAE.Enabled = false;
            this.buttonShipMeshLODImportDAE.Location = new System.Drawing.Point(110, 130);
            this.buttonShipMeshLODImportDAE.Name = "buttonShipMeshLODImportDAE";
            this.buttonShipMeshLODImportDAE.Size = new System.Drawing.Size(106, 23);
            this.buttonShipMeshLODImportDAE.TabIndex = 7;
            this.buttonShipMeshLODImportDAE.Text = "Import from DAE";
            this.buttonShipMeshLODImportDAE.UseVisualStyleBackColor = true;
            this.buttonShipMeshLODImportDAE.Click += new System.EventHandler(this.buttonShipMeshLODImportDAE_Click);
            // 
            // buttonShipMeshLODRemove
            // 
            this.buttonShipMeshLODRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonShipMeshLODRemove.Enabled = false;
            this.buttonShipMeshLODRemove.Location = new System.Drawing.Point(110, 101);
            this.buttonShipMeshLODRemove.Name = "buttonShipMeshLODRemove";
            this.buttonShipMeshLODRemove.Size = new System.Drawing.Size(106, 23);
            this.buttonShipMeshLODRemove.TabIndex = 5;
            this.buttonShipMeshLODRemove.Text = "Remove";
            this.buttonShipMeshLODRemove.UseVisualStyleBackColor = true;
            this.buttonShipMeshLODRemove.Click += new System.EventHandler(this.buttonShipMeshLODRemove_Click);
            // 
            // buttonShipMeshLODExportDAE
            // 
            this.buttonShipMeshLODExportDAE.Enabled = false;
            this.buttonShipMeshLODExportDAE.Location = new System.Drawing.Point(3, 130);
            this.buttonShipMeshLODExportDAE.Name = "buttonShipMeshLODExportDAE";
            this.buttonShipMeshLODExportDAE.Size = new System.Drawing.Size(98, 23);
            this.buttonShipMeshLODExportDAE.TabIndex = 6;
            this.buttonShipMeshLODExportDAE.Text = "Export to DAE";
            this.buttonShipMeshLODExportDAE.UseVisualStyleBackColor = true;
            this.buttonShipMeshLODExportDAE.Click += new System.EventHandler(this.buttonShipMeshLODExportDAE_Click);
            // 
            // buttonShipMeshLODAdd
            // 
            this.buttonShipMeshLODAdd.Location = new System.Drawing.Point(3, 101);
            this.buttonShipMeshLODAdd.Name = "buttonShipMeshLODAdd";
            this.buttonShipMeshLODAdd.Size = new System.Drawing.Size(98, 23);
            this.buttonShipMeshLODAdd.TabIndex = 4;
            this.buttonShipMeshLODAdd.Text = "Add";
            this.buttonShipMeshLODAdd.UseVisualStyleBackColor = true;
            this.buttonShipMeshLODAdd.Click += new System.EventHandler(this.buttonShipMeshLODAdd_Click);
            // 
            // groupShipMeshLODMaterials
            // 
            this.groupShipMeshLODMaterials.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupShipMeshLODMaterials.AutoSize = true;
            this.groupShipMeshLODMaterials.Location = new System.Drawing.Point(3, 188);
            this.groupShipMeshLODMaterials.Name = "groupShipMeshLODMaterials";
            this.groupShipMeshLODMaterials.Size = new System.Drawing.Size(216, 249);
            this.groupShipMeshLODMaterials.TabIndex = 3;
            this.groupShipMeshLODMaterials.TabStop = false;
            this.groupShipMeshLODMaterials.Text = "Assigned materials";
            // 
            // buttonShipMeshLODImportOBJ
            // 
            this.buttonShipMeshLODImportOBJ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonShipMeshLODImportOBJ.Enabled = false;
            this.buttonShipMeshLODImportOBJ.Location = new System.Drawing.Point(110, 159);
            this.buttonShipMeshLODImportOBJ.Name = "buttonShipMeshLODImportOBJ";
            this.buttonShipMeshLODImportOBJ.Size = new System.Drawing.Size(106, 23);
            this.buttonShipMeshLODImportOBJ.TabIndex = 2;
            this.buttonShipMeshLODImportOBJ.Text = "Import from OBJ";
            this.buttonShipMeshLODImportOBJ.UseVisualStyleBackColor = true;
            this.buttonShipMeshLODImportOBJ.Click += new System.EventHandler(this.buttonShipMeshLODImportOBJ_Click);
            // 
            // buttonShipMeshLODExportOBJ
            // 
            this.buttonShipMeshLODExportOBJ.Enabled = false;
            this.buttonShipMeshLODExportOBJ.Location = new System.Drawing.Point(3, 159);
            this.buttonShipMeshLODExportOBJ.Name = "buttonShipMeshLODExportOBJ";
            this.buttonShipMeshLODExportOBJ.Size = new System.Drawing.Size(98, 23);
            this.buttonShipMeshLODExportOBJ.TabIndex = 1;
            this.buttonShipMeshLODExportOBJ.Text = "Export to OBJ";
            this.buttonShipMeshLODExportOBJ.UseVisualStyleBackColor = true;
            this.buttonShipMeshLODExportOBJ.Click += new System.EventHandler(this.buttonShipMeshLODExportOBJ_Click);
            // 
            // listShipMeshLODs
            // 
            this.listShipMeshLODs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listShipMeshLODs.FormattingEnabled = true;
            this.listShipMeshLODs.Location = new System.Drawing.Point(3, 16);
            this.listShipMeshLODs.Name = "listShipMeshLODs";
            this.listShipMeshLODs.Size = new System.Drawing.Size(213, 79);
            this.listShipMeshLODs.TabIndex = 0;
            this.listShipMeshLODs.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listShipMeshLODs_ItemCheck);
            this.listShipMeshLODs.SelectedIndexChanged += new System.EventHandler(this.listShipMeshLODs_SelectedIndexChanged);
            // 
            // tabMaterials
            // 
            this.tabMaterials.AutoScroll = true;
            this.tabMaterials.Controls.Add(this.groupProgress);
            this.tabMaterials.Controls.Add(this.groupThrusterStrength);
            this.tabMaterials.Controls.Add(this.boxMaterialName);
            this.tabMaterials.Controls.Add(this.labelMaterialName);
            this.tabMaterials.Controls.Add(this.buttonMaterialRemove);
            this.tabMaterials.Controls.Add(this.buttonMaterialAdd);
            this.tabMaterials.Controls.Add(this.comboMaterialShader);
            this.tabMaterials.Controls.Add(this.groupMaterialTextures);
            this.tabMaterials.Controls.Add(this.label3);
            this.tabMaterials.Controls.Add(this.comboMaterialFormat);
            this.tabMaterials.Controls.Add(this.listMaterials);
            this.tabMaterials.Controls.Add(this.label2);
            this.tabMaterials.Location = new System.Drawing.Point(4, 76);
            this.tabMaterials.Name = "tabMaterials";
            this.tabMaterials.Padding = new System.Windows.Forms.Padding(3);
            this.tabMaterials.Size = new System.Drawing.Size(242, 756);
            this.tabMaterials.TabIndex = 6;
            this.tabMaterials.Text = "Materials";
            this.tabMaterials.UseVisualStyleBackColor = true;
            // 
            // groupProgress
            // 
            this.groupProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupProgress.Controls.Add(this.trackBarProgress);
            this.groupProgress.Location = new System.Drawing.Point(4, 628);
            this.groupProgress.Name = "groupProgress";
            this.groupProgress.Size = new System.Drawing.Size(235, 46);
            this.groupProgress.TabIndex = 32;
            this.groupProgress.TabStop = false;
            this.groupProgress.Text = "Progress (ore shader)";
            // 
            // trackBarProgress
            // 
            this.trackBarProgress.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.trackBarProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBarProgress.Location = new System.Drawing.Point(3, 16);
            this.trackBarProgress.Maximum = 100;
            this.trackBarProgress.Name = "trackBarProgress";
            this.trackBarProgress.Size = new System.Drawing.Size(229, 27);
            this.trackBarProgress.TabIndex = 0;
            this.trackBarProgress.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarProgress.Scroll += new System.EventHandler(this.trackBarProgress_Scroll);
            // 
            // groupThrusterStrength
            // 
            this.groupThrusterStrength.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupThrusterStrength.Controls.Add(this.trackBarThrusterStrength);
            this.groupThrusterStrength.Location = new System.Drawing.Point(4, 579);
            this.groupThrusterStrength.Name = "groupThrusterStrength";
            this.groupThrusterStrength.Size = new System.Drawing.Size(235, 46);
            this.groupThrusterStrength.TabIndex = 31;
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
            this.trackBarThrusterStrength.Size = new System.Drawing.Size(229, 27);
            this.trackBarThrusterStrength.TabIndex = 0;
            this.trackBarThrusterStrength.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarThrusterStrength.Value = 100;
            this.trackBarThrusterStrength.Scroll += new System.EventHandler(this.trackBarThrusterStrength_Scroll);
            // 
            // boxMaterialName
            // 
            this.boxMaterialName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boxMaterialName.Enabled = false;
            this.boxMaterialName.Location = new System.Drawing.Point(50, 279);
            this.boxMaterialName.Name = "boxMaterialName";
            this.boxMaterialName.Size = new System.Drawing.Size(185, 20);
            this.boxMaterialName.TabIndex = 30;
            this.boxMaterialName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.boxMaterialName_KeyPress);
            this.boxMaterialName.Leave += new System.EventHandler(this.boxMaterialName_Leave);
            // 
            // labelMaterialName
            // 
            this.labelMaterialName.AutoSize = true;
            this.labelMaterialName.Location = new System.Drawing.Point(8, 282);
            this.labelMaterialName.Name = "labelMaterialName";
            this.labelMaterialName.Size = new System.Drawing.Size(41, 13);
            this.labelMaterialName.TabIndex = 29;
            this.labelMaterialName.Text = "Name: ";
            // 
            // buttonMaterialRemove
            // 
            this.buttonMaterialRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMaterialRemove.Enabled = false;
            this.buttonMaterialRemove.Location = new System.Drawing.Point(123, 250);
            this.buttonMaterialRemove.Name = "buttonMaterialRemove";
            this.buttonMaterialRemove.Size = new System.Drawing.Size(112, 23);
            this.buttonMaterialRemove.TabIndex = 28;
            this.buttonMaterialRemove.Text = "Remove";
            this.buttonMaterialRemove.UseVisualStyleBackColor = true;
            this.buttonMaterialRemove.Click += new System.EventHandler(this.buttonMaterialRemove_Click);
            // 
            // buttonMaterialAdd
            // 
            this.buttonMaterialAdd.Location = new System.Drawing.Point(6, 250);
            this.buttonMaterialAdd.Name = "buttonMaterialAdd";
            this.buttonMaterialAdd.Size = new System.Drawing.Size(111, 23);
            this.buttonMaterialAdd.TabIndex = 27;
            this.buttonMaterialAdd.Text = "Add";
            this.buttonMaterialAdd.UseVisualStyleBackColor = true;
            this.buttonMaterialAdd.Click += new System.EventHandler(this.buttonMaterialAdd_Click);
            // 
            // comboMaterialShader
            // 
            this.comboMaterialShader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboMaterialShader.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboMaterialShader.Enabled = false;
            this.comboMaterialShader.FormattingEnabled = true;
            this.comboMaterialShader.Location = new System.Drawing.Point(50, 305);
            this.comboMaterialShader.Name = "comboMaterialShader";
            this.comboMaterialShader.Size = new System.Drawing.Size(185, 21);
            this.comboMaterialShader.TabIndex = 26;
            this.comboMaterialShader.SelectedIndexChanged += new System.EventHandler(this.comboMaterialShader_SelectedIndexChanged);
            // 
            // groupMaterialTextures
            // 
            this.groupMaterialTextures.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupMaterialTextures.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupMaterialTextures.Controls.Add(this.buttonMaterialTexturesBrowseDIFF);
            this.groupMaterialTextures.Controls.Add(this.listMaterialTextures);
            this.groupMaterialTextures.Location = new System.Drawing.Point(4, 359);
            this.groupMaterialTextures.Name = "groupMaterialTextures";
            this.groupMaterialTextures.Size = new System.Drawing.Size(235, 214);
            this.groupMaterialTextures.TabIndex = 23;
            this.groupMaterialTextures.TabStop = false;
            this.groupMaterialTextures.Text = "Textures";
            // 
            // buttonMaterialTexturesBrowseDIFF
            // 
            this.buttonMaterialTexturesBrowseDIFF.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMaterialTexturesBrowseDIFF.Enabled = false;
            this.buttonMaterialTexturesBrowseDIFF.Location = new System.Drawing.Point(3, 182);
            this.buttonMaterialTexturesBrowseDIFF.Name = "buttonMaterialTexturesBrowseDIFF";
            this.buttonMaterialTexturesBrowseDIFF.Size = new System.Drawing.Size(229, 23);
            this.buttonMaterialTexturesBrowseDIFF.TabIndex = 28;
            this.buttonMaterialTexturesBrowseDIFF.Text = "Browse DIFF...";
            this.buttonMaterialTexturesBrowseDIFF.UseVisualStyleBackColor = true;
            this.buttonMaterialTexturesBrowseDIFF.Click += new System.EventHandler(this.buttonMaterialTexturesBrowseDIFF_Click);
            // 
            // listMaterialTextures
            // 
            this.listMaterialTextures.Dock = System.Windows.Forms.DockStyle.Top;
            this.listMaterialTextures.FormattingEnabled = true;
            this.listMaterialTextures.Location = new System.Drawing.Point(3, 16);
            this.listMaterialTextures.Name = "listMaterialTextures";
            this.listMaterialTextures.Size = new System.Drawing.Size(229, 160);
            this.listMaterialTextures.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 335);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Format:";
            // 
            // comboMaterialFormat
            // 
            this.comboMaterialFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboMaterialFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboMaterialFormat.Enabled = false;
            this.comboMaterialFormat.FormattingEnabled = true;
            this.comboMaterialFormat.Location = new System.Drawing.Point(50, 332);
            this.comboMaterialFormat.Name = "comboMaterialFormat";
            this.comboMaterialFormat.Size = new System.Drawing.Size(185, 21);
            this.comboMaterialFormat.TabIndex = 21;
            this.comboMaterialFormat.SelectedIndexChanged += new System.EventHandler(this.comboMaterialFormat_SelectedIndexChanged);
            // 
            // listMaterials
            // 
            this.listMaterials.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listMaterials.FormattingEnabled = true;
            this.listMaterials.Location = new System.Drawing.Point(6, 6);
            this.listMaterials.Name = "listMaterials";
            this.listMaterials.Size = new System.Drawing.Size(229, 238);
            this.listMaterials.TabIndex = 19;
            this.listMaterials.SelectedIndexChanged += new System.EventHandler(this.listMaterials_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 308);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Shader:";
            // 
            // tabCollisionMeshes
            // 
            this.tabCollisionMeshes.AutoScroll = true;
            this.tabCollisionMeshes.Controls.Add(this.panel1);
            this.tabCollisionMeshes.Location = new System.Drawing.Point(4, 76);
            this.tabCollisionMeshes.Name = "tabCollisionMeshes";
            this.tabCollisionMeshes.Padding = new System.Windows.Forms.Padding(3);
            this.tabCollisionMeshes.Size = new System.Drawing.Size(242, 756);
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
            this.tabJoints.AutoScroll = true;
            this.tabJoints.Controls.Add(this.jointsTree);
            this.tabJoints.Location = new System.Drawing.Point(4, 76);
            this.tabJoints.Name = "tabJoints";
            this.tabJoints.Padding = new System.Windows.Forms.Padding(3);
            this.tabJoints.Size = new System.Drawing.Size(242, 756);
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
            this.jointsTree.Size = new System.Drawing.Size(236, 750);
            this.jointsTree.TabIndex = 0;
            this.jointsTree.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.jointsTree_AfterCheck);
            // 
            // tabMarkers
            // 
            this.tabMarkers.Controls.Add(this.splitTabMarkers);
            this.tabMarkers.Location = new System.Drawing.Point(4, 76);
            this.tabMarkers.Name = "tabMarkers";
            this.tabMarkers.Size = new System.Drawing.Size(242, 756);
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
            this.splitTabMarkers.Size = new System.Drawing.Size(242, 756);
            this.splitTabMarkers.SplitterDistance = 705;
            this.splitTabMarkers.TabIndex = 0;
            // 
            // listBoxMarkers
            // 
            this.listBoxMarkers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxMarkers.FormattingEnabled = true;
            this.listBoxMarkers.Location = new System.Drawing.Point(0, 0);
            this.listBoxMarkers.Name = "listBoxMarkers";
            this.listBoxMarkers.Size = new System.Drawing.Size(242, 705);
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
            this.tabDockpaths.Controls.Add(this.labelDockpathAnimationIndex);
            this.tabDockpaths.Controls.Add(this.numericDockpathAnimationIndex);
            this.tabDockpaths.Controls.Add(this.boxDockpathName);
            this.tabDockpaths.Controls.Add(this.labelDockpathName);
            this.tabDockpaths.Controls.Add(this.groupDockpathFlags);
            this.tabDockpaths.Controls.Add(this.groupDockpathSegments);
            this.tabDockpaths.Controls.Add(this.groupDockpathLinks);
            this.tabDockpaths.Controls.Add(this.groupDockpathFamilies);
            this.tabDockpaths.Controls.Add(this.panelDockpathList);
            this.tabDockpaths.Location = new System.Drawing.Point(4, 76);
            this.tabDockpaths.Name = "tabDockpaths";
            this.tabDockpaths.Size = new System.Drawing.Size(242, 756);
            this.tabDockpaths.TabIndex = 3;
            this.tabDockpaths.Text = "Dockpaths";
            this.tabDockpaths.UseVisualStyleBackColor = true;
            // 
            // labelDockpathAnimationIndex
            // 
            this.labelDockpathAnimationIndex.AutoSize = true;
            this.labelDockpathAnimationIndex.Location = new System.Drawing.Point(3, 197);
            this.labelDockpathAnimationIndex.Name = "labelDockpathAnimationIndex";
            this.labelDockpathAnimationIndex.Size = new System.Drawing.Size(84, 13);
            this.labelDockpathAnimationIndex.TabIndex = 24;
            this.labelDockpathAnimationIndex.Text = "Animation index:";
            // 
            // numericDockpathAnimationIndex
            // 
            this.numericDockpathAnimationIndex.Enabled = false;
            this.numericDockpathAnimationIndex.Location = new System.Drawing.Point(93, 195);
            this.numericDockpathAnimationIndex.Name = "numericDockpathAnimationIndex";
            this.numericDockpathAnimationIndex.Size = new System.Drawing.Size(41, 20);
            this.numericDockpathAnimationIndex.TabIndex = 23;
            // 
            // boxDockpathName
            // 
            this.boxDockpathName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boxDockpathName.Enabled = false;
            this.boxDockpathName.Location = new System.Drawing.Point(50, 169);
            this.boxDockpathName.Name = "boxDockpathName";
            this.boxDockpathName.Size = new System.Drawing.Size(189, 20);
            this.boxDockpathName.TabIndex = 22;
            // 
            // labelDockpathName
            // 
            this.labelDockpathName.AutoSize = true;
            this.labelDockpathName.Location = new System.Drawing.Point(3, 172);
            this.labelDockpathName.Name = "labelDockpathName";
            this.labelDockpathName.Size = new System.Drawing.Size(41, 13);
            this.labelDockpathName.TabIndex = 21;
            this.labelDockpathName.Text = "Name: ";
            // 
            // groupDockpathFlags
            // 
            this.groupDockpathFlags.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupDockpathFlags.Controls.Add(this.checkDockpathAjar);
            this.groupDockpathFlags.Controls.Add(this.checkDockpathLatch);
            this.groupDockpathFlags.Controls.Add(this.checkDockpathAnim);
            this.groupDockpathFlags.Controls.Add(this.checkDockpathExit);
            this.groupDockpathFlags.Location = new System.Drawing.Point(3, 232);
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
            this.groupDockpathSegments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupDockpathSegments.Controls.Add(this.groupDockpathSegmentFlags);
            this.groupDockpathSegments.Controls.Add(this.labelDockpathSegmentSpeed);
            this.groupDockpathSegments.Controls.Add(this.labelDockpathSegmentTolerance);
            this.groupDockpathSegments.Controls.Add(this.boxDockpathSegmentSpeed);
            this.groupDockpathSegments.Controls.Add(this.boxDockpathSegmentTolerance);
            this.groupDockpathSegments.Controls.Add(this.trackBarDockpathSegments);
            this.groupDockpathSegments.Location = new System.Drawing.Point(2, 516);
            this.groupDockpathSegments.Name = "groupDockpathSegments";
            this.groupDockpathSegments.Size = new System.Drawing.Size(236, 236);
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
            this.groupDockpathSegmentFlags.Location = new System.Drawing.Point(4, 119);
            this.groupDockpathSegmentFlags.Name = "groupDockpathSegmentFlags";
            this.groupDockpathSegmentFlags.Size = new System.Drawing.Size(227, 111);
            this.groupDockpathSegmentFlags.TabIndex = 11;
            this.groupDockpathSegmentFlags.TabStop = false;
            this.groupDockpathSegmentFlags.Text = "Flags";
            // 
            // checkDockpathSegmentFlagClip
            // 
            this.checkDockpathSegmentFlagClip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkDockpathSegmentFlagClip.AutoSize = true;
            this.checkDockpathSegmentFlagClip.Enabled = false;
            this.checkDockpathSegmentFlagClip.Location = new System.Drawing.Point(120, 89);
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
            this.checkDockpathSegmentFlagCheck.Location = new System.Drawing.Point(120, 66);
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
            this.checkDockpathSegmentFlagClose.Location = new System.Drawing.Point(120, 42);
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
            this.checkDockpathSegmentFlagPlayer.Location = new System.Drawing.Point(120, 19);
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
            this.boxDockpathSegmentSpeed.Size = new System.Drawing.Size(156, 20);
            this.boxDockpathSegmentSpeed.TabIndex = 8;
            // 
            // boxDockpathSegmentTolerance
            // 
            this.boxDockpathSegmentTolerance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boxDockpathSegmentTolerance.Location = new System.Drawing.Point(77, 70);
            this.boxDockpathSegmentTolerance.Name = "boxDockpathSegmentTolerance";
            this.boxDockpathSegmentTolerance.ReadOnly = true;
            this.boxDockpathSegmentTolerance.Size = new System.Drawing.Size(156, 20);
            this.boxDockpathSegmentTolerance.TabIndex = 7;
            // 
            // trackBarDockpathSegments
            // 
            this.trackBarDockpathSegments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarDockpathSegments.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.trackBarDockpathSegments.Location = new System.Drawing.Point(8, 16);
            this.trackBarDockpathSegments.Name = "trackBarDockpathSegments";
            this.trackBarDockpathSegments.Size = new System.Drawing.Size(225, 45);
            this.trackBarDockpathSegments.TabIndex = 3;
            this.trackBarDockpathSegments.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBarDockpathSegments.Scroll += new System.EventHandler(this.trackBarDockpathSegments_Scroll);
            // 
            // groupDockpathLinks
            // 
            this.groupDockpathLinks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupDockpathLinks.Controls.Add(this.listDockpathLinks);
            this.groupDockpathLinks.Location = new System.Drawing.Point(3, 409);
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
            this.groupDockpathFamilies.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupDockpathFamilies.Controls.Add(this.listDockpathFamilies);
            this.groupDockpathFamilies.Location = new System.Drawing.Point(3, 302);
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
            this.panelDockpathList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDockpathList.Controls.Add(this.dockpathList);
            this.panelDockpathList.Location = new System.Drawing.Point(4, 4);
            this.panelDockpathList.Name = "panelDockpathList";
            this.panelDockpathList.Size = new System.Drawing.Size(235, 159);
            this.panelDockpathList.TabIndex = 0;
            // 
            // dockpathList
            // 
            this.dockpathList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockpathList.FormattingEnabled = true;
            this.dockpathList.Location = new System.Drawing.Point(0, 0);
            this.dockpathList.Name = "dockpathList";
            this.dockpathList.Size = new System.Drawing.Size(235, 159);
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
            this.tabNavLights.Location = new System.Drawing.Point(4, 76);
            this.tabNavLights.Name = "tabNavLights";
            this.tabNavLights.Size = new System.Drawing.Size(242, 756);
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
            this.groupNavLightPreview.Size = new System.Drawing.Size(226, 145);
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
            this.tabEngineGlows.AutoScroll = true;
            this.tabEngineGlows.Controls.Add(this.groupEngineGlowLODs);
            this.tabEngineGlows.Controls.Add(this.panel2);
            this.tabEngineGlows.Location = new System.Drawing.Point(4, 76);
            this.tabEngineGlows.Name = "tabEngineGlows";
            this.tabEngineGlows.Padding = new System.Windows.Forms.Padding(3);
            this.tabEngineGlows.Size = new System.Drawing.Size(242, 756);
            this.tabEngineGlows.TabIndex = 8;
            this.tabEngineGlows.Text = "Engine Glows";
            this.tabEngineGlows.UseVisualStyleBackColor = true;
            // 
            // groupEngineGlowLODs
            // 
            this.groupEngineGlowLODs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupEngineGlowLODs.Controls.Add(this.listEngineGlowLODs);
            this.groupEngineGlowLODs.Location = new System.Drawing.Point(3, 471);
            this.groupEngineGlowLODs.Name = "groupEngineGlowLODs";
            this.groupEngineGlowLODs.Size = new System.Drawing.Size(218, 219);
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
            this.listEngineGlowLODs.Size = new System.Drawing.Size(212, 200);
            this.listEngineGlowLODs.TabIndex = 0;
            this.listEngineGlowLODs.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listEngineGlowLODs_ItemCheck);
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
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
            this.tabEngineShapes.AutoScroll = true;
            this.tabEngineShapes.Controls.Add(this.panel3);
            this.tabEngineShapes.Location = new System.Drawing.Point(4, 76);
            this.tabEngineShapes.Name = "tabEngineShapes";
            this.tabEngineShapes.Padding = new System.Windows.Forms.Padding(3);
            this.tabEngineShapes.Size = new System.Drawing.Size(242, 756);
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
            // tabEngineBurns
            // 
            this.tabEngineBurns.AutoScroll = true;
            this.tabEngineBurns.Controls.Add(this.comboEngineBurnParent);
            this.tabEngineBurns.Controls.Add(this.labelEngineBurnParent);
            this.tabEngineBurns.Controls.Add(this.boxEngineBurnName);
            this.tabEngineBurns.Controls.Add(this.labelEngineBurnName);
            this.tabEngineBurns.Controls.Add(this.groupEngineBurnFlames);
            this.tabEngineBurns.Controls.Add(this.panel4);
            this.tabEngineBurns.Location = new System.Drawing.Point(4, 76);
            this.tabEngineBurns.Name = "tabEngineBurns";
            this.tabEngineBurns.Size = new System.Drawing.Size(242, 756);
            this.tabEngineBurns.TabIndex = 10;
            this.tabEngineBurns.Text = "Engine Burns";
            this.tabEngineBurns.UseVisualStyleBackColor = true;
            // 
            // comboEngineBurnParent
            // 
            this.comboEngineBurnParent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboEngineBurnParent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboEngineBurnParent.Enabled = false;
            this.comboEngineBurnParent.FormattingEnabled = true;
            this.comboEngineBurnParent.Location = new System.Drawing.Point(47, 195);
            this.comboEngineBurnParent.Name = "comboEngineBurnParent";
            this.comboEngineBurnParent.Size = new System.Drawing.Size(190, 21);
            this.comboEngineBurnParent.TabIndex = 15;
            // 
            // labelEngineBurnParent
            // 
            this.labelEngineBurnParent.AutoSize = true;
            this.labelEngineBurnParent.Location = new System.Drawing.Point(3, 198);
            this.labelEngineBurnParent.Name = "labelEngineBurnParent";
            this.labelEngineBurnParent.Size = new System.Drawing.Size(41, 13);
            this.labelEngineBurnParent.TabIndex = 6;
            this.labelEngineBurnParent.Text = "Parent:";
            // 
            // boxEngineBurnName
            // 
            this.boxEngineBurnName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boxEngineBurnName.Enabled = false;
            this.boxEngineBurnName.Location = new System.Drawing.Point(47, 169);
            this.boxEngineBurnName.Name = "boxEngineBurnName";
            this.boxEngineBurnName.Size = new System.Drawing.Size(190, 20);
            this.boxEngineBurnName.TabIndex = 5;
            // 
            // labelEngineBurnName
            // 
            this.labelEngineBurnName.AutoSize = true;
            this.labelEngineBurnName.Location = new System.Drawing.Point(3, 172);
            this.labelEngineBurnName.Name = "labelEngineBurnName";
            this.labelEngineBurnName.Size = new System.Drawing.Size(38, 13);
            this.labelEngineBurnName.TabIndex = 4;
            this.labelEngineBurnName.Text = "Name:";
            // 
            // groupEngineBurnFlames
            // 
            this.groupEngineBurnFlames.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupEngineBurnFlames.Controls.Add(this.numericEngineBurnSpriteIndex);
            this.groupEngineBurnFlames.Controls.Add(this.labelEngineBurnFlameSpriteIndex);
            this.groupEngineBurnFlames.Controls.Add(this.trackBarEngineBurnFlames);
            this.groupEngineBurnFlames.Location = new System.Drawing.Point(4, 222);
            this.groupEngineBurnFlames.Name = "groupEngineBurnFlames";
            this.groupEngineBurnFlames.Size = new System.Drawing.Size(233, 98);
            this.groupEngineBurnFlames.TabIndex = 3;
            this.groupEngineBurnFlames.TabStop = false;
            this.groupEngineBurnFlames.Text = "Flames";
            // 
            // numericEngineBurnSpriteIndex
            // 
            this.numericEngineBurnSpriteIndex.Enabled = false;
            this.numericEngineBurnSpriteIndex.Location = new System.Drawing.Point(77, 71);
            this.numericEngineBurnSpriteIndex.Name = "numericEngineBurnSpriteIndex";
            this.numericEngineBurnSpriteIndex.Size = new System.Drawing.Size(41, 20);
            this.numericEngineBurnSpriteIndex.TabIndex = 10;
            // 
            // labelEngineBurnFlameSpriteIndex
            // 
            this.labelEngineBurnFlameSpriteIndex.AutoSize = true;
            this.labelEngineBurnFlameSpriteIndex.Location = new System.Drawing.Point(6, 73);
            this.labelEngineBurnFlameSpriteIndex.Name = "labelEngineBurnFlameSpriteIndex";
            this.labelEngineBurnFlameSpriteIndex.Size = new System.Drawing.Size(65, 13);
            this.labelEngineBurnFlameSpriteIndex.TabIndex = 9;
            this.labelEngineBurnFlameSpriteIndex.Text = "Sprite index:";
            // 
            // trackBarEngineBurnFlames
            // 
            this.trackBarEngineBurnFlames.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarEngineBurnFlames.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.trackBarEngineBurnFlames.Enabled = false;
            this.trackBarEngineBurnFlames.Location = new System.Drawing.Point(8, 16);
            this.trackBarEngineBurnFlames.Name = "trackBarEngineBurnFlames";
            this.trackBarEngineBurnFlames.Size = new System.Drawing.Size(223, 45);
            this.trackBarEngineBurnFlames.TabIndex = 3;
            this.trackBarEngineBurnFlames.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBarEngineBurnFlames.Scroll += new System.EventHandler(this.trackBarEngineBurnFlames_Scroll);
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.Controls.Add(this.listEngineBurns);
            this.panel4.Location = new System.Drawing.Point(4, 4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(235, 159);
            this.panel4.TabIndex = 0;
            // 
            // listEngineBurns
            // 
            this.listEngineBurns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listEngineBurns.FormattingEnabled = true;
            this.listEngineBurns.Location = new System.Drawing.Point(0, 0);
            this.listEngineBurns.Name = "listEngineBurns";
            this.listEngineBurns.Size = new System.Drawing.Size(235, 159);
            this.listEngineBurns.TabIndex = 7;
            this.listEngineBurns.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listEngineBurns_ItemCheck);
            this.listEngineBurns.SelectedIndexChanged += new System.EventHandler(this.listEngineBurns_SelectedIndexChanged);
            // 
            // tabAnimations
            // 
            this.tabAnimations.Controls.Add(this.labelAnimationLoopEndTime);
            this.tabAnimations.Controls.Add(this.labelAnimationLoopStartTime);
            this.tabAnimations.Controls.Add(this.numericAnimationLoopEndTime);
            this.tabAnimations.Controls.Add(this.numericAnimationLoopStartTime);
            this.tabAnimations.Controls.Add(this.labelAnimationEndTime);
            this.tabAnimations.Controls.Add(this.numericAnimationEndTime);
            this.tabAnimations.Controls.Add(this.numericAnimationStartTime);
            this.tabAnimations.Controls.Add(this.labelAnimationStartTime);
            this.tabAnimations.Controls.Add(this.buttonAnimationPlay);
            this.tabAnimations.Controls.Add(this.groupAnimationJoints);
            this.tabAnimations.Controls.Add(this.boxAnimationName);
            this.tabAnimations.Controls.Add(this.labelAnimationName);
            this.tabAnimations.Controls.Add(this.buttonAnimationRemove);
            this.tabAnimations.Controls.Add(this.buttonAnimationAdd);
            this.tabAnimations.Controls.Add(this.listAnimations);
            this.tabAnimations.Location = new System.Drawing.Point(4, 76);
            this.tabAnimations.Name = "tabAnimations";
            this.tabAnimations.Size = new System.Drawing.Size(242, 756);
            this.tabAnimations.TabIndex = 11;
            this.tabAnimations.Text = "Animations";
            this.tabAnimations.UseVisualStyleBackColor = true;
            // 
            // numericAnimationEndTime
            // 
            this.numericAnimationEndTime.DecimalPlaces = 3;
            this.numericAnimationEndTime.Enabled = false;
            this.numericAnimationEndTime.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericAnimationEndTime.Location = new System.Drawing.Point(66, 224);
            this.numericAnimationEndTime.Name = "numericAnimationEndTime";
            this.numericAnimationEndTime.Size = new System.Drawing.Size(173, 20);
            this.numericAnimationEndTime.TabIndex = 40;
            this.numericAnimationEndTime.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            // 
            // numericAnimationStartTime
            // 
            this.numericAnimationStartTime.DecimalPlaces = 3;
            this.numericAnimationStartTime.Enabled = false;
            this.numericAnimationStartTime.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericAnimationStartTime.Location = new System.Drawing.Point(66, 198);
            this.numericAnimationStartTime.Name = "numericAnimationStartTime";
            this.numericAnimationStartTime.Size = new System.Drawing.Size(173, 20);
            this.numericAnimationStartTime.TabIndex = 39;
            this.numericAnimationStartTime.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            // 
            // labelAnimationStartTime
            // 
            this.labelAnimationStartTime.AutoSize = true;
            this.labelAnimationStartTime.Location = new System.Drawing.Point(3, 200);
            this.labelAnimationStartTime.Name = "labelAnimationStartTime";
            this.labelAnimationStartTime.Size = new System.Drawing.Size(32, 13);
            this.labelAnimationStartTime.TabIndex = 38;
            this.labelAnimationStartTime.Text = "Start:";
            // 
            // buttonAnimationPlay
            // 
            this.buttonAnimationPlay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAnimationPlay.Enabled = false;
            this.buttonAnimationPlay.Location = new System.Drawing.Point(3, 302);
            this.buttonAnimationPlay.Name = "buttonAnimationPlay";
            this.buttonAnimationPlay.Size = new System.Drawing.Size(236, 23);
            this.buttonAnimationPlay.TabIndex = 36;
            this.buttonAnimationPlay.Text = "Play";
            this.buttonAnimationPlay.UseVisualStyleBackColor = true;
            this.buttonAnimationPlay.Click += new System.EventHandler(this.buttonAnimationPlay_Click);
            // 
            // groupAnimationJoints
            // 
            this.groupAnimationJoints.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupAnimationJoints.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupAnimationJoints.Controls.Add(this.buttonAnimationJointRemove);
            this.groupAnimationJoints.Controls.Add(this.buttonAnimationJointAdd);
            this.groupAnimationJoints.Controls.Add(this.listAnimationJoints);
            this.groupAnimationJoints.Location = new System.Drawing.Point(3, 331);
            this.groupAnimationJoints.Name = "groupAnimationJoints";
            this.groupAnimationJoints.Size = new System.Drawing.Size(236, 186);
            this.groupAnimationJoints.TabIndex = 35;
            this.groupAnimationJoints.TabStop = false;
            this.groupAnimationJoints.Text = "Animated joints";
            // 
            // buttonAnimationJointRemove
            // 
            this.buttonAnimationJointRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAnimationJointRemove.Enabled = false;
            this.buttonAnimationJointRemove.Location = new System.Drawing.Point(120, 159);
            this.buttonAnimationJointRemove.Name = "buttonAnimationJointRemove";
            this.buttonAnimationJointRemove.Size = new System.Drawing.Size(116, 23);
            this.buttonAnimationJointRemove.TabIndex = 38;
            this.buttonAnimationJointRemove.Text = "Remove";
            this.buttonAnimationJointRemove.UseVisualStyleBackColor = true;
            // 
            // buttonAnimationJointAdd
            // 
            this.buttonAnimationJointAdd.Enabled = false;
            this.buttonAnimationJointAdd.Location = new System.Drawing.Point(0, 159);
            this.buttonAnimationJointAdd.Name = "buttonAnimationJointAdd";
            this.buttonAnimationJointAdd.Size = new System.Drawing.Size(114, 23);
            this.buttonAnimationJointAdd.TabIndex = 37;
            this.buttonAnimationJointAdd.Text = "Add";
            this.buttonAnimationJointAdd.UseVisualStyleBackColor = true;
            // 
            // listAnimationJoints
            // 
            this.listAnimationJoints.FormattingEnabled = true;
            this.listAnimationJoints.Location = new System.Drawing.Point(0, 19);
            this.listAnimationJoints.Name = "listAnimationJoints";
            this.listAnimationJoints.Size = new System.Drawing.Size(236, 134);
            this.listAnimationJoints.TabIndex = 4;
            // 
            // boxAnimationName
            // 
            this.boxAnimationName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boxAnimationName.Enabled = false;
            this.boxAnimationName.Location = new System.Drawing.Point(66, 172);
            this.boxAnimationName.Name = "boxAnimationName";
            this.boxAnimationName.Size = new System.Drawing.Size(173, 20);
            this.boxAnimationName.TabIndex = 34;
            // 
            // labelAnimationName
            // 
            this.labelAnimationName.AutoSize = true;
            this.labelAnimationName.Location = new System.Drawing.Point(3, 175);
            this.labelAnimationName.Name = "labelAnimationName";
            this.labelAnimationName.Size = new System.Drawing.Size(41, 13);
            this.labelAnimationName.TabIndex = 33;
            this.labelAnimationName.Text = "Name: ";
            // 
            // buttonAnimationRemove
            // 
            this.buttonAnimationRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAnimationRemove.Enabled = false;
            this.buttonAnimationRemove.Location = new System.Drawing.Point(123, 143);
            this.buttonAnimationRemove.Name = "buttonAnimationRemove";
            this.buttonAnimationRemove.Size = new System.Drawing.Size(116, 23);
            this.buttonAnimationRemove.TabIndex = 32;
            this.buttonAnimationRemove.Text = "Remove";
            this.buttonAnimationRemove.UseVisualStyleBackColor = true;
            // 
            // buttonAnimationAdd
            // 
            this.buttonAnimationAdd.Enabled = false;
            this.buttonAnimationAdd.Location = new System.Drawing.Point(3, 143);
            this.buttonAnimationAdd.Name = "buttonAnimationAdd";
            this.buttonAnimationAdd.Size = new System.Drawing.Size(114, 23);
            this.buttonAnimationAdd.TabIndex = 31;
            this.buttonAnimationAdd.Text = "Add";
            this.buttonAnimationAdd.UseVisualStyleBackColor = true;
            // 
            // listAnimations
            // 
            this.listAnimations.FormattingEnabled = true;
            this.listAnimations.Location = new System.Drawing.Point(3, 3);
            this.listAnimations.Name = "listAnimations";
            this.listAnimations.Size = new System.Drawing.Size(236, 134);
            this.listAnimations.TabIndex = 3;
            this.listAnimations.SelectedIndexChanged += new System.EventHandler(this.listAnimations_SelectedIndexChanged);
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
            this.splitContainer2.Size = new System.Drawing.Size(1030, 836);
            this.splitContainer2.SplitterDistance = 835;
            this.splitContainer2.TabIndex = 0;
            // 
            // gridProblems
            // 
            this.gridProblems.AllowUserToAddRows = false;
            this.gridProblems.AllowUserToDeleteRows = false;
            this.gridProblems.AllowUserToResizeColumns = false;
            this.gridProblems.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridProblems.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.gridProblems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridProblems.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridProblems.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.gridProblems.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.gridProblems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gridProblems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnProblems});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridProblems.DefaultCellStyle = dataGridViewCellStyle7;
            this.gridProblems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridProblems.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridProblems.Location = new System.Drawing.Point(0, 0);
            this.gridProblems.MultiSelect = false;
            this.gridProblems.Name = "gridProblems";
            this.gridProblems.ReadOnly = true;
            this.gridProblems.RowHeadersVisible = false;
            this.gridProblems.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridProblems.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.gridProblems.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.gridProblems.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridProblems.RowTemplate.Height = 500;
            this.gridProblems.RowTemplate.ReadOnly = true;
            this.gridProblems.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.gridProblems.Size = new System.Drawing.Size(191, 836);
            this.gridProblems.TabIndex = 0;
            this.gridProblems.SelectionChanged += new System.EventHandler(this.gridProblems_SelectionChanged);
            // 
            // columnProblems
            // 
            this.columnProblems.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.columnProblems.DefaultCellStyle = dataGridViewCellStyle6;
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
            this.comboPerspectiveOrtho.Location = new System.Drawing.Point(1129, 2);
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
            this.labelFPS.Location = new System.Drawing.Point(999, 2);
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
            this.buttonProblems.Location = new System.Drawing.Point(1240, 0);
            this.buttonProblems.Name = "buttonProblems";
            this.buttonProblems.Size = new System.Drawing.Size(44, 25);
            this.buttonProblems.TabIndex = 8;
            this.buttonProblems.UseVisualStyleBackColor = false;
            this.buttonProblems.Click += new System.EventHandler(this.buttonProblems_Click);
            // 
            // saveObjDialog
            // 
            this.saveObjDialog.DefaultExt = "obj";
            this.saveObjDialog.Filter = "OBJ-Files|*.obj|All files|*.*";
            this.saveObjDialog.Title = "Save OBJ-file...";
            // 
            // openObjDialog
            // 
            this.openObjDialog.Filter = "OBJ-Files|*.obj|All files|*.*";
            this.openObjDialog.Title = "Open OBJ-file...";
            // 
            // browseMaterialTexturesDIFFDialog
            // 
            this.browseMaterialTexturesDIFFDialog.Filter = "TGA-files|*.tga|All files|*.*";
            this.browseMaterialTexturesDIFFDialog.Title = "Open TGA-DIFF-file...";
            // 
            // saveColladaMeshDialog
            // 
            this.saveColladaMeshDialog.DefaultExt = "dae";
            this.saveColladaMeshDialog.Filter = "COLLADA-files|*.dae|All files|*.*";
            this.saveColladaMeshDialog.Title = "Save DAE-file...";
            // 
            // openColladaMeshDialog
            // 
            this.openColladaMeshDialog.Filter = "COLLADA-Files|*.dae|All files|*.*";
            this.openColladaMeshDialog.Title = "Open DAE-file...";
            // 
            // labelAnimationEndTime
            // 
            this.labelAnimationEndTime.AutoSize = true;
            this.labelAnimationEndTime.Location = new System.Drawing.Point(3, 226);
            this.labelAnimationEndTime.Name = "labelAnimationEndTime";
            this.labelAnimationEndTime.Size = new System.Drawing.Size(29, 13);
            this.labelAnimationEndTime.TabIndex = 41;
            this.labelAnimationEndTime.Text = "End:";
            // 
            // numericAnimationLoopEndTime
            // 
            this.numericAnimationLoopEndTime.DecimalPlaces = 3;
            this.numericAnimationLoopEndTime.Enabled = false;
            this.numericAnimationLoopEndTime.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericAnimationLoopEndTime.Location = new System.Drawing.Point(66, 276);
            this.numericAnimationLoopEndTime.Name = "numericAnimationLoopEndTime";
            this.numericAnimationLoopEndTime.Size = new System.Drawing.Size(173, 20);
            this.numericAnimationLoopEndTime.TabIndex = 43;
            this.numericAnimationLoopEndTime.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            // 
            // numericAnimationLoopStartTime
            // 
            this.numericAnimationLoopStartTime.DecimalPlaces = 3;
            this.numericAnimationLoopStartTime.Enabled = false;
            this.numericAnimationLoopStartTime.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericAnimationLoopStartTime.Location = new System.Drawing.Point(66, 250);
            this.numericAnimationLoopStartTime.Name = "numericAnimationLoopStartTime";
            this.numericAnimationLoopStartTime.Size = new System.Drawing.Size(173, 20);
            this.numericAnimationLoopStartTime.TabIndex = 42;
            this.numericAnimationLoopStartTime.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            // 
            // labelAnimationLoopStartTime
            // 
            this.labelAnimationLoopStartTime.AutoSize = true;
            this.labelAnimationLoopStartTime.Location = new System.Drawing.Point(3, 252);
            this.labelAnimationLoopStartTime.Name = "labelAnimationLoopStartTime";
            this.labelAnimationLoopStartTime.Size = new System.Drawing.Size(57, 13);
            this.labelAnimationLoopStartTime.TabIndex = 44;
            this.labelAnimationLoopStartTime.Text = "Loop start:";
            // 
            // labelAnimationLoopEndTime
            // 
            this.labelAnimationLoopEndTime.AutoSize = true;
            this.labelAnimationLoopEndTime.Location = new System.Drawing.Point(3, 278);
            this.labelAnimationLoopEndTime.Name = "labelAnimationLoopEndTime";
            this.labelAnimationLoopEndTime.Size = new System.Drawing.Size(55, 13);
            this.labelAnimationLoopEndTime.TabIndex = 45;
            this.labelAnimationLoopEndTime.Text = "Loop end:";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 861);
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
            this.tabShipMeshes.PerformLayout();
            this.groupShipMeshLODs.ResumeLayout(false);
            this.groupShipMeshLODs.PerformLayout();
            this.tabMaterials.ResumeLayout(false);
            this.tabMaterials.PerformLayout();
            this.groupProgress.ResumeLayout(false);
            this.groupProgress.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarProgress)).EndInit();
            this.groupThrusterStrength.ResumeLayout(false);
            this.groupThrusterStrength.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarThrusterStrength)).EndInit();
            this.groupMaterialTextures.ResumeLayout(false);
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
            this.tabDockpaths.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericDockpathAnimationIndex)).EndInit();
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
            this.tabEngineBurns.ResumeLayout(false);
            this.tabEngineBurns.PerformLayout();
            this.groupEngineBurnFlames.ResumeLayout(false);
            this.groupEngineBurnFlames.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericEngineBurnSpriteIndex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarEngineBurnFlames)).EndInit();
            this.panel4.ResumeLayout(false);
            this.tabAnimations.ResumeLayout(false);
            this.tabAnimations.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericAnimationEndTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericAnimationStartTime)).EndInit();
            this.groupAnimationJoints.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridProblems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericAnimationLoopEndTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericAnimationLoopStartTime)).EndInit();
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
        private System.Windows.Forms.TabPage tabMaterials;
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
        private System.Windows.Forms.GroupBox groupMaterialTextures;
        private System.Windows.Forms.ListBox listMaterialTextures;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboMaterialFormat;
        private System.Windows.Forms.ListBox listMaterials;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripButton buttonCheckForUpdates;
        private System.Windows.Forms.ToolStripButton buttonSave;
        private System.Windows.Forms.CheckBox checkShipMeshDoScar;
        private System.Windows.Forms.ListBox listShipMeshes;
        private System.Windows.Forms.Label labelShipMeshParent;
        private System.Windows.Forms.ComboBox comboShipMeshParent;
        private System.Windows.Forms.Button buttonShipMeshLODExportOBJ;
        private System.Windows.Forms.SaveFileDialog saveObjDialog;
        private System.Windows.Forms.Button buttonShipMeshLODImportOBJ;
        private System.Windows.Forms.OpenFileDialog openObjDialog;
        private System.Windows.Forms.GroupBox groupShipMeshLODMaterials;
        private System.Windows.Forms.ToolStripButton buttonShaderSettings;
        private System.Windows.Forms.ComboBox comboMaterialShader;
        private System.Windows.Forms.Button buttonShipMeshRemove;
        private System.Windows.Forms.Button buttonShipMeshAdd;
        private System.Windows.Forms.Button buttonShipMeshLODRemove;
        private System.Windows.Forms.Button buttonShipMeshLODAdd;
        private System.Windows.Forms.TextBox boxShipMeshName;
        private System.Windows.Forms.Label labelShipMeshName;
        private System.Windows.Forms.TabPage tabEngineBurns;
        private System.Windows.Forms.TextBox boxEngineBurnName;
        private System.Windows.Forms.Label labelEngineBurnName;
        private System.Windows.Forms.GroupBox groupEngineBurnFlames;
        private System.Windows.Forms.NumericUpDown numericEngineBurnSpriteIndex;
        private System.Windows.Forms.Label labelEngineBurnFlameSpriteIndex;
        private System.Windows.Forms.TrackBar trackBarEngineBurnFlames;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.CheckedListBox listEngineBurns;
        private System.Windows.Forms.ComboBox comboEngineBurnParent;
        private System.Windows.Forms.Label labelEngineBurnParent;
        private System.Windows.Forms.Button buttonMaterialRemove;
        private System.Windows.Forms.Button buttonMaterialAdd;
        private System.Windows.Forms.TextBox boxMaterialName;
        private System.Windows.Forms.Label labelMaterialName;
        private System.Windows.Forms.Button buttonMaterialTexturesBrowseDIFF;
        private System.Windows.Forms.GroupBox groupProgress;
        private System.Windows.Forms.TrackBar trackBarProgress;
        private System.Windows.Forms.GroupBox groupThrusterStrength;
        private System.Windows.Forms.TrackBar trackBarThrusterStrength;
        private System.Windows.Forms.OpenFileDialog browseMaterialTexturesDIFFDialog;
        private System.Windows.Forms.ToolStripButton buttonNew;
        private System.Windows.Forms.Label labelDockpathAnimationIndex;
        private System.Windows.Forms.NumericUpDown numericDockpathAnimationIndex;
        private System.Windows.Forms.TextBox boxDockpathName;
        private System.Windows.Forms.Label labelDockpathName;
        private System.Windows.Forms.TabPage tabAnimations;
        private System.Windows.Forms.ListBox listAnimations;
        private System.Windows.Forms.TextBox boxAnimationName;
        private System.Windows.Forms.Label labelAnimationName;
        private System.Windows.Forms.Button buttonAnimationRemove;
        private System.Windows.Forms.Button buttonAnimationAdd;
        private System.Windows.Forms.GroupBox groupAnimationJoints;
        private System.Windows.Forms.ListBox listAnimationJoints;
        private System.Windows.Forms.Button buttonAnimationPlay;
        private System.Windows.Forms.Button buttonAnimationJointRemove;
        private System.Windows.Forms.Button buttonAnimationJointAdd;
        private System.Windows.Forms.Button buttonShipMeshLODExportDAE;
        private System.Windows.Forms.SaveFileDialog saveColladaMeshDialog;
        private System.Windows.Forms.Button buttonShipMeshLODImportDAE;
        private System.Windows.Forms.OpenFileDialog openColladaMeshDialog;
        private System.Windows.Forms.Label labelAnimationStartTime;
        private System.Windows.Forms.NumericUpDown numericAnimationStartTime;
        private System.Windows.Forms.NumericUpDown numericAnimationEndTime;
        private System.Windows.Forms.Label labelAnimationLoopEndTime;
        private System.Windows.Forms.Label labelAnimationLoopStartTime;
        private System.Windows.Forms.NumericUpDown numericAnimationLoopEndTime;
        private System.Windows.Forms.NumericUpDown numericAnimationLoopStartTime;
        private System.Windows.Forms.Label labelAnimationEndTime;
    }
}

