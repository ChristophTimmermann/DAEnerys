using OpenTK;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DAEnerys
{
    public class HWJoint : HWElement
    {
        public static List<HWJoint> Joints = new List<HWJoint>();
        public static HWJoint Root;

        public Matrix4 AnimationMatrix = Matrix4.Identity;
        public HWAnimationChannel PositionChannel = new HWAnimationChannel();
        public HWAnimationChannel RotationChannel = new HWAnimationChannel();
        public HWAnimationChannel ScalingChannel = new HWAnimationChannel();

        public List<HWMesh> Meshes = new List<HWMesh>();

        public EditorJoint EditorJoint;

        public override string FormattedName
        {
            get
            {
                return "JNT[" + Name + "]";
            }
        }

        public TreeNode TreeNode;

        public HWJoint(string name, HWJoint parent, Matrix4 transform) : base(name, parent, transform)
        {
            Joints.Add(this);

            Program.main.AddJoint(this, parent);

            //Visualization
            EditorJoint = new EditorJoint(this);
        }

        public override void Destroy()
        {
            Program.main.RemoveJoint(this);
            Joints.Remove(this);
            EditorJoint.Destroy();
            EditorJoint = null;

            HWMesh[] meshes = Meshes.ToArray();
            foreach(HWMesh mesh in meshes)
            {
                HWShipMeshLOD shipMeshLOD = mesh as HWShipMeshLOD;
                HWEngineGlowLOD engineGlowLOD = mesh as HWEngineGlowLOD;

                HWJoint newParent = HWJoint.Root;
                HWJoint jointParent = this.Parent as HWJoint;
                if (jointParent != null)
                    newParent = jointParent;

                if (shipMeshLOD != null)
                    shipMeshLOD.ShipMesh.Parent = newParent;
                else if (engineGlowLOD != null)
                    engineGlowLOD.EngineGlow.Parent = newParent;
                else
                    mesh.Parent = newParent;
            }
            
            Meshes.Clear();

            base.Destroy();
        }

        public static HWJoint GetByName(string name)
        {
            foreach(HWJoint joint in Joints)
                if (joint.Name == name)
                    return joint;

            return null;
        }

        public override void CalculateWorldMatrix()
        {
            invalid = false;

            if (AnimationMatrix == Matrix4.Identity)
            {
                LocalWorldMatrix = Matrix4.CreateFromQuaternion(localRotation);
                LocalWorldMatrix *= Matrix4.CreateScale(localScale);
                LocalWorldMatrix *= Matrix4.CreateTranslation(localPosition);
            }
            else
                LocalWorldMatrix = AnimationMatrix;

            GlobalWorldMatrix = LocalWorldMatrix;

            GlobalPosition = GlobalWorldMatrix.ExtractTranslation();
            GlobalRotation = GlobalWorldMatrix.ExtractRotation();
            GlobalScale = GlobalWorldMatrix.ExtractScale();

            if (Parent != null)
                GlobalWorldMatrix *= Parent.GlobalWorldMatrix;

            foreach (HWMesh mesh in Meshes)
                mesh.CalculateWorldMatrix();

            Renderer.InvalidateView();
            Renderer.Invalidate();
        }
    }
}
