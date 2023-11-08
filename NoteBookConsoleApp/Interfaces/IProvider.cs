namespace NoteBookApp.Interfaces
{
    public interface IProvider
    {
        void Append(Notebook noteBook); 
        List<Notebook> Get();
        void ChangeNotebook(Notebook notebook);
        void DeleteNotebook(Notebook notebook);
    }
}
