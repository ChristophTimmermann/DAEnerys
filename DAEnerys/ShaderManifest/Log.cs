using System;
using System.IO;
using System.Text;

namespace NewShaderManifest
{
    static class Log
    {
        private static string PATH = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
        private static StreamWriter stream;

        public static void Init()
        {
            stream = new StreamWriter(new FileStream(Path.Combine(PATH, "manifest.log"), FileMode.Create, FileAccess.Write, FileShare.Read));
        }

        public static void Reopen()
        {
            if (stream == null)
                stream = new StreamWriter(new FileStream(Path.Combine(PATH, "manifest.log"), FileMode.Append, FileAccess.Write, FileShare.Read));
        }

        public static void WriteLine(string line)
        {
            if (stream == null) Reopen();
            Console.WriteLine(line);
            stream.WriteLine(line);
        }

        public static void Close()
        {
            stream.Close();
            stream = null;
        }
    }
}
