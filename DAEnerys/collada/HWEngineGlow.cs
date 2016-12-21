using System.Collections.Generic;

namespace DAEnerys
{
    public class HWEngineGlow
    {
        public static List<HWEngineGlow> EngineGlows = new List<HWEngineGlow>();

        private HWJoint parent;
        public HWJoint Parent
        {
            get { return parent; }
            set
            {
                parent = value;
                foreach (HWEngineGlowLOD mesh in Meshes)
                {
                    mesh.Parent = value;
                }
            }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; foreach (HWEngineGlowLOD mesh in Meshes) mesh.Name = value; }
        }

        public List<HWEngineGlowLOD> Meshes = new List<HWEngineGlowLOD>();
        public List<HWEngineGlowLOD>[] LODMeshes = new List<HWEngineGlowLOD>[4];

        public HWEngineGlow(HWJoint parent, string name)
        {
            for (int i = 0; i < LODMeshes.Length; i++)
                LODMeshes[i] = new List<HWEngineGlowLOD>();

            Parent = parent;
            Name = name;

            EngineGlows.Add(this);
            Program.main.AddEngineGlow(this);
        }

        public void AddLODMesh(HWEngineGlowLOD lodMesh)
        {
            LODMeshes[lodMesh.LOD].Add(lodMesh);
            Meshes.Add(lodMesh);
        }

        public static void UpdateEngineStrength()
        {
            foreach(HWEngineGlow engineGlow in EngineGlows)
                foreach (List<HWEngineGlowLOD> list in engineGlow.LODMeshes)
                    foreach (HWEngineGlowLOD lodMesh in list)
                        lodMesh.Scale.Z = Renderer.ThrusterInterpolation;
        }

        public void Destroy()
        {
            EngineGlows.Remove(this);
            Program.main.RemoveEngineGlow(this);
            HWEngineGlowLOD[] lodMeshes = Meshes.ToArray();
            for (int i = 0; i < lodMeshes.Length; i++)
                lodMeshes[i].Destroy();
            Parent = null;
        }
    }
}
