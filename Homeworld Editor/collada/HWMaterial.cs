using OpenTK;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace HomeworldDAEEditor
{
    public class HWMaterial
    {
        public string Name;
        public string Shader = "default";

        public Vector3 AmbientColor = new Vector3();
        public Vector3 DiffuseColor = new Vector3();
        public Vector3 SpecularColor = new Vector3();
        public float SpecularExponent = 1;
        public float Opacity = 1.0f;

        public string AmbientMap = "";
        public string DiffuseMap = "grey.jpg";
        public string SpecularMap = "";
        public string OpacityMap = "";
        public string NormalMap = "";

        public HWTexture DiffuseTexture = Renderer.defaultTexture;

        public HWMaterial()
        {
            HWScene.Materials.Add(this);
        }

        public HWMaterial(string name, Vector3 ambient, Vector3 diffuse, Vector3 specular, float specexponent = 1.0f, float opacity = 1.0f)
        {
            Name = name;
            AmbientColor = ambient;
            DiffuseColor = diffuse;
            SpecularColor = specular;
            SpecularExponent = specexponent;
            Opacity = opacity;

            HWScene.Materials.Add(this);
        }
    }
}
