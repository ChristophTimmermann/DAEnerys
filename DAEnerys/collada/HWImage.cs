using System.Collections.Generic;
using System.IO;

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

        public HWImage(string name, string path, ImageFormat format = ImageFormat.DXT1)
        {
            Name = name;
            ColladaName = name;
            Path = path;
            Format = format;

            Images.Add(this);
        }

        public static HWImage Parse(string name, string path)
        {
            string absolutePath = System.IO.Path.Combine(Importer.ColladaPath, path.Replace("file://", ""));

            if (File.Exists(absolutePath))
            {
                if (name.StartsWith("IMG[")) //If texture is a valid homeworld texture
                {
                    string[] splitted = name.Split('[');
                    int end = -1;
                    string texName = "";
                    ImageFormat format = ImageFormat.DXT1;

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
                                    texName = combined.Substring(0, end);
                                }
                                else
                                    texName = splitted[i].Substring(0, end);

                            }
                            else if (splitted[i - 1].EndsWith("FMT")) //Format
                            {
                                string formatString = splitted[i].Substring(0, end);
                                switch (formatString)
                                {
                                    case "DXT1":
                                        format = ImageFormat.DXT1;
                                        break;
                                    case "DXT3":
                                        format = ImageFormat.DXT3;
                                        break;
                                    case "DXT5":
                                        format = ImageFormat.DXT5;
                                        break;
                                    case "8888":
                                        format = ImageFormat.UNCOMPRESSED;
                                        break;
                                }
                            }
                        }
                    }

                    HWImage newImage = new HWImage(texName, path, format);
                    newImage.ColladaName = name;
                    return newImage;
                }
                else
                    return null;
            }
            else
            {
                new Problem(ProblemTypes.WARNING, "Failed to load texture \"" + path + "\".");
                return null;
            }
        }

        public void Destroy()
        {
            Images.Remove(this);
            Material.Images.Remove(this);
            Material = null;
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
