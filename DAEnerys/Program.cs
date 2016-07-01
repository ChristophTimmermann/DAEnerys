using OpenTK;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace DAEnerys
{
    static class Program
    {
        //Windows
        public static Main main;
        public static Settings settings;
        public static Hotkeys hotkeys;

        public static Assembly Assembly = Assembly.GetExecutingAssembly();
        public static string AssemblyName = "DAEnerys.";
        public static int FSAASamples = 4;

        public static Camera Camera = new Camera();
        public static GLControl GLControl;

        public static OpenTK.NativeWindow NativeWindow = new OpenTK.NativeWindow();

        public static Stopwatch DeltaCounter = new Stopwatch();
        public static double ElapsedSeconds;
        public static double ElapsedMilliseconds;

        public static string EXECUTABLE_PATH = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
        public static string OPEN_PATH;

        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length > 0)
                OPEN_PATH = args[0];

            Log.Init();
            ActionKey.Init();

            main = new Main();
            Settings.LoadSettings();
            Hotkeys.LoadHotkeys();
            CreateGLControl();
            Application.Run(main);
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
            GLControl.KeyUp += new System.Windows.Forms.KeyEventHandler(main.glControl_KeyUp);
            GLControl.Leave += new System.EventHandler(main.glControl_Leave);
            GLControl.MouseDown += new System.Windows.Forms.MouseEventHandler(main.glControl_MouseDown);
            GLControl.MouseUp += new System.Windows.Forms.MouseEventHandler(main.glControl_MouseUp);
            GLControl.Resize += new System.EventHandler(main.glControl_Resize);

            main.splitContainer2.Panel1.Controls.Add(GLControl);
        }
    }
}
