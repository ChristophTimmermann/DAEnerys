using System.Collections.Generic;
using OpenTK;
using System.Globalization;
using System;

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
                string sect = "";
                if (Sect != 0)
                    sect = "_Sect[" + Sect.ToString() + "]";

                return "NAVL[" + Name + "]" + type + size + phase + frequency + color + distance + flags + sect;
            }
        }

        private HWNavLightStyle style;
        public HWNavLightStyle Style { get { return style; } set { style = value; Reset(); Renderer.Invalidate(); } }
        public float Size;
        private float phase;
        public float Phase { get { return phase; } set { phase = value; Reset(); Renderer.Invalidate(); } }
        private float frequency;
        public float Frequency { get { return frequency; } set { frequency = value; Reset(); Renderer.Invalidate(); } }
        private Vector3 color;
        public Vector3 Color { get { return color; } set { color = value; Setup(); } }
        private float distance;
        public float Distance { get { return distance; } set { distance = value; Setup(); } }
        private List<NavLightFlag> flags = new List<NavLightFlag>();
        public List<NavLightFlag> Flags { get { return flags; } set { flags = value; Setup(); } }
        public int Sect;

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

                if (RenderLight != null)
                    RenderLight.Enabled = value;

                if (RenderSprite != null)
                    RenderSprite.Visible = value;

                if (Program.main.DrawNavLightRadius)
                    if (RenderIcosphere != null)
                        RenderIcosphere.Visible = value;

                Icon.Visible = value;
            }
        }

        public HWNavLight(string name, HWJoint parent, Vector3 pos, HWNavLightStyle style, float size, float phase, float frequency, Vector3 color, float distance, List<NavLightFlag> flags, int sect) : base(name, parent, pos, Vector3.Zero, Vector3.One)
        {
            Style = style;
            Size = size;
            Phase = phase;
            Frequency = frequency;
            Color = color;
            Distance = distance;
            Flags = flags;
            Sect = sect;

            Setup();

            NavLights.Add(this);
            Program.main.AddNavLight(this);

            Visible = true;
            Program.main.CheckNavLightVisible(this, true); //Set all navlights visible by default
        }

        private void Setup()
        {
            if (RenderLight != null)
            {
                RenderLight.Destroy();
                RenderLight = null;
            }
            if (RenderIcosphere != null)
            {
                RenderIcosphere.Destroy();
                RenderIcosphere = null;
            }
            if (Icon != null)
            {
                Icon.Destroy();
                Icon = null;
            }

            //If the navlight emits light
            if (Distance > 0)
            {
                if (!Style.NoSelfLight)
                {
                    RenderLight = new Light(new Vector4(GlobalPosition, 1), color, 1 / distance, 0);
                }
                RenderIcosphere = new EditorIcosphere(this, color);
                RenderIcosphere.LocalScale = new Vector3(distance);
                RenderIcosphere.NeverDrawInFront = true;
                RenderIcosphere.Wireframe = true;
                RenderIcosphere.Visible = Program.main.DrawNavLightRadius;
            }

            //If the navlight has a sprite
            if (Flags.Contains(NavLightFlag.Sprite))
            {
                /*RenderSprite = new EditorIcon(Node.AbsolutePosition, HWData.NavLightSprite);
                RenderSprite.BlackIsTransparent = true;
                RenderSprite.Size = Size * 1.4f;
                RenderSprite.NeverDrawInFront = true;
                RenderSprite.Material.DiffuseColor = Color;*/
            }

            Icon = new EditorIcon(this, EditorIcon.LightbulbTexture);
            Icon.Visible = true;
            Icon.Size = IconSize;
            Icon.DrawAboveShip = true;
            Icon.VertexColored = false;
            Icon.Material.DiffuseColor = Color;

            Reset();

            Renderer.InvalidateView();
            Renderer.Invalidate();
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
                            waitedTime = phasedTime - Phase;
                        }
                        phasedTime += (float)Program.ElapsedSeconds;
                        break;

                    case NavLightState.BOTTOM:
                        if (waitedTime >= Style.BottomWaitTime)
                        {
                            state = NavLightState.RISE;
                            waitedTime = waitedTime - Style.BottomWaitTime;
                        }
                        break;

                    case NavLightState.RISE:
                        brightness = (float)Style.ClimbTime / waitedTime;
                        if (waitedTime >= Style.ClimbTime)
                        {
                            state = NavLightState.TOP;
                            brightness = 1;
                            waitedTime = waitedTime - Style.ClimbTime;
                        }
                        break;

                    case NavLightState.TOP:
                        if (waitedTime >= Style.TopWaitTime)
                        {
                            state = NavLightState.DECAY;
                            waitedTime = waitedTime - Style.TopWaitTime;
                        }
                        break;

                    case NavLightState.DECAY:
                        brightness = 1 - (float)(waitedTime / Style.DecayTime);
                        if (waitedTime >= Style.DecayTime)
                        {
                            state = NavLightState.BOTTOM;
                            brightness = 0;
                            waitedTime = waitedTime - Style.DecayTime;
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

        public static HWNavLight GetByName(string name)
        {
            foreach (HWNavLight light in NavLights)
                if (light.Name == name)
                    return light;

            return null;
        }

        public void Reset()
        {
            foreach (HWNavLight navLight in HWNavLight.NavLights)
            {
                if (navLight.Phase > 0)
                    navLight.state = NavLightState.SHIFT;
                else
                    navLight.state = NavLightState.BOTTOM;

                navLight.brightness = 0;
                navLight.waitedTime = 0;
                navLight.phasedTime = 0;
            }
        }

        public override void Destroy()
        {
            NavLights.Remove(this);
            Program.main.RemoveNavLight(this);
            Icon.Destroy();
            if (RenderIcosphere != null)
                RenderIcosphere.Destroy();
            if (RenderSprite != null)
                RenderSprite.Destroy();
            if (RenderLight != null)
                RenderLight.Destroy();

            base.Destroy();
        }

        public override void CalculateWorldMatrix()
        {
            base.CalculateWorldMatrix();

            if(Icon != null)
                Icon.Position = GlobalPosition;
            if(RenderLight != null)
                RenderLight.Position = new Vector4(GlobalPosition, 1);
        }
    }

    [Flags]
    public enum NavLightFlag
    {
        Sprite = 0,
        HighEnd = 1,
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
