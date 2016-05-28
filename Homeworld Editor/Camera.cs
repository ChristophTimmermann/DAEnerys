using OpenTK;
using OpenTK.Input;
using System;
using System.Drawing;
using System.Windows.Forms;
using Utilities;

namespace HomeworldDAEEditor
{
    public class Camera
    {
        public Vector3 Position = new Vector3(0, 0, 15);
        public Vector3 Orientation = new Vector3((float)Math.PI, 0f, 0f);

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

        public void Init()
        {

        }

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

        public void Update()
        {
            MouseState mouse = Mouse.GetState();
            Point position = Cursor.Position;

            if (Program.GLControl.Focused)
            {
                if (mouse.RightButton == OpenTK.Input.ButtonState.Pressed)
                {
                    float deltaX = position.X - lastPos.X;
                    float deltaY = position.Y - lastPos.Y;

                    angles.X += deltaY * 0.01f;
                    angles.Y -= deltaX * 0.01f;

                    angles.X = (float)Utilities.Utilities.Clamp(angles.X, Math.PI - Math.PI / 2, (Math.PI + Math.PI / 2) - 0.000001f);

                    Renderer.UpdateView();
                    Program.GLControl.Invalidate();
                }

                float zoomDelta = mouse.WheelPrecise - lastWheelPrecise;
                zoom -= zoomDelta * ZoomSpeed * 0.01f;
                zoom = Utilities.Utilities.Clamp(zoom, 0.001f, 50000);
            }

            if (lastZoom != zoom)
            {
                Renderer.UpdateView();
                Program.GLControl.Invalidate();
            }

            lastPos = position;
            lastZoom = zoom;
            lastWheelPrecise = mouse.WheelPrecise;
            Position = orbitPoint + Vector3.Transform(new Vector3(0, 0, zoom), Matrix3.CreateRotationX(angles.X) * Matrix3.CreateRotationY(angles.Y));
        }

        public Matrix4 GetViewMatrix()
        {
            return Matrix4.LookAt(Position, orbitPoint, new Vector3(0, 1, 0));
        }
    }
}
