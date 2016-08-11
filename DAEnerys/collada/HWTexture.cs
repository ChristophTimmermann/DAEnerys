
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



        public static HWTexture MakeTeamTexture(string teamTexturePath, string stripeTexturePath, string paintTexturePath)
        {
            if (teamTexturePath == "" && stripeTexturePath == "" && stripeTexturePath == "") return null;
            bool teamexists = File.Exists(teamTexturePath);
            bool strpexists = File.Exists(stripeTexturePath);
            bool painexists = File.Exists(paintTexturePath);
            if (!teamexists && teamTexturePath != "")
                new Problem(ProblemTypes.WARNING, "Failed to load texture \"" + teamTexturePath + "\".");
            if (!strpexists && stripeTexturePath != "")
                new Problem(ProblemTypes.WARNING, "Failed to load texture \"" + stripeTexturePath + "\".");
            if (!painexists && paintTexturePath != "")
                new Problem(ProblemTypes.WARNING, "Failed to load texture \"" + paintTexturePath + "\".");
            if (!teamexists && !strpexists && !painexists)
                return null;

            bool flipteam = false, flipstrp = false, flippain = false;
            if (teamexists && System.IO.Path.GetExtension(teamTexturePath).ToLower() != ".tga")
            {
                new Problem(ProblemTypes.WARNING, "The texture \"" + teamTexturePath + "\" is not in TGA-Format.");
                flipteam = true;
            }
            if (strpexists && System.IO.Path.GetExtension(stripeTexturePath).ToLower() != ".tga")
            {
                new Problem(ProblemTypes.WARNING, "The texture \"" + stripeTexturePath + "\" is not in TGA-Format.");
                flipstrp = true;
            }
            if (painexists && System.IO.Path.GetExtension(paintTexturePath).ToLower() != ".tga")
            {
                new Problem(ProblemTypes.WARNING, "The texture \"" + paintTexturePath + "\" is not in TGA-Format.");
                flippain = true;
            }

            int teamW, teamH;
            byte[] teamData = GetData(teamexists, teamTexturePath, flipteam, out teamW, out teamH);

            int strpW, strpH;
            byte[] strpData = GetData(strpexists, stripeTexturePath, flipstrp, out strpW, out strpH);
            
            int painW, painH;
            byte[] painData = GetData(painexists, paintTexturePath, flippain, out painW, out painH);


            if ((teamW != strpW && teamH != strpH && teamexists && strpexists) ||
                (teamW != painW && teamH != painH && teamexists && painexists) ||
                (strpW != painW && strpH != painH && strpexists && painexists))
            { 
                new Problem(ProblemTypes.ERROR, "The dimensions of the team/stripe/paint textures do not match.");
                return null;
            }

            int width = teamexists ? teamW : (strpexists ? strpW : painW);
            int height = teamexists ? teamH : (strpexists ? strpH : painH);

            IntPtr data = WrangleTeamData(width * height * 4, teamData, strpData, painData);
            int ID = RawLoadImage(width, height, PixelFormat.Rgba, PixelType.Float, data, false, false);
            Marshal.FreeHGlobal(data);
            
            return new HWTexture("TEAM_STRP_WRANGLE", ID);
        }

        private static IntPtr WrangleTeamData(int size, byte[] teamData, byte[] strpData, byte[] painData)
        {
            bool useTeam = teamData.Length > 0;
            bool useStrp = strpData.Length > 0;
            bool usePain = painData.Length > 0;
            float[] data = new float[size];
            for (int i = 0; i < size; i += 4)
            {
                data[i + 0] = (1f - (useTeam ? teamData[i + 3] : 0) / 255f); // dest.R = 1 - team.A
                data[i + 1] = (1f - (useStrp ? strpData[i + 3] : 0) / 255f); // dest.G = 1 - strp.A
                data[i + 2] = (1f - (usePain ? painData[i + 3] : 0) / 255f); // dest.B = 1 - pain.A
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
