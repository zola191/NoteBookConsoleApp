namespace NoteBookApp.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class StringContainsNumberAttribute : Attribute, IAttributeValidator
    {
        public bool IsContainsNumber { get; set; }

        public StringContainsNumberAttribute(bool isContainsNumber)
        {
            IsContainsNumber = isContainsNumber;
        }

        public bool IsValid(object value, out string errorMessage)
        {
            var item = value as string;
            errorMessage = null;
            if (item.Any(char.IsNumber) && IsContainsNumber == false)
            {
                errorMessage = "Строка не должна содержать цифры";
                return false;
            }
            return true;
        }
    }
}
