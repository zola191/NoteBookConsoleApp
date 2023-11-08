using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NoteBookApp.Interfaces;

namespace NoteBookApp
{
    public class FileNotebookProvider : IProvider
    {
        private static string path = @"result.json";
        public void Append(Notebook noteBook)
        {
            var notebooks = Get();
            if (notebooks == null)
            {
                notebooks = new List<Notebook>();
            }
            notebooks.Add(noteBook);
            var serializeNotebooks = JsonConvert.SerializeObject(notebooks, Formatting.Indented);
            using (var fs = new FileStream(path, FileMode.Append, FileAccess.Write))
            {
                using (var sw = new StreamWriter(fs))
                {
                    sw.WriteLine(serializeNotebooks);
                }
            }
        }

        public List<Notebook> Get()
        {
            if (!File.Exists(path))
            {
                return null;
            }
            using (var sw = new StreamReader(path))
            {
                var json = sw.ReadToEnd();
                return JsonConvert.DeserializeObject<List<Notebook>>(json);
            }
        }

        public void ChangeNotebook(Notebook notebook)
        {
            var notebooks = Get();
            //var index = notebooks.IndexOf(notebook); не пойму почему получаю -1 индекс разные ссылки
            var index = notebooks.IndexOf(notebooks.Where(f => f.Id == notebook.Id).First());
            notebooks.RemoveAt(index);
            notebooks.Insert(index, notebook);
            var serializeNotebooks = JsonConvert.SerializeObject(notebooks, Formatting.Indented);
            File.WriteAllText(path, serializeNotebooks);
        }

        public void DeleteNotebook(Notebook notebook)
        {
            var notebooks = Get();
            var index = notebooks.IndexOf(notebooks.Where(f => f.Id == notebook.Id).First());
            notebooks.RemoveAt(index);
            var serializeNotebooks = JsonConvert.SerializeObject(notebooks, Formatting.Indented);
            File.WriteAllText(path, serializeNotebooks);
        }
    }
}
