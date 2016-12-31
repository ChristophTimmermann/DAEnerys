using OpenTK;

namespace DAEnerys
{
    public class GenericMaterial
    {
        private Vector3 diffuseColor = Vector3.One;
        public Vector3 DiffuseColor { get { return diffuseColor; } set { diffuseColor = value; Renderer.Invalidate(); } }
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
