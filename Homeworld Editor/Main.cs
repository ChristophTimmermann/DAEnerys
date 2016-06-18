using System;
using System.Drawing;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL;
using Assimp;
using OpenTK;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Drawing.Imaging;
using OpenTK.Graphics;

namespace HomeworldDAEEditor
{
    public partial class Main : Form
    {
        int BUILD = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Build;

        public bool Loaded = false;
        HWDockpath selectedDockpath;
        HWNavLight selectedNavLight;

        public Dictionary<object, HWShipMesh> ShipMeshListItems = new Dictionary<object, HWShipMesh>();
        public Dictionary<HWJoint, object> ShipMeshParentComboItems = new Dictionary<HWJoint, object>();

        public Dictionary<object, HWEngineGlow> EngineGlowListItems = new Dictionary<object, HWEngineGlow>();
        public Dictionary<HWJoint, object> EngineGlowParentComboItems = new Dictionary<HWJoint, object>();

        public Dictionary<object, HWGoblinMesh> GoblinMeshListItems = new Dictionary<object, HWGoblinMesh>();
        public Dictionary<HWJoint, object> GoblinParentComboItems = new Dictionary<HWJoint, object>();

        public Dictionary<HWJoint, object> CollisionMeshParentComboItems = new Dictionary<HWJoint, object>();

        public Dictionary<object, HWMaterial> MaterialListItems = new Dictionary<object, HWMaterial>();

        public bool DrawNavLightRadius;

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
            Console.WriteLine("OpenTK initialized.");
            comboPerspectiveOrtho.SelectedIndex = 0;

            FPSCounter.LabelFPS = labelFPS;

            Loaded = true;
            Program.DeltaCounter.Start();

            HWData.ParseDataPaths();

            Clear();

            //Open DAE from arguments
            if (Program.OPEN_PATH != null)
                if (File.Exists(Program.OPEN_PATH))
                {
                    HWScene.LoadCollada(Program.OPEN_PATH);

                    Renderer.UpdateMeshData();
                    Renderer.UpdateView();
                    Program.GLControl.Invalidate();
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
            foreach(HWNavLight navLight in HWScene.NavLights)
            {
                if (navLight.Visible)
                    visibleNavLights++;

                navLight.Update();
            }

            //Only update render if it is needed
            if(visibleNavLights > 0)
                Program.GLControl.Invalidate();
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
            Renderer.UpdateView();
            Program.GLControl.Invalidate();
        }

        private void Clear()
        {
            listShipMeshes.Items.Clear();
            comboShipMeshParent.Items.Clear();
            checkShipMeshDoScar.Checked = false;
            listShipMeshLODs.Items.Clear();
            ShipMeshListItems.Clear();
            ShipMeshParentComboItems.Clear();

            listEngineGlows.Items.Clear();
            comboEngineGlowParent.Items.Clear();
            listEngineGlowLODs.Items.Clear();
            EngineGlowListItems.Clear();
            EngineGlowParentComboItems.Clear();

            listGoblinMeshes.Items.Clear();
            comboGoblinMeshParent.Items.Clear();
            checkGoblinDoScar.Checked = false;
            GoblinMeshListItems.Clear();

            listCollisionMeshes.Items.Clear();
            comboCollisionMeshParent.Items.Clear();

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

            foreach(HWDockSegment segment in HWScene.DockSegments)
            {
                segment.Icosphere.Color = new Vector3(1, 0, 0);
            }

            HWScene.Clear();
            EditorScene.Clear();

            comboShipMeshParent.Items.Add("Root"); //Add root joint to possible ship mesh parents
            comboGoblinMeshParent.Items.Add("Root"); //Add root joint to possible goblin parents
            comboCollisionMeshParent.Items.Add("Root"); //Add root joint to possible collision mesh parents
            comboEngineGlowParent.Items.Add("Root"); //Add root joint to possible engine glow parents

            comboMaterialFormat.Items.Add("DXT1");
            comboMaterialFormat.Items.Add("DXT3");
            comboMaterialFormat.Items.Add("DXT5");
            comboMaterialFormat.Items.Add("8888");

            comboShipMeshParent.SelectedItem = 0;
            comboGoblinMeshParent.SelectedItem = 0;
            comboCollisionMeshParent.SelectedItem = 0;
            comboEngineGlowParent.SelectedItem = 0;

            Renderer.UpdateMeshData();
            Renderer.UpdateView();
            Program.GLControl.Invalidate();
        }

        //--------------------------------------------------------------------------------------------------------------//
        //-------------------------------------------------- GUI STUFF -------------------------------------------------//
        //--------------------------------------------------------------------------------------------------------------//
        private void buttonOpen_Click(object sender, EventArgs e)
        {
            DialogResult result = openColladaDialog.ShowDialog();
            if(result == DialogResult.OK)
            {
                Clear();
                HWScene.LoadCollada(openColladaDialog.FileName);

                Renderer.UpdateMeshData();
                Renderer.UpdateView();
                Program.GLControl.Invalidate();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            DialogResult result = saveColladaDialog.ShowDialog();
            if(result == DialogResult.OK)
            {
                HWScene.SaveCollada(saveColladaDialog.FileName);
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Idle -= glControl_Update;
            HWTexture.Close();
            GraphicsContext.CurrentContext.Dispose();
            Settings.SaveSettings();
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
                    parent.Parent.TreeNode.Nodes[parent.TreeNode.Index].Nodes.Add(newNode);
                    joint.TreeNode = newNode;
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

            //Add joint to goblin parents
            comboGoblinMeshParent.Items.Add(item);
            joint.ComboItemGoblinParent = item;
            GoblinParentComboItems.Add(joint, item);
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

            foreach (HWDockpath dockpath in HWScene.Dockpaths)
            {
                if (dockpath.Name == dockpathList.Items[e.Index].ToString())
                {
                    dockpath.Visible = newValue;
                }
            }

            Renderer.UpdateMeshData();
            Renderer.UpdateView();
            Program.GLControl.Invalidate();
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
            }

            //Reset line colors
            foreach (EditorLine line in selectedDockpath.Lines)
            {
                line.StartColor = Color.Red;
                line.EndColor = Color.Red;
            }

            HWDockSegment selectedSegment = selectedDockpath.Segments[trackBarDockpathSegments.Value];

            selectedSegment.Icosphere.Color = new Vector3(1, 1, 0);

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

            Renderer.UpdateMeshData();
            Program.GLControl.Invalidate();
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

            Renderer.UpdateMeshData();
            Renderer.UpdateView();
            Program.GLControl.Invalidate();
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
                        if(navLight.RenderIcosphere != null)
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

            Renderer.UpdateMeshData();
            Renderer.UpdateView();
            Program.GLControl.Invalidate();
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

            Renderer.UpdateMeshData();
            Renderer.UpdateView();
            Program.GLControl.Invalidate();
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
            Program.Camera.KeyDown(e);
        }

        private void jointsTree_AfterCheck(object sender, TreeViewEventArgs e)
        {
            bool newValue = e.Node.Checked;

            //TODO: Optimize
            foreach(HWJoint joint in HWScene.Joints)
            {
                if(joint.TreeNode == e.Node)
                {
                    joint.EditorJoint.Visible = newValue;
                    break;
                }
            }

            Renderer.UpdateMeshData();
            Renderer.UpdateView();
            Program.GLControl.Invalidate();
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            Program.settings = new Settings();
            Program.settings.Visible = true;
            Program.settings.Init();
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
            checkShipMeshDoScar.Checked = false; //Reset do scar checkbox

            HWShipMesh selectedShipMesh = null;
            if (listShipMeshes.SelectedItem != null)
            {
                selectedShipMesh = ShipMeshListItems[listShipMeshes.SelectedItem];
            }

            if (selectedShipMesh != null)
            {
                //Check do scar checkbox
                if (selectedShipMesh.Tags.Contains(ShipMeshTag.DOSCAR))
                    checkShipMeshDoScar.Checked = true;

                //Select parent joint in combo box
                if (selectedShipMesh.Parent != null) //If ship mesh has a parent joint
                {
                    object item = ShipMeshParentComboItems[selectedShipMesh.Parent];
                    comboShipMeshParent.SelectedItem = item; //Select parent joint in combo box
                }
                else
                    comboShipMeshParent.SelectedIndex = 0; //Select root joint in combo box

                //Fill LOD list
                if (selectedShipMesh.LOD0Meshes.Count > 0) //If ship mesh has an LOD0
                    listShipMeshLODs.Items.Add("LOD 0");
                if (selectedShipMesh.LOD1Meshes.Count > 0) //If ship mesh has an LOD1
                    listShipMeshLODs.Items.Add("LOD 1");
                if (selectedShipMesh.LOD2Meshes.Count > 0) //If ship mesh has an LOD2
                    listShipMeshLODs.Items.Add("LOD 2");

                //Check LOD checkboxes if visible
                if (selectedShipMesh.LOD0Meshes.Count > 0)
                {
                    if (selectedShipMesh.LOD0Meshes[0].Mesh.Visible)
                        listShipMeshLODs.SetItemChecked(0, true);
                }
                if (selectedShipMesh.LOD1Meshes.Count > 0)
                {
                    if (selectedShipMesh.LOD1Meshes[0].Mesh.Visible)
                        listShipMeshLODs.SetItemChecked(1, true);
                }
                if (selectedShipMesh.LOD2Meshes.Count > 0)
                {
                    if (selectedShipMesh.LOD2Meshes[0].Mesh.Visible)
                        listShipMeshLODs.SetItemChecked(2, true);
                }
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

            switch (e.Index)
            {
                case 0:
                    {
                        foreach (HWShipMeshLOD shipMeshLOD in selectedShipMesh.LOD0Meshes)
                        {
                            shipMeshLOD.Mesh.Visible = visible;
                        }
                        break;
                    }
                case 1:
                    {
                        foreach (HWShipMeshLOD shipMeshLOD in selectedShipMesh.LOD1Meshes)
                        {
                            shipMeshLOD.Mesh.Visible = visible;
                        }
                        break;
                    }
                case 2:
                    {
                        foreach (HWShipMeshLOD shipMeshLOD in selectedShipMesh.LOD2Meshes)
                        {
                            shipMeshLOD.Mesh.Visible = visible;
                        }
                        break;
                    }
            }

            Renderer.UpdateMeshData();
            Renderer.UpdateView();
            Program.GLControl.Invalidate();
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
                if (selectedEngineGlow.LOD0Meshes.Count > 0) //If engine glow has an LOD0
                    listEngineGlowLODs.Items.Add("LOD 0");
                if (selectedEngineGlow.LOD1Meshes.Count > 0) //If engine glow has an LOD1
                    listEngineGlowLODs.Items.Add("LOD 1");
                if (selectedEngineGlow.LOD2Meshes.Count > 0) //If engine glow has an LOD2
                    listEngineGlowLODs.Items.Add("LOD 2");

                //Check LOD checkboxes if visible
                if (selectedEngineGlow.LOD0Meshes.Count > 0)
                {
                    if (selectedEngineGlow.LOD0Meshes[0].Mesh.Visible)
                        listEngineGlowLODs.SetItemChecked(0, true);
                }
                if (selectedEngineGlow.LOD1Meshes.Count > 0)
                {
                    if (selectedEngineGlow.LOD1Meshes[0].Mesh.Visible)
                        listEngineGlowLODs.SetItemChecked(1, true);
                }
                if (selectedEngineGlow.LOD2Meshes.Count > 0)
                {
                    if (selectedEngineGlow.LOD2Meshes[0].Mesh.Visible)
                        listEngineGlowLODs.SetItemChecked(2, true);
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

            switch (e.Index)
            {
                case 0:
                    {
                        foreach (HWEngineGlowLOD engineGlowLOD in selectedEngineGlow.LOD0Meshes)
                        {
                            engineGlowLOD.Mesh.Visible = visible;
                        }
                        break;
                    }
                case 1:
                    {
                        foreach (HWEngineGlowLOD engineGlowLOD in selectedEngineGlow.LOD1Meshes)
                        {
                            engineGlowLOD.Mesh.Visible = visible;
                        }
                        break;
                    }
                case 2:
                    {
                        foreach (HWEngineGlowLOD engineGlowLOD in selectedEngineGlow.LOD2Meshes)
                        {
                            engineGlowLOD.Mesh.Visible = visible;
                        }
                        break;
                    }
            }

            Renderer.UpdateMeshData();
            Renderer.UpdateView();
            Program.GLControl.Invalidate();
        }

        //--------------------------------- GOBLIN MESHES ---------------------------------//
        private void listGoblinMeshes_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkGoblinDoScar.Checked = false; //Reset do scar checkbox

            HWGoblinMesh selectedGoblinMesh = GoblinMeshListItems[listGoblinMeshes.SelectedItem];

            //Check do scar checkbox
            if (selectedGoblinMesh.Tags.Contains(GoblinMeshTag.DOSCAR))
                checkGoblinDoScar.Checked = true;

            //Select parent joint in combo box
            if (selectedGoblinMesh.Parent != null) //If ship mesh has a parent joint
            {
                object item = GoblinParentComboItems[selectedGoblinMesh.Parent];
                comboGoblinMeshParent.SelectedItem = item; //Select parent joint in combo box
            }
            else
                comboGoblinMeshParent.SelectedIndex = 0; //Select root joint in combo box
        }
        public void AddGoblinMesh(HWGoblinMesh mesh)
        {
            object item = mesh.Name;
            listGoblinMeshes.Items.Add(item);
            mesh.GoblinMeshListItemIndex = listGoblinMeshes.Items.Count - 1;
            GoblinMeshListItems.Add(item, mesh);
        }
        private void listGoblinMeshes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (listGoblinMeshes.SelectedItem != null)
            {
                HWGoblinMesh selectedGoblinMesh = GoblinMeshListItems[listGoblinMeshes.SelectedItem];

                bool visible = false;
                if (e.NewValue == CheckState.Checked)
                    visible = true;

                foreach (HWMesh mesh in selectedGoblinMesh.Meshes)
                {
                    mesh.Visible = visible;
                }

                Renderer.UpdateMeshData();
                Renderer.UpdateView();
                Program.GLControl.Invalidate();
            }
        }
        public void CheckGoblinVisible(HWGoblinMesh goblin, bool visible)
        {
            listGoblinMeshes.SetItemChecked(goblin.GoblinMeshListItemIndex, visible);
        }

        //--------------------------------- COLLISION MESHES ---------------------------------//
        private void listCollisionMeshes_SelectedIndexChanged(object sender, EventArgs e)
        {
            HWCollisionMesh selectedCollisionMesh = null;
            //Has to be done with a loop, because of multiple collision meshes with the same name
            foreach (HWCollisionMesh collisionMesh in HWScene.CollisionMeshes)
            {
                if(collisionMesh.CollisionMeshListItemIndex == listCollisionMeshes.SelectedIndex)
                {
                    selectedCollisionMesh = collisionMesh;
                    break;
                }
            }

            if(selectedCollisionMesh == null)
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

                selectedCollisionMesh.Mesh.Visible = visible;

                Renderer.UpdateMeshData();
                Renderer.UpdateView();
                Program.GLControl.Invalidate();
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

                material.MaterialListItem = item;
                MaterialListItems.Add(item, material);
            }
        }

        private void trackBarThrusterStrength_Scroll(object sender, EventArgs e)
        {
            Renderer.ThrusterInterpolation = (float)trackBarThrusterStrength.Value / 100;
            foreach(HWEngineGlow engineGlow in HWScene.EngineGlows)
            {
                foreach(HWEngineGlowLOD engineGlowLOD in engineGlow.LOD0Meshes)
                    engineGlowLOD.UpdateEngineStrength();
                foreach (HWEngineGlowLOD engineGlowLOD in engineGlow.LOD1Meshes)
                    engineGlowLOD.UpdateEngineStrength();
                foreach (HWEngineGlowLOD engineGlowLOD in engineGlow.LOD2Meshes)
                    engineGlowLOD.UpdateEngineStrength();
            }

            Renderer.UpdateView();
            Program.GLControl.Invalidate();
        }

        private void buttonAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "Homeworld DAE Editor b" + BUILD + "\n\nDeveloped by Christoph (PayDay) Timmermann\nwith help from the Gearbox forums.\n\nEditor icons made by SumoChick.\n\nUses\n - OpenTK\n - Assimp\n - Assimp.NET\n - FSharp\n - DevIL\n - DevILSharp", "Homeworld DAE Editor", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}
