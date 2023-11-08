using NoteBookApp.Interfaces;
using NoteBookApp.Data;

namespace NoteBookApp
{
    public class DBNotebookProvider : IProvider
    {
        public void Append(Notebook noteBook)
        {
            using (var context = new ApplicationDBContext())
            {
                //context.Model.GetEntityTypes().SingleOrDefault(f=>f); DAO Class
                //IRerositoryPattern EntityFramework;
                //в рамках одного DBProvider через switch или отдельные классы например DBNotebookProvider
                context.NoteBooks.Add(noteBook);
                context.SaveChanges();
            }
        }

        public List<Notebook> Get()
        {
            using (var context = new ApplicationDBContext())
            {
                return context.NoteBooks.Select(f => f).ToList();
            }
        }

        public void ReWrite(Notebook oldNotebook, Notebook newNotebook)
        {
            using (var context = new ApplicationDBContext())
            {
                context.NoteBooks.Remove(oldNotebook);
                context.NoteBooks.Add(newNotebook);
            }
        }

        public void ChangeNotebook(Notebook notebook)
        {
            using (var context = new ApplicationDBContext())
            {
                context.Update(notebook);
                context.SaveChanges();
            }
        }

        public void DeleteNotebook(Notebook notebook)
        {
            using (var context = new ApplicationDBContext())
            {
                context.Remove(notebook);
                context.SaveChanges();
            }
        }
    }
}
