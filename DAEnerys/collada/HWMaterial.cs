using OpenTK;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace DAEnerys
{
    public class HWMaterial
    {
        public string Name;
        public string Shader = "default";
        public ImageFormat Format = ImageFormat.DXT1;

        public Vector3 DiffuseColor = new Vector3(1);
        public Vector3 SpecularColor = new Vector3(1);
        public float SpecularExponent = 10;
        public float Opacity = 1.0f;

        public string DiffuseMap = "";

        public object MaterialListItem;

        public HWTexture DiffuseTexture;
        public HWTexture GlowTexture;
        public HWTexture ThrusterOffDiffuseTexture;
        public HWTexture ThrusterOffGlowTexture;
        public HWTexture NormalTexture;
        public HWTexture SpecularTexture;
        public HWTexture TeamTexture;
        public HWTexture StripeTexture;

        public List<HWImage> Images = new List<HWImage>();

        public HWMaterial()
        {
            HWScene.Materials.Add(this);
        }

        public HWMaterial(string name, Vector3 diffuse, Vector3 specular, float specexponent = 1.0f, float opacity = 1.0f)
        {
            Name = name;
            DiffuseColor = diffuse;
            SpecularColor = specular;
            SpecularExponent = specexponent;
            Opacity = opacity;

            HWScene.Materials.Add(this);
        }

        public void Parse()
        {
            String fullName = Name;
            if (Name.StartsWith("MAT[")) //If material is a homeworld valid material
            {
                string[] splitted = Name.Split('[');
                int end = -1;

                for (int i = 0; i < splitted.Length; i++)
                {
                    if (i != 0)
                    {
                        end = splitted[i].IndexOf(']');
                        if (end < 0) {
                            Problem.Problems.Add(new Problem(ProblemTypes.ERROR,
                                "Material parsing error: '" + fullName + "' has an invalid name format."));
                        } else if (splitted[i - 1].EndsWith("MAT")) //Name
                        {
                            Name = splitted[i].Substring(0, end);
                        }
                        else if (splitted[i - 1].EndsWith("SHD")) //Shader
                        {
                            Shader = splitted[i].Substring(0, end);
                        }
                    }
                }

                //Set diffuse image
                foreach(HWImage image in HWScene.Images)
                {
                    if(image.Path.Replace("file://", "") == DiffuseMap)
                    {
                        string realDiffuseName = Path.GetFileNameWithoutExtension(image.Path);
                        image.Name = realDiffuseName;
                        Images.Add(image);
                        image.Material = this;
                        Format = image.Format;

                        //Search for other images in the diffuse image folder
                        string diffuseName = Path.GetFileNameWithoutExtension(image.Path);
                        diffuseName = diffuseName.Remove(diffuseName.Length - 4);
                        int underspaceIndex = diffuseName.LastIndexOf('_');

                        if (underspaceIndex == -1)
                            continue;

                        string diffusePrefix = diffuseName.Remove(underspaceIndex);

                        //string diffusePath = new Uri(image.Path).LocalPath;
                        //string diffusePath = Path.GetFullPath(image.Path);
                        //diffusePath = diffusePath.Remove(0, 2);

                        string absolutePath = Path.Combine(HWScene.ColladaPath, image.Path.Replace("file://", ""));
                        absolutePath = Path.GetDirectoryName(absolutePath);

                        if (!Directory.Exists(absolutePath))
                            continue;

                        string[] files = Directory.GetFiles(absolutePath);
                        foreach(string file in files)
                        {
                            string fileName = Path.GetFileNameWithoutExtension(file);
                            int fileUnderspaceIndex = fileName.LastIndexOf('_');
                            if (fileUnderspaceIndex != -1)
                            {
                                string suffix = fileName.Substring(fileUnderspaceIndex + 1);
                                string prefix = fileName.Remove(fileUnderspaceIndex);
                                if (prefix == diffusePrefix)
                                {
                                    switch(suffix)
                                    {
                                        case "DIFF":
                                            DiffuseTexture = new HWTexture(file);
                                            break;
                                        case "GLOW":
                                            GlowTexture = new HWTexture(file);
                                            break;
                                        case "GLOX":
                                            ThrusterOffGlowTexture = new HWTexture(file);
                                            break;
                                        case "DIFX":
                                            ThrusterOffDiffuseTexture = new HWTexture(file);
                                            break;
                                        case "NORM":
                                            NormalTexture = new HWTexture(file);
                                            break;
                                        case "SPEC":
                                            SpecularTexture = new HWTexture(file);
                                            break;
                                        case "TEAM":
                                            TeamTexture = new HWTexture(file, true);
                                            break;
                                        case "STRP":
                                            StripeTexture = new HWTexture(file, true);
                                            break;
                                    }

                                    HWImage newImage = new HWImage(fileName, file);
                                    newImage.Material = this;
                                    Images.Add(newImage);
                                }
                            }
                        }
                        break;
                    }
                }

                Program.main.AddMaterial(this);
            }
        }
    }

    //Currently unused
    public enum MaterialSuffix
    {
        DIFF = 1,
        GLOW = 2,
        SPEC = 3,
        REFL = 4,
        TEAM = 5,
        STRP = 6,
        PAIN = 7,
        NORM = 8,
        PROG = 9,
        DIFX = 10,
        GLOX = 11,
        CLD1 = 12,
        CLD2 = 13,
        CLD3 = 14,
        WARP = 15,
        MASK = 16,
        NOIZ = 17,
    }
}
