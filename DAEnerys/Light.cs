using OpenTK;
using System.Collections.Generic;

namespace DAEnerys
{
    public class Light
    {
        public static List<Light> Lights = new List<Light>();

        public Light(Vector4 position, Vector3 color, float attenuation = 0, float ambientCoefficient = 0.001f)
        {
            Position = position;
            Color = color;

            Attenuation = attenuation;
            AmbientCoefficient = ambientCoefficient;

            Lights.Add(this);
        }

        public void Destroy()
        {
            Lights.Remove(this);
        }

        public Vector4 Position;
        public Vector3 Color = new Vector3();
        public float Attenuation;
        public float AmbientCoefficient;
        public bool Enabled = true;
    }
}
