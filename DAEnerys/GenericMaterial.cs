using OpenTK;

namespace DAEnerys
{
    public class GenericMaterial
    {
        public Vector3 DiffuseColor = new Vector3(1);
        public Vector3 SpecularColor = new Vector3(1);
        public float SpecularExponent = 10;
        public float Opacity = 1.0f;

        public string DiffusePath = "";
        public HWTexture DiffuseTexture;

        public GenericMaterial(Vector3 diffuseColor)
        {
            DiffuseColor = diffuseColor;
        }
    }
}
