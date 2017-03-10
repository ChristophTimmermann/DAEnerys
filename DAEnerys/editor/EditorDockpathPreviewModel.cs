using Assimp;
using OpenTK;
using System.Collections.Generic;

namespace DAEnerys
{
    public class EditorDockpathPreviewModel : EditorMesh
    {
        public static MeshData Data;

        public EditorDockpathPreviewModel(Element parent) : base(Data, parent, new GenericMaterial(new Vector3(1, 1, 1)))
        {
            Material.DiffuseColor = new Vector3(1);
        }
    }
}
