namespace DAEnerys
{
    class SurfaceDiff
    {
        public float Fren = 0.95f;
    }
    class SurfaceGlow
    {
        //public float Power = 1f, Fren = 0.25f;
        public float Power = 2f, Fren = 0.25f;
    }
    class SurfaceSpec
    {
        public float Power = 1f, Fren = 1.5f;
    }
    class SurfaceGloss
    {
        public float Curve = 0.1f, Scale = 75f, Bias = 30f;
    }
    class SurfaceRefl
    {
        public float Power = 0.15f, Fren = 0.35f, AddMix = 0.65f;
    }
    class SurfaceFren
    {
        public float Power = 3.8f, Bias = 1.1f, Curve = 2.1f;
    }
    class SurfacePaint
    {
        public float Curve = 1.8f, Scale = -115f, Bias = -25f, Dim = 0.88f;
    }
    class SurfacePeak
    {
        public float Base = 0.008f, Paint = 0.0035f, Fren = 0.5f, Scar = 2f;
    }
}
