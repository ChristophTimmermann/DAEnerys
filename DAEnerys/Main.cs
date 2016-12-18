using System;
using System.Drawing;
using System.Windows.Forms;
using OpenTK;
using System.IO;
using System.Collections.Generic;
using OpenTK.Graphics;
using Assimp;
using System.Linq;

namespace DAEnerys
{
    public partial class Main : Form
    {
        public int BUILD = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Build;
        public string OpenedFile = "";

        public bool Loaded = false;
        HWCollisionMesh selectedCollisionMesh;
        HWDockpath selectedDockpath;
        HWNavLight selectedNavLight;
        HWEngineBurn selectedEngineBurn;
        HWMaterial selectedMaterial;
        public HWAnimation selectedAnimation;

        public Dictionary<object, HWShipMesh> ShipMeshListItems = new Dictionary<object, HWShipMesh>();
        private Label[] ShipMeshLODMaterialLabels = new Label[MAX_MATERIALS_ON_MESH];
        private ComboBox[] ShipMeshLODMaterialComboBoxes = new ComboBox[MAX_MATERIALS_ON_MESH];
        private HWShipMesh selectedShipMesh;
        private int selectedShipMeshLOD;

        public Dictionary<object, HWEngineGlow> EngineGlowListItems = new Dictionary<object, HWEngineGlow>();

        public Dictionary<HWJoint, object> JointComboItems = new Dictionary<HWJoint, object>();

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

        private bool animationPlaying;
        public bool AnimationPlaying { get { return animationPlaying; } set { animationPlaying = value; HWAnimation.AnimationTime = 0; foreach (HWJoint joint in HWJoint.Joints) { joint.AnimationMatrix = Matrix4.Identity; joint.CalculateWorldMatrix(); Renderer.InvalidateView(); Renderer.Invalidate(); } string text = value ? "Stop" : "Play"; buttonAnimationPlay.Text = text; if (value) HWAnimation.AnimationTime = selectedAnimation.StartTime; } }

        const int MAX_MATERIALS_ON_MESH = 16;

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
                ShipMeshLODMaterialLabels[i].Location = new Point(6, 25 + i * 27);
                ShipMeshLODMaterialLabels[i].Text = "#" + i;
                ShipMeshLODMaterialLabels[i].AutoSize = true;
                ShipMeshLODMaterialLabels[i].Visible = false;

                ShipMeshLODMaterialComboBoxes[i] = new ComboBox();
                ShipMeshLODMaterialComboBoxes[i].Parent = groupShipMeshLODMaterials;
                ShipMeshLODMaterialComboBoxes[i].Location = new Point(38, 22 + i * 27);
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

            int visibleNavLights = 0;
            foreach (HWNavLight navLight in HWNavLight.NavLights)
            {
                if (navLight.Visible)
                    visibleNavLights++;

                navLight.Update();
            }

            int visibleEffects = 0;
            foreach (EditorEffect effect in EditorScene.effects)
            {
                if (effect.IsRunning)
                    visibleEffects++;

                effect.Update();
            }

            if(animationPlaying)
                HWAnimation.Update();

            //Only update render if it is needed
            if (visibleNavLights > 0 || visibleEffects > 0)
                Renderer.Invalidate();
            if (visibleEffects > 0)
                Renderer.InvalidateMeshData();

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
            JointComboItems.Clear();

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
            listEngineGlowLODs.Items.Clear();
            EngineGlowListItems.Clear();

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

            listBoxMarkers.Items.Clear();
            checkboxDrawMarkers.Checked = false;

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
            comboNavLightType.SelectedItem = null;
            navLightList.Items.Clear();
            numericNavLightSize.Value = 0;
            numericNavLightPhase.Value = 0;
            numericNavLightFrequency.Value = 0;
            buttonNavLightColor.BackColor = Color.White;
            numericNavLightDistance.Value = 0;
            checkNavLightFlagSprite.Checked = false;
            checkNavLightFlagHighEnd.Checked = false;
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

            comboShipMeshParent.SelectedItem = 0;
            comboCollisionMeshParent.SelectedItem = 0;
            comboEngineGlowParent.SelectedItem = 0;
            comboEngineShapeParent.SelectedItem = 0;
            comboEngineBurnParent.SelectedItem = 0;

            problemsVisible = false;
            splitContainer2.Panel2Collapsed = true;
            Problem.Problems.Clear();
            gridProblems.Rows.Clear();

            this.Text = "DAEnerys";

            listMaterials_SelectedIndexChanged(this, EventArgs.Empty);
            listShipMeshes_SelectedIndexChanged(this, EventArgs.Empty);

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

        public void AddMarker(HWMarker marker)
        {
            listBoxMarkers.Items.Add(marker.Name);
        }
        public void AddJoint(HWJoint joint, HWJoint parent)
        {
            TreeNode newNode = new TreeNode(joint.Name);

            if (parent == null) //If root joint
                jointsTree.Nodes.Add(newNode);
            else
                parent.TreeNode.Nodes.Add(newNode);

            joint.TreeNode = newNode;

            object item = joint.Name;
            JointComboItems.Add(joint, item);
            joint.ComboItem = item;

            comboShipMeshParent.Items.Add(item);
            comboCollisionMeshParent.Items.Add(item);
            comboEngineGlowParent.Items.Add(item);
            comboEngineShapeParent.Items.Add(item);
            comboEngineBurnParent.Items.Add(item);
        }
        public void RemoveJoint(HWJoint joint)
        {
            List<HWElement> jointChildren = new List<HWElement>();
            foreach (HWElement child in joint.Children)
            {
                HWJoint childJoint = child as HWJoint;
                if (childJoint == null)
                    continue;
                childJoint.TreeNode.Remove();
                AddJoint(childJoint, (HWJoint)joint.Parent);
            }

            jointsTree.Nodes.Remove(joint.TreeNode);

            JointComboItems.Remove(joint);
            joint.ComboItem = null;
            object item = joint.Name;

            //Remove joint from ship mesh parents
            comboShipMeshParent.Items.Remove(item);

            comboCollisionMeshParent.Items.Remove(item);

            //Remove joint from engine glow parents
            comboEngineGlowParent.Items.Remove(item);

            //Remove joint from engine shape parents
            comboEngineShapeParent.Items.Remove(item);

            comboEngineBurnParent.Items.Remove(item);
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
                line.StartColor = Color.Red;
                line.EndColor = Color.Red;
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
            navLightList.Items.Add(navLight.Name);
            navLight.NavLightListItemIndex = navLightList.Items.Count - 1;
        }
        private void navLightList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            bool newValue = false;

            if (e.NewValue == CheckState.Checked)
                newValue = true;

            foreach (HWNavLight navLight in HWNavLight.NavLights)
            {
                if (navLight.Name == navLightList.Items[e.Index].ToString())
                {
                    navLight.Visible = newValue;
                }
            }

            Renderer.InvalidateView();
            Renderer.Invalidate();
        }
        private void navLightList_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboNavLightType.SelectedIndex = 0;
            numericNavLightSize.Value = 0;
            numericNavLightPhase.Value = 0;
            numericNavLightFrequency.Value = 0;
            buttonNavLightColor.BackColor = Color.White;
            numericNavLightDistance.Value = 0;

            checkNavLightFlagSprite.Checked = false;
            checkNavLightFlagHighEnd.Checked = false;

            HWNavLight navLight = null;

            if (navLightList.SelectedItem != null)
            {
                foreach (HWNavLight light in HWNavLight.NavLights)
                {
                    if (light.Name == navLightList.SelectedItem.ToString())
                    {
                        navLight = light;
                        break;
                    }
                }
            }

            if (navLight != null)
            {
                comboNavLightType.SelectedItem = navLight.Style.Name;
                numericNavLightSize.Value = (decimal)navLight.Size;
                numericNavLightPhase.Value = (decimal)navLight.Phase;
                numericNavLightFrequency.Value = (decimal)navLight.Frequency;

                int red = (int)Math.Round((float)(navLight.Color.X * 255));
                int green = (int)Math.Round((float)(navLight.Color.Y * 255));
                int blue = (int)Math.Round((float)(navLight.Color.Z * 255));
                red = Math.Min(red, 255);
                green = Math.Min(green, 255);
                blue = Math.Min(blue, 255);
                buttonNavLightColor.BackColor = Color.FromArgb(255, red, green, blue);

                numericNavLightDistance.Value = (decimal)navLight.Distance;

                foreach (NavLightFlag flag in navLight.Flags)
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

                selectedNavLight = navLight;
            }
        }
        public void AddNavLightStyle(HWNavLightStyle navLightStyle)
        {
            comboNavLightType.Items.Add(navLightStyle.Name);
        }
        public void CheckNavLightVisible(HWNavLight navLight, bool visible)
        {
            navLightList.SetItemChecked(navLight.NavLightListItemIndex, visible);
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

            Renderer.Invalidate();
        }

        //--------------------------------- MISC ---------------------------------//
        private void checkboxDrawMarkers_CheckedChanged(object sender, EventArgs e)
        {
            foreach (HWMarker marker in HWMarker.Markers)
            {
                foreach (EditorLine line in marker.Lines)
                {
                    line.Visible = checkboxDrawMarkers.Checked;
                }
            }

            Renderer.InvalidateView();
            Renderer.Invalidate();
        }

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

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            if (Program.settings != null) return;
            Program.settings = new Settings();
            Program.settings.Visible = true;
            Program.settings.Init();
        }

        private void buttonHotkeys_Click(object sender, EventArgs e)
        {
            if (Program.hotkeys != null) return;
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

            //Select parent joint in combo box
            object item = JointComboItems[selectedShipMesh.Parent];
            comboShipMeshParent.SelectedItem = item; //Select parent joint in combo box

            comboShipMeshParent.Enabled = true; //Enable parent combo box

            //Fill LOD list
            for (int i = 0; i <= 3; i++)
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
                label.Visible = false;
            foreach (ComboBox comboBox in ShipMeshLODMaterialComboBoxes)
                comboBox.Visible = false;

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
                ShipMeshLODMaterialLabels[i].Visible = true;
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
                        HWMaterial material = HWMaterial.DefaultMaterial;
                        if (meshes.Count - 1 >= i)
                            material = meshes[i].Material;

                        HWShipMeshLOD newLOD = new HWShipMeshLOD(Importer.ParseAssimpMesh(newMeshes[i]), Matrix4.Identity, material, selectedShipMesh, listShipMeshLODs.SelectedIndex);
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
                        HWMaterial material = HWMaterial.DefaultMaterial;
                        if (meshes.Count - 1 >= i)
                            material = meshes[i].Material;

                        HWShipMeshLOD newLOD = new HWShipMeshLOD(Importer.ParseAssimpMesh(newMeshes[i]), Matrix4.Identity, material, selectedShipMesh, listShipMeshLODs.SelectedIndex);
                    }
                }

                for(int i = 0; i < meshes.Count; i++)
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

            List<ShipMeshTag> tags = new List<ShipMeshTag>(); tags.Add(ShipMeshTag.DoScar);
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

            if (selectedShipMeshLOD < 3)
            {
                for (int i = selectedShipMeshLOD + 1; i <= 3; i++)
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

            listShipMeshes_SelectedIndexChanged(this, EventArgs.Empty);

            listShipMeshLODs.ClearSelected();

            listShipMeshLODs_SelectedIndexChanged(this, EventArgs.Empty);

            HWScene.FindBiggestMesh();
        }
        private void buttonShipMeshLODAdd_Click(object sender, EventArgs e)
        {
            if (selectedShipMesh == null)
                return;

            int lowestLOD = -1;
            for (int i = 0; i <= 3; i++)
                if (selectedShipMesh.LODMeshes[i].Count > 0)
                    lowestLOD = i;

            HWShipMeshLOD newLODMesh = new HWShipMeshLOD(new MeshData(new Vertex[0], new int[0], 0), Matrix4.Identity, HWMaterial.DefaultMaterial, selectedShipMesh, lowestLOD + 1);
            newLODMesh.Visible = true;

            listShipMeshes_SelectedIndexChanged(this, EventArgs.Empty);
            listShipMeshLODs.SelectedIndex = lowestLOD + 1;
        }
        //--------------------------------- ENGINE GLOW MESHES ---------------------------------//
        private void listEngineGlows_SelectedIndexChanged(object sender, EventArgs e)
        {
            listEngineGlowLODs.Items.Clear(); //Clear LOD list

            comboEngineGlowParent.SelectedIndex = 0; //Select root joint in combo box

            HWEngineGlow selectedEngineGlow = null;
            if (listEngineGlows.SelectedItem != null)
            {
                selectedEngineGlow = EngineGlowListItems[listEngineGlows.SelectedItem];
            }

            if (selectedEngineGlow != null)
            {
                //Select parent joint in combo box
                object item = JointComboItems[selectedEngineGlow.Parent];
                comboEngineGlowParent.SelectedItem = item; //Select parent joint in combo box

                //Fill LOD list
                for (int i = 0; i < 3; i++)
                    if (selectedEngineGlow.LODMeshes[i].Count > 0)
                    {
                        listEngineGlowLODs.Items.Add("LOD " + i);
                        if(selectedEngineGlow.LODMeshes[i][0].Visible)
                            listEngineGlowLODs.SetItemChecked(i, true);
                    }
            }
        }
        public void AddEngineGlow(HWEngineGlow glow)
        {
            object item = glow.Name;
            listEngineGlows.Items.Add(item);
            glow.EngineGlowListItem = item;
            EngineGlowListItems.Add(item, glow);
        }
        private void listEngineGlowLODs_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            HWEngineGlow selectedEngineGlow = EngineGlowListItems[listEngineGlows.SelectedItem];

            bool visible = false;
            if (e.NewValue == CheckState.Checked)
                visible = true;

            foreach (HWEngineGlowLOD engineGlowLOD in selectedEngineGlow.LODMeshes[e.Index])
                engineGlowLOD.Visible = visible;

            Renderer.InvalidateView();
            Renderer.Invalidate();
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

                comboEngineBurnParent.SelectedItem = jointParent.ComboItem;

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
            comboCollisionMeshParent.SelectedItem = JointComboItems[selectedCollisionMesh.Parent];
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
            HWCollisionMesh newCollisionMesh = new HWCollisionMesh(new MeshData(), Matrix4.Identity, HWJoint.Root);

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

            //Select parent joint in combo box
            object item = JointComboItems[selectedEngineShape.Parent];
            comboEngineShapeParent.SelectedItem = item; //Select parent joint in combo box
                
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
            if (Program.ShaderSettings != null) return;
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