using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using System.IO;
using System.Text;
using System.Linq;

namespace DAEnerys
{
    class Shader
    {
        public int ProgramID = -1;
        public int VShaderID = -1;
        public int FShaderID = -1;
        public int AttributeCount = 0; 
        public int UniformCount = 0;

        public Dictionary<String, AttributeInfo> Attributes = new Dictionary<string, AttributeInfo>();
        public Dictionary<String, UniformInfo> Uniforms = new Dictionary<string, UniformInfo>();
        public Dictionary<String, uint> Buffers = new Dictionary<string, uint>();

        private String vshader;
        private String fshader;
        private bool fromFile = false;
        
        public Shader(String vshader, String fshader, bool fromFile = false)
        {
            this.vshader = vshader;
            this.fshader = fshader;
            this.fromFile = fromFile;

            Reload();
        }

        ~Shader()
        {
            //Delete();
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
            GL.DetachShader(ProgramID, VShaderID);
            GL.DetachShader(ProgramID, FShaderID);
            GL.DeleteProgram(ProgramID);
            GL.DeleteShader(VShaderID);
            GL.DeleteShader(FShaderID);
        }

        private int LoadShader(String code, ShaderType type)
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

        private int LoadShaderFromString(String code, ShaderType type)
        {
            int shaderID = 0;
            if (type == ShaderType.VertexShader || type == ShaderType.FragmentShader)
                shaderID = LoadShader(code, type);
            return shaderID;
        }

        private int LoadShaderFromFile(String filename, ShaderType type)
        {
            int shaderID = 0;
            StreamReader sr;
            if (File.exists("shaders\\" + filename))
                sr = new StreamReader(new FileStream("shaders\\" + filename, FileMode.Open));
            else 
                sr = new StreamReader(Program.Assembly.GetManifestResourceStream(Program.AssemblyName + @"shaders." + filename));
            using (sr)
            {
                if (type == ShaderType.VertexShader || type == ShaderType.FragmentShader)
                {
                    string file = sr.ReadToEnd();
                    shaderID = LoadShader(file, type);
                }
                sr.Close();
            }
            return shaderID;
        }

        private bool CreateProgram(out int pID, out int vsID, out int fsID)
        {
            pID = 0;
            vsID = 0;
            fsID = 0;

            int programID, vShaderID, fShaderID;

            if (fromFile)
            {
                vShaderID = LoadShaderFromFile(vshader, ShaderType.VertexShader);
                fShaderID = LoadShaderFromFile(fshader, ShaderType.FragmentShader);
            }
            else
            {
                vShaderID = LoadShaderFromString(vshader, ShaderType.VertexShader);
                fShaderID = LoadShaderFromString(fshader, ShaderType.FragmentShader);
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

        private void Link()
        {
            Attributes.Clear();
            Uniforms.Clear();
            //foreach (KeyValuePair<string, uint> buffer in Buffers)
            //{
            //    GL.DeleteBuffer(buffer.Value);
            //}
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
                if (Uniforms.ContainsKey(name.ToString()))
                    Uniforms[name.ToString()] = info;
                else
                    Uniforms.Add(name.ToString(), info);
            }

            for (int i = 0; i < Attributes.Count; i++)
            {
                uint buffer = 0;
                GL.GenBuffers(1, out buffer);

                Buffers.Add(Attributes.Values.ElementAt(i).name, buffer);
            }

            for (int i = 0; i < Uniforms.Count; i++)
            {
                uint buffer = 0;
                GL.GenBuffers(1, out buffer);

                Buffers.Add(Uniforms.Values.ElementAt(i).name, buffer);
            }
        }

        public void EnableVertexAttribArrays()
        {
            for (int i = 0; i < Attributes.Count; i++)
            {
                GL.EnableVertexAttribArray(Attributes.Values.ElementAt(i).address);
            }
        }
        public void DisableVertexAttribArrays()
        {
            for (int i = 0; i < Attributes.Count; i++)
            {
                GL.DisableVertexAttribArray(Attributes.Values.ElementAt(i).address);
            }
        }

        public int GetAttribute(string name)
        {
            if (Attributes.ContainsKey(name))
            {
                return Attributes[name].address;
            }
            else
            {
                return -1;
            }
        }

        public int GetUniform(string name)
        {
            if (Uniforms.ContainsKey(name))
            {
                return Uniforms[name].address;
            }
            else
            {
                return -1;
            }
        }

        public uint GetBuffer(string name)
        {
            if (Buffers.ContainsKey(name))
            {
                return Buffers[name];
            }
            else
            {
                return 0;
            }
        }
    }

    public class AttributeInfo
    {
        public String name = "";
        public int address = -1;
        public int size = 0;
        public ActiveAttribType type;
    }

    public class UniformInfo
    {
        public String name = "";
        public int address = -1;
        public int size = 0;
        public ActiveUniformType type;
    }
}
