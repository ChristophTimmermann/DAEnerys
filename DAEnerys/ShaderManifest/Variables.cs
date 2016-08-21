using System;
using System.Collections.Generic;

namespace NewShaderManifest
{
    [Serializable]
    public class Variables : Dictionary<string, dynamic>
    {
        public Variables() : base() { }

        public Variables(Variables variables) : base(variables) { }

        public new dynamic this[string key]
        {
            get
            {
                return base[key];
            }
            set
            {
                if (ContainsKey(key) && value.GetType() != base[key].GetType())
                    throw new InvalidCastException();
                base[key] = value;
            }
        }
    }
}
