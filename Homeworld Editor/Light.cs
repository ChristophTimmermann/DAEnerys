using OpenTK;

namespace HomeworldDAEEditor
{
    class Light
    {
        public Light(Vector4 position, Vector3 color, float attenuation = 0, float ambientCoefficient = 0.001f)
        {
            Position = position;
            Color = color;

            Attenuation = attenuation;
            AmbientCoefficient = ambientCoefficient;
        }

        public Vector4 Position;
        public Vector3 Color = new Vector3();
        public float Attenuation;
        public float AmbientCoefficient;
    }
}
