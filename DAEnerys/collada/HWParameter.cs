using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace DAEnerys
{
    class HWParameter : HWElement
    {
        public static List<HWParameter> Parameters = new List<HWParameter>();

        public override string FormattedName
        {
            get
            {
                string data = "";
                for (int i = 0; i < Data.Length; i++)
                {
                    if (i > 0)
                        data += ",";
                    data += Data[i].ToString(CultureInfo.InvariantCulture);
                }

                return "MAT[" + MaterialName + "]_" + "PARAM[" + Parameter + "]_Type[" + Type.ToString() + "]_Data[" + data + "]";
            }
        }

        public string MaterialName;
        public string Parameter;
        public ParameterType Type;
        public float[] Data = new float[0];

        public HWParameter(string materialName, string parameter, ParameterType type, float[] data) : base(materialName + "_" + parameter, HWJoint.Root)
        {
            MaterialName = materialName;
            Parameter = parameter;
            Type = type;
            Data = data;

            Parameters.Add(this);
        }

        public enum ParameterType
        {
            RGB,
            RGBA,
        }
    }
}
