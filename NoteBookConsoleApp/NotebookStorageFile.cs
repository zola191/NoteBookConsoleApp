using Newtonsoft.Json;

namespace NoteBookApp
{
    public static class NotebookStorageFile
    {
        public static void SaveByIndex(Notebook noteBook, int position)
        {
            var noteBooks = GetFromFile();
            noteBooks.RemoveAt(position);
            noteBooks.Insert(position, noteBook);
            var serializeNoteBooks = JsonConvert.SerializeObject(noteBooks, Formatting.Indented);
            var fileProvider = new FileProvider();
            fileProvider.ReWrite(serializeNoteBooks);
        }

        public static void SaveToFile(Notebook noteBook)
        {
            var noteBooks = GetFromFile();
            noteBooks.Add(noteBook);
            var serializeNoteBooks = JsonConvert.SerializeObject(noteBooks, Formatting.Indented);
            var fileProvider = new FileProvider();
            fileProvider.ReWrite(serializeNoteBooks);
        }

        public static void RemoveAtPosition(int position)
        {
            var noteBooks = GetFromFile();
            noteBooks.RemoveAt(position);
            var serializeNoteBooks = JsonConvert.SerializeObject(noteBooks, Formatting.Indented);
            var fileProvider = new FileProvider();
            fileProvider.ReWrite(serializeNoteBooks);
        }

        public static List<Notebook> GetFromFile()
        {
            var fileProvider =new FileProvider();
            var values = fileProvider.Get();
            if (values == null)
            {
                return new List<Notebook>();
            }
            return JsonConvert.DeserializeObject<List<Notebook>>(values);
        }
    }
}
