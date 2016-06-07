using OpenTK;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace HomeworldDAEEditor
{
    static class Program
    {
        //Windows
        public static Main main;
        public static Settings settings;

        public static Assembly Assembly = Assembly.GetExecutingAssembly();
        public static string AssemblyName = "Homeworld_Editor.";
        public static int FSAASamples = 4;

        public static Camera Camera = new Camera();
        public static GLControl GLControl;

        public static OpenTK.NativeWindow NativeWindow = new OpenTK.NativeWindow();

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            main = new Main();
            Settings.LoadSettings();
            CreateGLControl();
            Application.Run(main);

            Camera.Init();
        }

        public static void CreateGLControl()
        {
            GLControl = new CustomGLControl(FSAASamples);
            GLControl.BackColor = System.Drawing.Color.Black;
            GLControl.Dock = System.Windows.Forms.DockStyle.Fill;
            GLControl.Location = new System.Drawing.Point(0, 0);
            GLControl.Name = "glControl";
            GLControl.Size = new System.Drawing.Size(865, 758);
            GLControl.TabIndex = 0;
            GLControl.VSync = true;
            GLControl.Paint += new System.Windows.Forms.PaintEventHandler(main.glControl_Render);
            GLControl.Enter += new System.EventHandler(main.glControl_Enter);
            GLControl.KeyDown += new System.Windows.Forms.KeyEventHandler(main.glControl_KeyDown);
            GLControl.Leave += new System.EventHandler(main.glControl_Leave);
            GLControl.MouseDown += new System.Windows.Forms.MouseEventHandler(main.glControl_MouseDown);
            GLControl.MouseUp += new System.Windows.Forms.MouseEventHandler(main.glControl_MouseUp);
            GLControl.Resize += new System.EventHandler(main.glControl_Resize);

            main.splitContainer1.Panel2.Controls.Add(GLControl);
        }
    }
}
