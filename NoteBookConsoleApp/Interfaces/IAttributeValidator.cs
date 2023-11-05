namespace NoteBookApp
{
    public interface IAttributeValidator
    {
        bool IsValid(object value, out string errorMessage);
    }
}
