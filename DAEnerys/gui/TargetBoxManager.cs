using OpenTK;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAEnerys
{
    public static class TargetBoxManager
    {
        private static CheckedListBox list;

        private static Button buttonAdd;
        private static Button buttonRemove;

        private static NumericUpDown numericIndex;

        private static NumericUpDown numericMinX;
        private static NumericUpDown numericMinY;
        private static NumericUpDown numericMinZ;

        private static NumericUpDown numericMaxX;
        private static NumericUpDown numericMaxY;
        private static NumericUpDown numericMaxZ;
        private static bool ignoreValueChange = false;

        private static Label labelMinX;
        private static Label labelMinY;
        private static Label labelMinZ;

        private static Label labelMaxX;
        private static Label labelMaxY;
        private static Label labelMaxZ;

        private static Label labelWidth;
        private static Label labelHeight;
        private static Label labelLength;

        private static Button buttonShowCode;

        private static HWShipType.TargetBox selectedTargetBox;

        private static bool editorBoxesInvalid = false;
        private static HWCollisionMesh rootCollisionMesh = null;

        private static Vector3 boundsCenter;

        private static LUACodeWindow codeWindow;

        public static void Init(CheckedListBox list, Button buttonAdd, Button buttonRemove, NumericUpDown numericIndex, 
            NumericUpDown numericMinX, NumericUpDown numericMinY, NumericUpDown numericMinZ, 
            NumericUpDown numericMaxX, NumericUpDown numericMaxY, NumericUpDown numericMaxZ,
            Label labelMinX, Label labelMinY, Label labelMinZ, Label labelMaxX, Label labelMaxY, Label labelMaxZ, Label labelWidth, Label labelHeight, Label labelLength,
            Button buttonShowCode)
        {
            TargetBoxManager.list = list;

            TargetBoxManager.buttonAdd = buttonAdd;
            TargetBoxManager.buttonRemove = buttonRemove;

            TargetBoxManager.numericIndex = numericIndex;

            TargetBoxManager.numericMinX = numericMinX;
            TargetBoxManager.numericMinY = numericMinY;
            TargetBoxManager.numericMinZ = numericMinZ;

            TargetBoxManager.numericMaxX = numericMaxX;
            TargetBoxManager.numericMaxY = numericMaxY;
            TargetBoxManager.numericMaxZ = numericMaxZ;

            TargetBoxManager.labelMinX = labelMinX;
            TargetBoxManager.labelMinY = labelMinY;
            TargetBoxManager.labelMinZ = labelMinZ;

            TargetBoxManager.labelMaxX = labelMaxX;
            TargetBoxManager.labelMaxY = labelMaxY;
            TargetBoxManager.labelMaxZ = labelMaxZ;

            TargetBoxManager.labelWidth = labelWidth;
            TargetBoxManager.labelHeight = labelHeight;
            TargetBoxManager.labelLength = labelLength;

            TargetBoxManager.buttonShowCode = buttonShowCode;

            TargetBoxManager.list.SelectedIndexChanged += List_SelectedIndexChanged;
            TargetBoxManager.list.ItemCheck += List_ItemCheck;
            TargetBoxManager.numericIndex.ValueChanged += NumericIndex_ValueChanged;
            TargetBoxManager.numericMinX.ValueChanged += BoundsChanged;
            TargetBoxManager.numericMinY.ValueChanged += BoundsChanged;
            TargetBoxManager.numericMinZ.ValueChanged += BoundsChanged;
            TargetBoxManager.numericMaxX.ValueChanged += BoundsChanged;
            TargetBoxManager.numericMaxY.ValueChanged += BoundsChanged;
            TargetBoxManager.numericMaxZ.ValueChanged += BoundsChanged;

            TargetBoxManager.buttonAdd.Click += ButtonAdd_Click;
            TargetBoxManager.buttonRemove.Click += ButtonRemove_Click;

            TargetBoxManager.buttonShowCode.Click += ButtonShowCode_Click;
        }

        private static void ButtonShowCode_Click(object sender, EventArgs e)
        {
            List<HWShipType.TargetBox> targetBoxes = new List<HWShipType.TargetBox>();
            foreach (HWShipType.TargetBox targetBox in HWShipType.TargetBox.TargetBoxes)
            {
                if(targetBox.ItemIndex != -1)
                {
                    targetBoxes.Add(targetBox);
                }
            }

            if (targetBoxes.Count == 0)
                return;

            StringBuilder sb = new StringBuilder();
            foreach(HWShipType.TargetBox targetBox in targetBoxes)
            {
                sb.AppendLine("setTargetBox(NewShipType, " + targetBox.Index + ", " + targetBox.Min.X.ToString(CultureInfo.InvariantCulture) + ", " + targetBox.Min.Y.ToString(CultureInfo.InvariantCulture) + ", " + targetBox.Min.Z.ToString(CultureInfo.InvariantCulture) + ", " +
                    targetBox.Max.X.ToString(CultureInfo.InvariantCulture) + ", " + targetBox.Max.Y.ToString(CultureInfo.InvariantCulture) + ", " + targetBox.Max.Z.ToString(CultureInfo.InvariantCulture) + ");");
            }

            if (codeWindow != null)
                codeWindow.Close();

            codeWindow = new LUACodeWindow();
            codeWindow.Open();
            codeWindow.SetText(sb.ToString());
        }

        private static void ButtonAdd_Click(object sender, EventArgs e)
        {
            HWShipType.TargetBox newTargetBox = new HWShipType.TargetBox(ShipTypeMapping.ActiveShipType, list.Items.Count, -Vector3.One, Vector3.One);
            AddTargetBox(newTargetBox);
            newTargetBox.EditorCube.Visible = true;
            list.SetItemChecked(newTargetBox.ItemIndex, true);
            list.SelectedIndex = newTargetBox.ItemIndex;

            UpdateMeasurementLabels();
            if(list.Items.Count > 0)
            {
                buttonShowCode.Enabled = true;
            }
        }

        private static void ButtonRemove_Click(object sender, EventArgs e)
        {
            if (selectedTargetBox == null)
                return;

            int removedIndex = selectedTargetBox.ItemIndex;
            RemoveTargetBox(selectedTargetBox);

            UpdateMeasurementLabels();

            if (list.Items.Count == 0)
            {
                buttonShowCode.Enabled = false;
            }
        }

        private static void NumericIndex_ValueChanged(object sender, EventArgs e)
        {
            if (selectedTargetBox == null || ignoreValueChange)
                return;

            ignoreValueChange = true;
            selectedTargetBox.Index = (int)numericIndex.Value;
            list.Items[selectedTargetBox.ItemIndex] = "targetBox" + selectedTargetBox.Index;
            ignoreValueChange = false;
        }

        private static void List_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            HWShipType.TargetBox checkedTargetBox = null;

            if (list.SelectedIndex >= 0)
                checkedTargetBox = HWShipType.TargetBox.GetByItemIndex(list.SelectedIndex);

            if (checkedTargetBox == null)
                return;

            checkedTargetBox.EditorCube.Visible = e.NewValue == CheckState.Checked;
        }

        private static void BoundsChanged(object sender, EventArgs e)
        {
            if (selectedTargetBox == null || ignoreValueChange)
                return;

            selectedTargetBox.Min = new Vector3((float)numericMinX.Value, (float)numericMinY.Value, (float)numericMinZ.Value);
            selectedTargetBox.Max = new Vector3((float)numericMaxX.Value, (float)numericMaxY.Value, (float)numericMaxZ.Value);
            RefreshTargetBoxCube(selectedTargetBox);
        }

        public static void Update()
        {
            if (!editorBoxesInvalid)
                return;

            rootCollisionMesh = null;
            foreach (HWCollisionMesh mesh in HWCollisionMesh.CollisionMeshes)
            {
                if (mesh.Parent == HWJoint.Root)
                {
                    rootCollisionMesh = mesh;
                    break;
                }
            }

            if (rootCollisionMesh != null)
            {
                boundsCenter = (rootCollisionMesh.BoundsMax + rootCollisionMesh.BoundsMin) / 2;
            }
            else
            {
                boundsCenter = Vector3.Zero;
            }

            foreach (HWShipType.TargetBox targetBox in HWShipType.TargetBox.TargetBoxes)
            {
                RefreshTargetBoxCube(targetBox);
            }

            editorBoxesInvalid = false;
        }

        private static void UpdateMeasurementLabels()
        {
            labelMinX.Text = "0.0";
            labelMinY.Text = "0.0";
            labelMinZ.Text = "0.0";

            labelMaxX.Text = "0.0";
            labelMaxY.Text = "0.0";
            labelMaxZ.Text = "0.0";

            labelWidth.Text = "0.0";
            labelHeight.Text = "0.0";
            labelLength.Text = "0.0";

            if (selectedTargetBox == null)
                return;

            labelMinX.Text = Math.Round(selectedTargetBox.CubeMin.X, 4).ToString(CultureInfo.InvariantCulture);
            labelMinY.Text = Math.Round(selectedTargetBox.CubeMin.Y, 4).ToString(CultureInfo.InvariantCulture);
            labelMinZ.Text = Math.Round(selectedTargetBox.CubeMin.Z, 4).ToString(CultureInfo.InvariantCulture);

            labelMaxX.Text = Math.Round(selectedTargetBox.CubeMax.X, 4).ToString(CultureInfo.InvariantCulture);
            labelMaxY.Text = Math.Round(selectedTargetBox.CubeMax.Y, 4).ToString(CultureInfo.InvariantCulture);
            labelMaxZ.Text = Math.Round(selectedTargetBox.CubeMax.Z, 4).ToString(CultureInfo.InvariantCulture);

            float width = Math.Abs(selectedTargetBox.CubeMax.X - selectedTargetBox.CubeMin.X);
            float height = Math.Abs(selectedTargetBox.CubeMax.Y - selectedTargetBox.CubeMin.Y);
            float length = Math.Abs(selectedTargetBox.CubeMax.Z - selectedTargetBox.CubeMin.Z);

            labelWidth.Text = Math.Round(width, 4).ToString(CultureInfo.InvariantCulture);
            labelHeight.Text = Math.Round(height, 4).ToString(CultureInfo.InvariantCulture);
            labelLength.Text = Math.Round(length, 4).ToString(CultureInfo.InvariantCulture);
        }

        public static void InvalidateTargetBoxes()
        {
            editorBoxesInvalid = true;
        }

        private static void RefreshTargetBoxCube(HWShipType.TargetBox targetBox)
        {
            if(targetBox.EditorCube == null)
                return;

            Vector3 min = -Vector3.One;
            Vector3 max = Vector3.One;
            if (rootCollisionMesh != null)
            {
                min = rootCollisionMesh.BoundsMin;
                max = rootCollisionMesh.BoundsMax;
            }

            Vector3 cubeMin = new Vector3(  boundsCenter.X + (Math.Abs(min.X - boundsCenter.X) * targetBox.Min.X), 
                                            boundsCenter.Y + (Math.Abs(min.Y - boundsCenter.Y) * targetBox.Min.Y),
                                            boundsCenter.Z + (Math.Abs(min.Z - boundsCenter.Z) * targetBox.Min.Z)
                                        );
            Vector3 cubeMax = new Vector3(boundsCenter.X + (Math.Abs(max.X - boundsCenter.X) * targetBox.Max.X),
                                            boundsCenter.Y + (Math.Abs(max.Y - boundsCenter.Y) * targetBox.Max.Y),
                                            boundsCenter.Z + (Math.Abs(max.Z - boundsCenter.Z) * targetBox.Max.Z)
                                        );
           

            MeshData meshData = MeshData.GenerateBoundingCube(cubeMin, cubeMax);
            targetBox.EditorCube.SetData(meshData);

            targetBox.CubeMin = cubeMin;
            targetBox.CubeMax = cubeMax;

            UpdateMeasurementLabels();
        }

        private static void List_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonRemove.Enabled = false;

            numericIndex.Enabled = false;

            numericMinX.Enabled = false;
            numericMinY.Enabled = false;
            numericMinZ.Enabled = false;

            numericMaxX.Enabled = false;
            numericMaxY.Enabled = false;
            numericMaxZ.Enabled = false;

            ignoreValueChange = true;
            numericMinX.Value = 0;
            numericMinY.Value = 0;
            numericMinZ.Value = 0;

            numericMaxX.Value = 0;
            numericMaxY.Value = 0;
            numericMaxZ.Value = 0;
            ignoreValueChange = false;

            selectedTargetBox = null;
            if(list.SelectedIndex >= 0)
            {
                selectedTargetBox = HWShipType.TargetBox.GetByItemIndex(list.SelectedIndex);
            }

            UpdateMeasurementLabels();

            if (selectedTargetBox == null)
                return;

            buttonRemove.Enabled = true;

            numericIndex.Enabled = true;

            numericMinX.Enabled = true;
            numericMinY.Enabled = true;
            numericMinZ.Enabled = true;

            numericMaxX.Enabled = true;
            numericMaxY.Enabled = true;
            numericMaxZ.Enabled = true;

            ignoreValueChange = true;
            numericIndex.Value = (decimal)selectedTargetBox.Index;

            numericMinX.Value = (decimal)selectedTargetBox.Min.X;
            numericMinY.Value = (decimal)selectedTargetBox.Min.Y;
            numericMinZ.Value = (decimal)selectedTargetBox.Min.Z;

            numericMaxX.Value = (decimal)selectedTargetBox.Max.X;
            numericMaxY.Value = (decimal)selectedTargetBox.Max.Y;
            numericMaxZ.Value = (decimal)selectedTargetBox.Max.Z;
            ignoreValueChange = false;
        }

        private static void AddTargetBox(HWShipType.TargetBox targetBox)
        {
            if(targetBox.ItemIndex != -1)
            {
                return;
            }

            EditorCube newCube = new EditorCube(HWJoint.Root, new Vector3(1, 1, 0));
            newCube.Wireframe = true;
            targetBox.EditorCube = newCube;

            RefreshTargetBoxCube(targetBox);

            int newItem = list.Items.Add("targetBox" + targetBox.Index);
            targetBox.ItemIndex = list.Items.Count - 1;
        }

        private static void RemoveTargetBox(HWShipType.TargetBox targetBox)
        {
            if (targetBox.ItemIndex == -1)
            {
                return;
            }

            int index = targetBox.ItemIndex;
            list.Items.RemoveAt(targetBox.ItemIndex);
            targetBox.Destroy();

            foreach(HWShipType.TargetBox box in HWShipType.TargetBox.TargetBoxes)
            {
                if (box.ItemIndex == -1)
                    continue;

                if(box.ItemIndex > index)
                {
                    box.ItemIndex--;
                }
            }
        }

        public static void LoadFromShipType(HWShipType type)
        {
            if (list == null)
                return;

            foreach (HWShipType.TargetBox targetBox in HWShipType.TargetBox.TargetBoxes)
            {
                if(targetBox.EditorCube != null)
                {
                    targetBox.EditorCube.Destroy();
                    targetBox.ItemIndex = -1;
                }
            }

            list.Items.Clear();

            buttonShowCode.Enabled = false;

            if (type == null)
            {
                List_SelectedIndexChanged(null, EventArgs.Empty);
                return;
            }
            
            foreach (HWShipType.TargetBox targetBox in type.TargetBoxes)
            {
                AddTargetBox(targetBox);
            }

            List_SelectedIndexChanged(null, EventArgs.Empty);

            if(type.TargetBoxes.Count > 0)
            {
                buttonShowCode.Enabled = true;
            }
        }
    }
}
