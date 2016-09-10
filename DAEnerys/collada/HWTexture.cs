
using System;
using System.IO;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL;
using DevILSharp;
using System.Drawing;
using System.Collections.Generic;

namespace DAEnerys
{
    public enum TexMakeParams
    {
        ZERO,
        ONE,
        SRC_R,
        SRC_B,
        SRC_G,
        SRC_A,
        INVSRC_R,
        INVSRC_B,
        INVSRC_G,
        INVSRC_A
    }
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

        public static HWTexture MakeTexture(string name, string path, float r, float g, float b, float a, bool loadAlpha = false)
        {
            if (path == "")
            {
                float[] data = new float[1 * 1 * 4];
                data[0] = r;
                data[1] = g;
                data[2] = b;
                data[3] = a;
                GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
                IntPtr ptr = handle.AddrOfPinnedObject();
                int ID = RawLoadImage(1, 1, PixelFormat.Rgba, PixelType.Float, ptr, false, false);
                handle.Free();
                return new HWTexture(name, ID);
            }
            else
            {
                return new HWTexture(path, loadAlpha);
            }
        }

        public static HWTexture MakeMultTexture(
            string name,
            string SourceR, string SourceG, string SourceB,
            float DefaultR, float DefaultG, float DefaultB, float DefaultA,
            bool invertR = false, bool invertG = false, bool invertB = false,
            bool useAlpha = false
        ) {
            if (SourceR == "" && SourceG == "" && SourceB == "")
            {
                float[] data = new float[1 * 1 * 4];
                data[0] = invertR ? (1f - DefaultR) : DefaultR;
                data[1] = invertG ? (1f - DefaultG) : DefaultG;
                data[2] = invertB ? (1f - DefaultB) : DefaultB;
                data[3] = DefaultA;
                GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
                IntPtr ptr = handle.AddrOfPinnedObject();
                int ID = RawLoadImage(1, 1, PixelFormat.Rgba, PixelType.Float, ptr, false, false);
                handle.Free();
                return new HWTexture(name, ID);
            }

            {
                bool existsA = File.Exists(SourceR);
                bool existsB = File.Exists(SourceG);
                bool existsC = File.Exists(SourceB);
                if (!existsA && SourceR != "")
                    new Problem(ProblemTypes.WARNING, "Failed to load texture \"" + SourceR + "\".");
                if (!existsB && SourceG != "")
                    new Problem(ProblemTypes.WARNING, "Failed to load texture \"" + SourceG + "\".");
                if (!existsC && SourceB != "")
                    new Problem(ProblemTypes.WARNING, "Failed to load texture \"" + SourceB + "\".");
                if (!existsA && !existsB && !existsC)
                    return null;

                bool flipA = false, flipB = false, flipC = false;
                if (existsA && System.IO.Path.GetExtension(SourceR).ToLower() != ".tga")
                {
                    new Problem(ProblemTypes.WARNING, "The texture \"" + SourceR + "\" is not in TGA-Format.");
                    flipA = true;
                }
                if (existsB && System.IO.Path.GetExtension(SourceG).ToLower() != ".tga")
                {
                    new Problem(ProblemTypes.WARNING, "The texture \"" + SourceG + "\" is not in TGA-Format.");
                    flipB = true;
                }
                if (existsC && System.IO.Path.GetExtension(SourceB).ToLower() != ".tga")
                {
                    new Problem(ProblemTypes.WARNING, "The texture \"" + SourceB + "\" is not in TGA-Format.");
                    flipC = true;
                }

                int widthA, heightA;
                byte[,] aData = GetData(existsA, SourceR, flipA, out widthA, out heightA);

                int widthB, heightB;
                byte[,] bData = GetData(existsB, SourceG, flipB, out widthB, out heightB);

                int widthC, heightC;
                byte[,] cData = GetData(existsC, SourceB, flipC, out widthC, out heightC);


                if ((widthA != widthB && heightA != heightB && existsA && existsB) ||
                    (widthA != widthC && heightA != heightC && existsA && existsC) ||
                    (widthB != widthC && heightB != heightC && existsB && existsC))
                {
                    new Problem(ProblemTypes.ERROR, "The dimensions of the multi textures do not match.");
                    return null;
                }

                byte byteDefaultR = (byte)Math.Round(DefaultR * 255);
                byte byteDefaultG = (byte)Math.Round(DefaultG * 255);
                byte byteDefaultB = (byte)Math.Round(DefaultB * 255);
                byte byteDefaultA = (byte)Math.Round(DefaultA * 255);

                int width = existsA ? widthA : (existsB ? widthB : widthC);
                int height = existsA ? heightA : (existsB ? heightB : heightC);

                int size = width * 4;
                bool useA = aData.Length > 0;
                bool useB = bData.Length > 0;
                bool useC = cData.Length > 0;

                byte[,] data = new byte[height, size];

                for (int y = 0; y < height; y++)
                {
                    for (int i = 0; i < size; i += 4)
                    {
                        byte valA, valB, valC;
                        if (useAlpha)
                        {
                            valA = useA ? aData[y, i + 3] : byteDefaultR;
                            valB = useB ? bData[y, i + 3] : byteDefaultG;
                            valC = useC ? cData[y, i + 3] : byteDefaultB;
                        }
                        else
                        {
                            valA = useA ? (byte)((aData[y, i + 0] + aData[y, i + 1] + aData[y, i + 2] + aData[y, i + 3]) / 4) : byteDefaultR;
                            valB = useB ? (byte)((bData[y, i + 0] + bData[y, i + 1] + bData[y, i + 2] + bData[y, i + 3]) / 4) : byteDefaultG;
                            valC = useC ? (byte)((cData[y, i + 0] + cData[y, i + 1] + cData[y, i + 2] + cData[y, i + 3]) / 4) : byteDefaultB;
                        }

                        data[y, i + 0] = invertR ? (byte)(255 - valA) : valA;
                        data[y, i + 1] = invertG ? (byte)(255 - valB) : valB;
                        data[y, i + 2] = invertB ? (byte)(255 - valC) : valC;
                        data[y, i + 3] = byteDefaultA;
                    }
                }

                GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
                IntPtr ptr = handle.AddrOfPinnedObject();
                int ID = RawLoadImage(width, height, PixelFormat.Rgba, PixelType.UnsignedByte, ptr, false, false);
                handle.Free();
                data = null;
                return new HWTexture(name, ID);
            }
        }

        private static byte[,] GetData(bool exists, string path, bool flip, out int width, out int height)
        {
            byte[,] data = new byte[0, 0];

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

                int rowSize = width * 4;

                long ptr = IL.GetData().ToInt64();
                data = new byte[height, rowSize];

                for (int i = 0; i < height; i++)
                {
                    byte[] row = new byte[rowSize];
                    Marshal.Copy(new IntPtr(ptr), row, 0, rowSize);
                    Buffer.BlockCopy(row, 0, data, i * rowSize, rowSize);
                    ptr += width * 4;
                }

                IL.DeleteImage(img);
                IL.BindImage(0);
            }
            return data;
        }

        private static int RawLoadImage(int width, int height, PixelFormat pixelFormat, PixelType pixeltype, IntPtr ptr, bool loadAlpha, bool sprite)
        {
            int texID = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, texID);
            
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

        public static Bitmap LoadToBitmap(string path)
        {
            bool exists = File.Exists(path);

            if (!exists)
            {
                new Problem(ProblemTypes.WARNING, "Failed to load texture \"" + path + "\".");
                return null;
            }

            int img = IL.GenImage();
            IL.BindImage(img);
            IL.LoadImage(path);

            IL.ConvertImage(ChannelFormat.BGRA, ChannelType.UnsignedByte);

            int imageWidth = IL.GetInteger(IntName.ImageWidth);
            int imageHeight = IL.GetInteger(IntName.ImageHeight);

            Bitmap bitmap = new Bitmap(imageWidth, imageHeight, 4, System.Drawing.Imaging.PixelFormat.Format32bppArgb, IL.GetData());
            bitmap.LockBits(new Rectangle(0, 0, imageWidth, imageHeight), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            IL.DeleteImage(img);
            IL.BindImage(0);

            return bitmap;
        }
    }
}
