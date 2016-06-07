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
                new XElement("fsaaSamples", Program.FSAASamples));
            File.WriteAllText("settings.xml", settings.ToString());
        }

        public static void LoadSettings()
        {
            if (!File.Exists("settings.xml"))
            {
                Console.WriteLine("No settings.xml found, using default values.");
                return;
            }

            try
            {
                string file = File.ReadAllText("settings.xml");
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
                            int fsaaSamples;
                            int.TryParse(element.Value, out fsaaSamples);
                            Program.FSAASamples = fsaaSamples;
                            break;
                    }
                }
            }
            catch
            {
                Console.WriteLine("Failed to load \"settings.xml\".");
            }
        }
    }
}
