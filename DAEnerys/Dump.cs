//using System;
//using System.IO;
//using System.Reflection;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Threading.Tasks;
//using OpenTK.Graphics.OpenGL;
//using ShaderManifest;

//namespace DAEnerys
//{
//    static class Dump
//    {
//        private static Regex rgxNumber = new Regex("[-+]?[0-9]*.?[0-9]+([eE][-+]?[0-9]+)?");

//        public static void ADuiePyle()
//        {
//            StreamWriter sw = new StreamWriter(DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss.ffff") + ".dump");

//            sw.WriteLine("Vendor: " + GL.GetString(StringName.Vendor));
//            sw.WriteLine("Renderer: " + GL.GetString(StringName.Renderer));
//            sw.WriteLine("PartID: " + int.Parse(rgxNumber.Match(GL.GetString(StringName.Renderer)).Value));

//            sw.WriteLine("### CONFIG ###");
//            Type type_cfg = typeof(Config);
//            FieldInfo fi_cfg_defines = type_cfg.GetField("Defines", BindingFlags.NonPublic | BindingFlags.Static);
//            Dictionary<string, int> cfg_defines = (Dictionary<string, int>)fi_cfg_defines.GetValue(null);
//            foreach (KeyValuePair<string, int> kvp in cfg_defines)
//                sw.WriteLine(kvp.Key + " = " + kvp.Value);

//            sw.WriteLine("### MANIFEST ###");
//            Type type_manifest = typeof(Manifest);
//            FieldInfo fi_manifest_programs = type_manifest.GetField("programs", BindingFlags.NonPublic | BindingFlags.Static);
//            FieldInfo fi_manifest_surfaces = type_manifest.GetField("surfaces", BindingFlags.NonPublic | BindingFlags.Static);
//            FieldInfo fi_manifest_HODAliases = type_manifest.GetField("HODAliases", BindingFlags.NonPublic | BindingFlags.Static);

//            sw.WriteLine("!!! GLOBALS !!!");
//            foreach (string varname in Manifest.Globals.VarNames)
//                sw.WriteLine(varname + " : " + VariableToString(Manifest.Globals.Get(varname)));

//            Dictionary<string, ShaderManifest.Program> manifest_programs = (Dictionary<string, ShaderManifest.Program>)fi_manifest_programs.GetValue(null);
//            Dictionary<string, Surface> manifest_surfaces = (Dictionary<string, Surface>)fi_manifest_surfaces.GetValue(null);
//            object manifest_HODAliases = fi_manifest_HODAliases.GetValue(null);

//            sw.WriteLine("@@@ PROGRAMS @@@");
//            foreach (KeyValuePair<string, ShaderManifest.Program> kvp in manifest_programs)
//            {
//                if (kvp.Key != "sob_ship") continue;

//                ShaderManifest.Program program = kvp.Value;
//                Type type_program = program.GetType();
//                FieldInfo fi_program_filename = type_program.GetField("filename", BindingFlags.NonPublic | BindingFlags.Instance);
//                PropertyInfo pi_program_VertShader = type_program.GetProperty("VertShader", BindingFlags.NonPublic | BindingFlags.Instance);
//                PropertyInfo pi_program_GeomShader = type_program.GetProperty("GeomShader", BindingFlags.NonPublic | BindingFlags.Instance);
//                PropertyInfo pi_program_FragShader = type_program.GetProperty("FragShader", BindingFlags.NonPublic | BindingFlags.Instance);
//                FieldInfo fi_program_ProcessorDefines = type_program.GetField("ProcessorDefines", BindingFlags.NonPublic | BindingFlags.Instance);
//                string program_filename = (string)fi_program_filename.GetValue(program);
//                string program_VertShader = (string)pi_program_VertShader.GetValue(program);
//                string program_GeomShader = (string)pi_program_GeomShader.GetValue(program);
//                string program_FragShader = (string)pi_program_FragShader.GetValue(program);
//                Dictionary<string, string> program_ProcessorDefines =
//                    (Dictionary<string, string>)fi_program_ProcessorDefines.GetValue(program);

//                sw.WriteLine("##### PROGRAM " + kvp.Key + " -> " + program_filename);
//                sw.WriteLine("ProgramID = " + program.ProgramID);
//                sw.WriteLine("VShaderID = " + program.VShaderID);
//                sw.WriteLine("GShaderID = " + program.GShaderID);
//                sw.WriteLine("FShaderID = " + program.FShaderID);
//                sw.WriteLine("$$$ VertShader $$$");
//                sw.WriteLine(program_VertShader);
//                sw.WriteLine("$$$ GeomShader $$$");
//                sw.WriteLine(program_GeomShader);
//                sw.WriteLine("$$$ FragShader $$$");
//                sw.WriteLine(program_FragShader);

//                sw.WriteLine("--- ATTRIBUTES ---");
//                foreach (KeyValuePair<string, ShaderManifest.Program.AttributeInfo> entry in program.Attributes)
//                {
//                    ShaderManifest.Program.AttributeInfo info = entry.Value;
//                    sw.WriteLine(entry.Key + " : " + info.name + " ( " + info.type.ToString() + " ) @ " + info.address + ", size = " + info.size);
//                }
//                sw.WriteLine("--- UNIFORMS ---");
//                foreach (KeyValuePair<string, ShaderManifest.Program.UniformInfo> entry in program.Uniforms)
//                {
//                    ShaderManifest.Program.UniformInfo info = entry.Value;
//                    sw.WriteLine(entry.Key + " : " + info.name + " ( " + info.type.ToString() + " ) @ " + info.address + ", size = " + info.size);
//                }
//                sw.WriteLine("--- BUFFERS ---");
//                foreach (KeyValuePair<string, uint> entry in program.Buffers)
//                    sw.WriteLine(entry.Key + " : " + entry.Value);
//                sw.WriteLine("--- LOCALS ---");
//                foreach (string entry in program.Locals.VarNames)
//                    sw.WriteLine(entry + " : " + VariableToString(program.Locals.Get(entry)));
//                sw.WriteLine("--- UNIFORM MAP ---");
//                foreach (KeyValuePair<string, string> entry in program.UniformMap)
//                    sw.WriteLine(entry.Key + " : " + entry.Value);
//                sw.WriteLine("--- PROCESSOR DEFINES ---");
//                foreach (KeyValuePair<string, string> entry in program_ProcessorDefines)
//                    sw.WriteLine(entry.Key + " : " + entry.Value);
//            }

//            sw.WriteLine("@@@ SURFACES @@@");
//            foreach (KeyValuePair<string, Surface> kvp in manifest_surfaces)
//            {
//                if (kvp.Key != "sob_ship") continue;

//                Surface surface = kvp.Value;
//                Type type_surface = surface.GetType();
//                FieldInfo fi_surface_protect = type_surface.GetField("protect", BindingFlags.NonPublic | BindingFlags.Instance);
//                FieldInfo fi_surface_Locals = type_surface.GetField("Locals", BindingFlags.NonPublic | BindingFlags.Instance);
//                FieldInfo fi_surface_layers = type_surface.GetField("layers", BindingFlags.NonPublic | BindingFlags.Instance);

//                Dictionary<string, object> surface_protect =
//                    (Dictionary<string, object>)fi_surface_protect.GetValue(surface);
//                Variables surface_Locals = (Variables)fi_surface_Locals.GetValue(surface);
//                IList surface_layers = (IList)fi_surface_layers.GetValue(surface);

//                sw.WriteLine("##### SURFACE " + kvp.Key);
//                sw.WriteLine("--- PROTECT ---");
//                foreach (KeyValuePair<string, object> entry in surface_protect)
//                    sw.WriteLine(entry.Key + " : " + entry.Value);
//                sw.WriteLine("--- LOCALS ---");
//                foreach (string entry in surface_Locals.VarNames)
//                    sw.WriteLine(entry + " : " + VariableToString(surface_Locals.Get(entry)));

//                sw.WriteLine("--- LAYERS ---");
//                foreach (object entry in surface_layers)
//                {
//                    Type type_SurfaceLayer = entry.GetType();
//                    List<KeyValuePair<string, object>> GLOps = (List<KeyValuePair<string, object>>)
//                        type_SurfaceLayer.GetField("GLOps", BindingFlags.NonPublic | BindingFlags.Instance)
//                        .GetValue(entry);
//                    string ProgramName = (string)
//                        type_SurfaceLayer.GetProperty("ProgramName", BindingFlags.NonPublic | BindingFlags.Instance)
//                        .GetValue(entry);
//                    Variables SurfaceLayer_Locals = (Variables)
//                        type_SurfaceLayer.GetField("Locals", BindingFlags.NonPublic | BindingFlags.Instance)
//                        .GetValue(entry);

//                    sw.WriteLine("%%% LAYER " + ProgramName);
//                    sw.WriteLine("^^^ GLOps ^^^");
//                    foreach (KeyValuePair<string, object> glOP in GLOps)
//                        sw.WriteLine(glOP.Key + " : " + VariableToString(glOP.Value));
//                    sw.WriteLine("^^^ LOCALS ^^^");
//                    foreach (string varname in surface_Locals.VarNames)
//                        sw.WriteLine(varname + " : " + VariableToString(surface_Locals.Get(varname)));
//                }
//            }
//            sw.WriteLine("##### HODAliases #####");
//            MethodInfo mi_manifest_HODAliases_GetEnumerator = manifest_HODAliases.GetType().GetMethod("GetEnumerator");
//            IEnumerator enumerator = (IEnumerator)mi_manifest_HODAliases_GetEnumerator.Invoke(manifest_HODAliases, null);
//            while (enumerator.MoveNext())
//            {
//                object kvp = enumerator.Current;
//                Type type_kvp = kvp.GetType();
//                FieldInfo fi_kvp_key = type_kvp.GetField("key", BindingFlags.NonPublic | BindingFlags.Instance);
//                FieldInfo fi_kvp_value = type_kvp.GetField("value", BindingFlags.NonPublic | BindingFlags.Instance);
//                string kvp_key = (string)fi_kvp_key.GetValue(kvp);
//                object kvp_value = fi_kvp_value.GetValue(kvp);
//                Type type_kvp_value = kvp_value.GetType();
//                PropertyInfo fi_HODAlias_Surface = type_kvp_value.GetProperty("Surface", BindingFlags.NonPublic | BindingFlags.Instance);
//                PropertyInfo fi_HODAlias_Shader = type_kvp_value.GetProperty("Shader", BindingFlags.NonPublic | BindingFlags.Instance);
//                PropertyInfo fi_HODAlias_Prefix = type_kvp_value.GetProperty("Prefix", BindingFlags.NonPublic | BindingFlags.Instance);
//                string HODAlias_Surface = (string)fi_HODAlias_Surface.GetValue(kvp_value);
//                string HODAlias_Shader = (string)fi_HODAlias_Shader.GetValue(kvp_value);
//                string HODAlias_Prefix = (string)fi_HODAlias_Prefix.GetValue(kvp_value);

//                sw.WriteLine(kvp_key + " = " + HODAlias_Surface + " <- " + HODAlias_Shader + " - " + HODAlias_Prefix);
//            }



//            sw.Close();
//        }

//        private static string VariableToString(dynamic var)
//        {
//            Type type_var = var.GetType();
//            if (type_var.IsArray)
//            {
//                if (type_var.Name == "Single[]")
//                    return ArrayToString((float[])var);
//                else if (type_var.Name == "Int32[]")
//                    return ArrayToString((int[])var);
//                else
//                    return ArrayToString((object[])var);
//            }
//            else
//            {
//                return var.ToString();
//            }
//        }

//        private static string ArrayToString(float[] array)
//        {
//            StringBuilder str = new StringBuilder("{");
//            foreach (float val in array)
//                str.Append(val.ToString()).Append(",");
//            str.Append("}");
//            return str.ToString();
//        }

//        private static string ArrayToString(object[] array)
//        {
//            StringBuilder str = new StringBuilder("{");
//            foreach (float val in array)
//                str.Append(val.ToString()).Append(",");
//            str.Append("}");
//            return str.ToString();
//        }

//        private static string ArrayToString(int[] array)
//        {
//            StringBuilder str = new StringBuilder("{");
//            foreach (float val in array)
//                str.Append(val.ToString()).Append(",");
//            str.Append("}");
//            return str.ToString();
//        }
//    }
//}
