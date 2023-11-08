using NoteBookApp.Interfaces;

namespace NoteBookApp
{
    public class FileProvider
    {
        private static string path = @"result.json";

        public void Append(string value)
        {
            using (var fs = new FileStream(path, FileMode.Append, FileAccess.Write))
            {
                using (var sw = new StreamWriter(fs))
                {
                    sw.WriteLine(value);
                }
            }
        }

        public void ReWrite(string value)
        {
            File.WriteAllText(path, value);
        }

        public string Get()
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
