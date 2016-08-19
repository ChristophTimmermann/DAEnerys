using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using Assimp;
using NewShaderManifest;

namespace DAEnerys
{

    static class Renderer
    {
        static Shader editor_shader;

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

        public static Color TeamColor { get; set; } = Color.FromArgb(255, 92, 139, 170);
        public static Color StripeColor { get; set; } = Color.FromArgb(255, 204, 204, 204);

        public static Color EngineGlowColor { get; set; } = Color.FromArgb(64, 69, 120, 176);

        // VARIABLE SHADER INPUTS
        public static float Exec { get; set; } = 0f;
        public static float ExecDelta { get; set; } = 0f;
        public static float Sim { get; set; } = 0f;
        public static float SimDelta { get; set; } = 0f;

        public static float MinClipDistance { get; set; } = -1000f;
        public static float MaxClipDistance { get; set; } = 1000f;
        public static float ClipDistance { get; set; } = 1000f;

        public static float SOBAlpha { get; set; } = 0.3f;
        public static float SOBCloak { get; set; } = 0f;
        public static float SOBClip { get; set; } = 0f;

        public static bool HACK_SpecialSauce
        {
            get
            {
                return ManifestConfig.Options["HACK_SpecialSauce"].Value == 1;
            }
            set
            {
                ManifestConfig.Options["HACK_SpecialSauce"].Value = value ? 1 : 0;
            }
        }

        public static bool HACK_AllIFeelIsPain
        {
            get
            {
                return ManifestConfig.Options["HACK_AllIFeelIsPain"].Value == 1;
            }
            set
            {
                ManifestConfig.Options["HACK_AllIFeelIsPain"].Value = value ? 1 : 0;
            }
        }

        public static bool CFG_Patch_AltHyper
        {
            get
            {
                return ManifestConfig.Options["CFG_Patch_AltHyper"].Value == 1;
            }
            set
            {
                ManifestConfig.Options["CFG_Patch_AltHyper"].Value = value ? 1 : 0;
            }
        }

        public static float PaintCurve { get; set; } = 0f;
        public static float PaintScale { get; set; } = 2f;
        public static float PaintOffset { get; set; } = 0f;


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

        public static float ThrusterInterpolation = 1;

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
            Manifest.Init(HWData.DataPaths);
            editor_shader = new Shader("editor.vs", "editor.fs", true);

            //AmbientLight.Enabled = false;
            DefaultTexture = new HWTexture(Path.Combine(Program.EXECUTABLE_PATH, @"resources/missing.tga"));

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
            Manifest.ReloadManifest();
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

        public static void UpdateMeshData()
        {
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
                //List<HWMesh> hwMeshList = new List<HWMesh>();

                //foreach (HWMesh mesh in HWScene.Meshes)
                //{
                //    if (!mesh.Translucent)
                //        hwMeshList.Add(mesh);
                //}
                //foreach (HWMesh mesh in HWScene.Meshes)
                //{
                //    if (mesh.Translucent)
                //        hwMeshList.Add(mesh);
                //}
                //HWScene.Meshes = hwMeshList;

                foreach (HWMesh mesh in HWScene.Meshes)
                {
                    //if (mesh.Visible)
                    //{
                        foreach (HWVertex vtx in mesh.Vertices)
                        {
                            mesh_verts.Add(vtx.Position);
                            mesh_normals.Add(vtx.Normal);
                            mesh_tangents.Add(vtx.Tangent);
                            mesh_bitangents.Add(vtx.Binormal);
                            mesh_uv0.Add(vtx.UV0);
                            mesh_uv1.Add(vtx.UV1);
                        }
                        mesh_inds.AddRange(mesh.GetIndices(mesh_vertcount).ToList());

                        mesh_vertcount += mesh.VertexCount;
                    //}
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
                GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(indicedata.Length * sizeof(int)), indicedata, BufferUsageHint.StaticDraw);
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
                    if (mesh.Visible)
                    {
                        editor_verts.AddRange(mesh.Vertices);
                        editor_uv0.AddRange(mesh.TextureCoords);
                        editor_colors.AddRange(mesh.Colors);
                        editor_inds.AddRange(mesh.GetIndices(editor_vertcount).ToList());
                        editor_vertcount += mesh.VertexCount;
                    }
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
                GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(indicedata.Length * sizeof(int)), indicedata, BufferUsageHint.StaticDraw);
                GetError("Editor Buffering");
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

            int indiceat = 0;

            foreach (HWMesh mesh in HWScene.Meshes)
                    indiceat += DrawHWMesh(mesh, indiceat);

            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.DepthTest);

            GL.DepthMask(true);
            
            GL.UseProgram(editor_shader.ProgramID);
            editor_shader.LinkAttrib3(editor_pos_buffer, "inPos", false);
            editor_shader.LinkAttrib3(editor_col_buffer, "inColor", false);
            editor_shader.LinkAttrib2(editor_uv0_buffer, "inUV0", false);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, editor_ind_buffer);

            GL.Enable(EnableCap.AlphaTest);

            int editor_indiceat = 0;
            foreach (EditorMesh mesh in EditorScene.meshes)
            {
                if (mesh.Visible && mesh.NeverDrawInFront)
                    editor_indiceat += DrawEditorMesh(mesh, editor_indiceat);
            }

            if (DrawVisualizationsInFront)
                GL.Clear(ClearBufferMask.DepthBufferBit);

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

        private static void AttachTexture(Surface surface, string name, HWTexture tex)
        {
            if (tex != null)
                surface.AssignTexture(name, tex.Path, tex.ID);
            else
                surface.AssignTexture(name, "", 0);
        }

        private static void AttachTexture(Surface surface, string name, string path, int id)
        {
            surface.AssignTexture(name, path, id);
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


                Surface surface = Manifest.UseSurface(shader.ToLower());
                surface.Use();

                //load vertex buffers
                surface.LinkAttrib(mesh_pos_buffer, "inPos", 3, false);
                surface.LinkAttrib(mesh_nrm_buffer, "inNorm", 3, false);
                surface.LinkAttrib(mesh_tan_buffer, "inTan", 3, false);
                surface.LinkAttrib(mesh_bin_buffer, "inBiNorm", 3, false);
                surface.LinkAttrib(mesh_uv0_buffer, "inUV0", 2, false);
                surface.LinkAttrib(mesh_uv1_buffer, "inUV1", 2, false);
                //surface.LinkAttrib(mesh_uv2_buffer, "inUV2", 2, false);

                // load global vars
                float[] shiplights;
                int shiplight_count = GetShipLights(out shiplights);

                Matrix4 model = mesh.ModelMatrix;
                Matrix4 camera = Program.Camera.GetViewMatrix();
                Matrix4 projection = Matrix4.Identity;
                if (!Program.Camera.Orthographic)
                    projection = Matrix4.CreatePerspectiveFieldOfView(Program.Camera.FieldOfView, (float)Program.GLControl.Width / (float)Program.GLControl.Height, Program.Camera.NearClipDistance, Program.Camera.ClipDistance);
                else
                    projection = Matrix4.CreateOrthographic((float)(Program.GLControl.Width / Program.Camera.OrthographicSize), (float)(Program.GLControl.Height / Program.Camera.OrthographicSize), Program.Camera.NearClipDistance, Program.Camera.ClipDistance);

                Matrix4 mat_keylight = Matrix4.Identity;
                Matrix4 mat_altlight = Matrix4.Identity;

                surface["inLightShip[0]"] = shiplights;
                surface["inLightCore[0]"] = GetCoreLights();
                Manifest.Globals["timeTable"] = new float[] { Exec, ExecDelta, Sim, SimDelta };
                Manifest.Globals["lightCounts"] = new int[] { shiplight_count, 7 };
                Manifest.Globals["gammaScale"] = new float[] { 0.8625f, 0.8625f, 0.8625f, 0.95f };
                //Manifest.Globals["bgAddLight"] = new float[] { 0f, 0f, 0f, 0f };
                //Manifest.Globals["bgEnvParams"] = new float[] { 0f, 0f, 0f, 0f }; // Env Scale, unused x 3
                //Manifest.Globals["bgShipExps"] = new float[] { 1f, 1f, 1f, 1f };

                //float[] clipPlane = Manifest.Globals.Get("clipPlane");
                //ClipDistance = HyperspaceEffect.Effect.Position.Z;
                //Manifest.Globals["clipPlane", new float[] { clipPlane[0], clipPlane[1], clipPlane[2], ClipDistance });   // SOB_USECLIP
                Manifest.Globals["clipPlane"] = new float[] { 0, 0, -1, ClipDistance };   // SOB_USECLIP
                Manifest.Globals["sobParams"] = new float[] { SOBAlpha, SOBCloak, SOBClip, 0f };      // Alpha, Cloak, Clip, unused
                                                                                                      //Manifest.Globals["lifeParams"] = new float[] { 1f, 1f, 0f, 0f };     // Life Alpha, Death Ratio, unused x2

                //Manifest.Globals["fogColor"] = new float[] { 0f, 0f, 0f, 0f };       // R, G, B, A
                //Manifest.Globals["fogWindow"] = new float[] { 10f, 0f, 0f, 0f };     // Near, Min, Far, Max

                if (ManifestConfig.Options["CFG_Shadow_Quality"].Value >= 1)
                {
                    // Light windowing for shadows - Trans/Scale key, Trans/Scale fill
                    Manifest.Globals["shadowTrans"] = new float[] { 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f };
                }

                Manifest.Globals["modelview"] = model;
                Manifest.Globals["camera"] = camera;
                Manifest.Globals["projection"] = projection;

                if (ManifestConfig.Options["CFG_Shadow_Quality"].Value >= 1)
                    Manifest.Globals["keylight"] = mat_keylight;

                if (ManifestConfig.Options["CFG_Shadow_Quality"].Value >= 3)
                    Manifest.Globals["keylight"] = mat_altlight;

                // Vertex Shader
                if (SOB_RESOURCE(shader))
                {
                    surface["inFadeInfo"] = new float[] { 0f, 0f };
                    if (SOB_DUALINPUT(shader))
                    {
                        surface["inGridDiff"] = new float[] { 1f, 1f, 0f, 0f };
                        surface["inGridGlow"] = new float[] { 1f, 1f, 0f, 0f };
                        surface["inGridSpec"] = new float[] { 1f, 1f, 0f, 0f };
                        surface["inGridNorm"] = new float[] { 1f, 1f, 0f, 0f };
                    }
                }

                // Fragment Shader
                if (ManifestConfig.Options["CFG_Shadow_Quality"].Value >= 1)
                {
                    AttachTexture(surface, "inTexShadow", null);
                }

                if (SOB_BADGE(shader))
                {
                    AttachTexture(surface, "SOB_badge", BadgeTexture);
                }

                if (SOB_RESOURCE(shader))
                {
                    //AttachTexture(surface, "inTexProgress", mesh.Material.???);
                    AttachTexture(surface, "SOB_glow", mesh.Material.GlowTexture);
                    AttachTexture(surface, "SOB_spec", mesh.Material.SpecularTexture);
                    //surface["inFadeWindow", new float[] { 0.1f, 0.1f, 0.9f });
                    //surface["inGlowStyle", new float[] { 1f, 0f, 0f, 0f });

                    if (SOB_DUALINPUT(shader)) // SOB_DUALINPUT
                    {
                        //surface["inMulDiff0", new float[] { 1f, 1f, 1f, 1f });
                        //surface["inMulDiff1", new float[] { 1f, 1f, 1f, 1f });
                        //surface["inMulGlow0", new float[] { 1f, 1f, 1f, 1f });
                        //surface["inMulGlow1", new float[] { 1f, 1f, 1f, 1f });
                        //surface["inMulSpec0", new float[] { 1f, 1f, 1f, 1f });
                        //surface["inMulSpec1", new float[] { 1f, 1f, 1f, 1f });
                    }

                    if (SOB_DEBRIS(shader))
                    {
                        //uniform vec4 inFXInfo[2];
                    }
                }
                else
                {
                    if (SOB_THRUSTERS(shader))
                    {
                        AttachTexture(surface, "SOB_diffuseOn", mesh.Material.DiffuseTexture);
                        AttachTexture(surface, "SOB_glowOn", mesh.Material.GlowTexture);
                        AttachTexture(surface, "SOB_diffuseOff", mesh.Material.DiffuseOffTexture);
                        AttachTexture(surface, "SOB_glowOff", mesh.Material.GlowOffTexture);
                        surface["SOB_engine"] = new float[] { ThrusterInterpolation, 0, 0, 0 };
                    }
                    else
                    {
                        AttachTexture(surface, "SOB_diffuse", mesh.Material.DiffuseTexture);
                        AttachTexture(surface, "SOB_glow", mesh.Material.GlowTexture);
                        if (SOB_GLOWRGB(shader))
                        {
                            AttachTexture(surface, "SOB_spec", mesh.Material.SpecularTexture);
                        }
                    }
                }

                if (SOB_TEAMTEX(shader))
                {
                    if (mesh.Material.TeamTexture != null)
                        AttachTexture(surface, "SOB_team", mesh.Material.TeamTexture);
                }

                AttachTexture(surface, "SOB_normal", mesh.Material.NormalTexture);
                AttachTexture(surface, "inTexEnv0", BlackTexture);
                AttachTexture(surface, "inTexEnv1", BlackTexture);

                if (SOB_TEAM(shader))
                {
                    surface["SOB_teamCol"] = new float[] { TeamColor.R / 255f, TeamColor.G / 255f, TeamColor.B / 255f, TeamColor.A / 255f };
                    surface["SOB_stripeCol"] = new float[] { StripeColor.R / 255f, StripeColor.G / 255f, StripeColor.B / 255f, StripeColor.A / 255f };
                }

                if (SOB_GLOWCOL(shader)) //For engine glows
                {
                    surface["SOB_glowCol"] = new float[] { EngineGlowColor.R / 255f, EngineGlowColor.G / 255f, EngineGlowColor.B / 255f, EngineGlowColor.A / 255f * ThrusterInterpolation };
                }

                if (!SOB_DEBRIS(shader))
                    surface["SOB_uieffect"] = new float[] { 0.5f, 0.5f, 0.5f, 0f };

                //surface["inSurfDiff"] = new float[] { 0f, 0f, 0f, 0f };
                if (shader == "thruster")
                    surface["SOB_surfGlow"] = new float[] { 1.1f, 0.5f, 0f, 0f };
                else
                    surface["SOB_surfGlow"] = new float[] { 1.5f, 0.5f, 0f, 0f };
                //surface["inSurfSpec"] = new float[] { 0f, 0f, 0f, 0f };
                //surface["inSurfGloss"] = new float[] { 0f, 0f, 0f, 0f };
                //surface["inSurfRefl"] = new float[] { 0f, 0f, 0f, 0f };
                //surface["inSurfFren"] = new float[] { 0f, 0f, 0f, 0f };
                //surface["inSurfPaint"] = new float[] { 0f, 0f, 0f, 0f };
                //surface["inSurfPeak"] = new float[] { 0f, 0f, 0f, 0f };

                //surface["inSurfSpec"] = new float[] { 1f, 1.5f, 0f, 0f };
                //surface["inSurfFren"] = new float[] { 3.8f, 1.1f, 2.1f, 0f };
                //surface["inSurfPaint"] = new float[] { 1.8f, -115f, -25f, 0.88f };
                //surface["inSurfPeak"] = new float[] { 0.008f, 0.0035f, 0.5f, 2f };

                if (HACK_AllIFeelIsPain)
                    surface["inPaintStyle"] = new float[] { 1.0f, 12.0f, 5.0f, 0f };
                else
                    surface["inPaintStyle"] = new float[] { PaintCurve, PaintScale, PaintOffset, 0f };

                if (SOB_BAYLIGHT(shader))
                    surface["inBayExps"] = new float[] { 1f, 0.99f, 0.95f, 0.94f };

                // Draw
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, mesh_ind_buffer);
                surface.Draw(BeginMode.Triangles, mesh.IndiceCount, DrawElementsType.UnsignedInt, index * sizeof(uint));
                
                GetError("Post DrawHWMesh");
            }

            return mesh.IndiceCount;
        }

        private static int DrawEditorMesh(EditorMesh mesh, int index)
        {
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);

            if (mesh.Wireframe)
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);

            Matrix4 model = mesh.ModelMatrix;
            Matrix4 camera = Program.Camera.GetViewMatrix();
            Matrix4 projection = Matrix4.Identity;
            if (!Program.Camera.Orthographic)
                projection = Matrix4.CreatePerspectiveFieldOfView(Program.Camera.FieldOfView, (float)Program.GLControl.Width / (float)Program.GLControl.Height, Program.Camera.NearClipDistance, Program.Camera.ClipDistance);
            else
                projection = Matrix4.CreateOrthographic((float)(Program.GLControl.Width / Program.Camera.OrthographicSize), (float)(Program.GLControl.Height / Program.Camera.OrthographicSize), Program.Camera.NearClipDistance, Program.Camera.ClipDistance);

            GetError("OpenTK Rendering");
            GL.UniformMatrix4(editor_shader.GetUniform("inMatM"), false, ref model);
            GetError("OpenTK Rendering");
            GL.UniformMatrix4(editor_shader.GetUniform("inMatV"), false, ref camera);
            GetError("OpenTK Rendering");
            GL.UniformMatrix4(editor_shader.GetUniform("inMatP"), false, ref projection);

            GetError("OpenTK Rendering");
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
            {
                GL.Uniform1(editor_shader.GetUniform("blackIsTransparent"), 1);
            }
            else
                GL.Uniform1(editor_shader.GetUniform("blackIsTransparent"), 0);

            if (mesh.GetType() == typeof(EditorLine))
                GL.DrawElements(BeginMode.Lines, mesh.IndiceCount, DrawElementsType.UnsignedInt, index * sizeof(int));
            else
                GL.DrawElements(BeginMode.Triangles, mesh.IndiceCount, DrawElementsType.UnsignedInt, index * sizeof(int));

            return mesh.IndiceCount;
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

        private static bool SOB_GLOWCOL(string shader) //Not sure if this is right, it's for engine glows
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

        private static int GetShipLights(out float[] shiplights)
        {
            int shipLight = 0;
            shiplights = new float[96 * 4];
            foreach (Light l in Light.Lights)
            {
                if (l != AmbientLight && l.Enabled)
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
            return shipLight / 3;
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
            corelights[4 * 2 + 0] = 1f;
            corelights[4 * 2 + 1] = 1f;
            corelights[4 * 2 + 2] = 1f;

            // Key light specular color
            corelights[4 * 3 + 0] = 1f;
            corelights[4 * 3 + 1] = 1f;
            corelights[4 * 3 + 2] = 1f;

            // Fill light vector
            corelights[4 * 4 + 0] = fillLightVec.X;
            corelights[4 * 4 + 1] = fillLightVec.Y;
            corelights[4 * 4 + 2] = fillLightVec.Z;

            // Fill light diffuse color
            corelights[4 * 5 + 0] = 1f;
            corelights[4 * 5 + 1] = 1f;
            corelights[4 * 5 + 2] = 1f;

            // Fill light specular color
            corelights[4 * 6 + 0] = 1f;
            corelights[4 * 6 + 1] = 1f;
            corelights[4 * 6 + 2] = 1f;

            return corelights;
        }
    }
}