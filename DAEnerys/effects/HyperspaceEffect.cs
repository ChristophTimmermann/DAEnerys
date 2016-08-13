using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace DAEnerys
{
    public class HyperspaceEffect : EditorEffect
    {
        private static TimeFloatArray _Width;
        private static TimeFloatArray _Height;
        private static TimeVector3Array _Offset;
        // prop3
        // prop4
        private static TimeFloatArray _EdgeTexture_U_Repeat;
        private static TimeFloatArray _EdgeTexture2_U_Offset;
        private static TimeFloatArray _EdgeTexture2_U_Repeat;
        private static TimeColorArray _Colour;
        private static TimeColorArray _EdgeColour;
        // prop10
        // prop11
        // prop12
        private static string EdgeTexture = "art/fx/textures/hyperspace_edge1.tga";
        private static string EdgeTexture2 = "art/fx/textures/hyperspace_edge2.tga";

        private static bool IsInit = false;

        private static HyperspaceEffect _effect = null;
        public static HyperspaceEffect Effect {
            get {
                if (_effect == null)
                {
                    _effect = new HyperspaceEffect();
                    EditorScene.effects.Add(_effect);
                }
                return _effect;
            }
            private set { }
        }

        public Vector3 MinBounds { get; set; } = -Vector3.One;
        public Vector3 MaxBounds { get; set; } = Vector3.One;
        private static EditorEffect mesh = new EditorEffect(Vector3.Zero, null); //HWMesh.CreateSquareMesh();

        private static void Init()
        {
            _Width = new TimeFloatArray();
            _Width.Add(0f, 0f);
            _Width.Add(0.01734f, 0f);
            _Width.Add(0.03842f, 0.02500f);
            _Width.Add(0.05965f, 0.09167f);
            _Width.Add(0.07519f, 0.21667f);
            _Width.Add(0.11462f, 0.72500f);
            _Width.Add(0.12633f, 0.84167f);
            _Width.Add(0.14584f, 0.92083f);
            _Width.Add(0.16771f, 0.96250f);
            _Width.Add(0.19659f, 0.97500f);
            _Width.Add(0.85251f, 0.97500f);
            _Width.Add(0.87516f, 0.95625f);
            _Width.Add(0.89019f, 0.91875f);
            _Width.Add(0.90183f, 0.83750f);
            _Width.Add(0.91029f, 0.73750f);
            _Width.Add(0.94642f, 0.15417f);
            _Width.Add(0.95899f, 0.06458f);
            _Width.Add(0.97157f, 0.02500f);
            _Width.Add(0.98582f, 0.00208f);
            _Width.Add(1f, 0f);

            _Height = new TimeFloatArray();
            _Height.Add(0f, 0f);
            _Height.Add(0.20125f, 0f);
            _Height.Add(0.22000f, 0.01458f);
            _Height.Add(0.23179f, 0.04583f);
            _Height.Add(0.23679f, 0.10519f);
            _Height.Add(0.24307f, 0.18793f);
            _Height.Add(0.27763f, 0.64228f);
            _Height.Add(0.29082f, 0.81876f);
            _Height.Add(0.29875f, 0.90833f);
            _Height.Add(0.31280f, 0.96273f);
            _Height.Add(0.33039f, 0.99292f);
            _Height.Add(0.35249f, 0.99567f);
            _Height.Add(0.69022f, 0.99477f);
            _Height.Add(0.72533f, 0.99182f);
            _Height.Add(0.74234f, 0.96817f);
            _Height.Add(0.75481f, 0.90906f);
            _Height.Add(0.76455f, 0.82631f);
            _Height.Add(0.81242f, 0.14361f);
            _Height.Add(0.82125f, 0.07083f);
            _Height.Add(0.83500f, 0.01667f);
            _Height.Add(0.85375f, 0f);
            _Height.Add(1f, 0f);

            _Offset = new TimeVector3Array();
            _Offset.Add(0f, 0f, 0f, 1f);
            _Offset.Add(0.34328f, 0f, 0f, 1f);
            _Offset.Add(0.70299f, 0f, 0f, -1f);
            _Offset.Add(1f, 0f, 0f, -1f);

            _Colour = new TimeColorArray();
            _Colour.Add(0f, 0f, 0.12350f, 0.67925f, 1f);
            _Colour.Add(1f, 0f, 0.33139f, 0.65094f, 1f);

            _EdgeColour = new TimeColorArray();
            _EdgeColour.Add(0f, 0.14071f, 0.20260f, 0.48113f, 1f);
            _EdgeColour.Add(0.11045f, 0.08900f, 0.32558f, 0.47170f, 1f);
            _EdgeColour.Add(0.22090f, 0.00936f, 0.22293f, 0.30189f, 1f);
            _EdgeColour.Add(0.30448f, 0.21075f, 0.28116f, 0.34906f, 1f);
            _EdgeColour.Add(0.53134f, 0.18797f, 0.29391f, 0.45283f, 1f);
            _EdgeColour.Add(0.65672f, 0f, 0.29983f, 0.35849f, 1f);
            _EdgeColour.Add(0.81940f, 0.11392f, 0.23366f, 0.37736f, 1f);
            _EdgeColour.Add(0.92090f, 0.14549f, 0.12513f, 0.34906f, 1f);
            _EdgeColour.Add(1f, 0.24689f, 0.24891f, 0.35849f, 1f);

            IsInit = true;
        }

        public HyperspaceEffect() : base(Vector3.Zero, null)
        {
            if (!IsInit) Init();
        }

        public override void Start()
        {
            base.Start();
            mesh.Visible = true;
            if (!EditorScene.meshes.Contains(mesh))
            {
                EditorScene.meshes.Add(mesh);
                Renderer.UpdateMeshData();
            }
        }

        public override void Stop()
        {
            base.Stop();
            mesh.Visible = false;
        }

        protected override void __Update()
        {
            Vector3 Size = (MaxBounds - MinBounds) * 1.2f;
            Vector3 Center = (MaxBounds + MinBounds) / 2f;

            float time = (float)timer.Elapsed.TotalSeconds / 15f;
            mesh.Width = _Width.At(time) * Size.X / 2f;
            mesh.Height = _Height.At(time) * Size.Y / 2f;
            Position = _Offset.At(time) * Size / 2f + Center;

            if (time >= 1f)
                Stop();
        }
    }
}
