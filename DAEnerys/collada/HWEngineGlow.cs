using System.Collections.Generic;

namespace DAEnerys
{
    public class HWEngineGlow
    {
        public HWJoint Parent;
        public string Name;

        public List<HWEngineGlowLOD> LOD0Meshes = new List<HWEngineGlowLOD>();
        public List<HWEngineGlowLOD> LOD1Meshes = new List<HWEngineGlowLOD>();
        public List<HWEngineGlowLOD> LOD2Meshes = new List<HWEngineGlowLOD>();
        public List<HWEngineGlowLOD> LOD3Meshes = new List<HWEngineGlowLOD>();

        public object EngineGlowListItem;

        public HWEngineGlow(HWJoint parent, string name)
        {
            Parent = parent;
            Name = name;

            HWScene.EngineGlows.Add(this);
            Program.main.AddEngineGlow(this);
        }

        public void AddLODMesh(HWEngineGlowLOD lodMesh)
        {
            switch(lodMesh.LOD)
            {
                case 0:
                    LOD0Meshes.Add(lodMesh);
                    break;
                case 1:
                    LOD1Meshes.Add(lodMesh);
                    break;
                case 2:
                    LOD2Meshes.Add(lodMesh);
                    break;
                case 3:
                    LOD2Meshes.Add(lodMesh);
                    break;
            }
        }

        public static void UpdateEngineStrength()
        {
            foreach(HWEngineGlow engineGlow in HWScene.EngineGlows)
            {
                foreach(HWEngineGlowLOD engineGlowLOD in engineGlow.LOD0Meshes)
                {
                    engineGlowLOD.Mesh.Scale.Z = Renderer.ThrusterInterpolation;
                }

                foreach (HWEngineGlowLOD engineGlowLOD in engineGlow.LOD1Meshes)
                {
                    engineGlowLOD.Mesh.Scale.Z = Renderer.ThrusterInterpolation;
                }

                foreach (HWEngineGlowLOD engineGlowLOD in engineGlow.LOD2Meshes)
                {
                    engineGlowLOD.Mesh.Scale.Z = Renderer.ThrusterInterpolation;
                }
            }
        }
    }
}
