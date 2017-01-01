using Assimp;
using OpenTK;
using System.Collections.Generic;

namespace DAEnerys
{
    public class EditorJoint : EditorMesh
    {
        public static MeshData Data;

        private static float size = 0.6f;
        public static float Size { get { return size; } set { size = value; foreach (HWJoint joint in HWJoint.Joints) joint.EditorJoint.LocalScale = new Vector3(value); } }

        public EditorJoint(HWJoint joint) : base(Data, joint, null)
        {
            LocalScale = new Vector3(Size);
            VertexColored = true;
        }
    }
}
