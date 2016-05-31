using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworldDAEEditor
{
    public class HWImage
    {
        public string Name = "";
        public string Path = "";
        public ImageFormat Format = ImageFormat.DXT1;

        public HWMaterial Material;

        public HWImage(string name, string path)
        {
            Name = name;
            Path = path;

            if (name.StartsWith("IMG[")) //If texture is a valid homeworld texture
            {
                string[] splitted = name.Split('[');
                int end = -1;

                for (int i = 0; i < splitted.Length; i++)
                {
                    if (i != 0)
                    {
                        end = splitted[i].IndexOf(']');
                        if (splitted[i - 1].EndsWith("IMG")) //Name
                        {
                            Name = splitted[i].Substring(0, end);
                        }
                        else if (splitted[i - 1].EndsWith("FMT")) //Format
                        {
                            string formatString = splitted[i].Substring(0, end);
                            switch(formatString)
                            {
                                case "DXT1":
                                    Format = ImageFormat.DXT1;
                                    break;
                                case "DXT5":
                                    Format = ImageFormat.DXT5;
                                    break;
                                case "8888":
                                    Format = ImageFormat.UNCOMPRESSED;
                                    break;
                            }
                        }
                    }
                }
            }

            HWScene.Images.Add(this);
        }
    }

    public enum ImageFormat
    {
        DXT1 = 0,
        DXT5 = 1,
        UNCOMPRESSED = 2,
    }
}
