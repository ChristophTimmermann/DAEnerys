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

        public static float NearClipDistance = 0.01f;
        public static float ClipDistance = 1000;

        public static HWTexture defaultTexture = new HWTexture(@"resources/missing.tga");

        static Light activeLight;

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
            GL.ClearColor(Color.CornflowerBlue);

            GL.Enable(EnableCap.DepthTest);
            GL.EnableClientState(ArrayCap.VertexArray);

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            GL.LineWidth(2);

            activeLight = new Light(new Vector3(100, 0, 0), new Vector3(0.9f, 0.80f, 0.8f));
            activeShader = "default";

            GL.GenBuffers(1, out ibo_elements);

            // Load shaders from file
            shaders.Add("default", new Shader("vs.glsl", "fs.glsl", true));
            shaders.Add("textured", new Shader("vs_tex.glsl", "fs_tex.glsl", true));
            //shaders.Add("normal", new Shader("vs_norm.glsl", "fs_norm.glsl", true));
            //shaders.Add("lit", new Shader("vs_lit.glsl", "fs_lit.glsl", true));

            activeShader = "textured";
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

            GL.BindBuffer(BufferTarget.ArrayBuffer, shaders[activeShader].GetBuffer("vPosition"));

            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(vertdata.Length * Vector3.SizeInBytes), vertdata, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(shaders[activeShader].GetAttribute("vPosition"), 3, VertexAttribPointerType.Float, false, 0, 0);

            // Buffer texture coordinates if shader supports it
            if (shaders[activeShader].GetAttribute("texcoord") != -1)
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, shaders[activeShader].GetBuffer("texcoord"));
                GL.BufferData<Vector2>(BufferTarget.ArrayBuffer, (IntPtr)(texcoorddata.Length * Vector2.SizeInBytes), texcoorddata, BufferUsageHint.StaticDraw);
                GL.VertexAttribPointer(shaders[activeShader].GetAttribute("texcoord"), 2, VertexAttribPointerType.Float, true, 0, 0);
            }

            // Buffer vertex colors if shader supports it
            if (shaders[activeShader].GetAttribute("vColor") != -1)
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, shaders[activeShader].GetBuffer("vColor"));
                GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(coldata.Length * Vector3.SizeInBytes), coldata, BufferUsageHint.StaticDraw);
                GL.VertexAttribPointer(shaders[activeShader].GetAttribute("vColor"), 3, VertexAttribPointerType.Float, false, 0, 0);
            }

            if (shaders[activeShader].GetAttribute("vNormal") != -1)
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, shaders[activeShader].GetBuffer("vNormal"));
                GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(normdata.Length * Vector3.SizeInBytes), normdata, BufferUsageHint.StaticDraw);
                GL.VertexAttribPointer(shaders[activeShader].GetAttribute("vNormal"), 3, VertexAttribPointerType.Float, true, 0, 0);
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
            // Update model view matrices
            foreach (HWMesh mesh in HWScene.Meshes)
            {
                if (mesh.Visible)
                {
                    mesh.CalculateModelMatrix();
                    mesh.ViewProjectionMatrix = View * Matrix4.CreatePerspectiveFieldOfView(1.3f, (float)Program.GLControl.Width / (float)Program.GLControl.Height, NearClipDistance, ClipDistance);
                    mesh.ModelViewProjectionMatrix = mesh.ModelMatrix * mesh.ViewProjectionMatrix;
                }
            }

            foreach (EditorMesh mesh in EditorScene.meshes)
            {
                if (mesh.Visible)
                {
                    mesh.CalculateModelMatrix();
                    mesh.ViewProjectionMatrix = View * Matrix4.CreatePerspectiveFieldOfView(1.3f, (float)Program.GLControl.Width / (float)Program.GLControl.Height, NearClipDistance, ClipDistance);
                    mesh.ModelViewProjectionMatrix = mesh.ModelMatrix * mesh.ViewProjectionMatrix;
                }
            }
            View = Program.Camera.GetViewMatrix();
        }

        public static void Render()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.UseProgram(shaders[activeShader].ProgramID);
            shaders[activeShader].EnableVertexAttribArrays();

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
                        GL.BindTexture(TextureTarget.Texture2D, mesh.Material.DiffuseTexture.ID);
                        GL.Uniform1(shaders[activeShader].GetUniform("textured"), 1); //Tell shader to use texture colors
                    }
                    else
                    {
                        GL.Uniform1(shaders[activeShader].GetUniform("textured"), 0); //Tell shader to use vertex colors
                    }

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
                    if(mesh.Material != null)
                        texture = mesh.Material.DiffuseTexture;

                    if(texture != null)
                    {
                        GL.BindTexture(TextureTarget.Texture2D, mesh.Material.DiffuseTexture.ID);
                        GL.Uniform1(shaders[activeShader].GetUniform("textured"), 1); //Tell shader to use texture colors
                    }
                    else
                    {
                        GL.Uniform1(shaders[activeShader].GetUniform("textured"), 0); //Tell shader to use vertex colors
                    }

                    GL.UniformMatrix4(shaders[activeShader].GetUniform("modelview"), false, ref mesh.ModelViewProjectionMatrix);

                    if(mesh.GetType() == typeof(EditorLine))
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
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, (float)Program.GLControl.Width / (float)Program.GLControl.Height, NearClipDistance, ClipDistance);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);
        }
    }
}
