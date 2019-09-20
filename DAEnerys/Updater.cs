using System;
using System.Net;
using System.Windows.Forms;

namespace DAEnerys
{
    public static class Updater
    {
        const string downloadURL = @"https://bitbucket.org/PayDay_/daenerys/downloads/";
        const string latestBuildFile = "latestBuild.txt";
        const string zipFile = "DAEnerys_b";

        public static bool CheckForUpdatesOnStart = true;
        public static bool Checking = false;

        private static bool ShowMessageBoxOnLatestVersion = false;

        private static int latestBuild = 0;

        public static void CheckForUpdatesManually()
        {
            if (Checking)
                return;

            ShowMessageBoxOnLatestVersion = true;
            CheckForUpdates();
        }

        public static void CheckForUpdates()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            if (Checking)
                return;

            Checking = true;

            using (WebClient client = new WebClient())
            { 
                try
                {
                    client.DownloadStringCompleted += BuildFetchCompleted;
                    client.DownloadStringAsync(new Uri(downloadURL + latestBuildFile));
                }
                catch(WebException)
                {
                    if (ShowMessageBoxOnLatestVersion)
                    {
                        MessageBox.Show("Failed to connect to the update server (bitbucket.org).", "Update check failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ShowMessageBoxOnLatestVersion = false;
                    }

                    Checking = false;
                }
            }
        }

        static void BuildFetchCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                if (ShowMessageBoxOnLatestVersion)
                {
                    MessageBox.Show("Failed to connect to the update server (bitbucket.org).", "Update check failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ShowMessageBoxOnLatestVersion = false;
                }

                Checking = false;
                return;
            }

            int currentBuild = Program.main.BUILD;
            bool success = int.TryParse(e.Result, out latestBuild);
            if (!success)
                return;

            if (latestBuild > currentBuild)
            {
                System.Media.SystemSounds.Beep.Play();
                UpdateWindow updateWindow = new UpdateWindow();
                updateWindow.labelCurrentBuild.Text = currentBuild.ToString();
                updateWindow.labelLatestBuild.Text = latestBuild.ToString();
                updateWindow.Show();
            }
            else
            {
                if (ShowMessageBoxOnLatestVersion)
                    MessageBox.Show("DAEnerys is the latest version (Build " + currentBuild + ").", "No new version available", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Checking = false;
            }

            ShowMessageBoxOnLatestVersion = false;
        }

        public static void DownloadLatestBuild()
        {
            System.Diagnostics.Process.Start(downloadURL + zipFile + latestBuild + ".zip");
        }
    }
}
