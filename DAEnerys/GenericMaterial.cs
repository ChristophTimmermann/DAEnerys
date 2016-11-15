using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAEnerys
{
    public class GenericMaterial
    {
        public Vector3 DiffuseColor = new Vector3(1);
        public Vector3 SpecularColor = new Vector3(1);
        public float SpecularExponent = 10;
        public float Opacity = 1.0f;

        public string DiffusePath = "";
    }
}
