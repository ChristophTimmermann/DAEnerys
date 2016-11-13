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

        public float NearClipDistance = 0.01f;
        public float ClipDistance = 1000;
        public float FieldOfView = 0.9599f;

        public float MinZoom = 0.01f;
        public float MaxZoom = 5.0f;

        private bool orthographic;
        public bool Orthographic { get { return orthographic; } set { orthographic = value; Update(true); } }

        private float orthographicSize = 16;
        public float OrthographicSize { get { return orthographicSize; } set { orthographicSize = value; Update(); } }
        private float perspectiveZoom = 1;

        private float lastOrthographicSize;

        public float CalculatedZoom = 1;
        public float ZoomSpeed = 5;

        public Vector3 LookAt = Vector3.Zero;
        public Vector3 Direction
        {
            get
            {
                return Program.Camera.LookAt - Program.Camera.Position;
            }
        }

        private float zoom = 1;
        public float Zoom { get { return zoom; } set { zoom = value; Update(); } }


        private float lastZoom;
        private Vector2 angles = new Vector2((float)Math.PI, (float)Math.PI);

        private float lastWheelPrecise;

        private Point lastPos;
        private Point pressPos;

        //private Cursor cursor = new Cursor(Program.main.Cursor.Handle);

        public void MouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                MouseState mouse = Mouse.GetState();
                Cursor.Hide();
                pressPos = Cursor.Position;
            }
        }

        public void MouseUp(System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //Vector2 newPos = new Vector2(Program.GLControl.Bounds.Left + pressPos.X, Program.GLControl.Bounds.Top + pressPos.Y);
                //Mouse.SetPosition(newPos.X, newPos.Y);
                Cursor.Position = pressPos;
                Cursor.Show();
                pressPos = Point.Empty;
            }
        }

        public void KeyDown(System.Windows.Forms.KeyEventArgs e)
        {
            if (ActionKey.IsDown(Action.TOGGLE_ORTHOGRAPHIC)) //Toggle orthographic view
            {
                this.Orthographic = !this.Orthographic;
                if (this.Orthographic)
                {
                    this.perspectiveZoom = this.Zoom;
                    this.Zoom = CalculatedZoom;
                }
                else
                    this.Zoom = perspectiveZoom;

                Program.main.UpdatePerspectiveOrthoCombo();
                Renderer.InvalidateView();
                Renderer.Invalidate();
            }
            else if (ActionKey.IsDown(Action.VIEW_FRONT))
            {
                angles.X = (float)Math.PI;
                angles.Y = (float)Math.PI;

                UpdatePosition();
                Renderer.InvalidateView();
                Renderer.Invalidate();
            }
            else if (ActionKey.IsDown(Action.VIEW_BACK))
            {
                angles.X = (float)Math.PI;
                angles.Y = 0;

                UpdatePosition();
                Renderer.InvalidateView();
                Renderer.Invalidate();
            }
            else if (ActionKey.IsDown(Action.VIEW_LEFT))
            {
                angles.X = (float)Math.PI;
                angles.Y = (float)-Math.PI / 2;

                UpdatePosition();
                Renderer.InvalidateView();
                Renderer.Invalidate();
            }
            else if (ActionKey.IsDown(Action.VIEW_RIGHT))
            {
                angles.X = (float)Math.PI;
                angles.Y = (float)Math.PI / 2;

                UpdatePosition();
                Renderer.InvalidateView();
                Renderer.Invalidate();
            }
            else if (ActionKey.IsDown(Action.VIEW_TOP))
            {
                angles.X = (float)Math.PI * 1.5f;
                angles.Y = (float)Math.PI;

                angles.X = (float)Utilities.Clamp(angles.X, Math.PI - Math.PI / 2, (Math.PI + Math.PI / 2) - 0.000001f);

                UpdatePosition();
                Renderer.InvalidateView();
                Renderer.Invalidate();
            }
            else if (ActionKey.IsDown(Action.VIEW_BOTTOM))
            {
                angles.X = 0;
                angles.Y = (float)Math.PI;

                angles.X = (float)Utilities.Clamp(angles.X, Math.PI - Math.PI / 2, (Math.PI + Math.PI / 2) - 0.000001f);

                UpdatePosition();
                Renderer.InvalidateView();
                Renderer.Invalidate();
            }
            else if (ActionKey.IsDown(Action.CAM_RESET))
            {
                angles.X = (float)Math.PI;
                angles.Y = (float)Math.PI;

                this.Orthographic = false;

                this.Zoom = CalculatedZoom;
                this.perspectiveZoom = this.Zoom;
                this.orthographicSize = 16;

                UpdatePosition();
                Renderer.InvalidateView();
                Renderer.Invalidate();
                Program.main.UpdatePerspectiveOrthoCombo();
            }
        }

        public void Update(bool forceUpdate = false)
        {
            if (Program.GLControl == null)
                return;

            MouseState mouse = Mouse.GetState();
            Point position = Cursor.Position;

            if (Program.GLControl.Focused || forceUpdate)
            {
                float zoomDelta = mouse.WheelPrecise - lastWheelPrecise;

                if (mouse.RightButton == OpenTK.Input.ButtonState.Pressed || forceUpdate)
                {
                    float deltaX = position.X - lastPos.X;
                    float deltaY = position.Y - lastPos.Y;

                    angles.X += deltaY * 0.01f;
                    angles.Y -= deltaX * 0.01f;

                    angles.X = (float)Utilities.Clamp(angles.X, Math.PI - Math.PI / 2, (Math.PI + Math.PI / 2) - 0.000001f);

                    Renderer.InvalidateView();
                    Renderer.Invalidate();
                }

                if (!this.Orthographic)
                {
                    zoom -= zoomDelta * ZoomSpeed * 0.01f;
                    zoom = Utilities.Clamp(zoom, MinZoom, MaxZoom);
                }
                else
                {
                    orthographicSize += zoomDelta * (orthographicSize / 30);
                    orthographicSize = Utilities.Clamp(orthographicSize, 0.0001f, 500);
                }
            }

            UpdatePosition();

            if (lastZoom != zoom)
            {
                Renderer.InvalidateView();
                Renderer.Invalidate();
            }

            if (lastOrthographicSize != orthographicSize)
            {
                Renderer.InvalidateView();
                Renderer.Invalidate();
            }

            lastPos = position;
            lastZoom = zoom;
            lastOrthographicSize = orthographicSize;
            lastWheelPrecise = mouse.WheelPrecise;
        }

        private void UpdatePosition()
        {
            Position = LookAt + Vector3.Transform(new Vector3(0, 0, Zoom), Matrix4.CreateRotationX(angles.X) * Matrix4.CreateRotationY(angles.Y));
        }

        public Matrix4 GetViewMatrix()
        {
            return Matrix4.LookAt(Position, LookAt, new Vector3(0, 1, 0));
        }
    }
}