using Newtonsoft.Json;
using System.Collections.Generic;

namespace NoteBookConsoleApp
{
    public static class NotebookStorage
    {
        public static void SaveByIndex(NoteBook noteBook, int position)
        {
            var noteBooks = GetFromFile();
            noteBooks.RemoveAt(position);
            noteBooks.Insert(position, noteBook);
            var serializeNoteBooks = JsonConvert.SerializeObject(noteBooks, Formatting.Indented);
            FileProvider.ReWrite(serializeNoteBooks);
        }

        public static void SaveToFile(NoteBook noteBook)
        {
            var noteBooks = GetFromFile();
            noteBooks.Add(noteBook);
            var serializeNoteBooks = JsonConvert.SerializeObject(noteBooks, Formatting.Indented);
            FileProvider.ReWrite(serializeNoteBooks);
        }

        public static void RemoveAtPosition(int position)
        {
            var noteBooks = GetFromFile();
            noteBooks.RemoveAt(position);
            var serializeNoteBooks = JsonConvert.SerializeObject(noteBooks, Formatting.Indented);
            FileProvider.ReWrite(serializeNoteBooks);
        }

        public static List<NoteBook> GetFromFile()
        {
            var values = FileProvider.Get();
            if (values == null)
            {
                return new List<NoteBook>();
            }
            return JsonConvert.DeserializeObject<List<NoteBook>>(values);
        }
    }
}
