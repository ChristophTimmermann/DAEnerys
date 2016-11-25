using System.Collections.Generic;
using OpenTK;
using System.Globalization;

namespace DAEnerys
{
    public class HWNavLight : HWElement
    {
        //Statics
        public static List<HWNavLight> NavLights = new List<HWNavLight>();

        private static float iconSize = 3;
        public static float IconSize { get { return iconSize; } set { iconSize = value; foreach (HWNavLight navLight in HWNavLight.NavLights) { navLight.Icon.Size = value; } } }

        public int NavLightListItemIndex;

        public override string FormattedName
        {
            get
            {
                string type = "_Type[" + Style.Name + "]";
                string size = "_Sz[" + Size.ToString(CultureInfo.InvariantCulture) + "]";
                string phase = "_Ph[" + Phase.ToString(CultureInfo.InvariantCulture) + "]";
                string frequency = "_Fr[" + Frequency.ToString(CultureInfo.InvariantCulture) + "]";
                string color = "_Col[" + Color.X.ToString(CultureInfo.InvariantCulture) + "," + Color.Y.ToString(CultureInfo.InvariantCulture) + "," + Color.Z.ToString(CultureInfo.InvariantCulture) + "]";
                string distance = "_Dist[" + Distance.ToString(CultureInfo.InvariantCulture) + "]";
                string flags = "";
                if (Flags.Count > 0)
                {
                    flags = "_Flags[";
                    for (int i = 0; i < Flags.Count; i++)
                    {
                        flags += Flags[i];
                        if (i < Flags.Count - 1)
                            flags += " ";
                    }
                    flags += "]";
                }

                return "NAVL[" + Name + "]" + type + size + phase + frequency + color + distance + flags;
            }
        }

        public HWNavLightStyle Style;
        public float Size;
        public float Phase;
        public float Frequency;
        public Vector3 Color;
        public float Distance;
        public List<NavLightFlag> Flags;

        //Editor
        public EditorIcon Icon;

        //Rendering
        public Light RenderLight;
        public EditorIcosphere RenderIcosphere;
        public EditorIcon RenderSprite;

        //Logic
        private NavLightState state;
        private float waitedTime;
        private float phasedTime;
        private float brightness;

        private bool visible;
        public bool Visible
        {
            get { return visible; }
            set
            {
                visible = value;
                
                if(RenderLight != null)
                    RenderLight.Enabled = value;

                if (RenderSprite != null)
                    RenderSprite.Visible = value;

                if (Program.main.DrawNavLightRadius)
                    if (RenderIcosphere != null)
                        RenderIcosphere.Visible = value;

                Icon.Visible = value;
            }
        }

        public HWNavLight(string name, HWJoint parent, Matrix4 transform, HWNavLightStyle style, float size, float phase, float frequency, Vector3 color, float distance, List<NavLightFlag> flags) : base(name, parent, transform)
        {
            Style = style;
            Size = size;
            Phase = phase;
            Frequency = frequency;
            Color = color;
            Distance = distance;
            Flags = flags;

            //If the navlight emits light
            if(Distance > 0)
            {
                if (!Style.NoSelfLight)
                {
                    RenderLight = new Light(new Vector4(AbsolutePosition, 1), color, 1 / distance, 0);
                }
                RenderIcosphere = new EditorIcosphere(this, color);
                RenderIcosphere.Scale = new Vector3(distance);
                RenderIcosphere.NeverDrawInFront = true;
                RenderIcosphere.Wireframe = true;
                RenderIcosphere.Visible = false;
            }

            //If the navlight has a sprite
            if(Flags.Contains(NavLightFlag.SPRITE))
            {
                /*RenderSprite = new EditorIcon(Node.AbsolutePosition, HWData.NavLightSprite);
                RenderSprite.BlackIsTransparent = true;
                RenderSprite.Size = Size * 1.4f;
                RenderSprite.NeverDrawInFront = true;
                RenderSprite.Material.DiffuseColor = Color;*/
            }

            Icon = new EditorIcon(AbsolutePosition, EditorIcon.LightbulbTexture);
            Icon.Visible = true;
            Icon.Size = IconSize;
            Icon.DrawAboveShip = true;
            Icon.VertexColored = false;
            Icon.Material.DiffuseColor = Color;

            if (Phase > 0)
                state = NavLightState.SHIFT;
            else
                state = NavLightState.BOTTOM;

            NavLights.Add(this);
            Program.main.AddNavLight(this);

            Visible = true;
            Program.main.CheckNavLightVisible(this, true); //Set all navlights visible by default
        }

        public void Update()
        {
            if (Frequency > 0)
            {
                switch (state)
                {
                    case NavLightState.SHIFT:
                        if (phasedTime >= Phase)
                        {
                            state = NavLightState.BOTTOM;
                            brightness = 0;
                            waitedTime = 0;
                        }
                        phasedTime += (float)Program.ElapsedSeconds;
                        break;

                    case NavLightState.BOTTOM:
                        if (waitedTime >= Style.BottomWaitTime)
                        {
                            state = NavLightState.RISE;
                            waitedTime = 0;
                        }
                        break;

                    case NavLightState.RISE:
                        brightness = (float)Style.ClimbTime / waitedTime;
                        if (waitedTime >= Style.ClimbTime)
                        {
                            state = NavLightState.TOP;
                            brightness = 1;
                            waitedTime = 0;
                        }
                        break;

                    case NavLightState.TOP:
                        if (waitedTime >= Style.TopWaitTime)
                        {
                            state = NavLightState.DECAY;
                            waitedTime = 0;
                        }
                        break;

                    case NavLightState.DECAY:
                        brightness = 1 - (float)(waitedTime / Style.DecayTime);
                        if (waitedTime >= Style.DecayTime)
                        {
                            state = NavLightState.BOTTOM;
                            brightness = 0;
                            waitedTime = 0;
                        }
                        break;
                }
            }
            else
                brightness = 1;

            if (Style.LinkThrust) //If the navlight brightness is modified by the thruster strength
                brightness *= Renderer.ThrusterInterpolation;

            if (RenderSprite != null)
            {
                RenderSprite.Material.DiffuseColor = Color * brightness;
                RenderSprite.Material.Opacity = brightness;
            }

            if (RenderIcosphere != null)
            {
                RenderIcosphere.Material.DiffuseColor = Color * brightness;
            }

            Icon.Material.DiffuseColor = Color * brightness;

            if (RenderLight != null)
                RenderLight.Color = Color * brightness * 3;

            waitedTime += (float)Program.ElapsedSeconds * Frequency;
        }
    }

    public enum NavLightFlag
    {
        SPRITE = 1,
        HIGHEND = 2,
    }

    enum NavLightState
    {
        SHIFT = 0,
        BOTTOM = 1,
        RISE = 2,
        TOP = 3,
        DECAY = 4,
    }
}
