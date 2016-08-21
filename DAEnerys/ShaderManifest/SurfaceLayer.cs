using System;
using System.Collections.Generic;
using System.Globalization;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using static NewShaderManifest.Utils;

namespace NewShaderManifest
{
    internal class SurfaceLayer
    {
        internal List<KeyValuePair<string, object>> GLOps = new List<KeyValuePair<string, object>>();

        internal string ProgramName { get; set; }
        internal ShaderProgram Program { get; set; }

        internal Dictionary<string, string> Alias = new Dictionary<string, string>();

        internal Variables Locals = new Variables();

        internal void AddLocal(List<string> args)
        {
            if (args.Count < 3) throw new ArgumentException();
            string typename = args.PopFirst();
            string varname = args.PopFirst();

            if (typename == "tex")
            {
                Locals[varname] = args[0];
            }
            else if (typename == "float")
            {
                float value = 0.0f;
                value = float.Parse(args[2], CultureInfo.InvariantCulture);
                Locals[varname] = value;
            }
            else if (typename.StartsWith("float"))
            {
                int length = int.Parse(typename.Substring(5));
                float[] array = new float[length];
                for (int i = 0; i < args.Count; i++)
                    array[i] = float.Parse(args[i], CultureInfo.InvariantCulture);
                Locals[varname] = array;
            }
            else if (typename.StartsWith("vec"))
            {
                int length = int.Parse(typename.Substring(3));
                float[] array = new float[length];
                for (int i = 0; i < args.Count; i++)
                    array[i] = float.Parse(args[i], CultureInfo.InvariantCulture);
                if (length == 2) Locals[varname] = new Vector2(array[0], array[1]);
                else if (length == 3) Locals[varname] = new Vector3(array[0], array[1], array[2]);
                else if (length == 4) Locals[varname] = new Vector4(array[0], array[1], array[2], array[3]);
                else throw new SurfaceParseException("Invalid number of parameters for " + typename);
            }
            else
            {
                throw new SurfaceParseException("Unrecognized type: " + typename);
            }
        }

        internal void AddGLOp(string func, object args)
        {
            GLOps.Add(new KeyValuePair<string, object>(func, args));
        }

        internal void Draw(BeginMode mode, int count, DrawElementsType type, int indices)
        {
            //apply operations
            foreach (KeyValuePair<string, object> op in GLOps)
            {
                string key = op.Key;
                object value = op.Value;

                if (key == "BlendFunc")
                {
                    int[] values = (int[])value;
                    GL.BlendFunc((BlendingFactorSrc)values[0], (BlendingFactorDest)values[1]);
                }
                else if (key == "BlendFuncSeparate")
                {
                    int[] values = (int[])value;
                    BlendingFactorSrc srcRGB = (BlendingFactorSrc)values[0];
                    BlendingFactorDest dstRGB = (BlendingFactorDest)values[1];
                    BlendingFactorSrc srcAlpha = (BlendingFactorSrc)values[2];
                    BlendingFactorDest dstAlpha = (BlendingFactorDest)values[3];
                    GL.BlendFuncSeparate(srcRGB, dstRGB, srcAlpha, dstAlpha);
                }
                else if (key == "Enable")
                {
                    GL.Enable((EnableCap)value);
                }
                else if (key == "Disable")
                {
                    GL.Disable((EnableCap)value);
                }
                else if (key == "DepthMask")
                {
                    GL.DepthMask((bool)value);
                }
                else if (key == "DepthFunc")
                {
                    GL.DepthFunc((DepthFunction)value);
                }
                else if (key == "CullFace")
                {
                    GL.CullFace((CullFaceMode)value);
                }
                else if (key == "PolygonMode")
                {
                    int[] values = (int[])value;
                    GL.PolygonMode((MaterialFace)values[0], (PolygonMode)values[1]);
                }
                else if (key == "ColorMask")
                {
                    bool[] values = (bool[])value;
                    GL.ColorMask(values[0], values[1], values[2], values[3]);
                }
                else if (key == "PolygonOffset")
                {
                    float[] values = (float[])value;
                    GL.PolygonOffset(values[0], values[1]);
                }
                else
                {
                    throw new Exception();
                }
            } //apply operations
            
            GL.DrawElements(mode, count, type, indices);
            GetGLError("SurfaceLayer Draw");
        }

        internal void Use()
        {
            if (Program == null) return;
            Program.Use();
        }

        internal dynamic this[string key]
        {
            set
            {
                string dest = key;
                if (Alias.ContainsKey(key)) dest = Alias[key];
                Program.SetUniform(dest, value);
            }
        }
    }
}