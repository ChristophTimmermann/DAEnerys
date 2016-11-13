using System;
using System.Collections.Generic;
using System.Windows.Forms;
using NewShaderManifest;

namespace DAEnerys
{
    public partial class ShaderSettings : Form
    {
        public ShaderSettings()
        {
            InitializeComponent();
        }

        public void Init()
        {
            numExecTime.Value = (decimal)Renderer.Exec;
            numExecDelta.Value = (decimal)Renderer.ExecDelta;
            numSimTime.Value = (decimal)Renderer.Sim;
            numSimDelta.Value = (decimal)Renderer.SimDelta;

            numSOBAlpha.Value = (decimal)Renderer.SOBAlpha;
            numSOBCloak.Value = (decimal)Renderer.SOBCloak;
            numSOBClip.Value = (decimal)Renderer.SOBClip;

            numPaintCurve.Value = (decimal)Renderer.PaintCurve;
            numPaintScale.Value = (decimal)Renderer.PaintScale;
            numPaintOffset.Value = (decimal)Renderer.PaintOffset;

            numClipDist.Minimum = (decimal)Renderer.MinClipDistance;
            numClipDist.Maximum = (decimal)Renderer.MaxClipDistance;
            numClipDist.Value = (decimal)Renderer.ClipDistance;
            numClipDist.Increment = (numClipDist.Maximum - numClipDist.Minimum) / 100;

            cbxConfigOptions.Items.Clear();
            foreach (string opt in ManifestConfig.Options.Keys)
                cbxConfigOptions.Items.Add(opt);
        }

        //------------------------------------------ SETTINGS SAVING ----------------------------------------//
        public static void SaveSettings()
        {
            //Color ambientColor = Color.FromArgb(255, (int)Math.Round(Renderer.AmbientLight.Color.X * 255), (int)Math.Round(Renderer.AmbientLight.Color.Y * 255), (int)Math.Round(Renderer.AmbientLight.Color.Z * 255));

            //XElement settings =
            //    new XElement("settings",
            //    new XElement("backgroundColor", Renderer.BackgroundColor.ToArgb()),
            //    new XElement("ambientColor", ambientColor.ToArgb()),
            //    new XElement("teamColor", Renderer.TeamColor.ToArgb()),
            //    new XElement("stripeColor", Renderer.StripeColor.ToArgb()),
            //    new XElement("fieldOfView", MathHelper.RadiansToDegrees(Program.Camera.FieldOfView)),
            //    new XElement("fsaaSamples", Program.FSAASamples),
            //    new XElement("drawVisualizationsInFront", Renderer.DrawVisualizationsInFront),
            //    new XElement("enableVSync", Renderer.EnableVSync),
            //    new XElement("disableLighting", Renderer.DisableLighting));

            //foreach (string dataPath in HWData.DataPaths)
            //{
            //    settings.Add(new XElement("dataPath", dataPath));
            //}

            //File.WriteAllText(Path.Combine(Program.EXECUTABLE_PATH, "settings.xml"), settings.ToString());
        }

        public static void LoadSettings()
        {
            //if (!File.Exists(Path.Combine(Program.EXECUTABLE_PATH, "settings.xml")))
            //{
            //    Log.WriteLine("No settings.xml found, using default values.");
            //    return;
            //}

            //try
            //{
            //    string file = File.ReadAllText(Path.Combine(Program.EXECUTABLE_PATH, "settings.xml"));
            //    XElement settings = XElement.Parse(file);

            //    foreach (XElement element in settings.Elements())
            //    {
            //        switch (element.Name.LocalName)
            //        {
            //            case "backgroundColor":
            //                int aRGB;
            //                int.TryParse(element.Value, out aRGB);
            //                Renderer.BackgroundColor = Color.FromArgb(aRGB);
            //                break;
            //            case "ambientColor":
            //                aRGB = 0;
            //                int.TryParse(element.Value, out aRGB);
            //                Color ambientColor = Color.FromArgb(aRGB);
            //                Renderer.AmbientLight.Color = new Vector3((float)ambientColor.R / 255, (float)ambientColor.G / 255, (float)ambientColor.B / 255);
            //                break;
            //            case "teamColor":
            //                aRGB = 0;
            //                int.TryParse(element.Value, out aRGB);
            //                Renderer.TeamColor = Color.FromArgb(aRGB);
            //                break;
            //            case "stripeColor":
            //                aRGB = 0;
            //                int.TryParse(element.Value, out aRGB);
            //                Renderer.StripeColor = Color.FromArgb(aRGB);
            //                break;
            //            case "fieldOfView":
            //                double fov = 1.22f;
            //                double.TryParse(element.Value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out fov);
            //                Program.Camera.FieldOfView = (float)MathHelper.DegreesToRadians(fov);
            //                break;
            //            case "fsaaSamples":
            //                int fsaaSamples = 4;
            //                int.TryParse(element.Value, out fsaaSamples);
            //                Program.FSAASamples = fsaaSamples;
            //                break;
            //            case "drawVisualizationsInFront":
            //                bool drawInFront = true;
            //                bool.TryParse(element.Value, out drawInFront);
            //                Renderer.DrawVisualizationsInFront = drawInFront;
            //                break;
            //            case "enableVSync":
            //                bool enableVSync = true;
            //                bool.TryParse(element.Value, out enableVSync);
            //                Renderer.EnableVSync = enableVSync;
            //                break;
            //            case "disableLighting":
            //                bool disableLighting = false;
            //                bool.TryParse(element.Value, out disableLighting);
            //                Renderer.DisableLighting = disableLighting;
            //                break;
            //            case "dataPath":
            //                HWData.DataPaths.Add(element.Value);
            //                break;
            //        }
            //    }
            //}
            //catch
            //{
            //    Log.WriteLine("Failed to load \"" + Path.Combine(Program.EXECUTABLE_PATH, "settings.xml") + "\".");
            //}
        }

        private void numPaintCurve_ValueChanged(object sender, EventArgs e)
        {
            Renderer.PaintCurve = (float)numPaintCurve.Value;

            Renderer.InvalidateView();
            Renderer.Invalidate();
        }

        private void numPaintScale_ValueChanged(object sender, EventArgs e)
        {
            Renderer.PaintScale = (float)numPaintScale.Value;

            Renderer.InvalidateView();
            Renderer.Invalidate();
        }

        private void numPaintOffset_ValueChanged(object sender, EventArgs e)
        {
            Renderer.PaintOffset = (float)numPaintOffset.Value;

            Renderer.InvalidateView();
            Renderer.Invalidate();
        }

        private bool ignore = false;

        private void cbxConfigOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            ignore = true;
            if ((string)cbxConfigOptions.SelectedItem == "CFG_Patch_AltHyper")
                numConfigOption.Maximum = 1;
            else
                numConfigOption.Maximum = ManifestConfig.Options[(string)cbxConfigOptions.SelectedItem].MaxValue;
            numConfigOption.Value = ManifestConfig.Options[(string)cbxConfigOptions.SelectedItem].Value;
            ignore = false;
        }

        private void numConfigOption_ValueChanged(object sender, EventArgs e)
        {
            if (!ignore)
                ManifestConfig.Options[(string)cbxConfigOptions.SelectedItem].Value = (int)numConfigOption.Value;
        }

        private void btnReloadShaders_Click(object sender, EventArgs e)
        {
            Manifest.ReloadManifest();
            Renderer.InvalidateView();
            Renderer.Invalidate();
        }

        private void numExecTime_ValueChanged(object sender, EventArgs e)
        {
            Renderer.Exec = (float)numExecTime.Value;

            Renderer.InvalidateView();
            Renderer.Invalidate();
        }

        private void numExecDelta_ValueChanged(object sender, EventArgs e)
        {
            Renderer.ExecDelta = (float)numExecDelta.Value;

            Renderer.InvalidateView();
            Renderer.Invalidate();
        }

        private void numSimTime_ValueChanged(object sender, EventArgs e)
        {
            Renderer.Sim = (float)numSimTime.Value;

            Renderer.InvalidateView();
            Renderer.Invalidate();
        }

        private void numSimDelta_ValueChanged(object sender, EventArgs e)
        {
            Renderer.SimDelta = (float)numSimDelta.Value;

            Renderer.InvalidateView();
            Renderer.Invalidate();
        }

        private void numSOBAlpha_ValueChanged(object sender, EventArgs e)
        {
            Renderer.SOBAlpha = (float)numSOBAlpha.Value;

            Renderer.InvalidateView();
            Renderer.Invalidate();
        }

        private void numSOBCloak_ValueChanged(object sender, EventArgs e)
        {
            Renderer.SOBCloak = (float)numSOBCloak.Value;

            Renderer.InvalidateView();
            Renderer.Invalidate();
        }

        private void numSOBClip_ValueChanged(object sender, EventArgs e)
        {
            Renderer.SOBClip = (float)numSOBClip.Value;

            Renderer.InvalidateView();
            Renderer.Invalidate();
        }

        private void numClipDist_ValueChanged(object sender, EventArgs e)
        {
            Renderer.ClipDistance = (float)numClipDist.Value;

            Renderer.InvalidateView();
            Renderer.Invalidate();
        }

        private void chkHACKPain_CheckedChanged(object sender, EventArgs e)
        {
            Renderer.HACK_AllIFeelIsPain = chkHACKPain.Checked;

            Renderer.InvalidateView();
            Renderer.Invalidate();
        }

        private void btnEnterHyperspace_Click(object sender, EventArgs e)
        {
            //Manifest.Globals.Set("clipPlane", new float[] { 0, 0, -1, HWScene.CollisionMeshes[0].Mesh.Max.Z });
            //HyperspaceEffect.Effect.MinBounds = HWScene.Min;
            //HyperspaceEffect.Effect.MaxBounds = HWScene.Max;
            //HyperspaceEffect.Effect.Restart();
        }

        private void btnExitHyperspace_Click(object sender, EventArgs e)
        {
            //Manifest.Globals.Set("clipPlane", new float[] { 0, 0, 1, HWScene.CollisionMeshes[0].Mesh.Max.Z });
            //HyperspaceEffect.Effect.MinBounds = -HWScene.Min;
            //HyperspaceEffect.Effect.MaxBounds = -HWScene.Max;
            //HyperspaceEffect.Effect.Restart();
        }


        private void numLifeAlpha_ValueChanged(object sender, EventArgs e)
        {
            Renderer.LifeAlpha = (float)numLifeAlpha.Value;

            Renderer.InvalidateView();
            Renderer.Invalidate();
        }

        private void numDeathRatio_ValueChanged(object sender, EventArgs e)
        {
            Renderer.DeathRatio = (float)numDeathRatio.Value;

            Renderer.InvalidateView();
            Renderer.Invalidate();
        }
    }
}
