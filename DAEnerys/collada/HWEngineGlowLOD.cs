using Assimp;

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

        public HWEngineGlowLOD(Mesh assimpMesh, HWEngineGlow glowMesh, int lod) : base(assimpMesh)
        {
            GlowMesh = glowMesh;
            LOD = lod;
            this.Name = glowMesh.Name;

            Translucent = true;
            Material.Shader = "fx_eng_glowbasic";
        }
    }
}
