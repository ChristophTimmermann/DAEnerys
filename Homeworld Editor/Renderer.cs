using OpenTK;
using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Linq;
using Assimp;

namespace HomeworldDAEEditor
{
    static class Renderer
    {
        static Dictionary<string, Shader> shaders = new Dictionary<string, Shader>();

        public static HWTexture DefaultTexture;

        public static Light AmbientLight = new Light(new Vector4(0, 0, 0, 0), new Vector3(0.5f), 1, 0.000005f);

        private static Color backgroundColor = Color.CornflowerBlue;
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

        public static float ThrusterInterpolation = 1;

        static string activeShader;

        static int ibo_elements;

        static Vector3[] vertdata;
        static Vector3[] coldata;
        static Vector2[] texcoorddata;
        static int[] indicedata;
        static Vector3[] normdata;

        public static Matrix4 View = Matrix4.Identity;

        public static void Init()
        {
            GL.ClearColor(BackgroundColor);

            GL.Enable(EnableCap.DepthTest);

            GL.Enable(EnableCap.Blend);
            //GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            GL.LineWidth(2);

            GL.GenBuffers(1, out ibo_elements);

            // Load shaders from file
            shaders.Add("textured", new Shader("vs_tex.glsl", "fs_tex.glsl", true));
            shaders.Add("normal", new Shader("vs_norm.glsl", "fs_norm.glsl", true));
            shaders.Add("lit", new Shader("vs_lit.glsl", "fs_lit.glsl", true));

            activeShader = "lit";

            DefaultTexture = new HWTexture(@"resources/missing.tga");
        }

        public static void UpdateMeshData()
        {
            List<Vector3> verts = new List<Vector3>();
            List<int> inds = new List<int>();
            List<Vector3> colors = new List<Vector3>();
            List<Vector2> texcoords = new List<Vector2>();
            List<Vector3> normals = new List<Vector3>();

            // Assemble vertex and indice data for all volumes
            int vertcount = 0;
            foreach (HWMesh mesh in HWScene.Meshes)
            {
                if (mesh.Visible)
                {
                    verts.AddRange(mesh.GetVertices().ToList());
                    inds.AddRange(mesh.GetIndices(vertcount).ToList());
                    colors.AddRange(mesh.GetColorData().ToList());
                    texcoords.AddRange(mesh.GetTextureCoords());
                    normals.AddRange(mesh.GetNormals().ToList());
                    vertcount += mesh.VertexCount;
                }
            }

            foreach (EditorMesh mesh in EditorScene.meshes)
            {
                if (mesh.Visible)
                {
                    verts.AddRange(mesh.GetVertices().ToList());
                    inds.AddRange(mesh.GetIndices(vertcount).ToList());
                    colors.AddRange(mesh.GetColorData().ToList());
                    texcoords.AddRange(mesh.GetTextureCoords());
                    normals.AddRange(mesh.GetNormals().ToList());
                    vertcount += mesh.VertexCount;
                }
            }

            vertdata = verts.ToArray();
            indicedata = inds.ToArray();
            coldata = colors.ToArray();
            texcoorddata = texcoords.ToArray();
            normdata = normals.ToArray();

            GL.BindBuffer(BufferTarget.ArrayBuffer, shaders[activeShader].GetBuffer("vert"));
            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(vertdata.Length * Vector3.SizeInBytes), vertdata, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(shaders[activeShader].GetAttribute("vert"), 3, VertexAttribPointerType.Float, false, 0, 0);

            // Buffer texture coordinates if shader supports it
            if (shaders[activeShader].GetAttribute("vertTexCoord") != -1)
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, shaders[activeShader].GetBuffer("vertTexCoord"));
                GL.BufferData<Vector2>(BufferTarget.ArrayBuffer, (IntPtr)(texcoorddata.Length * Vector2.SizeInBytes), texcoorddata, BufferUsageHint.StaticDraw);
                GL.VertexAttribPointer(shaders[activeShader].GetAttribute("vertTexCoord"), 2, VertexAttribPointerType.Float, true, 0, 0);
            }

            // Buffer vertex colors if shader supports it
            if (shaders[activeShader].GetAttribute("vertColor") != -1)
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, shaders[activeShader].GetBuffer("vertColor"));
                GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(coldata.Length * Vector3.SizeInBytes), coldata, BufferUsageHint.StaticDraw);
                GL.VertexAttribPointer(shaders[activeShader].GetAttribute("vertColor"), 3, VertexAttribPointerType.Float, false, 0, 0);
            }

            // Buffer normals if shader supports it
            if (shaders[activeShader].GetAttribute("vertNormal") != -1)
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, shaders[activeShader].GetBuffer("vertNormal"));
                GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(normdata.Length * Vector3.SizeInBytes), normdata, BufferUsageHint.StaticDraw);
                GL.VertexAttribPointer(shaders[activeShader].GetAttribute("vertNormal"), 3, VertexAttribPointerType.Float, true, 0, 0);
            }

            GL.UseProgram(shaders[activeShader].ProgramID);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            // Buffer index data
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ibo_elements);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(indicedata.Length * sizeof(int)), indicedata, BufferUsageHint.StaticDraw);

            GL.Flush();
        }

        public static void UpdateView()
        {
            View = Program.Camera.GetViewMatrix();
            AmbientLight.Position = new Vector4(Program.Camera.Position, 0);

            // Update model view matrices
            foreach (HWMesh mesh in HWScene.Meshes)
            {
                if (mesh.Visible)
                {
                    mesh.CalculateModelMatrix();

                    if(!Program.Camera.Orthographic)
                        mesh.ViewProjectionMatrix = View * Matrix4.CreatePerspectiveFieldOfView(Program.Camera.FieldOfView, (float)Program.GLControl.Width / (float)Program.GLControl.Height, Program.Camera.NearClipDistance, Program.Camera.ClipDistance);
                    else
                        mesh.ViewProjectionMatrix = View * Matrix4.CreateOrthographic((float)(Program.GLControl.Width / Program.Camera.OrthographicSize), (float)(Program.GLControl.Height / Program.Camera.OrthographicSize), Program.Camera.NearClipDistance, Program.Camera.ClipDistance);

                    mesh.ModelViewProjectionMatrix = mesh.ModelMatrix * mesh.ViewProjectionMatrix;
                }
            }

            foreach (EditorMesh mesh in EditorScene.meshes)
            {
                if (mesh.Visible)
                {
                    mesh.CalculateModelMatrix();

                    if (!Program.Camera.Orthographic)
                        mesh.ViewProjectionMatrix = View * Matrix4.CreatePerspectiveFieldOfView(Program.Camera.FieldOfView, (float)Program.GLControl.Width / (float)Program.GLControl.Height, Program.Camera.NearClipDistance, Program.Camera.ClipDistance);
                    else
                        mesh.ViewProjectionMatrix = View * Matrix4.CreateOrthographic((float)(Program.GLControl.Width / Program.Camera.OrthographicSize), (float)(Program.GLControl.Height / Program.Camera.OrthographicSize), Program.Camera.NearClipDistance, Program.Camera.ClipDistance);

                    mesh.ModelViewProjectionMatrix = mesh.ModelMatrix * mesh.ViewProjectionMatrix;
                }
            }
        }

        public static void Render()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.UseProgram(shaders[activeShader].ProgramID);
            shaders[activeShader].EnableVertexAttribArrays();

            //These uniforms are the same for every mesh
            GL.Uniform1(shaders[activeShader].GetUniform("thrusterInterpolation"), ThrusterInterpolation); //Send thruster interpolation value

            int indiceat = 0;
            foreach (HWMesh mesh in HWScene.Meshes)
            {
                if (mesh.Visible)
                {
                    HWTexture texture = null;
                    if (mesh.Material != null)
                        texture = mesh.Material.DiffuseTexture;

                    if (texture != null)
                    {
                        if (mesh.Material.GlowTexture != null) //Check if the material has a GLOW-Map
                        {
                            GL.ActiveTexture(TextureUnit.Texture1);
                            GL.BindTexture(TextureTarget.Texture2D, mesh.Material.GlowTexture.ID);
                            GL.Uniform1(shaders[activeShader].GetUniform("glowTex"), 1);
                            GL.Uniform1(shaders[activeShader].GetUniform("emissive"), 1); //Tell shader to use a GLOW map

                            if (mesh.Material.Shader == "shipglow" || mesh.Material.Shader == "shipglow_ns") //If shader with discrete GLOW map
                                GL.Uniform1(shaders[activeShader].GetUniform("discreteGlow"), 1); //Tell shader that it is a discrete GLOW map
                            else
                                GL.Uniform1(shaders[activeShader].GetUniform("discreteGlow"), 0); //Tell shader that it is not a discrete GLOW map
                        }
                        else
                        {
                            GL.Uniform1(shaders[activeShader].GetUniform("glowTex"), 0); //Tell shader that it is not a discrete GLOW map
                            GL.Uniform1(shaders[activeShader].GetUniform("emissive"), 0); //Tell shader to not use a GLOW map
                        }

                        if(mesh.Material.Shader == "thruster") //If the mesh material is a thruster
                        {
                            if (mesh.Material.ThrusterOffDiffuseTexture != null)
                            {
                                GL.ActiveTexture(TextureUnit.Texture2);
                                GL.BindTexture(TextureTarget.Texture2D, mesh.Material.ThrusterOffDiffuseTexture.ID);
                                GL.Uniform1(shaders[activeShader].GetUniform("thrusterOffDiff"), 2);
                            }

                            if (mesh.Material.ThrusterOffGlowTexture != null)
                            {
                                GL.ActiveTexture(TextureUnit.Texture3);
                                GL.BindTexture(TextureTarget.Texture2D, mesh.Material.ThrusterOffGlowTexture.ID);
                                GL.Uniform1(shaders[activeShader].GetUniform("thrusterOffGlow"), 3);
                            }

                            GL.Uniform1(shaders[activeShader].GetUniform("thruster"), 1); //Tell shader to interpolate between thruster textures
                        }
                        else
                        {
                            GL.Uniform1(shaders[activeShader].GetUniform("thrusterOffDiff"), 0);
                            GL.Uniform1(shaders[activeShader].GetUniform("thrusterOffGlow"), 0);
                            GL.Uniform1(shaders[activeShader].GetUniform("thruster"), 0); //Tell shader not to interpolate between thruster textures
                        }

                        GL.ActiveTexture(TextureUnit.Texture0);
                        GL.BindTexture(TextureTarget.Texture2D, texture.ID);
                        GL.Uniform1(shaders[activeShader].GetUniform("textured"), 1); //Tell shader to use texture colors
                    }
                    else
                    {
                        GL.Uniform1(shaders[activeShader].GetUniform("textured"), 0); //Tell shader to use vertex colors
                    }

                    if (mesh.Shaded)
                    {
                        GL.Uniform1(shaders[activeShader].GetUniform("shaded"), 1); //Tell shader to calculate lighting

                        GL.Uniform1(shaders[activeShader].GetUniform("materialTex"), 0);

                        GL.Uniform3(shaders[activeShader].GetUniform("materialSpecularColor"), ref mesh.Material.SpecularColor);
                        GL.Uniform1(shaders[activeShader].GetUniform("materialShininess"), mesh.Material.SpecularExponent);
                        GL.Uniform3(shaders[activeShader].GetUniform("cameraPosition"), ref Program.Camera.Position);

                        GL.Uniform1(shaders[activeShader].GetUniform("numLights"), Light.Lights.Count);
                        for (int i = 0; i < Light.Lights.Count; i++)
                        {
                            GL.Uniform4(shaders[activeShader].GetUniform("allLights[" + i + "]." + "position"), ref Light.Lights[i].Position);
                            GL.Uniform3(shaders[activeShader].GetUniform("allLights[" + i + "]." + "intensities"), ref Light.Lights[i].Color);
                            GL.Uniform1(shaders[activeShader].GetUniform("allLights[" + i + "]." + "attenuation"), Light.Lights[i].Attenuation);
                            GL.Uniform1(shaders[activeShader].GetUniform("allLights[" + i + "]." + "ambientCoefficient"), Light.Lights[i].AmbientCoefficient);
                        }
                    }
                    else
                        GL.Uniform1(shaders[activeShader].GetUniform("shaded"), 0); //Tell shader not to calculate lighting

                    GL.UniformMatrix4(shaders[activeShader].GetUniform("camera"), false, ref View);
                    GL.UniformMatrix4(shaders[activeShader].GetUniform("model"), false, ref mesh.ModelMatrix);
                    GL.UniformMatrix4(shaders[activeShader].GetUniform("modelview"), false, ref mesh.ModelViewProjectionMatrix);

                    GL.DrawElements(BeginMode.Triangles, mesh.IndiceCount, DrawElementsType.UnsignedInt, indiceat * sizeof(uint));
                    indiceat += mesh.IndiceCount;
                }
            }

            GL.Clear(ClearBufferMask.DepthBufferBit);

            foreach (EditorMesh mesh in EditorScene.meshes)
            {
                if (mesh.Visible)
                {
                    HWTexture texture = null;
                    if (mesh.Material != null)
                        texture = mesh.Material.DiffuseTexture;

                    if (texture != null)
                    {
                        GL.BindTexture(TextureTarget.Texture2D, texture.ID);
                        GL.Uniform1(shaders[activeShader].GetUniform("textured"), 1); //Tell shader to use texture colors
                    }
                    else
                    {
                        GL.Uniform1(shaders[activeShader].GetUniform("textured"), 0); //Tell shader to use vertex colors
                    }

                    if (mesh.Shaded)
                    {
                        GL.Uniform1(shaders[activeShader].GetUniform("shaded"), 1); //Tell shader to calculate lighting

                        if (mesh.Material != null)
                        {
                            GL.Uniform1(shaders[activeShader].GetUniform("materialTex"), 0);
                            GL.Uniform3(shaders[activeShader].GetUniform("materialSpecularColor"), ref mesh.Material.SpecularColor);
                            GL.Uniform1(shaders[activeShader].GetUniform("materialShininess"), mesh.Material.SpecularExponent);
                            GL.Uniform3(shaders[activeShader].GetUniform("cameraPosition"), ref Program.Camera.Position);

                            GL.Uniform1(shaders[activeShader].GetUniform("numLights"), Light.Lights.Count);
                            for (int i = 0; i < Light.Lights.Count; i++)
                            {
                                GL.Uniform4(shaders[activeShader].GetUniform("allLights[" + i + "]." + "position"), ref Light.Lights[i].Position);
                                GL.Uniform3(shaders[activeShader].GetUniform("allLights[" + i + "]." + "intensities"), ref Light.Lights[i].Color);
                                GL.Uniform1(shaders[activeShader].GetUniform("allLights[" + i + "]." + "attenuation"), Light.Lights[i].Attenuation);
                                GL.Uniform1(shaders[activeShader].GetUniform("allLights[" + i + "]." + "ambientCoefficient"), Light.Lights[i].AmbientCoefficient);
                            }
                        }
                    }
                    else
                        GL.Uniform1(shaders[activeShader].GetUniform("shaded"), 0); //Tell shader not to calculate lighting

                    GL.UniformMatrix4(shaders[activeShader].GetUniform("camera"), false, ref View);
                    GL.UniformMatrix4(shaders[activeShader].GetUniform("model"), false, ref mesh.ModelMatrix);
                    GL.UniformMatrix4(shaders[activeShader].GetUniform("modelview"), false, ref mesh.ModelViewProjectionMatrix);

                    if (mesh.GetType() == typeof(EditorLine))
                        GL.DrawElements(BeginMode.Lines, mesh.IndiceCount, DrawElementsType.UnsignedInt, indiceat * sizeof(uint));
                    else
                        GL.DrawElements(BeginMode.Triangles, mesh.IndiceCount, DrawElementsType.UnsignedInt, indiceat * sizeof(uint));

                    indiceat += mesh.IndiceCount;
                }
            }

            shaders[activeShader].DisableVertexAttribArrays();

            GL.Flush();
            Program.GLControl.SwapBuffers();
        }

        public static void Resize()
        {
            GL.Viewport(Program.GLControl.ClientRectangle.X, Program.GLControl.ClientRectangle.Y, Program.GLControl.ClientRectangle.Width, Program.GLControl.ClientRectangle.Height);

            Matrix4 projection = Matrix4.Identity;
            if(!Program.Camera.Orthographic)
                projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, (float)Program.GLControl.Width / (float)Program.GLControl.Height, Program.Camera.NearClipDistance, Program.Camera.ClipDistance);
            else
                projection = Matrix4.CreateOrthographic((float)(Program.GLControl.Width / Program.Camera.OrthographicSize), (float)(Program.GLControl.Height / Program.Camera.OrthographicSize), Program.Camera.NearClipDistance, Program.Camera.ClipDistance);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);
        }
    }
}
