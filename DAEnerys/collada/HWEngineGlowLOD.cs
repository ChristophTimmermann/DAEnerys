using OpenTK;

namespace DAEnerys
{
    public class HWEngineGlowLOD : HWMesh
    {
        public HWEngineGlow EngineGlow;
        public int LOD;

        public override string FormattedName
        {
            get
            {
                string lod = "_LOD[" + LOD + "]";

                return "GLOW[" + Name + "]" + lod;
            }
        }

        public HWEngineGlowLOD(MeshData data, Matrix4 transform, HWEngineGlow glowMesh, int lod) : base(data, glowMesh.Parent, transform, new HWMaterial("fx_eng_glowbasic"))
        {
            EngineGlow = glowMesh;
            LOD = lod;
            this.Name = glowMesh.Name;

            Translucent = true;

            EngineGlow.AddLODMesh(this);
        }

        public override void Destroy()
        {
            EngineGlow.Meshes.Remove(this);
            EngineGlow.LODMeshes[LOD].Remove(this);
            EngineGlow = null;

            base.Destroy();
        }
    }
}
