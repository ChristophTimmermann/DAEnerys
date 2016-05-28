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
            numericFarClip.Value = (decimal)Renderer.ClipDistance;
            numericNearClip.Value = (decimal)Renderer.NearClipDistance;
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
            Renderer.ClipDistance = (float)numericFarClip.Value;

            Renderer.UpdateMeshData();
            Renderer.UpdateView();
            Program.GLControl.Invalidate();
        }

        private void numericNearClip_ValueChanged(object sender, EventArgs e)
        {
            Renderer.NearClipDistance = (float)numericNearClip.Value;

            Renderer.UpdateMeshData();
            Renderer.UpdateView();
            Program.GLControl.Invalidate();
        }
    }
}
