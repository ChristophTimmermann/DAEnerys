using System;

namespace DAEnerys
{
    public class HWNavLightStyle
    {
        public string Name;

        public float ClimbTime = 0;
        public float TopWaitTime = 0;
        public float DecayTime = 0;
        public float BottomWaitTime = 0;

        public float IllumSawHz = 0;
        public float IllumSawMin = 0;
        public float IllumSawMax = 0;
        public float IllumSawOfs = 0;

        public float IllumSinHz = 0;
        public float IllumSinMin = 0;
        public float IllumSinMax = 0;
        public float IllumSinOfs = 0;

        public bool NoSelfLight = false;
        public bool LinkThrust = false;

        public HWNavLightStyle(string name, float climbTime, float topWaitTime, float decayTime, float bottomWaitTime, float illumSawHz, float illumSawMin, float illumSawMax, float illumSawOfs, float illumSinHz, float illumSinMin, float illumSinMax, float illumSinOfs, bool noSelfLight, bool linkThrust)
        {
            Name = name;

            ClimbTime = climbTime;
            TopWaitTime = topWaitTime;
            DecayTime = decayTime;
            BottomWaitTime = bottomWaitTime;

            IllumSawHz = illumSawHz;
            IllumSawMin = illumSawMin;
            IllumSawMax = illumSawMax;
            IllumSawOfs = illumSawOfs;
            IllumSinHz = illumSinHz;
            IllumSinMin = illumSinMin;
            IllumSinMax = illumSinMax;
            IllumSinOfs = illumSinOfs;

            NoSelfLight = noSelfLight;
            LinkThrust = linkThrust;

            HWData.NavLightStyles.Add(this);
            Program.main.AddNavLightStyle(this);
        }

        public static HWNavLightStyle GetByName(string name)
        {
            foreach (HWNavLightStyle style in HWData.NavLightStyles)
                if (style.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                    return style;

            return null;
        }
    }
}
