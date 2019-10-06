using NLua;
using OpenTK;
using System;
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
        public static List<HWShipType> ShipTypes = new List<HWShipType>();

        public static HWTexture NavLightSprite;

        static Lua lua;
        static LuaTable shipConfig;
        private static HWShipType currentShipType;

        public static void ParseDataPaths()
        {
            //Check if there are any data paths
            if (DataPaths.Count <= 0)
            {
                MessageBox.Show("You did not specify any data paths yet!\nThis is very important as DAEnerys needs shaders and other things from your Homeworld data.\nDefine them in the settings window.\n\nDAENERYS WILL NOT WORK CORRECTLY WITHOUT THEM.", "No data paths specified", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                lua = new Lua();

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
                    NavLightSprite = new HWTexture(spritePath, false, true, true);
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

                //Parse ship types
                string shipTypesPath = Path.Combine(dataPath, "ship");

                //Check if ship types folder exists
                if (Directory.Exists(shipTypesPath))
                {
                    string[] files = Directory.GetFiles(shipTypesPath, "*.ship", SearchOption.AllDirectories);
                    foreach (string file in files)
                    {
                        ParseShipType(file);
                    }
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
                if (!File.Exists(PosX))
                { PosX = Path.Combine(dir, name + "_posx.dds"); if (!File.Exists(PosX)) continue; }
                if (!File.Exists(PosY))
                { PosY = Path.Combine(dir, name + "_posy.dds"); if (!File.Exists(PosY)) continue; }
                if (!File.Exists(PosZ))
                { PosZ = Path.Combine(dir, name + "_posz.dds"); if (!File.Exists(PosZ)) continue; }
                if (!File.Exists(NegX))
                { NegX = Path.Combine(dir, name + "_negx.dds"); if (!File.Exists(NegX)) continue; }
                if (!File.Exists(NegY))
                { NegY = Path.Combine(dir, name + "_negy.dds"); if (!File.Exists(NegY)) continue; }
                if (!File.Exists(NegZ))
                { NegZ = Path.Combine(dir, name + "_negz.dds"); if (!File.Exists(NegZ)) continue; }

                BackgroundTextures.Add(name, new HWTextureCube(PosX, NegX, PosY, NegY, PosZ, NegZ));
            }
        }

        private static void ParseShipType(string path)
        {
            lua = new Lua();
            string file = File.ReadAllText(path);

            Type type = typeof(HWData);

            lua.RegisterFunction("StartShipConfig", null, type.GetMethod("StartShipConfig"));
            lua.RegisterFunction("getShipNum", null, type.GetMethod("GetShipNum"));
            lua.RegisterFunction("getShipStr", null, type.GetMethod("GetShipStr"));
            lua.RegisterFunction("setSupplyValue", null, type.GetMethod("SetSupplyValue"));
            lua.RegisterFunction("StartShipWeaponConfig", null, type.GetMethod("StartShipWeaponConfig"));
            lua.RegisterFunction("setEngineBurn", null, type.GetMethod("SetEngineBurn"));
            lua.RegisterFunction("setEngineGlow", null, type.GetMethod("SetEngineGlow"));
            lua.RegisterFunction("setEngineTrail", null, type.GetMethod("SetEngineTrail"));
            lua.RegisterFunction("setTargetBox", null, type.GetMethod("SetTargetBox"));
            lua.RegisterFunction("LoadModel", null, type.GetMethod("LoadModel"));
            lua.RegisterFunction("LoadSharedModel", null, type.GetMethod("LoadSharedModel"));
            lua.RegisterFunction("addShield", null, type.GetMethod("AddShield"));
            lua.RegisterFunction("AddShipAbility", null, type.GetMethod("AddShipAbility"));
            lua.RegisterFunction("setConcurrentBuildLimit", null, type.GetMethod("SetConcurrentBuildLimit"));
            lua.RegisterFunction("setCollisionDamageToModifier", null, type.GetMethod("SetCollisionDamageToModifier"));
            lua.RegisterFunction("setCollisionDamageFromModifier", null, type.GetMethod("SetCollisionDamageFromModifier"));
            lua.RegisterFunction("setSpecialDieTime", null, type.GetMethod("SetSpecialDieTime"));
            lua.RegisterFunction("addMagneticField", null, type.GetMethod("AddMagneticField"));

            file = DisableLuaFunctionInString(file, "addAbility");
            file = DisableLuaFunctionInString(file, "addCustomCode");
            file = DisableLuaFunctionInString(file, "SpawnSalvageOnDeath");
            file = DisableLuaFunctionInString(file, "SpawnDustCloudOnDeath");
            file = DisableLuaFunctionInString(file, "loadShipPatchList");
            file = DisableLuaFunctionInString(file, "loadLatchPointList");
            file = DisableLuaFunctionInString(file, "AddShipMultiplier");
            file = DisableLuaFunctionInString(file, "setTacticsMults");
            file = DisableLuaFunctionInString(file, "setSpeedvsAccuracyApplied");
            file = DisableLuaFunctionInString(file, "StartShipHardPointConfig");

            string name = Path.GetFileNameWithoutExtension(path).ToLower();

            //Check if a type with that name already exists (because of multiple data paths)
            if (!HWShipType.ShipTypes.ContainsKey(name))
                //new HWShipType(name, avoidanceFamily);
                currentShipType = new HWShipType(name, path);
            else
            {
                //Overwrite existing style
                currentShipType = HWShipType.ShipTypes[name];
                currentShipType.Name = name;
                //existingType.AvoidanceFamily = avoidanceFamily;
            }

            lua.DoString(file);
            AvoidanceFamily avoidanceFamily = AvoidanceFamily.None;

            foreach (KeyValuePair<object, object> de in shipConfig)
            {
                switch (de.Key.ToString())
                {
                    case "AvoidanceFamily":
                        string family = de.Value.ToString();
                        avoidanceFamily = (AvoidanceFamily)Enum.Parse(typeof(AvoidanceFamily), family, true);
                        break;
                }
            }
        }

        #region Lua-Functions
        public static LuaTable StartShipConfig()
        {
            shipConfig = (LuaTable)lua.DoString("return {}")[0];
            return shipConfig;
        }


        private static string DisableLuaFunctionInString(string text, string function)
        {
            string newText = text;

            int index = 0;
            bool inFunction = false;
            bool inString = false;

            while (index <= newText.Length)
            {
                if (index < newText.Length)
                {
                    if (newText[index] == '\"' || newText[index] == '\'') //String start/end found
                    {
                        inString = !inString;
                    }
                }

                if (!inFunction)
                {
                    if (newText.Length >= index + function.Length)
                    {
                        if (newText.Substring(index, function.Length) == function) //Function occurence found
                        {
                            newText = newText.Insert(index, "--[[");
                            index += function.Length;
                            inFunction = true;
                        }
                    }
                }
                else
                {
                    if (!inString)
                    {
                        if (newText[index] == ')') //Function end found
                        {
                            newText = newText.Insert(index + 1, "]]");
                            inFunction = false;
                        }
                    }
                }

                index++;
            }

            return newText;
        }

        public static void SetCollisionDamageToModifier(LuaTable entity, string family, float modifier)
        {
            //Unused
        }

        public static void SetCollisionDamageFromModifier(LuaTable entity, string family, float modifier)
        {
            //Unused
        }

        public static void SpawnAsteroidOnDeath(LuaTable entity, string type, int count, float x, float y, float z, float randomX, float randomY, float randomZ, float velocityX, float velocityY, float velocityZ, float unknown1, float unknown2, float unknown3, float unknown4, float unknown5, float unknown6, float unknown7)
        {
            //SpawnAsteroidOnDeath(NewResourceTypeAsteroid_5_piece01, "Asteroid_4_piece01", 1, 0,  0,       0,            50,           -65,             0,             -30,               0,               0,              0,              0,              8,              0,              0,             -5,              2)
            //Unused
        }

        public static void ResourceAttackMode(LuaTable entity, LuaBase mode)
        {
            //Unused
        }

        public static float GetShipNum(LuaTable ship, string property, float parameter)
        {
            //Unused
            return 0;
        }

        public static string GetShipStr(LuaTable ship, string property, string parameter)
        {
            //Unused
            return "";
        }

        public static void SetSupplyValue(LuaTable ship, string family, float parameter)
        {
            //Unused
        }

        public static void StartShipWeaponConfig(LuaTable ship, string weapon, string joint, string animation)
        {
            //Unused
        }

        public static void SetEngineBurn(LuaTable ship, int sparkCount, float opacityLow, float opacityHigh, float sparkSize, float speedSparkSize, float flareMin, float flarePos, float flareSize)
        {
            //Unused
        }

        public static void SetEngineGlow(LuaTable ship, float unknown2, float unknown3, float unknown4, float unknown5, float unknown6, float unknown7, float unknown8, LuaTable color)
        {
            //Unused
        }

        public static void SetEngineTrail(LuaTable entity, int index, float lingerTime, string textureName, float bulgeFrequency, float textureScrollFactor, float textureScaleFactor, float diameterFactor)
        {
            //Unused
        }

        public static void SetTargetBox(LuaTable ship, int index, float minX, float minY, float minZ, float maxX, float maxY, float maxZ)
        {
            if (currentShipType == null)
                return;

            new HWShipType.TargetBox(currentShipType, index, new Vector3(minX, minY, minZ), new Vector3(maxX, maxY, maxZ));
        }

        public static void LoadModel(LuaTable entity, int enabled)
        {
            //Unused
        }

        public static void LoadSharedModel(LuaTable entity, string objectToShareWith)
        {
            //Unused
        }

        public static void AddShield(LuaTable ship, string type, float max, float rechargeTime)
        {
            //Unused
        }

        public static void AddShipAbility(LuaTable ship, string ability, int active, string target, float radius)
        {
            //Unused
        }

        public static void SetConcurrentBuildLimit(LuaTable ship, int min, int max)
        {
            //Unused
        }

        public static void SetSpecialDieTime(LuaTable ship, string killer, float time)
        {
            //Unused
        }

        public static void AddMagneticField(LuaTable entity, string type, float unknown1, float unknown2, string mesh, float unknown3, string mesh2, float unknown4, string hit, string effect)
        {
            //Unused
        }
#endregion

        public enum AvoidanceFamily
        {
            None,
            DontAvoid,
            Strikecraft,
            Utility,
            Frigate,
            SmallRock,
            Capital,
            SuperCap,
            BattleCruiser,
            MotherShip,
            BigRock,
            SuperPriority,
        }
    }
}