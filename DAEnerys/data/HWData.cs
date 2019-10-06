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
            InitLuaInterpreter();

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

        private static void InitLuaInterpreter()
        {
            lua = new Lua();
            
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

            LoadMathLib();
            LoadExtraFunctions();
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
            string file = File.ReadAllText(path);

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

        private static void LoadMathLib()
        {
            Type math = typeof(Math);
            Type[] args = new Type[] { typeof(double) };
            lua.RegisterFunction("abs", math.GetMethod("Abs", args));
            lua.RegisterFunction("sin", math.GetMethod("Sin"));
            lua.RegisterFunction("cos", math.GetMethod("Cos"));
            lua.RegisterFunction("tan", math.GetMethod("Tan"));
            lua.RegisterFunction("asin", math.GetMethod("Asin"));
            lua.RegisterFunction("acos", math.GetMethod("Acos"));
            lua.RegisterFunction("acos", math.GetMethod("Acos"));
            lua.RegisterFunction("atan", math.GetMethod("Atan"));
            lua.RegisterFunction("atan2", math.GetMethod("Atan2"));
            lua.RegisterFunction("ceil", math.GetMethod("Ceiling", args));
            lua.RegisterFunction("floor", math.GetMethod("Floor", args));
            lua.RegisterFunction("mod", math.GetMethod("IEEERemainder"));
            lua.RegisterFunction("sqrt", math.GetMethod("Sqrt"));
            lua.RegisterFunction("pow", math.GetMethod("Pow"));
            lua.RegisterFunction("log", math.GetMethod("Log", args));
            lua.RegisterFunction("log10", math.GetMethod("Log10"));
            lua.RegisterFunction("exp", math.GetMethod("Exp"));
            lua.RegisterFunction("deg", typeof(HWData).GetMethod("ToDegrees"));
            lua.RegisterFunction("rad", typeof(HWData).GetMethod("ToRadians"));
            lua.RegisterFunction("min", math.GetMethod("Min", new Type[] { typeof(double), typeof(double) }));
            lua.RegisterFunction("max", math.GetMethod("Max", new Type[] { typeof(double), typeof(double) }));

            lua.RegisterFunction("DAENERYS_random0", typeof(HWData).GetMethod("Random0"));
            lua.RegisterFunction("DAENERYS_random1", typeof(HWData).GetMethod("Random1"));
            lua.RegisterFunction("DAENERYS_random2", typeof(HWData).GetMethod("Random2"));
            lua.RegisterFunction("randomseed", typeof(HWData).GetMethod("RandomSeed"));

            lua.DoString(@"
function random(a, b)
    if (b) then
        return DAENERYS_random2(a, b)
    else
        if (a) then
            return DAENERYS_random1(a)
        else
            return DAENERYS_random0()
        end
    end
end");
        }

        private static void LoadExtraFunctions()
        {
            Type type = typeof(HWData);

            lua.RegisterFunction("DAENERYS_addAbility", null, type.GetMethod("AddAbility"));
            lua.DoString(@"
                function addAbility(entity, ability, ...)
	                return DAENERYS_addAbility(entity, ability, {...})
                end");

            lua.RegisterFunction("DAENERYS_addCustomCode", null, type.GetMethod("AddCustomCode"));
            lua.DoString(@"
                function addCustomCode(...)
	                return DAENERYS_addCustomCode({...})
                end");

            lua.RegisterFunction("DAENERYS_SpawnSalvageOnDeath", null, type.GetMethod("SpawnSalvageOnDeath"));
            lua.DoString(@"
                function SpawnSalvageOnDeath(entity, ...)
	                return DAENERYS_SpawnSalvageOnDeath(entity, {...})
                end");

            lua.RegisterFunction("DAENERYS_SpawnDustCloudOnDeath", null, type.GetMethod("SpawnDustCloudOnDeath"));
            lua.DoString(@"
                function SpawnDustCloudOnDeath(entity, ...)
	                return DAENERYS_SpawnDustCloudOnDeath(entity, {...})
                end");

            lua.RegisterFunction("DAENERYS_loadShipPatchList", null, type.GetMethod("LoadShipPatchList"));
            lua.DoString(@"
                function loadShipPatchList(entity, ...)
	                return DAENERYS_loadShipPatchList(entity, {...})
                end");

            lua.RegisterFunction("DAENERYS_loadLatchPointList", null, type.GetMethod("LoadLatchPointList"));
            lua.DoString(@"
                function loadLatchPointList(entity, ...)
	                return DAENERYS_loadLatchPointList(entity, {...})
                end");

            lua.RegisterFunction("DAENERYS_AddShipMultiplier", null, type.GetMethod("AddShipMultiplier"));
            lua.DoString(@"
                function AddShipMultiplier(entity, ...)
	                return DAENERYS_AddShipMultiplier(entity, {...})
                end");

            lua.RegisterFunction("DAENERYS_setTacticsMults", null, type.GetMethod("SetTacticsMults"));
            lua.DoString(@"
                function setTacticsMults(entity, ...)
	                return DAENERYS_setTacticsMults(entity, {...})
                end");

            lua.RegisterFunction("DAENERYS_setSpeedvsAccuracyApplied", null, type.GetMethod("SetSpeedvsAccuracyApplied"));
            lua.DoString(@"
                function setSpeedvsAccuracyApplied(entity, ...)
	                return DAENERYS_setSpeedvsAccuracyApplied(entity, {...})
                end");

            lua.RegisterFunction("DAENERYS_StartShipHardPointConfig", null, type.GetMethod("StartShipHardPointConfig"));
            lua.DoString(@"
                function StartShipHardPointConfig(entity, ...)
	                return DAENERYS_StartShipHardPointConfig(entity, {...})
                end");
        }


        #region Lua-Functions
        public static LuaTable StartShipConfig()
        {
            shipConfig = (LuaTable)lua.DoString("return {}")[0];
            return shipConfig;
        }


        public static void AddAbility(LuaTable entity, string ability, LuaTable arguments)
        {
            //Unused
        }
        public static void AddCustomCode(LuaTable arguments)
        {
            //Unused
        }
        public static void SpawnSalvageOnDeath(LuaTable entity, LuaTable arguments)
        {
            //Unused
        }
        public static void SpawnDustCloudOnDeath(LuaTable entity, LuaTable arguments)
        {
            //Unused
        }
        public static void LoadShipPatchList(LuaTable entity, LuaTable arguments)
        {
            //Unused
        }
        public static void LoadLatchPointList(LuaTable entity, LuaTable arguments)
        {
            //Unused
        }
        public static void AddShipMultiplier(LuaTable entity, LuaTable arguments)
        {
            //Unused
        }
        public static void SetTacticsMults(LuaTable entity, LuaTable arguments)
        {
            //Unused
        }
        public static void SetSpeedvsAccuracyApplied(LuaTable entity, LuaTable arguments)
        {
            //Unused
        }
        public static void StartShipHardPointConfig(LuaTable entity, LuaTable arguments)
        {
            //Unused
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

        //MathLib
        public static double ToRadians(double value)
        {
            return value * 180.0 / Math.PI;
        }

        public static double ToDegrees(double value)
        {
            return value * Math.PI / 180.0;
        }

        private static Random rng = new Random();

        public static double Random0()
        {
            return rng.NextDouble();
        }

        public static int Random1(double u)
        {
            return (int)(rng.NextDouble() * u + 1);
        }

        public static int Random2(double l, double u)
        {
            return (int)(rng.NextDouble() * (u - l + 1) + 1);
        }

        public static void RandomSeed(int seed)
        {
            rng = new Random(seed);
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