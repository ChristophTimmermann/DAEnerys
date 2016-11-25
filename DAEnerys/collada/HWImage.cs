using System.Collections.Generic;

namespace DAEnerys
{
    public class HWImage
    {
        public static List<HWImage> Images = new List<HWImage>();

        public string Name = "";
        public int Suffix = 1;
        public string ColladaName = "";
        public string Path = "";

        public string FormattedName
        {
            get
            {
                if(Suffix <= 1)
                    return "IMG[" + ColladaName + "]_FMT[" + Format + "]";
                else
                    return "IMG[" + ColladaName + "]_FMT[" + Format + "]_" + Suffix;
            }
        }

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
                            if (splitted.Length > 3)
                            {
                                string combined = splitted[i] + splitted[i + 1];
                                end = combined.LastIndexOf(']');
                                Name = combined.Substring(0, end);
                            }
                            else
                                Name = splitted[i].Substring(0, end);

                        }
                        else if (splitted[i - 1].EndsWith("FMT")) //Format
                        {
                            string formatString = splitted[i].Substring(0, end);
                            switch (formatString)
                            {
                                case "DXT1":
                                    Format = ImageFormat.DXT1;
                                    break;
                                case "DXT3":
                                    Format = ImageFormat.DXT3;
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
                ColladaName = Name;
            }

            Images.Add(this);
        }
    }

    public enum ImageFormat
    {
        DXT1 = 0,
        DXT3 = 1,
        DXT5 = 2,
        UNCOMPRESSED = 3,
    }
}
