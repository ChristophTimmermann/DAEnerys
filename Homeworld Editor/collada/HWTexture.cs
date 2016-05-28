using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Drawing.Imaging;
using DevILSharp;
using System.IO;
using System;

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
            string convertedPath = filename;
            string extension = System.IO.Path.GetExtension(filename);

            if (extension != ".png" || extension != ".jpg" || extension != ".bmp" || extension != ".gif" || extension != ".exif" || extension != ".tiff")
            {
                convertedPath = System.IO.Path.ChangeExtension(filename, "png");

                int img = IL.GenImage();
                IL.BindImage(img);
                IL.LoadImage(filename);
                IL.Save(ImageType.Png, convertedPath);
                IL.DeleteImage(img);
                IL.BindImage(0);
            }

            Bitmap image = new Bitmap(convertedPath);
            int texID = GL.GenTexture();

            GL.BindTexture(TextureTarget.Texture2D, texID);
            BitmapData data = image.LockBits(new System.Drawing.Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            //Anisotropic filtering
            float maxAniso;
            GL.GetFloat((GetPName)ExtTextureFilterAnisotropic.MaxTextureMaxAnisotropyExt, out maxAniso);
            GL.TexParameter(TextureTarget.Texture2D, (TextureParameterName)ExtTextureFilterAnisotropic.TextureMaxAnisotropyExt, maxAniso);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            image.UnlockBits(data);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            image.Dispose();
            File.Delete(convertedPath);
            return texID;
        }

        /*private static int loadImage(string filename)
        {
            int img = IL.GenImage();
            IL.BindImage(img);
            IL.LoadImage(filename);

            IntPtr data = IL.GetData();
            if(data == IntPtr.Zero)
            {
                IL.BindImage(0);
                IL.DeleteImage(1);
                return 0;
            }

            int width = IL.GetInteger(IntName.ImageWidth);
            int height = IL.GetInteger(IntName.ImageHeight);
            int type = IL.GetInteger(IntName.ImageType);
            int format = IL.GetInteger(IntName.ImageFormat);

            int texID = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, texID);

            GL.PixelStore(PixelStoreParameter.UnpackSwapBytes, 0);
            GL.PixelStore(PixelStoreParameter.UnpackRowLength, 0);
            GL.PixelStore(PixelStoreParameter.UnpackSkipPixels, 0);
            GL.PixelStore(PixelStoreParameter.UnpackSkipRows, 0);
            GL.PixelStore(PixelStoreParameter.UnpackAlignment, 1);

            //Anisotropic filtering
            float maxAniso;
            GL.GetFloat((GetPName)ExtTextureFilterAnisotropic.MaxTextureMaxAnisotropyExt, out maxAniso);
            GL.TexParameter(TextureTarget.Texture2D, (TextureParameterName)ExtTextureFilterAnisotropic.TextureMaxAnisotropyExt, maxAniso);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, width, height, 0, (OpenTK.Graphics.OpenGL.PixelFormat)format, (PixelType)type, data);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            GL.BindTexture(TextureTarget.Texture2D, 0);
            IL.BindImage(0);
            return texID;
        }*/

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
