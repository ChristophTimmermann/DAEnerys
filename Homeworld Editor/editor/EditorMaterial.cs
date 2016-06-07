using OpenTK;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace HomeworldDAEEditor
{
    public class EditorMaterial
    {
        public string Name;

        public Vector3 DiffuseColor = new Vector3(1, 1, 1);
        public Vector3 SpecularColor = new Vector3(0.1f, 0.1f, 0.1f);
        public float SpecularExponent = 0.3f;
        public float Opacity = 1.0f;

        public string DiffuseMap = "";
        public string SpecularMap = "";
        public string OpacityMap = "";
        public string NormalMap = "";

        public HWTexture DiffuseTexture = Renderer.DefaultTexture;

        public EditorMaterial()
        {
            EditorScene.materials.Add(this);
        }

        public EditorMaterial(string name, Vector3 diffuse, Vector3 specular, float specexponent = 1.0f, float opacity = 1.0f)
        {
            Name = name;
            DiffuseColor = diffuse;
            SpecularColor = specular;
            SpecularExponent = specexponent;
            Opacity = opacity;

            EditorScene.materials.Add(this);
        }
    }
}
