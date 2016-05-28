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

        public static Camera Camera = new Camera();
        public static GLControl GLControl;

        public static OpenTK.NativeWindow NativeWindow = new OpenTK.NativeWindow();

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            main = new Main();
            Application.Run(main);

            Camera.Init();
        }
    }
}
