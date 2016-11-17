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
        HWDockpath selectedDockpath;
        HWNavLight selectedNavLight;

        public Dictionary<object, HWShipMesh> ShipMeshListItems = new Dictionary<object, HWShipMesh>();
        public Dictionary<HWJoint, object> ShipMeshParentComboItems = new Dictionary<HWJoint, object>();
        private Label[] ShipMeshLODMaterialLabels = new Label[MAX_MATERIALS_ON_MESH];
        private ComboBox[] ShipMeshLODMaterialComboBoxes = new ComboBox[MAX_MATERIALS_ON_MESH];
        private HWShipMesh selectedShipMesh;
        private int selectedShipMeshLOD;

        public Dictionary<object, HWEngineGlow> EngineGlowListItems = new Dictionary<object, HWEngineGlow>();
        public Dictionary<HWJoint, object> EngineGlowParentComboItems = new Dictionary<HWJoint, object>();

        public Dictionary<HWJoint, object> CollisionMeshParentComboItems = new Dictionary<HWJoint, object>();
        public Dictionary<HWJoint, object> EngineShapeParentComboItems = new Dictionary<HWJoint, object>();

        public Dictionary<object, HWMaterial> MaterialListItems = new Dictionary<object, HWMaterial>();

        public bool DrawNavLightRadius;

        private bool problemsVisible;
        private bool ignoreShipMeshDoScarCheck;

        const int MAX_MATERIALS_ON_MESH = 8;

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

            HWBadge.LoadSavedBadge();

            gridProblems.RowTemplate.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            gridProblems.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            gridProblems.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            //Create ship mesh lod material selection
            for(int i = 0; i < MAX_MATERIALS_ON_MESH; i++)
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
                ShipMeshLODMaterialComboBoxes[i].Size = new Size(192, 21);
                ShipMeshLODMaterialComboBoxes[i].DropDownStyle = ComboBoxStyle.DropDownList;
                ShipMeshLODMaterialComboBoxes[i].Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                ShipMeshLODMaterialComboBoxes[i].SelectedIndexChanged += new EventHandler(OnShipMeshLODMaterialChanged);
                ShipMeshLODMaterialComboBoxes[i].Visible = false;
            }

            Clear();

            if(Updater.CheckForUpdatesOnStart)
                Updater.CheckForUpdates();

            //Open DAE from arguments
            if (Program.OPEN_PATH != null)
                if (File.Exists(Program.OPEN_PATH))
                {
                    HWScene.LoadCollada(Program.OPEN_PATH);
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
            foreach (HWNavLight navLight in HWScene.NavLights)
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
            listShipMeshes.Items.Clear();
            comboShipMeshParent.Items.Clear();
            checkShipMeshDoScar.Checked = false;
            listShipMeshLODs.Items.Clear();
            ShipMeshListItems.Clear();
            ShipMeshParentComboItems.Clear();

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
            EngineGlowParentComboItems.Clear();

            listCollisionMeshes.Items.Clear();
            comboCollisionMeshParent.Items.Clear();

            listEngineShapes.Items.Clear();
            comboEngineShapeParent.Items.Clear();

            listMaterials.Items.Clear();
            MaterialListItems.Clear();
            boxMaterialShader.Clear();
            listMaterialTextures.Items.Clear();
            comboMaterialFormat.Items.Clear();

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

            foreach (HWDockSegment segment in HWScene.DockSegments)
            {
                segment.Icosphere.Color = new Vector3(1, 0, 0);
            }

            HWScene.Clear();
            EditorScene.Clear();

            comboShipMeshParent.Items.Add("Root"); //Add root joint to possible ship mesh parents
            comboCollisionMeshParent.Items.Add("Root"); //Add root joint to possible collision mesh parents
            comboEngineGlowParent.Items.Add("Root"); //Add root joint to possible engine glow parents
            comboEngineShapeParent.Items.Add("Root"); //Add root joint to possible engine shape parents

            comboMaterialFormat.Items.Add("DXT1");
            comboMaterialFormat.Items.Add("DXT3");
            comboMaterialFormat.Items.Add("DXT5");
            comboMaterialFormat.Items.Add("8888");

            comboShipMeshParent.SelectedItem = 0;
            comboCollisionMeshParent.SelectedItem = 0;
            comboEngineGlowParent.SelectedItem = 0;
            comboEngineShapeParent.SelectedItem = 0;

            problemsVisible = false;
            splitContainer2.Panel2Collapsed = true;
            Problem.Problems.Clear();
            gridProblems.Rows.Clear();

            this.Text = "DAEnerys";

            Renderer.InvalidateMeshData();
            Renderer.InvalidateView();
            Renderer.Invalidate();
        }

        //--------------------------------------------------------------------------------------------------------------//
        //-------------------------------------------------- GUI STUFF -------------------------------------------------//
        //--------------------------------------------------------------------------------------------------------------//
        private void buttonOpen_Click(object sender, EventArgs e)
        {
            DialogResult result = openColladaDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                Clear();
                HWScene.LoadCollada(openColladaDialog.FileName);
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
            {
                jointsTree.Nodes.Add(newNode);
                joint.TreeNode = newNode;
            }
            else
            {
                if (parent.Parent != null)
                {
                    HWJoint parentJoint = parent.Parent as HWJoint;

                    if (parentJoint != null)
                    {
                        parentJoint.TreeNode.Nodes[parent.TreeNode.Index].Nodes.Add(newNode);
                        joint.TreeNode = newNode;
                    }
                    else
                    {
                        jointsTree.Nodes[parent.TreeNode.Index].Nodes.Add(newNode);
                        joint.TreeNode = newNode;
                    }
                }
                else
                {
                    jointsTree.Nodes[parent.TreeNode.Index].Nodes.Add(newNode);
                    joint.TreeNode = newNode;
                }
            }

            //Add joint to ship mesh parents
            object item = joint.Name;
            comboShipMeshParent.Items.Add(item);
            joint.ComboItemShipMeshParent = item;
            ShipMeshParentComboItems.Add(joint, item);

            //Add joint to engine glow parents
            comboEngineGlowParent.Items.Add(item);
            joint.ComboItemEngineGlowParent = item;
            EngineGlowParentComboItems.Add(joint, item);

            //Add joint to engine shape parents
            comboEngineShapeParent.Items.Add(item);
            joint.ComboItemEngineShapeParent = item;
            EngineShapeParentComboItems.Add(joint, item);
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

            foreach (HWDockSegment segment in HWScene.DockSegments)
            {
                segment.ToleranceIcosphere.Visible = false;
            }

            foreach (HWDockpath dockpath in HWScene.Dockpaths)
            {
                if (dockpath.Name == dockpathList.Items[e.Index].ToString())
                    dockpath.Visible = newValue;
            }

            trackBarDockpathSegments_Scroll(null, EventArgs.Empty);

            Renderer.InvalidateMeshData();
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
            checkDockpathSegmentFlagUseRot.Checked = false;
            checkDockpathSegmentFlagPlayer.Checked = false;
            checkDockpathSegmentFlagQueue.Checked = false;
            checkDockpathSegmentFlagClose.Checked = false;
            checkDockpathSegmentFlagClearRes.Checked = false;
            checkDockpathSegmentFlagCheck.Checked = false;
            checkDockpathSegmentFlagUnfocus.Checked = false;
            checkDockpathSegmentFlagClip.Checked = false;

            foreach (HWDockSegment segment in HWScene.DockSegments)
                segment.ToleranceIcosphere.Visible = false;

            HWDockpath dockpath = null;
            foreach (HWDockpath path in HWScene.Dockpaths)
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
                        case DockpathFlag.EXIT:
                            checkDockpathExit.Checked = true;
                            break;
                        case DockpathFlag.LATCH:
                            checkDockpathLatch.Checked = true;
                            break;
                        case DockpathFlag.ANIM:
                            checkDockpathAnim.Checked = true;
                            break;
                        case DockpathFlag.AJAR:
                            checkDockpathAjar.Checked = true;
                            break;
                    }
                }

                selectedDockpath = dockpath;
            }

            trackBarDockpathSegments.Maximum = dockpath.Segments.Count - 1;
            trackBarDockpathSegments_Scroll(null, EventArgs.Empty);
        }
        private void trackBarDockpathSegments_Scroll(object sender, EventArgs e)
        {
            //Reset segment colors
            foreach (HWDockSegment segment in HWScene.DockSegments)
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
                    case DockSegmentFlag.USEROT:
                        checkDockpathSegmentFlagUseRot.Checked = true;
                        break;
                    case DockSegmentFlag.PLAYER:
                        checkDockpathSegmentFlagPlayer.Checked = true;
                        break;
                    case DockSegmentFlag.QUEUE:
                        checkDockpathSegmentFlagQueue.Checked = true;
                        break;
                    case DockSegmentFlag.CLOSE:
                        checkDockpathSegmentFlagClose.Checked = true;
                        break;
                    case DockSegmentFlag.CLEARRES:
                        checkDockpathSegmentFlagClearRes.Checked = true;
                        break;
                    case DockSegmentFlag.CHECK:
                        checkDockpathSegmentFlagCheck.Checked = true;
                        break;
                    case DockSegmentFlag.UNFOCUS:
                        checkDockpathSegmentFlagUnfocus.Checked = true;
                        break;
                    case DockSegmentFlag.CLIP:
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

            foreach (HWNavLight navLight in HWScene.NavLights)
            {
                if (navLight.Name == navLightList.Items[e.Index].ToString())
                {
                    navLight.Visible = newValue;
                }
            }

            Renderer.InvalidateMeshData();
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
                foreach (HWNavLight light in HWScene.NavLights)
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
                        case NavLightFlag.SPRITE:
                            checkNavLightFlagSprite.Checked = true;
                            break;
                        case NavLightFlag.HIGHEND:
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
                foreach (HWNavLight navLight in HWScene.NavLights)
                {
                    if (navLight.Visible)
                        if (navLight.RenderIcosphere != null)
                            navLight.RenderIcosphere.Visible = true;
                }
            }
            else
            {
                foreach (HWNavLight navLight in HWScene.NavLights)
                {
                    if (navLight.RenderIcosphere != null)
                        navLight.RenderIcosphere.Visible = false;
                }
            }

            Renderer.InvalidateMeshData();
            Renderer.InvalidateView();
            Renderer.Invalidate();
        }

        private void checkboxDrawMarkers_CheckedChanged(object sender, EventArgs e)
        {
            foreach (HWMarker marker in HWScene.Markers)
            {
                foreach (EditorLine line in marker.Lines)
                {
                    line.Visible = checkboxDrawMarkers.Checked;
                }
            }

            Renderer.InvalidateMeshData();
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

            Renderer.InvalidateMeshData();
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
            listShipMeshLODs.Items.Clear(); //Clear LOD list

            ignoreShipMeshDoScarCheck = true;
            checkShipMeshDoScar.Checked = false; //Reset do scar checkbox
            ignoreShipMeshDoScarCheck = false;
            checkShipMeshDoScar.Enabled = false; //Disable do scar checkbox

            comboShipMeshParent.Enabled = false; //Disable parent combo box

            foreach (Label label in ShipMeshLODMaterialLabels)
                label.Visible = false;
            foreach (ComboBox comboBox in ShipMeshLODMaterialComboBoxes)
                comboBox.Visible = false;

            if (listShipMeshes.SelectedItem != null)
            {
                selectedShipMesh = ShipMeshListItems[listShipMeshes.SelectedItem];
            }

            if (selectedShipMesh != null)
            {
                //Check do scar checkbox
                if (selectedShipMesh.Tags.Contains(ShipMeshTag.DOSCAR))
                    checkShipMeshDoScar.Checked = true;

                ignoreShipMeshDoScarCheck = true;
                checkShipMeshDoScar.Enabled = true; //Enable do scar checkbox
                ignoreShipMeshDoScarCheck = false;

                //Select parent joint in combo box
                if (selectedShipMesh.Parent != null) //If ship mesh has a parent joint
                {
                    object item = ShipMeshParentComboItems[selectedShipMesh.Parent];
                    comboShipMeshParent.SelectedItem = item; //Select parent joint in combo box
                }
                else
                    comboShipMeshParent.SelectedIndex = 0; //Select root joint in combo box

                comboShipMeshParent.Enabled = true; //Enable parent combo box

                //Fill LOD list
                for (int i = 0; i < 3; i++)
                    if (selectedShipMesh.LODMeshes[i].Count > 0)
                    {
                        listShipMeshLODs.Items.Add("LOD " + i);
                        if (selectedShipMesh.LODMeshes[i][0].Visible)
                            listShipMeshLODs.SetItemChecked(i, true);
                    }

                if(selectedShipMesh.LODMeshes[0].Count > 0)
                    listShipMeshLODs.SelectedIndex = 0;
            }
        }
        public void AddShipMesh(HWShipMesh mesh)
        {
            object item = mesh.Name;
            listShipMeshes.Items.Add(item);
            mesh.ShipMeshListItem = item;
            ShipMeshListItems.Add(item, mesh);
        }
        private void listShipMeshLODs_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            HWShipMesh selectedShipMesh = ShipMeshListItems[listShipMeshes.SelectedItem];

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
        private void comboShipMeshParent_SelectedIndexChanged(object sender, EventArgs e)
        {
            HWShipMesh selectedShipMesh = null;
            if (listShipMeshes.SelectedItem != null)
            {
                selectedShipMesh = ShipMeshListItems[listShipMeshes.SelectedItem];
            }

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

            selectedShipMesh.Tags.Remove(ShipMeshTag.DOSCAR);
            if (checkShipMeshDoScar.Checked)
                selectedShipMesh.Tags.Add(ShipMeshTag.DOSCAR);
        }
        private void listShipMeshLODs_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Label label in ShipMeshLODMaterialLabels)
                label.Visible = false;
            foreach (ComboBox comboBox in ShipMeshLODMaterialComboBoxes)
                comboBox.Visible = false;

            selectedShipMeshLOD = listShipMeshLODs.SelectedIndex;
            List<HWShipMeshLOD> lodMeshes = selectedShipMesh.LODMeshes[selectedShipMeshLOD];

            int materialCount = 0;
            foreach (HWShipMeshLOD lodMesh in lodMeshes)
                if (lodMesh.Material != null)
                    materialCount++;

            for(int i = 0; i < materialCount; i++)
            {
                ShipMeshLODMaterialLabels[i].Visible = true;
                ShipMeshLODMaterialComboBoxes[i].Visible = true;

                ShipMeshLODMaterialComboBoxes[i].SelectedItem = lodMeshes[i].Material.MaterialListItem;
            }
        }
        private void OnShipMeshLODMaterialChanged(object sender, EventArgs e)
        {
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
                lodMeshes[materialIndex].Material = MaterialListItems.Values.ElementAt(selectedIndex);
            else
                ShipMeshLODMaterialComboBoxes[materialIndex].SelectedItem = lodMeshes[materialIndex].Material.MaterialListItem;
        }
        private void buttonShipMeshLODExport_Click(object sender, EventArgs e)
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

            saveObjDialog.FileName = OpenedFile + "_" + meshes[0].Name + "_LOD" + listShipMeshLODs.SelectedIndex;
            DialogResult result = saveObjDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                ObjExporter.ExportToFile(saveObjDialog.FileName, meshes);
            }
        }
        private void buttonShipMeshLODImport_Click(object sender, EventArgs e)
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
                Mesh[] newMeshes = ObjImporter.ImportFromFile(openObjDialog.FileName);
                if (newMeshes.Length > meshes.Count)
                {
                    for (int i = newMeshes.Length - (newMeshes.Length - meshes.Count); i < newMeshes.Length; i++)
                    {
                        HWShipMeshLOD newLOD = new HWShipMeshLOD(newMeshes[i], selectedShipMesh, listShipMeshLODs.SelectedIndex);
                        newLOD.Parent = meshes[0].Parent;
                    }
                }

                for(int i = 0; i < meshes.Count; i++)
                {
                    if (newMeshes.Length - 1 >= i)
                    {
                        HWMaterial material = meshes[i].Material;
                        meshes[i].SetMesh(newMeshes[i]);
                        meshes[i].CalculateBoundingBox();
                        meshes[i].Material = material;
                    }
                    else //Remove old mesh
                    {
                        meshes[i].Destroy();
                    }
                }

                listShipMeshLODs_SelectedIndexChanged(listShipMeshLODs, EventArgs.Empty);
                HWScene.CalibrateSettings();
            }
        }
        //--------------------------------- ENGINE GLOW MESHES ---------------------------------//
        private void listEngineGlows_SelectedIndexChanged(object sender, EventArgs e)
        {
            listEngineGlowLODs.Items.Clear(); //Clear LOD list

            HWEngineGlow selectedEngineGlow = null;
            if (listEngineGlows.SelectedItem != null)
            {
                selectedEngineGlow = EngineGlowListItems[listEngineGlows.SelectedItem];
            }

            if (selectedEngineGlow != null)
            {
                //Select parent joint in combo box
                if (selectedEngineGlow.Parent != null) //If engine glow has a parent joint
                {
                    object item = EngineGlowParentComboItems[selectedEngineGlow.Parent];
                    comboEngineGlowParent.SelectedItem = item; //Select parent joint in combo box
                }
                else
                    comboEngineGlowParent.SelectedIndex = 0; //Select root joint in combo box

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

        //--------------------------------- COLLISION MESHES ---------------------------------//
        private void listCollisionMeshes_SelectedIndexChanged(object sender, EventArgs e)
        {
            HWCollisionMesh selectedCollisionMesh = null;
            //Has to be done with a loop, because of multiple collision meshes with the same name
            foreach (HWCollisionMesh collisionMesh in HWScene.CollisionMeshes)
            {
                if (collisionMesh.CollisionMeshListItemIndex == listCollisionMeshes.SelectedIndex)
                {
                    selectedCollisionMesh = collisionMesh;
                    break;
                }
            }

            if (selectedCollisionMesh == null)
            {
                return;
            }

            //Select parent joint in combo box
            if (selectedCollisionMesh.Parent != null) //If collision mesh has a parent joint
            {
                object item = CollisionMeshParentComboItems[selectedCollisionMesh.Parent];
                comboCollisionMeshParent.SelectedItem = item; //Select parent joint in combo box
            }
            else
                comboCollisionMeshParent.SelectedIndex = 0; //Select root joint in combo box
        }
        public void AddCollisionMesh(HWCollisionMesh mesh)
        {
            object item = mesh.Name;
            listCollisionMeshes.Items.Add(item);
            mesh.CollisionMeshListItemIndex = listCollisionMeshes.Items.Count - 1;
        }
        private void listCollisionMeshes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (listCollisionMeshes.SelectedItem != null)
            {
                HWCollisionMesh selectedCollisionMesh = null;
                //Has to be done with a loop, because of multiple collision meshes with the same name
                foreach (HWCollisionMesh collisionMesh in HWScene.CollisionMeshes)
                {
                    if (collisionMesh.CollisionMeshListItemIndex == listCollisionMeshes.SelectedIndex)
                    {
                        selectedCollisionMesh = collisionMesh;
                        break;
                    }
                }

                bool visible = false;
                if (e.NewValue == CheckState.Checked)
                    visible = true;

                selectedCollisionMesh.Visible = visible;

                Renderer.InvalidateMeshData();
                Renderer.InvalidateView();
                Renderer.Invalidate();
            }
        }

        //--------------------------------- ENGINE SHAPES ---------------------------------//
        private void listEngineShapes_SelectedIndexChanged(object sender, EventArgs e)
        {
            HWEngineShape selectedEngineShape = null;
            //Has to be done with a loop, because of multiple engine shapes with the same name
            foreach (HWEngineShape engineShape in HWScene.EngineShapes)
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
            if (selectedEngineShape.Parent != null) //If engine shape has a parent joint
            {
                object item = EngineShapeParentComboItems[selectedEngineShape.Parent];
                comboEngineShapeParent.SelectedItem = item; //Select parent joint in combo box
            }
            else
                comboEngineShapeParent.SelectedIndex = 0; //Select root joint in combo box
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
                foreach (HWEngineShape engineShape in HWScene.EngineShapes)
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

                Renderer.InvalidateMeshData();
                Renderer.InvalidateView();
                Renderer.Invalidate();
            }
        }

        //----------------------------------- MATERIALS ----------------------------------//
        private void listMaterials_SelectedIndexChanged(object sender, EventArgs e)
        {
            listMaterialTextures.Items.Clear();

            if (listMaterials.SelectedItem == null)
                return;

            HWMaterial selectedMaterial = MaterialListItems[listMaterials.SelectedItem];

            //Set shader name
            boxMaterialShader.Text = selectedMaterial.Shader;

            //Fill texture list
            foreach (HWImage image in selectedMaterial.Images)
            {
                listMaterialTextures.Items.Add(image.Name);
            }

            //Set texture format
            if (selectedMaterial.Images.Count > 0)
            {
                if (selectedMaterial.Images[0] != null)
                {
                    comboMaterialFormat.SelectedIndex = (int)selectedMaterial.Format;
                }
            }
        }
        public void AddMaterial(HWMaterial material)
        {
            if (!listMaterials.Items.Contains(material.Name)) //If material not already in list
            {
                object item = material.Name;
                listMaterials.Items.Add(item);

                foreach (ComboBox comboBox in ShipMeshLODMaterialComboBoxes)
                    comboBox.Items.Add(item);

                material.MaterialListItem = item;
                MaterialListItems.Add(item, material);
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
            MessageBox.Show(this, "DAEnerys b" + BUILD + "\n\nDeveloped by Christoph (PayDay) Timmermann and\nAnthony (radar3301) Lofthouse (aka. ajlsunrise33)\nwith help from the Gearbox forums.\n\nEditor icons made by SumoChick.\n\nUses\n - OpenTK\n - Assimp\n - Assimp.NET\n - FSharp\n - DevIL\n - DevILSharp\n - AlphaColorDialog", "DAEnerys", MessageBoxButtons.OK, MessageBoxIcon.Information);
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