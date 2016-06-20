using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworldDAEEditor
{
    public abstract class Material
    {
        public static List<Material> Materials = new List<Material>();

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

        public Material()
        {
            Material.Materials.Add(this);
        }
    }
}
