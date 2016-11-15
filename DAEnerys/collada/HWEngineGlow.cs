using System.Collections.Generic;

namespace DAEnerys
{
    public class HWEngineGlow
    {
        public HWJoint Parent;
        public string Name;

        public List<HWEngineGlowLOD>[] LODMeshes = new List<HWEngineGlowLOD>[4];

        public object EngineGlowListItem;

        public HWEngineGlow(HWJoint parent, string name)
        {
            for (int i = 0; i < LODMeshes.Length; i++)
                LODMeshes[i] = new List<HWEngineGlowLOD>();

            Parent = parent;
            Name = name;

            HWScene.EngineGlows.Add(this);
            Program.main.AddEngineGlow(this);
        }

        public void AddLODMesh(HWEngineGlowLOD lodMesh)
        {
            LODMeshes[lodMesh.LOD].Add(lodMesh);
        }

        public static void UpdateEngineStrength()
        {
            foreach(HWEngineGlow engineGlow in HWScene.EngineGlows)
                foreach (List<HWEngineGlowLOD> list in engineGlow.LODMeshes)
                    foreach (HWEngineGlowLOD lodMesh in list)
                        lodMesh.Scale.Z = Renderer.ThrusterInterpolation;
        }
    }
}
