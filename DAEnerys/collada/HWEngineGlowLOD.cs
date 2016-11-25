using OpenTK;

namespace DAEnerys
{
    public class HWEngineGlowLOD : HWMesh
    {
        public HWEngineGlow GlowMesh;
        public int LOD;

        public override string FormattedName
        {
            get
            {
                string lod = "_LOD[" + LOD + "]";

                return "GLOW[" + Name + "]" + lod;
            }
        }

        public HWEngineGlowLOD(MeshData data, Matrix4 transform, HWEngineGlow glowMesh, int lod) : base(data, transform, new HWMaterial("fx_eng_glowbasic"))
        {
            GlowMesh = glowMesh;
            LOD = lod;
            Parent = GlowMesh.Parent;
            this.Name = glowMesh.Name;

            Translucent = true;

            GlowMesh.AddLODMesh(this);
        }
    }
}
