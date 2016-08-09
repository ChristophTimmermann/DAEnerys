
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



        public static HWTexture MakeTeamTexture(string teamTexturePath, string stripeTexturePath)
        {
            if (teamTexturePath == "" && stripeTexturePath == "") return null;
            bool teamexists = File.Exists(teamTexturePath);
            bool strpexists = File.Exists(stripeTexturePath);
            if (!teamexists)
                new Problem(ProblemTypes.WARNING, "Failed to load texture \"" + teamTexturePath + "\".");
            if (!strpexists)
                new Problem(ProblemTypes.WARNING, "Failed to load texture \"" + stripeTexturePath + "\".");
            if (!teamexists && !strpexists)
                return null;

            bool flipteam = false, flipstrp = false;
            if (System.IO.Path.GetExtension(teamTexturePath).ToLower() != ".tga")
            {
                new Problem(ProblemTypes.WARNING, "The texture \"" + teamTexturePath + "\" is not in TGA-Format.");
                flipteam = true;
            }
            if (System.IO.Path.GetExtension(stripeTexturePath).ToLower() != ".tga")
            {
                new Problem(ProblemTypes.WARNING, "The texture \"" + stripeTexturePath + "\" is not in TGA-Format.");
                flipstrp = true;
            }

            IntPtr teamDataPtr;
            byte[] teamData = new byte[0];
            int teamW = 0, teamH = 0;
            PixelFormat teamPF = 0;
            if (teamexists)
            {
                int team = IL.GenImage();
                IL.BindImage(team);
                IL.LoadImage(teamTexturePath);
                if (flipteam) ILU.FlipImage();
                IL.ConvertImage(ChannelFormat.RGBA, ChannelType.UnsignedByte);
                teamW = IL.GetInteger(IntName.ImageWidth);
                teamH = IL.GetInteger(IntName.ImageHeight);
                double teamLogW = Math.Log(teamW) / Math.Log(2);
                double teamLogH = Math.Log(teamH) / Math.Log(2);
                if ((((int)teamLogW) != teamLogW) || (((int)teamLogH) != teamLogH))
                    new Problem(ProblemTypes.WARNING, "The texture \"" + teamTexturePath + "\" does not have a power-of-2 dimension.");
                teamPF = (PixelFormat)IL.GetInteger(IntName.ImageFormat);
                teamDataPtr = IL.GetData();
                teamData = new byte[teamW * teamH * 4];
                Marshal.Copy(teamDataPtr, teamData, 0, teamW * teamH * 4);
                IL.DeleteImage(team);
                IL.BindImage(0);
            }

            IntPtr strpDataPtr;
            byte[] strpData = new byte[0];
            int strpW = 0, strpH = 0;
            OpenTK.Graphics.OpenGL.PixelFormat strpPF = 0;
            if (strpexists)
            {
                int strp = IL.GenImage();
                IL.BindImage(strp);
                IL.LoadImage(stripeTexturePath);
                if (flipstrp) ILU.FlipImage();
                IL.ConvertImage(ChannelFormat.RGBA, ChannelType.UnsignedByte);
                strpW = IL.GetInteger(IntName.ImageWidth);
                strpH = IL.GetInteger(IntName.ImageHeight);
                double strpLogW = Math.Log(strpW) / Math.Log(2);
                double strpLogH = Math.Log(strpH) / Math.Log(2);
                if ((((int)strpLogW) != strpLogW) || (((int)strpLogH) != strpLogH))
                    new Problem(ProblemTypes.WARNING, "The texture \"" + stripeTexturePath + "\" does not have a power-of-2 dimension.");
                strpPF = (PixelFormat)IL.GetInteger(IntName.ImageFormat);
                strpDataPtr = IL.GetData();
                strpData = new byte[strpW * strpH * 4];
                Marshal.Copy(strpDataPtr, strpData, 0, strpW * strpH * 4);
                IL.DeleteImage(strp);
                IL.BindImage(0);
            }

            if (teamexists && strpexists && teamW != strpW || teamH != strpH)
                new Problem(ProblemTypes.ERROR, "The dimensions of the team and stripe textures do not match.");

            IntPtr data = WrangleTeamData(teamW * teamH * 4, teamData, strpData);
            int ID = RawLoadImage(teamW, teamH, PixelFormat.Rgba, PixelType.Float, data, false, false);
            Marshal.FreeHGlobal(data);

            if (teamexists && !strpexists)
                return new HWTexture(teamTexturePath, ID);
            else if (!teamexists && strpexists)
                return new HWTexture(stripeTexturePath, ID);
            else if (teamexists && strpexists)
                return new HWTexture("TEAM_STRP_WRANGLE", ID);

            return null;
        }

        private static IntPtr WrangleTeamData(int size, byte[] teamData, byte[] strpData)
        {
            bool useTeam = teamData.Length > 0;
            bool useStrp = strpData.Length > 0;
            float[] data = new float[size];
            for (int i = 0; i < size; i += 4)
            {
                data[i + 0] = 1f - (useTeam ? teamData[i + 3] : 0) / 255f; // dest.R = 1 - team.A
                data[i + 1] = 1f - (useStrp ? strpData[i + 3] : 0) / 255f; // dest.G = 1 - strp.A
                data[i + 2] = 1f; // dest.B = 1
                data[i + 3] = 1f; // dest.A = 1
            }
            IntPtr ptr = Marshal.AllocHGlobal(data.Length * sizeof(float));
            Marshal.Copy(data, 0, ptr, data.Length);
            return ptr;
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
