using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworldDAEEditor
{
    public class HWGoblinMesh
    {
        public List<HWMesh> Meshes = new List<HWMesh>();
        public HWJoint Parent;
        public string Name;
        public List<GoblinMeshTags> Tags = new List<GoblinMeshTags>();

        public int GoblinMeshListItemIndex;

        public HWGoblinMesh(HWJoint parent, string name, List<GoblinMeshTags> tags)
        {
            Parent = parent;
            Name = name;
            Tags = tags;

            HWScene.GoblinMeshes.Add(this);
            Program.main.AddGoblinMesh(this);
        }

        public void AddMesh(HWMesh mesh)
        {
            Meshes.Add(mesh);
        }

        public void SetVisible(bool visible)
        {
            foreach(HWMesh mesh in Meshes)
            {
                mesh.Visible = visible;
            }
            Program.main.CheckGoblinVisible(this, visible);
        }
    }

    public enum GoblinMeshTags
    {
        DOSCAR = 1,
    }
}
