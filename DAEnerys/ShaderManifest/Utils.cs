using System;
using System.Collections.Generic;
using System.IO;
using OpenTK.Graphics.OpenGL;

namespace NewShaderManifest
{
    internal static class Utils
    {
        internal static T PopFirst<T>(this List<T> list)
        {
            T t = list[0];
            list.RemoveAt(0);
            return t;
        }

        internal static T PopFirst<T>(ref T[] array)
        {
            T[] temp = array;
            Array.Resize(ref array, array.Length - 1);
            Array.ConstrainedCopy(temp, 1, array, 0, array.Length);
            T t = temp[0];
            return t;
        }

        internal static string ReplaceAll(this string str, char oldChar, char newChar)
        {
            if (str.IndexOf(oldChar) == -1) return str;
            return ReplaceAll(str.Replace(oldChar, newChar), oldChar, newChar);
        }

        internal static string ReplaceAll(this string str, string oldValue, string newValue)
        {
            if (str.IndexOf(oldValue) == -1) return str;
            return ReplaceAll(str.Replace(oldValue, newValue), oldValue, newValue);
        }

        public static string GetLine(StreamReader sr)
        {
            string line = "";
            try
            {
                line = sr.ReadLine();
                int idx = line.IndexOf("//");
                if (idx >= 0) line = line.Substring(0, idx);
                return line.ReplaceAll('\t', ' '); //.ReplaceAll("  ", " ").Trim();
            }
            catch (IOException e)
            {
                if (sr.EndOfStream) return null;
                Log.WriteLine("Error reading file: " + e.Message);
                return null;
            }
        }

        public static bool GetLine(StreamReader sr, out string line)
        {
            line = "";
            try
            {
                line = sr.ReadLine();
                int comment = line.IndexOf("//");
                if (comment >= 0)
                    line = line.Substring(0, comment);
                line = line.Trim();
                line = line.ReplaceAll('\t', ' ');
                line = line.ReplaceAll("  ", " ");

                return true;
            }
            catch (Exception e)
            {
                if (sr.EndOfStream)
                    return false;
                Log.WriteLine("Error reading file: " + e.Message);
                return false;
            }
        }

        public static string[] GetDataPath(string filename)
        {
            List<string> files = new List<string>();
            foreach (string datapath in Manifest.DATA_PATHS)
            {
                string datafile = Path.Combine(datapath, filename);
                if (File.Exists(datafile))
                    files.Add(datafile);
            }
            return files.ToArray();
        }

        public static void GetGLError(string type)
        {
            ErrorCode code = GL.GetError();
            if (code != ErrorCode.NoError)
                Log.WriteLine(type + ": " + code);
        }
    }
}
