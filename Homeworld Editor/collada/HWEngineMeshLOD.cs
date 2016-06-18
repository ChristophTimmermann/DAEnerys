using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworldDAEEditor
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

            Mesh.Shaded = false;
            Mesh.Translucent = true;
            Mesh.VertexColored = false;

            //Hiigaran engine color
            Mesh.Material.DiffuseTexture = null;
            Mesh.Material.DiffuseColor = new Vector3(0.27f, 0.47f, 0.69f) * 2;
            Mesh.Material.Opacity = 0.25f;
        }

        public void UpdateEngineStrength()
        {
            Mesh.Scale.Z = Renderer.ThrusterInterpolation;
            Mesh.Material.Opacity = Renderer.ThrusterInterpolation / 4;
            Console.WriteLine(Mesh.Material.Opacity);
        }
    }
}
