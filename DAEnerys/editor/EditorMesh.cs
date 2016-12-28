using OpenTK;

namespace DAEnerys
{
    public abstract class EditorMesh : GenericMesh
    {
        public bool NeverDrawInFront = false;
        public bool BlackIsTransparent = false;
        public bool Wireframe = false;
        public bool DrawAboveShip = false;

        public EditorMesh(MeshData data, Element parent, GenericMaterial material) : base(parent, Matrix4.Identity)
        {
            SetData(data);

            Material = material;
            Parent = parent;

            EditorScene.meshes.Add(this);

            Renderer.InvalidateMeshData();
            Renderer.Invalidate();
        }

        public override void Destroy()
        {
            base.Destroy();

            EditorScene.meshes.Remove(this);

            Renderer.InvalidateMeshData();
            Renderer.Invalidate();
        }

        public override void CalculateWorldMatrix()
        {
            base.CalculateWorldMatrix();

            ModelViewProjectionMatrix = GlobalWorldMatrix * Renderer.ViewProjection;
        }
    }
}
