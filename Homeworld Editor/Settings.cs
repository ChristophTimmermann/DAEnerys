using OpenTK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace HomeworldDAEEditor
{
    public partial class Settings : Form
    {
        private int oldComboFSAAIndex;
        private bool hideFSAAMessage;

        public Settings()
        {
            InitializeComponent();
        }

        public void Init()
        {
            numericJointSize.Value = (decimal)EditorJoint.Size;
            numericMarkerSize.Value = (decimal)HWMarker.MarkerSize;
            numericZoomSpeed.Value = (decimal)Program.Camera.ZoomSpeed;
            numericFarClip.Value = (decimal)Program.Camera.ClipDistance;
            numericNearClip.Value = (decimal)Program.Camera.NearClipDistance;

            buttonAmbientColor.BackColor = Color.FromArgb((int)Math.Round(Renderer.AmbientLight.Color.X * 255), (int)Math.Round(Renderer.AmbientLight.Color.Y * 255), (int)Math.Round(Renderer.AmbientLight.Color.Z * 255));
            buttonBackgroundColor.BackColor = Renderer.BackgroundColor;

            numericFOV.Value = (int)Math.Round(MathHelper.RadiansToDegrees(Program.Camera.FieldOfView));
            numericIconSize.Value = (decimal)HWNavLight.IconSize;

            hideFSAAMessage = true;
            switch(Program.FSAASamples)
            {
                case 0:
                    comboFSAASamples.SelectedIndex = 0;
                    break;
                case 2:
                    comboFSAASamples.SelectedIndex = 1;
                    break;
                case 4:
                    comboFSAASamples.SelectedIndex = 2;
                    break;
            }
            hideFSAAMessage = false;

            checkRenderOnTop.Checked = Renderer.DrawVisualizationsInFront;
            checkVSync.Checked = Renderer.EnableVSync;

            listDataPaths.Items.Clear();
            listDataPaths.Items.AddRange(HWData.DataPaths.ToArray());
        }

        private void numericJointSize_ValueChanged(object sender, EventArgs e)
        {
            EditorJoint.Size = (float)numericJointSize.Value;

            Renderer.UpdateMeshData();
            Renderer.UpdateView();
            Program.GLControl.Invalidate();
        }

        private void numericMarkerSize_ValueChanged(object sender, EventArgs e)
        {
            HWMarker.MarkerSize = (float)numericMarkerSize.Value;

            //Update line vertices
            foreach(HWMarker marker in HWScene.Markers)
            {
                foreach(EditorLine line in marker.Lines)
                {
                    line.Vertices = line.GetVertices();
                }
            }

            Renderer.UpdateMeshData();
            Renderer.UpdateView();
            Program.GLControl.Invalidate();
        }

        private void numericZoomSpeed_ValueChanged(object sender, EventArgs e)
        {
            Program.Camera.ZoomSpeed = (float)numericZoomSpeed.Value;
        }

        private void numericClipDistance_ValueChanged(object sender, EventArgs e)
        {
            Program.Camera.ClipDistance = (float)numericFarClip.Value;

            Renderer.UpdateMeshData();
            Renderer.UpdateView();
            Program.GLControl.Invalidate();
        }

        private void numericNearClip_ValueChanged(object sender, EventArgs e)
        {
            Program.Camera.NearClipDistance = (float)numericNearClip.Value;

            Renderer.UpdateMeshData();
            Renderer.UpdateView();
            Program.GLControl.Invalidate();
        }

        private void buttonAmbientColor_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                Renderer.AmbientLight.Color = new Vector3((float)colorDialog.Color.R / 255, (float)colorDialog.Color.G / 255, (float)colorDialog.Color.B / 255);
                buttonAmbientColor.BackColor = Color.FromArgb((int)Math.Round(Renderer.AmbientLight.Color.X * 255), (int)Math.Round(Renderer.AmbientLight.Color.Y * 255), (int)Math.Round(Renderer.AmbientLight.Color.Z * 255));

                Program.GLControl.Invalidate();
            }
        }

        private void buttonBackgroundColor_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                Renderer.BackgroundColor = colorDialog.Color;
                buttonBackgroundColor.BackColor = colorDialog.Color;
            }
        }

        private void numericFOV_ValueChanged(object sender, EventArgs e)
        {
            Program.Camera.FieldOfView = MathHelper.DegreesToRadians((float)numericFOV.Value);

            Renderer.UpdateView();
            Program.GLControl.Invalidate();
        }

        private void comboFSAASamples_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(comboFSAASamples.SelectedIndex)
            {
                case 0:
                    Program.FSAASamples = 0;
                    break;
                case 1:
                    Program.FSAASamples = 2;
                    break;
                case 2:
                    Program.FSAASamples = 4;
                    break;
            }

            if(comboFSAASamples.SelectedIndex != oldComboFSAAIndex && !hideFSAAMessage)
                MessageBox.Show("This action will come into effect after the program has been restarted.", "Restart needed", MessageBoxButtons.OK, MessageBoxIcon.Information);

            oldComboFSAAIndex = comboFSAASamples.SelectedIndex;
        }

        private void checkRenderOnTop_CheckedChanged(object sender, EventArgs e)
        {
            Renderer.DrawVisualizationsInFront = checkRenderOnTop.Checked;

            Program.GLControl.Invalidate();
        }

        private void checkVSync_CheckedChanged(object sender, EventArgs e)
        {
            Renderer.EnableVSync = checkVSync.Checked;
        }

        private void numericIconSize_ValueChanged(object sender, EventArgs e)
        {
            HWNavLight.IconSize = (float)numericIconSize.Value;

            Renderer.UpdateView();
            Program.GLControl.Invalidate();
        }

        //------------------------------------------ SETTINGS SAVING ----------------------------------------//
        public static void SaveSettings()
        {
            Color ambientColor = Color.FromArgb(255, (int)Math.Round(Renderer.AmbientLight.Color.X * 255), (int)Math.Round(Renderer.AmbientLight.Color.Y * 255), (int)Math.Round(Renderer.AmbientLight.Color.Z * 255));

            XElement settings =
                new XElement("settings",
                new XElement("backgroundColor", Renderer.BackgroundColor.ToArgb()),
                new XElement("ambientColor", ambientColor.ToArgb()),
                new XElement("fieldOfView", MathHelper.RadiansToDegrees(Program.Camera.FieldOfView)),
                new XElement("fsaaSamples", Program.FSAASamples),
                new XElement("drawVisualizationsInFront", Renderer.DrawVisualizationsInFront),
                new XElement("enableVSync", Renderer.EnableVSync));

            foreach (string dataPath in HWData.DataPaths)
            {
                settings.Add(new XElement("dataPath", dataPath));
            }

            File.WriteAllText(Path.Combine(Program.EXECUTABLE_PATH, "settings.xml"), settings.ToString());
        }

        public static void LoadSettings()
        {
            if (!File.Exists(Path.Combine(Program.EXECUTABLE_PATH, "settings.xml")))
            {
                Console.WriteLine("No settings.xml found, using default values.");
                return;
            }

            try
            {
                string file = File.ReadAllText(Path.Combine(Program.EXECUTABLE_PATH, "settings.xml"));
                XElement settings = XElement.Parse(file);

                foreach (XElement element in settings.Elements())
                {
                    switch (element.Name.LocalName)
                    {
                        case "backgroundColor":
                            int aRGB;
                            int.TryParse(element.Value, out aRGB);
                            Renderer.BackgroundColor = Color.FromArgb(aRGB);
                            break;
                        case "ambientColor":
                            aRGB = 0;
                            int.TryParse(element.Value, out aRGB);
                            Color ambientColor = Color.FromArgb(aRGB);
                            Renderer.AmbientLight.Color = new Vector3((float)ambientColor.R / 255, (float)ambientColor.G / 255, (float)ambientColor.B / 255);
                            break;
                        case "fieldOfView":
                            double fov = 1.22f;
                            double.TryParse(element.Value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out fov);
                            Program.Camera.FieldOfView = (float)MathHelper.DegreesToRadians(fov);
                            break;
                        case "fsaaSamples":
                            int fsaaSamples = 4;
                            int.TryParse(element.Value, out fsaaSamples);
                            Program.FSAASamples = fsaaSamples;
                            break;
                        case "drawVisualizationsInFront":
                            bool drawInFront = true;
                            bool.TryParse(element.Value, out drawInFront);
                            Renderer.DrawVisualizationsInFront = drawInFront;
                            break;
                        case "enableVSync":
                            bool enableVSync = true;
                            bool.TryParse(element.Value, out enableVSync);
                            Renderer.EnableVSync = enableVSync;
                            break;
                        case "dataPath":
                            HWData.DataPaths.Add(element.Value);
                            break;
                    }
                }
            }
            catch
            {
                Console.WriteLine("Failed to load \"" + Path.Combine(Program.EXECUTABLE_PATH, "settings.xml") + "\".");
            }
        }

        //------------------------------------------ DATA PATHS ----------------------------------------//
        private void buttonAddDataPath_Click(object sender, EventArgs e)
        {
            DialogResult result = addDataPathDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string path = Path.GetDirectoryName(addDataPathDialog.FileName);

                if (HWData.DataPaths.Contains(path))
                {
                    MessageBox.Show("This data path has already been added to the list.", "Data path already added", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                HWData.DataPaths.Add(path);
                listDataPaths.Items.Add(path);
                MessageBox.Show("This action will come into effect after the program has been restarted.", "Restart needed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonRemoveDataPath_Click(object sender, EventArgs e)
        {
            if(listDataPaths.SelectedItem != null)
            {
                string path = (string)listDataPaths.SelectedItem;
                HWData.DataPaths.Remove(path);
                listDataPaths.Items.Remove(path);
            }
        }
    }
}
