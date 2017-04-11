using HWShaderManifest;
using OpenTK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DAEnerys
{
    public class HWMaterial : GenericMaterial
    {
        public static List<HWMaterial> Materials = new List<HWMaterial>();
        public static HWMaterial DefaultMaterial;

        public string Name = string.Empty;
        public int Suffix = -1;

        private string shader = "default";
        public string Shader { get { return shader; } set { shader = value; Renderer.Invalidate(); } }

        public bool Valid = true;
        public ImageFormat Format { get { if (Images.Count > 0) return Images[0].Format; else return ImageFormat.DXT1; } set { foreach (HWImage image in Images) image.Format = value; } }

        public string FormattedName
        {
            get
            {
                if (Suffix == -1)
                    return Name;

                if (Suffix <= 1)
                    return "MAT[" + Name + "]_SHD[" + Shader + "]";
                else
                    return "MAT[" + Name + "]_SHD[" + Shader + "]_" + Suffix;
            }
        }

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

        public HWMaterial() : base(Vector3.One)
        {
            Materials.Add(this);
        }

        public HWMaterial(string shader) : base(Vector3.One)
        {
            Shader = shader;

            Materials.Add(this);
        }

        public HWMaterial(string name, Vector3 diffuse, Vector3 specular, float specexponent = 1.0f, float opacity = 1.0f) : base(diffuse)
        {
            Name = name;
            SpecularColor = specular;
            SpecularExponent = specexponent;
            Opacity = opacity;

            Materials.Add(this);
        }

        public void Destroy()
        {
            Materials.Remove(this);
            Program.main.RemoveMaterial(this);
            foreach (HWMesh mesh in HWMesh.Meshes)
                if (mesh.Material == this)
                    mesh.Material = HWMaterial.DefaultMaterial;

            HWImage[] images = Images.ToArray();
            for (int i = 0; i < images.Length; i++)
                images[i].Destroy();
            Images.Clear();
        }

        public void Parse()
        {
            string fullName = Name;

            if (Name.StartsWith("MAT[")) //If material is a homeworld valid material
            {
                Suffix = 1;

                Dictionary<string, string> values = Importer.ParseNameParameters(Name, new string[] { "MAT", "SHD" });
                foreach (KeyValuePair<string, string> pair in values.ToArray())
                {
                    switch (pair.Key)
                    {
                        case "MAT":
                            Name = pair.Value;
                            break;
                        case "SHD":
                            Shader = pair.Value;
                            break;
                    }
                }

                if(Name == "")
                    new Problem(ProblemTypes.WARNING, "Failed to parse name of material \"" + fullName + "\".");
                
                if (!ShaderManifest.ContainsHODAlias(Shader))
                    new Problem(ProblemTypes.WARNING, "Unknown shader \"" + Shader + "\" of material \"" + fullName + "\".");

                LoadTextures();

                Program.main.AddMaterial(this);
            }
        }

        public void LoadTextures()
        {
            Images.Clear();

            Dictionary<string, string> paths = new Dictionary<string, string>();
            paths.Add("DIFF", "");
            paths.Add("GLOW", "");
            paths.Add("SPEC", "");
            paths.Add("REFL", "");
            paths.Add("TEAM", "");
            paths.Add("STRP", "");
            paths.Add("PAIN", "");
            paths.Add("NORM", "");
            paths.Add("PROG", "");
            paths.Add("DIFX", "");
            paths.Add("GLOX", "");
            paths.Add("SPEX", "");
            paths.Add("REFX", "");
            paths.Add("CLD1", "");
            paths.Add("CLD2", "");
            paths.Add("CLD3", "");
            paths.Add("WARP", "");
            paths.Add("MASK", "");
            paths.Add("NOIZ", "");

            foreach (HWImage image in HWImage.Images)
            {
                if (image.Path == DiffusePath)
                {
                    Format = image.Format;
                    Images.Add(image);
                    image.Material = this;

                    //Search for other images in the diffuse image folder
                    string diffuseName = Path.GetFileNameWithoutExtension(image.Path);
                    diffuseName = diffuseName.Remove(diffuseName.Length - 4);
                    int underspaceIndex = diffuseName.LastIndexOf('_');

                    if (underspaceIndex == -1)
                        continue;

                    string diffusePrefix = diffuseName.Remove(underspaceIndex);

                    string absolutePath = image.Path.Replace("file://", "");
                    absolutePath = Path.GetDirectoryName(absolutePath);
                    if (Importer.ColladaPath.Length > 0)
                    {
                        absolutePath = Path.Combine(Importer.ColladaPath, image.Path.Replace("file://", ""));
                        absolutePath = Path.GetDirectoryName(absolutePath);
                    }

                    if (!Directory.Exists(absolutePath))
                        continue;

                    string[] files = Directory.GetFiles(absolutePath);
                    foreach (string file in files)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(file);
                        string extension = Path.GetExtension(file).ToLower();
                        if (extension != ".tga" && extension != ".png" && extension != ".jpg" && extension != ".dds")
                            continue;

                        int fileUnderspaceIndex = fileName.LastIndexOf('_');
                        if (fileUnderspaceIndex != -1)
                        {
                            string suffix = fileName.Substring(fileUnderspaceIndex + 1);
                            string prefix = fileName.Remove(fileUnderspaceIndex);
                            if (prefix == diffusePrefix)
                            {
                                if (paths.ContainsKey(suffix))
                                {
                                    paths[suffix] = file;
                                }

                                if (suffix != "DIFF" && paths.ContainsKey(suffix)) //Only load textures that are actually used by HODOR/the shaders
                                {
                                    HWImage newImage = new HWImage(fileName, file);
                                    newImage.Material = this;
                                    Images.Add(newImage);
                                }
                            }
                        }
                    }
                    break;
                }
            }

            string DIFF = paths["DIFF"];
            string REFL = paths["REFL"];
            string GLOW = paths["GLOW"];
            string SPEC = paths["SPEC"];
            string TEAM = paths["TEAM"];
            string STRP = paths["STRP"];
            string PAIN = paths["PAIN"];
            string NORM = paths["NORM"];
            string PROG = paths["PROG"];
            string DIFX = paths["DIFX"];
            string REFX = paths["REFX"];
            string GLOX = paths["GLOX"];
            string SPEX = paths["SPEX"];
            string CLD1 = paths["CLD1"];
            string CLD2 = paths["CLD2"];
            string CLD3 = paths["CLD3"];
            string WARP = paths["WARP"];
            string MASK = paths["MASK"];
            string NOIZ = paths["NOIZ"];

            char[] splitter = new char[] { ',' };
            if (InArray(Shader.ToLower(), "ship,matte,matte2s,monolith,megalith,fxMatte,badge,shipanim".Split(splitter)))
            {
                DiffuseTexture = HWTexture.MakeTexture(Name + "_DIFF", DIFF, 1, 1, 1, 1);
                GlowTexture = HWTexture.MakeMultTexture(Name + "_GLOW", REFL, GLOW, SPEC, 0, 0, 0, 1);
                TeamTexture = HWTexture.MakeMultTexture(Name + "_TEAM", TEAM, STRP, PAIN, 0, 0, 0, 1, true, true, true, true);
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
            else if (InArray(Shader.ToLower(), "fx,fxsolid,fxsolidalphatest,fx_a,fx_s,fx_harvest,res_tendril,res_dustvein".Split(splitter)))
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
            else if (InArray(Shader.ToLower(), "dustcloudflash,dustcloudnebula,dustcloud".Split(splitter)))
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
                    DiffuseTexture = HWTexture.MakeTexture(Name + "_DIFF", "", 0.5f, 0.5f, 0.5f, 1f);
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

        public static HWMaterial GetByName(string name)
        {
            foreach (HWMaterial material in Materials)
                if (material.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                    return material;

            return null;
        }
    }

    public enum MaterialSuffix
    {
        DIFF,
        GLOW,
        GLOX,
        SPEC,
        SPEX,
        REFL,
        REFX,
        TEAM,
        STRP,
        PAIN,
        NORM,
        PROG,
        DIFX,
        CLD1,
        CLD2,
        CLD3,
        WARP,
        MASK,
        NOIZ,
    }
}
