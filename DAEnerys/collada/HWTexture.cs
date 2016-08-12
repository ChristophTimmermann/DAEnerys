
using System;
using System.IO;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL;
using DevILSharp;

namespace DAEnerys
{
    public class HWTexture
    {
        public int ID = -1;
        public string Path;

        private HWTexture(string path, int id)
        {
            Path = path;
            ID = id;
        }

        public HWTexture(string path, bool loadAlpha = false, bool sprite = false)
        {
            Path = path;
            ID = loadImage(path, loadAlpha, sprite);
        }
        
        public static HWTexture MakeMultTexture(string TexturePath_A, string TexturePath_B, string TexturePath_C)
        {
            if (TexturePath_A == "" && TexturePath_B == "" && TexturePath_B == "") return null;
            bool existsA = File.Exists(TexturePath_A);
            bool existsB = File.Exists(TexturePath_B);
            bool existsC = File.Exists(TexturePath_C);
            if (!existsA && TexturePath_A != "")
                new Problem(ProblemTypes.WARNING, "Failed to load texture \"" + TexturePath_A + "\".");
            if (!existsB && TexturePath_B != "")
                new Problem(ProblemTypes.WARNING, "Failed to load texture \"" + TexturePath_B + "\".");
            if (!existsC && TexturePath_C != "")
                new Problem(ProblemTypes.WARNING, "Failed to load texture \"" + TexturePath_C + "\".");
            if (!existsA && !existsB && !existsC)
                return null;

            bool flipA = false, flipB = false, flipC = false;
            if (existsA && System.IO.Path.GetExtension(TexturePath_A).ToLower() != ".tga")
            {
                new Problem(ProblemTypes.WARNING, "The texture \"" + TexturePath_A + "\" is not in TGA-Format.");
                flipA = true;
            }
            if (existsB && System.IO.Path.GetExtension(TexturePath_B).ToLower() != ".tga")
            {
                new Problem(ProblemTypes.WARNING, "The texture \"" + TexturePath_B + "\" is not in TGA-Format.");
                flipB = true;
            }
            if (existsC && System.IO.Path.GetExtension(TexturePath_C).ToLower() != ".tga")
            {
                new Problem(ProblemTypes.WARNING, "The texture \"" + TexturePath_C + "\" is not in TGA-Format.");
                flipC = true;
            }

            int widthA, heightA;
            byte[] aData = GetData(existsA, TexturePath_A, flipA, out widthA, out heightA);

            int widthB, heightB;
            byte[] bData = GetData(existsB, TexturePath_B, flipB, out widthB, out heightB);
            
            int widthC, heightC;
            byte[] cData = GetData(existsC, TexturePath_C, flipC, out widthC, out heightC);


            if ((widthA != widthB && heightA != heightB && existsA && existsB) ||
                (widthA != widthC && heightA != heightC && existsA && existsC) ||
                (widthB != widthC && heightB != heightC && existsB && existsC))
            { 
                new Problem(ProblemTypes.ERROR, "The dimensions of the multi textures do not match.");
                return null;
            }

            int width = existsA ? widthA : (existsB ? widthB : widthC);
            int height = existsA ? heightA : (existsB ? heightB : heightC);

            IntPtr data = WrangleMultData(width * height * 4, aData, bData, cData);
            int ID = RawLoadImage(width, height, PixelFormat.Rgba, PixelType.Float, data, false, false);
            Marshal.FreeHGlobal(data);
            
            return new HWTexture("MULT_WRANGLE", ID);
        }

        private static IntPtr WrangleMultData(int size, byte[] aData, byte[] bData, byte[] cData)
        {
            bool useA = aData.Length > 0;
            bool useB = bData.Length > 0;
            bool useC = cData.Length > 0;
            float[] data = new float[size];
            for (int i = 0; i < size; i += 4)
            {
                data[i + 0] = (1f - (useA ? aData[i + 3] : 0) / 255f); // dest.R = 1 - srcA.A
                data[i + 1] = (1f - (useB ? bData[i + 3] : 0) / 255f); // dest.G = 1 - srcB.A
                data[i + 2] = (1f - (useC ? cData[i + 3] : 0) / 255f); // dest.B = 1 - srcC.A
                data[i + 3] = 1f; // dest.A = 1
            }
            IntPtr ptr = Marshal.AllocHGlobal(data.Length * sizeof(float));
            Marshal.Copy(data, 0, ptr, data.Length);
            return ptr;
        }

        private static byte[] GetData(bool exists, string path, bool flip, out int width, out int height)
        {
            IntPtr dataPtr;
            byte[] data = new byte[0];
            PixelFormat PF = 0;
            width = 0; height = 0;
            if (exists)
            {
                int img = IL.GenImage();
                IL.BindImage(img);
                IL.LoadImage(path);
                if (flip) ILU.FlipImage();
                IL.ConvertImage(ChannelFormat.RGBA, ChannelType.UnsignedByte);
                width = IL.GetInteger(IntName.ImageWidth);
                height = IL.GetInteger(IntName.ImageHeight);
                double LogW = Math.Log(width) / Math.Log(2);
                double LogH = Math.Log(height) / Math.Log(2);
                if ((((int)LogW) != LogW) || (((int)LogH) != LogH))
                    new Problem(ProblemTypes.WARNING, "The texture \"" + path + "\" does not have a power-of-2 dimension.");
                PF = (PixelFormat)IL.GetInteger(IntName.ImageFormat);
                dataPtr = IL.GetData();
                data = new byte[width * height * 4];
                Marshal.Copy(dataPtr, data, 0, width * height * 4);
                IL.DeleteImage(img);
                IL.BindImage(0);
            }
            return data;
        }

        private static int RawLoadImage(int width, int height, PixelFormat pixelFormat, PixelType pixeltype, IntPtr ptr, bool loadAlpha, bool sprite)
        {
            int texID = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, texID);

            //GL.TexImage2D(TextureTarget.Texture2D, 0, (OpenTK.Graphics.OpenGL.PixelInternalFormat)IL.GetInteger(IntName.ImageFormat), IL.GetInteger(IntName.ImageWidth), IL.GetInteger(IntName.ImageHeight), 0, (OpenTK.Graphics.OpenGL.PixelFormat)IL.GetInteger(IntName.ImageFormat), PixelType.UnsignedByte, IL.GetData());

            //Anisotropic filtering
            float maxAniso;
            GL.GetFloat((GetPName)ExtTextureFilterAnisotropic.MaxTextureMaxAnisotropyExt, out maxAniso);
            GL.TexParameter(TextureTarget.Texture2D, (TextureParameterName)ExtTextureFilterAnisotropic.TextureMaxAnisotropyExt, maxAniso);

            PixelInternalFormat pxIntFormat = loadAlpha ? PixelInternalFormat.SrgbAlpha : PixelInternalFormat.Srgb;
            GL.TexImage2D(TextureTarget.Texture2D, 0, pxIntFormat, width, height, 0, pixelFormat, pixeltype, ptr);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            if (sprite)
            {
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMinFilter.Linear);
            }

            GL.BindTexture(TextureTarget.Texture2D, 0);

            return texID;
        }

        private static int loadImage(string filename, bool loadAlpha, bool sprite)
        {
            bool exists = File.Exists(filename);
            bool flip = false;

            if (!exists)
            {
                new Problem(ProblemTypes.WARNING, "Failed to load texture \"" + filename + "\".");
                return Renderer.DefaultTexture.ID;
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

            int imageWidth = IL.GetInteger(IntName.ImageWidth);
            int imageHeight = IL.GetInteger(IntName.ImageHeight);
            double widthLog2 = Math.Log(imageWidth) / Math.Log(2);
            double heightLog2 = Math.Log(imageHeight) / Math.Log(2);

            if ((((int)widthLog2) != widthLog2) || (((int)heightLog2) != heightLog2))
                new Problem(ProblemTypes.WARNING, "The texture \"" + filename + "\" does not have a power-of-2 dimension.");

            int texID = RawLoadImage(imageWidth, imageHeight, (PixelFormat)IL.GetInteger(IntName.ImageFormat), PixelType.UnsignedByte, IL.GetData(), loadAlpha, sprite);

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
