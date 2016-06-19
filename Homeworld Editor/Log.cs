using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworldDAEEditor
{
    static class Log
    {
        private static FileStream stream;

        public static void Init()
        {
            stream = new FileStream(Path.Combine(Program.EXECUTABLE_PATH, "log.txt"), FileMode.Create, FileAccess.Write, FileShare.Read);
        }

        public static void WriteLine(string line)
        {
            Console.WriteLine(line);

            byte[] bytes = Encoding.UTF8.GetBytes((line + "\n").ToCharArray());
            stream.Write(bytes, 0, bytes.Length);
        }

        public static void Close()
        {
            stream.Close();
        }
    }
}
