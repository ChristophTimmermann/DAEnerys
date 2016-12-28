using OpenTK;
using System.Collections.Generic;
using System.IO;

namespace DAEnerys
{
    public class EditorIcon : EditorMesh
    {
        public static MeshData Data;

        public Vector3 Position;

        public float Size = 10;

        public static HWTexture LightbulbTexture = new HWTexture(Path.Combine(Program.EXECUTABLE_PATH, @"resources/lightbulb.tga"), true, true, true);

        public EditorIcon(Element parent, HWTexture texture) : base(Data, parent, null)
        {
            this.Material = new GenericMaterial(Vector3.One);
            this.Material.DiffuseTexture = texture;
            Visible = false;
            VertexColored = false;
        }

        public override void CalculateWorldMatrix()
        {
            LocalWorldMatrix = Renderer.View.Inverted().ClearTranslation() * Matrix4.CreateScale(Size);
            GlobalWorldMatrix = LocalWorldMatrix;

            if(Parent != null)
                GlobalWorldMatrix *= Matrix4.CreateTranslation(Parent.GlobalPosition);
        }
    }
}
