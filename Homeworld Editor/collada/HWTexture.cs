using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Drawing.Imaging;
using DevILSharp;
using System.IO;
using System;
using System.Windows.Forms;

namespace HomeworldDAEEditor
{
    public class HWTexture
    {
        public int ID = -1;
        public string Path;

        public HWTexture(string path)
        {
            Path = path;
            ID = loadImage(path);
        }

        private static int loadImage(string filename)
        {
            if(!File.Exists(filename))
            {
                MessageBox.Show("Failed to load texture \"" + filename + "\".", "Texture loading error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return Renderer.defaultTexture.ID;
            }

            int img = IL.GenImage();
            IL.BindImage(img);
            IL.LoadImage(filename);

            ILU.Info info = new ILU.Info();
            ILU.GetImageInfo(ref info);
            if (info.Origin == OriginMode.LowerLeft)
                ILU.FlipImage();

            IL.ConvertImage(ChannelFormat.RGBA, ChannelType.UnsignedByte);

            int texID = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, texID);

            //Anisotropic filtering
            float maxAniso;
            GL.GetFloat((GetPName)ExtTextureFilterAnisotropic.MaxTextureMaxAnisotropyExt, out maxAniso);
            GL.TexParameter(TextureTarget.Texture2D, (TextureParameterName)ExtTextureFilterAnisotropic.TextureMaxAnisotropyExt, maxAniso);

            GL.TexImage2D(TextureTarget.Texture2D, 0, (OpenTK.Graphics.OpenGL.PixelInternalFormat)IL.GetInteger(IntName.ImageFormat), IL.GetInteger(IntName.ImageWidth), IL.GetInteger(IntName.ImageHeight), 0, (OpenTK.Graphics.OpenGL.PixelFormat)IL.GetInteger(IntName.ImageFormat), PixelType.UnsignedByte, IL.GetData());

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            GL.BindTexture(TextureTarget.Texture2D, 0);

            IL.DeleteImage(img);
            IL.BindImage(0);

            return texID;
        }

        public static void Init()
        {
            IL.Init();
        }

        public static void Close()
        {
            IL.ShutDown();
        }
    }
}
