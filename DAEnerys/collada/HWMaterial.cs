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
        public HWTexture DiffuseOffTexture;
        public HWTexture GlowOffTexture;
        public HWTexture NormalTexture;
        public HWTexture SpecularTexture;
        public HWTexture TeamTexture;
        public HWTexture ProgressTexture;
        public HWTexture CloudsTexture;
        public HWTexture MaskTexture;
        public HWTexture NoiseTexture;
        public HWTexture WarpTexture;

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
            string fullName = Name;
            string DIFF = "";
            string GLOW = "", SPEC = "", REFL = "";
            string TEAM = "", STRP = "", PAIN = "";
            string NORM = "";
            string PROG = "";
            string DIFX = "";
            string GLOX = "", SPEX = "", REFX = "";
            string CLD1 = "", CLD2 = "", CLD3 = "";
            string WARP = "";
            string MASK = "";
            string NOIZ = "";
            
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

                foreach(HWImage image in HWScene.Images)
                {
                    if(image.Path.Replace("file://", "") == DiffuseMap)
                    {
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
                                            DIFF = file;
                                            break;
                                        case "GLOW":
                                            GLOW = file;
                                            break;
                                        case "GLOX":
                                            GLOX = file;
                                            break;
                                        case "DIFX":
                                            DIFX = file;
                                            break;
                                        case "NORM":
                                            NORM = file;
                                            break;
                                        case "REFL":
                                            REFL = file;
                                            break;
                                        case "SPEC":
                                            SPEC = file;
                                            break;
                                        case "TEAM":
                                            TEAM = file;
                                            break;
                                        case "STRP":
                                            STRP = file;
                                            break;
                                        case "PAIN":
                                            PAIN = file;
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

            char[] splitter = new char[] { ',' };
            if (InArray(Shader.ToLower(), "ship,matte,matte2s,monolith,megalith,fxMatte,badge".Split(splitter)))
            {
                DiffuseTexture = HWTexture.MakeTexture(Name + "_DIFF", DIFF, 1, 1, 1, 1);
                GlowTexture = HWTexture.MakeMultTexture(Name + "_GLOW", REFL, GLOW, SPEC, 0, 0, 0, 1);
                TeamTexture = HWTexture.MakeMultTexture(Name + "_TEAM", TEAM, STRP, PAIN, 0, 0, 0, 1, true, true, true, false);
                NormalTexture = HWTexture.MakeTexture(Name + "_NORM", NORM, 5, 5, 1, 1);
            }
            else if (InArray(Shader.ToLower(), "mattealpha,mattealpha2s,mattescissor,mattescissor2s,fxMatte_a,fxMatte_s".Split(splitter)))
            {
                DiffuseTexture = HWTexture.MakeTexture(Name + "_DIFF", DIFF, 1, 1, 1, 1, true);
                GlowTexture = HWTexture.MakeMultTexture(Name + "_GLOW", REFL, GLOW, SPEC, 0, 0, 0, 1);
                TeamTexture = HWTexture.MakeMultTexture(Name + "_TEAM", TEAM, STRP, PAIN, 0, 0, 0, 1, true, true, true, true);
                NormalTexture = HWTexture.MakeTexture(Name + "_NORM", NORM, 5, 5, 1, 1);
            }
            else if (InArray(Shader.ToLower(), "fx_burn,salvage_burn,ore".Split(splitter)))
            {
                DiffuseTexture = HWTexture.MakeTexture(Name + "_DIFF", DIFF, 1, 1, 1, 1, Shader.ToLower() != "ore");
                GlowTexture = HWTexture.MakeTexture(Name + "_GLOW", GLOW, 0, 0, 0, 1);
                SpecularTexture = HWTexture.MakeMultTexture(Name + "_SPEC", REFL, PAIN, SPEC, 0, 0, 0, 1);
                ProgressTexture = HWTexture.MakeTexture(Name + "_PROG", PROG, 1, 1, 1, 1);
                NormalTexture = HWTexture.MakeTexture(Name + "_NORM", NORM, 5, 5, 1, 1);
            }
            else if (InArray(Shader.ToLower(), "shipglow,shipglow_ns,badgeglow".Split(splitter)))
            {
                DiffuseTexture = HWTexture.MakeTexture(Name + "_DIFF", DIFF, 1, 1, 1, 1);
                SpecularTexture = HWTexture.MakeMultTexture(Name + "_SPEC", REFL, "", SPEC, 0, 0, 0, 1);
                GlowTexture = HWTexture.MakeTexture(Name + "_GLOW", GLOW, 0, 0, 0, 1);
                TeamTexture = HWTexture.MakeMultTexture(Name + "_TEAM", TEAM, STRP, PAIN, 0, 0, 0, 1, true, true, true, true);
                NormalTexture = HWTexture.MakeTexture(Name + "_NORM", NORM, 5, 5, 1, 1);
            }
            else if (Shader.ToLower() == "bay")
            {
                DiffuseTexture = HWTexture.MakeTexture(Name + "_DIFF", DIFF, 1, 1, 1, 1);
                GlowTexture = HWTexture.MakeMultTexture(Name + "_GLOW", REFL, GLOW, SPEC, 0, 0, 0, 1);
                TeamTexture = HWTexture.MakeMultTexture(Name + "_TEAM", TEAM, STRP, PAIN, 0, 0, 0, 1, true, true, true, true);
                NormalTexture = HWTexture.MakeTexture(Name + "_NORM", NORM, 5, 5, 1, 1);
            }
            else if (Shader.ToLower() == "thruster")
            {
                DiffuseTexture = HWTexture.MakeTexture(Name + "_DIFF", DIFF, 1, 1, 1, 1);
                DiffuseOffTexture = HWTexture.MakeTexture(Name + "_DIFX", DIFX, 1, 1, 1, 1);
                GlowTexture = HWTexture.MakeMultTexture(Name + "_GLOW", REFL, GLOW, SPEC, 0, 0, 0, 1);
                GlowOffTexture = HWTexture.MakeMultTexture(Name + "_GLOX", REFX, GLOX, SPEX, 0, 0, 0, 1);
                TeamTexture = HWTexture.MakeMultTexture(Name + "_TEAM", TEAM, STRP, PAIN, 0, 0, 0, 1, true, true, true, true);
                NormalTexture = HWTexture.MakeTexture(Name + "_NORM", NORM, 5, 5, 1, 1);
            }
            else if (Shader.ToLower() == "background")
            {
                DiffuseTexture = HWTexture.MakeTexture(Name + "_DIFF", DIFF, 0, 0, 0, 1);
            }
            else if (Shader.ToLower() == "bg_planet")
            {
                DiffuseTexture = HWTexture.MakeTexture(Name + "_DIFF", DIFF, 1, 1, 1, 1); // terrain
                GlowTexture = HWTexture.MakeTexture(Name + "_GLOW", GLOW, 0, 0, 0, 1); // night
                SpecularTexture = HWTexture.MakeMultTexture(Name + "_SPEC", REFL, "", SPEC, 0, 0, 0, 1); // enviro
                CloudsTexture = HWTexture.MakeMultTexture(Name + "_CLDS", CLD1, CLD2, CLD3, 0, 0, 0, 1); // clouds
                WarpTexture = HWTexture.MakeTexture(Name + "_WARP", WARP, 5, 5, 0, 1); // warp
                NormalTexture = HWTexture.MakeTexture(Name + "_NORM", NORM, 5, 5, 1, 1);
            }
            else if (Shader.ToLower() == "bg_planetmelt")
            {
                DiffuseTexture = HWTexture.MakeTexture(Name + "_DIFF", DIFF, 1, 1, 1, 1); // terrain
                DiffuseOffTexture = HWTexture.MakeTexture(Name + "_DIFX", DIFX, 1, 1, 1, 1); // scorched
                GlowTexture = HWTexture.MakeTexture(Name + "_GLOW", GLOW, 0, 0, 0, 1); // night
                GlowOffTexture = HWTexture.MakeTexture(Name + "_GLOX", GLOX, 0, 0, 0, 1); // burn
                SpecularTexture = HWTexture.MakeMultTexture(Name + "_SPEC", REFL, "", SPEC, 0, 0, 0, 1); // enviro
                CloudsTexture = HWTexture.MakeMultTexture(Name + "_CLDS", CLD1, CLD2, CLD3, 0, 0, 0, 1); // clouds
                WarpTexture = HWTexture.MakeTexture(Name + "_WARP", WARP, 5, 5, 0, 1); // warp
                NormalTexture = HWTexture.MakeTexture(Name + "_NORM", NORM, 5, 5, 1, 1);
            }
            else if (InArray(Shader.ToLower(), "bg_moon,bg_planetoid".Split(splitter)))
            {
                DiffuseTexture = HWTexture.MakeTexture(Name + "_DIFF", DIFF, 1, 1, 1, 1); // terrain
                GlowTexture = HWTexture.MakeTexture(Name + "_GLOW", GLOW, 0, 0, 0, 1); // night
                SpecularTexture = HWTexture.MakeMultTexture(Name + "_SPEC", REFL, "", SPEC, 0, 0, 0, 1); // enviro
                NormalTexture = HWTexture.MakeTexture(Name + "_NORM", NORM, 5, 5, 1, 1);
            }
            else if (InArray(Shader.ToLower(), "bg_cosmic,bg_cosmic_a,bg_cosmic_an".Split(splitter)))
            {
                bool loadAlpha = Shader.ToLower() != "bg_cosmic";
                DiffuseTexture = HWTexture.MakeTexture(Name + "_DIFF", DIFF, 1, 1, 1, 1, loadAlpha); // matter
                MaskTexture = HWTexture.MakeTexture(Name + "_MASK", MASK, 1, 1, 1, 1, loadAlpha); // mask
                WarpTexture = HWTexture.MakeTexture(Name + "_WARP", WARP, 5, 5, 0, 1); // warp
                if (Shader.ToLower() != "bg_cosmic_an")
                    NormalTexture = HWTexture.MakeTexture(Name + "_NORM", NORM, 5, 5, 1, 1);
            }
            else if (InArray(Shader.ToLower(), "fx,fxSolid,fxSolidAlphaTest,fx_a,fx_s,fx_harvest,res_tendril,res_dustvein".Split(splitter)))
            {
                DiffuseTexture = HWTexture.MakeTexture(Name + "_DIFF", DIFF, 1, 1, 1, 1, true);
                if (Shader.ToLower() == "res_dustvein")
                    NoiseTexture = HWTexture.MakeTexture(Name + "_NOIZ", NOIZ, 1, 1, 1, 1, true);
            }
            else if (Shader.ToLower() == "nis_galaxy_disc")
            {
                DiffuseTexture = HWTexture.MakeTexture(Name + "_DIFF", DIFF, 0, 0, 0, 1); // disc
                MaskTexture = HWTexture.MakeTexture(Name + "_MASK", MASK, 0, 0, 0, 1); // masks
            }
            else if (Shader.ToLower() == "nis_galaxy_vectors")
            {
                DiffuseTexture = HWTexture.MakeTexture(Name + "_DIFF", DIFF, 0, 0, 0, 1);
            }
            else if (InArray(Shader.ToLower(), "dustCloudFlash,dustCloudNebula,dustCloud".Split(splitter)))
            {
                DiffuseTexture = HWTexture.MakeTexture(Name + "_DIFF", DIFF, 1, 1, 1, 1, true);
            }
            else
            {
                if (Name == "red")
                    DiffuseTexture = HWTexture.MakeTexture(Name + "_DIFF", "", 1, 0, 0, 1);
                else if (Name == "green")
                    DiffuseTexture = HWTexture.MakeTexture(Name + "_DIFF", "", 0, 1, 0, 1);
                else if (Name == "blue")
                    DiffuseTexture = HWTexture.MakeTexture(Name + "_DIFF", "", 0, 0, 1, 1);
                else if (Name == "white")
                    DiffuseTexture = HWTexture.MakeTexture(Name + "_DIFF", "", 1, 1, 1, 1);
                else if (Name == "black")
                    DiffuseTexture = HWTexture.MakeTexture(Name + "_DIFF", "", 0, 0, 0, 1);
                else
                    DiffuseTexture = HWTexture.MakeTexture(Name + "_DIFF", DIFF, 1, 1, 1, 1, true);
            }
        }

        private bool InArray(string needle, string[] haystack)
        {
            foreach (string straw in haystack)
            {
                if (needle == straw) return true;
            }
            return false;
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
