using System;
using System.IO;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using ShaderManifest;

namespace DAEnerys
{
    static class Dump
    {
        private static Regex rgxNumber = new Regex("[-+]?[0-9]*.?[0-9]+([eE][-+]?[0-9]+)?");

        public static void ADuiePyle()
        {
            StreamWriter sw = new StreamWriter(DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss.ffff") + ".dump");

            sw.WriteLine("Vendor: " + GL.GetString(StringName.Vendor));
            sw.WriteLine("Renderer: " + GL.GetString(StringName.Renderer));
            sw.WriteLine("PartID: " + int.Parse(rgxNumber.Match(GL.GetString(StringName.Renderer)).Value));

            sw.WriteLine("### CONFIG ###");
            Type type_cfg = typeof(Config);
            FieldInfo fi_cfg_defines = type_cfg.GetField("Defines", BindingFlags.NonPublic | BindingFlags.Static);
            Dictionary<string, int> cfg_defines = (Dictionary<string, int>)fi_cfg_defines.GetValue(null);
            foreach (KeyValuePair<string, int> kvp in cfg_defines)
                sw.WriteLine(kvp.Key + " = " + kvp.Value);

            sw.WriteLine("### MANIFEST ###");
            Type type_manifest = typeof(Manifest);
            FieldInfo fi_manifest_programs = type_manifest.GetField("programs", BindingFlags.NonPublic | BindingFlags.Static);
            FieldInfo fi_manifest_surfaces = type_manifest.GetField("surfaces", BindingFlags.NonPublic | BindingFlags.Static);
            FieldInfo fi_manifest_HODAliases = type_manifest.GetField("HODAliases", BindingFlags.NonPublic | BindingFlags.Static);
            Dictionary<string, ShaderManifest.Program> manifest_programs = (Dictionary<string, ShaderManifest.Program>)fi_manifest_programs.GetValue(null);
            Dictionary<string, Surface> manifest_surfaces = (Dictionary<string, Surface>)fi_manifest_surfaces.GetValue(null);
            object manifest_HODAliases = fi_manifest_HODAliases.GetValue(null);

            foreach (KeyValuePair<string, ShaderManifest.Program> kvp in manifest_programs)
            {
                ShaderManifest.Program program = kvp.Value;
                Type type_program = program.GetType();
                FieldInfo fi_program_filename = type_program.GetField("filename", BindingFlags.NonPublic | BindingFlags.Instance);
                PropertyInfo pi_program_VertShader = type_program.GetProperty("VertShader", BindingFlags.NonPublic | BindingFlags.Instance);
                PropertyInfo pi_program_GeomShader = type_program.GetProperty("GeomShader", BindingFlags.NonPublic | BindingFlags.Instance);
                PropertyInfo pi_program_FragShader = type_program.GetProperty("FragShader", BindingFlags.NonPublic | BindingFlags.Instance);
                FieldInfo fi_program_ProcessorDefines = type_program.GetField("ProcessorDefines", BindingFlags.NonPublic | BindingFlags.Instance);
                string program_filename = (string)fi_program_filename.GetValue(program);
                string program_VertShader = (string)pi_program_VertShader.GetValue(program);
                string program_GeomShader = (string)pi_program_GeomShader.GetValue(program);
                string program_FragShader = (string)pi_program_FragShader.GetValue(program);
                Dictionary<string, string> program_ProcessorDefines =
                    (Dictionary<string, string>)fi_program_ProcessorDefines.GetValue(program);

                sw.WriteLine("##### PROGRAM " + kvp.Key + " -> " + program_filename);
                sw.WriteLine("ProgramID = " + program.ProgramID);
                sw.WriteLine("VShaderID = " + program.VShaderID);
                sw.WriteLine("GShaderID = " + program.GShaderID);
                sw.WriteLine("FShaderID = " + program.FShaderID);
                sw.WriteLine("$$$ VertShader $$$");
                sw.WriteLine(program_VertShader);
                sw.WriteLine("$$$ GeomShader $$$");
                sw.WriteLine(program_GeomShader);
                sw.WriteLine("$$$ FragShader $$$");
                sw.WriteLine(program_FragShader);

                sw.WriteLine("--- ATTRIBUTES ---");
                foreach (KeyValuePair<string, ShaderManifest.Program.AttributeInfo> entry in program.Attributes)
                {
                    ShaderManifest.Program.AttributeInfo info = entry.Value;
                    sw.WriteLine(entry.Key + " : " + info.name + " ( " + info.type.ToString() + " ) @ " + info.address + ", size = " + info.size);
                }
                sw.WriteLine("--- UNIFORMS ---");
                foreach (KeyValuePair<string, ShaderManifest.Program.UniformInfo> entry in program.Uniforms)
                {
                    ShaderManifest.Program.UniformInfo info = entry.Value;
                    sw.WriteLine(entry.Key + " : " + info.name + " ( " + info.type.ToString() + " ) @ " + info.address + ", size = " + info.size);
                }
                sw.WriteLine("--- BUFFERS ---");
                foreach (KeyValuePair<string, uint> entry in program.Buffers)
                    sw.WriteLine(entry.Key + " : " + entry.Value);
                sw.WriteLine("--- LOCALS ---");
                foreach (string entry in program.Locals.VarNames)
                    sw.WriteLine(entry + " : " + program.Locals.Get(entry));
                sw.WriteLine("--- UNIFORM MAP ---");
                foreach (KeyValuePair<string, string> entry in program.UniformMap)
                    sw.WriteLine(entry.Key + " : " + entry.Value);
                sw.WriteLine("--- PROCESSOR DEFINES ---");
                foreach (KeyValuePair<string, string> entry in program_ProcessorDefines)
                    sw.WriteLine(entry.Key + " : " + entry.Value);
            }
            foreach (KeyValuePair<string, Surface> kvp in manifest_surfaces)
            {
                Surface surface = kvp.Value;
                Type type_surface = surface.GetType();
                //FieldInfo fi_program_filename = type_program.GetField("filename", BindingFlags.NonPublic);
                //PropertyInfo pi_program_VertShader = type_program.GetProperty("VertShader", BindingFlags.NonPublic);
                //PropertyInfo pi_program_GeomShader = type_program.GetProperty("GeomShader", BindingFlags.NonPublic);
                //PropertyInfo pi_program_FragShader = type_program.GetProperty("FragShader", BindingFlags.NonPublic);
                //FieldInfo fi_program_ProcessorDefines = type_program.GetField("ProcessorDefines", BindingFlags.NonPublic);
                //string program_filename = (string)fi_program_filename.GetValue(program);
                //string program_VertShader = (string)pi_program_VertShader.GetValue(program);
                //string program_GeomShader = (string)pi_program_GeomShader.GetValue(program);
                //string program_FragShader = (string)pi_program_FragShader.GetValue(program);
                //Dictionary<string, string> program_ProcessorDefines =
                //    (Dictionary<string, string>)fi_program_ProcessorDefines.GetValue(program);

                //sw.WriteLine("##### PROGRAM " + kvp.Key + " -> " + program_filename);
                //sw.WriteLine("ProgramID = " + program.ProgramID);
                //sw.WriteLine("VShaderID = " + program.VShaderID);
                //sw.WriteLine("GShaderID = " + program.GShaderID);
                //sw.WriteLine("FShaderID = " + program.FShaderID);
                //sw.WriteLine("$$$ VertShader $$$");
                //sw.WriteLine(program_VertShader);
                //sw.WriteLine("$$$ GeomShader $$$");
                //sw.WriteLine(program_GeomShader);
                //sw.WriteLine("$$$ FragShader $$$");
                //sw.WriteLine(program_FragShader);

                //sw.WriteLine("--- ATTRIBUTES ---");
                //foreach (KeyValuePair<string, ShaderManifest.Program.AttributeInfo> entry in program.Attributes)
                //{
                //    ShaderManifest.Program.AttributeInfo info = entry.Value;
                //    sw.WriteLine(entry.Key + " : " + info.name + " ( " + info.type.ToString() + " ) @ " + info.address + ", size = " + info.size);
                //}
                //sw.WriteLine("--- UNIFORMS ---");
                //foreach (KeyValuePair<string, ShaderManifest.Program.UniformInfo> entry in program.Uniforms)
                //{
                //    ShaderManifest.Program.UniformInfo info = entry.Value;
                //    sw.WriteLine(entry.Key + " : " + info.name + " ( " + info.type.ToString() + " ) @ " + info.address + ", size = " + info.size);
                //}
                //sw.WriteLine("--- BUFFERS ---");
                //foreach (KeyValuePair<string, uint> entry in program.Buffers)
                //    sw.WriteLine(entry.Key + " : " + entry.Value);
                //sw.WriteLine("--- LOCALS ---");
                //foreach (string entry in program.Locals.VarNames)
                //    sw.WriteLine(entry + " : " + program.Locals.Get(entry));
                //sw.WriteLine("--- UNIFORM MAP ---");
                //foreach (KeyValuePair<string, string> entry in program.UniformMap)
                //    sw.WriteLine(entry.Key + " : " + entry.Value);
                //sw.WriteLine("--- PROCESSOR DEFINES ---");
                //foreach (KeyValuePair<string, string> entry in program_ProcessorDefines)
                //    sw.WriteLine(entry.Key + " : " + entry.Value);
            }
            MethodInfo mi_manifest_HODAliases_GetEnumerator = manifest_HODAliases.GetType().GetMethod("GetEnumerator");
            IEnumerator enumerator = (IEnumerator)mi_manifest_HODAliases_GetEnumerator.Invoke(manifest_HODAliases, null);
            while (enumerator.MoveNext())
                sw.WriteLine(enumerator.Current + " = " + enumerator.Current.ToString());



            sw.Close();
        }
    }
}
