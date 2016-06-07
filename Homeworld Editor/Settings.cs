using OpenTK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            numericFOV.Value = (int)Math.Round(MathHelper.RadiansToDegrees(Program.Camera.FieldOfView));
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

        private void numericFOV_ValueChanged(object sender, EventArgs e)
        {
            Program.Camera.FieldOfView = MathHelper.DegreesToRadians((float)numericFOV.Value);

            Renderer.UpdateView();
            Program.GLControl.Invalidate();
        }
    }
}
