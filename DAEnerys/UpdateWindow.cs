using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAEnerys
{
    public partial class UpdateWindow : Form
    {
        public UpdateWindow()
        {
            InitializeComponent();
        }

        private void UpdateWindow_Load(object sender, EventArgs e)
        {
            pictureInfo.Image = SystemIcons.Information.ToBitmap();
        }

        private void checkNeverAskAgain_CheckedChanged(object sender, EventArgs e)
        {
            Updater.CheckForUpdatesOnStart = !checkNeverAskAgain.Checked;
        }

        private void buttonYes_Click(object sender, EventArgs e)
        {
            this.Close();
            Updater.DownloadLatestBuild();
            Updater.Checking = false;
        }

        private void buttonNo_Click(object sender, EventArgs e)
        {
            this.Close();
            Updater.Checking = false;
        }
    }
}
