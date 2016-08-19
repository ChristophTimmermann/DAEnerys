using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAEnerys
{
    public class HWEngineGlowLOD
    {
        public HWEngineGlow GlowMesh;

        public HWMesh Mesh;
        public int LOD;

        public HWEngineGlowLOD(HWEngineGlow glowMesh, HWMesh mesh, int lod)
        {
            GlowMesh = glowMesh;
            Mesh = mesh;
            LOD = lod;

            Mesh.Translucent = true;
            Mesh.Material.Shader = "fx_eng_glowbasic";
        }
    }
}
