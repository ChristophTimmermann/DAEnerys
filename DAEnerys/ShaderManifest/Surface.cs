using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using static NewShaderManifest.Utils;

namespace NewShaderManifest
{

    public class Surface
    {
        internal Dictionary<string, object> Protect = new Dictionary<string, object>();
        internal Variables Locals = new Variables();
        internal Variables Textures = new Variables();
        internal List<SurfaceLayer> Layers = new List<SurfaceLayer>();

        internal Variables AssignedLocals = new Variables();
        internal Variables AssignedTextures = new Variables();

        public string Sort { get; internal set; } = "";
        public bool NoTextures { get; internal set; } = false;
        public bool DeathScissor { get; internal set; } = false;

        internal bool Useable = false;

        public void Use()
        {
            if (!Useable) return;
            AssignedLocals = new Variables(Locals);
            AssignedTextures = new Variables(Textures);
            //foreach (SurfaceLayer layer in Layers)
            //    if (layer.Program != null)
            //        layer.Program.Compile();
        }

        public void LinkAttrib(int buffer, string attrname, int size, bool normalized)
        {
            foreach (SurfaceLayer layer in Layers)
            {
                if (layer.Program == null) continue;
                int attr = layer.Program.GetAttribute(attrname);
                if (attr == -1) continue;
                GL.BindBuffer(BufferTarget.ArrayBuffer, buffer);
                GL.VertexAttribPointer(attr, size, VertexAttribPointerType.Float, normalized, 0, 0);
                GL.EnableVertexAttribArray(attr);
                GetGLError("Surface LinkAttrib");
            }
        }

        private static void AttachTexture(int unit, int id)
        {
            TextureUnit tunit = TextureUnit.Texture0 + unit;
            GL.ActiveTexture(tunit);
            GL.BindTexture(TextureTarget.Texture2D, id);
        }

        public void AssignTexture(string name, string path, int id)
        {
            //if (!Textures.IsSet(name)) throw new ArgumentException();
            AssignedTextures[name] = path;
            dynamic var = this[name];
            if (var == null) return;
            AttachTexture(var, id);
        }

        //private void AssignTextures()
        //{
        //    if (NoTextures) return;
        //    foreach (string texname in AssignedTextures.VarNames.ToList())
        //    {
        //        string texval = AssignedTextures.Get(texname);
        //        if (texval == "default_black" ||
        //            texval == "default_white" ||
        //            texval == "default_normal")
        //            AssignTexture(texname, texval, Manifest.DefaultTexures[texval]);
        //    }
        //}

        public void Draw(BeginMode mode, int count, DrawElementsType type, int indices)
        {
            BeginProtect();

            // sort

            // AssignTextures();

            // draw layers
            foreach (SurfaceLayer layer in Layers)
            {
                layer.Use();
                // bind surface-specific vars
                foreach (KeyValuePair<string, dynamic> var in Manifest.Globals)
                    layer[var.Key] = var.Value;
                foreach (KeyValuePair<string, dynamic> var in AssignedLocals)
                    layer[var.Key] = var.Value;
                
                layer.Draw(mode, count, type, indices);
            }

            EndProtect();
        }

        private void BeginProtect()
        {
            foreach (string key in Protect.Keys.ToList())
            {
                if (key == "blendOps")
                {
                    Protect[key] = new int[] {
                        GL.GetInteger(GetPName.BlendSrcRgb),
                        GL.GetInteger(GetPName.BlendDstRgb),
                        GL.GetInteger(GetPName.BlendSrcAlpha),
                        GL.GetInteger(GetPName.BlendDstAlpha)
                    };
                }
                else if (key == "depthWrite")
                {
                    Protect[key] = GL.GetBoolean(GetPName.DepthWritemask);
                }
                else if (key == "depthFunc")
                {
                    Protect[key] = GL.GetInteger(GetPName.DepthFunc);
                }
                else if (key == "cull")
                {
                    Protect[key] = GL.GetInteger(GetPName.CullFaceMode);
                }
                else if (key == "fill")
                {
                    Protect[key] = GL.GetInteger(GetPName.PolygonMode);
                }
                else if (key == "clipPlane0")
                {
                    Protect[key] = GL.IsEnabled(EnableCap.ClipPlane0);
                }
                else if (key == "colorWrite")
                {
                    bool[] data = new bool[4];
                    GL.GetBoolean(GetPName.ColorWritemask, data);
                    Protect[key] = data;
                }
                else if (key == "ofsPolys")
                {
                    Protect[key] = GL.IsEnabled(EnableCap.PolygonOffsetFill);
                }
                else if (key == "ofsDepths")
                {
                    float factor, units;
                    GL.GetFloat(GetPName.PolygonOffsetFactor, out factor);
                    GL.GetFloat(GetPName.PolygonOffsetUnits, out units);
                    Protect[key] = new float[] { factor, units };
                }
                else
                {
                    throw new SurfaceDrawException("Unrecognized protection command: " + key);
                }
            }
        }

        private void EndProtect()
        {
            foreach (string key in Protect.Keys.ToList())
            {
                if (key == "blendOps")
                {
                    int[] values = (int[])Protect[key];
                    GL.BlendFuncSeparate((BlendingFactorSrc)values[0], (BlendingFactorDest)values[1], (BlendingFactorSrc)values[2], (BlendingFactorDest)values[3]);
                }
                else if (key == "depthWrite")
                {
                    GL.DepthMask((bool)Protect[key]);
                }
                else if (key == "depthFunc")
                {
                    GL.DepthFunc((DepthFunction)Protect[key]);
                }
                else if (key == "cull")
                {
                    GL.CullFace((CullFaceMode)Protect[key]);
                }
                else if (key == "fill")
                {
                    GL.PolygonMode(MaterialFace.FrontAndBack, (PolygonMode)Protect[key]);
                }
                else if (key == "clipPlane0")
                {
                    if ((bool)Protect[key])
                        GL.Enable(EnableCap.ClipPlane0);
                    else
                        GL.Disable(EnableCap.ClipPlane0);
                }
                else if (key == "colorWrite")
                {
                    bool[] values = (bool[])Protect[key];
                    GL.ColorMask(values[0], values[1], values[2], values[3]);
                }
                else if (key == "ofsPolys")
                {
                    if ((bool)Protect[key])
                        GL.Enable(EnableCap.PolygonOffsetFill);
                    else
                        GL.Disable(EnableCap.PolygonOffsetFill);
                }
                else if (key == "ofsDepths")
                {
                    float[] values = (float[])Protect[key];
                    GL.PolygonOffset(values[0], values[1]);
                }
                else
                {
                    throw new SurfaceDrawException("Unrecognized protection command: " + key);
                }
            }
        }

        public dynamic this[string name]
        {
            get
            {
                if (AssignedLocals.ContainsKey(name))
                    return AssignedLocals[name];
                foreach (SurfaceLayer layer in Layers)
                {
                    string alias = name;
                    if (layer.Alias.ContainsKey(alias)) alias = layer.Alias[alias];
                    if (layer.Locals.ContainsKey(alias)) return layer.Locals[alias];
                    if (layer.Program != null)
                    {
                        if (layer.Program.UniformMap.ContainsKey(alias)) alias = layer.Program.UniformMap[alias];
                        if (layer.Program.Locals.ContainsKey(alias)) return layer.Program.Locals[alias];
                    }
                }
                return null;
            }
            set
            {
                AssignedLocals[name] = value;
            }
        }
    }

    internal static class SurfaceLoader
    {
        private enum Keyword
        {
            Protect,
            Local,
            Layer,
            Sort,
            NoTextures,
            DeathScissor,
            NonKeyword
        }

        private static Surface Surface;

        internal static void LoadSurface(string name)
        {
            Surface = new Surface();
            string filename = @"shaders\gl_surf\" + name + ".surf";
            string path = GetDataPath(filename);
            if (string.IsNullOrEmpty(path))
                Console.WriteLine(@"Unable to locate surface: " + filename);
            else
            {
                Parse(new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read)));
                Surface.Useable = true;
            }
            Manifest.LoadedSurfaces[name] = Surface;
        }

        private static Keyword IsKeyword(string str)
        {
            if (str == "protect") return Keyword.Protect;
            if (str == "local") return Keyword.Local;
            if (str == "layer") return Keyword.Layer;
            if (str == "sort") return Keyword.Sort;
            if (str == "notextures") return Keyword.NoTextures;
            if (str == "deathscissor") return Keyword.DeathScissor;
            return Keyword.NonKeyword;
        }

        private static SurfaceLayer CurrentLayer; // helper for parser

        private static void Parse(StreamReader sr)
        {
            Keyword mode = Keyword.NonKeyword;
            string line;
            while (!sr.EndOfStream && GetLine(sr, out line))
            {
                if (line == "") continue;
                List<string> args = new List<string>(line.Split(new char[] { ' ' }));
                string str = args[0];
                Keyword word = IsKeyword(str);
                if (word != Keyword.NonKeyword) args.PopFirst();
                switch (word)
                {
                    case Keyword.Protect:
                        mode = Keyword.Protect;
                        break;
                    case Keyword.Local:
                        if (mode != Keyword.Layer)
                            AddLocal(args);
                        else
                            CurrentLayer.AddLocal(args);
                        break;
                    case Keyword.Layer:
                        mode = Keyword.Layer;
                        CurrentLayer = new SurfaceLayer();
                        Surface.Layers.Add(CurrentLayer);
                        break;
                    case Keyword.Sort:
                        Surface.Sort = args[0];
                        break;
                    case Keyword.NoTextures:
                        Surface.NoTextures = true;
                        break;
                    case Keyword.DeathScissor:
                        Surface.DeathScissor = true;
                        break;
                    case Keyword.NonKeyword:
                        if (mode == Keyword.Protect) Surface.Protect.Add(str, 0);
                        if (mode == Keyword.Layer) Layer(args);
                        break;
                }
            }
        }

        private static void AddLocal(List<string> args)
        {
            if (args.Count < 3) throw new ArgumentException();
            string typename = args.PopFirst();
            string varname = args.PopFirst();

            if (typename == "tex")
            {
                Surface.Textures[varname] = args[0];
            }
            else if (typename == "float")
            {
                float value = 0.0f;
                value = float.Parse(args[0], CultureInfo.InvariantCulture);
                Surface.Locals[varname] = value;
            }
            else if (typename.StartsWith("float"))
            {
                int length = int.Parse(typename.Substring(5));
                float[] array = new float[length];
                for (int i = 0; i < args.Count; i++)
                    array[i] = float.Parse(args[i], CultureInfo.InvariantCulture);
                Surface.Locals[varname] = array;
            }
            else if (typename.StartsWith("vec"))
            {
                int length = int.Parse(typename.Substring(3));
                float[] array = new float[length];
                for (int i = 0; i < args.Count; i++)
                    array[i] = float.Parse(args[i], CultureInfo.InvariantCulture);
                if (length == 2) Surface.Locals[varname] = new Vector2(array[0], array[1]);
                else if (length == 3) Surface.Locals[varname] = new Vector3(array[0], array[1], array[2]);
                else if (length == 4) Surface.Locals[varname] = new Vector4(array[0], array[1], array[2], array[3]);
                else throw new SurfaceParseException("Invalid number of parameters for " + typename);
            }
            else
            {
                throw new SurfaceParseException("Unrecognized type: " + typename);
            }
        }

        private static int getBlendingFactor(string str)
        {
            if (str == "zero") return 0;
            if (str == "one") return 1;
            if (str == "srcAlpha") return 770;
            if (str == "srcInvAlpha") return 771;
            if (str == "invSrcAlpha") return 771;
            throw new SurfaceParseException("Unrecognized BlendingFactor: " + str);
        }

        private static EnableCap getEnableCap(string str)
        {
            if (str == "DepthTest") return EnableCap.DepthTest;
            throw new SurfaceParseException("Unrecognized EnableCap: " + str);
        }

        private static DepthFunction getDepthFunc(string str)
        {
            if (str == "always") return DepthFunction.Always;
            if (str == "less") return DepthFunction.Less;
            if (str == "lequal") return DepthFunction.Lequal;
            if (str == "gequal") return DepthFunction.Gequal;
            throw new SurfaceParseException("Unrecognized DepthFunction: " + str);
        }

        private static void Layer(List<string> args)
        {
            string str = args.PopFirst();
            if (str == "program")
            {
                CurrentLayer.ProgramName = args.PopFirst();
                if (!Manifest.AvailablePrograms.Contains(CurrentLayer.ProgramName))
                    Log.WriteLine("Program " + CurrentLayer.ProgramName + " is not available.");
                else
                    CurrentLayer.Program = Manifest.GetProgram(CurrentLayer.ProgramName);
            }
            else if (str == "blendOps")
            {
                if (args.Count == 2)
                {
                    CurrentLayer.AddGLOp("BlendFunc", new int[] {
                        getBlendingFactor(args[0]),
                        getBlendingFactor(args[1])
                    });
                }
                else if (args.Count == 4)
                {
                    CurrentLayer.AddGLOp("BlendFuncSeparate", new int[] {
                        getBlendingFactor(args[0]),
                        getBlendingFactor(args[1]),
                        getBlendingFactor(args[2]),
                        getBlendingFactor(args[3])
                    });
                }
            }
            else if (str == "depthWrite")
            {
                string val = args[0];
                if (val == "0" || val == "false")
                {
                    CurrentLayer.AddGLOp("Disable", EnableCap.DepthTest);
                }
                else if (val == "1" || val == "true")
                {
                    CurrentLayer.AddGLOp("Enable", EnableCap.DepthTest);
                }
                else
                {
                    throw new SurfaceParseException("Unrecognized value (" + args[0] + ") for " + str);
                }
            }
            else if (str == "depthFunc")
            {
                CurrentLayer.AddGLOp("DepthFunc", getDepthFunc(args[0]));
            }
            else if (str == "cull")
            {
                if (args[0] == "none")
                {
                    CurrentLayer.AddGLOp("Disable", EnableCap.CullFace);
                }
                else if (args[0] == "back")
                {
                    CurrentLayer.AddGLOp("Enable", EnableCap.CullFace);
                    CurrentLayer.AddGLOp("CullFace", CullFaceMode.Back);
                }
                else if (args[0] == "front")
                {
                    CurrentLayer.AddGLOp("Enable", EnableCap.CullFace);
                    CurrentLayer.AddGLOp("CullFace", CullFaceMode.Front);
                }
                else
                {
                    throw new SurfaceParseException("Unrecognized value (" + args[0] + ") for " + str);
                }
            }
            else if (str == "fill")
            {
                CurrentLayer.AddGLOp("PolygonMode", new int[] { (int)MaterialFace.FrontAndBack, (int)PolygonMode.Fill });
            }
            else if (str == "alias")
            {
                CurrentLayer.Alias.Add(args[0], args[1]);
            }
            else if (str == "clipPlane0")
            {
                // unknown what effect this has
                if (args[0] == "1")
                    CurrentLayer.AddGLOp("Enable", EnableCap.ClipPlane0);
                else if (args[0] == "0")
                    CurrentLayer.AddGLOp("Disable", EnableCap.ClipPlane0);
                else
                    throw new SurfaceParseException("Unrecognized value (" + args[0] + ") for " + str);
            }
            else if (str == "colorWrite")
            {
                CurrentLayer.AddGLOp("ColorMask", new bool[] { args[0] == "1", args[1] == "1", args[2] == "1", args[3] == "1" });
            }
            else if (str == "ofsPolys")
            {
                string val = args[0];
                if (val == "0")
                {
                    CurrentLayer.AddGLOp("Disable", EnableCap.PolygonOffsetFill);
                }
                else if (val == "1")
                {
                    CurrentLayer.AddGLOp("Enable", EnableCap.PolygonOffsetFill);
                }
                else
                {
                    throw new SurfaceParseException("Unrecognized value (" + args[0] + ") for " + str);
                }
            }
            else if (str == "ofsDepths")
            {
                float factor = 0f;
                float units = 0f;
                if (args.Count >= 1) factor = float.Parse(args[0], CultureInfo.InvariantCulture);
                if (args.Count >= 2) units = float.Parse(args[1], CultureInfo.InvariantCulture);
                CurrentLayer.AddGLOp("PolygonOffset", new float[] { factor, units });
            }
            else if (str == "pointSize")
            {
                float size = 0f;
                if (args.Count >= 1) size = float.Parse(args[0], CultureInfo.InvariantCulture);
                CurrentLayer.AddGLOp("PointSize", size);
            }
            else
            {
                throw new SurfaceParseException("Unrecognized layer operation: " + str);
            }
        }
    }

}