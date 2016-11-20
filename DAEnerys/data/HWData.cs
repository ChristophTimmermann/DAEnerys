using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DAEnerys
{
    public static class HWData
    {
        public static List<string> DataPaths = new List<string>();

        public static List<HWNavLightStyle> NavLightStyles = new List<HWNavLightStyle>();
        public static List<HWBadge> Badges = new List<HWBadge>();
        public static Dictionary<string, HWTextureCube> BackgroundTextures = new Dictionary<string, HWTextureCube>();

        public static HWTexture NavLightSprite;

        public static void ParseDataPaths()
        {
            //Check if there are any data paths
            if (DataPaths.Count <= 0)
            {
                MessageBox.Show("You did not specify any data paths yet!\nThis is needed for parsing of navlight styles etc.\nDefine them in the settings window.", "No data paths specified", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Check if the data paths are still valid
            List<string> pathsToRemove = new List<string>();
            foreach (string dataPath in DataPaths)
            {
                if (!File.Exists(Path.Combine(dataPath, "keeper.txt")))
                {
                    MessageBox.Show("Could not find keeper.txt in \"" + dataPath + "\".\nRemoving data path from list...", "Data path invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    pathsToRemove.Add(dataPath);
                }
            }

            //Remove invalid data paths
            foreach (string pathToRemove in pathsToRemove)
            {
                DataPaths.Remove(pathToRemove);
            }

            //Parse data
            foreach (string dataPath in DataPaths)
            {
                //Parse navlight styles
                string navLightStylesPath = Path.Combine(dataPath, "scripts/navlightstyles");

                //Check if navlight styles folder exists
                if (Directory.Exists(navLightStylesPath))
                {
                    string[] files = Directory.GetFiles(navLightStylesPath, "*.navs");
                    foreach (string file in files)
                    {
                        ParseNavLightStyle(file);
                    }
                }

                //Load navlight sprite texture
                string spritePath = Path.Combine(dataPath, "effect/textures/navlight.tga");
                if (File.Exists(spritePath))
                {
                    NavLightSprite = new HWTexture(spritePath, false, true);
                }

                //Parse badges
                string badgesPath = Path.Combine(dataPath, "badges/");

                //Check if badges folder exists
                if (Directory.Exists(badgesPath))
                {
                    string[] files = Directory.GetFiles(badgesPath, "*.tga");
                    foreach (string file in files)
                    {
                        ParseBadge(file);
                    }
                }

                //Parse backgrounds
                string backgroundsPath = Path.Combine(dataPath, "background/");

                //Check if backgrounds folder exists
                if (Directory.Exists(backgroundsPath))
                {
                    ParseBackground(backgroundsPath);
                }
            }

            //When no navlight sprite could have been found
            if (NavLightSprite == null)
            {
                NavLightSprite = Renderer.DefaultTexture;
                MessageBox.Show("Could not find \"effect/textures/navlight.tga\". Be sure to add the default homeworld files to your data paths.", "Failed to find navlight sprite", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void ParseNavLightStyle(string path)
        {
            string file = File.ReadAllText(path);

            string name = Path.GetFileNameWithoutExtension(path);

            float climbTime = 0;
            float topWaitTime = 0;
            float decayTime = 0;
            float bottomWaitTime = 0;

            float illumSawHz = 0;
            float illumSawMin = 0;
            float illumSawMax = 0;
            float illumSawOfs = 0;

            float illumSinHz = 0;
            float illumSinMin = 0;
            float illumSinMax = 0;
            float illumSinOfs = 0;

            bool noSelfLight = false;
            bool linkThrust = false;

            string[] parameters = file.Split(',');

            //Check if there are parameters
            if (parameters.Length <= 0)
                return;

            parameters[0] = parameters[0].Remove(0, parameters[0].IndexOf('{') + 1);
            for (int i = 0; i < parameters.Length - 1; i++)
            {
                string newParameter = Regex.Unescape(parameters[i]);
                string[] split = newParameter.Split('=');
                if (split.Length <= 0)
                    continue;

                string key = split[0].Trim();
                string value = split[1].Trim();

                switch (key)
                {
                    case "climbTime":
                        climbTime = float.Parse(value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "topWaitTime":
                        topWaitTime = float.Parse(value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "decayTime":
                        decayTime = float.Parse(value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "bottomWaitTime":
                        bottomWaitTime = float.Parse(value, System.Globalization.CultureInfo.InvariantCulture);
                        break;

                    case "illumSawHz":
                        illumSawHz = float.Parse(value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "illumSawMin":
                        illumSawMin = float.Parse(value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "illumSawMax":
                        illumSawMax = float.Parse(value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "illumSawOfs":
                        illumSawOfs = float.Parse(value, System.Globalization.CultureInfo.InvariantCulture);
                        break;

                    case "illumSinHz":
                        illumSinHz = float.Parse(value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "illumSinMin":
                        illumSinMin = float.Parse(value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "illumSinMax":
                        illumSinMax = float.Parse(value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "illumSinOfs":
                        illumSinOfs = float.Parse(value, System.Globalization.CultureInfo.InvariantCulture);
                        break;

                    case "noSelfLight":
                        if (value == "0")
                            noSelfLight = false;
                        else
                            noSelfLight = true;
                        break;
                    case "linkThrust":
                        if (value == "0")
                            linkThrust = false;
                        else
                            linkThrust = true;
                        break;
                }
            }

            HWNavLightStyle existingStyle = null;
            foreach (HWNavLightStyle style in NavLightStyles)
            {
                if (style.Name == name)
                {
                    existingStyle = style;
                    break;
                }
            }

            //Check if a style with that name already exists (because of multiple data paths)
            if (existingStyle == null)
                new HWNavLightStyle(name, climbTime, topWaitTime, decayTime, bottomWaitTime, illumSawHz, illumSawMin, illumSawMax, illumSawOfs, illumSinHz, illumSinMin, illumSinMax, illumSinOfs, noSelfLight, linkThrust);
            else
            {
                //Overwrite existing style
                existingStyle.Name = name;
                existingStyle.ClimbTime = climbTime;
                existingStyle.TopWaitTime = topWaitTime;
                existingStyle.DecayTime = decayTime;
                existingStyle.BottomWaitTime = bottomWaitTime;
                existingStyle.IllumSawHz = illumSawHz;
                existingStyle.IllumSawMin = illumSawMin;
                existingStyle.IllumSawMax = illumSawMax;
                existingStyle.IllumSawOfs = illumSawOfs;
                existingStyle.IllumSinHz = illumSinHz;
                existingStyle.IllumSinMin = illumSinMin;
                existingStyle.IllumSinMax = illumSinMax;
                existingStyle.IllumSinOfs = illumSinOfs;
                existingStyle.NoSelfLight = noSelfLight;
                existingStyle.LinkThrust = linkThrust;
            }
        }

        private static void ParseBadge(string path)
        {
            new HWBadge(Path.GetFileNameWithoutExtension(path), path);
        }

        private static void ParseBackground(string path)
        {
            foreach (string dir in Directory.GetDirectories(path))
            {
                string name = Path.GetFileName(dir);
                // check for high-quality textures
                string PosX = Path.Combine(dir, name + "_hq_posx.dds");
                string PosY = Path.Combine(dir, name + "_hq_posy.dds");
                string PosZ = Path.Combine(dir, name + "_hq_posz.dds");
                string NegX = Path.Combine(dir, name + "_hq_negx.dds");
                string NegY = Path.Combine(dir, name + "_hq_negy.dds");
                string NegZ = Path.Combine(dir, name + "_hq_negz.dds");

                // if HQ texture doesn't exist, check for low-quality textures --> if LQ texture doesn't exist, continue with next directory
                if (!File.Exists(PosX)) { PosX = Path.Combine(dir, name + "_posx.dds"); if (!File.Exists(PosX)) continue; }
                if (!File.Exists(PosY)) { PosY = Path.Combine(dir, name + "_posy.dds"); if (!File.Exists(PosY)) continue; }
                if (!File.Exists(PosZ)) { PosZ = Path.Combine(dir, name + "_posz.dds"); if (!File.Exists(PosZ)) continue; }
                if (!File.Exists(NegX)) { NegX = Path.Combine(dir, name + "_negx.dds"); if (!File.Exists(NegX)) continue; }
                if (!File.Exists(NegY)) { NegY = Path.Combine(dir, name + "_negy.dds"); if (!File.Exists(NegY)) continue; }
                if (!File.Exists(NegZ)) { NegZ = Path.Combine(dir, name + "_negz.dds"); if (!File.Exists(NegZ)) continue; }
                
                BackgroundTextures.Add(name, new HWTextureCube(PosX, NegX, PosY, NegY, PosZ, NegZ));
            }
        }
    }
}
