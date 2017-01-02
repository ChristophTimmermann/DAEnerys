using System.Collections.Generic;
using System.IO;
using System.Linq;

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
                string format = Format.ToString();
                if (Format == ImageFormat.UNCOMPRESSED)
                    format = "8888";

                if(Suffix <= 1)
                    return "IMG[" + Name + "]_FMT[" + format + "]";
                else
                    return "IMG[" + Name + "]_FMT[" + format + "]_" + Suffix;
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
                    string texName = "";
                    ImageFormat format = ImageFormat.DXT1;

                    bool success = true;
                    Dictionary<string, string> values = Importer.ParseNameParameters(name, new string[] { "IMG", "FMT" });
                    foreach (KeyValuePair<string, string> pair in values.ToArray())
                    {
                        switch (pair.Key)
                        {
                            case "IMG":
                                texName = pair.Value;
                                break;
                            case "FMT":
                                switch (pair.Value)
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
                                    default:
                                        new Problem(ProblemTypes.WARNING, "Failed to parse format of image \"" + name + "\".");
                                        break;
                                }
                                break;
                        }
                    }

                    if (texName == "")
                    {
                        success = false;
                        new Problem(ProblemTypes.WARNING, "Failed to parse name of image \"" + name + "\".");
                    }

                    if (success)
                    {
                        HWImage newImage = new HWImage(texName, path, format);
                        newImage.ColladaName = name;
                        return newImage;
                    }
                    else
                        return null;
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
