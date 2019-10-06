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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.buttonNew = new System.Windows.Forms.ToolStripButton();
            this.buttonOpen = new System.Windows.Forms.ToolStripButton();
            this.buttonSave = new System.Windows.Forms.ToolStripButton();
            this.buttonSettings = new System.Windows.Forms.ToolStripButton();
            this.buttonShaderSettings = new System.Windows.Forms.ToolStripButton();
            this.buttonHotkeys = new System.Windows.Forms.ToolStripButton();
            this.buttonCheckForUpdates = new System.Windows.Forms.ToolStripButton();
            this.buttonAbout = new System.Windows.Forms.ToolStripButton();
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
            this.groupCollisionMeshPreview = new System.Windows.Forms.GroupBox();
            this.checkCollisionMeshPreviewSphere = new System.Windows.Forms.CheckBox();
            this.checkCollisionMeshPreviewBox = new System.Windows.Forms.CheckBox();
            this.buttonCollisionMeshImportDAE = new System.Windows.Forms.Button();
            this.buttonCollisionMeshExportDAE = new System.Windows.Forms.Button();
            this.buttonCollisionMeshImportOBJ = new System.Windows.Forms.Button();
            this.buttonCollisionMeshExportOBJ = new System.Windows.Forms.Button();
            this.buttonCollisionMeshRemove = new System.Windows.Forms.Button();
            this.buttonCollisionMeshAdd = new System.Windows.Forms.Button();
            this.listCollisionMeshes = new System.Windows.Forms.CheckedListBox();
            this.contextShowHideAll = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itemShowAll = new System.Windows.Forms.ToolStripMenuItem();
            this.itemHideAll = new System.Windows.Forms.ToolStripMenuItem();
            this.labelCollisionMeshParent = new System.Windows.Forms.Label();
            this.comboCollisionMeshParent = new System.Windows.Forms.ComboBox();
            this.tabJoints = new System.Windows.Forms.TabPage();
            this.buttonJointRemoveAll = new System.Windows.Forms.Button();
            this.buttonJointAddTemplate = new System.Windows.Forms.Button();
            this.groupJointRotation = new System.Windows.Forms.GroupBox();
            this.numericJointRotationZ = new System.Windows.Forms.NumericUpDown();
            this.labelJointRotationZ = new System.Windows.Forms.Label();
            this.numericJointRotationY = new System.Windows.Forms.NumericUpDown();
            this.labelJointRotationY = new System.Windows.Forms.Label();
            this.numericJointRotationX = new System.Windows.Forms.NumericUpDown();
            this.labelJointRotationX = new System.Windows.Forms.Label();
            this.groupJointPosition = new System.Windows.Forms.GroupBox();
            this.numericJointPositionZ = new System.Windows.Forms.NumericUpDown();
            this.labelJointPositionZ = new System.Windows.Forms.Label();
            this.numericJointPositionY = new System.Windows.Forms.NumericUpDown();
            this.labelJointPositionY = new System.Windows.Forms.Label();
            this.numericJointPositionX = new System.Windows.Forms.NumericUpDown();
            this.labelJointPositionX = new System.Windows.Forms.Label();
            this.labelJointParent = new System.Windows.Forms.Label();
            this.comboJointParent = new System.Windows.Forms.ComboBox();
            this.boxJointName = new System.Windows.Forms.TextBox();
            this.labelJointName = new System.Windows.Forms.Label();
            this.buttonJointRemove = new System.Windows.Forms.Button();
            this.buttonJointAdd = new System.Windows.Forms.Button();
            this.jointsTree = new System.Windows.Forms.TreeView();
            this.tabMarkers = new System.Windows.Forms.TabPage();
            this.groupMarkerRotation = new System.Windows.Forms.GroupBox();
            this.numericMarkerRotationZ = new System.Windows.Forms.NumericUpDown();
            this.labelMarkerRotationZ = new System.Windows.Forms.Label();
            this.numericMarkerRotationY = new System.Windows.Forms.NumericUpDown();
            this.labelMarkerRotationY = new System.Windows.Forms.Label();
            this.numericMarkerRotationX = new System.Windows.Forms.NumericUpDown();
            this.labelMarkerRotationX = new System.Windows.Forms.Label();
            this.groupMarkerPosition = new System.Windows.Forms.GroupBox();
            this.numericMarkerPositionZ = new System.Windows.Forms.NumericUpDown();
            this.labelMarkerPositionZ = new System.Windows.Forms.Label();
            this.numericMarkerPositionY = new System.Windows.Forms.NumericUpDown();
            this.labelMarkerPositionY = new System.Windows.Forms.Label();
            this.numericMarkerPositionX = new System.Windows.Forms.NumericUpDown();
            this.labelMarkerPositionX = new System.Windows.Forms.Label();
            this.labelMarkerParent = new System.Windows.Forms.Label();
            this.comboMarkerParent = new System.Windows.Forms.ComboBox();
            this.boxMarkerName = new System.Windows.Forms.TextBox();
            this.labelMarkerName = new System.Windows.Forms.Label();
            this.buttonMarkerRemove = new System.Windows.Forms.Button();
            this.buttonMarkerAdd = new System.Windows.Forms.Button();
            this.groupMarkerPreview = new System.Windows.Forms.GroupBox();
            this.checkDrawMarkers = new System.Windows.Forms.CheckBox();
            this.listMarkers = new System.Windows.Forms.ListBox();
            this.tabDockpaths = new System.Windows.Forms.TabPage();
            this.buttonDockpathRemove = new System.Windows.Forms.Button();
            this.buttonDockpathAdd = new System.Windows.Forms.Button();
            this.listDockpaths = new System.Windows.Forms.CheckedListBox();
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
            this.groupDockpathSegmentRotation = new System.Windows.Forms.GroupBox();
            this.numericDockpathSegmentRotationZ = new System.Windows.Forms.NumericUpDown();
            this.labelDockpathSegmentRotationZ = new System.Windows.Forms.Label();
            this.numericDockpathSegmentRotationY = new System.Windows.Forms.NumericUpDown();
            this.labelDockpathSegmentRotationY = new System.Windows.Forms.Label();
            this.numericDockpathSegmentRotationX = new System.Windows.Forms.NumericUpDown();
            this.labelDockpathSegmentRotationX = new System.Windows.Forms.Label();
            this.groupDockpathSegmentPosition = new System.Windows.Forms.GroupBox();
            this.numericDockpathSegmentPosZ = new System.Windows.Forms.NumericUpDown();
            this.labelDockpathSegmentPosZ = new System.Windows.Forms.Label();
            this.numericDockpathSegmentPosY = new System.Windows.Forms.NumericUpDown();
            this.labelDockpathSegmentPosY = new System.Windows.Forms.Label();
            this.numericDockpathSegmentPosX = new System.Windows.Forms.NumericUpDown();
            this.labelDockpathSegmentPosX = new System.Windows.Forms.Label();
            this.buttonDockpathSegmentInsertBefore = new System.Windows.Forms.Button();
            this.numericDockpathSegmentSpeed = new System.Windows.Forms.NumericUpDown();
            this.numericDockpathSegmentTolerance = new System.Windows.Forms.NumericUpDown();
            this.buttonDockpathSegmentRemove = new System.Windows.Forms.Button();
            this.buttonDockpathSegmentInsertAfter = new System.Windows.Forms.Button();
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
            this.trackBarDockpathSegments = new System.Windows.Forms.TrackBar();
            this.groupDockpathLinks = new System.Windows.Forms.GroupBox();
            this.comboDockpathLinkPath = new System.Windows.Forms.ComboBox();
            this.buttonDockpathLinkRemove = new System.Windows.Forms.Button();
            this.buttonDockpathLinkAdd = new System.Windows.Forms.Button();
            this.labelDockpathLinkPath = new System.Windows.Forms.Label();
            this.listDockpathLinks = new System.Windows.Forms.ListBox();
            this.groupDockpathFamilies = new System.Windows.Forms.GroupBox();
            this.buttonDockpathFamilyRemove = new System.Windows.Forms.Button();
            this.buttonDockpathFamilyAdd = new System.Windows.Forms.Button();
            this.boxDockpathFamilyName = new System.Windows.Forms.TextBox();
            this.labelDockpathFamilyName = new System.Windows.Forms.Label();
            this.listDockpathFamilies = new System.Windows.Forms.ListBox();
            this.tabNavLights = new System.Windows.Forms.TabPage();
            this.groupNavLightPosition = new System.Windows.Forms.GroupBox();
            this.numericNavLightPositionZ = new System.Windows.Forms.NumericUpDown();
            this.labelNavLightPositionZ = new System.Windows.Forms.Label();
            this.numericNavLightPositionY = new System.Windows.Forms.NumericUpDown();
            this.labelNavLightPositionY = new System.Windows.Forms.Label();
            this.numericNavLightPositionX = new System.Windows.Forms.NumericUpDown();
            this.labelNavLightPositionX = new System.Windows.Forms.Label();
            this.labelNavLightParent = new System.Windows.Forms.Label();
            this.comboNavLightParent = new System.Windows.Forms.ComboBox();
            this.boxNavLightName = new System.Windows.Forms.TextBox();
            this.labelNavLightName = new System.Windows.Forms.Label();
            this.listNavLights = new System.Windows.Forms.CheckedListBox();
            this.buttonNavLightRemove = new System.Windows.Forms.Button();
            this.buttonNavLightAdd = new System.Windows.Forms.Button();
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
            this.tabEngineGlows = new System.Windows.Forms.TabPage();
            this.buttonEngineGlowRemove = new System.Windows.Forms.Button();
            this.buttonEngineGlowAdd = new System.Windows.Forms.Button();
            this.boxEngineGlowName = new System.Windows.Forms.TextBox();
            this.labelEngineGlowName = new System.Windows.Forms.Label();
            this.listEngineGlows = new System.Windows.Forms.ListBox();
            this.labelEngineGlowParent = new System.Windows.Forms.Label();
            this.comboEngineGlowParent = new System.Windows.Forms.ComboBox();
            this.groupEngineGlowLODs = new System.Windows.Forms.GroupBox();
            this.buttonEngineGlowLODImportDAE = new System.Windows.Forms.Button();
            this.buttonEngineGlowLODRemove = new System.Windows.Forms.Button();
            this.buttonEngineGlowLODExportDAE = new System.Windows.Forms.Button();
            this.buttonEngineGlowLODAdd = new System.Windows.Forms.Button();
            this.buttonEngineGlowLODImportOBJ = new System.Windows.Forms.Button();
            this.buttonEngineGlowLODExportOBJ = new System.Windows.Forms.Button();
            this.listEngineGlowLODs = new System.Windows.Forms.CheckedListBox();
            this.tabEngineShapes = new System.Windows.Forms.TabPage();
            this.boxEngineShapeName = new System.Windows.Forms.TextBox();
            this.labelEngineShapeName = new System.Windows.Forms.Label();
            this.buttonEngineShapeImportDAE = new System.Windows.Forms.Button();
            this.buttonEngineShapeExportDAE = new System.Windows.Forms.Button();
            this.buttonEngineShapeImportOBJ = new System.Windows.Forms.Button();
            this.buttonEngineShapeExportOBJ = new System.Windows.Forms.Button();
            this.buttonEngineShapeRemove = new System.Windows.Forms.Button();
            this.buttonEngineShapeAdd = new System.Windows.Forms.Button();
            this.listEngineShapes = new System.Windows.Forms.CheckedListBox();
            this.labelEngineShapeParent = new System.Windows.Forms.Label();
            this.comboEngineShapeParent = new System.Windows.Forms.ComboBox();
            this.tabEngineBurns = new System.Windows.Forms.TabPage();
            this.buttonEngineBurnRemove = new System.Windows.Forms.Button();
            this.buttonEngineBurnAdd = new System.Windows.Forms.Button();
            this.listEngineBurns = new System.Windows.Forms.CheckedListBox();
            this.comboEngineBurnParent = new System.Windows.Forms.ComboBox();
            this.labelEngineBurnParent = new System.Windows.Forms.Label();
            this.boxEngineBurnName = new System.Windows.Forms.TextBox();
            this.labelEngineBurnName = new System.Windows.Forms.Label();
            this.groupEngineBurnFlames = new System.Windows.Forms.GroupBox();
            this.groupEngineBurnFlamePosition = new System.Windows.Forms.GroupBox();
            this.numericEngineBurnFlamePosZ = new System.Windows.Forms.NumericUpDown();
            this.labelEngineBurnFlamePosZ = new System.Windows.Forms.Label();
            this.numericEngineBurnFlamePosY = new System.Windows.Forms.NumericUpDown();
            this.labelEngineBurnFlamePosY = new System.Windows.Forms.Label();
            this.numericEngineBurnFlamePosX = new System.Windows.Forms.NumericUpDown();
            this.labelEngineBurnFlamePosX = new System.Windows.Forms.Label();
            this.buttonEngineBurnFlameRemove = new System.Windows.Forms.Button();
            this.buttonEngineBurnFlameAdd = new System.Windows.Forms.Button();
            this.numericEngineBurnSpriteIndex = new System.Windows.Forms.NumericUpDown();
            this.labelEngineBurnFlameSpriteIndex = new System.Windows.Forms.Label();
            this.trackBarEngineBurnFlames = new System.Windows.Forms.TrackBar();
            this.tabAnimations = new System.Windows.Forms.TabPage();
            this.labelAnimationLoopEndTime = new System.Windows.Forms.Label();
            this.labelAnimationLoopStartTime = new System.Windows.Forms.Label();
            this.numericAnimationLoopEndTime = new System.Windows.Forms.NumericUpDown();
            this.numericAnimationLoopStartTime = new System.Windows.Forms.NumericUpDown();
            this.labelAnimationEndTime = new System.Windows.Forms.Label();
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
            this.tabControlRight = new System.Windows.Forms.TabControl();
            this.tabProblems = new System.Windows.Forms.TabPage();
            this.gridProblems = new System.Windows.Forms.DataGridView();
            this.columnProblems = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabTargetBoxes = new System.Windows.Forms.TabPage();
            this.buttonTargetBoxShowCode = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.labelTargetBoxLength = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.labelTargetBoxHeight = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.labelTargetBoxWidth = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.labelTargetBoxMaxZ = new System.Windows.Forms.Label();
            this.labelTargetBoxMaxY = new System.Windows.Forms.Label();
            this.labelTargetBoxMaxX = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.labelTargetBoxMinZ = new System.Windows.Forms.Label();
            this.labelTargetBoxMinY = new System.Windows.Forms.Label();
            this.labelTargetBoxMinX = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.numericTargetBoxIndex = new System.Windows.Forms.NumericUpDown();
            this.listTargetBoxes = new System.Windows.Forms.CheckedListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericTargetBoxMaxZ = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numericTargetBoxMaxY = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numericTargetBoxMaxX = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numericTargetBoxMinZ = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.numericTargetBoxMinY = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.numericTargetBoxMinX = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.buttonRemoveTargetBox = new System.Windows.Forms.Button();
            this.buttonAddTargetBox = new System.Windows.Forms.Button();
            this.tabShipTypeMapping = new System.Windows.Forms.TabPage();
            this.dataGridShipTypes = new System.Windows.Forms.DataGridView();
            this.dataGridShipTypesColumnDAE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridShipTypesColumnSHIP = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.comboPerspectiveOrtho = new System.Windows.Forms.ComboBox();
            this.labelFPS = new System.Windows.Forms.Label();
            this.saveObjDialog = new System.Windows.Forms.SaveFileDialog();
            this.openObjDialog = new System.Windows.Forms.OpenFileDialog();
            this.browseMaterialTexturesDIFFDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveColladaMeshDialog = new System.Windows.Forms.SaveFileDialog();
            this.openColladaMeshDialog = new System.Windows.Forms.OpenFileDialog();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.contextJointRightClick = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextJointRightClickItemShowDescendants = new System.Windows.Forms.ToolStripMenuItem();
            this.contextJointRightClickItemHideDescendants = new System.Windows.Forms.ToolStripMenuItem();
            this.contextJointRightClickItemShowAll = new System.Windows.Forms.ToolStripMenuItem();
            this.contextJointRightClickItemHideAll = new System.Windows.Forms.ToolStripMenuItem();
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
            this.groupCollisionMeshPreview.SuspendLayout();
            this.contextShowHideAll.SuspendLayout();
            this.tabJoints.SuspendLayout();
            this.groupJointRotation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericJointRotationZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericJointRotationY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericJointRotationX)).BeginInit();
            this.groupJointPosition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericJointPositionZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericJointPositionY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericJointPositionX)).BeginInit();
            this.tabMarkers.SuspendLayout();
            this.groupMarkerRotation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericMarkerRotationZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMarkerRotationY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMarkerRotationX)).BeginInit();
            this.groupMarkerPosition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericMarkerPositionZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMarkerPositionY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMarkerPositionX)).BeginInit();
            this.groupMarkerPreview.SuspendLayout();
            this.tabDockpaths.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericDockpathAnimationIndex)).BeginInit();
            this.groupDockpathFlags.SuspendLayout();
            this.groupDockpathSegments.SuspendLayout();
            this.groupDockpathSegmentRotation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericDockpathSegmentRotationZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDockpathSegmentRotationY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDockpathSegmentRotationX)).BeginInit();
            this.groupDockpathSegmentPosition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericDockpathSegmentPosZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDockpathSegmentPosY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDockpathSegmentPosX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDockpathSegmentSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDockpathSegmentTolerance)).BeginInit();
            this.groupDockpathSegmentFlags.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDockpathSegments)).BeginInit();
            this.groupDockpathLinks.SuspendLayout();
            this.groupDockpathFamilies.SuspendLayout();
            this.tabNavLights.SuspendLayout();
            this.groupNavLightPosition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericNavLightPositionZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericNavLightPositionY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericNavLightPositionX)).BeginInit();
            this.groupNavLightPreview.SuspendLayout();
            this.groupNavLightParameters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericNavLightDistance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericNavLightFrequency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericNavLightPhase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericNavLightSize)).BeginInit();
            this.groupNavLightFlags.SuspendLayout();
            this.tabEngineGlows.SuspendLayout();
            this.groupEngineGlowLODs.SuspendLayout();
            this.tabEngineShapes.SuspendLayout();
            this.tabEngineBurns.SuspendLayout();
            this.groupEngineBurnFlames.SuspendLayout();
            this.groupEngineBurnFlamePosition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericEngineBurnFlamePosZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericEngineBurnFlamePosY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericEngineBurnFlamePosX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericEngineBurnSpriteIndex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarEngineBurnFlames)).BeginInit();
            this.tabAnimations.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericAnimationLoopEndTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericAnimationLoopStartTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericAnimationEndTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericAnimationStartTime)).BeginInit();
            this.groupAnimationJoints.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabControlRight.SuspendLayout();
            this.tabProblems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridProblems)).BeginInit();
            this.tabTargetBoxes.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericTargetBoxIndex)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericTargetBoxMaxZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericTargetBoxMaxY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericTargetBoxMaxX)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericTargetBoxMinZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericTargetBoxMinY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericTargetBoxMinX)).BeginInit();
            this.tabShipTypeMapping.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridShipTypes)).BeginInit();
            this.contextJointRightClick.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonNew,
            this.buttonOpen,
            this.buttonSave,
            this.buttonSettings,
            this.buttonShaderSettings,
            this.buttonHotkeys,
            this.buttonCheckForUpdates,
            this.buttonAbout});
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
            this.buttonNew.Size = new System.Drawing.Size(53, 22);
            this.buttonNew.Text = "New";
            this.buttonNew.Click += new System.EventHandler(this.buttonNew_Click);
            // 
            // buttonOpen
            // 
            this.buttonOpen.Image = ((System.Drawing.Image)(resources.GetObject("buttonOpen.Image")));
            this.buttonOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(58, 22);
            this.buttonOpen.Text = "Open";
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Image = ((System.Drawing.Image)(resources.GetObject("buttonSave.Image")));
            this.buttonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(67, 22);
            this.buttonSave.Text = "Save as";
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonSettings
            // 
            this.buttonSettings.Image = ((System.Drawing.Image)(resources.GetObject("buttonSettings.Image")));
            this.buttonSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Size = new System.Drawing.Size(71, 22);
            this.buttonSettings.Text = "Settings";
            this.buttonSettings.Click += new System.EventHandler(this.buttonSettings_Click);
            // 
            // buttonShaderSettings
            // 
            this.buttonShaderSettings.Image = ((System.Drawing.Image)(resources.GetObject("buttonShaderSettings.Image")));
            this.buttonShaderSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonShaderSettings.Name = "buttonShaderSettings";
            this.buttonShaderSettings.Size = new System.Drawing.Size(110, 22);
            this.buttonShaderSettings.Text = "Shader Settings";
            this.buttonShaderSettings.Click += new System.EventHandler(this.buttonShaderSettings_Click);
            // 
            // buttonHotkeys
            // 
            this.buttonHotkeys.Image = ((System.Drawing.Image)(resources.GetObject("buttonHotkeys.Image")));
            this.buttonHotkeys.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonHotkeys.Name = "buttonHotkeys";
            this.buttonHotkeys.Size = new System.Drawing.Size(72, 22);
            this.buttonHotkeys.Text = "Hotkeys";
            this.buttonHotkeys.Click += new System.EventHandler(this.buttonHotkeys_Click);
            // 
            // buttonCheckForUpdates
            // 
            this.buttonCheckForUpdates.Image = ((System.Drawing.Image)(resources.GetObject("buttonCheckForUpdates.Image")));
            this.buttonCheckForUpdates.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonCheckForUpdates.Name = "buttonCheckForUpdates";
            this.buttonCheckForUpdates.Size = new System.Drawing.Size(125, 22);
            this.buttonCheckForUpdates.Text = "Check for updates";
            this.buttonCheckForUpdates.Click += new System.EventHandler(this.buttonCheckForUpdates_Click);
            // 
            // buttonAbout
            // 
            this.buttonAbout.Image = ((System.Drawing.Image)(resources.GetObject("buttonAbout.Image")));
            this.buttonAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonAbout.Name = "buttonAbout";
            this.buttonAbout.Size = new System.Drawing.Size(62, 22);
            this.buttonAbout.Text = "About";
            this.buttonAbout.Click += new System.EventHandler(this.buttonAbout_Click);
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
            this.splitContainer1.Panel1MinSize = 280;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1284, 820);
            this.splitContainer1.SplitterDistance = 280;
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
            this.tabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(280, 820);
            this.tabControl.TabIndex = 0;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabShipMeshes
            // 
            this.tabShipMeshes.AutoScroll = true;
            this.tabShipMeshes.AutoScrollMinSize = new System.Drawing.Size(224, 600);
            this.tabShipMeshes.Controls.Add(this.buttonShipMeshRemove);
            this.tabShipMeshes.Controls.Add(this.buttonShipMeshAdd);
            this.tabShipMeshes.Controls.Add(this.boxShipMeshName);
            this.tabShipMeshes.Controls.Add(this.labelShipMeshName);
            this.tabShipMeshes.Controls.Add(this.checkShipMeshDoScar);
            this.tabShipMeshes.Controls.Add(this.listShipMeshes);
            this.tabShipMeshes.Controls.Add(this.labelShipMeshParent);
            this.tabShipMeshes.Controls.Add(this.comboShipMeshParent);
            this.tabShipMeshes.Controls.Add(this.groupShipMeshLODs);
            this.tabShipMeshes.Location = new System.Drawing.Point(4, 58);
            this.tabShipMeshes.Name = "tabShipMeshes";
            this.tabShipMeshes.Padding = new System.Windows.Forms.Padding(3);
            this.tabShipMeshes.Size = new System.Drawing.Size(272, 758);
            this.tabShipMeshes.TabIndex = 0;
            this.tabShipMeshes.Text = "Ship Meshes";
            this.tabShipMeshes.UseVisualStyleBackColor = true;
            // 
            // buttonShipMeshRemove
            // 
            this.buttonShipMeshRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonShipMeshRemove.Enabled = false;
            this.buttonShipMeshRemove.Location = new System.Drawing.Point(174, 211);
            this.buttonShipMeshRemove.Name = "buttonShipMeshRemove";
            this.buttonShipMeshRemove.Size = new System.Drawing.Size(92, 23);
            this.buttonShipMeshRemove.TabIndex = 17;
            this.buttonShipMeshRemove.Text = "Remove";
            this.buttonShipMeshRemove.UseVisualStyleBackColor = true;
            this.buttonShipMeshRemove.Click += new System.EventHandler(this.buttonShipMeshRemove_Click);
            // 
            // buttonShipMeshAdd
            // 
            this.buttonShipMeshAdd.Location = new System.Drawing.Point(6, 211);
            this.buttonShipMeshAdd.Name = "buttonShipMeshAdd";
            this.buttonShipMeshAdd.Size = new System.Drawing.Size(92, 23);
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
            this.boxShipMeshName.Size = new System.Drawing.Size(210, 20);
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
            this.listShipMeshes.Size = new System.Drawing.Size(260, 199);
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
            this.comboShipMeshParent.Size = new System.Drawing.Size(210, 21);
            this.comboShipMeshParent.Sorted = true;
            this.comboShipMeshParent.TabIndex = 12;
            this.comboShipMeshParent.SelectedIndexChanged += new System.EventHandler(this.comboShipMeshParent_SelectedIndexChanged);
            // 
            // groupShipMeshLODs
            // 
            this.groupShipMeshLODs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupShipMeshLODs.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupShipMeshLODs.Controls.Add(this.buttonShipMeshLODImportDAE);
            this.groupShipMeshLODs.Controls.Add(this.buttonShipMeshLODRemove);
            this.groupShipMeshLODs.Controls.Add(this.buttonShipMeshLODExportDAE);
            this.groupShipMeshLODs.Controls.Add(this.buttonShipMeshLODAdd);
            this.groupShipMeshLODs.Controls.Add(this.groupShipMeshLODMaterials);
            this.groupShipMeshLODs.Controls.Add(this.buttonShipMeshLODImportOBJ);
            this.groupShipMeshLODs.Controls.Add(this.buttonShipMeshLODExportOBJ);
            this.groupShipMeshLODs.Controls.Add(this.listShipMeshLODs);
            this.groupShipMeshLODs.Location = new System.Drawing.Point(3, 316);
            this.groupShipMeshLODs.MinimumSize = new System.Drawing.Size(0, 281);
            this.groupShipMeshLODs.Name = "groupShipMeshLODs";
            this.groupShipMeshLODs.Size = new System.Drawing.Size(263, 281);
            this.groupShipMeshLODs.TabIndex = 1;
            this.groupShipMeshLODs.TabStop = false;
            this.groupShipMeshLODs.Text = "Level of detail(s)";
            // 
            // buttonShipMeshLODImportDAE
            // 
            this.buttonShipMeshLODImportDAE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonShipMeshLODImportDAE.Enabled = false;
            this.buttonShipMeshLODImportDAE.Location = new System.Drawing.Point(169, 130);
            this.buttonShipMeshLODImportDAE.Name = "buttonShipMeshLODImportDAE";
            this.buttonShipMeshLODImportDAE.Size = new System.Drawing.Size(92, 23);
            this.buttonShipMeshLODImportDAE.TabIndex = 7;
            this.buttonShipMeshLODImportDAE.Text = "Import from DAE";
            this.buttonShipMeshLODImportDAE.UseVisualStyleBackColor = true;
            this.buttonShipMeshLODImportDAE.Click += new System.EventHandler(this.buttonShipMeshLODImportDAE_Click);
            // 
            // buttonShipMeshLODRemove
            // 
            this.buttonShipMeshLODRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonShipMeshLODRemove.Enabled = false;
            this.buttonShipMeshLODRemove.Location = new System.Drawing.Point(169, 101);
            this.buttonShipMeshLODRemove.Name = "buttonShipMeshLODRemove";
            this.buttonShipMeshLODRemove.Size = new System.Drawing.Size(92, 23);
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
            this.buttonShipMeshLODExportDAE.Size = new System.Drawing.Size(92, 23);
            this.buttonShipMeshLODExportDAE.TabIndex = 6;
            this.buttonShipMeshLODExportDAE.Text = "Export to DAE";
            this.buttonShipMeshLODExportDAE.UseVisualStyleBackColor = true;
            this.buttonShipMeshLODExportDAE.Click += new System.EventHandler(this.buttonShipMeshLODExportDAE_Click);
            // 
            // buttonShipMeshLODAdd
            // 
            this.buttonShipMeshLODAdd.Location = new System.Drawing.Point(3, 101);
            this.buttonShipMeshLODAdd.Name = "buttonShipMeshLODAdd";
            this.buttonShipMeshLODAdd.Size = new System.Drawing.Size(92, 23);
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
            this.groupShipMeshLODMaterials.Location = new System.Drawing.Point(3, 188);
            this.groupShipMeshLODMaterials.MinimumSize = new System.Drawing.Size(0, 87);
            this.groupShipMeshLODMaterials.Name = "groupShipMeshLODMaterials";
            this.groupShipMeshLODMaterials.Size = new System.Drawing.Size(257, 87);
            this.groupShipMeshLODMaterials.TabIndex = 3;
            this.groupShipMeshLODMaterials.TabStop = false;
            this.groupShipMeshLODMaterials.Text = "Assigned materials";
            // 
            // buttonShipMeshLODImportOBJ
            // 
            this.buttonShipMeshLODImportOBJ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonShipMeshLODImportOBJ.Enabled = false;
            this.buttonShipMeshLODImportOBJ.Location = new System.Drawing.Point(169, 159);
            this.buttonShipMeshLODImportOBJ.Name = "buttonShipMeshLODImportOBJ";
            this.buttonShipMeshLODImportOBJ.Size = new System.Drawing.Size(92, 23);
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
            this.buttonShipMeshLODExportOBJ.Size = new System.Drawing.Size(92, 23);
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
            this.listShipMeshLODs.Size = new System.Drawing.Size(257, 64);
            this.listShipMeshLODs.TabIndex = 0;
            this.listShipMeshLODs.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listShipMeshLODs_ItemCheck);
            this.listShipMeshLODs.SelectedIndexChanged += new System.EventHandler(this.listShipMeshLODs_SelectedIndexChanged);
            // 
            // tabMaterials
            // 
            this.tabMaterials.AutoScroll = true;
            this.tabMaterials.AutoScrollMinSize = new System.Drawing.Size(241, 681);
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
            this.tabMaterials.Location = new System.Drawing.Point(4, 58);
            this.tabMaterials.Name = "tabMaterials";
            this.tabMaterials.Padding = new System.Windows.Forms.Padding(3);
            this.tabMaterials.Size = new System.Drawing.Size(272, 758);
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
            this.groupProgress.Size = new System.Drawing.Size(262, 46);
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
            this.trackBarProgress.Size = new System.Drawing.Size(256, 27);
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
            this.groupThrusterStrength.Size = new System.Drawing.Size(262, 46);
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
            this.trackBarThrusterStrength.Size = new System.Drawing.Size(256, 27);
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
            this.boxMaterialName.Size = new System.Drawing.Size(216, 20);
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
            this.buttonMaterialRemove.Location = new System.Drawing.Point(174, 250);
            this.buttonMaterialRemove.Name = "buttonMaterialRemove";
            this.buttonMaterialRemove.Size = new System.Drawing.Size(92, 23);
            this.buttonMaterialRemove.TabIndex = 28;
            this.buttonMaterialRemove.Text = "Remove";
            this.buttonMaterialRemove.UseVisualStyleBackColor = true;
            this.buttonMaterialRemove.Click += new System.EventHandler(this.buttonMaterialRemove_Click);
            // 
            // buttonMaterialAdd
            // 
            this.buttonMaterialAdd.Location = new System.Drawing.Point(6, 250);
            this.buttonMaterialAdd.Name = "buttonMaterialAdd";
            this.buttonMaterialAdd.Size = new System.Drawing.Size(92, 23);
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
            this.comboMaterialShader.Size = new System.Drawing.Size(216, 21);
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
            this.groupMaterialTextures.Size = new System.Drawing.Size(262, 214);
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
            this.buttonMaterialTexturesBrowseDIFF.Size = new System.Drawing.Size(256, 23);
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
            this.listMaterialTextures.Size = new System.Drawing.Size(256, 160);
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
            this.comboMaterialFormat.Size = new System.Drawing.Size(216, 21);
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
            this.listMaterials.Size = new System.Drawing.Size(260, 238);
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
            this.tabCollisionMeshes.AutoScrollMinSize = new System.Drawing.Size(241, 325);
            this.tabCollisionMeshes.Controls.Add(this.groupCollisionMeshPreview);
            this.tabCollisionMeshes.Controls.Add(this.buttonCollisionMeshImportDAE);
            this.tabCollisionMeshes.Controls.Add(this.buttonCollisionMeshExportDAE);
            this.tabCollisionMeshes.Controls.Add(this.buttonCollisionMeshImportOBJ);
            this.tabCollisionMeshes.Controls.Add(this.buttonCollisionMeshExportOBJ);
            this.tabCollisionMeshes.Controls.Add(this.buttonCollisionMeshRemove);
            this.tabCollisionMeshes.Controls.Add(this.buttonCollisionMeshAdd);
            this.tabCollisionMeshes.Controls.Add(this.listCollisionMeshes);
            this.tabCollisionMeshes.Controls.Add(this.labelCollisionMeshParent);
            this.tabCollisionMeshes.Controls.Add(this.comboCollisionMeshParent);
            this.tabCollisionMeshes.Location = new System.Drawing.Point(4, 58);
            this.tabCollisionMeshes.Name = "tabCollisionMeshes";
            this.tabCollisionMeshes.Padding = new System.Windows.Forms.Padding(3);
            this.tabCollisionMeshes.Size = new System.Drawing.Size(272, 758);
            this.tabCollisionMeshes.TabIndex = 5;
            this.tabCollisionMeshes.Text = "Collision Meshes";
            this.tabCollisionMeshes.UseVisualStyleBackColor = true;
            // 
            // groupCollisionMeshPreview
            // 
            this.groupCollisionMeshPreview.Controls.Add(this.checkCollisionMeshPreviewSphere);
            this.groupCollisionMeshPreview.Controls.Add(this.checkCollisionMeshPreviewBox);
            this.groupCollisionMeshPreview.Enabled = false;
            this.groupCollisionMeshPreview.Location = new System.Drawing.Point(6, 325);
            this.groupCollisionMeshPreview.Name = "groupCollisionMeshPreview";
            this.groupCollisionMeshPreview.Size = new System.Drawing.Size(260, 118);
            this.groupCollisionMeshPreview.TabIndex = 31;
            this.groupCollisionMeshPreview.TabStop = false;
            this.groupCollisionMeshPreview.Text = "Preview";
            // 
            // checkCollisionMeshPreviewSphere
            // 
            this.checkCollisionMeshPreviewSphere.AutoSize = true;
            this.checkCollisionMeshPreviewSphere.Location = new System.Drawing.Point(6, 42);
            this.checkCollisionMeshPreviewSphere.Name = "checkCollisionMeshPreviewSphere";
            this.checkCollisionMeshPreviewSphere.Size = new System.Drawing.Size(106, 17);
            this.checkCollisionMeshPreviewSphere.TabIndex = 1;
            this.checkCollisionMeshPreviewSphere.Text = "Bounding sphere";
            this.checkCollisionMeshPreviewSphere.UseVisualStyleBackColor = true;
            this.checkCollisionMeshPreviewSphere.CheckedChanged += new System.EventHandler(this.checkCollisionMeshPreviewSphere_CheckedChanged);
            // 
            // checkCollisionMeshPreviewBox
            // 
            this.checkCollisionMeshPreviewBox.AutoSize = true;
            this.checkCollisionMeshPreviewBox.Location = new System.Drawing.Point(6, 19);
            this.checkCollisionMeshPreviewBox.Name = "checkCollisionMeshPreviewBox";
            this.checkCollisionMeshPreviewBox.Size = new System.Drawing.Size(91, 17);
            this.checkCollisionMeshPreviewBox.TabIndex = 0;
            this.checkCollisionMeshPreviewBox.Text = "Bounding box";
            this.checkCollisionMeshPreviewBox.UseVisualStyleBackColor = true;
            this.checkCollisionMeshPreviewBox.CheckedChanged += new System.EventHandler(this.checkCollisionMeshPreviewBox_CheckedChanged);
            // 
            // buttonCollisionMeshImportDAE
            // 
            this.buttonCollisionMeshImportDAE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCollisionMeshImportDAE.Enabled = false;
            this.buttonCollisionMeshImportDAE.Location = new System.Drawing.Point(174, 267);
            this.buttonCollisionMeshImportDAE.Name = "buttonCollisionMeshImportDAE";
            this.buttonCollisionMeshImportDAE.Size = new System.Drawing.Size(92, 23);
            this.buttonCollisionMeshImportDAE.TabIndex = 30;
            this.buttonCollisionMeshImportDAE.Text = "Import from DAE";
            this.buttonCollisionMeshImportDAE.UseVisualStyleBackColor = true;
            this.buttonCollisionMeshImportDAE.Click += new System.EventHandler(this.buttonCollisionMeshImportDAE_Click);
            // 
            // buttonCollisionMeshExportDAE
            // 
            this.buttonCollisionMeshExportDAE.Enabled = false;
            this.buttonCollisionMeshExportDAE.Location = new System.Drawing.Point(6, 267);
            this.buttonCollisionMeshExportDAE.Name = "buttonCollisionMeshExportDAE";
            this.buttonCollisionMeshExportDAE.Size = new System.Drawing.Size(92, 23);
            this.buttonCollisionMeshExportDAE.TabIndex = 29;
            this.buttonCollisionMeshExportDAE.Text = "Export to DAE";
            this.buttonCollisionMeshExportDAE.UseVisualStyleBackColor = true;
            this.buttonCollisionMeshExportDAE.Click += new System.EventHandler(this.buttonCollisionMeshExportDAE_Click);
            // 
            // buttonCollisionMeshImportOBJ
            // 
            this.buttonCollisionMeshImportOBJ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCollisionMeshImportOBJ.Enabled = false;
            this.buttonCollisionMeshImportOBJ.Location = new System.Drawing.Point(174, 296);
            this.buttonCollisionMeshImportOBJ.Name = "buttonCollisionMeshImportOBJ";
            this.buttonCollisionMeshImportOBJ.Size = new System.Drawing.Size(92, 23);
            this.buttonCollisionMeshImportOBJ.TabIndex = 28;
            this.buttonCollisionMeshImportOBJ.Text = "Import from OBJ";
            this.buttonCollisionMeshImportOBJ.UseVisualStyleBackColor = true;
            this.buttonCollisionMeshImportOBJ.Click += new System.EventHandler(this.buttonCollisionMeshImportOBJ_Click);
            // 
            // buttonCollisionMeshExportOBJ
            // 
            this.buttonCollisionMeshExportOBJ.Enabled = false;
            this.buttonCollisionMeshExportOBJ.Location = new System.Drawing.Point(6, 296);
            this.buttonCollisionMeshExportOBJ.Name = "buttonCollisionMeshExportOBJ";
            this.buttonCollisionMeshExportOBJ.Size = new System.Drawing.Size(92, 23);
            this.buttonCollisionMeshExportOBJ.TabIndex = 27;
            this.buttonCollisionMeshExportOBJ.Text = "Export to OBJ";
            this.buttonCollisionMeshExportOBJ.UseVisualStyleBackColor = true;
            this.buttonCollisionMeshExportOBJ.Click += new System.EventHandler(this.buttonCollisionMeshExportOBJ_Click);
            // 
            // buttonCollisionMeshRemove
            // 
            this.buttonCollisionMeshRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCollisionMeshRemove.Enabled = false;
            this.buttonCollisionMeshRemove.Location = new System.Drawing.Point(174, 211);
            this.buttonCollisionMeshRemove.Name = "buttonCollisionMeshRemove";
            this.buttonCollisionMeshRemove.Size = new System.Drawing.Size(92, 23);
            this.buttonCollisionMeshRemove.TabIndex = 24;
            this.buttonCollisionMeshRemove.Text = "Remove";
            this.buttonCollisionMeshRemove.UseVisualStyleBackColor = true;
            this.buttonCollisionMeshRemove.Click += new System.EventHandler(this.buttonCollisionMeshRemove_Click);
            // 
            // buttonCollisionMeshAdd
            // 
            this.buttonCollisionMeshAdd.Location = new System.Drawing.Point(6, 211);
            this.buttonCollisionMeshAdd.Name = "buttonCollisionMeshAdd";
            this.buttonCollisionMeshAdd.Size = new System.Drawing.Size(92, 23);
            this.buttonCollisionMeshAdd.TabIndex = 23;
            this.buttonCollisionMeshAdd.Text = "Add";
            this.buttonCollisionMeshAdd.UseVisualStyleBackColor = true;
            this.buttonCollisionMeshAdd.Click += new System.EventHandler(this.buttonCollisionMeshAdd_Click);
            // 
            // listCollisionMeshes
            // 
            this.listCollisionMeshes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listCollisionMeshes.ContextMenuStrip = this.contextShowHideAll;
            this.listCollisionMeshes.FormattingEnabled = true;
            this.listCollisionMeshes.Location = new System.Drawing.Point(6, 6);
            this.listCollisionMeshes.Name = "listCollisionMeshes";
            this.listCollisionMeshes.Size = new System.Drawing.Size(260, 184);
            this.listCollisionMeshes.TabIndex = 22;
            this.listCollisionMeshes.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listCollisionMeshes_ItemCheck);
            this.listCollisionMeshes.SelectedValueChanged += new System.EventHandler(this.listCollisionMeshes_SelectedIndexChanged);
            // 
            // contextShowHideAll
            // 
            this.contextShowHideAll.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemShowAll,
            this.itemHideAll});
            this.contextShowHideAll.Name = "contextShowHideAll";
            this.contextShowHideAll.ShowImageMargin = false;
            this.contextShowHideAll.Size = new System.Drawing.Size(94, 48);
            this.contextShowHideAll.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextShowHideAll_ItemClicked);
            // 
            // itemShowAll
            // 
            this.itemShowAll.Name = "itemShowAll";
            this.itemShowAll.Size = new System.Drawing.Size(93, 22);
            this.itemShowAll.Text = "Show all";
            // 
            // itemHideAll
            // 
            this.itemHideAll.Name = "itemHideAll";
            this.itemHideAll.Size = new System.Drawing.Size(93, 22);
            this.itemHideAll.Text = "Hide all";
            // 
            // labelCollisionMeshParent
            // 
            this.labelCollisionMeshParent.AutoSize = true;
            this.labelCollisionMeshParent.Location = new System.Drawing.Point(6, 243);
            this.labelCollisionMeshParent.Name = "labelCollisionMeshParent";
            this.labelCollisionMeshParent.Size = new System.Drawing.Size(44, 13);
            this.labelCollisionMeshParent.TabIndex = 21;
            this.labelCollisionMeshParent.Text = "Parent: ";
            // 
            // comboCollisionMeshParent
            // 
            this.comboCollisionMeshParent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboCollisionMeshParent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboCollisionMeshParent.Enabled = false;
            this.comboCollisionMeshParent.FormattingEnabled = true;
            this.comboCollisionMeshParent.Location = new System.Drawing.Point(56, 240);
            this.comboCollisionMeshParent.Name = "comboCollisionMeshParent";
            this.comboCollisionMeshParent.Size = new System.Drawing.Size(210, 21);
            this.comboCollisionMeshParent.Sorted = true;
            this.comboCollisionMeshParent.TabIndex = 20;
            this.comboCollisionMeshParent.SelectedIndexChanged += new System.EventHandler(this.comboCollisionMeshParent_SelectedIndexChanged);
            // 
            // tabJoints
            // 
            this.tabJoints.AutoScroll = true;
            this.tabJoints.AutoScrollMinSize = new System.Drawing.Size(241, 727);
            this.tabJoints.Controls.Add(this.buttonJointRemoveAll);
            this.tabJoints.Controls.Add(this.buttonJointAddTemplate);
            this.tabJoints.Controls.Add(this.groupJointRotation);
            this.tabJoints.Controls.Add(this.groupJointPosition);
            this.tabJoints.Controls.Add(this.labelJointParent);
            this.tabJoints.Controls.Add(this.comboJointParent);
            this.tabJoints.Controls.Add(this.boxJointName);
            this.tabJoints.Controls.Add(this.labelJointName);
            this.tabJoints.Controls.Add(this.buttonJointRemove);
            this.tabJoints.Controls.Add(this.buttonJointAdd);
            this.tabJoints.Controls.Add(this.jointsTree);
            this.tabJoints.Location = new System.Drawing.Point(4, 58);
            this.tabJoints.Name = "tabJoints";
            this.tabJoints.Padding = new System.Windows.Forms.Padding(3);
            this.tabJoints.Size = new System.Drawing.Size(272, 758);
            this.tabJoints.TabIndex = 1;
            this.tabJoints.Text = "Joints";
            this.tabJoints.UseVisualStyleBackColor = true;
            // 
            // buttonJointRemoveAll
            // 
            this.buttonJointRemoveAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonJointRemoveAll.Enabled = false;
            this.buttonJointRemoveAll.Location = new System.Drawing.Point(174, 447);
            this.buttonJointRemoveAll.Name = "buttonJointRemoveAll";
            this.buttonJointRemoveAll.Size = new System.Drawing.Size(92, 23);
            this.buttonJointRemoveAll.TabIndex = 35;
            this.buttonJointRemoveAll.Text = "Remove all";
            this.buttonJointRemoveAll.UseVisualStyleBackColor = true;
            this.buttonJointRemoveAll.Click += new System.EventHandler(this.buttonJointRemoveAll_Click);
            // 
            // buttonJointAddTemplate
            // 
            this.buttonJointAddTemplate.Location = new System.Drawing.Point(6, 447);
            this.buttonJointAddTemplate.Name = "buttonJointAddTemplate";
            this.buttonJointAddTemplate.Size = new System.Drawing.Size(92, 23);
            this.buttonJointAddTemplate.TabIndex = 34;
            this.buttonJointAddTemplate.Text = "Add template";
            this.buttonJointAddTemplate.UseVisualStyleBackColor = true;
            this.buttonJointAddTemplate.Click += new System.EventHandler(this.buttonJointAddTemplate_Click);
            // 
            // groupJointRotation
            // 
            this.groupJointRotation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupJointRotation.Controls.Add(this.numericJointRotationZ);
            this.groupJointRotation.Controls.Add(this.labelJointRotationZ);
            this.groupJointRotation.Controls.Add(this.numericJointRotationY);
            this.groupJointRotation.Controls.Add(this.labelJointRotationY);
            this.groupJointRotation.Controls.Add(this.numericJointRotationX);
            this.groupJointRotation.Controls.Add(this.labelJointRotationX);
            this.groupJointRotation.Location = new System.Drawing.Point(6, 628);
            this.groupJointRotation.Name = "groupJointRotation";
            this.groupJointRotation.Size = new System.Drawing.Size(260, 93);
            this.groupJointRotation.TabIndex = 33;
            this.groupJointRotation.TabStop = false;
            this.groupJointRotation.Text = "Rotation";
            // 
            // numericJointRotationZ
            // 
            this.numericJointRotationZ.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericJointRotationZ.DecimalPlaces = 4;
            this.numericJointRotationZ.Enabled = false;
            this.numericJointRotationZ.Increment = new decimal(new int[] {
            45,
            0,
            0,
            65536});
            this.numericJointRotationZ.Location = new System.Drawing.Point(72, 66);
            this.numericJointRotationZ.Maximum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            0});
            this.numericJointRotationZ.Minimum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            -2147483648});
            this.numericJointRotationZ.Name = "numericJointRotationZ";
            this.numericJointRotationZ.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericJointRotationZ.Size = new System.Drawing.Size(182, 20);
            this.numericJointRotationZ.TabIndex = 27;
            this.numericJointRotationZ.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.numericJointRotationZ.ValueChanged += new System.EventHandler(this.numericJointRotation_ValueChanged);
            // 
            // labelJointRotationZ
            // 
            this.labelJointRotationZ.AutoSize = true;
            this.labelJointRotationZ.Location = new System.Drawing.Point(6, 68);
            this.labelJointRotationZ.Name = "labelJointRotationZ";
            this.labelJointRotationZ.Size = new System.Drawing.Size(17, 13);
            this.labelJointRotationZ.TabIndex = 26;
            this.labelJointRotationZ.Text = "Z:";
            // 
            // numericJointRotationY
            // 
            this.numericJointRotationY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericJointRotationY.DecimalPlaces = 4;
            this.numericJointRotationY.Enabled = false;
            this.numericJointRotationY.Increment = new decimal(new int[] {
            45,
            0,
            0,
            65536});
            this.numericJointRotationY.Location = new System.Drawing.Point(72, 40);
            this.numericJointRotationY.Maximum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            0});
            this.numericJointRotationY.Minimum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            -2147483648});
            this.numericJointRotationY.Name = "numericJointRotationY";
            this.numericJointRotationY.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericJointRotationY.Size = new System.Drawing.Size(182, 20);
            this.numericJointRotationY.TabIndex = 25;
            this.numericJointRotationY.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.numericJointRotationY.ValueChanged += new System.EventHandler(this.numericJointRotation_ValueChanged);
            // 
            // labelJointRotationY
            // 
            this.labelJointRotationY.AutoSize = true;
            this.labelJointRotationY.Location = new System.Drawing.Point(6, 42);
            this.labelJointRotationY.Name = "labelJointRotationY";
            this.labelJointRotationY.Size = new System.Drawing.Size(17, 13);
            this.labelJointRotationY.TabIndex = 24;
            this.labelJointRotationY.Text = "Y:";
            // 
            // numericJointRotationX
            // 
            this.numericJointRotationX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericJointRotationX.DecimalPlaces = 4;
            this.numericJointRotationX.Enabled = false;
            this.numericJointRotationX.Increment = new decimal(new int[] {
            45,
            0,
            0,
            65536});
            this.numericJointRotationX.Location = new System.Drawing.Point(72, 14);
            this.numericJointRotationX.Maximum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            0});
            this.numericJointRotationX.Minimum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            -2147483648});
            this.numericJointRotationX.Name = "numericJointRotationX";
            this.numericJointRotationX.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericJointRotationX.Size = new System.Drawing.Size(182, 20);
            this.numericJointRotationX.TabIndex = 23;
            this.numericJointRotationX.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.numericJointRotationX.ValueChanged += new System.EventHandler(this.numericJointRotation_ValueChanged);
            // 
            // labelJointRotationX
            // 
            this.labelJointRotationX.AutoSize = true;
            this.labelJointRotationX.Location = new System.Drawing.Point(6, 16);
            this.labelJointRotationX.Name = "labelJointRotationX";
            this.labelJointRotationX.Size = new System.Drawing.Size(17, 13);
            this.labelJointRotationX.TabIndex = 22;
            this.labelJointRotationX.Text = "X:";
            // 
            // groupJointPosition
            // 
            this.groupJointPosition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupJointPosition.Controls.Add(this.numericJointPositionZ);
            this.groupJointPosition.Controls.Add(this.labelJointPositionZ);
            this.groupJointPosition.Controls.Add(this.numericJointPositionY);
            this.groupJointPosition.Controls.Add(this.labelJointPositionY);
            this.groupJointPosition.Controls.Add(this.numericJointPositionX);
            this.groupJointPosition.Controls.Add(this.labelJointPositionX);
            this.groupJointPosition.Location = new System.Drawing.Point(6, 529);
            this.groupJointPosition.Name = "groupJointPosition";
            this.groupJointPosition.Size = new System.Drawing.Size(260, 93);
            this.groupJointPosition.TabIndex = 32;
            this.groupJointPosition.TabStop = false;
            this.groupJointPosition.Text = "Position";
            // 
            // numericJointPositionZ
            // 
            this.numericJointPositionZ.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericJointPositionZ.DecimalPlaces = 4;
            this.numericJointPositionZ.Enabled = false;
            this.numericJointPositionZ.Location = new System.Drawing.Point(72, 66);
            this.numericJointPositionZ.Maximum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            0});
            this.numericJointPositionZ.Minimum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            -2147483648});
            this.numericJointPositionZ.Name = "numericJointPositionZ";
            this.numericJointPositionZ.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericJointPositionZ.Size = new System.Drawing.Size(182, 20);
            this.numericJointPositionZ.TabIndex = 27;
            this.numericJointPositionZ.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.numericJointPositionZ.ValueChanged += new System.EventHandler(this.numericJointPosition_ValueChanged);
            // 
            // labelJointPositionZ
            // 
            this.labelJointPositionZ.AutoSize = true;
            this.labelJointPositionZ.Location = new System.Drawing.Point(6, 68);
            this.labelJointPositionZ.Name = "labelJointPositionZ";
            this.labelJointPositionZ.Size = new System.Drawing.Size(17, 13);
            this.labelJointPositionZ.TabIndex = 26;
            this.labelJointPositionZ.Text = "Z:";
            // 
            // numericJointPositionY
            // 
            this.numericJointPositionY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericJointPositionY.DecimalPlaces = 4;
            this.numericJointPositionY.Enabled = false;
            this.numericJointPositionY.Location = new System.Drawing.Point(72, 40);
            this.numericJointPositionY.Maximum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            0});
            this.numericJointPositionY.Minimum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            -2147483648});
            this.numericJointPositionY.Name = "numericJointPositionY";
            this.numericJointPositionY.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericJointPositionY.Size = new System.Drawing.Size(182, 20);
            this.numericJointPositionY.TabIndex = 25;
            this.numericJointPositionY.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.numericJointPositionY.ValueChanged += new System.EventHandler(this.numericJointPosition_ValueChanged);
            // 
            // labelJointPositionY
            // 
            this.labelJointPositionY.AutoSize = true;
            this.labelJointPositionY.Location = new System.Drawing.Point(6, 42);
            this.labelJointPositionY.Name = "labelJointPositionY";
            this.labelJointPositionY.Size = new System.Drawing.Size(17, 13);
            this.labelJointPositionY.TabIndex = 24;
            this.labelJointPositionY.Text = "Y:";
            // 
            // numericJointPositionX
            // 
            this.numericJointPositionX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericJointPositionX.DecimalPlaces = 4;
            this.numericJointPositionX.Enabled = false;
            this.numericJointPositionX.Location = new System.Drawing.Point(72, 14);
            this.numericJointPositionX.Maximum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            0});
            this.numericJointPositionX.Minimum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            -2147483648});
            this.numericJointPositionX.Name = "numericJointPositionX";
            this.numericJointPositionX.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericJointPositionX.Size = new System.Drawing.Size(182, 20);
            this.numericJointPositionX.TabIndex = 23;
            this.numericJointPositionX.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.numericJointPositionX.ValueChanged += new System.EventHandler(this.numericJointPosition_ValueChanged);
            // 
            // labelJointPositionX
            // 
            this.labelJointPositionX.AutoSize = true;
            this.labelJointPositionX.Location = new System.Drawing.Point(6, 16);
            this.labelJointPositionX.Name = "labelJointPositionX";
            this.labelJointPositionX.Size = new System.Drawing.Size(17, 13);
            this.labelJointPositionX.TabIndex = 22;
            this.labelJointPositionX.Text = "X:";
            // 
            // labelJointParent
            // 
            this.labelJointParent.AutoSize = true;
            this.labelJointParent.Location = new System.Drawing.Point(8, 505);
            this.labelJointParent.Name = "labelJointParent";
            this.labelJointParent.Size = new System.Drawing.Size(44, 13);
            this.labelJointParent.TabIndex = 31;
            this.labelJointParent.Text = "Parent: ";
            // 
            // comboJointParent
            // 
            this.comboJointParent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboJointParent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboJointParent.Enabled = false;
            this.comboJointParent.FormattingEnabled = true;
            this.comboJointParent.Location = new System.Drawing.Point(56, 502);
            this.comboJointParent.Name = "comboJointParent";
            this.comboJointParent.Size = new System.Drawing.Size(210, 21);
            this.comboJointParent.Sorted = true;
            this.comboJointParent.TabIndex = 30;
            this.comboJointParent.SelectedIndexChanged += new System.EventHandler(this.comboJointParent_SelectedIndexChanged);
            // 
            // boxJointName
            // 
            this.boxJointName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boxJointName.Enabled = false;
            this.boxJointName.Location = new System.Drawing.Point(56, 476);
            this.boxJointName.Name = "boxJointName";
            this.boxJointName.Size = new System.Drawing.Size(210, 20);
            this.boxJointName.TabIndex = 29;
            this.boxJointName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.boxJointName_KeyPress);
            this.boxJointName.Leave += new System.EventHandler(this.boxJointName_Leave);
            // 
            // labelJointName
            // 
            this.labelJointName.AutoSize = true;
            this.labelJointName.Location = new System.Drawing.Point(8, 479);
            this.labelJointName.Name = "labelJointName";
            this.labelJointName.Size = new System.Drawing.Size(41, 13);
            this.labelJointName.TabIndex = 28;
            this.labelJointName.Text = "Name: ";
            // 
            // buttonJointRemove
            // 
            this.buttonJointRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonJointRemove.Enabled = false;
            this.buttonJointRemove.Location = new System.Drawing.Point(174, 418);
            this.buttonJointRemove.Name = "buttonJointRemove";
            this.buttonJointRemove.Size = new System.Drawing.Size(92, 23);
            this.buttonJointRemove.TabIndex = 27;
            this.buttonJointRemove.Text = "Remove";
            this.buttonJointRemove.UseVisualStyleBackColor = true;
            this.buttonJointRemove.Click += new System.EventHandler(this.buttonJointRemove_Click);
            // 
            // buttonJointAdd
            // 
            this.buttonJointAdd.Location = new System.Drawing.Point(6, 418);
            this.buttonJointAdd.Name = "buttonJointAdd";
            this.buttonJointAdd.Size = new System.Drawing.Size(92, 23);
            this.buttonJointAdd.TabIndex = 26;
            this.buttonJointAdd.Text = "Add";
            this.buttonJointAdd.UseVisualStyleBackColor = true;
            this.buttonJointAdd.Click += new System.EventHandler(this.buttonJointAdd_Click);
            // 
            // jointsTree
            // 
            this.jointsTree.AllowDrop = true;
            this.jointsTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.jointsTree.CheckBoxes = true;
            this.jointsTree.Location = new System.Drawing.Point(6, 6);
            this.jointsTree.Name = "jointsTree";
            this.jointsTree.Size = new System.Drawing.Size(260, 406);
            this.jointsTree.TabIndex = 0;
            this.jointsTree.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.jointsTree_AfterCheck);
            this.jointsTree.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.jointsTree_ItemDrag);
            this.jointsTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.jointsTree_AfterSelect);
            this.jointsTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.jointsTree_NodeMouseClick);
            this.jointsTree.DragDrop += new System.Windows.Forms.DragEventHandler(this.jointsTree_DragDrop);
            this.jointsTree.DragEnter += new System.Windows.Forms.DragEventHandler(this.jointsTree_DragEnter);
            // 
            // tabMarkers
            // 
            this.tabMarkers.AutoScroll = true;
            this.tabMarkers.AutoScrollMinSize = new System.Drawing.Size(241, 748);
            this.tabMarkers.Controls.Add(this.groupMarkerRotation);
            this.tabMarkers.Controls.Add(this.groupMarkerPosition);
            this.tabMarkers.Controls.Add(this.labelMarkerParent);
            this.tabMarkers.Controls.Add(this.comboMarkerParent);
            this.tabMarkers.Controls.Add(this.boxMarkerName);
            this.tabMarkers.Controls.Add(this.labelMarkerName);
            this.tabMarkers.Controls.Add(this.buttonMarkerRemove);
            this.tabMarkers.Controls.Add(this.buttonMarkerAdd);
            this.tabMarkers.Controls.Add(this.groupMarkerPreview);
            this.tabMarkers.Controls.Add(this.listMarkers);
            this.tabMarkers.Location = new System.Drawing.Point(4, 58);
            this.tabMarkers.Name = "tabMarkers";
            this.tabMarkers.Size = new System.Drawing.Size(272, 758);
            this.tabMarkers.TabIndex = 2;
            this.tabMarkers.Text = "Markers";
            this.tabMarkers.UseVisualStyleBackColor = true;
            // 
            // groupMarkerRotation
            // 
            this.groupMarkerRotation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupMarkerRotation.Controls.Add(this.numericMarkerRotationZ);
            this.groupMarkerRotation.Controls.Add(this.labelMarkerRotationZ);
            this.groupMarkerRotation.Controls.Add(this.numericMarkerRotationY);
            this.groupMarkerRotation.Controls.Add(this.labelMarkerRotationY);
            this.groupMarkerRotation.Controls.Add(this.numericMarkerRotationX);
            this.groupMarkerRotation.Controls.Add(this.labelMarkerRotationX);
            this.groupMarkerRotation.Location = new System.Drawing.Point(6, 600);
            this.groupMarkerRotation.Name = "groupMarkerRotation";
            this.groupMarkerRotation.Size = new System.Drawing.Size(260, 93);
            this.groupMarkerRotation.TabIndex = 41;
            this.groupMarkerRotation.TabStop = false;
            this.groupMarkerRotation.Text = "Rotation";
            // 
            // numericMarkerRotationZ
            // 
            this.numericMarkerRotationZ.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericMarkerRotationZ.DecimalPlaces = 4;
            this.numericMarkerRotationZ.Enabled = false;
            this.numericMarkerRotationZ.Increment = new decimal(new int[] {
            45,
            0,
            0,
            65536});
            this.numericMarkerRotationZ.Location = new System.Drawing.Point(72, 66);
            this.numericMarkerRotationZ.Maximum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            0});
            this.numericMarkerRotationZ.Minimum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            -2147483648});
            this.numericMarkerRotationZ.Name = "numericMarkerRotationZ";
            this.numericMarkerRotationZ.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericMarkerRotationZ.Size = new System.Drawing.Size(182, 20);
            this.numericMarkerRotationZ.TabIndex = 27;
            this.numericMarkerRotationZ.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.numericMarkerRotationZ.ValueChanged += new System.EventHandler(this.numericMarkerRotation_ValueChanged);
            // 
            // labelMarkerRotationZ
            // 
            this.labelMarkerRotationZ.AutoSize = true;
            this.labelMarkerRotationZ.Location = new System.Drawing.Point(6, 68);
            this.labelMarkerRotationZ.Name = "labelMarkerRotationZ";
            this.labelMarkerRotationZ.Size = new System.Drawing.Size(17, 13);
            this.labelMarkerRotationZ.TabIndex = 26;
            this.labelMarkerRotationZ.Text = "Z:";
            // 
            // numericMarkerRotationY
            // 
            this.numericMarkerRotationY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericMarkerRotationY.DecimalPlaces = 4;
            this.numericMarkerRotationY.Enabled = false;
            this.numericMarkerRotationY.Increment = new decimal(new int[] {
            45,
            0,
            0,
            65536});
            this.numericMarkerRotationY.Location = new System.Drawing.Point(72, 40);
            this.numericMarkerRotationY.Maximum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            0});
            this.numericMarkerRotationY.Minimum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            -2147483648});
            this.numericMarkerRotationY.Name = "numericMarkerRotationY";
            this.numericMarkerRotationY.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericMarkerRotationY.Size = new System.Drawing.Size(182, 20);
            this.numericMarkerRotationY.TabIndex = 25;
            this.numericMarkerRotationY.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.numericMarkerRotationY.ValueChanged += new System.EventHandler(this.numericMarkerRotation_ValueChanged);
            // 
            // labelMarkerRotationY
            // 
            this.labelMarkerRotationY.AutoSize = true;
            this.labelMarkerRotationY.Location = new System.Drawing.Point(6, 42);
            this.labelMarkerRotationY.Name = "labelMarkerRotationY";
            this.labelMarkerRotationY.Size = new System.Drawing.Size(17, 13);
            this.labelMarkerRotationY.TabIndex = 24;
            this.labelMarkerRotationY.Text = "Y:";
            // 
            // numericMarkerRotationX
            // 
            this.numericMarkerRotationX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericMarkerRotationX.DecimalPlaces = 4;
            this.numericMarkerRotationX.Enabled = false;
            this.numericMarkerRotationX.Increment = new decimal(new int[] {
            45,
            0,
            0,
            65536});
            this.numericMarkerRotationX.Location = new System.Drawing.Point(72, 14);
            this.numericMarkerRotationX.Maximum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            0});
            this.numericMarkerRotationX.Minimum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            -2147483648});
            this.numericMarkerRotationX.Name = "numericMarkerRotationX";
            this.numericMarkerRotationX.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericMarkerRotationX.Size = new System.Drawing.Size(182, 20);
            this.numericMarkerRotationX.TabIndex = 23;
            this.numericMarkerRotationX.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.numericMarkerRotationX.ValueChanged += new System.EventHandler(this.numericMarkerRotation_ValueChanged);
            // 
            // labelMarkerRotationX
            // 
            this.labelMarkerRotationX.AutoSize = true;
            this.labelMarkerRotationX.Location = new System.Drawing.Point(6, 16);
            this.labelMarkerRotationX.Name = "labelMarkerRotationX";
            this.labelMarkerRotationX.Size = new System.Drawing.Size(17, 13);
            this.labelMarkerRotationX.TabIndex = 22;
            this.labelMarkerRotationX.Text = "X:";
            // 
            // groupMarkerPosition
            // 
            this.groupMarkerPosition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupMarkerPosition.Controls.Add(this.numericMarkerPositionZ);
            this.groupMarkerPosition.Controls.Add(this.labelMarkerPositionZ);
            this.groupMarkerPosition.Controls.Add(this.numericMarkerPositionY);
            this.groupMarkerPosition.Controls.Add(this.labelMarkerPositionY);
            this.groupMarkerPosition.Controls.Add(this.numericMarkerPositionX);
            this.groupMarkerPosition.Controls.Add(this.labelMarkerPositionX);
            this.groupMarkerPosition.Location = new System.Drawing.Point(6, 501);
            this.groupMarkerPosition.Name = "groupMarkerPosition";
            this.groupMarkerPosition.Size = new System.Drawing.Size(260, 93);
            this.groupMarkerPosition.TabIndex = 40;
            this.groupMarkerPosition.TabStop = false;
            this.groupMarkerPosition.Text = "Position";
            // 
            // numericMarkerPositionZ
            // 
            this.numericMarkerPositionZ.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericMarkerPositionZ.DecimalPlaces = 4;
            this.numericMarkerPositionZ.Enabled = false;
            this.numericMarkerPositionZ.Location = new System.Drawing.Point(72, 66);
            this.numericMarkerPositionZ.Maximum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            0});
            this.numericMarkerPositionZ.Minimum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            -2147483648});
            this.numericMarkerPositionZ.Name = "numericMarkerPositionZ";
            this.numericMarkerPositionZ.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericMarkerPositionZ.Size = new System.Drawing.Size(182, 20);
            this.numericMarkerPositionZ.TabIndex = 27;
            this.numericMarkerPositionZ.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.numericMarkerPositionZ.ValueChanged += new System.EventHandler(this.numericMarkerPosition_ValueChanged);
            // 
            // labelMarkerPositionZ
            // 
            this.labelMarkerPositionZ.AutoSize = true;
            this.labelMarkerPositionZ.Location = new System.Drawing.Point(6, 68);
            this.labelMarkerPositionZ.Name = "labelMarkerPositionZ";
            this.labelMarkerPositionZ.Size = new System.Drawing.Size(17, 13);
            this.labelMarkerPositionZ.TabIndex = 26;
            this.labelMarkerPositionZ.Text = "Z:";
            // 
            // numericMarkerPositionY
            // 
            this.numericMarkerPositionY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericMarkerPositionY.DecimalPlaces = 4;
            this.numericMarkerPositionY.Enabled = false;
            this.numericMarkerPositionY.Location = new System.Drawing.Point(72, 40);
            this.numericMarkerPositionY.Maximum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            0});
            this.numericMarkerPositionY.Minimum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            -2147483648});
            this.numericMarkerPositionY.Name = "numericMarkerPositionY";
            this.numericMarkerPositionY.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericMarkerPositionY.Size = new System.Drawing.Size(182, 20);
            this.numericMarkerPositionY.TabIndex = 25;
            this.numericMarkerPositionY.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.numericMarkerPositionY.ValueChanged += new System.EventHandler(this.numericMarkerPosition_ValueChanged);
            // 
            // labelMarkerPositionY
            // 
            this.labelMarkerPositionY.AutoSize = true;
            this.labelMarkerPositionY.Location = new System.Drawing.Point(6, 42);
            this.labelMarkerPositionY.Name = "labelMarkerPositionY";
            this.labelMarkerPositionY.Size = new System.Drawing.Size(17, 13);
            this.labelMarkerPositionY.TabIndex = 24;
            this.labelMarkerPositionY.Text = "Y:";
            // 
            // numericMarkerPositionX
            // 
            this.numericMarkerPositionX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericMarkerPositionX.DecimalPlaces = 4;
            this.numericMarkerPositionX.Enabled = false;
            this.numericMarkerPositionX.Location = new System.Drawing.Point(72, 14);
            this.numericMarkerPositionX.Maximum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            0});
            this.numericMarkerPositionX.Minimum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            -2147483648});
            this.numericMarkerPositionX.Name = "numericMarkerPositionX";
            this.numericMarkerPositionX.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericMarkerPositionX.Size = new System.Drawing.Size(182, 20);
            this.numericMarkerPositionX.TabIndex = 23;
            this.numericMarkerPositionX.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.numericMarkerPositionX.ValueChanged += new System.EventHandler(this.numericMarkerPosition_ValueChanged);
            // 
            // labelMarkerPositionX
            // 
            this.labelMarkerPositionX.AutoSize = true;
            this.labelMarkerPositionX.Location = new System.Drawing.Point(6, 16);
            this.labelMarkerPositionX.Name = "labelMarkerPositionX";
            this.labelMarkerPositionX.Size = new System.Drawing.Size(17, 13);
            this.labelMarkerPositionX.TabIndex = 22;
            this.labelMarkerPositionX.Text = "X:";
            // 
            // labelMarkerParent
            // 
            this.labelMarkerParent.AutoSize = true;
            this.labelMarkerParent.Location = new System.Drawing.Point(8, 477);
            this.labelMarkerParent.Name = "labelMarkerParent";
            this.labelMarkerParent.Size = new System.Drawing.Size(44, 13);
            this.labelMarkerParent.TabIndex = 39;
            this.labelMarkerParent.Text = "Parent: ";
            // 
            // comboMarkerParent
            // 
            this.comboMarkerParent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboMarkerParent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboMarkerParent.Enabled = false;
            this.comboMarkerParent.FormattingEnabled = true;
            this.comboMarkerParent.Location = new System.Drawing.Point(56, 474);
            this.comboMarkerParent.Name = "comboMarkerParent";
            this.comboMarkerParent.Size = new System.Drawing.Size(210, 21);
            this.comboMarkerParent.Sorted = true;
            this.comboMarkerParent.TabIndex = 38;
            this.comboMarkerParent.SelectedIndexChanged += new System.EventHandler(this.comboMarkerParent_SelectedIndexChanged);
            // 
            // boxMarkerName
            // 
            this.boxMarkerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boxMarkerName.Enabled = false;
            this.boxMarkerName.Location = new System.Drawing.Point(56, 448);
            this.boxMarkerName.Name = "boxMarkerName";
            this.boxMarkerName.Size = new System.Drawing.Size(210, 20);
            this.boxMarkerName.TabIndex = 37;
            this.boxMarkerName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.boxMarkerName_KeyPress);
            this.boxMarkerName.Leave += new System.EventHandler(this.boxMarkerName_Leave);
            // 
            // labelMarkerName
            // 
            this.labelMarkerName.AutoSize = true;
            this.labelMarkerName.Location = new System.Drawing.Point(8, 451);
            this.labelMarkerName.Name = "labelMarkerName";
            this.labelMarkerName.Size = new System.Drawing.Size(41, 13);
            this.labelMarkerName.TabIndex = 36;
            this.labelMarkerName.Text = "Name: ";
            // 
            // buttonMarkerRemove
            // 
            this.buttonMarkerRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMarkerRemove.Enabled = false;
            this.buttonMarkerRemove.Location = new System.Drawing.Point(174, 419);
            this.buttonMarkerRemove.Name = "buttonMarkerRemove";
            this.buttonMarkerRemove.Size = new System.Drawing.Size(92, 23);
            this.buttonMarkerRemove.TabIndex = 35;
            this.buttonMarkerRemove.Text = "Remove";
            this.buttonMarkerRemove.UseVisualStyleBackColor = true;
            this.buttonMarkerRemove.Click += new System.EventHandler(this.buttonMarkerRemove_Click);
            // 
            // buttonMarkerAdd
            // 
            this.buttonMarkerAdd.Location = new System.Drawing.Point(6, 419);
            this.buttonMarkerAdd.Name = "buttonMarkerAdd";
            this.buttonMarkerAdd.Size = new System.Drawing.Size(92, 23);
            this.buttonMarkerAdd.TabIndex = 34;
            this.buttonMarkerAdd.Text = "Add";
            this.buttonMarkerAdd.UseVisualStyleBackColor = true;
            this.buttonMarkerAdd.Click += new System.EventHandler(this.buttonMarkerAdd_Click);
            // 
            // groupMarkerPreview
            // 
            this.groupMarkerPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupMarkerPreview.Controls.Add(this.checkDrawMarkers);
            this.groupMarkerPreview.Location = new System.Drawing.Point(6, 699);
            this.groupMarkerPreview.Name = "groupMarkerPreview";
            this.groupMarkerPreview.Size = new System.Drawing.Size(260, 43);
            this.groupMarkerPreview.TabIndex = 15;
            this.groupMarkerPreview.TabStop = false;
            this.groupMarkerPreview.Text = "Preview";
            // 
            // checkDrawMarkers
            // 
            this.checkDrawMarkers.AutoSize = true;
            this.checkDrawMarkers.Location = new System.Drawing.Point(6, 19);
            this.checkDrawMarkers.Name = "checkDrawMarkers";
            this.checkDrawMarkers.Size = new System.Drawing.Size(91, 17);
            this.checkDrawMarkers.TabIndex = 5;
            this.checkDrawMarkers.Text = "Draw markers";
            this.checkDrawMarkers.UseVisualStyleBackColor = true;
            this.checkDrawMarkers.CheckedChanged += new System.EventHandler(this.checkDrawMarkers_CheckedChanged);
            // 
            // listMarkers
            // 
            this.listMarkers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listMarkers.FormattingEnabled = true;
            this.listMarkers.Location = new System.Drawing.Point(6, 6);
            this.listMarkers.Name = "listMarkers";
            this.listMarkers.Size = new System.Drawing.Size(260, 407);
            this.listMarkers.TabIndex = 3;
            this.listMarkers.SelectedIndexChanged += new System.EventHandler(this.listMarkers_SelectedIndexChanged);
            // 
            // tabDockpaths
            // 
            this.tabDockpaths.AutoScrollMinSize = new System.Drawing.Size(241, 923);
            this.tabDockpaths.Controls.Add(this.buttonDockpathRemove);
            this.tabDockpaths.Controls.Add(this.buttonDockpathAdd);
            this.tabDockpaths.Controls.Add(this.listDockpaths);
            this.tabDockpaths.Controls.Add(this.labelDockpathAnimationIndex);
            this.tabDockpaths.Controls.Add(this.numericDockpathAnimationIndex);
            this.tabDockpaths.Controls.Add(this.boxDockpathName);
            this.tabDockpaths.Controls.Add(this.labelDockpathName);
            this.tabDockpaths.Controls.Add(this.groupDockpathFlags);
            this.tabDockpaths.Controls.Add(this.groupDockpathSegments);
            this.tabDockpaths.Controls.Add(this.groupDockpathLinks);
            this.tabDockpaths.Controls.Add(this.groupDockpathFamilies);
            this.tabDockpaths.Location = new System.Drawing.Point(4, 58);
            this.tabDockpaths.Name = "tabDockpaths";
            this.tabDockpaths.Size = new System.Drawing.Size(272, 758);
            this.tabDockpaths.TabIndex = 3;
            this.tabDockpaths.Text = "Dockpaths";
            this.tabDockpaths.UseVisualStyleBackColor = true;
            // 
            // buttonDockpathRemove
            // 
            this.buttonDockpathRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDockpathRemove.Enabled = false;
            this.buttonDockpathRemove.Location = new System.Drawing.Point(174, 166);
            this.buttonDockpathRemove.Name = "buttonDockpathRemove";
            this.buttonDockpathRemove.Size = new System.Drawing.Size(92, 23);
            this.buttonDockpathRemove.TabIndex = 27;
            this.buttonDockpathRemove.Text = "Remove";
            this.buttonDockpathRemove.UseVisualStyleBackColor = true;
            this.buttonDockpathRemove.Click += new System.EventHandler(this.buttonDockpathRemove_Click);
            // 
            // buttonDockpathAdd
            // 
            this.buttonDockpathAdd.Location = new System.Drawing.Point(6, 166);
            this.buttonDockpathAdd.Name = "buttonDockpathAdd";
            this.buttonDockpathAdd.Size = new System.Drawing.Size(92, 23);
            this.buttonDockpathAdd.TabIndex = 26;
            this.buttonDockpathAdd.Text = "Add";
            this.buttonDockpathAdd.UseVisualStyleBackColor = true;
            this.buttonDockpathAdd.Click += new System.EventHandler(this.buttonDockpathAdd_Click);
            // 
            // listDockpaths
            // 
            this.listDockpaths.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listDockpaths.ContextMenuStrip = this.contextShowHideAll;
            this.listDockpaths.FormattingEnabled = true;
            this.listDockpaths.Location = new System.Drawing.Point(6, 6);
            this.listDockpaths.Name = "listDockpaths";
            this.listDockpaths.Size = new System.Drawing.Size(260, 154);
            this.listDockpaths.TabIndex = 25;
            this.listDockpaths.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listDockpaths_ItemCheck);
            this.listDockpaths.SelectedIndexChanged += new System.EventHandler(this.listDockpaths_SelectedIndexChanged);
            // 
            // labelDockpathAnimationIndex
            // 
            this.labelDockpathAnimationIndex.AutoSize = true;
            this.labelDockpathAnimationIndex.Location = new System.Drawing.Point(8, 223);
            this.labelDockpathAnimationIndex.Name = "labelDockpathAnimationIndex";
            this.labelDockpathAnimationIndex.Size = new System.Drawing.Size(84, 13);
            this.labelDockpathAnimationIndex.TabIndex = 24;
            this.labelDockpathAnimationIndex.Text = "Animation index:";
            // 
            // numericDockpathAnimationIndex
            // 
            this.numericDockpathAnimationIndex.Enabled = false;
            this.numericDockpathAnimationIndex.Location = new System.Drawing.Point(98, 221);
            this.numericDockpathAnimationIndex.Name = "numericDockpathAnimationIndex";
            this.numericDockpathAnimationIndex.Size = new System.Drawing.Size(41, 20);
            this.numericDockpathAnimationIndex.TabIndex = 23;
            this.numericDockpathAnimationIndex.ValueChanged += new System.EventHandler(this.numericDockpathAnimationIndex_ValueChanged);
            // 
            // boxDockpathName
            // 
            this.boxDockpathName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boxDockpathName.Enabled = false;
            this.boxDockpathName.Location = new System.Drawing.Point(49, 195);
            this.boxDockpathName.Name = "boxDockpathName";
            this.boxDockpathName.Size = new System.Drawing.Size(217, 20);
            this.boxDockpathName.TabIndex = 22;
            this.boxDockpathName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.boxDockpathName_KeyPress);
            this.boxDockpathName.Leave += new System.EventHandler(this.boxDockpathName_Leave);
            // 
            // labelDockpathName
            // 
            this.labelDockpathName.AutoSize = true;
            this.labelDockpathName.Location = new System.Drawing.Point(8, 198);
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
            this.groupDockpathFlags.Location = new System.Drawing.Point(6, 247);
            this.groupDockpathFlags.Name = "groupDockpathFlags";
            this.groupDockpathFlags.Size = new System.Drawing.Size(260, 64);
            this.groupDockpathFlags.TabIndex = 12;
            this.groupDockpathFlags.TabStop = false;
            this.groupDockpathFlags.Text = "Flags";
            // 
            // checkDockpathAjar
            // 
            this.checkDockpathAjar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkDockpathAjar.AutoSize = true;
            this.checkDockpathAjar.Enabled = false;
            this.checkDockpathAjar.Location = new System.Drawing.Point(201, 42);
            this.checkDockpathAjar.Name = "checkDockpathAjar";
            this.checkDockpathAjar.Size = new System.Drawing.Size(44, 17);
            this.checkDockpathAjar.TabIndex = 11;
            this.checkDockpathAjar.Text = "Ajar";
            this.checkDockpathAjar.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.checkDockpathAjar.UseVisualStyleBackColor = true;
            this.checkDockpathAjar.CheckedChanged += new System.EventHandler(this.DockpathFlagsChanged);
            // 
            // checkDockpathLatch
            // 
            this.checkDockpathLatch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkDockpathLatch.AutoSize = true;
            this.checkDockpathLatch.Enabled = false;
            this.checkDockpathLatch.Location = new System.Drawing.Point(201, 19);
            this.checkDockpathLatch.Name = "checkDockpathLatch";
            this.checkDockpathLatch.Size = new System.Drawing.Size(53, 17);
            this.checkDockpathLatch.TabIndex = 10;
            this.checkDockpathLatch.Text = "Latch";
            this.checkDockpathLatch.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.checkDockpathLatch.UseVisualStyleBackColor = true;
            this.checkDockpathLatch.CheckedChanged += new System.EventHandler(this.DockpathFlagsChanged);
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
            this.checkDockpathAnim.CheckedChanged += new System.EventHandler(this.DockpathFlagsChanged);
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
            this.checkDockpathExit.CheckedChanged += new System.EventHandler(this.DockpathFlagsChanged);
            // 
            // groupDockpathSegments
            // 
            this.groupDockpathSegments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupDockpathSegments.Controls.Add(this.groupDockpathSegmentRotation);
            this.groupDockpathSegments.Controls.Add(this.groupDockpathSegmentPosition);
            this.groupDockpathSegments.Controls.Add(this.buttonDockpathSegmentInsertBefore);
            this.groupDockpathSegments.Controls.Add(this.numericDockpathSegmentSpeed);
            this.groupDockpathSegments.Controls.Add(this.numericDockpathSegmentTolerance);
            this.groupDockpathSegments.Controls.Add(this.buttonDockpathSegmentRemove);
            this.groupDockpathSegments.Controls.Add(this.buttonDockpathSegmentInsertAfter);
            this.groupDockpathSegments.Controls.Add(this.groupDockpathSegmentFlags);
            this.groupDockpathSegments.Controls.Add(this.labelDockpathSegmentSpeed);
            this.groupDockpathSegments.Controls.Add(this.labelDockpathSegmentTolerance);
            this.groupDockpathSegments.Controls.Add(this.trackBarDockpathSegments);
            this.groupDockpathSegments.Location = new System.Drawing.Point(6, 653);
            this.groupDockpathSegments.Name = "groupDockpathSegments";
            this.groupDockpathSegments.Size = new System.Drawing.Size(260, 468);
            this.groupDockpathSegments.TabIndex = 3;
            this.groupDockpathSegments.TabStop = false;
            this.groupDockpathSegments.Text = "Segments";
            // 
            // groupDockpathSegmentRotation
            // 
            this.groupDockpathSegmentRotation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupDockpathSegmentRotation.Controls.Add(this.numericDockpathSegmentRotationZ);
            this.groupDockpathSegmentRotation.Controls.Add(this.labelDockpathSegmentRotationZ);
            this.groupDockpathSegmentRotation.Controls.Add(this.numericDockpathSegmentRotationY);
            this.groupDockpathSegmentRotation.Controls.Add(this.labelDockpathSegmentRotationY);
            this.groupDockpathSegmentRotation.Controls.Add(this.numericDockpathSegmentRotationX);
            this.groupDockpathSegmentRotation.Controls.Add(this.labelDockpathSegmentRotationX);
            this.groupDockpathSegmentRotation.Location = new System.Drawing.Point(3, 198);
            this.groupDockpathSegmentRotation.Name = "groupDockpathSegmentRotation";
            this.groupDockpathSegmentRotation.Size = new System.Drawing.Size(253, 96);
            this.groupDockpathSegmentRotation.TabIndex = 43;
            this.groupDockpathSegmentRotation.TabStop = false;
            this.groupDockpathSegmentRotation.Text = "Rotation";
            // 
            // numericDockpathSegmentRotationZ
            // 
            this.numericDockpathSegmentRotationZ.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericDockpathSegmentRotationZ.DecimalPlaces = 4;
            this.numericDockpathSegmentRotationZ.Enabled = false;
            this.numericDockpathSegmentRotationZ.Location = new System.Drawing.Point(29, 71);
            this.numericDockpathSegmentRotationZ.Maximum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            0});
            this.numericDockpathSegmentRotationZ.Minimum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            -2147483648});
            this.numericDockpathSegmentRotationZ.Name = "numericDockpathSegmentRotationZ";
            this.numericDockpathSegmentRotationZ.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericDockpathSegmentRotationZ.Size = new System.Drawing.Size(218, 20);
            this.numericDockpathSegmentRotationZ.TabIndex = 42;
            this.numericDockpathSegmentRotationZ.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.numericDockpathSegmentRotationZ.ValueChanged += new System.EventHandler(this.DockpathSegmentRotationChanged);
            // 
            // labelDockpathSegmentRotationZ
            // 
            this.labelDockpathSegmentRotationZ.AutoSize = true;
            this.labelDockpathSegmentRotationZ.Location = new System.Drawing.Point(6, 73);
            this.labelDockpathSegmentRotationZ.Name = "labelDockpathSegmentRotationZ";
            this.labelDockpathSegmentRotationZ.Size = new System.Drawing.Size(17, 13);
            this.labelDockpathSegmentRotationZ.TabIndex = 41;
            this.labelDockpathSegmentRotationZ.Text = "Z:";
            // 
            // numericDockpathSegmentRotationY
            // 
            this.numericDockpathSegmentRotationY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericDockpathSegmentRotationY.DecimalPlaces = 4;
            this.numericDockpathSegmentRotationY.Enabled = false;
            this.numericDockpathSegmentRotationY.Location = new System.Drawing.Point(29, 45);
            this.numericDockpathSegmentRotationY.Maximum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            0});
            this.numericDockpathSegmentRotationY.Minimum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            -2147483648});
            this.numericDockpathSegmentRotationY.Name = "numericDockpathSegmentRotationY";
            this.numericDockpathSegmentRotationY.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericDockpathSegmentRotationY.Size = new System.Drawing.Size(218, 20);
            this.numericDockpathSegmentRotationY.TabIndex = 40;
            this.numericDockpathSegmentRotationY.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.numericDockpathSegmentRotationY.ValueChanged += new System.EventHandler(this.DockpathSegmentRotationChanged);
            // 
            // labelDockpathSegmentRotationY
            // 
            this.labelDockpathSegmentRotationY.AutoSize = true;
            this.labelDockpathSegmentRotationY.Location = new System.Drawing.Point(6, 47);
            this.labelDockpathSegmentRotationY.Name = "labelDockpathSegmentRotationY";
            this.labelDockpathSegmentRotationY.Size = new System.Drawing.Size(17, 13);
            this.labelDockpathSegmentRotationY.TabIndex = 39;
            this.labelDockpathSegmentRotationY.Text = "Y:";
            // 
            // numericDockpathSegmentRotationX
            // 
            this.numericDockpathSegmentRotationX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericDockpathSegmentRotationX.DecimalPlaces = 4;
            this.numericDockpathSegmentRotationX.Enabled = false;
            this.numericDockpathSegmentRotationX.Location = new System.Drawing.Point(29, 19);
            this.numericDockpathSegmentRotationX.Maximum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            0});
            this.numericDockpathSegmentRotationX.Minimum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            -2147483648});
            this.numericDockpathSegmentRotationX.Name = "numericDockpathSegmentRotationX";
            this.numericDockpathSegmentRotationX.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericDockpathSegmentRotationX.Size = new System.Drawing.Size(218, 20);
            this.numericDockpathSegmentRotationX.TabIndex = 38;
            this.numericDockpathSegmentRotationX.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.numericDockpathSegmentRotationX.ValueChanged += new System.EventHandler(this.DockpathSegmentRotationChanged);
            // 
            // labelDockpathSegmentRotationX
            // 
            this.labelDockpathSegmentRotationX.AutoSize = true;
            this.labelDockpathSegmentRotationX.Location = new System.Drawing.Point(6, 21);
            this.labelDockpathSegmentRotationX.Name = "labelDockpathSegmentRotationX";
            this.labelDockpathSegmentRotationX.Size = new System.Drawing.Size(17, 13);
            this.labelDockpathSegmentRotationX.TabIndex = 37;
            this.labelDockpathSegmentRotationX.Text = "X:";
            // 
            // groupDockpathSegmentPosition
            // 
            this.groupDockpathSegmentPosition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupDockpathSegmentPosition.Controls.Add(this.numericDockpathSegmentPosZ);
            this.groupDockpathSegmentPosition.Controls.Add(this.labelDockpathSegmentPosZ);
            this.groupDockpathSegmentPosition.Controls.Add(this.numericDockpathSegmentPosY);
            this.groupDockpathSegmentPosition.Controls.Add(this.labelDockpathSegmentPosY);
            this.groupDockpathSegmentPosition.Controls.Add(this.numericDockpathSegmentPosX);
            this.groupDockpathSegmentPosition.Controls.Add(this.labelDockpathSegmentPosX);
            this.groupDockpathSegmentPosition.Location = new System.Drawing.Point(4, 96);
            this.groupDockpathSegmentPosition.Name = "groupDockpathSegmentPosition";
            this.groupDockpathSegmentPosition.Size = new System.Drawing.Size(253, 96);
            this.groupDockpathSegmentPosition.TabIndex = 37;
            this.groupDockpathSegmentPosition.TabStop = false;
            this.groupDockpathSegmentPosition.Text = "Position";
            // 
            // numericDockpathSegmentPosZ
            // 
            this.numericDockpathSegmentPosZ.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericDockpathSegmentPosZ.DecimalPlaces = 4;
            this.numericDockpathSegmentPosZ.Enabled = false;
            this.numericDockpathSegmentPosZ.Location = new System.Drawing.Point(29, 71);
            this.numericDockpathSegmentPosZ.Maximum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            0});
            this.numericDockpathSegmentPosZ.Minimum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            -2147483648});
            this.numericDockpathSegmentPosZ.Name = "numericDockpathSegmentPosZ";
            this.numericDockpathSegmentPosZ.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericDockpathSegmentPosZ.Size = new System.Drawing.Size(218, 20);
            this.numericDockpathSegmentPosZ.TabIndex = 42;
            this.numericDockpathSegmentPosZ.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.numericDockpathSegmentPosZ.ValueChanged += new System.EventHandler(this.DockpathSegmentPositionChanged);
            // 
            // labelDockpathSegmentPosZ
            // 
            this.labelDockpathSegmentPosZ.AutoSize = true;
            this.labelDockpathSegmentPosZ.Location = new System.Drawing.Point(6, 73);
            this.labelDockpathSegmentPosZ.Name = "labelDockpathSegmentPosZ";
            this.labelDockpathSegmentPosZ.Size = new System.Drawing.Size(17, 13);
            this.labelDockpathSegmentPosZ.TabIndex = 41;
            this.labelDockpathSegmentPosZ.Text = "Z:";
            // 
            // numericDockpathSegmentPosY
            // 
            this.numericDockpathSegmentPosY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericDockpathSegmentPosY.DecimalPlaces = 4;
            this.numericDockpathSegmentPosY.Enabled = false;
            this.numericDockpathSegmentPosY.Location = new System.Drawing.Point(29, 45);
            this.numericDockpathSegmentPosY.Maximum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            0});
            this.numericDockpathSegmentPosY.Minimum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            -2147483648});
            this.numericDockpathSegmentPosY.Name = "numericDockpathSegmentPosY";
            this.numericDockpathSegmentPosY.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericDockpathSegmentPosY.Size = new System.Drawing.Size(218, 20);
            this.numericDockpathSegmentPosY.TabIndex = 40;
            this.numericDockpathSegmentPosY.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.numericDockpathSegmentPosY.ValueChanged += new System.EventHandler(this.DockpathSegmentPositionChanged);
            // 
            // labelDockpathSegmentPosY
            // 
            this.labelDockpathSegmentPosY.AutoSize = true;
            this.labelDockpathSegmentPosY.Location = new System.Drawing.Point(6, 47);
            this.labelDockpathSegmentPosY.Name = "labelDockpathSegmentPosY";
            this.labelDockpathSegmentPosY.Size = new System.Drawing.Size(17, 13);
            this.labelDockpathSegmentPosY.TabIndex = 39;
            this.labelDockpathSegmentPosY.Text = "Y:";
            // 
            // numericDockpathSegmentPosX
            // 
            this.numericDockpathSegmentPosX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericDockpathSegmentPosX.DecimalPlaces = 4;
            this.numericDockpathSegmentPosX.Enabled = false;
            this.numericDockpathSegmentPosX.Location = new System.Drawing.Point(29, 19);
            this.numericDockpathSegmentPosX.Maximum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            0});
            this.numericDockpathSegmentPosX.Minimum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            -2147483648});
            this.numericDockpathSegmentPosX.Name = "numericDockpathSegmentPosX";
            this.numericDockpathSegmentPosX.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericDockpathSegmentPosX.Size = new System.Drawing.Size(218, 20);
            this.numericDockpathSegmentPosX.TabIndex = 38;
            this.numericDockpathSegmentPosX.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.numericDockpathSegmentPosX.ValueChanged += new System.EventHandler(this.DockpathSegmentPositionChanged);
            // 
            // labelDockpathSegmentPosX
            // 
            this.labelDockpathSegmentPosX.AutoSize = true;
            this.labelDockpathSegmentPosX.Location = new System.Drawing.Point(6, 21);
            this.labelDockpathSegmentPosX.Name = "labelDockpathSegmentPosX";
            this.labelDockpathSegmentPosX.Size = new System.Drawing.Size(17, 13);
            this.labelDockpathSegmentPosX.TabIndex = 37;
            this.labelDockpathSegmentPosX.Text = "X:";
            // 
            // buttonDockpathSegmentInsertBefore
            // 
            this.buttonDockpathSegmentInsertBefore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDockpathSegmentInsertBefore.Enabled = false;
            this.buttonDockpathSegmentInsertBefore.Location = new System.Drawing.Point(82, 67);
            this.buttonDockpathSegmentInsertBefore.Name = "buttonDockpathSegmentInsertBefore";
            this.buttonDockpathSegmentInsertBefore.Size = new System.Drawing.Size(101, 23);
            this.buttonDockpathSegmentInsertBefore.TabIndex = 34;
            this.buttonDockpathSegmentInsertBefore.Text = "Insert before";
            this.buttonDockpathSegmentInsertBefore.UseVisualStyleBackColor = true;
            this.buttonDockpathSegmentInsertBefore.Click += new System.EventHandler(this.buttonDockpathSegmentInsertBefore_Click);
            // 
            // numericDockpathSegmentSpeed
            // 
            this.numericDockpathSegmentSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericDockpathSegmentSpeed.DecimalPlaces = 2;
            this.numericDockpathSegmentSpeed.Enabled = false;
            this.numericDockpathSegmentSpeed.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericDockpathSegmentSpeed.Location = new System.Drawing.Point(68, 327);
            this.numericDockpathSegmentSpeed.Maximum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            0});
            this.numericDockpathSegmentSpeed.Name = "numericDockpathSegmentSpeed";
            this.numericDockpathSegmentSpeed.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericDockpathSegmentSpeed.Size = new System.Drawing.Size(188, 20);
            this.numericDockpathSegmentSpeed.TabIndex = 33;
            this.numericDockpathSegmentSpeed.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.numericDockpathSegmentSpeed.ValueChanged += new System.EventHandler(this.numericDockpathSegmentSpeed_ValueChanged);
            // 
            // numericDockpathSegmentTolerance
            // 
            this.numericDockpathSegmentTolerance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericDockpathSegmentTolerance.DecimalPlaces = 2;
            this.numericDockpathSegmentTolerance.Enabled = false;
            this.numericDockpathSegmentTolerance.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericDockpathSegmentTolerance.Location = new System.Drawing.Point(68, 300);
            this.numericDockpathSegmentTolerance.Maximum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            0});
            this.numericDockpathSegmentTolerance.Name = "numericDockpathSegmentTolerance";
            this.numericDockpathSegmentTolerance.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericDockpathSegmentTolerance.Size = new System.Drawing.Size(188, 20);
            this.numericDockpathSegmentTolerance.TabIndex = 32;
            this.numericDockpathSegmentTolerance.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.numericDockpathSegmentTolerance.ValueChanged += new System.EventHandler(this.numericDockpathSegmentTolerance_ValueChanged);
            // 
            // buttonDockpathSegmentRemove
            // 
            this.buttonDockpathSegmentRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDockpathSegmentRemove.Enabled = false;
            this.buttonDockpathSegmentRemove.Location = new System.Drawing.Point(183, 67);
            this.buttonDockpathSegmentRemove.Name = "buttonDockpathSegmentRemove";
            this.buttonDockpathSegmentRemove.Size = new System.Drawing.Size(74, 23);
            this.buttonDockpathSegmentRemove.TabIndex = 31;
            this.buttonDockpathSegmentRemove.Text = "Remove";
            this.buttonDockpathSegmentRemove.UseVisualStyleBackColor = true;
            this.buttonDockpathSegmentRemove.Click += new System.EventHandler(this.buttonDockpathSegmentRemove_Click);
            // 
            // buttonDockpathSegmentInsertAfter
            // 
            this.buttonDockpathSegmentInsertAfter.Enabled = false;
            this.buttonDockpathSegmentInsertAfter.Location = new System.Drawing.Point(3, 67);
            this.buttonDockpathSegmentInsertAfter.Name = "buttonDockpathSegmentInsertAfter";
            this.buttonDockpathSegmentInsertAfter.Size = new System.Drawing.Size(73, 23);
            this.buttonDockpathSegmentInsertAfter.TabIndex = 30;
            this.buttonDockpathSegmentInsertAfter.Text = "Insert after";
            this.buttonDockpathSegmentInsertAfter.UseVisualStyleBackColor = true;
            this.buttonDockpathSegmentInsertAfter.Click += new System.EventHandler(this.buttonDockpathSegmentInsertAfter_Click);
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
            this.groupDockpathSegmentFlags.Location = new System.Drawing.Point(4, 351);
            this.groupDockpathSegmentFlags.Name = "groupDockpathSegmentFlags";
            this.groupDockpathSegmentFlags.Size = new System.Drawing.Size(253, 111);
            this.groupDockpathSegmentFlags.TabIndex = 11;
            this.groupDockpathSegmentFlags.TabStop = false;
            this.groupDockpathSegmentFlags.Text = "Flags";
            // 
            // checkDockpathSegmentFlagClip
            // 
            this.checkDockpathSegmentFlagClip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkDockpathSegmentFlagClip.AutoSize = true;
            this.checkDockpathSegmentFlagClip.Enabled = false;
            this.checkDockpathSegmentFlagClip.Location = new System.Drawing.Point(141, 88);
            this.checkDockpathSegmentFlagClip.Name = "checkDockpathSegmentFlagClip";
            this.checkDockpathSegmentFlagClip.Size = new System.Drawing.Size(72, 17);
            this.checkDockpathSegmentFlagClip.TabIndex = 27;
            this.checkDockpathSegmentFlagClip.Text = "Clip plane";
            this.checkDockpathSegmentFlagClip.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.checkDockpathSegmentFlagClip.UseVisualStyleBackColor = true;
            this.checkDockpathSegmentFlagClip.CheckedChanged += new System.EventHandler(this.DockpathSegmentFlagsChanged);
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
            this.checkDockpathSegmentFlagUnfocus.CheckedChanged += new System.EventHandler(this.DockpathSegmentFlagsChanged);
            // 
            // checkDockpathSegmentFlagCheck
            // 
            this.checkDockpathSegmentFlagCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkDockpathSegmentFlagCheck.AutoSize = true;
            this.checkDockpathSegmentFlagCheck.Enabled = false;
            this.checkDockpathSegmentFlagCheck.Location = new System.Drawing.Point(141, 65);
            this.checkDockpathSegmentFlagCheck.Name = "checkDockpathSegmentFlagCheck";
            this.checkDockpathSegmentFlagCheck.Size = new System.Drawing.Size(95, 17);
            this.checkDockpathSegmentFlagCheck.TabIndex = 25;
            this.checkDockpathSegmentFlagCheck.Text = "Check rotation";
            this.checkDockpathSegmentFlagCheck.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.checkDockpathSegmentFlagCheck.UseVisualStyleBackColor = true;
            this.checkDockpathSegmentFlagCheck.CheckedChanged += new System.EventHandler(this.DockpathSegmentFlagsChanged);
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
            this.checkDockpathSegmentFlagClearRes.CheckedChanged += new System.EventHandler(this.DockpathSegmentFlagsChanged);
            // 
            // checkDockpathSegmentFlagClose
            // 
            this.checkDockpathSegmentFlagClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkDockpathSegmentFlagClose.AutoSize = true;
            this.checkDockpathSegmentFlagClose.Enabled = false;
            this.checkDockpathSegmentFlagClose.Location = new System.Drawing.Point(141, 42);
            this.checkDockpathSegmentFlagClose.Name = "checkDockpathSegmentFlagClose";
            this.checkDockpathSegmentFlagClose.Size = new System.Drawing.Size(102, 17);
            this.checkDockpathSegmentFlagClose.TabIndex = 23;
            this.checkDockpathSegmentFlagClose.Text = "Close behaviour";
            this.checkDockpathSegmentFlagClose.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.checkDockpathSegmentFlagClose.UseVisualStyleBackColor = true;
            this.checkDockpathSegmentFlagClose.CheckedChanged += new System.EventHandler(this.DockpathSegmentFlagsChanged);
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
            this.checkDockpathSegmentFlagUseRot.CheckedChanged += new System.EventHandler(this.DockpathSegmentFlagsChanged);
            // 
            // checkDockpathSegmentFlagPlayer
            // 
            this.checkDockpathSegmentFlagPlayer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkDockpathSegmentFlagPlayer.AutoSize = true;
            this.checkDockpathSegmentFlagPlayer.Enabled = false;
            this.checkDockpathSegmentFlagPlayer.Location = new System.Drawing.Point(141, 19);
            this.checkDockpathSegmentFlagPlayer.Name = "checkDockpathSegmentFlagPlayer";
            this.checkDockpathSegmentFlagPlayer.Size = new System.Drawing.Size(101, 17);
            this.checkDockpathSegmentFlagPlayer.TabIndex = 22;
            this.checkDockpathSegmentFlagPlayer.Text = "Player in control";
            this.checkDockpathSegmentFlagPlayer.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.checkDockpathSegmentFlagPlayer.UseVisualStyleBackColor = true;
            this.checkDockpathSegmentFlagPlayer.CheckedChanged += new System.EventHandler(this.DockpathSegmentFlagsChanged);
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
            this.checkDockpathSegmentFlagQueue.CheckedChanged += new System.EventHandler(this.DockpathSegmentFlagsChanged);
            // 
            // labelDockpathSegmentSpeed
            // 
            this.labelDockpathSegmentSpeed.AutoSize = true;
            this.labelDockpathSegmentSpeed.Location = new System.Drawing.Point(5, 329);
            this.labelDockpathSegmentSpeed.Name = "labelDockpathSegmentSpeed";
            this.labelDockpathSegmentSpeed.Size = new System.Drawing.Size(41, 13);
            this.labelDockpathSegmentSpeed.TabIndex = 10;
            this.labelDockpathSegmentSpeed.Text = "Speed:";
            // 
            // labelDockpathSegmentTolerance
            // 
            this.labelDockpathSegmentTolerance.AutoSize = true;
            this.labelDockpathSegmentTolerance.Location = new System.Drawing.Point(5, 303);
            this.labelDockpathSegmentTolerance.Name = "labelDockpathSegmentTolerance";
            this.labelDockpathSegmentTolerance.Size = new System.Drawing.Size(61, 13);
            this.labelDockpathSegmentTolerance.TabIndex = 9;
            this.labelDockpathSegmentTolerance.Text = "Tolerance: ";
            // 
            // trackBarDockpathSegments
            // 
            this.trackBarDockpathSegments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarDockpathSegments.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.trackBarDockpathSegments.Enabled = false;
            this.trackBarDockpathSegments.Location = new System.Drawing.Point(8, 16);
            this.trackBarDockpathSegments.Name = "trackBarDockpathSegments";
            this.trackBarDockpathSegments.Size = new System.Drawing.Size(246, 45);
            this.trackBarDockpathSegments.TabIndex = 3;
            this.trackBarDockpathSegments.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBarDockpathSegments.Scroll += new System.EventHandler(this.trackBarDockpathSegments_Scroll);
            // 
            // groupDockpathLinks
            // 
            this.groupDockpathLinks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupDockpathLinks.Controls.Add(this.comboDockpathLinkPath);
            this.groupDockpathLinks.Controls.Add(this.buttonDockpathLinkRemove);
            this.groupDockpathLinks.Controls.Add(this.buttonDockpathLinkAdd);
            this.groupDockpathLinks.Controls.Add(this.labelDockpathLinkPath);
            this.groupDockpathLinks.Controls.Add(this.listDockpathLinks);
            this.groupDockpathLinks.Location = new System.Drawing.Point(6, 484);
            this.groupDockpathLinks.Name = "groupDockpathLinks";
            this.groupDockpathLinks.Size = new System.Drawing.Size(260, 163);
            this.groupDockpathLinks.TabIndex = 2;
            this.groupDockpathLinks.TabStop = false;
            this.groupDockpathLinks.Text = "Links";
            // 
            // comboDockpathLinkPath
            // 
            this.comboDockpathLinkPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboDockpathLinkPath.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboDockpathLinkPath.Enabled = false;
            this.comboDockpathLinkPath.FormattingEnabled = true;
            this.comboDockpathLinkPath.Location = new System.Drawing.Point(47, 133);
            this.comboDockpathLinkPath.Name = "comboDockpathLinkPath";
            this.comboDockpathLinkPath.Size = new System.Drawing.Size(210, 21);
            this.comboDockpathLinkPath.Sorted = true;
            this.comboDockpathLinkPath.TabIndex = 34;
            this.comboDockpathLinkPath.SelectedIndexChanged += new System.EventHandler(this.comboDockpathLinkPath_SelectedIndexChanged);
            // 
            // buttonDockpathLinkRemove
            // 
            this.buttonDockpathLinkRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDockpathLinkRemove.Enabled = false;
            this.buttonDockpathLinkRemove.Location = new System.Drawing.Point(165, 104);
            this.buttonDockpathLinkRemove.Name = "buttonDockpathLinkRemove";
            this.buttonDockpathLinkRemove.Size = new System.Drawing.Size(92, 23);
            this.buttonDockpathLinkRemove.TabIndex = 33;
            this.buttonDockpathLinkRemove.Text = "Remove";
            this.buttonDockpathLinkRemove.UseVisualStyleBackColor = true;
            this.buttonDockpathLinkRemove.Click += new System.EventHandler(this.buttonDockpathLinkRemove_Click);
            // 
            // buttonDockpathLinkAdd
            // 
            this.buttonDockpathLinkAdd.Enabled = false;
            this.buttonDockpathLinkAdd.Location = new System.Drawing.Point(3, 104);
            this.buttonDockpathLinkAdd.Name = "buttonDockpathLinkAdd";
            this.buttonDockpathLinkAdd.Size = new System.Drawing.Size(92, 23);
            this.buttonDockpathLinkAdd.TabIndex = 32;
            this.buttonDockpathLinkAdd.Text = "Add";
            this.buttonDockpathLinkAdd.UseVisualStyleBackColor = true;
            this.buttonDockpathLinkAdd.Click += new System.EventHandler(this.buttonDockpathLinkAdd_Click);
            // 
            // labelDockpathLinkPath
            // 
            this.labelDockpathLinkPath.AutoSize = true;
            this.labelDockpathLinkPath.Location = new System.Drawing.Point(3, 136);
            this.labelDockpathLinkPath.Name = "labelDockpathLinkPath";
            this.labelDockpathLinkPath.Size = new System.Drawing.Size(35, 13);
            this.labelDockpathLinkPath.TabIndex = 30;
            this.labelDockpathLinkPath.Text = "Path: ";
            // 
            // listDockpathLinks
            // 
            this.listDockpathLinks.Dock = System.Windows.Forms.DockStyle.Top;
            this.listDockpathLinks.Enabled = false;
            this.listDockpathLinks.FormattingEnabled = true;
            this.listDockpathLinks.Location = new System.Drawing.Point(3, 16);
            this.listDockpathLinks.Name = "listDockpathLinks";
            this.listDockpathLinks.Size = new System.Drawing.Size(254, 82);
            this.listDockpathLinks.TabIndex = 11;
            this.listDockpathLinks.SelectedIndexChanged += new System.EventHandler(this.listDockpathLinks_SelectedIndexChanged);
            // 
            // groupDockpathFamilies
            // 
            this.groupDockpathFamilies.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupDockpathFamilies.Controls.Add(this.buttonDockpathFamilyRemove);
            this.groupDockpathFamilies.Controls.Add(this.buttonDockpathFamilyAdd);
            this.groupDockpathFamilies.Controls.Add(this.boxDockpathFamilyName);
            this.groupDockpathFamilies.Controls.Add(this.labelDockpathFamilyName);
            this.groupDockpathFamilies.Controls.Add(this.listDockpathFamilies);
            this.groupDockpathFamilies.Location = new System.Drawing.Point(6, 317);
            this.groupDockpathFamilies.Name = "groupDockpathFamilies";
            this.groupDockpathFamilies.Size = new System.Drawing.Size(260, 161);
            this.groupDockpathFamilies.TabIndex = 1;
            this.groupDockpathFamilies.TabStop = false;
            this.groupDockpathFamilies.Text = "Families";
            // 
            // buttonDockpathFamilyRemove
            // 
            this.buttonDockpathFamilyRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDockpathFamilyRemove.Enabled = false;
            this.buttonDockpathFamilyRemove.Location = new System.Drawing.Point(165, 104);
            this.buttonDockpathFamilyRemove.Name = "buttonDockpathFamilyRemove";
            this.buttonDockpathFamilyRemove.Size = new System.Drawing.Size(92, 23);
            this.buttonDockpathFamilyRemove.TabIndex = 29;
            this.buttonDockpathFamilyRemove.Text = "Remove";
            this.buttonDockpathFamilyRemove.UseVisualStyleBackColor = true;
            this.buttonDockpathFamilyRemove.Click += new System.EventHandler(this.buttonDockpathFamilyRemove_Click);
            // 
            // buttonDockpathFamilyAdd
            // 
            this.buttonDockpathFamilyAdd.Enabled = false;
            this.buttonDockpathFamilyAdd.Location = new System.Drawing.Point(3, 104);
            this.buttonDockpathFamilyAdd.Name = "buttonDockpathFamilyAdd";
            this.buttonDockpathFamilyAdd.Size = new System.Drawing.Size(92, 23);
            this.buttonDockpathFamilyAdd.TabIndex = 28;
            this.buttonDockpathFamilyAdd.Text = "Add";
            this.buttonDockpathFamilyAdd.UseVisualStyleBackColor = true;
            this.buttonDockpathFamilyAdd.Click += new System.EventHandler(this.buttonDockpathFamilyAdd_Click);
            // 
            // boxDockpathFamilyName
            // 
            this.boxDockpathFamilyName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boxDockpathFamilyName.Enabled = false;
            this.boxDockpathFamilyName.Location = new System.Drawing.Point(47, 133);
            this.boxDockpathFamilyName.Name = "boxDockpathFamilyName";
            this.boxDockpathFamilyName.Size = new System.Drawing.Size(210, 20);
            this.boxDockpathFamilyName.TabIndex = 24;
            this.boxDockpathFamilyName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.boxDockpathFamilyName_KeyPress);
            this.boxDockpathFamilyName.Leave += new System.EventHandler(this.boxDockpathFamilyName_Leave);
            // 
            // labelDockpathFamilyName
            // 
            this.labelDockpathFamilyName.AutoSize = true;
            this.labelDockpathFamilyName.Location = new System.Drawing.Point(3, 136);
            this.labelDockpathFamilyName.Name = "labelDockpathFamilyName";
            this.labelDockpathFamilyName.Size = new System.Drawing.Size(41, 13);
            this.labelDockpathFamilyName.TabIndex = 23;
            this.labelDockpathFamilyName.Text = "Name: ";
            // 
            // listDockpathFamilies
            // 
            this.listDockpathFamilies.Dock = System.Windows.Forms.DockStyle.Top;
            this.listDockpathFamilies.Enabled = false;
            this.listDockpathFamilies.FormattingEnabled = true;
            this.listDockpathFamilies.Location = new System.Drawing.Point(3, 16);
            this.listDockpathFamilies.Name = "listDockpathFamilies";
            this.listDockpathFamilies.Size = new System.Drawing.Size(254, 82);
            this.listDockpathFamilies.TabIndex = 11;
            this.listDockpathFamilies.SelectedIndexChanged += new System.EventHandler(this.listDockpathFamilies_SelectedIndexChanged);
            // 
            // tabNavLights
            // 
            this.tabNavLights.AutoScroll = true;
            this.tabNavLights.AutoScrollMinSize = new System.Drawing.Size(241, 711);
            this.tabNavLights.Controls.Add(this.groupNavLightPosition);
            this.tabNavLights.Controls.Add(this.labelNavLightParent);
            this.tabNavLights.Controls.Add(this.comboNavLightParent);
            this.tabNavLights.Controls.Add(this.boxNavLightName);
            this.tabNavLights.Controls.Add(this.labelNavLightName);
            this.tabNavLights.Controls.Add(this.listNavLights);
            this.tabNavLights.Controls.Add(this.buttonNavLightRemove);
            this.tabNavLights.Controls.Add(this.buttonNavLightAdd);
            this.tabNavLights.Controls.Add(this.groupNavLightPreview);
            this.tabNavLights.Controls.Add(this.groupNavLightParameters);
            this.tabNavLights.Controls.Add(this.groupNavLightFlags);
            this.tabNavLights.Location = new System.Drawing.Point(4, 58);
            this.tabNavLights.Name = "tabNavLights";
            this.tabNavLights.Size = new System.Drawing.Size(272, 758);
            this.tabNavLights.TabIndex = 7;
            this.tabNavLights.Text = "NavLights";
            this.tabNavLights.UseVisualStyleBackColor = true;
            // 
            // groupNavLightPosition
            // 
            this.groupNavLightPosition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupNavLightPosition.Controls.Add(this.numericNavLightPositionZ);
            this.groupNavLightPosition.Controls.Add(this.labelNavLightPositionZ);
            this.groupNavLightPosition.Controls.Add(this.numericNavLightPositionY);
            this.groupNavLightPosition.Controls.Add(this.labelNavLightPositionY);
            this.groupNavLightPosition.Controls.Add(this.numericNavLightPositionX);
            this.groupNavLightPosition.Controls.Add(this.labelNavLightPositionX);
            this.groupNavLightPosition.Location = new System.Drawing.Point(6, 323);
            this.groupNavLightPosition.Name = "groupNavLightPosition";
            this.groupNavLightPosition.Size = new System.Drawing.Size(260, 93);
            this.groupNavLightPosition.TabIndex = 25;
            this.groupNavLightPosition.TabStop = false;
            this.groupNavLightPosition.Text = "Position";
            // 
            // numericNavLightPositionZ
            // 
            this.numericNavLightPositionZ.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericNavLightPositionZ.DecimalPlaces = 4;
            this.numericNavLightPositionZ.Enabled = false;
            this.numericNavLightPositionZ.Location = new System.Drawing.Point(72, 66);
            this.numericNavLightPositionZ.Maximum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            0});
            this.numericNavLightPositionZ.Minimum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            -2147483648});
            this.numericNavLightPositionZ.Name = "numericNavLightPositionZ";
            this.numericNavLightPositionZ.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericNavLightPositionZ.Size = new System.Drawing.Size(182, 20);
            this.numericNavLightPositionZ.TabIndex = 27;
            this.numericNavLightPositionZ.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.numericNavLightPositionZ.ValueChanged += new System.EventHandler(this.numericNavLightPosition_ValueChanged);
            // 
            // labelNavLightPositionZ
            // 
            this.labelNavLightPositionZ.AutoSize = true;
            this.labelNavLightPositionZ.Location = new System.Drawing.Point(6, 68);
            this.labelNavLightPositionZ.Name = "labelNavLightPositionZ";
            this.labelNavLightPositionZ.Size = new System.Drawing.Size(17, 13);
            this.labelNavLightPositionZ.TabIndex = 26;
            this.labelNavLightPositionZ.Text = "Z:";
            // 
            // numericNavLightPositionY
            // 
            this.numericNavLightPositionY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericNavLightPositionY.DecimalPlaces = 4;
            this.numericNavLightPositionY.Enabled = false;
            this.numericNavLightPositionY.Location = new System.Drawing.Point(72, 40);
            this.numericNavLightPositionY.Maximum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            0});
            this.numericNavLightPositionY.Minimum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            -2147483648});
            this.numericNavLightPositionY.Name = "numericNavLightPositionY";
            this.numericNavLightPositionY.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericNavLightPositionY.Size = new System.Drawing.Size(182, 20);
            this.numericNavLightPositionY.TabIndex = 25;
            this.numericNavLightPositionY.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.numericNavLightPositionY.ValueChanged += new System.EventHandler(this.numericNavLightPosition_ValueChanged);
            // 
            // labelNavLightPositionY
            // 
            this.labelNavLightPositionY.AutoSize = true;
            this.labelNavLightPositionY.Location = new System.Drawing.Point(6, 42);
            this.labelNavLightPositionY.Name = "labelNavLightPositionY";
            this.labelNavLightPositionY.Size = new System.Drawing.Size(17, 13);
            this.labelNavLightPositionY.TabIndex = 24;
            this.labelNavLightPositionY.Text = "Y:";
            // 
            // numericNavLightPositionX
            // 
            this.numericNavLightPositionX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericNavLightPositionX.DecimalPlaces = 4;
            this.numericNavLightPositionX.Enabled = false;
            this.numericNavLightPositionX.Location = new System.Drawing.Point(72, 14);
            this.numericNavLightPositionX.Maximum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            0});
            this.numericNavLightPositionX.Minimum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            -2147483648});
            this.numericNavLightPositionX.Name = "numericNavLightPositionX";
            this.numericNavLightPositionX.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericNavLightPositionX.Size = new System.Drawing.Size(182, 20);
            this.numericNavLightPositionX.TabIndex = 23;
            this.numericNavLightPositionX.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.numericNavLightPositionX.ValueChanged += new System.EventHandler(this.numericNavLightPosition_ValueChanged);
            // 
            // labelNavLightPositionX
            // 
            this.labelNavLightPositionX.AutoSize = true;
            this.labelNavLightPositionX.Location = new System.Drawing.Point(6, 16);
            this.labelNavLightPositionX.Name = "labelNavLightPositionX";
            this.labelNavLightPositionX.Size = new System.Drawing.Size(17, 13);
            this.labelNavLightPositionX.TabIndex = 22;
            this.labelNavLightPositionX.Text = "X:";
            // 
            // labelNavLightParent
            // 
            this.labelNavLightParent.AutoSize = true;
            this.labelNavLightParent.Location = new System.Drawing.Point(8, 299);
            this.labelNavLightParent.Name = "labelNavLightParent";
            this.labelNavLightParent.Size = new System.Drawing.Size(44, 13);
            this.labelNavLightParent.TabIndex = 24;
            this.labelNavLightParent.Text = "Parent: ";
            // 
            // comboNavLightParent
            // 
            this.comboNavLightParent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboNavLightParent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboNavLightParent.Enabled = false;
            this.comboNavLightParent.FormattingEnabled = true;
            this.comboNavLightParent.Location = new System.Drawing.Point(56, 296);
            this.comboNavLightParent.Name = "comboNavLightParent";
            this.comboNavLightParent.Size = new System.Drawing.Size(210, 21);
            this.comboNavLightParent.Sorted = true;
            this.comboNavLightParent.TabIndex = 23;
            this.comboNavLightParent.SelectedIndexChanged += new System.EventHandler(this.comboNavLightParent_SelectedIndexChanged);
            // 
            // boxNavLightName
            // 
            this.boxNavLightName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boxNavLightName.Enabled = false;
            this.boxNavLightName.Location = new System.Drawing.Point(56, 270);
            this.boxNavLightName.Name = "boxNavLightName";
            this.boxNavLightName.Size = new System.Drawing.Size(210, 20);
            this.boxNavLightName.TabIndex = 22;
            this.boxNavLightName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.boxNavLightName_KeyPress);
            this.boxNavLightName.Leave += new System.EventHandler(this.boxNavLightName_Leave);
            // 
            // labelNavLightName
            // 
            this.labelNavLightName.AutoSize = true;
            this.labelNavLightName.Location = new System.Drawing.Point(8, 273);
            this.labelNavLightName.Name = "labelNavLightName";
            this.labelNavLightName.Size = new System.Drawing.Size(41, 13);
            this.labelNavLightName.TabIndex = 21;
            this.labelNavLightName.Text = "Name: ";
            // 
            // listNavLights
            // 
            this.listNavLights.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listNavLights.ContextMenuStrip = this.contextShowHideAll;
            this.listNavLights.FormattingEnabled = true;
            this.listNavLights.Location = new System.Drawing.Point(6, 6);
            this.listNavLights.Name = "listNavLights";
            this.listNavLights.Size = new System.Drawing.Size(260, 229);
            this.listNavLights.TabIndex = 20;
            this.listNavLights.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listNavLights_ItemCheck);
            this.listNavLights.SelectedIndexChanged += new System.EventHandler(this.listNavLights_SelectedIndexChanged);
            // 
            // buttonNavLightRemove
            // 
            this.buttonNavLightRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNavLightRemove.Enabled = false;
            this.buttonNavLightRemove.Location = new System.Drawing.Point(174, 241);
            this.buttonNavLightRemove.Name = "buttonNavLightRemove";
            this.buttonNavLightRemove.Size = new System.Drawing.Size(92, 23);
            this.buttonNavLightRemove.TabIndex = 19;
            this.buttonNavLightRemove.Text = "Remove";
            this.buttonNavLightRemove.UseVisualStyleBackColor = true;
            this.buttonNavLightRemove.Click += new System.EventHandler(this.buttonNavLightRemove_Click);
            // 
            // buttonNavLightAdd
            // 
            this.buttonNavLightAdd.Location = new System.Drawing.Point(6, 241);
            this.buttonNavLightAdd.Name = "buttonNavLightAdd";
            this.buttonNavLightAdd.Size = new System.Drawing.Size(92, 23);
            this.buttonNavLightAdd.TabIndex = 18;
            this.buttonNavLightAdd.Text = "Add";
            this.buttonNavLightAdd.UseVisualStyleBackColor = true;
            this.buttonNavLightAdd.Click += new System.EventHandler(this.buttonNavLightAdd_Click);
            // 
            // groupNavLightPreview
            // 
            this.groupNavLightPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupNavLightPreview.Controls.Add(this.checkNavLightDrawRadius);
            this.groupNavLightPreview.Location = new System.Drawing.Point(6, 662);
            this.groupNavLightPreview.Name = "groupNavLightPreview";
            this.groupNavLightPreview.Size = new System.Drawing.Size(260, 43);
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
            this.groupNavLightParameters.Location = new System.Drawing.Point(6, 422);
            this.groupNavLightParameters.Name = "groupNavLightParameters";
            this.groupNavLightParameters.Size = new System.Drawing.Size(260, 185);
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
            this.comboNavLightType.Size = new System.Drawing.Size(182, 21);
            this.comboNavLightType.TabIndex = 24;
            this.comboNavLightType.SelectedIndexChanged += new System.EventHandler(this.comboNavLightType_SelectedIndexChanged);
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
            this.numericNavLightDistance.Size = new System.Drawing.Size(182, 20);
            this.numericNavLightDistance.TabIndex = 22;
            this.numericNavLightDistance.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.numericNavLightDistance.ValueChanged += new System.EventHandler(this.numericNavLightDistance_ValueChanged);
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
            this.buttonNavLightColor.Size = new System.Drawing.Size(182, 20);
            this.buttonNavLightColor.TabIndex = 20;
            this.buttonNavLightColor.UseVisualStyleBackColor = false;
            this.buttonNavLightColor.Click += new System.EventHandler(this.buttonNavLightColor_Click);
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
            this.numericNavLightFrequency.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericNavLightFrequency.Location = new System.Drawing.Point(72, 97);
            this.numericNavLightFrequency.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericNavLightFrequency.Name = "numericNavLightFrequency";
            this.numericNavLightFrequency.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericNavLightFrequency.Size = new System.Drawing.Size(182, 20);
            this.numericNavLightFrequency.TabIndex = 18;
            this.numericNavLightFrequency.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.numericNavLightFrequency.ValueChanged += new System.EventHandler(this.numericNavLightFrequency_ValueChanged);
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
            this.numericNavLightPhase.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericNavLightPhase.Location = new System.Drawing.Point(72, 71);
            this.numericNavLightPhase.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericNavLightPhase.Name = "numericNavLightPhase";
            this.numericNavLightPhase.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericNavLightPhase.Size = new System.Drawing.Size(182, 20);
            this.numericNavLightPhase.TabIndex = 16;
            this.numericNavLightPhase.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.numericNavLightPhase.ValueChanged += new System.EventHandler(this.numericNavLightPhase_ValueChanged);
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
            this.numericNavLightSize.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericNavLightSize.Location = new System.Drawing.Point(72, 45);
            this.numericNavLightSize.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericNavLightSize.Name = "numericNavLightSize";
            this.numericNavLightSize.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericNavLightSize.Size = new System.Drawing.Size(182, 20);
            this.numericNavLightSize.TabIndex = 14;
            this.numericNavLightSize.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.numericNavLightSize.ValueChanged += new System.EventHandler(this.numericNavLightSize_ValueChanged);
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
            this.groupNavLightFlags.Location = new System.Drawing.Point(6, 613);
            this.groupNavLightFlags.Name = "groupNavLightFlags";
            this.groupNavLightFlags.Size = new System.Drawing.Size(260, 43);
            this.groupNavLightFlags.TabIndex = 12;
            this.groupNavLightFlags.TabStop = false;
            this.groupNavLightFlags.Text = "Flags";
            // 
            // checkNavLightFlagHighEnd
            // 
            this.checkNavLightFlagHighEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkNavLightFlagHighEnd.AutoSize = true;
            this.checkNavLightFlagHighEnd.Enabled = false;
            this.checkNavLightFlagHighEnd.Location = new System.Drawing.Point(184, 19);
            this.checkNavLightFlagHighEnd.Name = "checkNavLightFlagHighEnd";
            this.checkNavLightFlagHighEnd.Size = new System.Drawing.Size(69, 17);
            this.checkNavLightFlagHighEnd.TabIndex = 10;
            this.checkNavLightFlagHighEnd.Text = "High-end";
            this.checkNavLightFlagHighEnd.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.checkNavLightFlagHighEnd.UseVisualStyleBackColor = true;
            this.checkNavLightFlagHighEnd.CheckedChanged += new System.EventHandler(this.checkNavLightFlagHighEnd_CheckedChanged);
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
            this.checkNavLightFlagSprite.CheckedChanged += new System.EventHandler(this.checkNavLightFlagSprite_CheckedChanged);
            // 
            // tabEngineGlows
            // 
            this.tabEngineGlows.AutoScroll = true;
            this.tabEngineGlows.AutoScrollMinSize = new System.Drawing.Size(241, 488);
            this.tabEngineGlows.Controls.Add(this.buttonEngineGlowRemove);
            this.tabEngineGlows.Controls.Add(this.buttonEngineGlowAdd);
            this.tabEngineGlows.Controls.Add(this.boxEngineGlowName);
            this.tabEngineGlows.Controls.Add(this.labelEngineGlowName);
            this.tabEngineGlows.Controls.Add(this.listEngineGlows);
            this.tabEngineGlows.Controls.Add(this.labelEngineGlowParent);
            this.tabEngineGlows.Controls.Add(this.comboEngineGlowParent);
            this.tabEngineGlows.Controls.Add(this.groupEngineGlowLODs);
            this.tabEngineGlows.Location = new System.Drawing.Point(4, 58);
            this.tabEngineGlows.Name = "tabEngineGlows";
            this.tabEngineGlows.Padding = new System.Windows.Forms.Padding(3);
            this.tabEngineGlows.Size = new System.Drawing.Size(272, 758);
            this.tabEngineGlows.TabIndex = 8;
            this.tabEngineGlows.Text = "Engine Glows";
            this.tabEngineGlows.UseVisualStyleBackColor = true;
            // 
            // buttonEngineGlowRemove
            // 
            this.buttonEngineGlowRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEngineGlowRemove.Enabled = false;
            this.buttonEngineGlowRemove.Location = new System.Drawing.Point(174, 211);
            this.buttonEngineGlowRemove.Name = "buttonEngineGlowRemove";
            this.buttonEngineGlowRemove.Size = new System.Drawing.Size(92, 23);
            this.buttonEngineGlowRemove.TabIndex = 26;
            this.buttonEngineGlowRemove.Text = "Remove";
            this.buttonEngineGlowRemove.UseVisualStyleBackColor = true;
            this.buttonEngineGlowRemove.Click += new System.EventHandler(this.buttonEngineGlowRemove_Click);
            // 
            // buttonEngineGlowAdd
            // 
            this.buttonEngineGlowAdd.Location = new System.Drawing.Point(6, 211);
            this.buttonEngineGlowAdd.Name = "buttonEngineGlowAdd";
            this.buttonEngineGlowAdd.Size = new System.Drawing.Size(92, 23);
            this.buttonEngineGlowAdd.TabIndex = 25;
            this.buttonEngineGlowAdd.Text = "Add";
            this.buttonEngineGlowAdd.UseVisualStyleBackColor = true;
            this.buttonEngineGlowAdd.Click += new System.EventHandler(this.buttonEngineGlowAdd_Click);
            // 
            // boxEngineGlowName
            // 
            this.boxEngineGlowName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boxEngineGlowName.Enabled = false;
            this.boxEngineGlowName.Location = new System.Drawing.Point(56, 240);
            this.boxEngineGlowName.Name = "boxEngineGlowName";
            this.boxEngineGlowName.Size = new System.Drawing.Size(210, 20);
            this.boxEngineGlowName.TabIndex = 28;
            this.boxEngineGlowName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.boxEngineGlowName_KeyPress);
            this.boxEngineGlowName.Leave += new System.EventHandler(this.boxEngineGlowName_Leave);
            // 
            // labelEngineGlowName
            // 
            this.labelEngineGlowName.AutoSize = true;
            this.labelEngineGlowName.Location = new System.Drawing.Point(6, 243);
            this.labelEngineGlowName.Name = "labelEngineGlowName";
            this.labelEngineGlowName.Size = new System.Drawing.Size(41, 13);
            this.labelEngineGlowName.TabIndex = 27;
            this.labelEngineGlowName.Text = "Name: ";
            // 
            // listEngineGlows
            // 
            this.listEngineGlows.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listEngineGlows.FormattingEnabled = true;
            this.listEngineGlows.Location = new System.Drawing.Point(6, 6);
            this.listEngineGlows.Name = "listEngineGlows";
            this.listEngineGlows.Size = new System.Drawing.Size(260, 199);
            this.listEngineGlows.TabIndex = 24;
            this.listEngineGlows.SelectedIndexChanged += new System.EventHandler(this.listEngineGlows_SelectedIndexChanged);
            // 
            // labelEngineGlowParent
            // 
            this.labelEngineGlowParent.AutoSize = true;
            this.labelEngineGlowParent.Location = new System.Drawing.Point(6, 269);
            this.labelEngineGlowParent.Name = "labelEngineGlowParent";
            this.labelEngineGlowParent.Size = new System.Drawing.Size(44, 13);
            this.labelEngineGlowParent.TabIndex = 23;
            this.labelEngineGlowParent.Text = "Parent: ";
            // 
            // comboEngineGlowParent
            // 
            this.comboEngineGlowParent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboEngineGlowParent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboEngineGlowParent.Enabled = false;
            this.comboEngineGlowParent.FormattingEnabled = true;
            this.comboEngineGlowParent.Location = new System.Drawing.Point(56, 266);
            this.comboEngineGlowParent.Name = "comboEngineGlowParent";
            this.comboEngineGlowParent.Size = new System.Drawing.Size(210, 21);
            this.comboEngineGlowParent.Sorted = true;
            this.comboEngineGlowParent.TabIndex = 22;
            this.comboEngineGlowParent.SelectedIndexChanged += new System.EventHandler(this.comboEngineGlowParent_SelectedIndexChanged);
            // 
            // groupEngineGlowLODs
            // 
            this.groupEngineGlowLODs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupEngineGlowLODs.Controls.Add(this.buttonEngineGlowLODImportDAE);
            this.groupEngineGlowLODs.Controls.Add(this.buttonEngineGlowLODRemove);
            this.groupEngineGlowLODs.Controls.Add(this.buttonEngineGlowLODExportDAE);
            this.groupEngineGlowLODs.Controls.Add(this.buttonEngineGlowLODAdd);
            this.groupEngineGlowLODs.Controls.Add(this.buttonEngineGlowLODImportOBJ);
            this.groupEngineGlowLODs.Controls.Add(this.buttonEngineGlowLODExportOBJ);
            this.groupEngineGlowLODs.Controls.Add(this.listEngineGlowLODs);
            this.groupEngineGlowLODs.Location = new System.Drawing.Point(3, 293);
            this.groupEngineGlowLODs.Name = "groupEngineGlowLODs";
            this.groupEngineGlowLODs.Size = new System.Drawing.Size(263, 189);
            this.groupEngineGlowLODs.TabIndex = 21;
            this.groupEngineGlowLODs.TabStop = false;
            this.groupEngineGlowLODs.Text = "Level of detail(s)";
            // 
            // buttonEngineGlowLODImportDAE
            // 
            this.buttonEngineGlowLODImportDAE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEngineGlowLODImportDAE.Enabled = false;
            this.buttonEngineGlowLODImportDAE.Location = new System.Drawing.Point(168, 130);
            this.buttonEngineGlowLODImportDAE.Name = "buttonEngineGlowLODImportDAE";
            this.buttonEngineGlowLODImportDAE.Size = new System.Drawing.Size(92, 23);
            this.buttonEngineGlowLODImportDAE.TabIndex = 7;
            this.buttonEngineGlowLODImportDAE.Text = "Import from DAE";
            this.buttonEngineGlowLODImportDAE.UseVisualStyleBackColor = true;
            this.buttonEngineGlowLODImportDAE.Click += new System.EventHandler(this.buttonEngineGlowLODImportDAE_Click);
            // 
            // buttonEngineGlowLODRemove
            // 
            this.buttonEngineGlowLODRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEngineGlowLODRemove.Enabled = false;
            this.buttonEngineGlowLODRemove.Location = new System.Drawing.Point(168, 101);
            this.buttonEngineGlowLODRemove.Name = "buttonEngineGlowLODRemove";
            this.buttonEngineGlowLODRemove.Size = new System.Drawing.Size(92, 23);
            this.buttonEngineGlowLODRemove.TabIndex = 5;
            this.buttonEngineGlowLODRemove.Text = "Remove";
            this.buttonEngineGlowLODRemove.UseVisualStyleBackColor = true;
            this.buttonEngineGlowLODRemove.Click += new System.EventHandler(this.buttonEngineGlowLODRemove_Click);
            // 
            // buttonEngineGlowLODExportDAE
            // 
            this.buttonEngineGlowLODExportDAE.Enabled = false;
            this.buttonEngineGlowLODExportDAE.Location = new System.Drawing.Point(3, 130);
            this.buttonEngineGlowLODExportDAE.Name = "buttonEngineGlowLODExportDAE";
            this.buttonEngineGlowLODExportDAE.Size = new System.Drawing.Size(92, 23);
            this.buttonEngineGlowLODExportDAE.TabIndex = 6;
            this.buttonEngineGlowLODExportDAE.Text = "Export to DAE";
            this.buttonEngineGlowLODExportDAE.UseVisualStyleBackColor = true;
            this.buttonEngineGlowLODExportDAE.Click += new System.EventHandler(this.buttonEngineGlowLODExportDAE_Click);
            // 
            // buttonEngineGlowLODAdd
            // 
            this.buttonEngineGlowLODAdd.Location = new System.Drawing.Point(3, 101);
            this.buttonEngineGlowLODAdd.Name = "buttonEngineGlowLODAdd";
            this.buttonEngineGlowLODAdd.Size = new System.Drawing.Size(92, 23);
            this.buttonEngineGlowLODAdd.TabIndex = 4;
            this.buttonEngineGlowLODAdd.Text = "Add";
            this.buttonEngineGlowLODAdd.UseVisualStyleBackColor = true;
            this.buttonEngineGlowLODAdd.Click += new System.EventHandler(this.buttonEngineGlowLODAdd_Click);
            // 
            // buttonEngineGlowLODImportOBJ
            // 
            this.buttonEngineGlowLODImportOBJ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEngineGlowLODImportOBJ.Enabled = false;
            this.buttonEngineGlowLODImportOBJ.Location = new System.Drawing.Point(168, 159);
            this.buttonEngineGlowLODImportOBJ.Name = "buttonEngineGlowLODImportOBJ";
            this.buttonEngineGlowLODImportOBJ.Size = new System.Drawing.Size(92, 23);
            this.buttonEngineGlowLODImportOBJ.TabIndex = 2;
            this.buttonEngineGlowLODImportOBJ.Text = "Import from OBJ";
            this.buttonEngineGlowLODImportOBJ.UseVisualStyleBackColor = true;
            this.buttonEngineGlowLODImportOBJ.Click += new System.EventHandler(this.buttonEngineGlowLODImportOBJ_Click);
            // 
            // buttonEngineGlowLODExportOBJ
            // 
            this.buttonEngineGlowLODExportOBJ.Enabled = false;
            this.buttonEngineGlowLODExportOBJ.Location = new System.Drawing.Point(3, 159);
            this.buttonEngineGlowLODExportOBJ.Name = "buttonEngineGlowLODExportOBJ";
            this.buttonEngineGlowLODExportOBJ.Size = new System.Drawing.Size(92, 23);
            this.buttonEngineGlowLODExportOBJ.TabIndex = 1;
            this.buttonEngineGlowLODExportOBJ.Text = "Export to OBJ";
            this.buttonEngineGlowLODExportOBJ.UseVisualStyleBackColor = true;
            this.buttonEngineGlowLODExportOBJ.Click += new System.EventHandler(this.buttonEngineGlowLODExportOBJ_Click);
            // 
            // listEngineGlowLODs
            // 
            this.listEngineGlowLODs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listEngineGlowLODs.FormattingEnabled = true;
            this.listEngineGlowLODs.Location = new System.Drawing.Point(3, 16);
            this.listEngineGlowLODs.Name = "listEngineGlowLODs";
            this.listEngineGlowLODs.Size = new System.Drawing.Size(257, 64);
            this.listEngineGlowLODs.TabIndex = 0;
            this.listEngineGlowLODs.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listEngineGlowLODs_ItemCheck);
            this.listEngineGlowLODs.SelectedIndexChanged += new System.EventHandler(this.listEngineGlowLODs_SelectedIndexChanged);
            // 
            // tabEngineShapes
            // 
            this.tabEngineShapes.AutoScroll = true;
            this.tabEngineShapes.AutoScrollMinSize = new System.Drawing.Size(241, 336);
            this.tabEngineShapes.Controls.Add(this.boxEngineShapeName);
            this.tabEngineShapes.Controls.Add(this.labelEngineShapeName);
            this.tabEngineShapes.Controls.Add(this.buttonEngineShapeImportDAE);
            this.tabEngineShapes.Controls.Add(this.buttonEngineShapeExportDAE);
            this.tabEngineShapes.Controls.Add(this.buttonEngineShapeImportOBJ);
            this.tabEngineShapes.Controls.Add(this.buttonEngineShapeExportOBJ);
            this.tabEngineShapes.Controls.Add(this.buttonEngineShapeRemove);
            this.tabEngineShapes.Controls.Add(this.buttonEngineShapeAdd);
            this.tabEngineShapes.Controls.Add(this.listEngineShapes);
            this.tabEngineShapes.Controls.Add(this.labelEngineShapeParent);
            this.tabEngineShapes.Controls.Add(this.comboEngineShapeParent);
            this.tabEngineShapes.Location = new System.Drawing.Point(4, 58);
            this.tabEngineShapes.Name = "tabEngineShapes";
            this.tabEngineShapes.Padding = new System.Windows.Forms.Padding(3);
            this.tabEngineShapes.Size = new System.Drawing.Size(272, 758);
            this.tabEngineShapes.TabIndex = 9;
            this.tabEngineShapes.Text = "Engine Shapes";
            this.tabEngineShapes.UseVisualStyleBackColor = true;
            // 
            // boxEngineShapeName
            // 
            this.boxEngineShapeName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boxEngineShapeName.Enabled = false;
            this.boxEngineShapeName.Location = new System.Drawing.Point(56, 225);
            this.boxEngineShapeName.Name = "boxEngineShapeName";
            this.boxEngineShapeName.Size = new System.Drawing.Size(210, 20);
            this.boxEngineShapeName.TabIndex = 38;
            this.boxEngineShapeName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.boxEngineShapeName_KeyPress);
            this.boxEngineShapeName.Leave += new System.EventHandler(this.boxEngineShapeName_Leave);
            // 
            // labelEngineShapeName
            // 
            this.labelEngineShapeName.AutoSize = true;
            this.labelEngineShapeName.Location = new System.Drawing.Point(8, 228);
            this.labelEngineShapeName.Name = "labelEngineShapeName";
            this.labelEngineShapeName.Size = new System.Drawing.Size(41, 13);
            this.labelEngineShapeName.TabIndex = 37;
            this.labelEngineShapeName.Text = "Name: ";
            // 
            // buttonEngineShapeImportDAE
            // 
            this.buttonEngineShapeImportDAE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEngineShapeImportDAE.Enabled = false;
            this.buttonEngineShapeImportDAE.Location = new System.Drawing.Point(175, 278);
            this.buttonEngineShapeImportDAE.Name = "buttonEngineShapeImportDAE";
            this.buttonEngineShapeImportDAE.Size = new System.Drawing.Size(92, 23);
            this.buttonEngineShapeImportDAE.TabIndex = 36;
            this.buttonEngineShapeImportDAE.Text = "Import from DAE";
            this.buttonEngineShapeImportDAE.UseVisualStyleBackColor = true;
            this.buttonEngineShapeImportDAE.Click += new System.EventHandler(this.buttonEngineShapeImportDAE_Click);
            // 
            // buttonEngineShapeExportDAE
            // 
            this.buttonEngineShapeExportDAE.Enabled = false;
            this.buttonEngineShapeExportDAE.Location = new System.Drawing.Point(6, 278);
            this.buttonEngineShapeExportDAE.Name = "buttonEngineShapeExportDAE";
            this.buttonEngineShapeExportDAE.Size = new System.Drawing.Size(92, 23);
            this.buttonEngineShapeExportDAE.TabIndex = 35;
            this.buttonEngineShapeExportDAE.Text = "Export to DAE";
            this.buttonEngineShapeExportDAE.UseVisualStyleBackColor = true;
            this.buttonEngineShapeExportDAE.Click += new System.EventHandler(this.buttonEngineShapeExportDAE_Click);
            // 
            // buttonEngineShapeImportOBJ
            // 
            this.buttonEngineShapeImportOBJ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEngineShapeImportOBJ.Enabled = false;
            this.buttonEngineShapeImportOBJ.Location = new System.Drawing.Point(175, 307);
            this.buttonEngineShapeImportOBJ.Name = "buttonEngineShapeImportOBJ";
            this.buttonEngineShapeImportOBJ.Size = new System.Drawing.Size(92, 23);
            this.buttonEngineShapeImportOBJ.TabIndex = 34;
            this.buttonEngineShapeImportOBJ.Text = "Import from OBJ";
            this.buttonEngineShapeImportOBJ.UseVisualStyleBackColor = true;
            this.buttonEngineShapeImportOBJ.Click += new System.EventHandler(this.buttonEngineShapeImportOBJ_Click);
            // 
            // buttonEngineShapeExportOBJ
            // 
            this.buttonEngineShapeExportOBJ.Enabled = false;
            this.buttonEngineShapeExportOBJ.Location = new System.Drawing.Point(6, 307);
            this.buttonEngineShapeExportOBJ.Name = "buttonEngineShapeExportOBJ";
            this.buttonEngineShapeExportOBJ.Size = new System.Drawing.Size(92, 23);
            this.buttonEngineShapeExportOBJ.TabIndex = 33;
            this.buttonEngineShapeExportOBJ.Text = "Export to OBJ";
            this.buttonEngineShapeExportOBJ.UseVisualStyleBackColor = true;
            this.buttonEngineShapeExportOBJ.Click += new System.EventHandler(this.buttonEngineShapeExportOBJ_Click);
            // 
            // buttonEngineShapeRemove
            // 
            this.buttonEngineShapeRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEngineShapeRemove.Enabled = false;
            this.buttonEngineShapeRemove.Location = new System.Drawing.Point(175, 196);
            this.buttonEngineShapeRemove.Name = "buttonEngineShapeRemove";
            this.buttonEngineShapeRemove.Size = new System.Drawing.Size(92, 23);
            this.buttonEngineShapeRemove.TabIndex = 32;
            this.buttonEngineShapeRemove.Text = "Remove";
            this.buttonEngineShapeRemove.UseVisualStyleBackColor = true;
            this.buttonEngineShapeRemove.Click += new System.EventHandler(this.buttonEngineShapeRemove_Click);
            // 
            // buttonEngineShapeAdd
            // 
            this.buttonEngineShapeAdd.Location = new System.Drawing.Point(6, 196);
            this.buttonEngineShapeAdd.Name = "buttonEngineShapeAdd";
            this.buttonEngineShapeAdd.Size = new System.Drawing.Size(92, 23);
            this.buttonEngineShapeAdd.TabIndex = 31;
            this.buttonEngineShapeAdd.Text = "Add";
            this.buttonEngineShapeAdd.UseVisualStyleBackColor = true;
            this.buttonEngineShapeAdd.Click += new System.EventHandler(this.buttonEngineShapeAdd_Click);
            // 
            // listEngineShapes
            // 
            this.listEngineShapes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listEngineShapes.ContextMenuStrip = this.contextShowHideAll;
            this.listEngineShapes.FormattingEnabled = true;
            this.listEngineShapes.Location = new System.Drawing.Point(6, 6);
            this.listEngineShapes.Name = "listEngineShapes";
            this.listEngineShapes.Size = new System.Drawing.Size(260, 184);
            this.listEngineShapes.TabIndex = 15;
            this.listEngineShapes.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listEngineShapes_ItemCheck);
            this.listEngineShapes.SelectedIndexChanged += new System.EventHandler(this.listEngineShapes_SelectedIndexChanged);
            // 
            // labelEngineShapeParent
            // 
            this.labelEngineShapeParent.AutoSize = true;
            this.labelEngineShapeParent.Location = new System.Drawing.Point(8, 254);
            this.labelEngineShapeParent.Name = "labelEngineShapeParent";
            this.labelEngineShapeParent.Size = new System.Drawing.Size(44, 13);
            this.labelEngineShapeParent.TabIndex = 14;
            this.labelEngineShapeParent.Text = "Parent: ";
            // 
            // comboEngineShapeParent
            // 
            this.comboEngineShapeParent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboEngineShapeParent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboEngineShapeParent.Enabled = false;
            this.comboEngineShapeParent.FormattingEnabled = true;
            this.comboEngineShapeParent.Location = new System.Drawing.Point(56, 251);
            this.comboEngineShapeParent.Name = "comboEngineShapeParent";
            this.comboEngineShapeParent.Size = new System.Drawing.Size(210, 21);
            this.comboEngineShapeParent.Sorted = true;
            this.comboEngineShapeParent.TabIndex = 13;
            this.comboEngineShapeParent.SelectedIndexChanged += new System.EventHandler(this.comboEngineShapeParent_SelectedIndexChanged);
            // 
            // tabEngineBurns
            // 
            this.tabEngineBurns.AutoScroll = true;
            this.tabEngineBurns.AutoScrollMinSize = new System.Drawing.Size(241, 322);
            this.tabEngineBurns.Controls.Add(this.buttonEngineBurnRemove);
            this.tabEngineBurns.Controls.Add(this.buttonEngineBurnAdd);
            this.tabEngineBurns.Controls.Add(this.listEngineBurns);
            this.tabEngineBurns.Controls.Add(this.comboEngineBurnParent);
            this.tabEngineBurns.Controls.Add(this.labelEngineBurnParent);
            this.tabEngineBurns.Controls.Add(this.boxEngineBurnName);
            this.tabEngineBurns.Controls.Add(this.labelEngineBurnName);
            this.tabEngineBurns.Controls.Add(this.groupEngineBurnFlames);
            this.tabEngineBurns.Location = new System.Drawing.Point(4, 58);
            this.tabEngineBurns.Name = "tabEngineBurns";
            this.tabEngineBurns.Size = new System.Drawing.Size(272, 758);
            this.tabEngineBurns.TabIndex = 10;
            this.tabEngineBurns.Text = "Engine Burns";
            this.tabEngineBurns.UseVisualStyleBackColor = true;
            // 
            // buttonEngineBurnRemove
            // 
            this.buttonEngineBurnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEngineBurnRemove.Enabled = false;
            this.buttonEngineBurnRemove.Location = new System.Drawing.Point(174, 241);
            this.buttonEngineBurnRemove.Name = "buttonEngineBurnRemove";
            this.buttonEngineBurnRemove.Size = new System.Drawing.Size(92, 23);
            this.buttonEngineBurnRemove.TabIndex = 34;
            this.buttonEngineBurnRemove.Text = "Remove";
            this.buttonEngineBurnRemove.UseVisualStyleBackColor = true;
            this.buttonEngineBurnRemove.Click += new System.EventHandler(this.buttonEngineBurnRemove_Click);
            // 
            // buttonEngineBurnAdd
            // 
            this.buttonEngineBurnAdd.Location = new System.Drawing.Point(6, 241);
            this.buttonEngineBurnAdd.Name = "buttonEngineBurnAdd";
            this.buttonEngineBurnAdd.Size = new System.Drawing.Size(92, 23);
            this.buttonEngineBurnAdd.TabIndex = 33;
            this.buttonEngineBurnAdd.Text = "Add";
            this.buttonEngineBurnAdd.UseVisualStyleBackColor = true;
            this.buttonEngineBurnAdd.Click += new System.EventHandler(this.buttonEngineBurnAdd_Click);
            // 
            // listEngineBurns
            // 
            this.listEngineBurns.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listEngineBurns.ContextMenuStrip = this.contextShowHideAll;
            this.listEngineBurns.FormattingEnabled = true;
            this.listEngineBurns.Location = new System.Drawing.Point(6, 6);
            this.listEngineBurns.Name = "listEngineBurns";
            this.listEngineBurns.Size = new System.Drawing.Size(260, 229);
            this.listEngineBurns.TabIndex = 16;
            this.listEngineBurns.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listEngineBurns_ItemCheck);
            this.listEngineBurns.SelectedIndexChanged += new System.EventHandler(this.listEngineBurns_SelectedIndexChanged);
            // 
            // comboEngineBurnParent
            // 
            this.comboEngineBurnParent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboEngineBurnParent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboEngineBurnParent.Enabled = false;
            this.comboEngineBurnParent.FormattingEnabled = true;
            this.comboEngineBurnParent.Location = new System.Drawing.Point(48, 296);
            this.comboEngineBurnParent.Name = "comboEngineBurnParent";
            this.comboEngineBurnParent.Size = new System.Drawing.Size(218, 21);
            this.comboEngineBurnParent.Sorted = true;
            this.comboEngineBurnParent.TabIndex = 15;
            this.comboEngineBurnParent.SelectedIndexChanged += new System.EventHandler(this.comboEngineBurnParent_SelectedIndexChanged);
            // 
            // labelEngineBurnParent
            // 
            this.labelEngineBurnParent.AutoSize = true;
            this.labelEngineBurnParent.Location = new System.Drawing.Point(8, 299);
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
            this.boxEngineBurnName.Location = new System.Drawing.Point(48, 270);
            this.boxEngineBurnName.Name = "boxEngineBurnName";
            this.boxEngineBurnName.Size = new System.Drawing.Size(218, 20);
            this.boxEngineBurnName.TabIndex = 5;
            this.boxEngineBurnName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.boxEngineBurnName_KeyPress);
            this.boxEngineBurnName.Leave += new System.EventHandler(this.boxEngineBurnName_Leave);
            // 
            // labelEngineBurnName
            // 
            this.labelEngineBurnName.AutoSize = true;
            this.labelEngineBurnName.Location = new System.Drawing.Point(8, 273);
            this.labelEngineBurnName.Name = "labelEngineBurnName";
            this.labelEngineBurnName.Size = new System.Drawing.Size(38, 13);
            this.labelEngineBurnName.TabIndex = 4;
            this.labelEngineBurnName.Text = "Name:";
            // 
            // groupEngineBurnFlames
            // 
            this.groupEngineBurnFlames.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupEngineBurnFlames.Controls.Add(this.groupEngineBurnFlamePosition);
            this.groupEngineBurnFlames.Controls.Add(this.buttonEngineBurnFlameRemove);
            this.groupEngineBurnFlames.Controls.Add(this.buttonEngineBurnFlameAdd);
            this.groupEngineBurnFlames.Controls.Add(this.numericEngineBurnSpriteIndex);
            this.groupEngineBurnFlames.Controls.Add(this.labelEngineBurnFlameSpriteIndex);
            this.groupEngineBurnFlames.Controls.Add(this.trackBarEngineBurnFlames);
            this.groupEngineBurnFlames.Location = new System.Drawing.Point(6, 323);
            this.groupEngineBurnFlames.Name = "groupEngineBurnFlames";
            this.groupEngineBurnFlames.Size = new System.Drawing.Size(260, 223);
            this.groupEngineBurnFlames.TabIndex = 3;
            this.groupEngineBurnFlames.TabStop = false;
            this.groupEngineBurnFlames.Text = "Flames";
            // 
            // groupEngineBurnFlamePosition
            // 
            this.groupEngineBurnFlamePosition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupEngineBurnFlamePosition.Controls.Add(this.numericEngineBurnFlamePosZ);
            this.groupEngineBurnFlamePosition.Controls.Add(this.labelEngineBurnFlamePosZ);
            this.groupEngineBurnFlamePosition.Controls.Add(this.numericEngineBurnFlamePosY);
            this.groupEngineBurnFlamePosition.Controls.Add(this.labelEngineBurnFlamePosY);
            this.groupEngineBurnFlamePosition.Controls.Add(this.numericEngineBurnFlamePosX);
            this.groupEngineBurnFlamePosition.Controls.Add(this.labelEngineBurnFlamePosX);
            this.groupEngineBurnFlamePosition.Location = new System.Drawing.Point(6, 96);
            this.groupEngineBurnFlamePosition.Name = "groupEngineBurnFlamePosition";
            this.groupEngineBurnFlamePosition.Size = new System.Drawing.Size(248, 93);
            this.groupEngineBurnFlamePosition.TabIndex = 37;
            this.groupEngineBurnFlamePosition.TabStop = false;
            this.groupEngineBurnFlamePosition.Text = "Position";
            // 
            // numericEngineBurnFlamePosZ
            // 
            this.numericEngineBurnFlamePosZ.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericEngineBurnFlamePosZ.DecimalPlaces = 4;
            this.numericEngineBurnFlamePosZ.Enabled = false;
            this.numericEngineBurnFlamePosZ.Location = new System.Drawing.Point(72, 66);
            this.numericEngineBurnFlamePosZ.Maximum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            0});
            this.numericEngineBurnFlamePosZ.Minimum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            -2147483648});
            this.numericEngineBurnFlamePosZ.Name = "numericEngineBurnFlamePosZ";
            this.numericEngineBurnFlamePosZ.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericEngineBurnFlamePosZ.Size = new System.Drawing.Size(170, 20);
            this.numericEngineBurnFlamePosZ.TabIndex = 27;
            this.numericEngineBurnFlamePosZ.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.numericEngineBurnFlamePosZ.ValueChanged += new System.EventHandler(this.EngineBurnFlamePositionChanged);
            // 
            // labelEngineBurnFlamePosZ
            // 
            this.labelEngineBurnFlamePosZ.AutoSize = true;
            this.labelEngineBurnFlamePosZ.Location = new System.Drawing.Point(6, 68);
            this.labelEngineBurnFlamePosZ.Name = "labelEngineBurnFlamePosZ";
            this.labelEngineBurnFlamePosZ.Size = new System.Drawing.Size(17, 13);
            this.labelEngineBurnFlamePosZ.TabIndex = 26;
            this.labelEngineBurnFlamePosZ.Text = "Z:";
            // 
            // numericEngineBurnFlamePosY
            // 
            this.numericEngineBurnFlamePosY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericEngineBurnFlamePosY.DecimalPlaces = 4;
            this.numericEngineBurnFlamePosY.Enabled = false;
            this.numericEngineBurnFlamePosY.Location = new System.Drawing.Point(72, 40);
            this.numericEngineBurnFlamePosY.Maximum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            0});
            this.numericEngineBurnFlamePosY.Minimum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            -2147483648});
            this.numericEngineBurnFlamePosY.Name = "numericEngineBurnFlamePosY";
            this.numericEngineBurnFlamePosY.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericEngineBurnFlamePosY.Size = new System.Drawing.Size(170, 20);
            this.numericEngineBurnFlamePosY.TabIndex = 25;
            this.numericEngineBurnFlamePosY.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.numericEngineBurnFlamePosY.ValueChanged += new System.EventHandler(this.EngineBurnFlamePositionChanged);
            // 
            // labelEngineBurnFlamePosY
            // 
            this.labelEngineBurnFlamePosY.AutoSize = true;
            this.labelEngineBurnFlamePosY.Location = new System.Drawing.Point(6, 42);
            this.labelEngineBurnFlamePosY.Name = "labelEngineBurnFlamePosY";
            this.labelEngineBurnFlamePosY.Size = new System.Drawing.Size(17, 13);
            this.labelEngineBurnFlamePosY.TabIndex = 24;
            this.labelEngineBurnFlamePosY.Text = "Y:";
            // 
            // numericEngineBurnFlamePosX
            // 
            this.numericEngineBurnFlamePosX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericEngineBurnFlamePosX.DecimalPlaces = 4;
            this.numericEngineBurnFlamePosX.Enabled = false;
            this.numericEngineBurnFlamePosX.Location = new System.Drawing.Point(72, 14);
            this.numericEngineBurnFlamePosX.Maximum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            0});
            this.numericEngineBurnFlamePosX.Minimum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            -2147483648});
            this.numericEngineBurnFlamePosX.Name = "numericEngineBurnFlamePosX";
            this.numericEngineBurnFlamePosX.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericEngineBurnFlamePosX.Size = new System.Drawing.Size(170, 20);
            this.numericEngineBurnFlamePosX.TabIndex = 23;
            this.numericEngineBurnFlamePosX.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.numericEngineBurnFlamePosX.ValueChanged += new System.EventHandler(this.EngineBurnFlamePositionChanged);
            // 
            // labelEngineBurnFlamePosX
            // 
            this.labelEngineBurnFlamePosX.AutoSize = true;
            this.labelEngineBurnFlamePosX.Location = new System.Drawing.Point(6, 16);
            this.labelEngineBurnFlamePosX.Name = "labelEngineBurnFlamePosX";
            this.labelEngineBurnFlamePosX.Size = new System.Drawing.Size(17, 13);
            this.labelEngineBurnFlamePosX.TabIndex = 22;
            this.labelEngineBurnFlamePosX.Text = "X:";
            // 
            // buttonEngineBurnFlameRemove
            // 
            this.buttonEngineBurnFlameRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEngineBurnFlameRemove.Enabled = false;
            this.buttonEngineBurnFlameRemove.Location = new System.Drawing.Point(162, 67);
            this.buttonEngineBurnFlameRemove.Name = "buttonEngineBurnFlameRemove";
            this.buttonEngineBurnFlameRemove.Size = new System.Drawing.Size(92, 23);
            this.buttonEngineBurnFlameRemove.TabIndex = 36;
            this.buttonEngineBurnFlameRemove.Text = "Remove";
            this.buttonEngineBurnFlameRemove.UseVisualStyleBackColor = true;
            this.buttonEngineBurnFlameRemove.Click += new System.EventHandler(this.buttonEngineBurnFlameRemove_Click);
            // 
            // buttonEngineBurnFlameAdd
            // 
            this.buttonEngineBurnFlameAdd.Enabled = false;
            this.buttonEngineBurnFlameAdd.Location = new System.Drawing.Point(6, 67);
            this.buttonEngineBurnFlameAdd.Name = "buttonEngineBurnFlameAdd";
            this.buttonEngineBurnFlameAdd.Size = new System.Drawing.Size(92, 23);
            this.buttonEngineBurnFlameAdd.TabIndex = 35;
            this.buttonEngineBurnFlameAdd.Text = "Add";
            this.buttonEngineBurnFlameAdd.UseVisualStyleBackColor = true;
            this.buttonEngineBurnFlameAdd.Click += new System.EventHandler(this.buttonEngineBurnFlameAdd_Click);
            // 
            // numericEngineBurnSpriteIndex
            // 
            this.numericEngineBurnSpriteIndex.Enabled = false;
            this.numericEngineBurnSpriteIndex.Location = new System.Drawing.Point(78, 195);
            this.numericEngineBurnSpriteIndex.Name = "numericEngineBurnSpriteIndex";
            this.numericEngineBurnSpriteIndex.Size = new System.Drawing.Size(41, 20);
            this.numericEngineBurnSpriteIndex.TabIndex = 10;
            this.numericEngineBurnSpriteIndex.ValueChanged += new System.EventHandler(this.numericEngineBurnSpriteIndex_ValueChanged);
            // 
            // labelEngineBurnFlameSpriteIndex
            // 
            this.labelEngineBurnFlameSpriteIndex.AutoSize = true;
            this.labelEngineBurnFlameSpriteIndex.Location = new System.Drawing.Point(7, 197);
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
            this.trackBarEngineBurnFlames.Size = new System.Drawing.Size(250, 45);
            this.trackBarEngineBurnFlames.TabIndex = 3;
            this.trackBarEngineBurnFlames.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBarEngineBurnFlames.Scroll += new System.EventHandler(this.trackBarEngineBurnFlames_Scroll);
            // 
            // tabAnimations
            // 
            this.tabAnimations.AutoScroll = true;
            this.tabAnimations.AutoScrollMinSize = new System.Drawing.Size(241, 526);
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
            this.tabAnimations.Location = new System.Drawing.Point(4, 58);
            this.tabAnimations.Name = "tabAnimations";
            this.tabAnimations.Size = new System.Drawing.Size(272, 758);
            this.tabAnimations.TabIndex = 11;
            this.tabAnimations.Text = "Animations";
            this.tabAnimations.UseVisualStyleBackColor = true;
            // 
            // labelAnimationLoopEndTime
            // 
            this.labelAnimationLoopEndTime.AutoSize = true;
            this.labelAnimationLoopEndTime.Location = new System.Drawing.Point(8, 278);
            this.labelAnimationLoopEndTime.Name = "labelAnimationLoopEndTime";
            this.labelAnimationLoopEndTime.Size = new System.Drawing.Size(55, 13);
            this.labelAnimationLoopEndTime.TabIndex = 45;
            this.labelAnimationLoopEndTime.Text = "Loop end:";
            // 
            // labelAnimationLoopStartTime
            // 
            this.labelAnimationLoopStartTime.AutoSize = true;
            this.labelAnimationLoopStartTime.Location = new System.Drawing.Point(8, 252);
            this.labelAnimationLoopStartTime.Name = "labelAnimationLoopStartTime";
            this.labelAnimationLoopStartTime.Size = new System.Drawing.Size(57, 13);
            this.labelAnimationLoopStartTime.TabIndex = 44;
            this.labelAnimationLoopStartTime.Text = "Loop start:";
            // 
            // numericAnimationLoopEndTime
            // 
            this.numericAnimationLoopEndTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericAnimationLoopEndTime.DecimalPlaces = 3;
            this.numericAnimationLoopEndTime.Enabled = false;
            this.numericAnimationLoopEndTime.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericAnimationLoopEndTime.Location = new System.Drawing.Point(66, 276);
            this.numericAnimationLoopEndTime.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
            this.numericAnimationLoopEndTime.Name = "numericAnimationLoopEndTime";
            this.numericAnimationLoopEndTime.Size = new System.Drawing.Size(200, 20);
            this.numericAnimationLoopEndTime.TabIndex = 43;
            this.numericAnimationLoopEndTime.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            // 
            // numericAnimationLoopStartTime
            // 
            this.numericAnimationLoopStartTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericAnimationLoopStartTime.DecimalPlaces = 3;
            this.numericAnimationLoopStartTime.Enabled = false;
            this.numericAnimationLoopStartTime.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericAnimationLoopStartTime.Location = new System.Drawing.Point(66, 250);
            this.numericAnimationLoopStartTime.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
            this.numericAnimationLoopStartTime.Name = "numericAnimationLoopStartTime";
            this.numericAnimationLoopStartTime.Size = new System.Drawing.Size(200, 20);
            this.numericAnimationLoopStartTime.TabIndex = 42;
            this.numericAnimationLoopStartTime.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            // 
            // labelAnimationEndTime
            // 
            this.labelAnimationEndTime.AutoSize = true;
            this.labelAnimationEndTime.Location = new System.Drawing.Point(8, 226);
            this.labelAnimationEndTime.Name = "labelAnimationEndTime";
            this.labelAnimationEndTime.Size = new System.Drawing.Size(29, 13);
            this.labelAnimationEndTime.TabIndex = 41;
            this.labelAnimationEndTime.Text = "End:";
            // 
            // numericAnimationEndTime
            // 
            this.numericAnimationEndTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericAnimationEndTime.DecimalPlaces = 3;
            this.numericAnimationEndTime.Enabled = false;
            this.numericAnimationEndTime.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericAnimationEndTime.Location = new System.Drawing.Point(66, 224);
            this.numericAnimationEndTime.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
            this.numericAnimationEndTime.Name = "numericAnimationEndTime";
            this.numericAnimationEndTime.Size = new System.Drawing.Size(200, 20);
            this.numericAnimationEndTime.TabIndex = 40;
            this.numericAnimationEndTime.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            // 
            // numericAnimationStartTime
            // 
            this.numericAnimationStartTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericAnimationStartTime.DecimalPlaces = 3;
            this.numericAnimationStartTime.Enabled = false;
            this.numericAnimationStartTime.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericAnimationStartTime.Location = new System.Drawing.Point(66, 198);
            this.numericAnimationStartTime.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
            this.numericAnimationStartTime.Name = "numericAnimationStartTime";
            this.numericAnimationStartTime.Size = new System.Drawing.Size(200, 20);
            this.numericAnimationStartTime.TabIndex = 39;
            this.numericAnimationStartTime.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            // 
            // labelAnimationStartTime
            // 
            this.labelAnimationStartTime.AutoSize = true;
            this.labelAnimationStartTime.Location = new System.Drawing.Point(8, 200);
            this.labelAnimationStartTime.Name = "labelAnimationStartTime";
            this.labelAnimationStartTime.Size = new System.Drawing.Size(32, 13);
            this.labelAnimationStartTime.TabIndex = 38;
            this.labelAnimationStartTime.Text = "Start:";
            // 
            // buttonAnimationPlay
            // 
            this.buttonAnimationPlay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAnimationPlay.Enabled = false;
            this.buttonAnimationPlay.Location = new System.Drawing.Point(6, 302);
            this.buttonAnimationPlay.Name = "buttonAnimationPlay";
            this.buttonAnimationPlay.Size = new System.Drawing.Size(260, 23);
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
            this.groupAnimationJoints.Location = new System.Drawing.Point(6, 331);
            this.groupAnimationJoints.Name = "groupAnimationJoints";
            this.groupAnimationJoints.Size = new System.Drawing.Size(260, 189);
            this.groupAnimationJoints.TabIndex = 35;
            this.groupAnimationJoints.TabStop = false;
            this.groupAnimationJoints.Text = "Animated joints";
            // 
            // buttonAnimationJointRemove
            // 
            this.buttonAnimationJointRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAnimationJointRemove.Enabled = false;
            this.buttonAnimationJointRemove.Location = new System.Drawing.Point(163, 159);
            this.buttonAnimationJointRemove.Name = "buttonAnimationJointRemove";
            this.buttonAnimationJointRemove.Size = new System.Drawing.Size(92, 23);
            this.buttonAnimationJointRemove.TabIndex = 38;
            this.buttonAnimationJointRemove.Text = "Remove";
            this.buttonAnimationJointRemove.UseVisualStyleBackColor = true;
            // 
            // buttonAnimationJointAdd
            // 
            this.buttonAnimationJointAdd.Enabled = false;
            this.buttonAnimationJointAdd.Location = new System.Drawing.Point(6, 159);
            this.buttonAnimationJointAdd.Name = "buttonAnimationJointAdd";
            this.buttonAnimationJointAdd.Size = new System.Drawing.Size(92, 23);
            this.buttonAnimationJointAdd.TabIndex = 37;
            this.buttonAnimationJointAdd.Text = "Add";
            this.buttonAnimationJointAdd.UseVisualStyleBackColor = true;
            // 
            // listAnimationJoints
            // 
            this.listAnimationJoints.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listAnimationJoints.FormattingEnabled = true;
            this.listAnimationJoints.Location = new System.Drawing.Point(6, 19);
            this.listAnimationJoints.Name = "listAnimationJoints";
            this.listAnimationJoints.Size = new System.Drawing.Size(248, 134);
            this.listAnimationJoints.TabIndex = 4;
            // 
            // boxAnimationName
            // 
            this.boxAnimationName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boxAnimationName.Enabled = false;
            this.boxAnimationName.Location = new System.Drawing.Point(66, 172);
            this.boxAnimationName.Name = "boxAnimationName";
            this.boxAnimationName.Size = new System.Drawing.Size(200, 20);
            this.boxAnimationName.TabIndex = 34;
            // 
            // labelAnimationName
            // 
            this.labelAnimationName.AutoSize = true;
            this.labelAnimationName.Location = new System.Drawing.Point(8, 175);
            this.labelAnimationName.Name = "labelAnimationName";
            this.labelAnimationName.Size = new System.Drawing.Size(41, 13);
            this.labelAnimationName.TabIndex = 33;
            this.labelAnimationName.Text = "Name: ";
            // 
            // buttonAnimationRemove
            // 
            this.buttonAnimationRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAnimationRemove.Enabled = false;
            this.buttonAnimationRemove.Location = new System.Drawing.Point(169, 143);
            this.buttonAnimationRemove.Name = "buttonAnimationRemove";
            this.buttonAnimationRemove.Size = new System.Drawing.Size(97, 23);
            this.buttonAnimationRemove.TabIndex = 32;
            this.buttonAnimationRemove.Text = "Remove";
            this.buttonAnimationRemove.UseVisualStyleBackColor = true;
            // 
            // buttonAnimationAdd
            // 
            this.buttonAnimationAdd.Enabled = false;
            this.buttonAnimationAdd.Location = new System.Drawing.Point(6, 143);
            this.buttonAnimationAdd.Name = "buttonAnimationAdd";
            this.buttonAnimationAdd.Size = new System.Drawing.Size(92, 23);
            this.buttonAnimationAdd.TabIndex = 31;
            this.buttonAnimationAdd.Text = "Add";
            this.buttonAnimationAdd.UseVisualStyleBackColor = true;
            // 
            // listAnimations
            // 
            this.listAnimations.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listAnimations.FormattingEnabled = true;
            this.listAnimations.Location = new System.Drawing.Point(6, 6);
            this.listAnimations.Name = "listAnimations";
            this.listAnimations.Size = new System.Drawing.Size(260, 134);
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
            this.splitContainer2.Panel2.Controls.Add(this.tabControlRight);
            this.splitContainer2.Size = new System.Drawing.Size(1000, 820);
            this.splitContainer2.SplitterDistance = 803;
            this.splitContainer2.TabIndex = 0;
            // 
            // tabControlRight
            // 
            this.tabControlRight.Controls.Add(this.tabProblems);
            this.tabControlRight.Controls.Add(this.tabTargetBoxes);
            this.tabControlRight.Controls.Add(this.tabShipTypeMapping);
            this.tabControlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlRight.Location = new System.Drawing.Point(0, 0);
            this.tabControlRight.Multiline = true;
            this.tabControlRight.Name = "tabControlRight";
            this.tabControlRight.SelectedIndex = 0;
            this.tabControlRight.Size = new System.Drawing.Size(193, 820);
            this.tabControlRight.TabIndex = 1;
            // 
            // tabProblems
            // 
            this.tabProblems.BackColor = System.Drawing.Color.Transparent;
            this.tabProblems.Controls.Add(this.gridProblems);
            this.tabProblems.ForeColor = System.Drawing.Color.Transparent;
            this.tabProblems.Location = new System.Drawing.Point(4, 40);
            this.tabProblems.Name = "tabProblems";
            this.tabProblems.Padding = new System.Windows.Forms.Padding(3);
            this.tabProblems.Size = new System.Drawing.Size(185, 776);
            this.tabProblems.TabIndex = 1;
            this.tabProblems.Text = "Problems";
            // 
            // gridProblems
            // 
            this.gridProblems.AllowUserToAddRows = false;
            this.gridProblems.AllowUserToDeleteRows = false;
            this.gridProblems.AllowUserToResizeColumns = false;
            this.gridProblems.AllowUserToResizeRows = false;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridProblems.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.gridProblems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridProblems.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridProblems.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gridProblems.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.gridProblems.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.gridProblems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gridProblems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnProblems});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridProblems.DefaultCellStyle = dataGridViewCellStyle8;
            this.gridProblems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridProblems.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridProblems.Location = new System.Drawing.Point(3, 3);
            this.gridProblems.MultiSelect = false;
            this.gridProblems.Name = "gridProblems";
            this.gridProblems.ReadOnly = true;
            this.gridProblems.RowHeadersVisible = false;
            this.gridProblems.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridProblems.RowsDefaultCellStyle = dataGridViewCellStyle9;
            this.gridProblems.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.gridProblems.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridProblems.RowTemplate.Height = 500;
            this.gridProblems.RowTemplate.ReadOnly = true;
            this.gridProblems.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.gridProblems.Size = new System.Drawing.Size(179, 770);
            this.gridProblems.TabIndex = 1;
            // 
            // columnProblems
            // 
            this.columnProblems.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.columnProblems.DefaultCellStyle = dataGridViewCellStyle7;
            this.columnProblems.HeaderText = "Problems";
            this.columnProblems.Name = "columnProblems";
            this.columnProblems.ReadOnly = true;
            this.columnProblems.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.columnProblems.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // tabTargetBoxes
            // 
            this.tabTargetBoxes.Controls.Add(this.buttonTargetBoxShowCode);
            this.tabTargetBoxes.Controls.Add(this.groupBox3);
            this.tabTargetBoxes.Controls.Add(this.label10);
            this.tabTargetBoxes.Controls.Add(this.numericTargetBoxIndex);
            this.tabTargetBoxes.Controls.Add(this.listTargetBoxes);
            this.tabTargetBoxes.Controls.Add(this.groupBox1);
            this.tabTargetBoxes.Controls.Add(this.groupBox2);
            this.tabTargetBoxes.Controls.Add(this.buttonRemoveTargetBox);
            this.tabTargetBoxes.Controls.Add(this.buttonAddTargetBox);
            this.tabTargetBoxes.Location = new System.Drawing.Point(4, 40);
            this.tabTargetBoxes.Name = "tabTargetBoxes";
            this.tabTargetBoxes.Size = new System.Drawing.Size(185, 776);
            this.tabTargetBoxes.TabIndex = 2;
            this.tabTargetBoxes.Text = "Target boxes";
            this.tabTargetBoxes.UseVisualStyleBackColor = true;
            // 
            // buttonTargetBoxShowCode
            // 
            this.buttonTargetBoxShowCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonTargetBoxShowCode.Enabled = false;
            this.buttonTargetBoxShowCode.Location = new System.Drawing.Point(3, 725);
            this.buttonTargetBoxShowCode.Name = "buttonTargetBoxShowCode";
            this.buttonTargetBoxShowCode.Size = new System.Drawing.Size(180, 23);
            this.buttonTargetBoxShowCode.TabIndex = 44;
            this.buttonTargetBoxShowCode.Text = "Show code";
            this.buttonTargetBoxShowCode.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.labelTargetBoxLength);
            this.groupBox3.Controls.Add(this.label20);
            this.groupBox3.Controls.Add(this.labelTargetBoxHeight);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.labelTargetBoxWidth);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.labelTargetBoxMaxZ);
            this.groupBox3.Controls.Add(this.labelTargetBoxMaxY);
            this.groupBox3.Controls.Add(this.labelTargetBoxMaxX);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Controls.Add(this.label19);
            this.groupBox3.Controls.Add(this.labelTargetBoxMinZ);
            this.groupBox3.Controls.Add(this.labelTargetBoxMinY);
            this.groupBox3.Controls.Add(this.labelTargetBoxMinX);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Location = new System.Drawing.Point(3, 529);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(179, 190);
            this.groupBox3.TabIndex = 41;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Measurements";
            // 
            // labelTargetBoxLength
            // 
            this.labelTargetBoxLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTargetBoxLength.AutoSize = true;
            this.labelTargetBoxLength.Location = new System.Drawing.Point(71, 167);
            this.labelTargetBoxLength.Name = "labelTargetBoxLength";
            this.labelTargetBoxLength.Size = new System.Drawing.Size(22, 13);
            this.labelTargetBoxLength.TabIndex = 41;
            this.labelTargetBoxLength.Text = "0,0";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(6, 167);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(43, 13);
            this.label20.TabIndex = 40;
            this.label20.Text = "Length:";
            // 
            // labelTargetBoxHeight
            // 
            this.labelTargetBoxHeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTargetBoxHeight.AutoSize = true;
            this.labelTargetBoxHeight.Location = new System.Drawing.Point(71, 150);
            this.labelTargetBoxHeight.Name = "labelTargetBoxHeight";
            this.labelTargetBoxHeight.Size = new System.Drawing.Size(22, 13);
            this.labelTargetBoxHeight.TabIndex = 39;
            this.labelTargetBoxHeight.Text = "0,0";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 150);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(38, 13);
            this.label16.TabIndex = 38;
            this.label16.Text = "Height";
            // 
            // labelTargetBoxWidth
            // 
            this.labelTargetBoxWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTargetBoxWidth.AutoSize = true;
            this.labelTargetBoxWidth.Location = new System.Drawing.Point(71, 133);
            this.labelTargetBoxWidth.Name = "labelTargetBoxWidth";
            this.labelTargetBoxWidth.Size = new System.Drawing.Size(22, 13);
            this.labelTargetBoxWidth.TabIndex = 37;
            this.labelTargetBoxWidth.Text = "0,0";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 133);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(38, 13);
            this.label15.TabIndex = 36;
            this.label15.Text = "Width:";
            // 
            // labelTargetBoxMaxZ
            // 
            this.labelTargetBoxMaxZ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTargetBoxMaxZ.AutoSize = true;
            this.labelTargetBoxMaxZ.Location = new System.Drawing.Point(71, 110);
            this.labelTargetBoxMaxZ.Name = "labelTargetBoxMaxZ";
            this.labelTargetBoxMaxZ.Size = new System.Drawing.Size(22, 13);
            this.labelTargetBoxMaxZ.TabIndex = 35;
            this.labelTargetBoxMaxZ.Text = "0,0";
            // 
            // labelTargetBoxMaxY
            // 
            this.labelTargetBoxMaxY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTargetBoxMaxY.AutoSize = true;
            this.labelTargetBoxMaxY.Location = new System.Drawing.Point(71, 92);
            this.labelTargetBoxMaxY.Name = "labelTargetBoxMaxY";
            this.labelTargetBoxMaxY.Size = new System.Drawing.Size(22, 13);
            this.labelTargetBoxMaxY.TabIndex = 34;
            this.labelTargetBoxMaxY.Text = "0,0";
            // 
            // labelTargetBoxMaxX
            // 
            this.labelTargetBoxMaxX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTargetBoxMaxX.AutoSize = true;
            this.labelTargetBoxMaxX.Location = new System.Drawing.Point(71, 74);
            this.labelTargetBoxMaxX.Name = "labelTargetBoxMaxX";
            this.labelTargetBoxMaxX.Size = new System.Drawing.Size(22, 13);
            this.labelTargetBoxMaxX.TabIndex = 33;
            this.labelTargetBoxMaxX.Text = "0,0";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 110);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(37, 13);
            this.label17.TabIndex = 32;
            this.label17.Text = "MaxZ:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 92);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(37, 13);
            this.label18.TabIndex = 31;
            this.label18.Text = "MaxY:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(6, 74);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(37, 13);
            this.label19.TabIndex = 30;
            this.label19.Text = "MaxX:";
            // 
            // labelTargetBoxMinZ
            // 
            this.labelTargetBoxMinZ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTargetBoxMinZ.AutoSize = true;
            this.labelTargetBoxMinZ.Location = new System.Drawing.Point(71, 52);
            this.labelTargetBoxMinZ.Name = "labelTargetBoxMinZ";
            this.labelTargetBoxMinZ.Size = new System.Drawing.Size(22, 13);
            this.labelTargetBoxMinZ.TabIndex = 29;
            this.labelTargetBoxMinZ.Text = "0,0";
            // 
            // labelTargetBoxMinY
            // 
            this.labelTargetBoxMinY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTargetBoxMinY.AutoSize = true;
            this.labelTargetBoxMinY.Location = new System.Drawing.Point(71, 34);
            this.labelTargetBoxMinY.Name = "labelTargetBoxMinY";
            this.labelTargetBoxMinY.Size = new System.Drawing.Size(22, 13);
            this.labelTargetBoxMinY.TabIndex = 28;
            this.labelTargetBoxMinY.Text = "0,0";
            // 
            // labelTargetBoxMinX
            // 
            this.labelTargetBoxMinX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTargetBoxMinX.AutoSize = true;
            this.labelTargetBoxMinX.Location = new System.Drawing.Point(71, 16);
            this.labelTargetBoxMinX.Name = "labelTargetBoxMinX";
            this.labelTargetBoxMinX.Size = new System.Drawing.Size(22, 13);
            this.labelTargetBoxMinX.TabIndex = 27;
            this.labelTargetBoxMinX.Text = "0,0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 52);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(34, 13);
            this.label11.TabIndex = 26;
            this.label11.Text = "MinZ:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 34);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(34, 13);
            this.label12.TabIndex = 24;
            this.label12.Text = "MinY:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 16);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(34, 13);
            this.label13.TabIndex = 22;
            this.label13.Text = "MinX:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 307);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(36, 13);
            this.label10.TabIndex = 43;
            this.label10.Text = "Index:";
            // 
            // numericTargetBoxIndex
            // 
            this.numericTargetBoxIndex.Enabled = false;
            this.numericTargetBoxIndex.Location = new System.Drawing.Point(45, 304);
            this.numericTargetBoxIndex.Name = "numericTargetBoxIndex";
            this.numericTargetBoxIndex.Size = new System.Drawing.Size(41, 20);
            this.numericTargetBoxIndex.TabIndex = 42;
            // 
            // listTargetBoxes
            // 
            this.listTargetBoxes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listTargetBoxes.FormattingEnabled = true;
            this.listTargetBoxes.Location = new System.Drawing.Point(3, 3);
            this.listTargetBoxes.Name = "listTargetBoxes";
            this.listTargetBoxes.Size = new System.Drawing.Size(179, 259);
            this.listTargetBoxes.TabIndex = 41;
            this.listTargetBoxes.SelectedIndexChanged += new System.EventHandler(this.listTargetBoxes_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.numericTargetBoxMaxZ);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.numericTargetBoxMaxY);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.numericTargetBoxMaxX);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(3, 430);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(179, 93);
            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Max";
            // 
            // numericTargetBoxMaxZ
            // 
            this.numericTargetBoxMaxZ.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericTargetBoxMaxZ.DecimalPlaces = 4;
            this.numericTargetBoxMaxZ.Enabled = false;
            this.numericTargetBoxMaxZ.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numericTargetBoxMaxZ.Location = new System.Drawing.Point(72, 66);
            this.numericTargetBoxMaxZ.Maximum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            0});
            this.numericTargetBoxMaxZ.Minimum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            -2147483648});
            this.numericTargetBoxMaxZ.Name = "numericTargetBoxMaxZ";
            this.numericTargetBoxMaxZ.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericTargetBoxMaxZ.Size = new System.Drawing.Size(101, 20);
            this.numericTargetBoxMaxZ.TabIndex = 27;
            this.numericTargetBoxMaxZ.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "Z:";
            // 
            // numericTargetBoxMaxY
            // 
            this.numericTargetBoxMaxY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericTargetBoxMaxY.DecimalPlaces = 4;
            this.numericTargetBoxMaxY.Enabled = false;
            this.numericTargetBoxMaxY.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numericTargetBoxMaxY.Location = new System.Drawing.Point(72, 40);
            this.numericTargetBoxMaxY.Maximum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            0});
            this.numericTargetBoxMaxY.Minimum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            -2147483648});
            this.numericTargetBoxMaxY.Name = "numericTargetBoxMaxY";
            this.numericTargetBoxMaxY.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericTargetBoxMaxY.Size = new System.Drawing.Size(101, 20);
            this.numericTargetBoxMaxY.TabIndex = 25;
            this.numericTargetBoxMaxY.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Y:";
            // 
            // numericTargetBoxMaxX
            // 
            this.numericTargetBoxMaxX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericTargetBoxMaxX.DecimalPlaces = 4;
            this.numericTargetBoxMaxX.Enabled = false;
            this.numericTargetBoxMaxX.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numericTargetBoxMaxX.Location = new System.Drawing.Point(72, 14);
            this.numericTargetBoxMaxX.Maximum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            0});
            this.numericTargetBoxMaxX.Minimum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            -2147483648});
            this.numericTargetBoxMaxX.Name = "numericTargetBoxMaxX";
            this.numericTargetBoxMaxX.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericTargetBoxMaxX.Size = new System.Drawing.Size(101, 20);
            this.numericTargetBoxMaxX.TabIndex = 23;
            this.numericTargetBoxMaxX.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "X:";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.numericTargetBoxMinZ);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.numericTargetBoxMinY);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.numericTargetBoxMinX);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Location = new System.Drawing.Point(3, 331);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(179, 93);
            this.groupBox2.TabIndex = 39;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Min";
            // 
            // numericTargetBoxMinZ
            // 
            this.numericTargetBoxMinZ.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericTargetBoxMinZ.DecimalPlaces = 4;
            this.numericTargetBoxMinZ.Enabled = false;
            this.numericTargetBoxMinZ.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numericTargetBoxMinZ.Location = new System.Drawing.Point(72, 66);
            this.numericTargetBoxMinZ.Maximum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            0});
            this.numericTargetBoxMinZ.Minimum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            -2147483648});
            this.numericTargetBoxMinZ.Name = "numericTargetBoxMinZ";
            this.numericTargetBoxMinZ.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericTargetBoxMinZ.Size = new System.Drawing.Size(101, 20);
            this.numericTargetBoxMinZ.TabIndex = 27;
            this.numericTargetBoxMinZ.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 68);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 13);
            this.label7.TabIndex = 26;
            this.label7.Text = "Z:";
            // 
            // numericTargetBoxMinY
            // 
            this.numericTargetBoxMinY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericTargetBoxMinY.DecimalPlaces = 4;
            this.numericTargetBoxMinY.Enabled = false;
            this.numericTargetBoxMinY.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numericTargetBoxMinY.Location = new System.Drawing.Point(72, 40);
            this.numericTargetBoxMinY.Maximum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            0});
            this.numericTargetBoxMinY.Minimum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            -2147483648});
            this.numericTargetBoxMinY.Name = "numericTargetBoxMinY";
            this.numericTargetBoxMinY.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericTargetBoxMinY.Size = new System.Drawing.Size(101, 20);
            this.numericTargetBoxMinY.TabIndex = 25;
            this.numericTargetBoxMinY.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 42);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 13);
            this.label8.TabIndex = 24;
            this.label8.Text = "Y:";
            // 
            // numericTargetBoxMinX
            // 
            this.numericTargetBoxMinX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericTargetBoxMinX.DecimalPlaces = 4;
            this.numericTargetBoxMinX.Enabled = false;
            this.numericTargetBoxMinX.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numericTargetBoxMinX.Location = new System.Drawing.Point(72, 14);
            this.numericTargetBoxMinX.Maximum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            0});
            this.numericTargetBoxMinX.Minimum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            -2147483648});
            this.numericTargetBoxMinX.Name = "numericTargetBoxMinX";
            this.numericTargetBoxMinX.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericTargetBoxMinX.Size = new System.Drawing.Size(101, 20);
            this.numericTargetBoxMinX.TabIndex = 23;
            this.numericTargetBoxMinX.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "X:";
            // 
            // buttonRemoveTargetBox
            // 
            this.buttonRemoveTargetBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRemoveTargetBox.Enabled = false;
            this.buttonRemoveTargetBox.Location = new System.Drawing.Point(98, 273);
            this.buttonRemoveTargetBox.Name = "buttonRemoveTargetBox";
            this.buttonRemoveTargetBox.Size = new System.Drawing.Size(84, 23);
            this.buttonRemoveTargetBox.TabIndex = 36;
            this.buttonRemoveTargetBox.Text = "Remove";
            this.buttonRemoveTargetBox.UseVisualStyleBackColor = true;
            // 
            // buttonAddTargetBox
            // 
            this.buttonAddTargetBox.Location = new System.Drawing.Point(3, 273);
            this.buttonAddTargetBox.Name = "buttonAddTargetBox";
            this.buttonAddTargetBox.Size = new System.Drawing.Size(89, 23);
            this.buttonAddTargetBox.TabIndex = 35;
            this.buttonAddTargetBox.Text = "Add";
            this.buttonAddTargetBox.UseVisualStyleBackColor = true;
            // 
            // tabShipTypeMapping
            // 
            this.tabShipTypeMapping.Controls.Add(this.dataGridShipTypes);
            this.tabShipTypeMapping.Location = new System.Drawing.Point(4, 40);
            this.tabShipTypeMapping.Name = "tabShipTypeMapping";
            this.tabShipTypeMapping.Size = new System.Drawing.Size(185, 776);
            this.tabShipTypeMapping.TabIndex = 3;
            this.tabShipTypeMapping.Text = "Ship type mapping";
            this.tabShipTypeMapping.UseVisualStyleBackColor = true;
            // 
            // dataGridShipTypes
            // 
            this.dataGridShipTypes.AllowUserToAddRows = false;
            this.dataGridShipTypes.AllowUserToResizeRows = false;
            this.dataGridShipTypes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridShipTypes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridShipTypes.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridShipTypes.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dataGridShipTypes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridShipTypes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridShipTypesColumnDAE,
            this.dataGridShipTypesColumnSHIP});
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridShipTypes.DefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridShipTypes.Location = new System.Drawing.Point(0, 0);
            this.dataGridShipTypes.MultiSelect = false;
            this.dataGridShipTypes.Name = "dataGridShipTypes";
            this.dataGridShipTypes.RowHeadersVisible = false;
            this.dataGridShipTypes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridShipTypes.Size = new System.Drawing.Size(185, 314);
            this.dataGridShipTypes.TabIndex = 0;
            // 
            // dataGridShipTypesColumnDAE
            // 
            this.dataGridShipTypesColumnDAE.HeaderText = "DAE file";
            this.dataGridShipTypesColumnDAE.Name = "dataGridShipTypesColumnDAE";
            // 
            // dataGridShipTypesColumnSHIP
            // 
            this.dataGridShipTypesColumnSHIP.HeaderText = "ship file";
            this.dataGridShipTypesColumnSHIP.Name = "dataGridShipTypesColumnSHIP";
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
            this.comboPerspectiveOrtho.Location = new System.Drawing.Point(1175, 2);
            this.comboPerspectiveOrtho.MaxDropDownItems = 1;
            this.comboPerspectiveOrtho.Name = "comboPerspectiveOrtho";
            this.comboPerspectiveOrtho.Size = new System.Drawing.Size(105, 21);
            this.comboPerspectiveOrtho.TabIndex = 5;
            this.comboPerspectiveOrtho.SelectedIndexChanged += new System.EventHandler(this.comboPerspectiveOrtho_SelectedIndexChanged);
            // 
            // labelFPS
            // 
            this.labelFPS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelFPS.AutoSize = true;
            this.labelFPS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.labelFPS.Location = new System.Drawing.Point(1133, 6);
            this.labelFPS.Name = "labelFPS";
            this.labelFPS.Size = new System.Drawing.Size(36, 13);
            this.labelFPS.TabIndex = 7;
            this.labelFPS.Text = "0 FPS";
            this.labelFPS.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            // colorDialog
            // 
            this.colorDialog.AnyColor = true;
            this.colorDialog.Color = System.Drawing.Color.Gray;
            this.colorDialog.FullOpen = true;
            this.colorDialog.SolidColorOnly = true;
            // 
            // contextJointRightClick
            // 
            this.contextJointRightClick.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextJointRightClickItemShowDescendants,
            this.contextJointRightClickItemHideDescendants,
            this.contextJointRightClickItemShowAll,
            this.contextJointRightClickItemHideAll});
            this.contextJointRightClick.Name = "contextShowHideAll";
            this.contextJointRightClick.ShowImageMargin = false;
            this.contextJointRightClick.Size = new System.Drawing.Size(148, 92);
            this.contextJointRightClick.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextJointRightClick_ItemClicked);
            // 
            // contextJointRightClickItemShowDescendants
            // 
            this.contextJointRightClickItemShowDescendants.Name = "contextJointRightClickItemShowDescendants";
            this.contextJointRightClickItemShowDescendants.Size = new System.Drawing.Size(147, 22);
            this.contextJointRightClickItemShowDescendants.Text = "Show descendants";
            // 
            // contextJointRightClickItemHideDescendants
            // 
            this.contextJointRightClickItemHideDescendants.Name = "contextJointRightClickItemHideDescendants";
            this.contextJointRightClickItemHideDescendants.Size = new System.Drawing.Size(147, 22);
            this.contextJointRightClickItemHideDescendants.Text = "Hide descendants";
            // 
            // contextJointRightClickItemShowAll
            // 
            this.contextJointRightClickItemShowAll.Name = "contextJointRightClickItemShowAll";
            this.contextJointRightClickItemShowAll.Size = new System.Drawing.Size(147, 22);
            this.contextJointRightClickItemShowAll.Text = "Show all";
            // 
            // contextJointRightClickItemHideAll
            // 
            this.contextJointRightClickItemHideAll.Name = "contextJointRightClickItemHideAll";
            this.contextJointRightClickItemHideAll.Size = new System.Drawing.Size(147, 22);
            this.contextJointRightClickItemHideAll.Text = "Hide all";
            // 
            // Main
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 845);
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
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Main_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Main_DragEnter);
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
            this.tabCollisionMeshes.PerformLayout();
            this.groupCollisionMeshPreview.ResumeLayout(false);
            this.groupCollisionMeshPreview.PerformLayout();
            this.contextShowHideAll.ResumeLayout(false);
            this.tabJoints.ResumeLayout(false);
            this.tabJoints.PerformLayout();
            this.groupJointRotation.ResumeLayout(false);
            this.groupJointRotation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericJointRotationZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericJointRotationY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericJointRotationX)).EndInit();
            this.groupJointPosition.ResumeLayout(false);
            this.groupJointPosition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericJointPositionZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericJointPositionY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericJointPositionX)).EndInit();
            this.tabMarkers.ResumeLayout(false);
            this.tabMarkers.PerformLayout();
            this.groupMarkerRotation.ResumeLayout(false);
            this.groupMarkerRotation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericMarkerRotationZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMarkerRotationY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMarkerRotationX)).EndInit();
            this.groupMarkerPosition.ResumeLayout(false);
            this.groupMarkerPosition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericMarkerPositionZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMarkerPositionY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMarkerPositionX)).EndInit();
            this.groupMarkerPreview.ResumeLayout(false);
            this.groupMarkerPreview.PerformLayout();
            this.tabDockpaths.ResumeLayout(false);
            this.tabDockpaths.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericDockpathAnimationIndex)).EndInit();
            this.groupDockpathFlags.ResumeLayout(false);
            this.groupDockpathFlags.PerformLayout();
            this.groupDockpathSegments.ResumeLayout(false);
            this.groupDockpathSegments.PerformLayout();
            this.groupDockpathSegmentRotation.ResumeLayout(false);
            this.groupDockpathSegmentRotation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericDockpathSegmentRotationZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDockpathSegmentRotationY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDockpathSegmentRotationX)).EndInit();
            this.groupDockpathSegmentPosition.ResumeLayout(false);
            this.groupDockpathSegmentPosition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericDockpathSegmentPosZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDockpathSegmentPosY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDockpathSegmentPosX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDockpathSegmentSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDockpathSegmentTolerance)).EndInit();
            this.groupDockpathSegmentFlags.ResumeLayout(false);
            this.groupDockpathSegmentFlags.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDockpathSegments)).EndInit();
            this.groupDockpathLinks.ResumeLayout(false);
            this.groupDockpathLinks.PerformLayout();
            this.groupDockpathFamilies.ResumeLayout(false);
            this.groupDockpathFamilies.PerformLayout();
            this.tabNavLights.ResumeLayout(false);
            this.tabNavLights.PerformLayout();
            this.groupNavLightPosition.ResumeLayout(false);
            this.groupNavLightPosition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericNavLightPositionZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericNavLightPositionY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericNavLightPositionX)).EndInit();
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
            this.tabEngineGlows.ResumeLayout(false);
            this.tabEngineGlows.PerformLayout();
            this.groupEngineGlowLODs.ResumeLayout(false);
            this.tabEngineShapes.ResumeLayout(false);
            this.tabEngineShapes.PerformLayout();
            this.tabEngineBurns.ResumeLayout(false);
            this.tabEngineBurns.PerformLayout();
            this.groupEngineBurnFlames.ResumeLayout(false);
            this.groupEngineBurnFlames.PerformLayout();
            this.groupEngineBurnFlamePosition.ResumeLayout(false);
            this.groupEngineBurnFlamePosition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericEngineBurnFlamePosZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericEngineBurnFlamePosY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericEngineBurnFlamePosX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericEngineBurnSpriteIndex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarEngineBurnFlames)).EndInit();
            this.tabAnimations.ResumeLayout(false);
            this.tabAnimations.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericAnimationLoopEndTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericAnimationLoopStartTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericAnimationEndTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericAnimationStartTime)).EndInit();
            this.groupAnimationJoints.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tabControlRight.ResumeLayout(false);
            this.tabProblems.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridProblems)).EndInit();
            this.tabTargetBoxes.ResumeLayout(false);
            this.tabTargetBoxes.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericTargetBoxIndex)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericTargetBoxMaxZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericTargetBoxMaxY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericTargetBoxMaxX)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericTargetBoxMinZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericTargetBoxMinY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericTargetBoxMinX)).EndInit();
            this.tabShipTypeMapping.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridShipTypes)).EndInit();
            this.contextJointRightClick.ResumeLayout(false);
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
        private System.Windows.Forms.TabPage tabJoints;
        private System.Windows.Forms.TabPage tabMarkers;
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
        private System.Windows.Forms.TrackBar trackBarDockpathSegments;
        private System.Windows.Forms.GroupBox groupDockpathLinks;
        private System.Windows.Forms.ListBox listDockpathLinks;
        private System.Windows.Forms.GroupBox groupDockpathFamilies;
        private System.Windows.Forms.ListBox listDockpathFamilies;
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
        private System.Windows.Forms.TabPage tabEngineGlows;
        private System.Windows.Forms.ToolStripButton buttonHotkeys;
        private System.Windows.Forms.TabPage tabEngineShapes;
        public System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer1;
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
        private System.Windows.Forms.Button buttonCollisionMeshImportDAE;
        private System.Windows.Forms.Button buttonCollisionMeshExportDAE;
        private System.Windows.Forms.Button buttonCollisionMeshImportOBJ;
        private System.Windows.Forms.Button buttonCollisionMeshExportOBJ;
        private System.Windows.Forms.Button buttonCollisionMeshRemove;
        private System.Windows.Forms.Button buttonCollisionMeshAdd;
        private System.Windows.Forms.CheckedListBox listCollisionMeshes;
        private System.Windows.Forms.Label labelCollisionMeshParent;
        private System.Windows.Forms.ComboBox comboCollisionMeshParent;
        private System.Windows.Forms.Button buttonEngineGlowRemove;
        private System.Windows.Forms.Button buttonEngineGlowAdd;
        private System.Windows.Forms.TextBox boxEngineGlowName;
        private System.Windows.Forms.Label labelEngineGlowName;
        private System.Windows.Forms.ListBox listEngineGlows;
        private System.Windows.Forms.Label labelEngineGlowParent;
        private System.Windows.Forms.ComboBox comboEngineGlowParent;
        private System.Windows.Forms.GroupBox groupEngineGlowLODs;
        private System.Windows.Forms.Button buttonEngineGlowLODImportDAE;
        private System.Windows.Forms.Button buttonEngineGlowLODRemove;
        private System.Windows.Forms.Button buttonEngineGlowLODExportDAE;
        private System.Windows.Forms.Button buttonEngineGlowLODAdd;
        private System.Windows.Forms.Button buttonEngineGlowLODImportOBJ;
        private System.Windows.Forms.Button buttonEngineGlowLODExportOBJ;
        private System.Windows.Forms.CheckedListBox listEngineGlowLODs;
        private System.Windows.Forms.CheckedListBox listNavLights;
        private System.Windows.Forms.Button buttonNavLightRemove;
        private System.Windows.Forms.Button buttonNavLightAdd;
        private System.Windows.Forms.TextBox boxNavLightName;
        private System.Windows.Forms.Label labelNavLightName;
        private System.Windows.Forms.Label labelNavLightParent;
        private System.Windows.Forms.ComboBox comboNavLightParent;
        private System.Windows.Forms.GroupBox groupNavLightPosition;
        private System.Windows.Forms.NumericUpDown numericNavLightPositionX;
        private System.Windows.Forms.Label labelNavLightPositionX;
        private System.Windows.Forms.NumericUpDown numericNavLightPositionZ;
        private System.Windows.Forms.Label labelNavLightPositionZ;
        private System.Windows.Forms.NumericUpDown numericNavLightPositionY;
        private System.Windows.Forms.Label labelNavLightPositionY;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.GroupBox groupJointPosition;
        private System.Windows.Forms.NumericUpDown numericJointPositionZ;
        private System.Windows.Forms.Label labelJointPositionZ;
        private System.Windows.Forms.NumericUpDown numericJointPositionY;
        private System.Windows.Forms.Label labelJointPositionY;
        private System.Windows.Forms.NumericUpDown numericJointPositionX;
        private System.Windows.Forms.Label labelJointPositionX;
        private System.Windows.Forms.Label labelJointParent;
        private System.Windows.Forms.ComboBox comboJointParent;
        private System.Windows.Forms.TextBox boxJointName;
        private System.Windows.Forms.Label labelJointName;
        private System.Windows.Forms.Button buttonJointRemove;
        private System.Windows.Forms.Button buttonJointAdd;
        private System.Windows.Forms.GroupBox groupJointRotation;
        private System.Windows.Forms.NumericUpDown numericJointRotationZ;
        private System.Windows.Forms.Label labelJointRotationZ;
        private System.Windows.Forms.NumericUpDown numericJointRotationY;
        private System.Windows.Forms.Label labelJointRotationY;
        private System.Windows.Forms.NumericUpDown numericJointRotationX;
        private System.Windows.Forms.Label labelJointRotationX;
        private System.Windows.Forms.GroupBox groupMarkerPreview;
        private System.Windows.Forms.CheckBox checkDrawMarkers;
        private System.Windows.Forms.GroupBox groupMarkerRotation;
        private System.Windows.Forms.NumericUpDown numericMarkerRotationZ;
        private System.Windows.Forms.Label labelMarkerRotationZ;
        private System.Windows.Forms.NumericUpDown numericMarkerRotationY;
        private System.Windows.Forms.Label labelMarkerRotationY;
        private System.Windows.Forms.NumericUpDown numericMarkerRotationX;
        private System.Windows.Forms.Label labelMarkerRotationX;
        private System.Windows.Forms.GroupBox groupMarkerPosition;
        private System.Windows.Forms.NumericUpDown numericMarkerPositionZ;
        private System.Windows.Forms.Label labelMarkerPositionZ;
        private System.Windows.Forms.NumericUpDown numericMarkerPositionY;
        private System.Windows.Forms.Label labelMarkerPositionY;
        private System.Windows.Forms.NumericUpDown numericMarkerPositionX;
        private System.Windows.Forms.Label labelMarkerPositionX;
        private System.Windows.Forms.Label labelMarkerParent;
        private System.Windows.Forms.ComboBox comboMarkerParent;
        private System.Windows.Forms.TextBox boxMarkerName;
        private System.Windows.Forms.Label labelMarkerName;
        private System.Windows.Forms.Button buttonMarkerRemove;
        private System.Windows.Forms.Button buttonMarkerAdd;
        private System.Windows.Forms.ListBox listMarkers;
        private System.Windows.Forms.Button buttonJointAddTemplate;
        public System.Windows.Forms.TreeView jointsTree;
        private System.Windows.Forms.Button buttonJointRemoveAll;
        private System.Windows.Forms.CheckedListBox listEngineShapes;
        private System.Windows.Forms.Label labelEngineShapeParent;
        private System.Windows.Forms.ComboBox comboEngineShapeParent;
        private System.Windows.Forms.CheckedListBox listEngineBurns;
        private System.Windows.Forms.CheckedListBox listDockpaths;
        private System.Windows.Forms.Button buttonEngineShapeImportDAE;
        private System.Windows.Forms.Button buttonEngineShapeExportDAE;
        private System.Windows.Forms.Button buttonEngineShapeImportOBJ;
        private System.Windows.Forms.Button buttonEngineShapeExportOBJ;
        private System.Windows.Forms.Button buttonEngineShapeRemove;
        private System.Windows.Forms.Button buttonEngineShapeAdd;
        private System.Windows.Forms.TextBox boxEngineShapeName;
        private System.Windows.Forms.Label labelEngineShapeName;
        private System.Windows.Forms.Button buttonDockpathRemove;
        private System.Windows.Forms.Button buttonDockpathAdd;
        private System.Windows.Forms.TextBox boxDockpathFamilyName;
        private System.Windows.Forms.Label labelDockpathFamilyName;
        private System.Windows.Forms.Button buttonDockpathFamilyRemove;
        private System.Windows.Forms.Button buttonDockpathFamilyAdd;
        private System.Windows.Forms.ComboBox comboDockpathLinkPath;
        private System.Windows.Forms.Button buttonDockpathLinkRemove;
        private System.Windows.Forms.Button buttonDockpathLinkAdd;
        private System.Windows.Forms.Label labelDockpathLinkPath;
        private System.Windows.Forms.Button buttonDockpathSegmentRemove;
        private System.Windows.Forms.Button buttonDockpathSegmentInsertAfter;
        private System.Windows.Forms.NumericUpDown numericDockpathSegmentSpeed;
        private System.Windows.Forms.NumericUpDown numericDockpathSegmentTolerance;
        private System.Windows.Forms.Button buttonDockpathSegmentInsertBefore;
        private System.Windows.Forms.GroupBox groupDockpathSegmentPosition;
        private System.Windows.Forms.NumericUpDown numericDockpathSegmentPosZ;
        private System.Windows.Forms.Label labelDockpathSegmentPosZ;
        private System.Windows.Forms.NumericUpDown numericDockpathSegmentPosY;
        private System.Windows.Forms.Label labelDockpathSegmentPosY;
        private System.Windows.Forms.NumericUpDown numericDockpathSegmentPosX;
        private System.Windows.Forms.Label labelDockpathSegmentPosX;
        private System.Windows.Forms.GroupBox groupDockpathSegmentRotation;
        private System.Windows.Forms.NumericUpDown numericDockpathSegmentRotationZ;
        private System.Windows.Forms.Label labelDockpathSegmentRotationZ;
        private System.Windows.Forms.NumericUpDown numericDockpathSegmentRotationY;
        private System.Windows.Forms.Label labelDockpathSegmentRotationY;
        private System.Windows.Forms.NumericUpDown numericDockpathSegmentRotationX;
        private System.Windows.Forms.Label labelDockpathSegmentRotationX;
        private System.Windows.Forms.Button buttonEngineBurnRemove;
        private System.Windows.Forms.Button buttonEngineBurnAdd;
        private System.Windows.Forms.Button buttonEngineBurnFlameRemove;
        private System.Windows.Forms.Button buttonEngineBurnFlameAdd;
        private System.Windows.Forms.GroupBox groupEngineBurnFlamePosition;
        private System.Windows.Forms.NumericUpDown numericEngineBurnFlamePosZ;
        private System.Windows.Forms.Label labelEngineBurnFlamePosZ;
        private System.Windows.Forms.NumericUpDown numericEngineBurnFlamePosY;
        private System.Windows.Forms.Label labelEngineBurnFlamePosY;
        private System.Windows.Forms.NumericUpDown numericEngineBurnFlamePosX;
        private System.Windows.Forms.Label labelEngineBurnFlamePosX;
        private System.Windows.Forms.ContextMenuStrip contextShowHideAll;
        private System.Windows.Forms.ToolStripMenuItem itemShowAll;
        private System.Windows.Forms.ToolStripMenuItem itemHideAll;
        private System.Windows.Forms.ContextMenuStrip contextJointRightClick;
        private System.Windows.Forms.ToolStripMenuItem contextJointRightClickItemShowDescendants;
        private System.Windows.Forms.ToolStripMenuItem contextJointRightClickItemHideDescendants;
        private System.Windows.Forms.ToolStripMenuItem contextJointRightClickItemShowAll;
        private System.Windows.Forms.ToolStripMenuItem contextJointRightClickItemHideAll;
        private System.Windows.Forms.GroupBox groupCollisionMeshPreview;
        private System.Windows.Forms.CheckBox checkCollisionMeshPreviewBox;
        private System.Windows.Forms.CheckBox checkCollisionMeshPreviewSphere;
        private System.Windows.Forms.TabControl tabControlRight;
        private System.Windows.Forms.TabPage tabProblems;
        private System.Windows.Forms.DataGridView gridProblems;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnProblems;
        private System.Windows.Forms.TabPage tabTargetBoxes;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numericTargetBoxMaxZ;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericTargetBoxMaxY;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericTargetBoxMaxX;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown numericTargetBoxMinZ;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericTargetBoxMinY;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numericTargetBoxMinX;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button buttonRemoveTargetBox;
        private System.Windows.Forms.Button buttonAddTargetBox;
        private System.Windows.Forms.CheckedListBox listTargetBoxes;
        private System.Windows.Forms.TabPage tabShipTypeMapping;
        private System.Windows.Forms.DataGridView dataGridShipTypes;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridShipTypesColumnDAE;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridShipTypesColumnSHIP;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown numericTargetBoxIndex;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label labelTargetBoxMaxZ;
        private System.Windows.Forms.Label labelTargetBoxMaxY;
        private System.Windows.Forms.Label labelTargetBoxMaxX;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label labelTargetBoxMinZ;
        private System.Windows.Forms.Label labelTargetBoxMinY;
        private System.Windows.Forms.Label labelTargetBoxMinX;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label labelTargetBoxHeight;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label labelTargetBoxWidth;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label labelTargetBoxLength;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button buttonTargetBoxShowCode;
    }
}

