using System.IO;

namespace NoteBookApp
{
    public static class FileProvider
    {
        private static string path = @"result.json";

        public static void Append(string value)
        {
            using (var fs = new FileStream(path, FileMode.Append, FileAccess.Write))
            {
                using (var sw = new StreamWriter(fs))
                {
                    sw.WriteLine(value);
                }
            }
        }

        public static void ReWrite(string value)
        {
            File.WriteAllText(path, value);
        }

        public static string Get()
        {
            if (!File.Exists(path))
            {
                return null;
            }
            using (var sw = new StreamReader(path))
            {
                return sw.ReadToEnd();
            }
        }
    }
}
