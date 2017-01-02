using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using static NewShaderManifest.Utils;
using System.Globalization;

namespace NewShaderManifest
{
    internal class ShaderProgram
    {

        public class AttributeInfo
        {
            public string name = "";
            public int address = -1;
            public int size = 0;
            public ActiveAttribType type;
        }

        public class UniformInfo
        {
            public string name = "";
            public int address = -1;
            public int size = 0;
            public ActiveUniformType type;
        }

        internal static ShaderProgram DefaultProgram = null;

        public int ProgramID { get; private set; } = -1;
        public int VShaderID { get; private set; } = -1;
        public int GShaderID { get; private set; } = -1;
        public int FShaderID { get; private set; } = -1;
        private int AttributeCount = 0;
        private int UniformCount = 0;

        public Dictionary<string, AttributeInfo> Attributes { get; private set; }
            = new Dictionary<string, AttributeInfo>();
        public Dictionary<string, UniformInfo> Uniforms { get; private set; }
            = new Dictionary<string, UniformInfo>();
        public Dictionary<string, uint> Buffers { get; private set; }
            = new Dictionary<string, uint>();
        
        private Dictionary<string, string> ProcessorDefines
            = new Dictionary<string, string>();

        internal string filename;
        internal string VertShader { get; set; } = "";
        internal string GeomShader { get; set; } = "";
        internal string FragShader { get; set; } = "";

        public Variables Locals = new Variables();
        public Dictionary<string, string> UniformMap { get; set; }
            = new Dictionary<string, string>();

        private bool Useable = false;

        internal ShaderProgram(string filename)
        {
            this.filename = filename;
        }

        public void Reload()
        {
            VertShader = "";
            GeomShader = "";
            FragShader = "";

            ProgramLoader.ReloadProgram(this);
        }

        internal void SetUniform(string dest, dynamic var)
        {
            if (!Useable) return;
            if (UniformMap.ContainsKey(dest))
                dest = UniformMap[dest];
            if (!Uniforms.ContainsKey(dest))
            {
                if (Uniforms.ContainsKey(dest + "[0]"))
                    dest = dest + "[0]";
                else
                    return;
            }
            if (var is float[])
            {
                float[] arr = (float[])var;
                UniformInfo unif = Uniforms[dest];
                if (unif.type == ActiveUniformType.FloatVec2 && arr.Length == 2)
                    GL.Uniform2(Uniforms[dest].address, new Vector2(arr[0], arr[1]));
                else if (unif.type == ActiveUniformType.FloatVec3 && arr.Length == 3)
                    GL.Uniform3(Uniforms[dest].address, new Vector3(arr[0], arr[1], arr[2]));
                else if (unif.type == ActiveUniformType.FloatVec4 && arr.Length == 4)
                    GL.Uniform4(Uniforms[dest].address, new Vector4(arr[0], arr[1], arr[2], arr[3]));
                else if (unif.type == ActiveUniformType.FloatVec4 && (arr.Length % 4) == 0)
                {
                    GL.Uniform4(Uniforms[dest].address, arr.Length / 4, arr);
                }
                else
                    throw new Exception();
            }
            else if (var is int[])
            {
                int[] arr = (int[])var;
                UniformInfo unif = Uniforms[dest];
                if (unif.type == ActiveUniformType.IntVec2 && arr.Length == 2)
                    GL.Uniform2(Uniforms[dest].address, arr[0], arr[1]);
                else if (unif.type == ActiveUniformType.IntVec3 && arr.Length == 3)
                    GL.Uniform3(Uniforms[dest].address, new Vector3(arr[0], arr[1], arr[2]));
                else if (unif.type == ActiveUniformType.IntVec4 && arr.Length == 4)
                    GL.Uniform4(Uniforms[dest].address, new Vector4(arr[0], arr[1], arr[2], arr[3]));
                else
                    GL.Uniform1(Uniforms[dest].address, arr.Length, arr);
            }
            else if (var is float || var is int)
                GL.Uniform1(Uniforms[dest].address, var);
            else if (var is Vector2)
                GL.Uniform2(Uniforms[dest].address, var);
            else if (var is Vector3)
                GL.Uniform3(Uniforms[dest].address, var);
            else if (var is Vector4)
                GL.Uniform4(Uniforms[dest].address, var);
            else if (var is Matrix4)
            {
                Matrix4 mat = (Matrix4)var;
                GL.UniformMatrix4(Uniforms[dest].address, false, ref mat);
            }
            else
                throw new Exception();
            GetGLError("SetUniform");
        }

        internal void Compile()
        {
            int pID, vsID, fsID;

            if (CreateProgram(out pID, out vsID, out fsID))
            {
                Useable = true;
                Delete();
                ProgramID = pID;
                VShaderID = vsID;
                FShaderID = fsID;
                Link();
            }
        }

        private void Delete()
        {
            if (ProgramID == -1) return;
            GL.DetachShader(ProgramID, VShaderID);
            GL.DetachShader(ProgramID, FShaderID);
            GL.DeleteProgram(ProgramID);
            GL.DeleteShader(VShaderID);
            GL.DeleteShader(FShaderID);
        }

        private int LoadShader(string code, ShaderType type)
        {
            int shaderID = GL.CreateShader(type);

            if (code == string.Empty)
            {
                Log.WriteLine("Failed to compile shader:");
                Log.WriteLine(GL.GetShaderInfoLog(shaderID));
                GL.DeleteShader(shaderID);
                return 0;
            }

            GL.ShaderSource(shaderID, code);
            GL.CompileShader(shaderID);

            int shader_ok;
            GL.GetShader(shaderID, ShaderParameter.CompileStatus, out shader_ok);

            if (shader_ok == 0)
            {
                Log.WriteLine("Failed to compile shader:");
                Log.WriteLine(GL.GetShaderInfoLog(shaderID));
                GL.DeleteShader(shaderID);
                return 0;
            }

            return shaderID;
        }

        private bool CreateProgram(out int pID, out int vsID, out int fsID)
        {
            pID = 0;
            vsID = 0;
            fsID = 0;

            int programID = 0, vShaderID = 0, fShaderID = 0;

            if ((vShaderID = LoadShader(VertShader, ShaderType.VertexShader)) == 0) return false;
            if ((fShaderID = LoadShader(FragShader, ShaderType.FragmentShader)) == 0) return false;

            programID = GL.CreateProgram();
            GL.AttachShader(programID, vShaderID);
            GL.AttachShader(programID, fShaderID);
            GL.LinkProgram(programID);

            int program_ok;
            GL.GetProgram(programID, GetProgramParameterName.LinkStatus, out program_ok);
            if (program_ok == 0)
            {
                Log.WriteLine("Failed to link shader program:");
                Log.WriteLine(GL.GetProgramInfoLog(programID));
                GL.DeleteProgram(programID);
                return false;
            }

            pID = programID;
            vsID = vShaderID;
            fsID = fShaderID;

            return true;
        }

        private void Link()
        {
            if (!Useable) return;
            Attributes.Clear();
            Uniforms.Clear();
            Buffers.Clear();

            GL.GetProgram(ProgramID, GetProgramParameterName.ActiveAttributes, out AttributeCount);
            GL.GetProgram(ProgramID, GetProgramParameterName.ActiveUniforms, out UniformCount);

            for (int i = 0; i < AttributeCount; i++)
            {
                AttributeInfo info = new AttributeInfo();
                int length = 0;

                StringBuilder name = new StringBuilder(32);

                GL.GetActiveAttrib(ProgramID, i, 256, out length, out info.size, out info.type, name);

                info.name = name.ToString();
                info.address = GL.GetAttribLocation(ProgramID, info.name);
                Attributes.Add(name.ToString(), info);

                uint buffer = 0;
                GL.GenBuffers(1, out buffer);
                Buffers.Add(Attributes.Values.ElementAt(i).name, buffer);
            }

            for (int i = 0; i < UniformCount; i++)
            {
                UniformInfo info = new UniformInfo();
                int length = 0;

                StringBuilder name = new StringBuilder(128);

                GL.GetActiveUniform(ProgramID, i, 256, out length, out info.size, out info.type, name);

                info.name = name.ToString();
                info.address = GL.GetUniformLocation(ProgramID, info.name);
                Uniforms.Add(name.ToString(), info);

                //    uint buffer = 0;
                //    GL.GenBuffers(1, out buffer);
                //    Buffers.Add(Uniforms.Values.ElementAt(i).name, buffer);
            }

            GetGLError("Link");
        }

        public void Use()
        {
            if (!Useable) return;
            if (ProgramID == -1)
                Compile();
            if (ProgramID != -1)
                GL.UseProgram(ProgramID);
            foreach (KeyValuePair<string, dynamic> var in Locals)
                SetUniform(var.Key, var.Value);
        }

        public void EnableVertexAttribArrays()
        {
            if (!Useable) return;
            for (int i = 0; i < Attributes.Count; i++)
            {
                AttributeInfo info = Attributes.Values.ElementAt(i);
                GL.EnableVertexAttribArray(info.address);
            }
        }

        public void DisableVertexAttribArrays()
        {
            if (!Useable) return;
            for (int i = 0; i < Attributes.Count; i++)
            {
                AttributeInfo info = Attributes.Values.ElementAt(i);
                GL.DisableVertexAttribArray(info.address);
            }
        }

        public int GetAttribute(string name)
        {
            return Attributes.ContainsKey(name) ? Attributes[name].address : -1;
        }

        public int GetUniform(string name)
        {
            return Uniforms.ContainsKey(name) ?
                Uniforms[name].address :
                -1;
        }

        public uint GetBuffer(string name)
        {
            return Buffers.ContainsKey(name) ? Buffers[name] : 0;
        }
    }

    internal static class ProgramLoader
    {
        private static bool DefaultProgramLoaded = false;
        private static Interpreter interpreter;
        internal static ShaderProgram Program;

        private static Dictionary<string, string> ProcessorDefines;

        public static ShaderProgram LoadProgram(string name)
        {
            string filename = @"shaders\gl_prog\" + name + ".prog";
            Program = new ShaderProgram(filename);
            ReloadProgram(Program);
            Manifest.LoadedPrograms[name] = Program;
            return Program;
        }

        internal static void ReloadProgram(ShaderProgram prog)
        {
            ProcessorDefines = new Dictionary<string, string>();

            string[] filenames = GetDataPath(prog.filename);
            foreach (string filename in filenames)
            {
                Node code = Compiler.CompileSource(
                    new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read));

                interpreter = new Interpreter(HandleIf);
                interpreter.RegisterFunction("debug", DebugReload);
                interpreter.RegisterFunction("add", AddTextScript);
                interpreter.RegisterFunction("compile", LoaderCompile);
                interpreter.RegisterFunction("input", Input);
                interpreter.RegisterFunction("matrix", MapUniform);
                interpreter.RegisterFunction("local", AddLocal);
                interpreter.RegisterFunction("global", MapUniform);
                interpreter.RegisterFunction("#import", Import);
                interpreter.RegisterFunction("#def", ProcessorDefine);
                interpreter.RegisterFunction("#undef", ProcessorUndefine);
                interpreter.RegisterFunction("debugReload", DebugReload);

                interpreter.Run(code);

                Program.Compile();
            }
        }


        private static int ParseTextSource(Node args, out string source)
        {
            if (args[1] is FuncNode)
            {
                // handle special "#undef" case
                if (((FuncNode)args[1]).funcname == "#undef")
                {
                    source = "#undef " + ((TextNode)(((FuncNode)args[1]).arguments[0])).str;
                    return 2;
                }
                else
                {
                    throw new Exception();
                }
            }
            else
            {
                source = "";
                int i = 1;
                for (; !(args[i].Next is EndOfLineNode); i++)
                    source += ((TextNode)args[i]).str + " ";
                source += ((TextNode)args[i]).str;
                return i + 1;
            }
        }

        private static int AddTextScript(int nargs, Node args)
        {
            string dest = ((IdentNode)args[0]).identifier;

            string source;
            int ret = ParseTextSource(args, out source);

            if (dest == "vert_text")
            {
                Program.VertShader += source + "\r\n";
                return ret;
            }
            else if (dest == "geo_text")
            {
                Program.GeomShader += source + "\r\n";
                return ret;
            }
            else if (dest == "frag_text" || dest == "frag_text_")
            {
                Program.FragShader += source + "\r\n";
                return ret;
            }
            else if (dest == "vert_script")
            {
                string[] scriptfiles = GetDataPath("shaders\\gl_prog\\" + source);
                foreach (string scriptfile in scriptfiles)
                {
                    if (scriptfile == null)
                        continue;

                    StreamReader sr = new StreamReader(
                        new FileStream(scriptfile, FileMode.Open, FileAccess.Read, FileShare.Read));
                    Program.VertShader += sr.ReadToEnd();
                    sr.Close();
                }
                return 2;
            }
            else if (dest == "geom_script")
            {
                string[] scriptfiles = GetDataPath("shaders\\gl_prog\\" + source);
                foreach (string scriptfile in scriptfiles)
                {
                    if (scriptfile == null)
                        continue;

                    StreamReader sr = new StreamReader(
                        new FileStream(scriptfile, FileMode.Open, FileAccess.Read, FileShare.Read));
                    Program.GeomShader += sr.ReadToEnd();
                    sr.Close();
                }
                return 2;
            }
            else if (dest == "frag_script")
            {
                string[] scriptfiles = GetDataPath("shaders\\gl_prog\\" + source);
                foreach (string scriptfile in scriptfiles)
                {
                    if (scriptfile == null)
                        continue;

                    StreamReader sr = new StreamReader(
                        new FileStream(scriptfile, FileMode.Open, FileAccess.Read, FileShare.Read));
                    Program.FragShader += sr.ReadToEnd();
                    sr.Close();
                }
                return 2;
            }
            else
                throw new Exception("Illegal destination.");

        }

        private static int LoaderCompile(int nargs, Node args)
        {
            // ignore loader compile command
            return 0;
        }

        private static int Input(int nargs, Node args)
        {
            int i = 0;
            for (Node n = args; !(n is EndOfLineNode); n = n.Next, i++)
            {
                // Inputs.Add();
            }
            return i;
        }

        private static int MapUniform(int nargs, Node args)
        {
            string globalsrc = ((IdentNode)args[0]).identifier;
            string uniformdest = ((IdentNode)args[1]).identifier;
            Program.UniformMap.Add(globalsrc, uniformdest);
            return 2;
        }

        private static int AddLocal(int nargs, Node args)
        {
            string typename = ((IdentNode)args[0]).identifier;
            string varname = ((IdentNode)args[1]).identifier;
            if (typename == "tex")
            {
                Program.Locals[varname] = int.Parse(((TextNode)args[2]).str);
                return 3;
            }
            else if (typename == "float")
            {
                float value = 0.0f;
                value = float.Parse(((TextNode)args[2]).str, CultureInfo.InvariantCulture);
                Program.Locals[varname] = value;
                return 3;
            }
            else if (typename.StartsWith("float"))
            {
                int length = int.Parse(typename.Substring(5));
                float[] array = new float[length];
                int i = 2;
                while (!(args[i] is EndOfLineNode))
                {
                    array[i - 2] = float.Parse(((TextNode)args[i]).str, CultureInfo.InvariantCulture);
                    i++;
                }
                Program.Locals[varname] = array;
                return i;
            }
            else if (typename.StartsWith("vec"))
            {
                int length = int.Parse(typename.Substring(3));
                float[] array = new float[length];
                int i = 2;
                while (!(args[i] is EndOfLineNode))
                {
                    array[i - 2] = float.Parse(((TextNode)args[i]).str, CultureInfo.InvariantCulture);
                    i++;
                }
                if (length == 2) Program.Locals[varname] = new Vector2(array[0], array[1]);
                else if (length == 3) Program.Locals[varname] = new Vector3(array[0], array[1], array[2]);
                else if (length == 4) Program.Locals[varname] = new Vector4(array[0], array[1], array[2], array[3]);
                else throw new Exception();
                return i;
            }
            else
            {
                throw new Exception("Unrecognized type: " + typename);
            }
        }

        private static int Import(int nargs, Node args)
        {
            if (nargs != 1) throw new Exception("`#import' expected 1 argument, found " + nargs);
            string impname = ((TextNode)args[0]).str;

            string[] paths = GetDataPath(@"shaders\gl_prog\" + impname);

            foreach (string path in paths)
            {
                if (path == null)
                    continue;

                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                Node code = Compiler.CompileSource(fs);
                interpreter.Run(code);
                break;
            }

            return 0; // throw new Exception("Unable to locate file: shaders\\dev_config.manifest");
        }

        private static int ProcessorDefine(int nargs, Node args)
        {
            string arg0 = ((TextNode)args[0]).str;
            string arg1 = nargs > 1 ? ((TextNode)args[1]).str : "";
            ProcessorDefines[arg0] = arg1;
            return 0;
        }

        private static int ProcessorUndefine(int nargs, Node args)
        {
            string arg0 = ((TextNode)args[0]).str;
            ProcessorDefines.Remove(arg0);
            return 0;
        }

        private static int DebugReload(int nargs, Node args)
        {
            //ignore
            return 1;
        }
        
        private static bool HandleIf(BinOpr op, string left, string right)
        {
            switch (op)
            {
                case BinOpr.OP_NE:
                    break;
                case BinOpr.OP_EQ:
                    break;
                case BinOpr.OP_LT:
                    break;
                case BinOpr.OP_LE:
                    break;
                case BinOpr.OP_GT:
                    break;
                case BinOpr.OP_GE:
                    if (ManifestConfig.Options.ContainsKey(left))
                    {
                        int num;
                        if (int.TryParse(right, out num))
                        {
                            return ManifestConfig.Options[left].Value >= num;
                        }
                        else
                        {
                            throw new Exception();
                        }
                    }
                    else
                    {
                        return false;
                    }
                case BinOpr.OP_STR:
                    break;
                case BinOpr.OP_DEF:
                    return ManifestConfig.Options.ContainsKey(left) || ProcessorDefines.ContainsKey(left);
            }
            throw new Exception();
        }

        internal static void LoadDefaultProgram()
        {
            if (!DefaultProgramLoaded)
            {
                ShaderProgram.DefaultProgram = new ShaderProgram("daenerys_default");
                ShaderProgram.DefaultProgram.Locals.Add("inTexDiff", 0);
                ShaderProgram.DefaultProgram.Locals.Add("inTexTeam", 2);
                ShaderProgram.DefaultProgram.UniformMap.Add("modelview", "inMatM");
                ShaderProgram.DefaultProgram.UniformMap.Add("camera", "inMatV");
                ShaderProgram.DefaultProgram.UniformMap.Add("projection", "inMatP");
            }
            string vPath = Path.Combine(DAEnerys.Program.EXECUTABLE_PATH, @"shaders\default.vert");
            string fPath = Path.Combine(DAEnerys.Program.EXECUTABLE_PATH, @"shaders\default.frag");
            StreamReader vReader = new StreamReader(
                new FileStream(vPath, FileMode.Open, FileAccess.Read, FileShare.Read));
            StreamReader fReader = new StreamReader(
                new FileStream(fPath, FileMode.Open, FileAccess.Read, FileShare.Read));
            ShaderProgram.DefaultProgram.VertShader = vReader.ReadToEnd();
            ShaderProgram.DefaultProgram.FragShader = fReader.ReadToEnd();
            vReader.Dispose();
            fReader.Dispose();

            ShaderProgram.DefaultProgram.Compile();
            Manifest.AvailablePrograms.Add("daenerys_default");
            Manifest.LoadedPrograms["daenerys_default"] = ShaderProgram.DefaultProgram;
            DefaultProgramLoaded = true;
        }
    }
}