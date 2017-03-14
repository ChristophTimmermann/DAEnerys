using Assimp;
using OpenTK;
using System.Collections.Generic;

namespace DAEnerys
{
    public class EditorVisualizationMesh : EditorMesh
    {
        public EditorVisualizationMesh(Element parent, MeshData data) : base(data, parent, new GenericMaterial(new Vector3(1, 1, 1)))
        {
            VertexColored = true;
            DrawAboveShip = false;
            NeverDrawInFront = true;
        }
    }
}
