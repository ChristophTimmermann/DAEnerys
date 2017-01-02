using System;
using System.Collections.Generic;
using System.IO;
using static NewShaderManifest.Utils;

namespace NewShaderManifest
{
    public class ConfigOption
    {
        private static ConfigOption NewDefaultConfigOption()
        {
            ConfigOption opt = new ConfigOption(0);
            opt.MaxValue = 0;
            return opt;
        }

        public static ConfigOption DEFAULT = NewDefaultConfigOption();
        private int _Value;
        public int DefaultValue { get; internal set; }
        public int MaxValue { get; internal set; } = int.MaxValue;
        public int Value
        {
            get
            {
                return _Value;
            }
            set
            {
                if (value < 0) value = 0;
                if (value > MaxValue) value = MaxValue;
                _Value = value;
            }
        }

        internal ConfigOption(int value)
        {
            DefaultValue = _Value = value;
        }

        public void ResetToDefault()
        {
            _Value = DefaultValue;
        }
    }

    [Serializable]
    public class ConfigOptions : Dictionary<string, ConfigOption>
    {
        public new ConfigOption this[string key]
        {
            get
            {
                if (!ContainsKey(key)) return ConfigOption.DEFAULT;
                return base[key];
            }
            set
            {
                base[key] = value;
            }
        }
    }

    public static class ManifestConfig
    {
        public static ConfigOptions Options = new ConfigOptions();
        internal static List<string> UserOptions = new List<string>();
        internal static List<string> ProgOptions = new List<string>();

        internal static void Load()
        {
            string[] paths = GetDataPath(@"shaders\dev_config.manifest");
            bool found = false;

            foreach (string path in paths)
            {
                if (string.IsNullOrEmpty(path))
                    continue;

                Load(new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read)));
                found = true;
            }

            if(!found)
                Log.WriteLine(@"Unable to locate shader config file: shaders\dev_config.manifest");
        }

        private static void Load(StreamReader sr)
        {
            while (!sr.EndOfStream)
            {
                string line = GetLine(sr);
                if (line == null) throw new ConfigParseException("End of stream detected while parsing the config file.");
                if (string.IsNullOrWhiteSpace(line)) continue;

                ParseStatement(sr, line);
            }
        }

        private static void ParseStatement(StreamReader sr, string input)
        {
            if (input.Contains("#defuser"))
                ParseDefUser(input);
            else if (input.Contains("#defprog"))
                ParseDefProg(input);
            else if (input.Contains("#def"))
                ParseDef(input);
            else if (input.Contains("#if"))
                ParseIf(sr, input);
            else if (input.Contains("#import"))
                ParseImport(input);
            else
                throw new ConfigParseException("Unrecognized command.");
        }

        private static void ParseImport(string input)
        {
            string[] args = input.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string[] paths = GetDataPath(@"shaders\" + args[1]);

            foreach (string path in paths)
            {
                if (string.IsNullOrWhiteSpace(path))
                    continue;

                Load(new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read)));
            }
        }

        private static void ParseDef(string input)
        {
            string[] args = input.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string name = args[1];
            int value;
            bool isnum = int.TryParse(args[2], out value);
            if (name.Contains("_MAX"))
            {
                name = name.Substring(0, name.IndexOf("_MAX"));
                Options[name].MaxValue = value;
            }
            else if (name.Contains("_DEF"))
            {
                name = name.Substring(0, name.IndexOf("_DEF"));
                Options[name].DefaultValue = isnum ? value : Options[args[2]].Value;
            }
            else if (Options.ContainsKey(name))
                Options[name].Value = value;
            else
                Options[name] = new ConfigOption(value);
        }

        private static void ParseDefUser(string input)
        {
            UserOptions.Add(input.Trim().Split(new char[] { ' ' })[1]);
        }

        private static void ParseDefProg(string input)
        {
            ProgOptions.Add(input.Trim().Split(new char[] { ' ' })[1]);
        }

        private static void ParseIf(StreamReader sr, string input)
        {
            bool ignore = !CheckCondition(input.Trim());
            int offset = input.IndexOf('#');

            while (!sr.EndOfStream)
            {
                string line = GetLine(sr);
                if (line == null) throw new ConfigParseException("End of stream detected while parsing the config file.");
                if (string.IsNullOrWhiteSpace(line)) continue;

                int breakoffset = line.IndexOf('#');
                if (line.Contains("#fi") && breakoffset == offset) return;

                if (ignore)
                {
                    if (breakoffset == offset)
                    {
                        if (line.Trim() == "#else")
                            ignore = !ignore;
                        else if (line.Contains("#else"))
                            ignore = !CheckCondition(line.Trim());
                    }
                    continue;
                }
                else if (breakoffset == offset)
                {
                    if (line.Trim() == "#else")
                        ignore = !ignore;
                    else if (line.Contains("#else"))
                        ignore = !CheckCondition(line.Trim());
                    continue;
                }
                ParseStatement(sr, line);
            }
        }

        private static bool CheckCondition(string input)
        {
            string[] args = input.Split(new char[] { ' ' });
            string op = args[1];
            string left = args[2];
            string right = args[3];

            if (op == "str")
            {
                if (left == "vendor")
                    return Manifest.Vendor.Contains(right);
                else if (left == "renderer")
                    return Manifest.Renderer.Contains(right);
                else
                    throw new ConfigParseException("Unrecognized string comparison operator: " + left);
            }
            else if (op == "gte")
            {
                if (left == "part_id")
                    return Manifest.PartID >= int.Parse(right);
                else
                    throw new ConfigParseException("Unrecognized numeric comparison operator: " + left);
            }
            else if (op == "lt")
            {
                if (left == "part_id")
                    return Manifest.PartID < int.Parse(right);
                else
                    throw new ConfigParseException("Unrecognized numeric comparison operator: " + left);
            }
            else
                throw new ConfigParseException("Unrecognized comparison operator: " + op);
        }
    }
}