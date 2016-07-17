using OpenTK;
using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Linq;
using Assimp;
using OpenTK.Graphics;
using System.IO;

namespace DAEnerys
{
    static class Renderer
    {
        static string ShaderDirectory = Directory.GetCurrentDirectory() + "\\shaders\\hwrm\\";
        static Dictionary<string, Shader> shaders = new Dictionary<string, Shader>();
        static string CurrentShader;

        public static HWTexture DefaultTexture;

        public static Light AmbientLight = new Light(new Vector4(0), new Vector3(0.4f), 0, 0.05f);

        private static Color backgroundColor = Color.FromArgb(255, 10, 10, 10);
        public static Color BackgroundColor
        {
            get { return backgroundColor; }
            set
            {
                backgroundColor = value;
                if (Program.main.Loaded)
                {
                    GL.ClearColor(value);
                    Program.GLControl.Invalidate();
                }
            }
        }

        private static Color teamColor = Color.FromArgb(255, 92, 139, 170);
        public static Color TeamColor
        {
            get { return teamColor; }
            set { teamColor = value; }
        }

        private static Color stripeColor = Color.FromArgb(255, 204, 204, 204);
        public static Color StripeColor
        {
            get { return stripeColor; }
            set { stripeColor = value; }
        }

        public static bool DrawVisualizationsInFront = true;
        public static bool DisableLighting = false;

        private static bool enableVSync = true;
        public static bool EnableVSync
        {
            get
            {
                return enableVSync;
            }
            set
            {
                enableVSync = value;
                if (GraphicsContext.CurrentContext != null)
                {
                    if (value)
                        GraphicsContext.CurrentContext.SwapInterval = 1;
                    else
                        GraphicsContext.CurrentContext.SwapInterval = 0;
                }
            }
        }

        public static float ThrusterInterpolation = 1;

        static Dictionary<string, int> ibo_elements = new Dictionary<string, int>();

        public static Matrix4 View = Matrix4.Identity;

        private static void AddShader(string name, Shader shader)
        {
            GetError("AddShader");
            int ibo = 0;
            GL.GenBuffers(1, out ibo);
            ibo_elements[name] = ibo;
            shaders.Add(name, shader);
            GetError("AddShader");
        }

        public static void Init()
        {
            GL.ClearColor(BackgroundColor);

            GL.Enable(EnableCap.DepthTest);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            GL.Enable(EnableCap.CullFace);

            GL.AlphaFunc(AlphaFunction.Greater, 0.05f);

            GL.LineWidth(2);

            // Load shaders from file
            AddShader("editor", new Shader("editor.vs", "editor.fs", true));
            AddShader("ship", new Shader("ship.prog", "ship.vert", "ship.frag", true));
            AddShader("thruster", new Shader("thruster.prog", "ship.vert", "ship.frag", true));
            AddShader("badge", new Shader("badge.prog", "ship.vert", "ship.frag", true));
            AddShader("bay", new Shader("bay.prog", "ship.vert", "ship.frag", true));
            CurrentShader = "ship";

            //AmbientLight.Enabled = false;
            DefaultTexture = new HWTexture(Path.Combine(Program.EXECUTABLE_PATH, @"resources/missing.tga"));

            if (EnableVSync)
                GraphicsContext.CurrentContext.SwapInterval = 1;
            else
                GraphicsContext.CurrentContext.SwapInterval = 0;

            GL.UseProgram(shaders[CurrentShader].ProgramID);
            shaders[CurrentShader].EnableVertexAttribArrays();

            //new Light(new Vector4(3, 0, 0, 1), new Vector3(1, 0, 0), 0.01f);
        }

        public static void ReloadShaders()
        {
            shaders[CurrentShader].DisableVertexAttribArrays();
            foreach (Shader s in shaders.Values)
                s.Reload();
            GL.UseProgram(shaders[CurrentShader].ProgramID);
            shaders[CurrentShader].EnableVertexAttribArrays();
        }

        public static void UpdateMeshData()
        {
            // Assemble mesh data on a per-shader basis
            List<string> mesh_shaders = new List<string>();
            Dictionary<string, List<Vector3>> verts = new Dictionary<string, List<Vector3>>();
            Dictionary<string, List<int>> inds = new Dictionary<string, List<int>>();
            Dictionary<string, List<Vector2>> texcoords = new Dictionary<string, List<Vector2>>();
            Dictionary<string, List<Vector3>> normals = new Dictionary<string, List<Vector3>>();
            Dictionary<string, List<Vector3>> tangents = new Dictionary<string, List<Vector3>>();
            Dictionary<string, List<Vector3>> bitangents = new Dictionary<string, List<Vector3>>();

            // Assemble vertex and indice data for all volumes
            Dictionary<string, int> vertcount = new Dictionary<string, int>();

            //SORT SHIP MESHES
            List<HWMesh> hwMeshList = new List<HWMesh>();
            foreach (HWMesh mesh in HWScene.Meshes)
            {
                if (!mesh.Translucent)
                    hwMeshList.Add(mesh);
            }
            foreach (HWMesh mesh in HWScene.Meshes)
            {
                if (mesh.Translucent)
                    hwMeshList.Add(mesh);
            }
            HWScene.Meshes = hwMeshList;

            foreach (HWMesh mesh in HWScene.Meshes)
            {
                if (mesh.Visible)
                {
                    string shader = mesh.Material.Shader;
                    if (!mesh_shaders.Contains(shader)) mesh_shaders.Add(shader);
                    if (!verts.ContainsKey(shader)) verts.Add(shader, new List<Vector3>());
                    if (!normals.ContainsKey(shader)) normals.Add(shader, new List<Vector3>());
                    if (!tangents.ContainsKey(shader)) tangents.Add(shader, new List<Vector3>());
                    if (!bitangents.ContainsKey(shader)) bitangents.Add(shader, new List<Vector3>());
                    if (!texcoords.ContainsKey(shader)) texcoords.Add(shader, new List<Vector2>());
                    if (!inds.ContainsKey(shader)) inds.Add(shader, new List<int>());
                    if (!vertcount.ContainsKey(shader)) vertcount.Add(shader, 0);

                    verts[shader].AddRange(mesh.Vertices);
                    normals[shader].AddRange(mesh.Normals);
                    tangents[shader].AddRange(mesh.Tangents);
                    bitangents[shader].AddRange(mesh.BiTangents);
                    texcoords[shader].AddRange(mesh.TextureCoords);
                    inds[shader].AddRange(mesh.GetIndices(vertcount[shader]).ToList());

                    vertcount[shader] += mesh.VertexCount;
                }
            }

            GetError("OpenTK Buffering");
            foreach (string shader in mesh_shaders)
            {
                Vector3[] vertdata = verts[shader].ToArray();
                Vector3[] normdata = normals[shader].ToArray();
                Vector3[] tangentdata = tangents[shader].ToArray();
                Vector3[] bitangentdata = bitangents[shader].ToArray();
                Vector2[] texcoorddata = texcoords[shader].ToArray();
                int[] indicedata = inds[shader].ToArray();

                if (!shaders.ContainsKey(shader))
                    AddShader(shader, shaders["ship"]);

                GL.BindBuffer(BufferTarget.ArrayBuffer, shaders[shader].GetBuffer("inPos"));
                GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(vertdata.Length * Vector3.SizeInBytes), vertdata, BufferUsageHint.StaticDraw);
                GL.VertexAttribPointer(shaders[shader].GetAttribute("inPos"), 3, VertexAttribPointerType.Float, false, 0, 0);

                GL.BindBuffer(BufferTarget.ArrayBuffer, shaders[shader].GetBuffer("inNorm"));
                GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(normdata.Length * Vector3.SizeInBytes), normdata, BufferUsageHint.StaticDraw);
                GL.VertexAttribPointer(shaders[shader].GetAttribute("inNorm"), 3, VertexAttribPointerType.Float, true, 0, 0);

                GL.BindBuffer(BufferTarget.ArrayBuffer, shaders[shader].GetBuffer("inTan"));
                GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(tangentdata.Length * Vector3.SizeInBytes), tangentdata, BufferUsageHint.StaticDraw);
                GL.VertexAttribPointer(shaders[shader].GetAttribute("inTan"), 3, VertexAttribPointerType.Float, true, 0, 0);

                GL.BindBuffer(BufferTarget.ArrayBuffer, shaders[shader].GetBuffer("inBiNorm"));
                GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(bitangentdata.Length * Vector3.SizeInBytes), bitangentdata, BufferUsageHint.StaticDraw);
                GL.VertexAttribPointer(shaders[shader].GetAttribute("inBiNorm"), 3, VertexAttribPointerType.Float, true, 0, 0);

                GL.BindBuffer(BufferTarget.ArrayBuffer, shaders[shader].GetBuffer("inUV0"));
                GL.BufferData<Vector2>(BufferTarget.ArrayBuffer, (IntPtr)(texcoorddata.Length * Vector2.SizeInBytes), texcoorddata, BufferUsageHint.StaticDraw);
                GL.VertexAttribPointer(shaders[shader].GetAttribute("inUV0"), 2, VertexAttribPointerType.Float, true, 0, 0);

                if (SOB_BADGE(shader))
                {
                    //GL.BindBuffer(BufferTarget.ArrayBuffer, shaders[shader].GetBuffer("inUV1"));
                    //GL.BufferData<Vector2>(BufferTarget.ArrayBuffer, (IntPtr)(badgeUV.Length * Vector2.SizeInBytes), badgeUV, BufferUsageHint.StaticDraw);
                    //GL.VertexAttribPointer(shaders[shader].GetAttribute("inUV1"), 2, VertexAttribPointerType.Float, true, 0, 0);
                }

                // Buffer index data
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, ibo_elements[shader]);
                GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(indicedata.Length * sizeof(int)), indicedata, BufferUsageHint.StaticDraw);
                GetError("OpenTK Buffering");
            }

            //SORT EDITOR MESHES
            List<EditorMesh> newList = new List<EditorMesh>();
            foreach (EditorMesh mesh in EditorScene.meshes)
                if (mesh.NeverDrawInFront && !mesh.DrawAboveShip)
                    newList.Add(mesh);

            foreach (EditorMesh mesh in EditorScene.meshes)
                if (!mesh.NeverDrawInFront && mesh.DrawAboveShip)
                    newList.Add(mesh);

            foreach (EditorMesh mesh in EditorScene.meshes)
                if (!mesh.NeverDrawInFront && !mesh.DrawAboveShip)
                    newList.Add(mesh);

            EditorScene.meshes = newList;

            List<Vector3> editor_verts = new List<Vector3>();
            //List<Vector3> editor_normals = new List<Vector3>();
            List<Vector2> editor_texcoords = new List<Vector2>();
            List<Vector3> editor_colors = new List<Vector3>();
            List<int> editor_inds = new List<int>();
            int editor_vertcount = 0;

            foreach (EditorMesh mesh in EditorScene.meshes)
            {
                if (mesh.Visible)
                {
                    editor_verts.AddRange(mesh.Vertices);
                    //editor_normals.AddRange(mesh.Normals);
                    editor_texcoords.AddRange(mesh.TextureCoords);
                    editor_colors.AddRange(mesh.Colors);
                    editor_inds.AddRange(mesh.GetIndices(editor_vertcount).ToList());
                    editor_vertcount += mesh.VertexCount;
                }
            }

            {
                Vector3[] vertdata = editor_verts.ToArray();
                //Vector4[] normdata = new Vector4[editor_normals.Count];
                //for (int i = 0; i < editor_normals.Count; i++)
                //    normdata[i] = new Vector4(editor_normals[i], 0);
                Vector2[] texcoorddata = editor_texcoords.ToArray();
                Vector3[] colors = editor_colors.ToArray();
                int[] indicedata = editor_inds.ToArray();

                GetError("OpenTK Buffering");
                GL.BindBuffer(BufferTarget.ArrayBuffer, shaders["editor"].GetBuffer("inPos"));
                GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(vertdata.Length * Vector3.SizeInBytes), vertdata, BufferUsageHint.StaticDraw);
                GL.VertexAttribPointer(shaders["editor"].GetAttribute("inPos"), 3, VertexAttribPointerType.Float, false, 0, 0);

                //GL.BindBuffer(BufferTarget.ArrayBuffer, shaders["editor"].GetBuffer("inNorm"));
                //GL.BufferData<Vector4>(BufferTarget.ArrayBuffer, (IntPtr)(normdata.Length * Vector4.SizeInBytes), normdata, BufferUsageHint.StaticDraw);
                //GL.VertexAttribPointer(shaders["editor"].GetAttribute("inNorm"), 4, VertexAttribPointerType.Float, true, 0, 0);

                GetError("OpenTK Buffering");
                GL.BindBuffer(BufferTarget.ArrayBuffer, shaders["editor"].GetBuffer("inUV0"));
                GL.BufferData<Vector2>(BufferTarget.ArrayBuffer, (IntPtr)(texcoorddata.Length * Vector2.SizeInBytes), texcoorddata, BufferUsageHint.StaticDraw);
                GL.VertexAttribPointer(shaders["editor"].GetAttribute("inUV0"), 2, VertexAttribPointerType.Float, true, 0, 0);

                GetError("OpenTK Buffering");
                GL.BindBuffer(BufferTarget.ArrayBuffer, shaders["editor"].GetBuffer("inColor"));
                GL.BufferData<Vector2>(BufferTarget.ArrayBuffer, (IntPtr)(texcoorddata.Length * Vector2.SizeInBytes), texcoorddata, BufferUsageHint.StaticDraw);
                GL.VertexAttribPointer(shaders["editor"].GetAttribute("inColor"), 2, VertexAttribPointerType.Float, true, 0, 0);

                GetError("OpenTK Buffering");
                // Buffer index data
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, ibo_elements["editor"]);
                GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(indicedata.Length * sizeof(int)), indicedata, BufferUsageHint.StaticDraw);
                GetError("OpenTK Buffering");
            }

            GetError("OpenTK Buffering");
        }

        public static void UpdateView()
        {
            View = Program.Camera.GetViewMatrix();
            AmbientLight.Position = new Vector4(Program.Camera.Position, 0);
            float aspectRatio = (float)Program.GLControl.Width / (float)Program.GLControl.Height;
            float aspectRatioWidthOrtho = (float)(Program.GLControl.Width / Program.Camera.OrthographicSize);
            float aspectRatioHeightOrtho = (float)(Program.GLControl.Height / Program.Camera.OrthographicSize);

            // Update model view matrices
            foreach (HWMesh mesh in HWScene.Meshes)
            {
                if (mesh.Visible)
                {
                    mesh.CalculateModelMatrix();

                    if (!Program.Camera.Orthographic)
                        mesh.ViewProjectionMatrix = View * Matrix4.CreatePerspectiveFieldOfView(Program.Camera.FieldOfView, aspectRatio, Program.Camera.NearClipDistance, Program.Camera.ClipDistance);
                    else
                        mesh.ViewProjectionMatrix = View * Matrix4.CreateOrthographic(aspectRatioWidthOrtho, aspectRatioHeightOrtho, Program.Camera.NearClipDistance, Program.Camera.ClipDistance);

                    mesh.ModelViewProjectionMatrix = mesh.ModelMatrix * mesh.ViewProjectionMatrix;
                }
            }

            foreach (EditorMesh mesh in EditorScene.meshes)
            {
                if (mesh.Visible)
                {
                    mesh.CalculateModelMatrix();

                    if (!Program.Camera.Orthographic)
                        mesh.ViewProjectionMatrix = View * Matrix4.CreatePerspectiveFieldOfView(Program.Camera.FieldOfView, aspectRatio, Program.Camera.NearClipDistance, Program.Camera.ClipDistance);
                    else
                        mesh.ViewProjectionMatrix = View * Matrix4.CreateOrthographic(aspectRatioWidthOrtho, aspectRatioHeightOrtho, Program.Camera.NearClipDistance, Program.Camera.ClipDistance);

                    mesh.ModelViewProjectionMatrix = mesh.ModelMatrix * mesh.ViewProjectionMatrix;
                }
            }
        }

        public static void Render()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Dictionary<string, int> indiceat = new Dictionary<string, int>();
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            foreach (HWMesh mesh in HWScene.Meshes)
            {
                if (!indiceat.ContainsKey(mesh.Material.Shader))
                    indiceat[mesh.Material.Shader] = 0;
                if (mesh.Visible && !mesh.Translucent)
                    indiceat[mesh.Material.Shader] += DrawHWMesh(mesh, indiceat[mesh.Material.Shader]);
            }

            GL.Enable(EnableCap.Blend);

            //Draw translucent ship meshes
            GL.DepthMask(false);
            foreach (HWMesh mesh in HWScene.Meshes)
            {
                if (!indiceat.ContainsKey(mesh.Material.Shader))
                    indiceat[mesh.Material.Shader] = 0;
                if (mesh.Visible && mesh.Translucent)
                    indiceat[mesh.Material.Shader] += DrawHWMesh(mesh, indiceat[mesh.Material.Shader]);
            }
            GL.DepthMask(true);

            int editor_indiceat = 0;
            foreach (EditorMesh mesh in EditorScene.meshes)
            {
                if (mesh.Visible && mesh.NeverDrawInFront)
                    editor_indiceat += DrawEditorMesh(mesh, editor_indiceat);
            }
            
            if (DrawVisualizationsInFront)
                GL.Clear(ClearBufferMask.DepthBufferBit);

            GL.Enable(EnableCap.AlphaTest);
            foreach (EditorMesh mesh in EditorScene.meshes)
            {
                if (mesh.Visible && !mesh.NeverDrawInFront && mesh.DrawAboveShip)
                    editor_indiceat += DrawEditorMesh(mesh, editor_indiceat);
            }
            
            foreach (EditorMesh mesh in EditorScene.meshes)
            {
                if (mesh.Visible && !mesh.NeverDrawInFront && !mesh.DrawAboveShip)
                    editor_indiceat += DrawEditorMesh(mesh, editor_indiceat);
            }
            
            GL.Disable(EnableCap.AlphaTest);
            GL.Disable(EnableCap.Blend);

            GetError("OpenTK Rendering");
            Program.GLControl.SwapBuffers();
        }

        private static int DrawHWMesh(HWMesh mesh, int index)
        {
            HWTexture texture = null;
            if (mesh.Material != null)
            {
                texture = mesh.Material.DiffuseTexture;
                CurrentShader = mesh.Material.Shader;
            }
            GL.UseProgram(shaders[CurrentShader].ProgramID);

            Matrix4 model = mesh.ModelMatrix;
            Matrix4 camera = Program.Camera.GetViewMatrix();
            Matrix4 projection = Matrix4.Identity;

            if (!Program.Camera.Orthographic)
                projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, (float)Program.GLControl.Width / (float)Program.GLControl.Height, Program.Camera.NearClipDistance, Program.Camera.ClipDistance);
            else
                projection = Matrix4.CreateOrthographic((float)(Program.GLControl.Width / Program.Camera.OrthographicSize), (float)(Program.GLControl.Height / Program.Camera.OrthographicSize), Program.Camera.NearClipDistance, Program.Camera.ClipDistance);

            GL.UniformMatrix4(shaders[CurrentShader].GetUniform("inMatM"), false, ref model);
            GL.UniformMatrix4(shaders[CurrentShader].GetUniform("inMatV"), false, ref camera);
            GL.UniformMatrix4(shaders[CurrentShader].GetUniform("inMatP"), false, ref projection);
            
            if (SOB_USECLIP(CurrentShader))
            {
                GL.Uniform4(shaders[CurrentShader].GetUniform("inClipPlane"), 0f, 0f, 0f, 0f);
            }
            
            GL.Uniform4(shaders[CurrentShader].GetUniform("inShipExps"), 0f, 0f, 0f, 0f);
            GL.Uniform4(shaders[CurrentShader].GetUniform("inTime"), Vector4.Zero);
            
            if (SOB_BADGE(CurrentShader))
            {
                //AttachTexture(CurrentShader, TextureUnit.Texture4, Program.BadgeTexture, "inTexBadge", 4);
            }

            if (SOB_THRUSTERS(CurrentShader))
            {
                AttachTexture(CurrentShader, TextureUnit.Texture0, mesh.Material.DiffuseTexture, "inTexDiffOn", 0);
                AttachTexture(CurrentShader, TextureUnit.Texture1, mesh.Material.GlowTexture, "inTexGlowOn", 1);
                AttachTexture(CurrentShader, TextureUnit.Texture4, mesh.Material.ThrusterOffDiffuseTexture, "inTexDiffOff", 4);
                AttachTexture(CurrentShader, TextureUnit.Texture5, mesh.Material.ThrusterOffGlowTexture, "inTexGlowOff", 5);
                GL.Uniform4(shaders[CurrentShader].GetUniform("inColEngine"), ThrusterInterpolation, 0, 0, 0);
            }
            else
            {
                AttachTexture(CurrentShader, TextureUnit.Texture0, mesh.Material.DiffuseTexture, "inTexDiff", 0);
                AttachTexture(CurrentShader, TextureUnit.Texture1, mesh.Material.GlowTexture, "inTexGlow", 1);
                if (SOB_GLOWRGB(CurrentShader))
                {
                    if (CurrentShader == "ore" || CurrentShader == "salvage")
                        AttachTexture(CurrentShader, TextureUnit.Texture2, mesh.Material.SpecularTexture, "inTexSpec", 2);
                    else
                        AttachTexture(CurrentShader, TextureUnit.Texture4, mesh.Material.SpecularTexture, "inTexSpec", 4);
                }
            }

            if (SOB_RESOURCE(CurrentShader))
            {
                if (SOB_DUALINPUT(CurrentShader))
                {
                    GL.Uniform4(shaders[CurrentShader].GetUniform("inGridDiff"), 1f, 1f, 0f, 0f);
                    GL.Uniform4(shaders[CurrentShader].GetUniform("inGridGlow"), 1f, 1f, 0f, 0f);
                    GL.Uniform4(shaders[CurrentShader].GetUniform("inGridSpec"), 1f, 1f, 0f, 0f);
                    GL.Uniform4(shaders[CurrentShader].GetUniform("inGridNorm"), 1f, 1f, 0f, 0f);
                }
            }

            if (SOB_RESOURCE(CurrentShader))
            {
                //AttachTexture(CurrentShader, TextureUnit.Texture4, mesh.Material.Progress, "inTexProgress", 4);
                GL.Uniform2(shaders[CurrentShader].GetUniform("inFadeInfo"), 0f, 0f);
                GL.Uniform4(shaders[CurrentShader].GetUniform("inTime"), 0f, 0f, 0f, 0f);
                GL.Uniform3(shaders[CurrentShader].GetUniform("inFadeWindow"), 0.1f, 0.1f, 0.9f);
                GL.Uniform4(shaders[CurrentShader].GetUniform("inGlowStyle"), 0f, 1f, 0f, 0f);

                if (SOB_DUALINPUT(CurrentShader)) // SOB_DUALINPUT
                {
                    GL.Uniform4(shaders[CurrentShader].GetUniform("inGridDiff"), 1f, 1f, 0f, 0f);
                    GL.Uniform4(shaders[CurrentShader].GetUniform("inGridGlow"), 1f, 1f, 0f, 0f);
                    GL.Uniform4(shaders[CurrentShader].GetUniform("inGridSpec"), 1f, 1f, 0f, 0f);
                    GL.Uniform4(shaders[CurrentShader].GetUniform("inGridNorm"), 1f, 1f, 0f, 0f);

                    GL.Uniform4(shaders[CurrentShader].GetUniform("inMulDiff0"), 1f, 1f, 1f, 1f);
                    GL.Uniform4(shaders[CurrentShader].GetUniform("inMulDiff1"), 1f, 1f, 1f, 1f);
                    GL.Uniform4(shaders[CurrentShader].GetUniform("inMulGlow0"), 1f, 1f, 1f, 1f);
                    GL.Uniform4(shaders[CurrentShader].GetUniform("inMulGlow1"), 1f, 1f, 1f, 1f);
                    GL.Uniform4(shaders[CurrentShader].GetUniform("inMulSpec0"), 1f, 1f, 1f, 1f);
                    GL.Uniform4(shaders[CurrentShader].GetUniform("inMulSpec1"), 1f, 1f, 1f, 1f);
                }

                if (SOB_DEBRIS(CurrentShader))
                {
                    //uniform vec4 inFXInfo[2];
                }
            }

            if (SOB_TEAMTEX(CurrentShader))
            {
                AttachTexture(CurrentShader, TextureUnit.Texture2, mesh.Material.TeamTexture, "inTexTeam", 2);
                AttachTexture(CurrentShader, TextureUnit.Texture9, mesh.Material.StripeTexture, "inTexStripe", 9);
            }

            AttachTexture(CurrentShader, TextureUnit.Texture3, mesh.Material.NormalTexture, "inTexTeam", 3);
            //AttachTexture(CurrentShader, TextureUnit.Texture6, texEnv0, "inTexEnv0", 6);
            //AttachTexture(CurrentShader, TextureUnit.Texture7, texEnv1, "inTexEnv1", 7);

            GL.Uniform4(shaders[CurrentShader].GetUniform("inSOBParams"), 1f, 0f, 0f, 0f);
            //GL.Uniform4(shaders[CurrentShader].GetUniform("inLifeParams"), Vector4.One);
            GL.Uniform4(shaders[CurrentShader].GetUniform("inFogColor"), Vector4.Zero);
            GL.Uniform4(shaders[CurrentShader].GetUniform("inFogWindow"), Vector4.Zero);
            
            if (SOB_TEAM(CurrentShader))
            {
                GL.Uniform4(shaders[CurrentShader].GetUniform("inColTeam"), TeamColor);
                GL.Uniform4(shaders[CurrentShader].GetUniform("inColStripe"), StripeColor);
            }
            if (!SOB_DEBRIS(CurrentShader))
                GL.Uniform4(shaders[CurrentShader].GetUniform("inColEffect"), 0f, 0f, 0f, 0f);

            GL.Uniform4(shaders[CurrentShader].GetUniform("inSurfDiff"), 0f, 0.95f, 0f, 0f);
            if (CurrentShader == "thruster")
                GL.Uniform4(shaders[CurrentShader].GetUniform("inSurfGlow"), 1.1f, 0.25f, 0f, 0f);
            else
                GL.Uniform4(shaders[CurrentShader].GetUniform("inSurfGlow"), 1f, 0.25f, 0f, 0f);
            GL.Uniform4(shaders[CurrentShader].GetUniform("inSurfSpec"), 1f, 1.5f, 0f, 0f);
            GL.Uniform4(shaders[CurrentShader].GetUniform("inSurfGloss"), 0.1f, 75f, 30f, 0f);
            GL.Uniform4(shaders[CurrentShader].GetUniform("inSurfRefl"), 0.15f, 0.35f, 0.65f, 0f);
            GL.Uniform4(shaders[CurrentShader].GetUniform("inSurfFren"), 3.8f, 1.1f, 2.1f, 0f);
            GL.Uniform4(shaders[CurrentShader].GetUniform("inSurfPaint"), 1.8f, -115f, -25f, 0.88f);
            GL.Uniform4(shaders[CurrentShader].GetUniform("inSurfPeak"), 0.008f, 0.0035f, 0.5f, 2f);

            GetError("OpenTK Rendering");
            GL.Uniform4(shaders[CurrentShader].GetUniform("inPaintStyle"), 0f, 2f, 0f, 0f);

            GL.Uniform4(shaders[CurrentShader].GetUniform("inGammaScale"), Vector4.One);
            //GL.Uniform4(shaders[CurrentShader].GetUniform("inBGAddLight"), Vector4.Zero);
            //GL.Uniform4(shaders[CurrentShader].GetUniform("inBGEnvParams"), Vector4.Zero);

            if (SOB_BAYLIGHT(CurrentShader))
            {
                GL.Uniform4(shaders[CurrentShader].GetUniform("inBayExps"), 0f, 0f, 0f, 0f);
            }
            else
            {
                GL.Uniform2(shaders[CurrentShader].GetUniform("inLightCounts"), Light.Lights.Count - 1, 7);
                GL.Uniform4(shaders[CurrentShader].GetUniform("inLightShip[0]"), 96, GetShipLights());
                GL.Uniform4(shaders[CurrentShader].GetUniform("inLightCore[0]"), 7, GetCoreLights());
            }

            GL.DrawElements(BeginMode.Triangles, mesh.IndiceCount, DrawElementsType.UnsignedInt, index * sizeof(uint));

            GetError("OpenTK Rendering");
            return mesh.IndiceCount;
        }

        private static int DrawEditorMesh(EditorMesh mesh, int index)
        {
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);

            if (mesh.Wireframe)
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);

            CurrentShader = "editor";
            GL.UseProgram(shaders[CurrentShader].ProgramID);

            GL.BindBuffer(BufferTarget.ArrayBuffer, shaders[CurrentShader].GetBuffer("inPos"));
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(mesh.Vertices.Length * Vector3.SizeInBytes), mesh.Vertices, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(shaders[CurrentShader].GetAttribute("inPos"), 3, VertexAttribPointerType.Float, false, 0, 0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, shaders[CurrentShader].GetBuffer("inUV0"));
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(mesh.TextureCoords.Length * Vector2.SizeInBytes), mesh.TextureCoords, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(shaders[CurrentShader].GetAttribute("inUV0"), 2, VertexAttribPointerType.Float, false, 0, 0);

            Matrix4 model = mesh.ModelMatrix;
            Matrix4 camera = Program.Camera.GetViewMatrix();
            Matrix4 projection = Matrix4.Identity;
            if (!Program.Camera.Orthographic)
                projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, (float)Program.GLControl.Width / (float)Program.GLControl.Height, Program.Camera.NearClipDistance, Program.Camera.ClipDistance);
            else
                projection = Matrix4.CreateOrthographic((float)(Program.GLControl.Width / Program.Camera.OrthographicSize), (float)(Program.GLControl.Height / Program.Camera.OrthographicSize), Program.Camera.NearClipDistance, Program.Camera.ClipDistance);

            GL.UniformMatrix4(shaders[CurrentShader].GetUniform("inMatM"), false, ref model);
            GL.UniformMatrix4(shaders[CurrentShader].GetUniform("inMatV"), false, ref camera);
            GL.UniformMatrix4(shaders[CurrentShader].GetUniform("inMatP"), false, ref projection);

            HWTexture texture = null;
            if (mesh.Material != null)
            {
                texture = mesh.Material.DiffuseTexture;
                GL.Uniform3(shaders[CurrentShader].GetUniform("diffuse"), ref mesh.Material.DiffuseColor);
                GL.Uniform1(shaders[CurrentShader].GetUniform("opacity"), mesh.Material.Opacity);
            }
            else
            {
                GL.Uniform3(shaders[CurrentShader].GetUniform("diffuse"), Vector3.One);
                GL.Uniform1(shaders[CurrentShader].GetUniform("opacity"), 1f);
            }

            if (texture != null)
            {
                GL.ActiveTexture(TextureUnit.Texture0);
                GL.BindTexture(TextureTarget.Texture2D, texture.ID);
                GL.Uniform1(shaders[CurrentShader].GetUniform("inTexMat"), 0);
                GL.Uniform1(shaders[CurrentShader].GetUniform("isTextured"), 1); //Tell shader to use texture colors
            }
            else
            {
                GL.Uniform1(shaders[CurrentShader].GetUniform("isTextured"), 0); //Tell shader to use vertex colors
            }

            if (mesh.Shaded)
            {
                //GL.Uniform1(shaders[CurrentShader].GetUniform("shaded"), 1); //Tell shader to calculate lighting

                if (mesh.Material != null)
                {
                    GL.Uniform3(shaders[CurrentShader].GetUniform("specular"), ref mesh.Material.SpecularColor);
                    GL.Uniform1(shaders[CurrentShader].GetUniform("shininess"), mesh.Material.SpecularExponent);
                }
            }
            //else
            //    GL.Uniform1(shaders[CurrentShader].GetUniform("shaded"), 0); //Tell shader not to calculate lighting

            if (mesh.BlackIsTransparent)
                GL.Uniform1(shaders[CurrentShader].GetUniform("isNavLight"), 1);
            else
                GL.Uniform1(shaders[CurrentShader].GetUniform("isNavLight"), 0);

            if (mesh.VertexColored)
                GL.Uniform1(shaders[CurrentShader].GetUniform("vertexColored"), 1);
            else
                GL.Uniform1(shaders[CurrentShader].GetUniform("vertexColored"), 0);


            if (mesh.GetType() == typeof(EditorLine))
                GL.DrawElements(BeginMode.Lines, mesh.IndiceCount, DrawElementsType.UnsignedInt, 0);
            else
                GL.DrawElements(BeginMode.Triangles, mesh.IndiceCount, DrawElementsType.UnsignedInt, 0);

            return 0; // mesh.IndiceCount;
        }

        public static void Resize()
        {
            GL.Viewport(Program.GLControl.ClientRectangle.X, Program.GLControl.ClientRectangle.Y, Program.GLControl.ClientRectangle.Width, Program.GLControl.ClientRectangle.Height);

            Matrix4 projection = Matrix4.Identity;
            if (!Program.Camera.Orthographic)
                projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, (float)Program.GLControl.Width / (float)Program.GLControl.Height, Program.Camera.NearClipDistance, Program.Camera.ClipDistance);
            else
                projection = Matrix4.CreateOrthographic((float)(Program.GLControl.Width / Program.Camera.OrthographicSize), (float)(Program.GLControl.Height / Program.Camera.OrthographicSize), Program.Camera.NearClipDistance, Program.Camera.ClipDistance);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);
        }

        private static void GetError(string type)
        {
            ErrorCode code = GL.GetError();
            if (code != ErrorCode.NoError)
                Log.WriteLine(type + ": " + code);
        }

        private static bool SOB_BADGE(string shader)
        {
            return
                shader == "badge" ||
                shader == "badge_glow";
        }

        private static bool SOB_BAYLIGHT(string shader)
        {
            return shader == "bay";
        }

        private static bool SOB_DUALINPUT(string shader)
        {
            return shader == "ore";
        }

        private static bool SOB_DEBRIS(string shader)
        {
            return false;
        }

        private static bool SOB_GLOWRGB(string shader)
        {
            return
                shader == "badge_glow" ||
                shader == "ore" ||
                shader == "salvage" ||
                shader == "ship_glow";
        }

        private static bool SOB_RESOURCE(string shader)
        {
            return
                shader == "ore" ||
                shader == "salvage";
        }

        private static bool SOB_TEAM(string shader)
        {
            return
                shader == "badge" ||
                shader == "badge_glow" ||
                shader == "bay" ||
                shader == "ship" ||
                shader == "ship_glow" ||
                shader == "thruster";
        }

        private static bool SOB_TEAMTEX(string shader)
        {
            return
                shader.StartsWith("matte") ||
                SOB_TEAM(shader);
        }

        private static bool SOB_THRUSTERS(string shader)
        {
            return shader == "thruster";
        }

        private static bool SOB_USECLIP(string shader)
        {
            return SOB_TEAM(shader);
        }

        private static void AttachTexture(string shader, TextureUnit tunit, HWTexture tex, string uniform, int id)
        {
            if (tex != null)
            {
                GL.ActiveTexture(tunit);
                GL.BindTexture(TextureTarget.Texture2D, tex.ID);
                GL.Uniform1(shaders[shader].GetUniform(uniform), id);
            }
        }

        private static float[] GetShipLights()
        {
            int shipLight = 0;
            float[] shiplights = new float[96 * 4];
            foreach (Light l in Light.Lights)
            {
                if (l != AmbientLight)
                {
                    // uniform vec4 inLightShip[96];   // Pos/Diff/Spec (or just Pos/Diff) + attenuations in W
                    shiplights[4 * shipLight + 0] = l.Position.X;
                    shiplights[4 * shipLight + 1] = l.Position.Y;
                    shiplights[4 * shipLight + 2] = l.Position.Z;
                    shiplights[4 * shipLight + 3] = l.Attenuation; // l.ConstantAttenuation
                    shipLight++;
                    shiplights[4 * shipLight + 0] = l.Color.X;
                    shiplights[4 * shipLight + 1] = l.Color.Y;
                    shiplights[4 * shipLight + 2] = l.Color.Z;
                    shiplights[4 * shipLight + 3] = l.Attenuation; // l.LinearAttenuation
                    shipLight++;
                    shiplights[4 * shipLight + 0] = 0; // l.Specular.X
                    shiplights[4 * shipLight + 1] = 0; // l.Specular.Y
                    shiplights[4 * shipLight + 2] = 0; // l.Specular.Z
                    shiplights[4 * shipLight + 3] = l.Attenuation; // l.QuadraticAttenuation
                    shipLight++;
                }
            }
            return shiplights;
        }

        private static float[] GetCoreLights()
        {
            Vector3 keyLightVec = new Vector3(0f, -0.1f, 1f);
            Vector3 fillLightVec = new Vector3(-0.6f, 0.4f, -0.3f);

            float[] corelights = new float[7 * 4];

            // ambient
            corelights[4 * 0 + 0] = AmbientLight.Color.X;
            corelights[4 * 0 + 1] = AmbientLight.Color.Y;
            corelights[4 * 0 + 2] = AmbientLight.Color.Z;

            // Key light vector
            corelights[4 * 1 + 0] = keyLightVec.X;
            corelights[4 * 1 + 1] = keyLightVec.Y;
            corelights[4 * 1 + 2] = keyLightVec.Z;

            // Key light diffuse color
            corelights[4 * 2 + 0] = 1f;
            corelights[4 * 2 + 1] = 1f;
            corelights[4 * 2 + 2] = 1f;

            // Key light specular color
            corelights[4 * 3 + 0] = 0.2f;
            corelights[4 * 3 + 1] = 0.2f;
            corelights[4 * 3 + 2] = 0.2f;

            // Fill light vector
            corelights[4 * 4 + 0] = fillLightVec.X;
            corelights[4 * 4 + 1] = fillLightVec.Y;
            corelights[4 * 4 + 2] = fillLightVec.Z;

            // Fill light diffuse color
            corelights[4 * 5 + 0] = 1f;
            corelights[4 * 5 + 1] = 1f;
            corelights[4 * 5 + 2] = 1f;

            // Fill light specular color
            corelights[4 * 6 + 0] = 0.2f;
            corelights[4 * 6 + 1] = 0.2f;
            corelights[4 * 6 + 2] = 0.2f;

            return corelights;
        }
    }
}