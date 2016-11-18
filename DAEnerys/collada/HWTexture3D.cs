
using System;
using System.IO;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL;
using DevILSharp;
using System.Drawing;

namespace DAEnerys
{
    public class HWTexture3D
    {
        public int ID = -1;
        public string Path;

        private HWTexture3D(string path, int id)
        {
            Path = path;
            ID = id;
        }

        private static int RawLoadImage(int width, int height, int depth, PixelFormat pixelFormat, PixelType pixeltype, IntPtr ptr)
        {
            int texID = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture3D, texID);

            //Anisotropic filtering
            float maxAniso;
            GL.GetFloat((GetPName)ExtTextureFilterAnisotropic.MaxTextureMaxAnisotropyExt, out maxAniso);
            GL.TexParameter(TextureTarget.Texture3D, (TextureParameterName)ExtTextureFilterAnisotropic.TextureMaxAnisotropyExt, maxAniso);

            PixelInternalFormat pxIntFormat = PixelInternalFormat.SrgbAlpha;
            GL.TexImage3D(TextureTarget.Texture3D, 0, pxIntFormat, width, height, depth, 0, pixelFormat, pixeltype, ptr);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);


            GL.BindTexture(TextureTarget.Texture3D, 0);

            return texID;
        }

        private static int loadImage(string filename)
        {
            bool exists = File.Exists(filename);
            bool flip = false;
            
            int img = IL.GenImage();
            IL.BindImage(img);
            IL.LoadImage(filename);

            if (flip)
                ILU.FlipImage();

            IL.ConvertImage(ChannelFormat.RGBA, ChannelType.UnsignedByte);

            int texID = RawLoadImage(1024, 1024, 1024, (PixelFormat)IL.GetInteger(IntName.ImageFormat), PixelType.UnsignedByte, IL.GetData());

            IL.DeleteImage(img);
            IL.BindImage(0);

            return texID;
        }
    }
}
