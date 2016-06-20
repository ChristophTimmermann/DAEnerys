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
        public float FieldOfView = 1.22f;

        private bool orthographic;
        public bool Orthographic { get { return orthographic; } set { orthographic = value; Update(true); } }

        private float orthographicSize = 12;
        public float OrthographicSize { get { return orthographicSize; } set { orthographicSize = value; Update(); } }
        private float perspectiveZoom = 1;

        private float lastOrthographicSize;

        public float CalculatedZoom = 1;
        public float ZoomSpeed = 5;

        private Vector3 orbitPoint = Vector3.Zero;

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
            if (e.KeyCode == Keys.NumPad5) //Toggle orthographic view
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
                Renderer.UpdateView();
                Program.GLControl.Invalidate();
            }
            else if (e.KeyCode == Keys.NumPad1) 
            {
                if (!e.Control) //Front view
                {
                    angles.X = (float)Math.PI;
                    angles.Y = (float)Math.PI;
                }
                else //Back view
                {
                    angles.X = (float)Math.PI;
                    angles.Y = 0;
                }

                UpdatePosition();
                Renderer.UpdateView();
                Program.GLControl.Invalidate();
            }
            else if (e.KeyCode == Keys.NumPad3) 
            {
                if (!e.Control) //Left side view
                {
                    angles.X = (float)Math.PI;
                    angles.Y = (float)-Math.PI / 2;
                }
                else //Right side view
                {
                    angles.X = (float)Math.PI;
                    angles.Y = (float)Math.PI / 2;
                }

                UpdatePosition();
                Renderer.UpdateView();
                Program.GLControl.Invalidate();
            }
            else if (e.KeyCode == Keys.NumPad7) 
            {
                if (!e.Control) //Top view
                {
                    angles.X = (float)Math.PI * 1.5f;
                    angles.Y = (float)Math.PI;
                }
                else //Bottom view
                {
                    angles.X = 0;
                    angles.Y = (float)Math.PI;
                }

                angles.X = (float)Utilities.Clamp(angles.X, Math.PI - Math.PI / 2, (Math.PI + Math.PI / 2) - 0.000001f);

                UpdatePosition();
                Renderer.UpdateView();
                Program.GLControl.Invalidate();
            }
        }

        public void Update(bool forceUpdate = false)
        {
            if (Program.GLControl == null)
                return;

            MouseState mouse = Mouse.GetState();
            Point position = Cursor.Position;

            float zoomDelta = mouse.WheelPrecise - lastWheelPrecise;

            if (Program.GLControl.Focused || forceUpdate)
            {
                if (mouse.RightButton == OpenTK.Input.ButtonState.Pressed || forceUpdate)
                {
                    float deltaX = position.X - lastPos.X;
                    float deltaY = position.Y - lastPos.Y;

                    angles.X += deltaY * 0.01f;
                    angles.Y -= deltaX * 0.01f;

                    angles.X = (float)Utilities.Clamp(angles.X, Math.PI - Math.PI / 2, (Math.PI + Math.PI / 2) - 0.000001f);

                    Renderer.UpdateView();
                    Program.GLControl.Invalidate();
                }

                if (!this.Orthographic)
                {
                    zoom -= zoomDelta * ZoomSpeed * 0.01f;
                    zoom = Utilities.Clamp(zoom, 0.001f, 500000);
                }
                else
                {
                    orthographicSize += zoomDelta * (orthographicSize / 30);
                    orthographicSize = Utilities.Clamp(orthographicSize, 0.0001f, 500);
                }
            }

            if (lastZoom != zoom)
            {
                Renderer.UpdateView();
                Program.GLControl.Invalidate();
            }

            if (lastOrthographicSize != orthographicSize)
            {
                Renderer.UpdateView();
                Program.GLControl.Invalidate();
            }

            lastPos = position;
            lastZoom = zoom;
            lastOrthographicSize = orthographicSize;
            lastWheelPrecise = mouse.WheelPrecise;

            UpdatePosition();
        }

        private void UpdatePosition()
        {
            Position = orbitPoint + Vector3.Transform(new Vector3(0, 0, Zoom), Matrix4.CreateRotationX(angles.X) * Matrix4.CreateRotationY(angles.Y));
        }

        public Matrix4 GetViewMatrix()
        {
            return Matrix4.LookAt(Position, orbitPoint, new Vector3(0, 1, 0));
        }
    }
}
