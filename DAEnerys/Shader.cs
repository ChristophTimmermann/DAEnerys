using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace DAEnerys
{
    class Shader
    {
        public int ProgramID = -1;
        public int VShaderID = -1;
        public int FShaderID = -1;
        public int AttributeCount = 0;
        public int UniformCount = 0;

        public Dictionary<string, AttributeInfo> Attributes = new Dictionary<string, AttributeInfo>();
        public Dictionary<string, UniformInfo> Uniforms = new Dictionary<string, UniformInfo>();
        public Dictionary<string, uint> Buffers = new Dictionary<string, uint>();

        private string pshader;
        private string vshader;
        private string fshader;
        private bool fromFile = false;
        //private string programdir = "";
        //private string programpath = "";

        //public Shader(string sprog)
        //{
        //    if (File.Exists(sprog))
        //    {
        //        string curdir = Directory.GetCurrentDirectory();
        //        fromFile = true;
        //        programpath = sprog;
        //        program = new ShaderProgram.Program(programpath);
        //        programdir = Directory.GetCurrentDirectory();
        //        Directory.SetCurrentDirectory(curdir);
        //        Reload();
        //    }
        //}

        public Shader(string pshader, string vshader, string fshader, bool fromFile = false)
        {
            this.pshader = pshader;
            this.vshader = vshader;
            this.fshader = fshader;
            this.fromFile = fromFile;

            Reload();
        }

        public Shader(string vshader, string fshader, bool fromFile = false)
        {
            this.pshader = "";
            this.vshader = vshader;
            this.fshader = fshader;
            this.fromFile = fromFile;

            Reload();
        }

        public void Reload()
        {
            int pID, vsID, fsID;

            if (CreateProgram(out pID, out vsID, out fsID))
            {
                Delete();
                Apply(pID, vsID, fsID);
                Link();
            }
        }

        private void Apply(int pID, int vsID, int fsID)
        {
            ProgramID = pID;
            VShaderID = vsID;
            FShaderID = fsID;
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

        private int LoadShaderFromstring(string code, ShaderType type)
        {
            int shaderID = 0;
            if (type == ShaderType.VertexShader || type == ShaderType.FragmentShader)
                shaderID = LoadShader(code, type);
            return shaderID;
        }

        private int LoadShaderFromFile(string filename, ShaderType type)
        {
            StreamReader progsr;
            string progcode = "";
            if (File.Exists("shaders\\" + pshader))
            {
                progsr = new StreamReader(new FileStream("shaders\\" + pshader, FileMode.Open));
                progcode = progsr.ReadToEnd();
                progsr.Close();
            }

            int shaderID = 0;
            StreamReader sr;
            if (File.Exists("shaders\\" + filename))
                sr = new StreamReader(new FileStream("shaders\\" + filename, FileMode.Open));
            else
                sr = new StreamReader(Program.Assembly.GetManifestResourceStream(Program.AssemblyName + @"shaders." + filename));
            using (sr)
            {
                if (type == ShaderType.VertexShader || type == ShaderType.FragmentShader)
                {
                    string file = progcode + "\n" + sr.ReadToEnd();
                    shaderID = LoadShader(file, type);
                }
            }
            return shaderID;
        }

        private bool CreateProgram(out int pID, out int vsID, out int fsID)
        {
            pID = 0;
            vsID = 0;
            fsID = 0;

            int programID = 0, vShaderID = 0, fShaderID = 0;

            if (fromFile)
            {
                vShaderID = LoadShaderFromFile(vshader, ShaderType.VertexShader);
                fShaderID = LoadShaderFromFile(fshader, ShaderType.FragmentShader);
            }
            else
            {
                vShaderID = LoadShaderFromstring(vshader, ShaderType.VertexShader);
                fShaderID = LoadShaderFromstring(fshader, ShaderType.FragmentShader);
            }
            if (vShaderID == 0) return false;
            if (fShaderID == 0) return false;

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

        public void SetUniform(string name, System.Drawing.Color v)
        {
            GL.Uniform4(GetUniform(name), v);
        }

        public void SetUniform(string name, float v0, float v1)
        {
            GL.Uniform2(GetUniform(name), v0, v1);
        }

        public void SetUniform(string name, int v0, int v1)
        {
            GL.Uniform2(GetUniform(name), v0, v1);
        }

        public void SetUniform(string name, float v0, float v1, float v2)
        {
            GL.Uniform3(GetUniform(name), v0, v1, v2);
        }

        internal void LinkAttrib1(int buffer, string attrname, bool normalized)
        {
            int attr = GetAttribute(attrname);
            if (attr == -1)
                return;
            GL.BindBuffer(BufferTarget.ArrayBuffer, buffer);
            GL.VertexAttribPointer(attr, 1, VertexAttribPointerType.Float, normalized, 0, 0);
            GL.EnableVertexAttribArray(attr);
            GetError("Shader LinkAttrib");
        }

        internal void LinkAttrib2(int buffer, string attrname, bool normalized)
        {
            int attr = GetAttribute(attrname);
            if (attr == -1)
                return;
            GL.BindBuffer(BufferTarget.ArrayBuffer, buffer);
            GL.VertexAttribPointer(attr, 2, VertexAttribPointerType.Float, normalized, 0, 0);
            GL.EnableVertexAttribArray(attr);
            GetError("Shader LinkAttrib");
        }

        internal void LinkAttrib3(int buffer, string attrname, bool normalized)
        {
            int attr = GetAttribute(attrname);
            if (attr == -1)
                return;
            GL.BindBuffer(BufferTarget.ArrayBuffer, buffer);
            GetError("Shader LinkAttrib");
            GL.VertexAttribPointer(attr, 3, VertexAttribPointerType.Float, normalized, 0, 0);
            GetError("Shader LinkAttrib");
            GL.EnableVertexAttribArray(attr);
            GetError("Shader LinkAttrib");
        }

        internal void LinkAttrib4(int buffer, string attrname, bool normalized)
        {
            int attr = GetAttribute(attrname);
            if (attr == -1)
                return;
            GL.BindBuffer(BufferTarget.ArrayBuffer, buffer);
            GL.VertexAttribPointer(attr, 4, VertexAttribPointerType.Float, normalized, 0, 0);
            GL.EnableVertexAttribArray(attr);
            GetError("Shader LinkAttrib");
        }

        private void Link()
        {
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
            }

            for (int i = 0; i < Attributes.Count; i++)
            {
                uint buffer = 0;
                GL.GenBuffers(1, out buffer);

                Buffers.Add(Attributes.Values.ElementAt(i).name, buffer);
            }

            //for (int i = 0; i < Uniforms.Count; i++)
            //{
            //    uint buffer = 0;
            //    GL.GenBuffers(1, out buffer);

            //    Buffers.Add(Uniforms.Values.ElementAt(i).name, buffer);
            //}
            GetError("Link");
        }

        public void EnableVertexAttribArrays()
        {
            for (int i = 0; i < Attributes.Count; i++)
            {
                AttributeInfo info = Attributes.Values.ElementAt(i);
                GL.EnableVertexAttribArray(info.address);
            }
        }
        public void DisableVertexAttribArrays()
        {
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

        private static void GetError(string type)
        {
            ErrorCode code = GL.GetError();
            if (code != ErrorCode.NoError)
                Log.WriteLine(type + ": " + code);
        }
    }

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
}
