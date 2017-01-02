using System;
using System.Drawing;
using System.Windows.Forms;
using OpenTK;
using System.IO;
using System.Collections.Generic;
using OpenTK.Graphics;
using Assimp;
using System.Media;
using static Extensions.Utilities;

namespace DAEnerys
{
    public partial class Main : Form
    {
        public int BUILD = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Build;
        public string OpenedFile = "";

        public bool Loaded = false;
        HWJoint selectedJoint;
        HWCollisionMesh selectedCollisionMesh;
        HWDockpath selectedDockpath;
        HWNavLight selectedNavLight;
        HWEngineBurn selectedEngineBurn;
        HWEngineGlow selectedEngineGlow;
        HWMaterial selectedMaterial;
        HWMarker selectedMarker;
        public HWAnimation selectedAnimation;

        public Dictionary<object, HWShipMesh> ShipMeshListItems = new Dictionary<object, HWShipMesh>();
        private Label[] ShipMeshLODMaterialLabels = new Label[MAX_MATERIALS_ON_MESH];
        private ComboBox[] ShipMeshLODMaterialComboBoxes = new ComboBox[MAX_MATERIALS_ON_MESH];
        private HWShipMesh selectedShipMesh;
        private int selectedShipMeshLOD;

        public Dictionary<object, HWEngineGlow> EngineGlowListItems = new Dictionary<object, HWEngineGlow>();
        private int selectedEngineGlowLOD;

        public Dictionary<string, HWMaterial> MaterialNames = new Dictionary<string, HWMaterial>();
        public Dictionary<string, HWAnimation> AnimationNames = new Dictionary<string, HWAnimation>();

        public bool DrawNavLightRadius;

        private bool problemsVisible;
        private bool ignoreShipMeshListSelectedIndexChanged;
        private bool ignoreMaterialListSelectedIndexChanged;
        private bool ignoreShipMeshDoScarCheck;
        private bool ignoreMaterialShaderChanged;
        private bool ignoreShipMeshLODMaterialChanged;
        private bool ignoreCollisionMeshParentChanged;
        private bool ignoreEngineGlowListSelectedIndexChanged;
        private bool ignoreNavLightValuesChanged;
        private bool ignoreNavLightListSelectedIndexChanged;
        private bool ignoreJointValuesChanged;
        private bool ignoreJointSelection;
        private bool ignoreMarkerListSelectedIndexChanged;
        private bool ignoreMarkerValuesChanged;

        private bool animationPlaying;
        public bool AnimationPlaying { get { return animationPlaying; } set { animationPlaying = value; HWAnimation.AnimationTime = 0; foreach (HWJoint joint in HWJoint.Joints) { joint.AnimationMatrix = Matrix4.Identity; joint.Invalidate(); Renderer.InvalidateView(); Renderer.Invalidate(); } string text = value ? "Stop" : "Play"; buttonAnimationPlay.Text = text; if (value) HWAnimation.AnimationTime = selectedAnimation.StartTime; } }

        const int MAX_MATERIALS_ON_MESH = 64;
        const int MAX_LEVEL_OF_DETAIL = 3;

        private float positionIncrement = 1;
        public float PositionIncrement
        {
            get { return positionIncrement; }
            set
            {
                positionIncrement = value;
                decimal increment = (decimal)value;

                numericJointPositionX.Increment = increment;
                numericJointPositionY.Increment = increment;
                numericJointPositionZ.Increment = increment;

                numericNavLightPositionX.Increment = increment;
                numericNavLightPositionY.Increment = increment;
                numericNavLightPositionZ.Increment = increment;
            }
        }

        private float rotationIncrement = 4.5f;
        public float RotationIncrement
        {
            get { return rotationIncrement; }
            set
            {
                rotationIncrement = value;
                decimal increment = (decimal)value;

                numericJointRotationX.Increment = increment;
                numericJointRotationY.Increment = increment;
                numericJointRotationZ.Increment = increment;
            }
        }

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            HWTexture.Init();
            Renderer.Init();
            EditorScene.Init();
            Application.Idle += glControl_Update;
            Log.WriteLine("OpenTK initialized.");
            comboPerspectiveOrtho.SelectedIndex = 0;

            FPSCounter.LabelFPS = labelFPS;

            Loaded = true;
            Program.DeltaCounter.Start();

            HWData.ParseDataPaths();
            Settings.SavedBackground = Settings.SavedBackground; //Loads the background after the data paths have been parsed

            HWBadge.LoadSavedBadge();

            gridProblems.RowTemplate.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            gridProblems.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            gridProblems.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            //Create ship mesh lod material selection
            for (int i = 0; i < MAX_MATERIALS_ON_MESH; i++)
            {
                ShipMeshLODMaterialLabels[i] = new Label();
                ShipMeshLODMaterialLabels[i].Parent = groupShipMeshLODMaterials;
                ShipMeshLODMaterialLabels[i].Location = new Point(6, 25);
                //ShipMeshLODMaterialLabels[i].Location = new Point(6, 25 + i * 27);
                ShipMeshLODMaterialLabels[i].Text = "#" + i;
                ShipMeshLODMaterialLabels[i].AutoSize = true;
                ShipMeshLODMaterialLabels[i].Visible = false;

                ShipMeshLODMaterialComboBoxes[i] = new ComboBox();
                ShipMeshLODMaterialComboBoxes[i].Parent = groupShipMeshLODMaterials;
                ShipMeshLODMaterialComboBoxes[i].Location = new Point(38, 22);
                //ShipMeshLODMaterialComboBoxes[i].Location = new Point(38, 22 + i * 27);
                ShipMeshLODMaterialComboBoxes[i].Size = new Size(180, 21);
                ShipMeshLODMaterialComboBoxes[i].DropDownStyle = ComboBoxStyle.DropDownList;
                ShipMeshLODMaterialComboBoxes[i].Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                ShipMeshLODMaterialComboBoxes[i].SelectedIndexChanged += new EventHandler(OnShipMeshLODMaterialChanged);
                ShipMeshLODMaterialComboBoxes[i].Visible = false;
            }

            Clear();

            if (Updater.CheckForUpdatesOnStart)
                Updater.CheckForUpdates();

            //Fill shader combo box
            foreach (string shader in NewShaderManifest.Manifest.HODAliases.Keys)
                comboMaterialShader.Items.Add(shader);

            //Open DAE from arguments
            if (Program.OPEN_PATH != null)
                if (File.Exists(Program.OPEN_PATH))
                {
                    Importer.ImportFromFile(Program.OPEN_PATH);
                    this.Text = Program.OPEN_PATH + " - DAEnerys";
                    OpenedFile = Path.GetFileNameWithoutExtension(Program.OPEN_PATH);

                    Renderer.InvalidateMeshData();
                    Renderer.InvalidateView();
                    Renderer.Invalidate();
                }
        }

        public void glControl_Update(object sender, EventArgs e)
        {
            //For frame-independent stuff
            Program.DeltaCounter.Stop();
            Program.ElapsedSeconds = Program.DeltaCounter.Elapsed.TotalSeconds;
            Program.ElapsedMilliseconds = Program.DeltaCounter.Elapsed.TotalMilliseconds;
            Program.DeltaCounter.Reset();
            Program.DeltaCounter.Start();

            Program.Camera.Update();

            Element.UpdateInvalids();

            int visibleNavLights = 0;
            foreach (HWNavLight navLight in HWNavLight.NavLights)
            {
                if (navLight.Visible)
                    visibleNavLights++;

                navLight.Update();
            }

            /*int visibleEffects = 0;
            foreach (EditorEffect effect in EditorScene.effects)
            {
                if (effect.IsRunning)
                    visibleEffects++;

                effect.Update();
            }*/

            if (animationPlaying)
                HWAnimation.Update();

            //Only update render if it is needed
            if (visibleNavLights > 0/* || visibleEffects > 0*/)
            {
                if (Program.ElapsedSeconds > 1)
                    HWNavLight.Reset(); //Prevents death-flickering on navlights

                Renderer.Invalidate();
            }
            /*if (visibleEffects > 0)
                Renderer.InvalidateMeshData();*/

            //Rainbow.Update();
        }

        public void glControl_Render(object sender, PaintEventArgs e)
        {
            if (!Loaded)
                return;

            FPSCounter.Update();
            Renderer.Render();
        }

        public void glControl_Resize(object sender, EventArgs e)
        {
            if (!Loaded)
                return;

            Renderer.Resize();
            Renderer.InvalidateView();
            Renderer.Invalidate();
        }

        private void Clear()
        {
            comboJointParent.Items.Clear();

            comboMarkerParent.Items.Clear();

            listShipMeshes.Items.Clear();
            comboShipMeshParent.Items.Clear();
            checkShipMeshDoScar.Checked = false;
            listShipMeshLODs.Items.Clear();
            ShipMeshListItems.Clear();
            boxShipMeshName.Clear();
            boxShipMeshName.Enabled = false;
            selectedShipMesh = null;

            foreach (Label label in ShipMeshLODMaterialLabels)
                label.Visible = false;

            foreach (ComboBox comboBox in ShipMeshLODMaterialComboBoxes)
            {
                comboBox.Items.Clear();
                comboBox.Visible = false;
            }

            listEngineGlows.Items.Clear();
            comboEngineGlowParent.Items.Clear();
            EngineGlowListItems.Clear();
            selectedEngineGlow = null;

            listCollisionMeshes.Items.Clear();
            comboCollisionMeshParent.Items.Clear();
            listCollisionMeshes_SelectedIndexChanged(this, EventArgs.Empty);

            listEngineShapes.Items.Clear();
            comboEngineShapeParent.Items.Clear();

            listMaterials.Items.Clear();
            MaterialNames.Clear();
            listMaterialTextures.Items.Clear();
            comboMaterialFormat.Items.Clear();
            boxMaterialName.Enabled = false;
            boxMaterialName.Clear();
            selectedMaterial = null;

            jointsTree.Nodes.Clear();

            listMarkers.Items.Clear();
            checkDrawMarkers.Checked = false;

            //Dockpaths
            dockpathList.Items.Clear();
            listDockpathFamilies.Items.Clear();
            listDockpathLinks.Items.Clear();
            checkDockpathExit.Checked = false;
            checkDockpathLatch.Checked = false;
            checkDockpathAnim.Checked = false;
            checkDockpathAjar.Checked = false;
            trackBarDockpathSegments.Enabled = false;
            trackBarDockpathSegments.Value = 0;
            trackBarDockpathSegments.Maximum = 1;
            boxDockpathSegmentTolerance.Clear();
            boxDockpathSegmentSpeed.Clear();
            checkDockpathSegmentFlagUseRot.Checked = false;
            checkDockpathSegmentFlagPlayer.Checked = false;
            checkDockpathSegmentFlagQueue.Checked = false;
            checkDockpathSegmentFlagClose.Checked = false;
            checkDockpathSegmentFlagClearRes.Checked = false;
            checkDockpathSegmentFlagCheck.Checked = false;
            checkDockpathSegmentFlagUnfocus.Checked = false;
            checkDockpathSegmentFlagClip.Checked = false;
            selectedDockpath = null;

            //Navlights
            listNavLights.Items.Clear();
            comboNavLightParent.Items.Clear();
            selectedNavLight = null;

            //Engine burns
            listEngineBurns.Items.Clear();
            boxEngineBurnName.Clear();
            comboEngineBurnParent.Items.Clear();
            trackBarEngineBurnFlames.Enabled = false;
            trackBarEngineBurnFlames.Value = 0;
            trackBarEngineBurnFlames.Maximum = 1;
            numericEngineBurnSpriteIndex.Value = 0;
            numericEngineBurnSpriteIndex.Enabled = false;

            //Animations
            AnimationNames.Clear();
            listAnimations.Items.Clear();
            selectedAnimation = null;
            AnimationPlaying = false;
            listAnimations_SelectedIndexChanged(this, EventArgs.Empty);

            foreach (HWDockSegment segment in HWDockSegment.DockSegments)
            {
                segment.Icosphere.Color = new Vector3(1, 0, 0);
            }

            foreach (HWEngineFlame flame in HWEngineFlame.EngineFlames)
            {
                flame.Cube.Color = new Vector3(1, 1, 1);
            }

            EditorScene.Clear();
            HWScene.Clear();

            comboMaterialFormat.Items.Add("DXT1");
            comboMaterialFormat.Items.Add("DXT3");
            comboMaterialFormat.Items.Add("DXT5");
            comboMaterialFormat.Items.Add("8888");

            problemsVisible = false;
            splitContainer2.Panel2Collapsed = true;
            Problem.Problems.Clear();
            gridProblems.Rows.Clear();

            this.Text = "DAEnerys";

            jointsTree_AfterSelect(this, new TreeViewEventArgs(null));
            listMaterials_SelectedIndexChanged(this, EventArgs.Empty);
            listShipMeshes_SelectedIndexChanged(this, EventArgs.Empty);
            listEngineGlows_SelectedIndexChanged(this, EventArgs.Empty);
            listNavLights_SelectedIndexChanged(this, EventArgs.Empty);
            listMarkers_SelectedIndexChanged(this, EventArgs.Empty);

            Renderer.InvalidateMeshData();
            Renderer.InvalidateView();
            Renderer.Invalidate();
        }

        //--------------------------------------------------------------------------------------------------------------//
        //-------------------------------------------------- GUI STUFF -------------------------------------------------//
        //--------------------------------------------------------------------------------------------------------------//
        private void buttonNew_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure that you want to clear the scene?\nAll unsaved changes will be lost forever.", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result != DialogResult.Yes)
                return;

            Clear();

            HWScene.CalibrateSettings();

            this.Text = "DAEnerys";

            Renderer.InvalidateMeshData();
            Renderer.InvalidateView();
            Renderer.Invalidate();
        }
        private void buttonOpen_Click(object sender, EventArgs e)
        {
            DialogResult result = openColladaDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                Clear();
                Importer.ImportFromFile(openColladaDialog.FileName);
                this.Text = openColladaDialog.FileName + " - DAEnerys";
                OpenedFile = Path.GetFileNameWithoutExtension(openColladaDialog.FileName);

                Renderer.InvalidateMeshData();
                Renderer.InvalidateView();
                Renderer.Invalidate();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            DialogResult result = saveColladaDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                HWScene.SaveCollada(saveColladaDialog.FileName);
                this.Text = saveColladaDialog.FileName + " - DAEnerys";
                OpenedFile = Path.GetFileNameWithoutExtension(saveColladaDialog.FileName);
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Idle -= glControl_Update;
            HWTexture.Close();
            GraphicsContext.CurrentContext.Dispose();
            Settings.SaveSettings();
            Hotkeys.SaveHotkeys();
            Log.Close();
        }

        //--------------------------------- JOINTS ---------------------------------//
        public void AddJoint(HWJoint joint, HWJoint parent)
        {
            TreeNode newNode = new TreeNode(joint.Name);

            if (parent == null) //If root joint
                jointsTree.Nodes.Add(newNode);
            else
                parent.TreeNode.Nodes.Add(newNode);

            joint.TreeNode = newNode;

            jointsTree.Sort();

            AddJointToCombos(joint);
        }
        public void RemoveJoint(HWJoint joint)
        {
            List<HWElement> jointChildren = new List<HWElement>();
            foreach (Element child in joint.Children)
            {
                HWJoint childJoint = child as HWJoint;
                if (childJoint == null)
                    continue;
                childJoint.TreeNode.Remove();
                AddJoint(childJoint, (HWJoint)joint.Parent);
            }

            jointsTree.Nodes.Remove(joint.TreeNode);

            object item = joint.Name;

            RemoveJointFromCombos(joint);

            jointsTree.Sort();
        }
        private void AddJointToCombos(HWJoint joint, bool addToJointParents = true)
        {
            if(addToJointParents)
                comboJointParent.Items.Add(joint.Name);
            comboShipMeshParent.Items.Add(joint.Name);
            comboCollisionMeshParent.Items.Add(joint.Name);
            comboEngineGlowParent.Items.Add(joint.Name);
            comboEngineShapeParent.Items.Add(joint.Name);
            comboEngineBurnParent.Items.Add(joint.Name);
            comboNavLightParent.Items.Add(joint.Name);
            comboMarkerParent.Items.Add(joint.Name);
        }
        private void RemoveJointFromCombos(HWJoint joint)
        {
            comboJointParent.Items.Remove(joint.Name);
            comboShipMeshParent.Items.Remove(joint.Name);
            comboCollisionMeshParent.Items.Remove(joint.Name);
            comboEngineGlowParent.Items.Remove(joint.Name);
            comboEngineShapeParent.Items.Remove(joint.Name);
            comboEngineBurnParent.Items.Remove(joint.Name);
            comboNavLightParent.Items.Remove(joint.Name);
            comboMarkerParent.Items.Remove(joint.Name);
        }
        private void SetJointParent(HWJoint joint, HWJoint newParent)
        {
            joint.TreeNode.Parent.Nodes.Remove(joint.TreeNode);
            newParent.TreeNode.Nodes.Add(joint.TreeNode);

            jointsTree.Sort();
        }
        private void AddJointToJointParentComboRecursive(HWJoint joint)
        {
            foreach (HWElement child in joint.Children)
            {
                HWJoint childJoint = child as HWJoint;
                if (childJoint != null)
                    AddJointToJointParentComboRecursive(childJoint);
            }

            comboJointParent.Items.Add(joint.Name);
        }
        private void RemoveJointFromJointParentComboRecursive(HWJoint joint)
        {
            foreach(Element child in joint.Children)
            {
                HWElement hwChild = child as HWElement;

                if (hwChild == null)
                    continue;

                HWJoint childJoint = hwChild as HWJoint;
                if (childJoint == null)
                    continue;

                RemoveJointFromJointParentComboRecursive(childJoint);
            }

            comboJointParent.Items.Remove(joint.Name);
        }
        private void jointsTree_AfterCheck(object sender, TreeViewEventArgs e)
        {
            bool newValue = e.Node.Checked;

            //TODO: Optimize
            foreach (HWJoint joint in HWJoint.Joints)
            {
                if (joint.TreeNode == e.Node)
                {
                    joint.EditorJoint.Visible = newValue;
                    break;
                }
            }

            Renderer.InvalidateView();
            Renderer.Invalidate();
        }
        private void jointsTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (ignoreJointSelection)
                return;

            comboJointParent.Items.Clear();

            selectedJoint = null;

            ignoreJointValuesChanged = true;

            buttonJointRemove.Enabled = false;
            boxJointName.Clear();
            boxJointName.Enabled = false;
            comboJointParent.SelectedItem = "";
            comboJointParent.Enabled = false;

            numericJointPositionX.Value = 0;
            numericJointPositionX.Enabled = false;
            numericJointPositionY.Value = 0;
            numericJointPositionY.Enabled = false;
            numericJointPositionZ.Value = 0;
            numericJointPositionZ.Enabled = false;

            numericJointRotationX.Value = 0;
            numericJointRotationX.Enabled = false;
            numericJointRotationY.Value = 0;
            numericJointRotationY.Enabled = false;
            numericJointRotationZ.Value = 0;
            numericJointRotationZ.Enabled = false;

            foreach (HWJoint joint in HWJoint.Joints)
            {
                if (joint.TreeNode == e.Node)
                {
                    selectedJoint = joint;
                    break;
                }
            }

            ignoreJointValuesChanged = false;

            if (selectedJoint == null)
                return;

            ignoreJointValuesChanged = true;

            if (selectedJoint != HWJoint.Root)
            {
                buttonJointRemove.Enabled = true;
                boxJointName.Enabled = true;
                comboJointParent.Enabled = true;
            }

            boxJointName.Text = selectedJoint.Name;
            
            foreach(HWJoint joint in HWJoint.Joints)
            {
                comboJointParent.Items.Add(joint.Name);
            }

            RemoveJointFromJointParentComboRecursive(selectedJoint);

            if (selectedJoint.Parent != null)
            {
                HWJoint parentJoint = selectedJoint.Parent as HWJoint;
                comboJointParent.SelectedItem = parentJoint.Name;
            }

            numericJointPositionX.Enabled = true;
            numericJointPositionX.Value = (decimal)selectedJoint.LocalPosition.X;
            numericJointPositionY.Enabled = true;
            numericJointPositionY.Value = (decimal)selectedJoint.LocalPosition.Y;
            numericJointPositionZ.Enabled = true;
            numericJointPositionZ.Value = (decimal)selectedJoint.LocalPosition.Z;

            Vector3 eulerAngles = selectedJoint.LocalRotation;
            numericJointRotationX.Enabled = true;
            numericJointRotationX.Value = (decimal)MathHelper.RadiansToDegrees(eulerAngles.X);
            numericJointRotationY.Enabled = true;
            numericJointRotationY.Value = (decimal)MathHelper.RadiansToDegrees(eulerAngles.Y);
            numericJointRotationZ.Enabled = true;
            numericJointRotationZ.Value = (decimal)MathHelper.RadiansToDegrees(eulerAngles.Z);

            ignoreJointValuesChanged = false;
        }
        private void buttonJointRemove_Click(object sender, EventArgs e)
        {
            if (selectedJoint == null)
                return;

            selectedJoint.Destroy();
        }
        private void buttonJointAdd_Click(object sender, EventArgs e)
        {
            HWJoint parent = selectedJoint;
            if (selectedJoint == null)
                parent = HWJoint.Root;

            int indexOffset = 1;
            string newName = "Joint" + (HWJoint.Joints.Count + indexOffset);
            while (HWJoint.GetByName(newName) != null)
            {
                indexOffset++;
                newName = "Joint" + (HWJoint.Joints.Count + indexOffset);
            }

            HWJoint newJoint = new HWJoint(newName, parent);
            jointsTree.SelectedNode = newJoint.TreeNode;
            newJoint.TreeNode.EnsureVisible();
            jointsTree.Focus();
        }
        private void boxJointName_Leave(object sender, EventArgs e)
        {
            if (selectedJoint == null)
                return;

            UpdateJointName(selectedJoint, boxJointName.Text);
        }
        private void boxJointName_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Return)
                return;

            if (selectedJoint == null)
                return;

            UpdateJointName(selectedJoint, boxJointName.Text);
        }
        private void UpdateJointName(HWJoint joint, string newName)
        {
            if (HWJoint.GetByName(joint.Name) == null)
                return;

            //Joint with this name already exists
            if (HWJoint.GetByName(newName) != null)
            {
                HWJoint existingJoint = HWJoint.GetByName(newName);
                if (existingJoint != joint)
                {
                    MessageBox.Show("A joint with this name already exists.", "Error while changing joint name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    boxJointName.Text = joint.Name;
                    boxJointName.Focus();
                    return;
                }
            }

            RemoveJointFromCombos(joint);
            joint.Name = boxJointName.Text;
            joint.TreeNode.Text = joint.Name;
            AddJointToCombos(joint, false);
        }
        private void numericJointPosition_ValueChanged(object sender, EventArgs e)
        {
            if (selectedJoint == null)
                return;

            if (ignoreJointValuesChanged)
                return;

            selectedJoint.LocalPosition = new Vector3((float)numericJointPositionX.Value, (float)numericJointPositionY.Value, (float)numericJointPositionZ.Value);
        }
        private void numericJointRotation_ValueChanged(object sender, EventArgs e)
        {
            if (selectedJoint == null)
                return;

            if (ignoreJointValuesChanged)
                return;

            float x = MathHelper.DegreesToRadians((float)numericJointRotationX.Value);
            float y = MathHelper.DegreesToRadians((float)numericJointRotationY.Value);
            float z = MathHelper.DegreesToRadians((float)numericJointRotationZ.Value);
            Vector3 eulerAngles = new Vector3(x, y, z);

            selectedJoint.LocalRotation = eulerAngles;
        }
        private void comboJointParent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectedJoint == null)
                return;

            if (ignoreJointValuesChanged)
                return;

            ignoreJointSelection = true;

            HWJoint newParent = HWJoint.GetByName((string)comboJointParent.SelectedItem);

            selectedJoint.Parent = newParent;
            SetJointParent(selectedJoint, newParent);

            ignoreJointSelection = false;

            jointsTree.SelectedNode = selectedJoint.TreeNode;
            selectedJoint.TreeNode.EnsureVisible();
            jointsTree.Focus();
        }
        private bool IsJointDescendantOf(HWJoint joint, HWJoint parent)
        {
            HWJoint jointChecking = (HWJoint)joint.Parent;
            while (jointChecking != null)
            {
                if (jointChecking == parent)
                    return true;

                jointChecking = (HWJoint)jointChecking.Parent;
            }

            return false;
        }
        private void jointsTree_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Move);
        }
        private void jointsTree_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }
        private void jointsTree_DragDrop(object sender, DragEventArgs e)
        {
            TreeNode NewNode;

            if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", false))
            {
                Point pt = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
                TreeNode DestinationNode = ((TreeView)sender).GetNodeAt(pt);

                if (DestinationNode == null)
                {
                    SystemSounds.Beep.Play();
                    return;
                }

                NewNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");

                if (NewNode == DestinationNode)
                {
                    SystemSounds.Beep.Play();
                    return;
                }

                HWJoint draggedJoint = HWJoint.GetByName(NewNode.Text);
                HWJoint newParent = HWJoint.GetByName((string)DestinationNode.Text);

                if (IsJointDescendantOf(newParent, draggedJoint))
                {
                    SystemSounds.Beep.Play();
                    return;
                }

                ignoreJointSelection = true;

                draggedJoint.Parent = newParent;
                SetJointParent(draggedJoint, newParent);

                ignoreJointSelection = false;

                jointsTree.SelectedNode = draggedJoint.TreeNode;
                draggedJoint.TreeNode.EnsureVisible();
                jointsTree.Focus();
            }
        }

        //--------------------------------- DOCKPATHS ---------------------------------//
        public void AddDockpath(HWDockpath dockpath)
        {
            dockpathList.Items.Add(dockpath.Name);
        }
        private void dockpathList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            bool newValue = false;

            if (e.NewValue == CheckState.Checked)
                newValue = true;

            foreach (HWDockSegment segment in HWDockSegment.DockSegments)
            {
                segment.ToleranceIcosphere.Visible = false;
            }

            foreach (HWDockpath dockpath in HWDockpath.Dockpaths)
            {
                if (dockpath.Name == dockpathList.Items[e.Index].ToString())
                    dockpath.Visible = newValue;
            }

            trackBarDockpathSegments_Scroll(null, EventArgs.Empty);

            Renderer.InvalidateView();
            Renderer.Invalidate();
        }
        private void dockpathList_SelectedIndexChanged(object sender, EventArgs e)
        {
            listDockpathFamilies.Items.Clear();
            listDockpathLinks.Items.Clear();
            checkDockpathExit.Checked = false;
            checkDockpathLatch.Checked = false;
            checkDockpathAnim.Checked = false;
            checkDockpathAjar.Checked = false;

            trackBarDockpathSegments.Enabled = true;
            trackBarDockpathSegments.Value = 0;
            trackBarDockpathSegments.Maximum = 1;
            boxDockpathSegmentTolerance.Clear();
            boxDockpathSegmentSpeed.Clear();
            boxDockpathName.Clear();
            numericDockpathAnimationIndex.Value = 0;
            checkDockpathSegmentFlagUseRot.Checked = false;
            checkDockpathSegmentFlagPlayer.Checked = false;
            checkDockpathSegmentFlagQueue.Checked = false;
            checkDockpathSegmentFlagClose.Checked = false;
            checkDockpathSegmentFlagClearRes.Checked = false;
            checkDockpathSegmentFlagCheck.Checked = false;
            checkDockpathSegmentFlagUnfocus.Checked = false;
            checkDockpathSegmentFlagClip.Checked = false;

            foreach (HWDockSegment segment in HWDockSegment.DockSegments)
                segment.ToleranceIcosphere.Visible = false;

            HWDockpath dockpath = null;
            foreach (HWDockpath path in HWDockpath.Dockpaths)
            {
                if (path.Name == dockpathList.SelectedItem.ToString())
                {
                    dockpath = path;
                    break;
                }
            }

            if (dockpath != null)
            {
                foreach (string family in dockpath.Families)
                {
                    listDockpathFamilies.Items.Add(family);
                }

                foreach (string link in dockpath.Links)
                {
                    listDockpathLinks.Items.Add(link);
                }

                foreach (DockpathFlag flag in dockpath.Flags)
                {
                    switch (flag)
                    {
                        case DockpathFlag.Exit:
                            checkDockpathExit.Checked = true;
                            break;
                        case DockpathFlag.Latch:
                            checkDockpathLatch.Checked = true;
                            break;
                        case DockpathFlag.Anim:
                            checkDockpathAnim.Checked = true;
                            break;
                        case DockpathFlag.Ajar:
                            checkDockpathAjar.Checked = true;
                            break;
                    }
                }

                boxDockpathName.Text = dockpath.Name;
                numericDockpathAnimationIndex.Value = dockpath.AnimationIndex;

                selectedDockpath = dockpath;
            }

            trackBarDockpathSegments.Maximum = dockpath.Segments.Count - 1;
            trackBarDockpathSegments_Scroll(null, EventArgs.Empty);
        }
        private void trackBarDockpathSegments_Scroll(object sender, EventArgs e)
        {
            if (selectedDockpath == null)
                return;

            //Reset segment colors
            foreach (HWDockSegment segment in HWDockSegment.DockSegments)
            {
                segment.Icosphere.Color = new Vector3(1, 0, 0);
                segment.ToleranceIcosphere.Visible = false;
            }

            //Reset line colors
            foreach (EditorLine line in selectedDockpath.Lines)
            {
                line.StartColor = new Vector3(1, 0, 0);
                line.EndColor = new Vector3(1, 0, 0);
            }

            HWDockSegment selectedSegment = selectedDockpath.Segments[trackBarDockpathSegments.Value];

            if (selectedDockpath.Visible)
            {
                selectedSegment.Icosphere.Color = new Vector3(1, 1, 0);
                selectedSegment.ToleranceIcosphere.Visible = true;
            }

            /* if(selectedSegment.ID < selectedDockpath.Lines.Count)
             selectedDockpath.Lines[selectedSegment.ID].StartColor = Color.Yellow;

             if(selectedSegment.ID > 0)
                 selectedDockpath.Lines[selectedSegment.ID - 1].EndColor = Color.Yellow;*/

            boxDockpathSegmentTolerance.Text = selectedSegment.Tolerance.ToString();
            boxDockpathSegmentSpeed.Text = selectedSegment.Speed.ToString();

            checkDockpathSegmentFlagUseRot.Checked = false;
            checkDockpathSegmentFlagPlayer.Checked = false;
            checkDockpathSegmentFlagQueue.Checked = false;
            checkDockpathSegmentFlagClose.Checked = false;
            checkDockpathSegmentFlagClearRes.Checked = false;
            checkDockpathSegmentFlagCheck.Checked = false;
            checkDockpathSegmentFlagUnfocus.Checked = false;
            checkDockpathSegmentFlagClip.Checked = false;

            foreach (DockSegmentFlag flag in selectedSegment.Flags)
            {
                switch (flag)
                {
                    case DockSegmentFlag.UseRot:
                        checkDockpathSegmentFlagUseRot.Checked = true;
                        break;
                    case DockSegmentFlag.Player:
                        checkDockpathSegmentFlagPlayer.Checked = true;
                        break;
                    case DockSegmentFlag.Queue:
                        checkDockpathSegmentFlagQueue.Checked = true;
                        break;
                    case DockSegmentFlag.Close:
                        checkDockpathSegmentFlagClose.Checked = true;
                        break;
                    case DockSegmentFlag.ClearRes:
                        checkDockpathSegmentFlagClearRes.Checked = true;
                        break;
                    case DockSegmentFlag.Check:
                        checkDockpathSegmentFlagCheck.Checked = true;
                        break;
                    case DockSegmentFlag.UnFocus:
                        checkDockpathSegmentFlagUnfocus.Checked = true;
                        break;
                    case DockSegmentFlag.Clip:
                        checkDockpathSegmentFlagClip.Checked = true;
                        break;
                }
            }

            Renderer.InvalidateMeshData();
            Renderer.InvalidateView();
            Renderer.Invalidate();
        }

        //--------------------------------- NAVLIGHTS ---------------------------------//
        public void AddNavLight(HWNavLight navLight)
        {
            listNavLights.Items.Add(navLight.Name);
            navLight.NavLightListItemIndex = listNavLights.Items.Count - 1;
        }
        public void RemoveNavLight(HWNavLight navLight)
        {
            listNavLights.Items.Remove(navLight.Name);
        }
        private void listNavLights_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            bool newValue = false;

            if (e.NewValue == CheckState.Checked)
                newValue = true;

            foreach (HWNavLight navLight in HWNavLight.NavLights)
            {
                if (navLight.Name == listNavLights.Items[e.Index].ToString())
                {
                    navLight.Visible = newValue;
                }
            }

            Renderer.InvalidateView();
            Renderer.Invalidate();
        }
        private void listNavLights_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreNavLightListSelectedIndexChanged)
                return;

            selectedNavLight = null;

            boxNavLightName.Clear();
            comboNavLightParent.SelectedIndex = 0;
            comboNavLightType.SelectedItem = null;
            numericNavLightSize.Value = 0;
            numericNavLightPhase.Value = 0;
            numericNavLightFrequency.Value = 0;
            buttonNavLightColor.BackColor = Color.White;
            numericNavLightDistance.Value = 0;

            numericNavLightPositionX.Enabled = false;
            numericNavLightPositionY.Enabled = false;
            numericNavLightPositionZ.Enabled = false;

            comboNavLightParent.Enabled = false;
            boxNavLightName.Enabled = false;
            buttonNavLightRemove.Enabled = false;
            comboNavLightType.Enabled = false;
            numericNavLightSize.Enabled = false;
            numericNavLightPhase.Enabled = false;
            numericNavLightFrequency.Enabled = false;
            buttonNavLightColor.Enabled = false;
            numericNavLightDistance.Enabled = false;
            checkNavLightFlagSprite.Enabled = false;
            checkNavLightFlagHighEnd.Enabled = false;

            checkNavLightFlagSprite.Checked = false;
            checkNavLightFlagHighEnd.Checked = false;

            if (listNavLights.SelectedItem != null)
            {
                foreach (HWNavLight light in HWNavLight.NavLights)
                {
                    if (light.Name == listNavLights.SelectedItem.ToString())
                    {
                        selectedNavLight = light;
                        break;
                    }
                }
            }

            if (selectedNavLight != null)
            {
                numericNavLightPositionX.Enabled = true;
                numericNavLightPositionY.Enabled = true;
                numericNavLightPositionZ.Enabled = true;

                comboNavLightParent.Enabled = true;
                boxNavLightName.Enabled = true;
                buttonNavLightRemove.Enabled = true;
                comboNavLightType.Enabled = true;
                numericNavLightSize.Enabled = true;
                numericNavLightPhase.Enabled = true;
                numericNavLightFrequency.Enabled = true;
                buttonNavLightColor.Enabled = true;
                numericNavLightDistance.Enabled = true;
                checkNavLightFlagSprite.Enabled = true;
                checkNavLightFlagHighEnd.Enabled = true;

                ignoreNavLightValuesChanged = true;
                numericNavLightPositionX.Value = (decimal)selectedNavLight.LocalPosition.X;
                numericNavLightPositionY.Value = (decimal)selectedNavLight.LocalPosition.Y;
                numericNavLightPositionZ.Value = (decimal)selectedNavLight.LocalPosition.Z;

                boxNavLightName.Text = selectedNavLight.Name;

                HWJoint parentJoint = selectedNavLight.Parent as HWJoint;
                comboNavLightParent.SelectedItem = parentJoint.Name;
                comboNavLightType.SelectedItem = selectedNavLight.Style.Name;
                numericNavLightSize.Value = (decimal)selectedNavLight.Size;
                numericNavLightPhase.Value = (decimal)selectedNavLight.Phase;
                numericNavLightFrequency.Value = (decimal)selectedNavLight.Frequency;

                int red = (int)Math.Round((float)(selectedNavLight.Color.X * 255));
                int green = (int)Math.Round((float)(selectedNavLight.Color.Y * 255));
                int blue = (int)Math.Round((float)(selectedNavLight.Color.Z * 255));
                red = Math.Min(red, 255);
                green = Math.Min(green, 255);
                blue = Math.Min(blue, 255);
                buttonNavLightColor.BackColor = Color.FromArgb(255, red, green, blue);

                numericNavLightDistance.Value = (decimal)selectedNavLight.Distance;

                foreach (NavLightFlag flag in selectedNavLight.Flags)
                {
                    switch (flag)
                    {
                        case NavLightFlag.Sprite:
                            checkNavLightFlagSprite.Checked = true;
                            break;
                        case NavLightFlag.HighEnd:
                            checkNavLightFlagHighEnd.Checked = true;
                            break;
                    }
                }

                ignoreNavLightValuesChanged = false;
            }
        }
        public void AddNavLightStyle(HWNavLightStyle navLightStyle)
        {
            comboNavLightType.Items.Add(navLightStyle.Name);
        }
        public void CheckNavLightVisible(HWNavLight navLight, bool visible)
        {
            listNavLights.SetItemChecked(navLight.NavLightListItemIndex, visible);
        }
        private void checkNavLightDrawRadius_CheckedChanged(object sender, EventArgs e)
        {
            DrawNavLightRadius = checkNavLightDrawRadius.Checked;

            if (DrawNavLightRadius)
            {
                foreach (HWNavLight navLight in HWNavLight.NavLights)
                {
                    if (navLight.Visible)
                        if (navLight.RenderIcosphere != null)
                            navLight.RenderIcosphere.Visible = true;
                }
            }
            else
            {
                foreach (HWNavLight navLight in HWNavLight.NavLights)
                {
                    if (navLight.RenderIcosphere != null)
                        navLight.RenderIcosphere.Visible = false;
                }
            }

            Renderer.InvalidateView();
            Renderer.Invalidate();
        }
        private void numericNavLightPosition_ValueChanged(object sender, EventArgs e)
        {
            if (selectedNavLight == null)
                return;

            if (ignoreNavLightValuesChanged)
                return;

            selectedNavLight.LocalPosition = new Vector3((float)numericNavLightPositionX.Value, (float)numericNavLightPositionY.Value, (float)numericNavLightPositionZ.Value);
        }
        private void boxNavLightName_Leave(object sender, EventArgs e)
        {
            if (selectedNavLight == null)
                return;

            UpdateNavLightName(selectedNavLight, boxNavLightName.Text);
        }
        private void boxNavLightName_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Return)
                return;

            if (selectedNavLight == null)
                return;

            UpdateNavLightName(selectedNavLight, boxNavLightName.Text);
        }
        private void UpdateNavLightName(HWNavLight navLight, string newName)
        {
            if (!listNavLights.Items.Contains(navLight.Name))
                return;

            //Ship mesh with this name already exists
            if (listNavLights.Items.Contains(newName))
            {
                HWNavLight existingNavLight = HWNavLight.GetByName(newName);
                if (existingNavLight != navLight)
                {
                    MessageBox.Show("A navlight with this name already exists.", "Error while changing navlight name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    boxNavLightName.Text = navLight.Name;
                    boxNavLightName.Focus();
                    return;
                }
            }

            ignoreNavLightListSelectedIndexChanged = true;
            int index = listNavLights.Items.IndexOf(navLight.Name);
            listNavLights.Items.Remove(navLight.Name);
            listNavLights.Items.Remove(navLight.Name);
            navLight.Name = boxNavLightName.Text;
            listNavLights.Items.Insert(index, navLight.Name);
            listNavLights.SelectedItem = navLight.Name;
            CheckNavLightVisible(navLight, navLight.Visible);
            ignoreNavLightListSelectedIndexChanged = false;
        }
        private void buttonNavLightRemove_Click(object sender, EventArgs e)
        {
            if (selectedNavLight == null)
                return;

            selectedNavLight.Destroy();
            listNavLights.ClearSelected();
            listNavLights_SelectedIndexChanged(this, EventArgs.Empty);
        }
        private void buttonNavLightAdd_Click(object sender, EventArgs e)
        {
            int indexOffset = 1;
            string newName = "NavLight" + (listNavLights.Items.Count + indexOffset);
            while (listNavLights.Items.Contains(newName))
            {
                indexOffset++;
                newName = "NavLight" + (listNavLights.Items.Count + indexOffset);
            }

            HWNavLightStyle style = null;
            if (HWData.NavLightStyles.Count > 0)
                style = HWData.NavLightStyles[0];

            HWNavLight newNavLight = new HWNavLight(newName, HWJoint.Root, Vector3.Zero, style, 1, 0, 1, Vector3.One, 5, new List<NavLightFlag>(), 0);

            listNavLights.SelectedItem = newNavLight.Name;
        }
        private void comboNavLightParent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectedNavLight == null)
                return;

            if (ignoreNavLightValuesChanged)
                return;

            HWJoint newParent = HWJoint.GetByName((string)comboNavLightParent.SelectedItem);
            selectedNavLight.Parent = newParent;
        }
        private void comboNavLightType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectedNavLight == null)
                return;

            if (ignoreNavLightValuesChanged)
                return;

            selectedNavLight.Style = HWNavLightStyle.GetByName((string)comboNavLightType.SelectedItem);
        }
        private void numericNavLightSize_ValueChanged(object sender, EventArgs e)
        {
            if (selectedNavLight == null)
                return;

            if (ignoreNavLightValuesChanged)
                return;

            selectedNavLight.Size = (float)numericNavLightSize.Value;
        }
        private void numericNavLightPhase_ValueChanged(object sender, EventArgs e)
        {
            if (selectedNavLight == null)
                return;

            if (ignoreNavLightValuesChanged)
                return;

            selectedNavLight.Phase = (float)numericNavLightPhase.Value;
        }
        private void numericNavLightFrequency_ValueChanged(object sender, EventArgs e)
        {
            if (selectedNavLight == null)
                return;

            if (ignoreNavLightValuesChanged)
                return;

            selectedNavLight.Frequency = (float)numericNavLightFrequency.Value;
        }
        private void buttonNavLightColor_Click(object sender, EventArgs e)
        {
            if (selectedNavLight == null)
                return;

            if (ignoreNavLightValuesChanged)
                return;

            colorDialog.Color = buttonNavLightColor.BackColor;
            DialogResult result = colorDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                selectedNavLight.Color = new Vector3((float)colorDialog.Color.R / 255, (float)colorDialog.Color.G / 255, (float)colorDialog.Color.B / 255);
                buttonNavLightColor.BackColor = Color.FromArgb((int)Math.Round(selectedNavLight.Color.X * 255), (int)Math.Round(selectedNavLight.Color.Y * 255), (int)Math.Round(selectedNavLight.Color.Z * 255));
            }
        }
        private void numericNavLightDistance_ValueChanged(object sender, EventArgs e)
        {
            if (selectedNavLight == null)
                return;

            if (ignoreNavLightValuesChanged)
                return;

            selectedNavLight.Distance = (float)numericNavLightDistance.Value;
        }
        private void checkNavLightFlagSprite_CheckedChanged(object sender, EventArgs e)
        {
            if (selectedNavLight == null)
                return;

            if (ignoreNavLightValuesChanged)
                return;

            List<NavLightFlag> flags = selectedNavLight.Flags;
            flags.Remove(NavLightFlag.Sprite);

            if(checkNavLightFlagSprite.Checked)
                flags.Add(NavLightFlag.Sprite);

            selectedNavLight.Flags = flags;
        }
        private void checkNavLightFlagHighEnd_CheckedChanged(object sender, EventArgs e)
        {
            if (selectedNavLight == null)
                return;

            if (ignoreNavLightValuesChanged)
                return;

            List<NavLightFlag> flags = selectedNavLight.Flags;
            flags.Remove(NavLightFlag.HighEnd);

            if (checkNavLightFlagHighEnd.Checked)
                flags.Add(NavLightFlag.HighEnd);

            selectedNavLight.Flags = flags;
        }

        //--------------------------------- MARKERS ---------------------------------//
        public void AddMarker(HWMarker marker)
        {
            listMarkers.Items.Add(marker.Name);
        }
        public void RemoveMarker(HWMarker marker)
        {
            listMarkers.Items.Remove(marker.Name);
        }
        private void checkDrawMarkers_CheckedChanged(object sender, EventArgs e)
        {
            HWMarker.DisplayMarkers = checkDrawMarkers.Checked;
        }
        private void listMarkers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreMarkerListSelectedIndexChanged)
                return;

            if (selectedMarker != null)
                selectedMarker.EditorMarker.Color = new Vector3(1, 1, 0);

            ignoreMarkerValuesChanged = true;

            buttonMarkerRemove.Enabled = false;
            boxMarkerName.Enabled = false;
            comboMarkerParent.Enabled = false;

            numericMarkerPositionX.Enabled = false;
            numericMarkerPositionY.Enabled = false;
            numericMarkerPositionZ.Enabled = false;

            numericMarkerRotationX.Enabled = false;
            numericMarkerRotationY.Enabled = false;
            numericMarkerRotationZ.Enabled = false;

            boxMarkerName.Clear();
            comboMarkerParent.SelectedItem = "";

            numericMarkerPositionX.Value = 0;
            numericMarkerPositionY.Value = 0;
            numericMarkerPositionZ.Value = 0;

            numericMarkerRotationX.Value = 0;
            numericMarkerRotationY.Value = 0;
            numericMarkerRotationZ.Value = 0;

            ignoreMarkerValuesChanged = false;

            selectedMarker = HWMarker.GetByName((string)listMarkers.SelectedItem);
            if (selectedMarker == null)
                return;

            selectedMarker.EditorMarker.Color = new Vector3(1, 0, 0);

            ignoreMarkerValuesChanged = true;

            buttonMarkerRemove.Enabled = true;
            boxMarkerName.Enabled = true;
            comboMarkerParent.Enabled = true;

            numericMarkerPositionX.Enabled = true;
            numericMarkerPositionY.Enabled = true;
            numericMarkerPositionZ.Enabled = true;

            numericMarkerRotationX.Enabled = true;
            numericMarkerRotationY.Enabled = true;
            numericMarkerRotationZ.Enabled = true;

            boxMarkerName.Text = selectedMarker.Name;

            HWJoint parentJoint = selectedMarker.Parent as HWJoint;
            if (parentJoint != null)
                comboMarkerParent.SelectedItem = parentJoint.Name;

            numericMarkerPositionX.Value = (decimal)selectedMarker.LocalPosition.X;
            numericMarkerPositionY.Value = (decimal)selectedMarker.LocalPosition.Y;
            numericMarkerPositionZ.Value = (decimal)selectedMarker.LocalPosition.Z;

            Vector3 eulerAngles = selectedMarker.LocalRotation;
            numericMarkerRotationX.Value = (decimal)MathHelper.RadiansToDegrees(eulerAngles.X);
            numericMarkerRotationY.Value = (decimal)MathHelper.RadiansToDegrees(eulerAngles.Y);
            numericMarkerRotationZ.Value = (decimal)MathHelper.RadiansToDegrees(eulerAngles.Z);

            ignoreMarkerValuesChanged = false;
        }
        private void buttonMarkerRemove_Click(object sender, EventArgs e)
        {
            if (selectedMarker == null)
                return;

            selectedMarker.Destroy();
            listMarkers.ClearSelected();
            listMarkers_SelectedIndexChanged(this, EventArgs.Empty);
        }
        private void buttonMarkerAdd_Click(object sender, EventArgs e)
        {
            int indexOffset = 1;
            string newName = "marker" + (listMarkers.Items.Count + indexOffset);
            while (listMarkers.Items.Contains(newName))
            {
                indexOffset++;
                newName = "marker" + (listMarkers.Items.Count + indexOffset);
            }

            HWMarker newMarker = new HWMarker(newName, HWJoint.Root);
            listMarkers.SelectedItem = newMarker.Name;
        }
        private void boxMarkerName_Leave(object sender, EventArgs e)
        {
            if (selectedMarker == null)
                return;

            UpdateMarkerName(selectedMarker, boxMarkerName.Text);
        }
        private void boxMarkerName_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Return)
                return;

            if (selectedMarker == null)
                return;

            UpdateMarkerName(selectedMarker, boxMarkerName.Text);
        }
        private void UpdateMarkerName(HWMarker marker, string newName)
        {
            if (!listMarkers.Items.Contains(marker.Name))
                return;

            //Marker with this name already exists
            if (listMarkers.Items.Contains(newName))
            {
                HWMarker existingMarker = HWMarker.GetByName(newName);
                if (existingMarker != marker)
                {
                    MessageBox.Show("A marker with this name already exists.", "Error while changing marker name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    boxMarkerName.Text = marker.Name;
                    boxMarkerName.Focus();
                    return;
                }
            }

            ignoreMarkerListSelectedIndexChanged = true;
            int index = listMarkers.Items.IndexOf(marker.Name);
            listMarkers.Items.Remove(marker.Name);
            marker.Name = boxMarkerName.Text;
            listMarkers.Items.Insert(index, marker.Name);
            listMarkers.SelectedItem = marker.Name;
            ignoreMarkerListSelectedIndexChanged = false;
        }
        private void numericMarkerPosition_ValueChanged(object sender, EventArgs e)
        {
            if (selectedMarker == null)
                return;

            if (ignoreMarkerValuesChanged)
                return;

            selectedMarker.LocalPosition = new Vector3((float)numericMarkerPositionX.Value, (float)numericMarkerPositionY.Value, (float)numericMarkerPositionZ.Value);
        }
        private void numericMarkerRotation_ValueChanged(object sender, EventArgs e)
        {
            if (selectedMarker == null)
                return;

            if (ignoreMarkerValuesChanged)
                return;

            float x = MathHelper.DegreesToRadians((float)numericMarkerRotationX.Value);
            float y = MathHelper.DegreesToRadians((float)numericMarkerRotationY.Value);
            float z = MathHelper.DegreesToRadians((float)numericMarkerRotationZ.Value);
            Vector3 eulerAngles = new Vector3(x, y, z);

            selectedMarker.LocalRotation = eulerAngles;
        }
        private void comboMarkerParent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectedMarker == null)
                return;

            if (ignoreMarkerValuesChanged)
                return;

            HWJoint newParent = HWJoint.GetByName((string)comboMarkerParent.SelectedItem);
            selectedMarker.Parent = newParent;
        }

        //--------------------------------- MISC ---------------------------------//

        public void glControl_MouseDown(object sender, MouseEventArgs e)
        {
            Program.Camera.MouseDown(e);
        }
        public void glControl_MouseUp(object sender, MouseEventArgs e)
        {
            Program.Camera.MouseUp(e);
        }
        public void glControl_KeyDown(object sender, KeyEventArgs e)
        {
            ActionKey.KeyDown(e);
            Program.Camera.KeyDown(e);
        }
        public void glControl_KeyUp(object sender, KeyEventArgs e)
        {
            ActionKey.KeyUp(e);
        }
        private void buttonSettings_Click(object sender, EventArgs e)
        {
            if (Program.settings != null)
                return;
            Program.settings = new Settings();
            Program.settings.Visible = true;
            Program.settings.Init();
        }
        private void buttonHotkeys_Click(object sender, EventArgs e)
        {
            if (Program.hotkeys != null)
                return;
            Program.hotkeys = new Hotkeys();
            Program.hotkeys.Visible = true;
            Program.hotkeys.Init();
        }
        public void glControl_Enter(object sender, EventArgs e)
        {
            Program.GLControl.Focus();
        }
        public void glControl_Leave(object sender, EventArgs e)
        {
            this.Focus();
        }

        //--------------------------------- SHIP MESHES ---------------------------------//
        private void listShipMeshes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreShipMeshListSelectedIndexChanged)
                return;

            selectedShipMesh = null;

            listShipMeshLODs.Items.Clear(); //Clear LOD list

            ignoreShipMeshDoScarCheck = true;
            checkShipMeshDoScar.Checked = false; //Reset do scar checkbox
            ignoreShipMeshDoScarCheck = false;
            checkShipMeshDoScar.Enabled = false; //Disable do scar checkbox

            comboShipMeshParent.SelectedIndex = 0; //Reset parent combo box
            comboShipMeshParent.Enabled = false; //Disable parent combo box

            buttonShipMeshRemove.Enabled = false; //Disable remove button
            buttonShipMeshLODRemove.Enabled = false;
            buttonShipMeshLODAdd.Enabled = false;

            boxShipMeshName.Clear();
            boxShipMeshName.Enabled = false;

            foreach (Label label in ShipMeshLODMaterialLabels)
                label.Visible = false;
            foreach (ComboBox comboBox in ShipMeshLODMaterialComboBoxes)
                comboBox.Visible = false;

            if (listShipMeshes.SelectedItem != null)
            {
                selectedShipMesh = ShipMeshListItems[listShipMeshes.SelectedItem];
            }
            else
                return;

            //Check do scar checkbox
            if (selectedShipMesh.Tags.Contains(ShipMeshTag.DoScar))
                checkShipMeshDoScar.Checked = true;

            ignoreShipMeshDoScarCheck = true;
            checkShipMeshDoScar.Enabled = true; //Enable do scar checkbox
            ignoreShipMeshDoScarCheck = false;

            boxShipMeshName.Enabled = true;
            boxShipMeshName.Text = selectedShipMesh.Name;

            comboShipMeshParent.SelectedItem = selectedShipMesh.Parent.Name; //Select parent joint in combo box

            comboShipMeshParent.Enabled = true; //Enable parent combo box

            //Fill LOD list
            for (int i = 0; i <= MAX_LEVEL_OF_DETAIL; i++)
                if (selectedShipMesh.LODMeshes[i].Count > 0)
                {
                    listShipMeshLODs.Items.Add("LOD " + i);
                    if (selectedShipMesh.LODMeshes[i][0].Visible)
                        listShipMeshLODs.SetItemChecked(i, true);
                }

            if (selectedShipMesh.LODMeshes[0].Count > 0)
                listShipMeshLODs.SelectedIndex = 0;
            else
                listShipMeshLODs.ClearSelected();

            //Enable remove button
            buttonShipMeshRemove.Enabled = true;

            buttonShipMeshLODAdd.Enabled = true;
        }
        public void AddShipMesh(HWShipMesh mesh)
        {
            object item = mesh.Name;
            listShipMeshes.Items.Add(item);
            mesh.ListItem = item;
            ShipMeshListItems.Add(item, mesh);
        }
        public void RemoveShipMesh(HWShipMesh mesh)
        {
            listShipMeshes.Items.Remove(mesh.ListItem);
            ShipMeshListItems.Remove(mesh.ListItem);
        }
        private void listShipMeshLODs_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (selectedShipMesh == null)
                return;

            bool visible = false;
            if (e.NewValue == CheckState.Checked)
                visible = true;

            foreach (HWShipMeshLOD shipMeshLOD in selectedShipMesh.LODMeshes[e.Index])
            {
                shipMeshLOD.Visible = visible;
            }

            Renderer.InvalidateView();
            Renderer.Invalidate();
        }
        private void boxShipMeshName_Leave(object sender, EventArgs e)
        {
            if (selectedShipMesh == null)
                return;

            UpdateShipMeshName(selectedShipMesh, boxShipMeshName.Text);
        }
        private void boxShipMeshName_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Return)
                return;

            if (selectedShipMesh == null)
                return;

            UpdateShipMeshName(selectedShipMesh, boxShipMeshName.Text);
        }
        private void UpdateShipMeshName(HWShipMesh shipMesh, string newName)
        {
            if (!listShipMeshes.Items.Contains(shipMesh.Name))
                return;

            //Ship mesh with this name already exists
            if (ShipMeshListItems.ContainsKey(newName))
            {
                HWShipMesh existingShipMesh = ShipMeshListItems[newName];
                if (existingShipMesh != shipMesh)
                {
                    MessageBox.Show("A ship mesh with this name already exists.", "Error while changing ship mesh name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    boxShipMeshName.Text = shipMesh.Name;
                    boxShipMeshName.Focus();
                    return;
                }
            }

            ignoreShipMeshListSelectedIndexChanged = true;
            int index = listShipMeshes.Items.IndexOf(selectedShipMesh.ListItem);
            ShipMeshListItems.Remove(selectedShipMesh.ListItem);
            listShipMeshes.Items.Remove(selectedShipMesh.ListItem);
            selectedShipMesh.Name = boxShipMeshName.Text;
            object item = selectedShipMesh.Name;
            selectedShipMesh.ListItem = item;
            listShipMeshes.Items.Insert(index, item);
            ShipMeshListItems.Add(item, selectedShipMesh);
            listShipMeshes.SelectedItem = item;
            ignoreShipMeshListSelectedIndexChanged = false;
        }
        private void comboShipMeshParent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectedShipMesh == null)
                return;

            HWJoint newParent = HWJoint.GetByName((string)comboShipMeshParent.SelectedItem);
            selectedShipMesh.Parent = newParent;
            HWScene.CalibrateSettings(false);
        }
        private void checkShipMeshDoScar_CheckedChanged(object sender, EventArgs e)
        {
            if (ignoreShipMeshDoScarCheck)
                return;

            HWShipMesh selectedShipMesh = null;
            if (listShipMeshes.SelectedItem != null)
            {
                selectedShipMesh = ShipMeshListItems[listShipMeshes.SelectedItem];
            }

            if (selectedShipMesh == null)
                return;

            selectedShipMesh.Tags.Remove(ShipMeshTag.DoScar);
            if (checkShipMeshDoScar.Checked)
                selectedShipMesh.Tags.Add(ShipMeshTag.DoScar);
        }
        private void listShipMeshLODs_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Label label in ShipMeshLODMaterialLabels)
            {
                label.Location = new Point(6, 25);
                label.Visible = false;
            }
            foreach (ComboBox comboBox in ShipMeshLODMaterialComboBoxes)
            {
                comboBox.Location = new Point(38, 22);
                comboBox.Visible = false;
            }

            buttonShipMeshLODRemove.Enabled = false;
            buttonShipMeshLODImportOBJ.Enabled = false;
            buttonShipMeshLODExportOBJ.Enabled = false;
            buttonShipMeshLODExportDAE.Enabled = false;
            buttonShipMeshLODImportDAE.Enabled = false;

            selectedShipMeshLOD = listShipMeshLODs.SelectedIndex;

            if (selectedShipMeshLOD == -1)
                return;

            List<HWShipMeshLOD> lodMeshes = selectedShipMesh.LODMeshes[selectedShipMeshLOD];

            buttonShipMeshLODRemove.Enabled = true;
            buttonShipMeshLODImportOBJ.Enabled = true;
            buttonShipMeshLODExportOBJ.Enabled = true;
            buttonShipMeshLODExportDAE.Enabled = true;
            buttonShipMeshLODImportDAE.Enabled = true;

            int materialCount = 0;
            foreach (HWShipMeshLOD lodMesh in lodMeshes)
                if (lodMesh.Material != null)
                    materialCount++;

            for (int i = 0; i < materialCount; i++)
            {
                ShipMeshLODMaterialLabels[i].Location = new Point(6, 25 + i * 27);
                ShipMeshLODMaterialLabels[i].Visible = true;

                ShipMeshLODMaterialComboBoxes[i].Location = new Point(38, 22 + i * 27);
                ShipMeshLODMaterialComboBoxes[i].Visible = true;
                ShipMeshLODMaterialComboBoxes[i].SelectedItem = lodMeshes[i].Material.Name;
            }
        }
        private void OnShipMeshLODMaterialChanged(object sender, EventArgs e)
        {
            if (ignoreShipMeshLODMaterialChanged)
                return;

            int materialIndex = -1;
            for (int i = 0; i < MAX_MATERIALS_ON_MESH; i++)
                if (sender == ShipMeshLODMaterialComboBoxes[i])
                {
                    materialIndex = i;
                    break;
                }

            if (materialIndex == -1)
                return;

            List<HWShipMeshLOD> lodMeshes = selectedShipMesh.LODMeshes[selectedShipMeshLOD];
            int selectedIndex = ShipMeshLODMaterialComboBoxes[materialIndex].SelectedIndex;

            if (selectedIndex != -1)
                lodMeshes[materialIndex].Material = MaterialNames[(string)ShipMeshLODMaterialComboBoxes[materialIndex].SelectedItem];
            else
                ShipMeshLODMaterialComboBoxes[materialIndex].SelectedItem = lodMeshes[materialIndex].Material.Name;
        }
        private void buttonShipMeshLODExportDAE_Click(object sender, EventArgs e)
        {
            HWShipMesh selectedShipMesh = null;
            if (listShipMeshes.SelectedItem != null)
                selectedShipMesh = ShipMeshListItems[listShipMeshes.SelectedItem];

            if (listShipMeshLODs.SelectedIndex < 0)
                return;

            List<HWMesh> meshes = new List<HWMesh>();
            foreach (HWShipMeshLOD lodMesh in selectedShipMesh.LODMeshes[listShipMeshLODs.SelectedIndex])
                meshes.Add(lodMesh);

            if (meshes.Count == 0)
                return;

            saveColladaMeshDialog.FileName = OpenedFile + "_" + meshes[0].Name + "_LOD" + listShipMeshLODs.SelectedIndex;
            DialogResult result = saveColladaMeshDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                Exporter.ExportMeshes(saveColladaMeshDialog.FileName, meshes);
            }
        }
        private void buttonShipMeshLODImportDAE_Click(object sender, EventArgs e)
        {
            HWShipMesh selectedShipMesh = null;
            if (listShipMeshes.SelectedItem != null)
                selectedShipMesh = ShipMeshListItems[listShipMeshes.SelectedItem];

            if (listShipMeshLODs.SelectedIndex < 0)
                return;

            List<HWShipMeshLOD> meshes = selectedShipMesh.LODMeshes[listShipMeshLODs.SelectedIndex];

            if (meshes.Count == 0)
                return;

            DialogResult result = openColladaMeshDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                Mesh[] newMeshes = Importer.ImportMeshesFromFile(openColladaMeshDialog.FileName);

                if (newMeshes.Length > meshes.Count)
                {
                    for (int i = newMeshes.Length - (newMeshes.Length - meshes.Count); i < newMeshes.Length; i++)
                    {
                        HWMaterial material;

                        if (HWMaterial.Materials.Count == 0)
                            material = HWMaterial.DefaultMaterial;
                        else
                            material = HWMaterial.Materials[0];

                        if (meshes.Count - 1 >= i)
                            material = meshes[i].Material;

                        HWShipMeshLOD newLOD = new HWShipMeshLOD(Importer.ParseAssimpMesh(newMeshes[i]), Vector3.Zero, Vector3.Zero, Vector3.One, material, selectedShipMesh, listShipMeshLODs.SelectedIndex);
                    }
                }

                for (int i = 0; i < meshes.Count; i++)
                {
                    if (newMeshes.Length - 1 >= i)
                    {
                        HWMaterial material = meshes[i].Material;
                        meshes[i].SetData(Importer.ParseAssimpMesh(newMeshes[i]));
                        meshes[i].CalculateBoundingBox();
                        meshes[i].Material = material;
                    }
                    else //Remove old mesh
                    {
                        meshes[i].Destroy();
                    }
                }

                foreach (HWShipMeshLOD lodMesh in selectedShipMesh.LODMeshes[selectedShipMeshLOD])
                    lodMesh.Visible = true;

                listShipMeshes_SelectedIndexChanged(this, EventArgs.Empty);
                HWScene.CalibrateSettings();
            }
        }
        private void buttonShipMeshLODExportOBJ_Click(object sender, EventArgs e)
        {
            HWShipMesh selectedShipMesh = null;
            if (listShipMeshes.SelectedItem != null)
                selectedShipMesh = ShipMeshListItems[listShipMeshes.SelectedItem];

            if (listShipMeshLODs.SelectedIndex < 0)
                return;

            List<HWMesh> meshes = new List<HWMesh>();
            foreach (HWShipMeshLOD lodMesh in selectedShipMesh.LODMeshes[listShipMeshLODs.SelectedIndex])
                meshes.Add(lodMesh);

            if (meshes.Count == 0)
                return;

            bool hasUV2 = false;
            foreach (HWMesh lodMesh in meshes)
                if (lodMesh.UVCount > 1)
                {
                    hasUV2 = true;
                    break;
                }

            if (hasUV2)
            {
                DialogResult yesNoResult = MessageBox.Show("This mesh has a second UV-channel, it will get lost when exporting to OBJ. Use the DAE format instead.\n\nDo you want to continue?", "OBJ-format limitations", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (yesNoResult == DialogResult.No)
                    return;
            }

            saveObjDialog.FileName = OpenedFile + "_" + meshes[0].Name + "_LOD" + listShipMeshLODs.SelectedIndex;
            DialogResult result = saveObjDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                ObjExporter.ExportToFile(saveObjDialog.FileName, meshes);
            }
        }
        private void buttonShipMeshLODImportOBJ_Click(object sender, EventArgs e)
        {
            HWShipMesh selectedShipMesh = null;
            if (listShipMeshes.SelectedItem != null)
                selectedShipMesh = ShipMeshListItems[listShipMeshes.SelectedItem];

            if (listShipMeshLODs.SelectedIndex < 0)
                return;

            List<HWShipMeshLOD> meshes = selectedShipMesh.LODMeshes[listShipMeshLODs.SelectedIndex];

            if (meshes.Count == 0)
                return;

            DialogResult result = openObjDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                Mesh[] newMeshes = ObjImporter.ImportMeshesFromFile(openObjDialog.FileName);

                if (newMeshes.Length > meshes.Count)
                {
                    for (int i = newMeshes.Length - (newMeshes.Length - meshes.Count); i < newMeshes.Length; i++)
                    {
                        HWMaterial material;

                        if (HWMaterial.Materials.Count == 0)
                            material = HWMaterial.DefaultMaterial;
                        else
                            material = HWMaterial.Materials[0];

                        if (meshes.Count - 1 >= i)
                            material = meshes[i].Material;

                        HWShipMeshLOD newLOD = new HWShipMeshLOD(Importer.ParseAssimpMesh(newMeshes[i]), Vector3.Zero, Vector3.Zero, Vector3.One, material, selectedShipMesh, listShipMeshLODs.SelectedIndex);
                    }
                }

                for (int i = 0; i < meshes.Count; i++)
                {
                    if (newMeshes.Length - 1 >= i)
                    {
                        HWMaterial material = meshes[i].Material;
                        meshes[i].SetData(Importer.ParseAssimpMesh(newMeshes[i]));
                        meshes[i].CalculateBoundingBox();
                        meshes[i].Material = material;
                    }
                    else //Remove old mesh
                    {
                        meshes[i].Destroy();
                    }
                }

                foreach (HWShipMeshLOD lodMesh in selectedShipMesh.LODMeshes[selectedShipMeshLOD])
                    lodMesh.Visible = true;

                listShipMeshes_SelectedIndexChanged(this, EventArgs.Empty);
                HWScene.CalibrateSettings();
            }
        }
        private void buttonShipMeshRemove_Click(object sender, EventArgs e)
        {
            if (selectedShipMesh == null)
                return;

            selectedShipMesh.Destroy();
            listShipMeshes.ClearSelected();
            listShipMeshes_SelectedIndexChanged(this, EventArgs.Empty);

            HWScene.FindBiggestMesh();
        }
        private void buttonShipMeshAdd_Click(object sender, EventArgs e)
        {
            int indexOffset = 1;
            string newName = "ShipMesh" + (listShipMeshes.Items.Count + indexOffset);
            while (listShipMeshes.Items.Contains(newName))
            {
                indexOffset++;
                newName = "ShipMesh" + (listShipMeshes.Items.Count + indexOffset);
            }

            List<ShipMeshTag> tags = new List<ShipMeshTag>();
            tags.Add(ShipMeshTag.DoScar);
            HWShipMesh newShipMesh = new HWShipMesh(HWJoint.Root, newName, tags);

            listShipMeshes.SelectedItem = newShipMesh.ListItem;
        }
        private void buttonShipMeshLODRemove_Click(object sender, EventArgs e)
        {
            if (selectedShipMesh == null)
                return;
            if (selectedShipMeshLOD == -1)
                return;

            HWShipMeshLOD[] lodMeshes = selectedShipMesh.LODMeshes[selectedShipMeshLOD].ToArray();
            foreach (HWShipMeshLOD lodMesh in lodMeshes)
                lodMesh.Destroy();

            if (selectedShipMeshLOD < MAX_LEVEL_OF_DETAIL)
            {
                for (int i = selectedShipMeshLOD + 1; i <= MAX_LEVEL_OF_DETAIL; i++)
                {
                    lodMeshes = selectedShipMesh.LODMeshes[i].ToArray();
                    foreach (HWShipMeshLOD lodMesh in lodMeshes)
                    {
                        lodMesh.LOD -= 1;
                        selectedShipMesh.LODMeshes[i].Remove(lodMesh);
                        selectedShipMesh.LODMeshes[i - 1].Add(lodMesh);
                    }
                }
            }

            listShipMeshLODs.Items.RemoveAt(selectedShipMeshLOD);
            listShipMeshLODs.ClearSelected();

            listShipMeshes_SelectedIndexChanged(this, EventArgs.Empty);

            HWScene.FindBiggestMesh();
        }
        private void buttonShipMeshLODAdd_Click(object sender, EventArgs e)
        {
            if (selectedShipMesh == null)
                return;

            int lowestLOD = -1;
            for (int i = 0; i <= MAX_LEVEL_OF_DETAIL; i++)
                if (selectedShipMesh.LODMeshes[i].Count > 0)
                    lowestLOD = i;

            if (lowestLOD > MAX_LEVEL_OF_DETAIL - 1)
                return;

            HWMaterial material;

            if (HWMaterial.Materials.Count == 0)
                material = HWMaterial.DefaultMaterial;
            else
                material = HWMaterial.Materials[0];

            HWShipMeshLOD newLODMesh = new HWShipMeshLOD(new MeshData(new Vertex[0], new int[0], 0), Vector3.Zero, Vector3.Zero, Vector3.One, material, selectedShipMesh, lowestLOD + 1);

            listShipMeshes_SelectedIndexChanged(this, EventArgs.Empty);
            listShipMeshLODs.SelectedIndex = lowestLOD + 1;
            listShipMeshLODs_SelectedIndexChanged(this, EventArgs.Empty);
        }
        //--------------------------------- ENGINE GLOW MESHES ---------------------------------//
        private void listEngineGlows_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEngineGlowListSelectedIndexChanged)
                return;

            selectedEngineGlow = null;

            listEngineGlowLODs.Items.Clear();
            buttonEngineGlowRemove.Enabled = false;
            buttonEngineGlowLODAdd.Enabled = false;
            boxEngineGlowName.Enabled = false;
            boxEngineGlowName.Clear();

            comboEngineGlowParent.Enabled = false;

            comboEngineGlowParent.SelectedIndex = 0; //Select root joint in combo box

            if (listEngineGlows.SelectedItem != null)
            {
                selectedEngineGlow = EngineGlowListItems[listEngineGlows.SelectedItem];
            }

            if (selectedEngineGlow != null)
            {
                buttonEngineGlowRemove.Enabled = true;
                buttonEngineGlowLODAdd.Enabled = true;

                boxEngineGlowName.Enabled = true;
                boxEngineGlowName.Text = selectedEngineGlow.Name;

                comboEngineGlowParent.Enabled = true;
                comboEngineGlowParent.SelectedItem = selectedEngineGlow.Parent.Name; //Select parent joint in combo box

                //Fill LOD list
                for (int i = 0; i <= MAX_LEVEL_OF_DETAIL; i++)
                    if (selectedEngineGlow.LODMeshes[i].Count > 0)
                    {
                        listEngineGlowLODs.Items.Add("LOD " + i);
                        if (selectedEngineGlow.LODMeshes[i][0].Visible)
                            listEngineGlowLODs.SetItemChecked(i, true);
                    }

                if (selectedEngineGlow.LODMeshes[0].Count > 0)
                    listEngineGlowLODs.SelectedIndex = 0;
                else
                    listEngineGlowLODs.ClearSelected();
            }
        }
        public void AddEngineGlow(HWEngineGlow glow)
        {
            object item = glow.Name;
            listEngineGlows.Items.Add(item);
            EngineGlowListItems.Add(item, glow);
        }
        public void RemoveEngineGlow(HWEngineGlow glow)
        {
            listEngineGlows.Items.Remove(glow.Name);
            EngineGlowListItems.Remove(glow.Name);
        }
        private void listEngineGlowLODs_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (selectedEngineGlow == null)
                return;

            bool visible = false;
            if (e.NewValue == CheckState.Checked)
                visible = true;

            foreach (HWEngineGlowLOD engineGlowLOD in selectedEngineGlow.LODMeshes[e.Index])
                engineGlowLOD.Visible = visible;

            Renderer.InvalidateView();
            Renderer.Invalidate();
        }
        private void buttonEngineGlowRemove_Click(object sender, EventArgs e)
        {
            if (selectedEngineGlow == null)
                return;

            selectedEngineGlow.Destroy();
            listEngineGlows.ClearSelected();
            listEngineGlows_SelectedIndexChanged(this, EventArgs.Empty);
        }
        private void buttonEngineGlowAdd_Click(object sender, EventArgs e)
        {
            int indexOffset = 1;
            string newName = "EngineGlow" + (listEngineGlows.Items.Count + indexOffset);
            while (listEngineGlows.Items.Contains(newName))
            {
                indexOffset++;
                newName = "EngineGlow" + (listEngineGlows.Items.Count + indexOffset);
            }

            HWEngineGlow newEngineGlow = new HWEngineGlow(HWJoint.Root, newName);

            listEngineGlows.SelectedItem = newEngineGlow.Name;
        }
        private void boxEngineGlowName_Leave(object sender, EventArgs e)
        {
            if (selectedEngineGlow == null)
                return;

            UpdateEngineGlowName(selectedEngineGlow, boxEngineGlowName.Text);
        }
        private void boxEngineGlowName_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Return)
                return;

            if (selectedEngineGlow == null)
                return;

            UpdateEngineGlowName(selectedEngineGlow, boxEngineGlowName.Text);
        }
        private void UpdateEngineGlowName(HWEngineGlow glowMesh, string newName)
        {
            if (!listEngineGlows.Items.Contains(glowMesh.Name))
                return;

            //Engine glow with this name already exists
            if (EngineGlowListItems.ContainsKey(newName))
            {
                HWEngineGlow existingEngineGlow = EngineGlowListItems[newName];
                if (existingEngineGlow != glowMesh)
                {
                    MessageBox.Show("An engine glow with this name already exists.", "Error while changing engine glow name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    boxEngineGlowName.Text = glowMesh.Name;
                    boxEngineGlowName.Focus();
                    return;
                }
            }

            ignoreEngineGlowListSelectedIndexChanged = true;
            int index = listEngineGlows.Items.IndexOf(selectedEngineGlow.Name);
            EngineGlowListItems.Remove(selectedEngineGlow.Name);
            listEngineGlows.Items.Remove(selectedEngineGlow.Name);
            selectedEngineGlow.Name = boxEngineGlowName.Text;
            listEngineGlows.Items.Insert(index, selectedEngineGlow.Name);
            EngineGlowListItems.Add(selectedEngineGlow.Name, selectedEngineGlow);
            listEngineGlows.SelectedItem = selectedEngineGlow.Name;
            ignoreEngineGlowListSelectedIndexChanged = false;
        }
        private void comboEngineGlowParent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectedEngineGlow == null)
                return;

            HWJoint newParent = HWJoint.GetByName((string)comboEngineGlowParent.SelectedItem);
            selectedEngineGlow.Parent = newParent;
        }
        private void listEngineGlowLODs_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonEngineGlowLODRemove.Enabled = false;
            buttonEngineGlowLODImportOBJ.Enabled = false;
            buttonEngineGlowLODExportOBJ.Enabled = false;
            buttonEngineGlowLODExportDAE.Enabled = false;
            buttonEngineGlowLODImportDAE.Enabled = false;

            selectedEngineGlowLOD = listEngineGlowLODs.SelectedIndex;

            if (selectedEngineGlowLOD == -1)
                return;

            buttonEngineGlowLODRemove.Enabled = true;
            buttonEngineGlowLODImportOBJ.Enabled = true;
            buttonEngineGlowLODExportOBJ.Enabled = true;
            buttonEngineGlowLODExportDAE.Enabled = true;
            buttonEngineGlowLODImportDAE.Enabled = true;
        }
        private void buttonEngineGlowLODExportDAE_Click(object sender, EventArgs e)
        {
            HWEngineGlow selectedEngineGlow = null;
            if (listEngineGlows.SelectedItem != null)
                selectedEngineGlow = EngineGlowListItems[listEngineGlows.SelectedItem];

            if (listEngineGlowLODs.SelectedIndex < 0)
                return;

            saveColladaMeshDialog.FileName = OpenedFile + "_" + selectedEngineGlow.Name + "_LOD" + listEngineGlowLODs.SelectedIndex;
            DialogResult result = saveColladaMeshDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                List<HWMesh> meshes = new List<HWMesh>();
                meshes.Add(selectedEngineGlow.LODMeshes[listEngineGlowLODs.SelectedIndex][0]);
                Exporter.ExportMeshes(saveColladaMeshDialog.FileName, meshes);
            }
        }
        private void buttonEngineGlowLODImportDAE_Click(object sender, EventArgs e)
        {
            if (selectedEngineGlow == null)
                return;

            DialogResult result = openColladaMeshDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                Mesh newMesh = Importer.ImportMeshFromFile(openColladaMeshDialog.FileName);

                selectedEngineGlow.LODMeshes[selectedEngineGlowLOD][0].SetData(Importer.ParseAssimpMesh(newMesh));
                selectedEngineGlow.LODMeshes[selectedEngineGlowLOD][0].Visible = true;

                listEngineGlowLODs.SetItemChecked(selectedEngineGlowLOD, true);
                listEngineGlowLODs_SelectedIndexChanged(this, EventArgs.Empty);
            }
        }
        private void buttonEngineGlowLODExportOBJ_Click(object sender, EventArgs e)
        {
            HWEngineGlow selectedEngineGlow = null;
            if (listEngineGlows.SelectedItem != null)
                selectedEngineGlow = EngineGlowListItems[listEngineGlows.SelectedItem];

            if (listEngineGlowLODs.SelectedIndex < 0)
                return;

            saveObjDialog.FileName = OpenedFile + "_" + selectedEngineGlow.Name + "_LOD" + listEngineGlowLODs.SelectedIndex;
            DialogResult result = saveObjDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                List<HWMesh> meshes = new List<HWMesh>();
                meshes.Add(selectedEngineGlow.LODMeshes[listEngineGlowLODs.SelectedIndex][0]);
                ObjExporter.ExportToFile(saveObjDialog.FileName, meshes);
            }
        }
        private void buttonEngineGlowLODImportOBJ_Click(object sender, EventArgs e)
        {
            if (selectedEngineGlow == null)
                return;

            DialogResult result = openObjDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                Mesh newMesh = ObjImporter.ImportMeshFromFile(openObjDialog.FileName);

                selectedEngineGlow.LODMeshes[selectedEngineGlowLOD][0].SetData(Importer.ParseAssimpMesh(newMesh));
                selectedEngineGlow.LODMeshes[selectedEngineGlowLOD][0].Visible = true;

                listEngineGlowLODs.SetItemChecked(selectedEngineGlowLOD, true);
                listEngineGlowLODs_SelectedIndexChanged(this, EventArgs.Empty);
            }
        }
        private void buttonEngineGlowLODRemove_Click(object sender, EventArgs e)
        {
            if (selectedEngineGlow == null)
                return;
            if (selectedEngineGlowLOD == -1)
                return;

            HWEngineGlowLOD[] lodMeshes = selectedEngineGlow.LODMeshes[selectedEngineGlowLOD].ToArray();
            foreach (HWEngineGlowLOD lodMesh in lodMeshes)
                lodMesh.Destroy();

            if (selectedEngineGlowLOD < MAX_LEVEL_OF_DETAIL)
            {
                for (int i = selectedEngineGlowLOD + 1; i <= MAX_LEVEL_OF_DETAIL; i++)
                {
                    lodMeshes = selectedEngineGlow.LODMeshes[i].ToArray();
                    foreach (HWEngineGlowLOD lodMesh in lodMeshes)
                    {
                        lodMesh.LOD -= 1;
                        selectedEngineGlow.LODMeshes[i].Remove(lodMesh);
                        selectedEngineGlow.LODMeshes[i - 1].Add(lodMesh);
                    }
                }
            }

            listEngineGlowLODs.Items.RemoveAt(selectedEngineGlowLOD);
            listEngineGlowLODs.ClearSelected();

            listEngineGlows_SelectedIndexChanged(this, EventArgs.Empty);
        }
        private void buttonEngineGlowLODAdd_Click(object sender, EventArgs e)
        {
            if (selectedEngineGlow == null)
                return;

            int lowestLOD = -1;
            for (int i = 0; i <= MAX_LEVEL_OF_DETAIL; i++)
                if (selectedEngineGlow.LODMeshes[i].Count > 0)
                    lowestLOD = i;

            if (lowestLOD > MAX_LEVEL_OF_DETAIL - 1)
                return;

            HWEngineGlowLOD newLODMesh = new HWEngineGlowLOD(new MeshData(new Vertex[0], new int[0], 0), Vector3.Zero, Vector3.Zero, Vector3.One, selectedEngineGlow, lowestLOD + 1);

            listEngineGlows_SelectedIndexChanged(this, EventArgs.Empty);
            listEngineGlowLODs.SelectedIndex = lowestLOD + 1;
            listEngineGlowLODs_SelectedIndexChanged(this, EventArgs.Empty);
        }
        //--------------------------------- ENGINE BURNS ---------------------------------//
        public void AddEngineBurn(HWEngineBurn engineBurn)
        {
            listEngineBurns.Items.Add(engineBurn.Name);
        }
        private void listEngineBurns_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            bool newValue = false;

            if (e.NewValue == CheckState.Checked)
                newValue = true;

            foreach (HWEngineBurn engineBurn in HWEngineBurn.EngineBurns)
            {
                if (engineBurn.Name == listEngineBurns.Items[e.Index].ToString())
                    engineBurn.Visible = newValue;
            }

            trackBarEngineBurnFlames_Scroll(null, EventArgs.Empty);

            Renderer.InvalidateView();
            Renderer.Invalidate();
        }
        private void listEngineBurns_SelectedIndexChanged(object sender, EventArgs e)
        {
            trackBarEngineBurnFlames.Enabled = true;
            trackBarEngineBurnFlames.Value = 0;
            trackBarEngineBurnFlames.Maximum = 1;
            numericEngineBurnSpriteIndex.Value = 0;

            HWEngineBurn engineBurn = null;
            foreach (HWEngineBurn burn in HWEngineBurn.EngineBurns)
            {
                if (burn.Name == listEngineBurns.SelectedItem.ToString())
                {
                    engineBurn = burn;
                    break;
                }
            }

            if (engineBurn != null)
            {
                boxEngineBurnName.Text = engineBurn.Name;
                HWJoint jointParent = (HWJoint)engineBurn.Parent;

                comboEngineBurnParent.SelectedItem = jointParent.Name;

                selectedEngineBurn = engineBurn;

                trackBarEngineBurnFlames.Maximum = engineBurn.Flames.Count - 1;
                trackBarEngineBurnFlames_Scroll(null, EventArgs.Empty);
            }
        }
        private void trackBarEngineBurnFlames_Scroll(object sender, EventArgs e)
        {
            if (selectedEngineBurn == null)
                return;

            //Reset flame colors
            foreach (HWEngineFlame flame in HWEngineFlame.EngineFlames)
            {
                flame.Cube.Color = new Vector3(1, 1, 1);
            }

            HWEngineFlame selectedFlame = selectedEngineBurn.Flames[trackBarEngineBurnFlames.Value];

            if (selectedEngineBurn.Visible)
                selectedFlame.Cube.Color = new Vector3(1, 0, 0);

            numericEngineBurnSpriteIndex.Value = selectedFlame.SpriteIndex;

            Renderer.InvalidateMeshData();
            Renderer.InvalidateView();
            Renderer.Invalidate();
        }

        //--------------------------------- COLLISION MESHES ---------------------------------//
        private void listCollisionMeshes_SelectedIndexChanged(object sender, EventArgs e)
        {
            ignoreCollisionMeshParentChanged = true;
            comboCollisionMeshParent.SelectedItem = null;
            ignoreCollisionMeshParentChanged = false;

            comboCollisionMeshParent.Enabled = false;
            buttonCollisionMeshRemove.Enabled = false;

            buttonCollisionMeshExportDAE.Enabled = false;
            buttonCollisionMeshImportDAE.Enabled = false;
            buttonCollisionMeshExportOBJ.Enabled = false;
            buttonCollisionMeshImportOBJ.Enabled = false;

            selectedCollisionMesh = null;

            if (listCollisionMeshes.SelectedItem == null)
                return;

            //Has to be done with a loop, because of multiple collision meshes with the same name
            foreach (HWCollisionMesh collisionMesh in HWCollisionMesh.CollisionMeshes)
            {
                if (collisionMesh.ItemIndex == listCollisionMeshes.SelectedIndex)
                {
                    selectedCollisionMesh = collisionMesh;
                    break;
                }
            }

            if (selectedCollisionMesh == null)
                return;

            ignoreCollisionMeshParentChanged = true;
            comboCollisionMeshParent.SelectedItem = selectedCollisionMesh.Parent.Name;
            ignoreCollisionMeshParentChanged = false;

            comboCollisionMeshParent.Enabled = true;
            buttonCollisionMeshRemove.Enabled = true;

            buttonCollisionMeshExportDAE.Enabled = true;
            buttonCollisionMeshImportDAE.Enabled = true;
            buttonCollisionMeshExportOBJ.Enabled = true;
            buttonCollisionMeshImportOBJ.Enabled = true;

        }
        public void AddCollisionMesh(HWCollisionMesh mesh)
        {
            listCollisionMeshes.Items.Add(mesh.Parent.Name);
            mesh.ItemIndex = listCollisionMeshes.Items.Count - 1;
        }
        public void RemoveCollisionMesh(HWCollisionMesh mesh)
        {
            for (int i = mesh.ItemIndex + 1; i < listCollisionMeshes.Items.Count; i++)
            {
                foreach (HWCollisionMesh colMesh in HWCollisionMesh.CollisionMeshes)
                {
                    if (colMesh.ItemIndex == i)
                    {
                        colMesh.ItemIndex--;
                        break;
                    }
                }
            }

            listCollisionMeshes.Items.Remove(mesh.Parent.Name);
            mesh.ItemIndex = -1;

            for (int i = 0; i < listCollisionMeshes.Items.Count; i++)
            {
                foreach (HWCollisionMesh colMesh in HWCollisionMesh.CollisionMeshes)
                {
                    if (colMesh.ItemIndex == i)
                    {
                        listCollisionMeshes.SetItemChecked(i, colMesh.Visible);
                        break;
                    }
                }
            }
        }
        private void listCollisionMeshes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (selectedCollisionMesh == null)
                return;

            bool visible = false;
            if (e.NewValue == CheckState.Checked)
                visible = true;

            selectedCollisionMesh.Visible = visible;

            Renderer.Invalidate();
        }
        private void buttonCollisionMeshRemove_Click(object sender, EventArgs e)
        {
            if (selectedCollisionMesh == null)
                return;

            selectedCollisionMesh.Destroy();
            listCollisionMeshes.ClearSelected();
            listCollisionMeshes_SelectedIndexChanged(this, EventArgs.Empty);
        }
        private void buttonCollisionMeshAdd_Click(object sender, EventArgs e)
        {
            HWCollisionMesh newCollisionMesh = new HWCollisionMesh(new MeshData(), Vector3.Zero, Vector3.Zero, Vector3.One, HWJoint.Root);

            listCollisionMeshes.SelectedIndex = newCollisionMesh.ItemIndex;
            listCollisionMeshes_SelectedIndexChanged(this, EventArgs.Empty);
        }
        private void buttonCollisionMeshExportDAE_Click(object sender, EventArgs e)
        {
            if (selectedCollisionMesh == null)
                return;

            saveColladaMeshDialog.FileName = OpenedFile + "_COL_" + selectedCollisionMesh.Parent.Name;
            DialogResult result = saveColladaMeshDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                List<HWMesh> meshes = new List<HWMesh>();
                meshes.Add(selectedCollisionMesh);
                Exporter.ExportMeshes(saveColladaMeshDialog.FileName, meshes);
            }
        }
        private void buttonCollisionMeshImportDAE_Click(object sender, EventArgs e)
        {
            if (selectedCollisionMesh == null)
                return;

            DialogResult result = openColladaMeshDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                Mesh newMesh = Importer.ImportMeshFromFile(openColladaMeshDialog.FileName);

                selectedCollisionMesh.SetData(Importer.ParseAssimpMesh(newMesh));
                selectedCollisionMesh.Visible = true;

                listCollisionMeshes.SetItemChecked(selectedCollisionMesh.ItemIndex, true);
                listCollisionMeshes_SelectedIndexChanged(this, EventArgs.Empty);
            }
        }
        private void buttonCollisionMeshExportOBJ_Click(object sender, EventArgs e)
        {
            if (selectedCollisionMesh == null)
                return;

            saveObjDialog.FileName = OpenedFile + "_COL_" + selectedCollisionMesh.Parent.Name;
            DialogResult result = saveObjDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                List<HWMesh> meshes = new List<HWMesh>();
                meshes.Add(selectedCollisionMesh);
                ObjExporter.ExportToFile(saveObjDialog.FileName, meshes);
            }
        }
        private void buttonCollisionMeshImportOBJ_Click(object sender, EventArgs e)
        {
            if (selectedCollisionMesh == null)
                return;

            DialogResult result = openObjDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                Mesh newMesh = ObjImporter.ImportMeshFromFile(openObjDialog.FileName);

                selectedCollisionMesh.SetData(Importer.ParseAssimpMesh(newMesh));
                selectedCollisionMesh.Visible = true;

                listCollisionMeshes.SetItemChecked(selectedCollisionMesh.ItemIndex, true);
                listCollisionMeshes_SelectedIndexChanged(this, EventArgs.Empty);
            }
        }
        private void comboCollisionMeshParent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectedCollisionMesh == null)
                return;

            if (ignoreCollisionMeshParentChanged)
                return;

            HWJoint newParent = HWJoint.GetByName((string)comboCollisionMeshParent.SelectedItem);
            selectedCollisionMesh.Parent = newParent;

            listCollisionMeshes.Items[selectedCollisionMesh.ItemIndex] = selectedCollisionMesh.Parent.Name;
        }

        //--------------------------------- ENGINE SHAPES ---------------------------------//
        private void listEngineShapes_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboEngineShapeParent.SelectedIndex = 0; //Select root joint in combo box

            HWEngineShape selectedEngineShape = null;
            //Has to be done with a loop, because of multiple engine shapes with the same name
            foreach (HWEngineShape engineShape in HWEngineShape.EngineShapes)
            {
                if (engineShape.EngineShapeListItemIndex == listEngineShapes.SelectedIndex)
                {
                    selectedEngineShape = engineShape;
                    break;
                }
            }

            if (selectedEngineShape == null)
                return;

            comboEngineShapeParent.SelectedItem = selectedEngineShape.Parent.Name; //Select parent joint in combo box
        }
        public void AddEngineShape(HWEngineShape mesh)
        {
            object item = mesh.Name;
            listEngineShapes.Items.Add(item);
            mesh.EngineShapeListItemIndex = listEngineShapes.Items.Count - 1;
        }
        private void listEngineShapes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (listEngineShapes.SelectedItem != null)
            {
                HWEngineShape selectedEngineShape = null;
                //Has to be done with a loop, because of multiple engine shapes with the same name
                foreach (HWEngineShape engineShape in HWEngineShape.EngineShapes)
                {
                    if (engineShape.EngineShapeListItemIndex == listEngineShapes.SelectedIndex)
                    {
                        selectedEngineShape = engineShape;
                        break;
                    }
                }

                bool visible = false;
                if (e.NewValue == CheckState.Checked)
                    visible = true;

                selectedEngineShape.Visible = visible;

                Renderer.Invalidate();
            }
        }

        //----------------------------------- ANIMATIONS ----------------------------------//
        public void AddAnimation(HWAnimation animation)
        {
            listAnimations.Items.Add(animation.Name);
            AnimationNames.Add(animation.Name, animation);
        }
        private void listAnimations_SelectedIndexChanged(object sender, EventArgs e)
        {
            listAnimationJoints.Items.Clear();
            boxAnimationName.Clear();
            buttonAnimationPlay.Enabled = false;
            AnimationPlaying = false;
            selectedAnimation = null;

            numericAnimationStartTime.Value = 0;
            numericAnimationEndTime.Value = 0;
            numericAnimationLoopStartTime.Value = 0;
            numericAnimationLoopEndTime.Value = 0;

            if (listAnimations.SelectedItem == null)
                return;

            if (AnimationNames.ContainsKey((string)listAnimations.SelectedItem))
                selectedAnimation = AnimationNames[(string)listAnimations.SelectedItem];

            if (selectedAnimation == null)
                return;

            boxAnimationName.Text = selectedAnimation.Name;
            buttonAnimationPlay.Enabled = true;

            numericAnimationStartTime.Value = (decimal)selectedAnimation.StartTime;
            numericAnimationEndTime.Value = (decimal)selectedAnimation.EndTime;
            numericAnimationLoopStartTime.Value = (decimal)selectedAnimation.LoopStartTime;
            numericAnimationLoopEndTime.Value = (decimal)selectedAnimation.LoopEndTime;

            foreach (HWJoint joint in selectedAnimation.AnimatedJoints)
            {
                listAnimationJoints.Items.Add(joint.Name);
            }
        }
        private void buttonAnimationPlay_Click(object sender, EventArgs e)
        {
            if (selectedAnimation == null)
                return;

            if (AnimationPlaying)
                AnimationPlaying = false;
            else
                AnimationPlaying = true;
        }

        //----------------------------------- MATERIALS ----------------------------------//
        private void listMaterials_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreMaterialListSelectedIndexChanged)
                return;

            listMaterialTextures.Items.Clear();
            comboMaterialShader.Enabled = false;
            comboMaterialFormat.Enabled = false;
            buttonMaterialRemove.Enabled = false;
            boxMaterialName.Enabled = false;
            buttonMaterialTexturesBrowseDIFF.Enabled = false;
            boxMaterialName.Clear();

            if (listMaterials.SelectedItem == null)
                return;

            selectedMaterial = MaterialNames[(string)listMaterials.SelectedItem];

            //Set shader combo
            ignoreMaterialShaderChanged = true;
            comboMaterialShader.SelectedItem = selectedMaterial.Shader;
            ignoreMaterialShaderChanged = false;

            comboMaterialShader.Enabled = true;
            comboMaterialFormat.Enabled = true;
            buttonMaterialRemove.Enabled = true;
            boxMaterialName.Enabled = true;
            boxMaterialName.Text = selectedMaterial.Name;
            buttonMaterialTexturesBrowseDIFF.Enabled = true;

            //Fill texture list
            foreach (HWImage image in selectedMaterial.Images)
            {
                listMaterialTextures.Items.Add(image.Name);
            }

            //Set texture format
            comboMaterialFormat.SelectedIndex = (int)selectedMaterial.Format;
        }
        public void AddMaterial(HWMaterial material)
        {
            if (!listMaterials.Items.Contains(material.Name)) //If material not already in list
            {
                object item = material.Name;
                listMaterials.Items.Add(item);

                foreach (ComboBox comboBox in ShipMeshLODMaterialComboBoxes)
                    comboBox.Items.Add(item);

                MaterialNames.Add(material.Name, material);
            }
        }
        public void RemoveMaterial(HWMaterial material)
        {
            listMaterials.Items.Remove(material.Name);
            MaterialNames.Remove(material.Name);
            foreach (ComboBox comboBox in ShipMeshLODMaterialComboBoxes)
                comboBox.Items.Remove(material.Name);
        }
        private void comboMaterialShader_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreMaterialShaderChanged)
                return;

            if (selectedMaterial == null)
                return;

            selectedMaterial.Shader = comboMaterialShader.SelectedItem.ToString();
            selectedMaterial.LoadTextures();
        }
        private void comboMaterialFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectedMaterial == null)
                return;

            ImageFormat format;
            if (comboMaterialFormat.SelectedItem.ToString() != "8888")
                Enum.TryParse(comboMaterialFormat.SelectedItem.ToString(), out format);
            else
                format = ImageFormat.UNCOMPRESSED;
            selectedMaterial.Format = format;
        }
        private void buttonMaterialRemove_Click(object sender, EventArgs e)
        {
            if (selectedMaterial == null)
                return;

            selectedMaterial.Destroy();
            listMaterials.ClearSelected();
            listMaterials_SelectedIndexChanged(this, EventArgs.Empty);
        }
        private void boxMaterialName_Leave(object sender, EventArgs e)
        {
            if (selectedMaterial == null)
                return;

            UpdateMaterialName(selectedMaterial, boxMaterialName.Text);
        }
        private void boxMaterialName_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Return)
                return;

            if (selectedMaterial == null)
                return;

            UpdateMaterialName(selectedMaterial, boxMaterialName.Text);
        }
        private void UpdateMaterialName(HWMaterial material, string newName)
        {
            //Material with this name already exists
            if (MaterialNames.ContainsKey(newName))
            {
                HWMaterial existingMaterial = MaterialNames[newName];
                if (existingMaterial != material)
                {
                    MessageBox.Show("A material with this name already exists.", "Error while changing material name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    boxMaterialName.Text = material.Name;
                    boxMaterialName.Focus();
                    return;
                }
            }


            int index = listMaterials.Items.IndexOf(selectedMaterial.Name);
            if (index == -1)
                return;

            ignoreShipMeshLODMaterialChanged = true;

            foreach (ComboBox comboMaterial in ShipMeshLODMaterialComboBoxes)
                comboMaterial.Items.Remove(selectedMaterial.Name);

            ignoreMaterialListSelectedIndexChanged = true;
            MaterialNames.Remove(selectedMaterial.Name);
            listMaterials.Items.Remove(selectedMaterial.Name);
            selectedMaterial.Name = boxMaterialName.Text;
            listMaterials.Items.Insert(index, selectedMaterial.Name);
            MaterialNames.Add(selectedMaterial.Name, selectedMaterial);
            listMaterials.SelectedItem = selectedMaterial.Name;
            ignoreMaterialListSelectedIndexChanged = false;

            foreach (ComboBox comboMaterial in ShipMeshLODMaterialComboBoxes)
                comboMaterial.Items.Add(selectedMaterial.Name);

            ignoreShipMeshLODMaterialChanged = false;

            listShipMeshLODs_SelectedIndexChanged(this, EventArgs.Empty);
        }
        private void buttonMaterialAdd_Click(object sender, EventArgs e)
        {
            HWMaterial newMaterial = new HWMaterial("ship");

            int indexOffset = 1;
            string newName = "material" + (listMaterials.Items.Count + indexOffset);
            while (listMaterials.Items.Contains(newName))
            {
                indexOffset++;
                newName = "material" + (listMaterials.Items.Count + indexOffset);
            }
            newMaterial.Name = newName;
            newMaterial.Suffix = 1;
            newMaterial.LoadTextures();

            AddMaterial(newMaterial);
            listMaterials.SelectedItem = newMaterial.Name;
        }
        private void buttonMaterialTexturesBrowseDIFF_Click(object sender, EventArgs e)
        {
            if (selectedMaterial == null)
                return;

            DialogResult result = browseMaterialTexturesDIFFDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string filePath = browseMaterialTexturesDIFFDialog.FileName;
                string fileName = Path.GetFileNameWithoutExtension(filePath);
                if (!fileName.EndsWith("_DIFF"))
                {
                    MessageBox.Show("The texture name does not end with \"_DIFF\"!", "Error while loading new textures", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //Remove previous images
                HWImage[] images = selectedMaterial.Images.ToArray();
                for (int i = 0; i < images.Length; i++)
                    images[i].Destroy();

                HWImage newDiff = new HWImage(Path.GetFileNameWithoutExtension(filePath), filePath, selectedMaterial.Format);
                newDiff.Material = selectedMaterial;

                selectedMaterial.DiffusePath = filePath;

                selectedMaterial.LoadTextures();

                listMaterials_SelectedIndexChanged(this, EventArgs.Empty);

                Renderer.Invalidate();
            }
        }


        private void trackBarThrusterStrength_Scroll(object sender, EventArgs e)
        {
            Renderer.ThrusterInterpolation = (float)trackBarThrusterStrength.Value / 100;
            HWEngineGlow.UpdateEngineStrength();

            glControl_Update(this, EventArgs.Empty);

            Renderer.InvalidateView();
            Renderer.Invalidate();
        }
        private void trackBarProgress_Scroll(object sender, EventArgs e)
        {
            Renderer.Progress = (float)trackBarProgress.Value / 100;

            Renderer.Invalidate();
        }

        private void buttonAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "DAEnerys b" + BUILD + "\n\nDeveloped by Christoph (PayDay) Timmermann and\nAnthony (radar3301) Lofthouse (aka. ajlsunrise33)\nwith help from the Gearbox forums.\n\nEditor icons made by SumoChick and Alekfix789.\n\nUses\n - OpenTK\n - Assimp\n - Assimp.NET\n - FSharp\n - DevIL\n - DevILSharp\n - AlphaColorDialog", "DAEnerys", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //Perspective-Orthographic combobox
        private void comboPerspectiveOrtho_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboPerspectiveOrtho.SelectedIndex == 0)
                Program.Camera.Orthographic = false;
            else
                Program.Camera.Orthographic = true;
        }
        public void UpdatePerspectiveOrthoCombo()
        {
            if (!Program.Camera.Orthographic)
                comboPerspectiveOrtho.SelectedIndex = 0;
            else
                comboPerspectiveOrtho.SelectedIndex = 1;
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            AnimationPlaying = false;
        }

        //--------------------------------- PROBLEMS TAB ---------------------------------//
        private void gridProblems_SelectionChanged(object sender, EventArgs e)
        {
            gridProblems.ClearSelection();
        }

        private void buttonProblems_Click(object sender, EventArgs e)
        {
            problemsVisible = !problemsVisible;
            splitContainer2.Panel2Collapsed = !problemsVisible;

            if (problemsVisible)
                buttonProblems.BackColor = Color.FromArgb(255, 178, 178, 178);
            else
                buttonProblems.BackColor = Color.FromArgb(255, 248, 248, 248);
        }

        public void AddProblem(Problem problem)
        {
            DataGridViewRow row = (DataGridViewRow)gridProblems.RowTemplate.Clone();
            row.CreateCells(gridProblems, problem.Description);
            gridProblems.Rows.Add(row);

            if (problem.Type == ProblemTypes.ERROR)
                row.Cells[0].Style.ForeColor = Color.Red;
            else if (problem.Type == ProblemTypes.WARNING)
                row.Cells[0].Style.ForeColor = Color.DarkOrange;
        }

        public void UpdateProblems()
        {
            bool errors = false;
            bool warnings = false;

            foreach (Problem problem in Problem.Problems)
            {
                if (problem.Type == ProblemTypes.ERROR)
                    errors = true;
                else if (problem.Type == ProblemTypes.WARNING)
                    warnings = true;
            }

            if (warnings)
            {
                buttonProblems.Image = this.buttonProblems.Image = global::DAEnerys.Properties.Resources.flagYellow;
                problemsVisible = true;
            }

            if (errors)
            {
                buttonProblems.Image = this.buttonProblems.Image = global::DAEnerys.Properties.Resources.flagRed;
                problemsVisible = true;
            }

            if (!warnings && !errors)
            {
                problemsVisible = false;
                buttonProblems.Image = this.buttonProblems.Image = global::DAEnerys.Properties.Resources.flagWhite;
            }

            if (problemsVisible)
                buttonProblems.BackColor = Color.FromArgb(255, 178, 178, 178);
            else
                buttonProblems.BackColor = Color.FromArgb(255, 248, 248, 248);

            splitContainer2.Panel2Collapsed = !problemsVisible;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Renderer.ReloadShaders();
            Renderer.Invalidate();
        }

        private void buttonShaderSettings_Click(object sender, EventArgs e)
        {
            if (Program.ShaderSettings != null)
                return;
            Program.ShaderSettings = new ShaderSettings();
            Program.ShaderSettings.Visible = true;
            Program.ShaderSettings.Init();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            //Dump.ADuiePyle();
        }

        private void buttonCheckForUpdates_Click(object sender, EventArgs e)
        {
            Updater.CheckForUpdatesManually();
        }
    }
}