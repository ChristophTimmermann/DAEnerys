using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using HWShaderManifest;

namespace DAEnerys
{

    static class Renderer
    {
        static Shader editor_shader;

        public static bool ViewInvalid = false;
        public static bool MeshDataInvalid = false;

        public static HWTexture DefaultTexture;
        public static HWTextureCube BackgroundTexture = null;
        public static HWTextureCube EnvironmentTexture0 = null;
        public static HWTextureCube EnvironmentTexture1 = null;

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
                    Renderer.Invalidate();
                }
            }
        }

        public static Color TeamColor { get; set; } = Color.FromArgb(255, 92, 139, 170);
        public static Color StripeColor { get; set; } = Color.FromArgb(255, 204, 204, 204);
        public static Color EngineGlowColor { get; set; } = Color.FromArgb(255, 66, 120, 163);

        // VARIABLE SHADER INPUTS
        //timeTable
        public static float Exec { get; set; } = 0f;
        public static float ExecDelta { get; set; } = 0f;
        public static float Sim { get; set; } = 0f;
        public static float SimDelta { get; set; } = 0f;

        //sobParams
        public static float SOBAlpha { get; set; } = 0.3f;
        public static float SOBCloak { get; set; } = 0f;
        public static float SOBClip { get; set; } = 0f;

        //lifeParams
        public static float LifeAlpha { get; set; } = 1f;
        public static float DeathRatio { get; set; } = 1f;

        public static Color FogColor { get; set; } = Color.FromArgb(0, 255, 255, 255);

        public static float MinClipDistance { get; set; } = -1000f;
        public static float MaxClipDistance { get; set; } = 1000f;
        public static float ClipDistance { get; set; } = 1000f;

        public static bool HACK_SpecialSauce
        {
            get
            {
                return ManifestConfig.GetValue("HACK_SpecialSauce") == 1;
            }
            set
            {
                ManifestConfig.SetValue("HACK_SpecialSauce", value ? 1 : 0);
            }
        }

        public static bool HACK_AllIFeelIsPain
        {
            get
            {
                return ManifestConfig.GetValue("HACK_AllIFeelIsPain") == 1;
            }
            set
            {
                ManifestConfig.SetValue("HACK_AllIFeelIsPain", value ? 1 : 0);
            }
        }

        public static bool CFG_Patch_AltHyper
        {
            get
            {
                return ManifestConfig.GetValue("CFG_Patch_AltHyper") == 1;
            }
            set
            {
                ManifestConfig.SetValue("CFG_Patch_AltHyper", value ? 1 : 0);
            }
        }

        public static float PaintStyleCurve { get; set; } = 0f;
        public static float PaintStyleScale { get; set; } = 2f;
        public static float PaintStyleOffset { get; set; } = 0f;


        private static HWTexture badgeTexture = null;
        public static HWTexture BadgeTexture
        {
            get { return badgeTexture; }
            set { badgeTexture = value; }
        }

        private static HWTexture blackTexture = null;
        public static HWTexture BlackTexture
        {
            get
            {
                if (blackTexture == null)
                    blackTexture = HWTexture.MakeTexture("default_black", "", 0, 0, 0, 1);
                return blackTexture;
            }
            private set { }
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

        public static SurfaceDiff SurfaceDiff { get; private set; } = new SurfaceDiff();
        public static SurfaceGlow SurfaceGlow { get; private set; } = new SurfaceGlow();
        public static SurfaceSpec SurfaceSpec { get; private set; } = new SurfaceSpec();
        public static SurfaceGloss SurfaceGloss { get; private set; } = new SurfaceGloss();
        public static SurfaceRefl SurfaceRefl { get; private set; } = new SurfaceRefl();
        public static SurfaceFren SurfaceFren { get; private set; } = new SurfaceFren();
        public static SurfacePaint SurfacePaint { get; private set; } = new SurfacePaint();
        public static SurfacePeak SurfacePeak { get; private set; } = new SurfacePeak();

        public static float ThrusterInterpolation = 1;
        public static float Progress = 0;

        static int editor_pos_buffer = 0;
        static int editor_col_buffer = 0;
        static int editor_uv0_buffer = 0;
        static int editor_ind_buffer = 0;
        static int mesh_ind_buffer = 0;
        static int mesh_pos_buffer = 0;
        static int mesh_nrm_buffer = 0;
        static int mesh_tan_buffer = 0;
        static int mesh_bin_buffer = 0;
        static int mesh_uv0_buffer = 0;
        static int mesh_uv1_buffer = 0;

        public static Matrix4 View = Matrix4.Identity;
        public static Matrix4 ViewProjection = Matrix4.Identity;

        public static void Init()
        {
            GL.ClearColor(BackgroundColor);

            GL.Enable(EnableCap.DepthTest);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            GL.Enable(EnableCap.CullFace);
            GL.AlphaFunc(AlphaFunction.Greater, 0.05f);

            GL.LineWidth(2);

            // Gen buffers
            GL.GenBuffers(1, out editor_pos_buffer);
            GL.GenBuffers(1, out editor_uv0_buffer);
            GL.GenBuffers(1, out editor_col_buffer);
            GL.GenBuffers(1, out editor_ind_buffer);
            GL.GenBuffers(1, out mesh_pos_buffer);
            GL.GenBuffers(1, out mesh_nrm_buffer);
            GL.GenBuffers(1, out mesh_tan_buffer);
            GL.GenBuffers(1, out mesh_bin_buffer);
            GL.GenBuffers(1, out mesh_uv0_buffer);
            GL.GenBuffers(1, out mesh_uv1_buffer);
            GL.GenBuffers(1, out mesh_ind_buffer);

            // Load shaders from file
            ShaderManifest.DataPaths.AddRange(HWData.DataPaths);
            ShaderManifest.Init();
            editor_shader = new Shader("editor.vs", "editor.fs", true);

            //AmbientLight.Enabled = false;
            DefaultTexture = new HWTexture(Path.Combine(Program.EXECUTABLE_PATH, @"resources/missing.tga"));
            HWMaterial.DefaultMaterial = new HWMaterial("matte");
            HWMaterial.DefaultMaterial.LoadTextures();

            //EnvironmentTexture0 = new HWTextureCube(
            //    Path.Combine(Program.EXECUTABLE_PATH, @"resources/cubemap-test/black.tga"),
            //    Path.Combine(Program.EXECUTABLE_PATH, @"resources/cubemap-test/black.tga"),
            //    Path.Combine(Program.EXECUTABLE_PATH, @"resources/cubemap-test/black.tga"),
            //    Path.Combine(Program.EXECUTABLE_PATH, @"resources/cubemap-test/black.tga"),
            //    Path.Combine(Program.EXECUTABLE_PATH, @"resources/cubemap-test/black.tga"),
            //    Path.Combine(Program.EXECUTABLE_PATH, @"resources/cubemap-test/black.tga"));
            //EnvironmentTexture1 = new HWTextureCube(
            //    Path.Combine(Program.EXECUTABLE_PATH, @"resources/cubemap-test/posx.tga"),
            //    Path.Combine(Program.EXECUTABLE_PATH, @"resources/cubemap-test/negx.tga"),
            //    Path.Combine(Program.EXECUTABLE_PATH, @"resources/cubemap-test/posy.tga"),
            //    Path.Combine(Program.EXECUTABLE_PATH, @"resources/cubemap-test/negy.tga"),
            //    Path.Combine(Program.EXECUTABLE_PATH, @"resources/cubemap-test/posz.tga"),
            //    Path.Combine(Program.EXECUTABLE_PATH, @"resources/cubemap-test/negz.tga"));

            HWBadge.DefaultBadge = new HWBadge("daenerys", Path.Combine(Program.EXECUTABLE_PATH, @"resources/daenerys.tga"));
            BadgeTexture = HWBadge.DefaultBadge.Texture;

            if (EnableVSync)
                GraphicsContext.CurrentContext.SwapInterval = 1;
            else
                GraphicsContext.CurrentContext.SwapInterval = 0;
        }

        public static void ReloadShaders()
        {
            editor_shader.Reload();
            ShaderManifest.Reload();
        }

        private static void BindBufferData(int buffer, Vector2[] data, bool normalized)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, buffer);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(data.Length * Vector2.SizeInBytes), data, BufferUsageHint.StaticDraw);
            GetError("BindBufferData");
        }

        private static void BindBufferData(int buffer, Vector3[] data, bool normalized)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, buffer);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(data.Length * Vector3.SizeInBytes), data, BufferUsageHint.StaticDraw);
            GetError("BindBufferData");
        }

        private static void BindBufferData(int buffer, Vector4[] data, bool normalized)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, buffer);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(data.Length * Vector4.SizeInBytes), data, BufferUsageHint.StaticDraw);
            GetError("BindBufferData");
        }

        private static void UpdateMeshData()
        {
            if (!MeshDataInvalid)
                return;
            MeshDataInvalid = false;

            {
                // Assemble mesh data
                List<Vector3> mesh_verts = new List<Vector3>();
                List<Vector3> mesh_normals = new List<Vector3>();
                List<Vector3> mesh_tangents = new List<Vector3>();
                List<Vector3> mesh_bitangents = new List<Vector3>();
                List<Vector2> mesh_uv0 = new List<Vector2>();
                List<Vector2> mesh_uv1 = new List<Vector2>();
                List<int> mesh_inds = new List<int>();

                // Assemble vertex and index data for all volumes
                int mesh_vertcount = 0;

                //SORT SHIP MESHES
                List<HWMesh> hwMeshList = new List<HWMesh>();

                foreach (HWMesh mesh in HWMesh.Meshes)
                {
                    if (!mesh.Translucent)
                        hwMeshList.Add(mesh);
                }
                foreach (HWMesh mesh in HWMesh.Meshes)
                {
                    if (mesh.Translucent)
                        hwMeshList.Add(mesh);
                }
                HWMesh.Meshes = hwMeshList;

                foreach (HWMesh mesh in HWMesh.Meshes)
                {
                    mesh_verts.AddRange(mesh.Vertices);
                    mesh_normals.AddRange(mesh.Normals);
                    mesh_tangents.AddRange(mesh.Tangents);
                    mesh_bitangents.AddRange(mesh.BiTangents);
                    mesh_uv0.AddRange(mesh.UV0);
                    mesh_uv1.AddRange(mesh.UV1);

                    mesh_inds.AddRange(mesh.GetIndices(mesh_vertcount));

                    mesh_vertcount += mesh.VertexCount;
                }

                Vector3[] vertdata = mesh_verts.ToArray();
                Vector3[] normdata = mesh_normals.ToArray();
                Vector3[] tangentdata = mesh_tangents.ToArray();
                Vector3[] bitangentdata = mesh_bitangents.ToArray();
                Vector2[] uv0data = mesh_uv0.ToArray();
                Vector2[] uv1data = mesh_uv1.ToArray();
                int[] indicedata = mesh_inds.ToArray();

                BindBufferData(mesh_pos_buffer, vertdata, false);
                BindBufferData(mesh_nrm_buffer, normdata, false);
                BindBufferData(mesh_tan_buffer, tangentdata, false);
                BindBufferData(mesh_bin_buffer, bitangentdata, false);
                BindBufferData(mesh_uv0_buffer, uv0data, false);

                BindBufferData(mesh_uv1_buffer, uv1data, false); // for SOB_BADGE shader

                GL.BindBuffer(BufferTarget.ElementArrayBuffer, mesh_ind_buffer);
                GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(indicedata.Length * sizeof(uint)), indicedata, BufferUsageHint.StaticDraw);
                GetError("Mesh Buffering");
            }

            {
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
                List<Vector3> editor_colors = new List<Vector3>();
                List<Vector2> editor_uv0 = new List<Vector2>();
                List<int> editor_inds = new List<int>();
                int editor_vertcount = 0;

                foreach (EditorMesh mesh in EditorScene.meshes)
                {
                    editor_verts.AddRange(mesh.Vertices);
                    editor_uv0.AddRange(mesh.UV0);
                    editor_colors.AddRange(mesh.Colors);
                    editor_inds.AddRange(mesh.GetIndices(editor_vertcount).ToList());
                    editor_vertcount += mesh.VertexCount;
                }
                Vector3[] vertdata = editor_verts.ToArray();
                Vector2[] uv0data = editor_uv0.ToArray();
                Vector3[] coldata = editor_colors.ToArray();
                int[] indicedata = editor_inds.ToArray();

                BindBufferData(editor_pos_buffer, vertdata, false);
                BindBufferData(editor_uv0_buffer, uv0data, false);
                BindBufferData(editor_col_buffer, coldata, false);

                // Buffer index data
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, editor_ind_buffer);
                GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(indicedata.Length * sizeof(uint)), indicedata, BufferUsageHint.StaticDraw);
                GetError("Editor Buffering");
            }

            GetError("OpenTK Buffering");
        }
        private static void UpdateView()
        {
            if (!ViewInvalid)
                return;
            ViewInvalid = false;

            View = Program.Camera.GetViewMatrix();
            AmbientLight.Position = new Vector4(Program.Camera.Position, 0);
            float aspectRatio = (float)Program.GLControl.Width / (float)Program.GLControl.Height;
            float aspectRatioWidthOrtho = (float)(Program.GLControl.Width / Program.Camera.OrthographicSize);
            float aspectRatioHeightOrtho = (float)(Program.GLControl.Height / Program.Camera.OrthographicSize);

            if (!Program.Camera.Orthographic)
                ViewProjection = View * Matrix4.CreatePerspectiveFieldOfView(Program.Camera.FieldOfView, aspectRatio, Program.Camera.NearClipDistance, Program.Camera.ClipDistance);
            else
                ViewProjection = View * Matrix4.CreateOrthographic(aspectRatioWidthOrtho, aspectRatioHeightOrtho, Program.Camera.NearClipDistance, Program.Camera.ClipDistance);
        }

        public static void Render()
        {
            UpdateMeshData();
            UpdateView();

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            int indiceat = 0;

            foreach (HWMesh mesh in HWMesh.Meshes)
                if (!mesh.Translucent)
                    indiceat += DrawHWMesh(mesh, indiceat);

            GL.Enable(EnableCap.AlphaTest);
            GL.Enable(EnableCap.Blend);

            foreach (HWMesh mesh in HWMesh.Meshes)
                if (mesh.Translucent)
                    indiceat += DrawHWMesh(mesh, indiceat);

            GL.UseProgram(editor_shader.ProgramID);
            editor_shader.LinkAttrib3(editor_pos_buffer, "inPos", false);
            editor_shader.LinkAttrib3(editor_col_buffer, "inColor", false);
            editor_shader.LinkAttrib2(editor_uv0_buffer, "inUV0", false);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, editor_ind_buffer);

            int editor_indiceat = 0;
            foreach (EditorMesh mesh in EditorScene.meshes)
            {
                if (mesh.NeverDrawInFront)
                    editor_indiceat += DrawEditorMesh(mesh, editor_indiceat);
            }

            if (DrawVisualizationsInFront)
                GL.Clear(ClearBufferMask.DepthBufferBit);

            foreach (EditorMesh mesh in EditorScene.meshes)
            {
                if (!mesh.NeverDrawInFront && mesh.DrawAboveShip)
                    editor_indiceat += DrawEditorMesh(mesh, editor_indiceat);
            }

            foreach (EditorMesh mesh in EditorScene.meshes)
            {
                if (!mesh.NeverDrawInFront && !mesh.DrawAboveShip)
                    editor_indiceat += DrawEditorMesh(mesh, editor_indiceat);
            }

            GL.Disable(EnableCap.AlphaTest);
            GL.Disable(EnableCap.Blend);

            GetError("OpenTK Rendering");
            Program.GLControl.SwapBuffers();
        }

        private static void UpdateSurface(Surface surface, HWMesh mesh)
        {
            string shader = mesh.Material.Shader;
            if (shader == "default")
                shader = "matte";

            surface.SetUniform("inTime", new float[] { Exec, ExecDelta, Sim, SimDelta });

            surface.SetUniform("inBGAddLight", new float[] { 0f, 0f, 0f, 0f });
            surface.SetUniform("inBGEnvParams", new float[] { 1f, 0f, 0f, 0f }); // Env Scale, unused x 3
            surface.SetUniform("inShipExps", new float[] { 1f, 1f, 1f, 1f }); // br, bR, Br, BR
            surface.SetUniform("inBackExps", new float[] { 1f, 1f, 1f, 1f }); // br, bR, Br, BR

            surface.SetUniform("inSOBParams", new float[] { SOBAlpha, SOBCloak, SOBClip, 0f });    // Alpha, Cloak, Clip, unused
            surface.SetUniform("inLifeParams", new float[] { LifeAlpha, DeathRatio, 0f, 0f });     // Life Alpha, Death Ratio, unused x2
            surface.SetUniform("inFogColor", FogColor);
            surface.SetUniform("inFogWindow", new float[] { 0f, 0f, 10000f, 1f }); // Near, Min, Far, Max

            surface.SetUniform("inShadowTrans", new float[] {
                0f, 0f, 0f, 0f, // keylight Trans
                0f, 0f, 0f, 0f, // keylight Scale
                0f, 0f, 0f, 0f, // filllight Trans
                0f, 0f, 0f, 0f // filllight Scale
            });

            //Manifest.Globals["scarInfo"] = new float[] { 0, 0, 0, 0 };   // SOB_USECLIP

            float[] shiplights;
            int shiplight_count = GetShipLights(out shiplights);
            float[] corelights = GetCoreLights();
            surface.SetUniform("inLightCounts", new int[] { shiplight_count, 7 });
            surface.SetUniform("inLightShip", shiplights);
            surface.SetUniform("inLightCore", corelights);

            float[] fxInfo = new float[] {
                0f, 0f, 0f, 0f, // R, G, B, A
                0f, 0f, 0f, 0f, // Alpha/Add, Scale, 0, 0
            };
            surface.SetUniform("inFXInfo", fxInfo);

            //Manifest.Globals["clipPlane"] = new float[] { 0, 0, -1, ClipDistance };   // SOB_USECLIP

            surface.SetUniform("inGammaScale", new float[] { 0.8625f, 0.8625f, 0.8625f, 0.95f });

            //Not manifest globals, but still the same across all surfaces.
            Matrix4 camera = Program.Camera.GetViewMatrix();
            Matrix4 projection = Matrix4.Identity;
            if (!Program.Camera.Orthographic)
                projection = Matrix4.CreatePerspectiveFieldOfView(Program.Camera.FieldOfView, (float)Program.GLControl.Width / (float)Program.GLControl.Height, Program.Camera.NearClipDistance, Program.Camera.ClipDistance);
            else
                projection = Matrix4.CreateOrthographic((float)(Program.GLControl.Width / Program.Camera.OrthographicSize), (float)(Program.GLControl.Height / Program.Camera.OrthographicSize), Program.Camera.NearClipDistance, Program.Camera.ClipDistance);

            surface.SetUniform("camera", camera);
            surface.SetUniform("projection", projection);


            // ##### Surface uniforms #####

            surface.SetUniform("modelview", mesh.GlobalWorldMatrix);


            if (ManifestConfig.GetValue("CFG_Shadow_Quality") >= 1)
            {
                Matrix4 mat_keylight = Matrix4.Identity;
                surface.SetUniform("inMatKL", mat_keylight);

                if (ManifestConfig.GetValue("CFG_Shadow_Quality") >= 3)
                {
                    Matrix4 mat_altlight = Matrix4.Identity;
                    surface.SetUniform("inMatFL", mat_altlight);
                }
            }

            if (SOB_BADGE(shader))
            {
                surface.BindTexture("inTexBadge", BadgeTexture.ID);
            }

            if (SOB_THRUSTERS(shader))
            {
                surface.BindTexture("inTexDiffOn", mesh.Material.DiffuseTexture.ID);
                surface.BindTexture("inTexGlowOn", mesh.Material.GlowTexture.ID);
                surface.BindTexture("inTexDiffOff", mesh.Material.DiffuseOffTexture.ID);
                surface.BindTexture("inTexGlowOff", mesh.Material.GlowOffTexture.ID);
                surface.SetUniform("inColEngine", new float[] { ThrusterInterpolation, 0, 0, 0 });
            }
            else
            {
                if (shader != "fx_eng_glowbasic")
                {
                    surface.BindTexture("SOB_diffuse", mesh.Material.DiffuseTexture.ID);
                    if(mesh.Material.GlowTexture != null)
                        surface.BindTexture("SOB_glow", mesh.Material.GlowTexture.ID);
                    if (SOB_GLOWRGB(shader))
                        surface.BindTexture("inTexSpec", mesh.Material.SpecularTexture.ID);
                }
            }

            if (SOB_RESOURCE(shader))
            {
                surface.BindTexture("inTexProgress", mesh.Material.ProgressTexture.ID);
                surface.SetUniform("inFadeInfo", new float[] { Progress, 0f });
                surface.SetUniform("inFadeWindow", new float[] { 0.2f, 0.0f, 1.0f }); // Blend range, low progress, high progress
                surface.SetUniform("inGlowStyle", new float[] { 1f, 0f, 0f, 0f }); // Fade Delta, Fade Burn, unused x2

                if (SOB_DUALINPUT(shader)) // SOB_DUALINPUT
                {
                    // Grid - Scale UV for 0, Offset UV for 1
                    surface.SetUniform("inGridDiff", new float[] { 0.5f, 1f, 0.5f, 0f });
                    surface.SetUniform("inGridGlow", new float[] { 0.5f, 1f, 0.5f, 0f });
                    surface.SetUniform("inGridSpec", new float[] { 0.5f, 1f, 0.5f, 0f });
                    surface.SetUniform("inGridNorm", new float[] { 0.5f, 1f, 0.5f, 0f });

                    surface.SetUniform("inMulDiff0", new float[] { 1f, 1f, 1f, 1f });
                    surface.SetUniform("inMulDiff1", new float[] { 1f, 1f, 1f, 1f });
                    surface.SetUniform("inMulGlow0", new float[] { 1f, 1f, 1f, 1f });
                    surface.SetUniform("inMulGlow1", new float[] { 1f, 1f, 1f, 1f });
                    surface.SetUniform("inMulSpec0", new float[] { 1f, 1f, 1f, 1f });
                    surface.SetUniform("inMulSpec1", new float[] { 1f, 1f, 1f, 1f });
                }

                if (SOB_DEBRIS(shader))
                {
                    //uniform vec4 inFXInfo[2];
                }
            }

            if (SOB_TEAMTEX(shader))
            {
                surface.BindTexture("inTexTeam", mesh.Material.TeamTexture.ID);
            }

            if (shader != "fx_eng_glowbasic")
                if(mesh.Material.NormalTexture != null)
                    surface.BindTexture("inTexNorm", mesh.Material.NormalTexture.ID);

            if (!SOB_BAYLIGHT(shader))
            {
                surface.BindTexture("inTexEnv0", BackgroundTexture.ID);
                surface.BindTexture("inTexEnv1", BackgroundTexture.ID);
            }

            if (SOB_TEAM(shader))
            {
                surface.SetUniform("inColTeam", TeamColor);
                surface.SetUniform("inColStripe", StripeColor);
            }

            if (SOB_GLOWCOL(shader)) //For engine glows
            {
                surface.SetUniform("inColGlow", new float[] { (float)EngineGlowColor.R / 255, (float)EngineGlowColor.G / 255, (float)EngineGlowColor.B / 255, ThrusterInterpolation * ((float)EngineGlowColor.A / 255) } );
            }

            if (!SOB_DEBRIS(shader))
                surface.SetUniform("inColEffect", new float[] { 0.5f, 0.5f, 0.5f, 0f });

            surface.SetUniform("inSurfDiff", new float[] { 0f, SurfaceDiff.Fren, 0f, 0f });
            surface.SetUniform("inSurfGlow", new float[] { SurfaceGlow.Power, SurfaceGlow.Fren, 0f, 0f });
            surface.SetUniform("inSurfSpec", new float[] { SurfaceSpec.Power, SurfaceSpec.Fren, 0f, 0f });
            surface.SetUniform("inSurfPaint", new float[] { SurfacePaint.Curve, SurfacePaint.Scale, SurfacePaint.Bias, SurfacePaint.Dim });
            surface.SetUniform("inSurfGloss", new float[] { SurfaceGloss.Curve, SurfaceGloss.Scale, SurfaceGloss.Bias, 0f });
            surface.SetUniform("inSurfRefl", new float[] { SurfaceRefl.Power, SurfaceRefl.Fren, SurfaceRefl.AddMix, 0f });
            surface.SetUniform("inSurfFren", new float[] { SurfaceFren.Power, SurfaceFren.Bias, SurfaceFren.Curve, 0f });
            surface.SetUniform("inSurfPeak", new float[] { SurfacePeak.Base, SurfacePeak.Paint, SurfacePeak.Fren, SurfacePeak.Scar });


            if (HACK_AllIFeelIsPain)
                surface.SetUniform("inPaintStyle", new float[] { 1.0f, 12.0f, 5.0f, 0f });
            else
                surface.SetUniform("inPaintStyle", new float[] { PaintStyleCurve, PaintStyleScale, PaintStyleOffset, 0f });

            if (SOB_BAYLIGHT(shader))
                surface.SetUniform("inBayExps", new float[] { 1f, 0.99f, 0.95f, 0.94f });

            if (shader == "shipAnim")
            {
                surface.SetUniform("inAnimParams", new float[] { 1f, 1f, 1f, 0f });
            }
        }

        private static int DrawHWMesh(HWMesh mesh, int index)
        {
            if (mesh.Visible && mesh.Material != null)
            {
                GetError("Pre DrawHWMesh");

                HWTexture texture = mesh.Material.DiffuseTexture;

                string shader = mesh.Material.Shader;
                if (shader == "default")
                    shader = "matte";

                Surface surface = ShaderManifest.GetSurface(shader);

                //load vertex buffers
                surface.LinkAttrib("inPos", mesh_pos_buffer, 3, false);
                surface.LinkAttrib("inNorm", mesh_nrm_buffer, 3, false);
                surface.LinkAttrib("inTan", mesh_tan_buffer, 3, false);
                surface.LinkAttrib("inBiNorm", mesh_bin_buffer, 3, false);
                surface.LinkAttrib("inUV0", mesh_uv0_buffer, 2, false);
                surface.LinkAttrib("inUV1", mesh_uv1_buffer, 2, false);
                //surface.LinkAttrib(mesh_uv2_buffer, "inUV2", 2, false);

                // Draw
                UpdateSurface(surface, mesh);

                GL.BindBuffer(BufferTarget.ElementArrayBuffer, mesh_ind_buffer);
                surface.Draw(BeginMode.Triangles, mesh.IndexCount, DrawElementsType.UnsignedInt, index * sizeof(uint));

                GetError("Post DrawHWMesh");
            }

            return mesh.IndexCount;
        }

        private static int DrawEditorMesh(EditorMesh mesh, int index)
        {
            if (mesh.Visible)
            {
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);

                if (mesh.Wireframe)
                    GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);

                Matrix4 model = mesh.GlobalWorldMatrix;
                Matrix4 camera = Program.Camera.GetViewMatrix();
                Matrix4 projection = Matrix4.Identity;
                if (!Program.Camera.Orthographic)
                    projection = Matrix4.CreatePerspectiveFieldOfView(Program.Camera.FieldOfView, (float)Program.GLControl.Width / (float)Program.GLControl.Height, Program.Camera.NearClipDistance, Program.Camera.ClipDistance);
                else
                    projection = Matrix4.CreateOrthographic((float)(Program.GLControl.Width / Program.Camera.OrthographicSize), (float)(Program.GLControl.Height / Program.Camera.OrthographicSize), Program.Camera.NearClipDistance, Program.Camera.ClipDistance);

                GL.UniformMatrix4(editor_shader.GetUniform("inMatM"), false, ref model);
                GL.UniformMatrix4(editor_shader.GetUniform("inMatV"), false, ref camera);
                GL.UniformMatrix4(editor_shader.GetUniform("inMatP"), false, ref projection);

                HWTexture texture = null;
                if (mesh.Material != null)
                {
                    texture = mesh.Material.DiffuseTexture;
                    Vector4 diffuse = new Vector4(mesh.Material.DiffuseColor, mesh.Material.Opacity);

                    GL.Uniform4(editor_shader.GetUniform("matDiffuse"), ref diffuse);
                }
                else
                {
                    GL.Uniform4(editor_shader.GetUniform("matDiffuse"), 1f, 1f, 1f, 1f);
                }

                if (texture != null)
                {
                    GL.ActiveTexture(TextureUnit.Texture0);
                    GL.BindTexture(TextureTarget.Texture2D, texture.ID);
                    GL.Uniform1(editor_shader.GetUniform("inTexMat"), 0);
                    GL.Uniform1(editor_shader.GetUniform("isTextured"), 1); //Tell shader to use texture colors
                }
                else
                {
                    GL.Uniform1(editor_shader.GetUniform("isTextured"), 0); //Tell shader to use vertex colors
                }

                if (mesh.Shaded)
                {
                    //GL.Uniform4(CurrentShader.GetUniform("shaded", 1); //Tell shader to calculate lighting

                    if (mesh.Material != null)
                    {
                        Vector4 specular = new Vector4(mesh.Material.SpecularColor, mesh.Material.SpecularExponent);
                        GL.Uniform4(editor_shader.GetUniform("matSpecular"), ref specular);
                    }
                }
                //else
                //    GL.Uniform4(CurrentShader.GetUniform("shaded", 0); //Tell shader not to calculate lighting

                if (mesh.VertexColored)
                    GL.Uniform1(editor_shader.GetUniform("vertexColored"), 1);
                else
                    GL.Uniform1(editor_shader.GetUniform("vertexColored"), 0);

                if (mesh.BlackIsTransparent)
                    GL.Uniform1(editor_shader.GetUniform("blackIsTransparent"), 1);
                else
                    GL.Uniform1(editor_shader.GetUniform("blackIsTransparent"), 0);

                if (mesh.GetType() == typeof(EditorLine))
                    GL.DrawElements(BeginMode.Lines, mesh.IndexCount, DrawElementsType.UnsignedInt, index * sizeof(int));
                else
                    GL.DrawElements(BeginMode.Triangles, mesh.IndexCount, DrawElementsType.UnsignedInt, index * sizeof(int));
            }
            return mesh.IndexCount;
        }

        public static void Resize()
        {
            GL.Viewport(Program.GLControl.ClientRectangle.X, Program.GLControl.ClientRectangle.Y, Program.GLControl.ClientRectangle.Width, Program.GLControl.ClientRectangle.Height);

            Matrix4 projection = Matrix4.Identity;
            if (!Program.Camera.Orthographic)
                projection = Matrix4.CreatePerspectiveFieldOfView(Program.Camera.FieldOfView, (float)Program.GLControl.Width / (float)Program.GLControl.Height, Program.Camera.NearClipDistance, Program.Camera.ClipDistance);
            else
                projection = Matrix4.CreateOrthographic((float)(Program.GLControl.Width / Program.Camera.OrthographicSize), (float)(Program.GLControl.Height / Program.Camera.OrthographicSize), Program.Camera.NearClipDistance, Program.Camera.ClipDistance);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);
        }

        public static void Invalidate()
        {
            Program.GLControl.Invalidate();
        }

        public static void InvalidateView()
        {
            ViewInvalid = true;
        }

        public static void InvalidateMeshData()
        {
            MeshDataInvalid = true;
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
                shader == "badge_glow" ||
                shader == "badgeglow";
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
                shader == "badgeglow" ||
                shader == "ore" ||
                shader == "salvage" ||
                shader == "ship_glow" ||
                shader == "shipglow";
        }

        private static bool SOB_GLOWCOL(string shader)
        {
            return
                shader == "fx_eng_glowbasic";
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
                shader == "badgeglow" ||
                shader == "bay" ||
                shader == "ship" ||
                shader == "ship_glow" ||
                shader == "shipglow" ||
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

        private static int GetShipLights(out float[] shiplights)
        {
            int shipLight = 0, shipLightCount = 0;
            shiplights = new float[96 * 4];
            foreach (Light l in Light.Lights)
            {
                if (l != AmbientLight && l.Enabled && l.Color.Length != 0)
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
                    shipLightCount++;
                    if (shipLightCount == 32) break;
                }
            }
            return shipLightCount;
        }

        private static float[] GetCoreLights()
        {
            Vector3 keyLightVec = Program.Camera.Position; // new Vector3(1000f, 300f, -100f);
            Vector3 fillLightVec = new Vector3(keyLightVec.X * 1.05f, keyLightVec.Y * 1.2f, keyLightVec.Z * 1.05f);
            fillLightVec += new Vector3(keyLightVec.Length * 0.1f);

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
            corelights[4 * 2 + 0] = 100 / 255f;
            corelights[4 * 2 + 1] = 100 / 255f;
            corelights[4 * 2 + 2] = 100 / 255f;

            // Key light specular color
            corelights[4 * 3 + 0] = 100 / 255f;
            corelights[4 * 3 + 1] = 100 / 255f;
            corelights[4 * 3 + 2] = 100 / 255f;

            // Fill light vector
            corelights[4 * 4 + 0] = fillLightVec.X;
            corelights[4 * 4 + 1] = fillLightVec.Y;
            corelights[4 * 4 + 2] = fillLightVec.Z;

            // Fill light diffuse color
            corelights[4 * 5 + 0] = 100 / 255f;
            corelights[4 * 5 + 1] = 100 / 255f;
            corelights[4 * 5 + 2] = 100 / 255f;

            // Fill light specular color
            corelights[4 * 6 + 0] = 100 / 255f;
            corelights[4 * 6 + 1] = 100 / 255f;
            corelights[4 * 6 + 2] = 100 / 255f;

            return corelights;
        }
    }
}