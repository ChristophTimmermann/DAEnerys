
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using static NewShaderManifest.Utils;
using System.Globalization;

namespace NewShaderManifest
{
    public static class Manifest
    {
        private static Regex IntegerRegex = new Regex(@"\d+");

        internal static List<string> DATA_PATHS = new List<string>();

        internal static string Vendor { get; set; }
        internal static string Renderer { get; set; }
        internal static int PartID { get; set; }

        public static Variables Globals = new Variables();
        internal static HashSet<string> AvailablePrograms = new HashSet<string>();
        internal static HashSet<string> AvailableSurfaces = new HashSet<string>();
        internal static Dictionary<string, string> HODAliases = new Dictionary<string, string>();

        internal static Dictionary<string, Surface> LoadedSurfaces = new Dictionary<string, Surface>();
        internal static Dictionary<string, ShaderProgram> LoadedPrograms = new Dictionary<string, ShaderProgram>();

        public static bool IsInitialized { get; private set; } = false;

        public static void Init(List<string> DataPaths)
        {
            if (IsInitialized) return;
            DATA_PATHS = DataPaths;

            Log.Init();

            Log.WriteLine("Vendor: " + (Vendor = GL.GetString(StringName.Vendor)));
            Log.WriteLine("Renderer: " + (Renderer = GL.GetString(StringName.Renderer)));
            Log.WriteLine("PartID: " + (PartID = int.Parse(IntegerRegex.Match(Renderer).Value)));

            ManifestConfig.Load();

            ReloadManifest();

            IsInitialized = true;
        }

        internal static ShaderProgram GetProgram(string name)
        {
            if (!IsInitialized) throw new ManifestNotInitializedException();
            if (!AvailablePrograms.Contains(name)) throw new ProgramNotAvailableException(name);
            if (!LoadedPrograms.ContainsKey(name))
                ProgramLoader.LoadProgram(name);
            return LoadedPrograms[name];
        }

        public static Surface UseSurface(string name)
        {
            if (!IsInitialized) throw new ManifestNotInitializedException();
            if (HODAliases.ContainsKey(name)) name = HODAliases[name];
            if (!AvailableSurfaces.Contains(name)) throw new SurfaceNotAvailableException(name);
            if (!LoadedSurfaces.ContainsKey(name))
                SurfaceLoader.LoadSurface(name);
            return LoadedSurfaces[name];
        }

        public static void ReloadManifest()
        {
            //Surfaces.Clear();
            //Programs.Clear();

            string path = GetDataPath(@"shaders\master.manifest");
            if (!string.IsNullOrEmpty(path))
                Load(new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read)));
            else
                Log.WriteLine(@"Unable to locate shader config file: shaders\master.manifest");

            Log.Close();
        }

        private static void Load(StreamReader sr)
        {
            while (!sr.EndOfStream)
            {
                string line = GetLine(sr);
                if (line == null) throw new ManifestParseException("End of stream detected while parsing the master manifest file.");
                line = line.Trim();
                if (string.IsNullOrWhiteSpace(line)) continue;
                
                ParseStatement(sr, line);
            }
        }

        private static void ParseStatement(StreamReader sr, string input)
        {
            if (input.StartsWith("#import"))
                ParseImport(input);
            else if (input.StartsWith("global"))
                ParseGlobal(input);
            else if (input.StartsWith("load program"))
                ParseLoadProgram(input);
            else if (input.StartsWith("load surface"))
                ParseLoadSurface(input);
            else if (input.StartsWith("hod_alias"))
                ParseHODAlias(input);
            else
                throw new ConfigParseException("Unrecognized command.");
        }

        private static void ParseImport(string input)
        {
            string path = GetDataPath(@"shaders\" + input.Substring(8));
            if (!string.IsNullOrWhiteSpace(path))
                Load(new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read)));
        }

        private static void ParseGlobal(string input)
        {
            string[] args = input.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            PopFirst(ref args);

            string typename = PopFirst(ref args);
            string varname = PopFirst(ref args);
            int i = 0;
            if (typename.StartsWith("float"))
            {
                int length = int.Parse(typename.Substring(5));
                float[] array = new float[length];
                while (args.Length > 0)
                    array[i++] = float.Parse(PopFirst(ref args), CultureInfo.InvariantCulture);
                Globals[varname] = array;
            }
            else if (typename.StartsWith("int"))
            {
                int length = int.Parse(typename.Substring(3));
                int[] array = new int[length];
                while (args.Length > 0)
                    array[i++] = int.Parse(PopFirst(ref args), CultureInfo.InvariantCulture);
                Globals[varname] = array;
            }
            else
            {
                throw new ManifestParseException("Unrecognized type: " + typename);
            }
        }

        private static void ParseLoadProgram(string input)
        {
            string prog = input.Substring(13);
            string path = GetDataPath(@"shaders\gl_prog\" + prog + ".prog");
            if (path != "")
            {
                if (!AvailablePrograms.Contains(prog))
                    AvailablePrograms.Add(prog);
                else
                    Log.WriteLine("Duplicate program detected: " + prog);
            }
            else
                Log.WriteLine("Could not locate program " + prog);
        }

        private static void ParseLoadSurface(string input)
        {
            string surf = input.Substring(13);
            string path = GetDataPath(@"shaders\gl_surf\" + surf + ".surf");
            if (path != "")
            {
                if (!AvailableSurfaces.Contains(surf))
                    AvailableSurfaces.Add(surf);
                else
                    Log.WriteLine("Duplicate surface detected: " + surf);
            }
            else
                Log.WriteLine("Could not locate surface " + surf);
        }

        private static void ParseHODAlias(string input)
        {
            string[] args = input.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            PopFirst(ref args);
            string surf = args[0];
            string shad = args[1];
            string pref = args[2];

            HODAliases[shad] = surf;
        }
    }
}
