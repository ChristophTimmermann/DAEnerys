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

        bool loaded = false;
        HWDockpath selectedDockpath;

        public Dictionary<object, HWShipMesh> ShipMeshListItems = new Dictionary<object, HWShipMesh>();
        public Dictionary<HWJoint, object> ShipMeshParentComboItems = new Dictionary<HWJoint, object>();

        public Dictionary<object, HWGoblinMesh> GoblinMeshListItems = new Dictionary<object, HWGoblinMesh>();
        public Dictionary<HWJoint, object> GoblinParentComboItems = new Dictionary<HWJoint, object>();

        public Dictionary<object, HWCollisionMesh> CollisionMeshListItems = new Dictionary<object, HWCollisionMesh>();
        public Dictionary<HWJoint, object> CollisionMeshParentComboItems = new Dictionary<HWJoint, object>();

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Program.GLControl = glControl;
            HWTexture.Init();
            Renderer.Init();
            EditorScene.Init();
            Application.Idle += glControl_Update;
            Console.WriteLine("OpenTK initialized.");
            GraphicsContext.CurrentContext.SwapInterval = 1;

            loaded = true;

            Clear();
        }

        void glControl_Update(object sender, EventArgs e)
        {
            if (!loaded)
                return;

            Program.Camera.Update();
        }

        private void glControl_Render(object sender, PaintEventArgs e)
        {
            if (!loaded)
                return;

            Renderer.Render();
        }

        private void glControl_Resize(object sender, EventArgs e)
        {
            if (!loaded)
                return;

            Renderer.Resize();
            Renderer.UpdateView();
            glControl.Invalidate();
        }

        private void Clear()
        {
            listShipMeshes.Items.Clear();
            comboShipMeshParent.Items.Clear();
            checkShipMeshDoScar.Checked = false;
            listShipMeshLODs.Items.Clear();
            ShipMeshListItems.Clear();
            ShipMeshParentComboItems.Clear();

            listGoblinMeshes.Items.Clear();
            comboGoblinMeshParent.Items.Clear();
            checkGoblinDoScar.Checked = false;
            GoblinMeshListItems.Clear();

            listCollisionMeshes.Items.Clear();
            comboCollisionMeshParent.Items.Clear();
            CollisionMeshListItems.Clear();

            jointsTree.Nodes.Clear();

            listBoxMarkers.Items.Clear();
            checkboxDrawMarkers.Checked = false;

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

            foreach(HWDockSegment segment in HWScene.DockSegments)
            {
                segment.Icosphere.Material = EditorIcosphere.RedMaterial;
            }

            HWScene.Clear();
            EditorScene.Clear();

            comboShipMeshParent.Items.Add("Root"); //Add root joint to possible ship mesh parents
            comboGoblinMeshParent.Items.Add("Root"); //Add root joint to possible goblin parents
            comboCollisionMeshParent.Items.Add("Root"); //Add root joint to possible collision mesh parents

            comboShipMeshParent.SelectedItem = 0;
            comboGoblinMeshParent.SelectedItem = 0;
            comboCollisionMeshParent.SelectedItem = 0;

            Renderer.UpdateMeshData();
            Renderer.UpdateView();
            glControl.Invalidate();
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
                glControl.Invalidate();
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

            //Add joint to goblin parents
            comboGoblinMeshParent.Items.Add(item);
            joint.ComboItemGoblinParent = item;
            GoblinParentComboItems.Add(joint, item);
        }

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
            glControl.Invalidate();
        }

        private void glControl_MouseDown(object sender, MouseEventArgs e)
        {
            Program.Camera.MouseDown(e);
        }

        private void glControl_MouseUp(object sender, MouseEventArgs e)
        {
            Program.Camera.MouseUp(e);
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
            glControl.Invalidate();
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            Program.settings = new Settings();
            Program.settings.Visible = true;
            Program.settings.Init();
        }

        private void checkboxDrawMarkers_CheckedChanged(object sender, EventArgs e)
        {
            foreach(HWMarker marker in HWScene.Markers)
            {
                foreach(EditorLine line in marker.Lines)
                {
                    line.Visible = checkboxDrawMarkers.Checked;
                }
            }

            Renderer.UpdateMeshData();
            Renderer.UpdateView();
            glControl.Invalidate();
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
            foreach(HWDockpath path in HWScene.Dockpaths)
            {
                if(path.Name == dockpathList.SelectedItem.ToString())
                {
                    dockpath = path;
                    break;
                }
            }

            if(dockpath != null)
            {
                foreach(string family in dockpath.Families)
                {
                    listDockpathFamilies.Items.Add(family);
                }

                foreach (string link in dockpath.Links)
                {
                    listDockpathLinks.Items.Add(link);
                }

                foreach (DockpathFlags flag in dockpath.Flags)
                {
                    switch(flag)
                    {
                        case DockpathFlags.EXIT:
                            checkDockpathExit.Checked = true;
                            break;
                        case DockpathFlags.LATCH:
                            checkDockpathLatch.Checked = true;
                            break;
                        case DockpathFlags.ANIM:
                            checkDockpathAnim.Checked = true;
                            break;
                        case DockpathFlags.AJAR:
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
            foreach (HWDockSegment segment in HWScene.DockSegments)
            {
                segment.Icosphere.Material = EditorIcosphere.RedMaterial;
            }

            HWDockSegment selectedSegment = selectedDockpath.Segments[trackBarDockpathSegments.Value];

            selectedSegment.Icosphere.Material = EditorIcosphere.YellowMaterial;

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

            foreach (DockSegmentFlags flag in selectedSegment.Flags)
            {
                switch (flag)
                {
                    case DockSegmentFlags.USEROT:
                        checkDockpathSegmentFlagUseRot.Checked = true;
                        break;
                    case DockSegmentFlags.PLAYER:
                        checkDockpathSegmentFlagPlayer.Checked = true;
                        break;
                    case DockSegmentFlags.QUEUE:
                        checkDockpathSegmentFlagQueue.Checked = true;
                        break;
                    case DockSegmentFlags.CLOSE:
                        checkDockpathSegmentFlagClose.Checked = true;
                        break;
                    case DockSegmentFlags.CLEARRES:
                        checkDockpathSegmentFlagClearRes.Checked = true;
                        break;
                    case DockSegmentFlags.CHECK:
                        checkDockpathSegmentFlagCheck.Checked = true;
                        break;
                    case DockSegmentFlags.UNFOCUS:
                        checkDockpathSegmentFlagUnfocus.Checked = true;
                        break;
                    case DockSegmentFlags.CLIP:
                        checkDockpathSegmentFlagClip.Checked = true;
                        break;
                }
            }

            glControl.Invalidate();
        }

        private void glControl_Enter(object sender, EventArgs e)
        {
            glControl.Focus();
        }

        private void glControl_Leave(object sender, EventArgs e)
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
                if (selectedShipMesh.Tags.Contains(ShipMeshTags.DOSCAR))
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
            glControl.Invalidate();
        }

        //--------------------------------- GOBLIN MESHES ---------------------------------//
        private void listGoblinMeshes_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkGoblinDoScar.Checked = false; //Reset do scar checkbox

            HWGoblinMesh selectedGoblinMesh = GoblinMeshListItems[listGoblinMeshes.SelectedItem];

            //Check do scar checkbox
            if (selectedGoblinMesh.Tags.Contains(GoblinMeshTags.DOSCAR))
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
                glControl.Invalidate();
            }
        }
        public void CheckGoblinVisible(HWGoblinMesh goblin, bool visible)
        {
            listGoblinMeshes.SetItemChecked(goblin.GoblinMeshListItemIndex, visible);
        }

        //--------------------------------- COLLISION MESHES ---------------------------------//
        private void listCollisionMeshes_SelectedIndexChanged(object sender, EventArgs e)
        {
            HWCollisionMesh selectedCollisionMesh = CollisionMeshListItems[listCollisionMeshes.SelectedItem];

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
            CollisionMeshListItems.Add(item, mesh);
        }
        private void listCollisionMeshes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (listCollisionMeshes.SelectedItem != null)
            {
                HWCollisionMesh selectedCollisionMesh = CollisionMeshListItems[listCollisionMeshes.SelectedItem];

                bool visible = false;
                if (e.NewValue == CheckState.Checked)
                    visible = true;

                selectedCollisionMesh.Mesh.Visible = visible;

                Renderer.UpdateMeshData();
                Renderer.UpdateView();
                glControl.Invalidate();
            }
        }

        private void buttonAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "Homeworld DAE Editor b" + BUILD + "\n\nDeveloped by Christoph (PayDay) Timmermann\nwith help from the Gearbox forums.\n\nUses\n - OpenTK\n - Assimp\n - Assimp.NET\n - FSharp\n - DevIL\n - DevILSharp", "Homeworld DAE Editor", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
