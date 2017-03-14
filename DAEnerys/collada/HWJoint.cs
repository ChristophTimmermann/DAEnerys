using OpenTK;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DAEnerys
{
    public class HWJoint : HWElement
    {
        public static List<HWJoint> Joints = new List<HWJoint>();
        public static HWJoint Root;

        public static MeshData CaptureVisualizationMeshData;
        public static MeshData SalvageVisualizationMeshData;
        public static MeshData RepairVisualizationMeshData;

        public Matrix4 AnimationMatrix = Matrix4.Identity;
        public HWAnimationChannel PositionChannel = new HWAnimationChannel();
        public HWAnimationChannel RotationChannel = new HWAnimationChannel();
        public HWAnimationChannel ScalingChannel = new HWAnimationChannel();

        public List<HWMesh> Meshes = new List<HWMesh>();

        public EditorJoint EditorJoint;
        public EditorVisualizationMesh VisualizationMesh;

        public override string Name
        {
            get { return base.Name; }
            set
            {
                base.Name = value;

                DestroyVisualization();

                if (value.Length == 0)
                    return;

                //Check for capture point
                if (value.StartsWith("CapturePoint", StringComparison.InvariantCultureIgnoreCase) && char.IsNumber(value[value.Length - 1]))
                {
                    VisualizationMesh = new EditorVisualizationMesh(this, CaptureVisualizationMeshData);
                    if (EditorJoint != null)
                        VisualizationMesh.Visible = EditorJoint.Visible;
                }
                //Check for salvage point
                else if (value.StartsWith("SalvagePoint", StringComparison.InvariantCultureIgnoreCase) && char.IsNumber(value[value.Length - 1]))
                {
                    VisualizationMesh = new EditorVisualizationMesh(this, SalvageVisualizationMeshData);
                }
                //Check for repair point
                else if (value.StartsWith("RepairPoint", StringComparison.InvariantCultureIgnoreCase) && char.IsNumber(value[value.Length - 1]))
                {
                    VisualizationMesh = new EditorVisualizationMesh(this, RepairVisualizationMeshData);
                }
                //Check for latch point
                else if (value.StartsWith("Latch", StringComparison.InvariantCultureIgnoreCase) && char.IsNumber(value[value.Length - 1]))
                {
                    VisualizationMesh = new EditorVisualizationMesh(this, RepairVisualizationMeshData);
                }
                else
                    return;

                if (EditorJoint != null)
                    VisualizationMesh.Visible = EditorJoint.Visible;
            }
        }
        public override string FormattedName
        {
            get
            {
                return "JNT[" + Name + "]";
            }
        }

        public TreeNode TreeNode;

        public HWJoint(string name, HWJoint parent) : this(name, parent, Vector3.Zero, Vector3.Zero, Vector3.One)
        {

        }
        public HWJoint(string name, HWJoint parent, Vector3 pos) : this(name, parent, pos, Vector3.Zero, Vector3.One)
        {

        }
        public HWJoint(string name, HWJoint parent, Vector3 pos, Vector3 rot, Vector3 scale) : base(name, parent, pos, rot, scale)
        {
            Joints.Add(this);

            Program.main.AddJoint(this, parent);

            //Visualization
            EditorJoint = new EditorJoint(this);
        }

        public override void Destroy()
        {
            Destroy(false);   
        }
        public void Destroy(bool withChildren)
        {
            Program.main.RemoveJoint(this);
            Joints.Remove(this);
            EditorJoint.Destroy();
            EditorJoint = null;

            DestroyVisualization();

            if (withChildren)
            {
                Element[] children = Children.ToArray();
                foreach (Element child in children)
                {
                    HWJoint childJoint = child as HWJoint;
                    if (childJoint != null)
                        childJoint.Destroy(true);
                }
            }

            HWMesh[] meshes = Meshes.ToArray();
            foreach (HWMesh mesh in meshes)
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

        private void DestroyVisualization()
        {
            if (VisualizationMesh != null)
            {
                VisualizationMesh.Destroy();
                VisualizationMesh = null;
            }
        }

        public static HWJoint GetByName(string name)
        {
            foreach(HWJoint joint in Joints)
                if (joint.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                    return joint;

            return null;
        }

        public override void CalculateWorldMatrix()
        {
            invalid = false;

            if (AnimationMatrix == Matrix4.Identity)
            {
                LocalWorldMatrix = Matrix4.CreateRotationX(LocalRotation.X) * Matrix4.CreateRotationY(LocalRotation.Y) * Matrix4.CreateRotationZ(LocalRotation.Z);
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

    public enum JointType
    {
        GENERIC,
        WEAPON,
        TURRET,
        HARDPOINT,
        CAPTUREPOINT,
        REPAIRPOINT,
        SALVAGEPOINT,
    }
}
