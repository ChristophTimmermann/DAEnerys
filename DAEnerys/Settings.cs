using OpenTK;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DAEnerys
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

            buttonTeamColor.BackColor = SavedTeamColor;
            buttonStripeColor.BackColor = SavedStripeColor;
            teamColorButtonCustom.SetColors(SavedTeamColor, SavedStripeColor);

            buttonEngineColor.BackColor = Color.FromArgb(255, SavedEngineColor.R, SavedEngineColor.G, SavedEngineColor.B); //The button colors look weird when the alpha is set too
            engineColorButtonCustom.SetColor(SavedEngineColor);

            numericFOV.Value = (int)Math.Round(MathHelper.RadiansToDegrees(Program.Camera.FieldOfView));
            numericIconSize.Value = (decimal)HWNavLight.IconSize;

            hideFSAAMessage = true;
            switch (Program.FSAASamples)
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
            checkDisableLighting.Checked = Renderer.DisableLighting;

            listDataPaths.Items.Clear();
            listDataPaths.Items.AddRange(HWData.DataPaths.ToArray());

            foreach(HWBadge badge in HWData.Badges)
            {
                comboBadge.Items.Add(badge.Name);
            }

            if (HWBadge.BadgeNames.Keys.Contains(SavedBadge))
            {
                comboBadge.SelectedItem = SavedBadge;
                comboBadge_SelectedIndexChanged(this, EventArgs.Empty);
            }

            comboBackground.Items.Clear();
            foreach (string bgName in HWData.BackgroundTextures.Keys)
            {
                comboBackground.Items.Add(bgName).ToString();
            }

            if (HWData.BackgroundTextures.Keys.Contains(SavedBackground))
                comboBackground.SelectedItem = SavedBackground;
            else
                comboBackground.SelectedItem = "";

            checkCheckForUpdates.Checked = Updater.CheckForUpdatesOnStart;
        }

        private void numericJointSize_ValueChanged(object sender, EventArgs e)
        {
            EditorJoint.Size = (float)numericJointSize.Value;

            Renderer.InvalidateView();
            Renderer.Invalidate();
        }

        private void numericMarkerSize_ValueChanged(object sender, EventArgs e)
        {
            HWMarker.MarkerSize = (float)numericMarkerSize.Value;

            //Update line vertices
            foreach (HWMarker marker in HWMarker.Markers)
            {
                foreach (EditorLine line in marker.Lines)
                {
                    line.Vertices = line.GetVertices();
                }
            }

            Renderer.InvalidateMeshData();
            Renderer.InvalidateView();
            Renderer.Invalidate();
        }

        private void numericZoomSpeed_ValueChanged(object sender, EventArgs e)
        {
            Program.Camera.ZoomSpeed = (float)numericZoomSpeed.Value;
        }

        private void numericClipDistance_ValueChanged(object sender, EventArgs e)
        {
            Program.Camera.ClipDistance = (float)numericFarClip.Value;
            numericNearClip.Maximum = numericFarClip.Value - (decimal)0.0001;

            Renderer.InvalidateView();
            Renderer.Invalidate();
        }

        private void numericNearClip_ValueChanged(object sender, EventArgs e)
        {
            Program.Camera.NearClipDistance = (float)numericNearClip.Value;
            numericFarClip.Minimum = numericNearClip.Value + (decimal)0.0001;

            Renderer.InvalidateView();
            Renderer.Invalidate();
        }

        private void buttonAmbientColor_Click(object sender, EventArgs e)
        {
            colorDialog.Color = buttonAmbientColor.BackColor;
            DialogResult result = colorDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                Renderer.AmbientLight.Color = new Vector3((float)colorDialog.Color.R / 255, (float)colorDialog.Color.G / 255, (float)colorDialog.Color.B / 255);
                buttonAmbientColor.BackColor = Color.FromArgb((int)Math.Round(Renderer.AmbientLight.Color.X * 255), (int)Math.Round(Renderer.AmbientLight.Color.Y * 255), (int)Math.Round(Renderer.AmbientLight.Color.Z * 255));

                Renderer.Invalidate();
            }
        }

        private void buttonBackgroundColor_Click(object sender, EventArgs e)
        {
            colorDialog.Color = buttonBackgroundColor.BackColor;
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

            Renderer.InvalidateView();
            Renderer.Invalidate();
        }

        private void comboFSAASamples_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboFSAASamples.SelectedIndex)
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

            if (comboFSAASamples.SelectedIndex != oldComboFSAAIndex && !hideFSAAMessage)
                MessageBox.Show("This action will come into effect after the program has been restarted.", "Restart needed", MessageBoxButtons.OK, MessageBoxIcon.Information);

            oldComboFSAAIndex = comboFSAASamples.SelectedIndex;
        }

        private void checkRenderOnTop_CheckedChanged(object sender, EventArgs e)
        {
            Renderer.DrawVisualizationsInFront = checkRenderOnTop.Checked;

            Renderer.Invalidate();
        }

        private void checkVSync_CheckedChanged(object sender, EventArgs e)
        {
            Renderer.EnableVSync = checkVSync.Checked;
        }

        private void numericIconSize_ValueChanged(object sender, EventArgs e)
        {
            HWNavLight.IconSize = (float)numericIconSize.Value;

            Renderer.InvalidateView();
            Renderer.Invalidate();
        }

        private void checkDisableLighting_CheckedChanged(object sender, EventArgs e)
        {
            Renderer.DisableLighting = checkDisableLighting.Checked;

            Renderer.Invalidate();
        }

        //------------------------------------------ SETTINGS SAVING ----------------------------------------//
        public static void SaveSettings()
        {
            Color ambientColor = Color.FromArgb(255, (int)Math.Round(Renderer.AmbientLight.Color.X * 255), (int)Math.Round(Renderer.AmbientLight.Color.Y * 255), (int)Math.Round(Renderer.AmbientLight.Color.Z * 255));

            XElement settings =
                new XElement("settings",
                new XElement("backgroundColor", Renderer.BackgroundColor.ToArgb()),
                new XElement("ambientColor", ambientColor.ToArgb()),
                new XElement("teamColor", SavedTeamColor.ToArgb()),
                new XElement("stripeColor", SavedStripeColor.ToArgb()),
                new XElement("badge", SavedBadge),
                new XElement("engineColor", SavedEngineColor.ToArgb()),
                new XElement("background", SavedBackground),
                new XElement("fieldOfView", MathHelper.RadiansToDegrees(Program.Camera.FieldOfView)),
                new XElement("fsaaSamples", Program.FSAASamples),
                new XElement("drawVisualizationsInFront", Renderer.DrawVisualizationsInFront),
                new XElement("enableVSync", Renderer.EnableVSync),
                new XElement("disableLighting", Renderer.DisableLighting),
                new XElement("checkForUpdatesOnStart", Updater.CheckForUpdatesOnStart));

            foreach (string dataPath in HWData.DataPaths)
            {
                settings.Add(new XElement("dataPath", dataPath));
            }

            File.WriteAllText(Path.Combine(Program.EXECUTABLE_PATH, "settings.xml"), settings.ToString());
        }

        public static Color SavedTeamColor = Color.FromArgb(0, 127, 255);
        public static Color SavedStripeColor = Color.SpringGreen;
        public static string SavedBadge = "daenerys";
        public static Color SavedEngineColor = Color.FromArgb(64, 69, 120, 176);
        private static string savedBackground = "";
        public static string SavedBackground
        {
            get { return savedBackground; }
            set
            {
                savedBackground = value;
                if (Renderer.BackgroundTexture != null)
                    Renderer.BackgroundTexture.Unload();
                if (value != "<nothing>" && HWData.BackgroundTextures.Keys.Contains(value))
                {
                    Renderer.BackgroundTexture = HWData.BackgroundTextures[value];
                    Renderer.BackgroundTexture.Load();
                }
                else
                {
                    if (HWData.BackgroundTextures.Keys.Count > 0)
                    {
                        Renderer.BackgroundTexture = HWData.BackgroundTextures.ElementAt(0).Value;
                        Renderer.BackgroundTexture.Load();
                    }
                    else
                        Renderer.BackgroundTexture = null;

                }
                Renderer.Invalidate();
            }
        }

        public static void LoadSettings()
        {
            if (!File.Exists(Path.Combine(Program.EXECUTABLE_PATH, "settings.xml")))
            {
                Log.WriteLine("No settings.xml found, using default values.");
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
                        case "teamColor":
                            aRGB = 0;
                            int.TryParse(element.Value, out aRGB);
                            SavedTeamColor = Color.FromArgb(aRGB);
                            Renderer.TeamColor = Color.FromArgb(aRGB);
                            break;
                        case "stripeColor":
                            aRGB = 0;
                            int.TryParse(element.Value, out aRGB);
                            SavedStripeColor = Color.FromArgb(aRGB);
                            Renderer.StripeColor = Color.FromArgb(aRGB);
                            break;
                        case "badge":
                            string badge = element.Value;
                            SavedBadge = badge;
                            break;
                        case "engineColor":
                            aRGB = 0;
                            int.TryParse(element.Value, out aRGB);
                            SavedEngineColor = Color.FromArgb(aRGB);
                            Renderer.EngineGlowColor = Color.FromArgb(aRGB);
                            break;
                        case "background":
                            string background = element.Value;
                            savedBackground = background;
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
                        case "disableLighting":
                            bool disableLighting = false;
                            bool.TryParse(element.Value, out disableLighting);
                            Renderer.DisableLighting = disableLighting;
                            break;
                        case "checkForUpdatesOnStart":
                            bool checkForUpdatesOnStart = false;
                            bool.TryParse(element.Value, out checkForUpdatesOnStart);
                            Updater.CheckForUpdatesOnStart = checkForUpdatesOnStart;
                            break;
                        case "dataPath":
                            HWData.DataPaths.Add(element.Value);
                            break;
                    }
                }
            }
            catch
            {
                Log.WriteLine("Failed to load \"" + Path.Combine(Program.EXECUTABLE_PATH, "settings.xml") + "\".");
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
            if (listDataPaths.SelectedItem != null)
            {
                string path = (string)listDataPaths.SelectedItem;
                HWData.DataPaths.Remove(path);
                listDataPaths.Items.Remove(path);
            }
        }

        private void buttonTeamColor_Click(object sender, EventArgs e)
        {
            colorDialog.Color = buttonTeamColor.BackColor;
            DialogResult result = colorDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                Renderer.TeamColor = colorDialog.Color;
                buttonTeamColor.BackColor = colorDialog.Color;
                SavedTeamColor = colorDialog.Color;
                teamColorButtonCustom.SetColors(Renderer.TeamColor, Renderer.StripeColor);

                Renderer.Invalidate();
            }
        }

        private void buttonStripeColor_Click(object sender, EventArgs e)
        {
            colorDialog.Color = buttonStripeColor.BackColor;
            DialogResult result = colorDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                Renderer.StripeColor = colorDialog.Color;
                buttonStripeColor.BackColor = colorDialog.Color;
                SavedStripeColor = colorDialog.Color;
                teamColorButtonCustom.SetColors(Renderer.TeamColor, Renderer.StripeColor);

                Renderer.Invalidate();
            }
        }

        private void buttonTeamColorPreset_Click(object sender, EventArgs e)
        {
            buttonTeamColor.BackColor = Renderer.TeamColor = ((TeamColorButton)sender).TeamColor;
            buttonStripeColor.BackColor = Renderer.StripeColor = ((TeamColorButton)sender).StripeColor;
            Renderer.Invalidate();
        }

        private void buttonTeamColorSwap_Click(object sender, EventArgs e)
        {
            Color save = Renderer.TeamColor;
            buttonTeamColor.BackColor = Renderer.TeamColor = Renderer.StripeColor;
            buttonStripeColor.BackColor = Renderer.StripeColor = save;
            Renderer.Invalidate();
        }

        private void comboBadge_SelectedIndexChanged(object sender, EventArgs e)
        {
            SavedBadge = (string)comboBadge.SelectedItem;
            Renderer.BadgeTexture = HWBadge.BadgeNames[(string)comboBadge.SelectedItem].Texture;
            Renderer.Invalidate();
        }

        private void buttonEngineColor_Click(object sender, EventArgs e)
        {
            colorDialogAlpha.Color = Renderer.EngineGlowColor;
            DialogResult result = colorDialogAlpha.ShowDialog();
            if (result == DialogResult.OK)
            {
                Renderer.EngineGlowColor = colorDialogAlpha.Color;
                buttonEngineColor.BackColor = Color.FromArgb(255, colorDialogAlpha.Color.R, colorDialogAlpha.Color.G, colorDialogAlpha.Color.B); //The button colors look weird when the alpha is set too
                SavedEngineColor = colorDialogAlpha.Color;
                engineColorButtonCustom.SetColor(Renderer.EngineGlowColor);

                Renderer.Invalidate();
            }
        }

        private void buttonEngineColorPreset_Click(object sender, EventArgs e)
        {
            Color presetColor = ((EngineColorButton)sender).EngineColor;

            buttonEngineColor.BackColor = Color.FromArgb(255, presetColor.R, presetColor.G, presetColor.B); //The button colors look weird when the alpha is set too
            Renderer.EngineGlowColor = presetColor;
            Renderer.Invalidate();
        }

        private void checkCheckForUpdates_CheckedChanged(object sender, EventArgs e)
        {
            Updater.CheckForUpdatesOnStart = checkCheckForUpdates.Checked;
        }

        public void comboBackground_SelectedIndexChanged(object sender, EventArgs e)
        {
            SavedBackground = (string)comboBackground.SelectedItem;
        }
    }
}
