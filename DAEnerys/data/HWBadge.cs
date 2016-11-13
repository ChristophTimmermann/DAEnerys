using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DAEnerys
{
    public class HWBadge
    {
        public static Dictionary<string, HWBadge> BadgeNames = new Dictionary<string, HWBadge>();

        public static HWBadge DefaultBadge;

        public static void LoadSavedBadge()
        {
            if (Settings.SavedBadge.Length == 0)
            {
                if (BadgeNames.Keys.Count > 0)
                {
                    Settings.SavedBadge = HWData.Badges[0].Name;
                    Renderer.BadgeTexture = HWData.Badges[0].Texture;
                    Renderer.Invalidate();
                }

                return;
            }

            if (BadgeNames.Keys.Contains(Settings.SavedBadge))
                Renderer.BadgeTexture = BadgeNames[Settings.SavedBadge].Texture;
            else
            {
                MessageBox.Show("Could not find badge \"" + Settings.SavedBadge + "\".", "Failed to find badge", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                if (BadgeNames.Keys.Count > 0)
                {
                    Settings.SavedBadge = HWData.Badges[0].Name;
                    Renderer.BadgeTexture = HWData.Badges[0].Texture;
                    Renderer.Invalidate();
                }
            }
        }

        public string Name;
        public string Path;
        public HWTexture Texture;
        public Bitmap Bitmap;

        public HWBadge(string name, string path)
        {
            Name = name;
            Path = path;

            Texture = new HWTexture(path, true);
            Bitmap = HWTexture.LoadToBitmap(path);

            BadgeNames.Add(name, this);
            HWData.Badges.Add(this);
        }
    }
}
