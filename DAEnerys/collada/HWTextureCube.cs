
using System;
using System.IO;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL;
using DevILSharp;
using System.Drawing;

namespace DAEnerys
{
    public class HWTextureCube : HWTexture
    {
        private string PathPosX;
        private string PathNegX;
        private string PathPosY;
        private string PathNegY;
        private string PathPosZ;
        private string PathNegZ;

        public HWTextureCube(
            string pathPosX, string pathNegX,
            string pathPosY, string pathNegY,
            string pathPosZ, string pathNegZ) : base("cubemap", 0)
        {
            PathPosX = pathPosX;
            PathNegX = pathNegX;
            PathPosY = pathPosY;
            PathNegY = pathNegY;
            PathPosZ = pathPosZ;
            PathNegZ = pathNegZ;
            ID = CreateCubeMap();
        }

        private static void LoadCubeMapSide(int texture, TextureTarget target, CubeTex tex)
        {
            GL.BindTexture(TextureTarget.TextureCubeMap, texture);
            
            GL.TexImage2D(
                target,
                0,
                PixelInternalFormat.SrgbAlpha,
                tex.width,
                tex.height,
                0,
                tex.pixelFormat,
                PixelType.UnsignedByte,
                tex.ptr);
            tex.Release();
        }

        private int CreateCubeMap()
        {
            GL.ActiveTexture(TextureUnit.Texture0);
            int texID = GL.GenTexture();

            LoadCubeMapSide(texID, TextureTarget.TextureCubeMapPositiveX, LoadCubeTex(PathPosX));
            LoadCubeMapSide(texID, TextureTarget.TextureCubeMapNegativeX, LoadCubeTex(PathNegX));
            LoadCubeMapSide(texID, TextureTarget.TextureCubeMapPositiveY, LoadCubeTex(PathPosY));
            LoadCubeMapSide(texID, TextureTarget.TextureCubeMapNegativeY, LoadCubeTex(PathNegY));
            LoadCubeMapSide(texID, TextureTarget.TextureCubeMapPositiveZ, LoadCubeTex(PathPosZ));
            LoadCubeMapSide(texID, TextureTarget.TextureCubeMapNegativeZ, LoadCubeTex(PathNegZ));

            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapR, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
            
            return texID;
        }

        private static CubeTex LoadCubeTex(string filename)
        {
            bool exists = File.Exists(filename);
            bool flip = false;

            if (!exists)
            {
                new Problem(ProblemTypes.WARNING, "Failed to load texture \"" + filename + "\".");
                return null;
            }

            if (System.IO.Path.GetExtension(filename).ToLower() != ".tga")
            {
                new Problem(ProblemTypes.WARNING, "The texture \"" + filename + "\" is not in TGA-Format.");
                flip = true;
            }

            int img = IL.GenImage();
            IL.BindImage(img);
            IL.LoadImage(filename);

            if (flip)
                ILU.FlipImage();

            IL.ConvertImage(ChannelFormat.RGBA, ChannelType.UnsignedByte);

            return new CubeTex(
                img,
                (PixelFormat)IL.GetInteger(IntName.ImageFormat),
                IL.GetInteger(IntName.ImageWidth),
                IL.GetInteger(IntName.ImageHeight),
                IL.GetData());
        }

        private class CubeTex
        {
            public int id;
            public IntPtr ptr;
            public int width;
            public int height;
            public PixelFormat pixelFormat;

            public CubeTex(int ilID, PixelFormat pf, int w, int h, IntPtr intPtr)
            {
                id = ilID;
                pixelFormat = pf;
                width = w;
                height = h;
                ptr = intPtr;
            }

            internal void Release()
            {
                IL.DeleteImage(id);
                IL.BindImage(0);
            }
        }
    }
}
