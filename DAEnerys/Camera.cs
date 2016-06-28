using OpenTK;
using OpenTK.Input;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DAEnerys
{
    public class Camera
    {
        public Vector3 Position = new Vector3(0, 0, 15);
        public Vector3 Orientation = new Vector3((float)Math.PI, 0f, 0f);
        private Matrix4 viewMatrix = Matrix4.Identity;

        public float NearClipDistance = 0.01f;
        public float ClipDistance = 1000;
        public float FieldOfView = 1.22f;


        private bool orthographic;
        public bool Orthographic {
            get { return orthographic; }
            set {
                orthographic = value;
                Program.main.UpdatePerspectiveOrthoCombo();
                Update(true);
            }
        }

        private float orthographicSize = 1f;
        public float OrthographicSize { get { return orthographicSize; } set { orthographicSize = value; Update(); } }

        public float MinZoom = 0.01f;
        public float MaxZoom = 5.0f;
        public float ZoomScalar = 1.2f;

        private Vector3 orbitPoint = Vector3.Zero;

        private float zoom = 1f;
        public float ZoomFactor {
            get { return zoom; }
            set {
                CameraZoom(value - zoom);
                Update(true);
            }
        }
        public void SetDistance(float distance)
        {
            zoom = UnfactorZoom(distance);
            Update(true);
        }


        private Vector2 angles = new Vector2((float)Math.PI, (float)Math.PI);

        private bool leftButton;
        private bool rightButton;
        
        private Point pressPos;
        
        public void MouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)  leftButton = true;
            if (e.Button == MouseButtons.Right) rightButton = true;
            
            if (rightButton ^ leftButton)
            {
                MouseState mouse = Mouse.GetState();
                Cursor.Hide();
                pressPos = Cursor.Position;
            }
        }

        public void MouseUp(System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)  leftButton = false;
            if (e.Button == MouseButtons.Right) rightButton = false;

            if (!leftButton && !rightButton)
            {
                Cursor.Position = pressPos;
                Cursor.Show();
                pressPos = Point.Empty;
            }
        }

        public void MouseMove(System.Windows.Forms.MouseEventArgs e)
        {
            if (leftButton || rightButton) {
                float dX = Cursor.Position.X - pressPos.X;
                float dY = Cursor.Position.Y - pressPos.Y;
                if (dX == 0 && dY == 0) return;

                if (leftButton && !rightButton)
                    CameraRotate(dX, dY);
                else if (leftButton && rightButton)
                    CameraPan(dX, dY);
                else if (!leftButton && rightButton)
                    CameraZoom(-dY);

                Cursor.Position = pressPos;
            }
        }

        public void MouseWheel(System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Delta != 0)
                CameraZoom(e.Delta / 30);
        }

        public void KeyDown(System.Windows.Forms.KeyEventArgs e)
        {
            if (ActionKey.IsDown(Action.TOGGLE_ORTHOGRAPHIC)) //Toggle orthographic view
                this.Orthographic = !this.Orthographic;
            else if (ActionKey.IsDown(Action.VIEW_FRONT))
                UpdateAngles((float)Math.PI, (float)Math.PI);
            else if (ActionKey.IsDown(Action.VIEW_BACK))
                UpdateAngles((float)Math.PI, 0);
            else if (ActionKey.IsDown(Action.VIEW_LEFT))
                UpdateAngles((float)Math.PI, (float)-Math.PI / 2);
            else if (ActionKey.IsDown(Action.VIEW_RIGHT))
                UpdateAngles((float)Math.PI, (float)Math.PI / 2);
            else if (ActionKey.IsDown(Action.VIEW_TOP))
                UpdateAngles((float)Math.PI * 1.5f, (float)Math.PI);
            else if (ActionKey.IsDown(Action.VIEW_BOTTOM))
                UpdateAngles(0, (float)Math.PI);
        }
        
        private void UpdateAngles(float X, float Y) {
            angles.X = X;
            angles.Y = Y;

            Update(true);
        }

        private void CameraRotate(float deltaX, float deltaY)
        {
            UpdateAngles(angles.X + deltaY * 0.01f, angles.Y - deltaX * 0.01f);
        }

        private void CameraZoom(float delta)
        {
            if (!this.Orthographic)
            {
                zoom -= delta;
                float len = FactorZoom(zoom);
                if (len < MinZoom) SetDistance(MinZoom);
                if (len > MaxZoom) SetDistance(MaxZoom);
            }
            else
                orthographicSize += delta * (orthographicSize / 30);

            Update(true);
        }

        private void CameraPan(float deltaX, float deltaY)
        {
            Vector3 left = Vector3.Transform(new Vector3(1, 0, 0), Matrix4.CreateRotationY(angles.Y));
            Vector3 upTemp = Vector3.Transform(new Vector3(0, 1, 0), Matrix4.CreateRotationX(angles.X));
            Vector3 up = Vector3.Transform(upTemp, Matrix4.CreateRotationY(angles.Y));
            orbitPoint += left * deltaX;
            orbitPoint -= up * deltaY;
            Update(true);
        }

        private float FactorZoom(float value)
        {
            return (float)Math.Pow(ZoomScalar, value);
        }

        private float UnfactorZoom(float value)
        {
            return (float)(Math.Log(value) / Math.Log(ZoomScalar));
        }

        private void ConstrainValues()
        {
            angles.X = (float)Utilities.Clamp(angles.X, Math.PI * 0.5, Math.PI * 1.5 - 0.000001f);
            angles.Y = (float)(angles.Y % (2.0 * Math.PI));
            zoom = Utilities.Clamp(zoom, UnfactorZoom(MinZoom), UnfactorZoom(MaxZoom));
            orthographicSize = Utilities.Clamp(orthographicSize, FactorZoom(MinZoom), MaxZoom);
        }

        public void Update(bool forceUpdate = false)
        {
            if (Program.GLControl == null)
                return;

            if (Program.GLControl.Focused || forceUpdate)
            {
                UpdatePosition();
                Renderer.UpdateView();
                Program.GLControl.Invalidate();
            }
        }

        private void UpdatePosition()
        {
            ConstrainValues();
            float len = FactorZoom(orthographic ? orthographicSize : zoom);
            Position = orbitPoint + Vector3.Transform(new Vector3(0, 0, len), Matrix4.CreateRotationX(angles.X) * Matrix4.CreateRotationY(angles.Y));
            viewMatrix = Matrix4.LookAt(Position, orbitPoint, new Vector3(0, 1, 0));
        }

        public Matrix4 GetViewMatrix()
        {
            return viewMatrix;
        }
    }
}
