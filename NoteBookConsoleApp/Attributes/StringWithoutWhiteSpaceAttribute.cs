using System;
using System.Linq;

namespace NoteBookConsoleApp.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class StringWithoutWhiteSpaceAttribute : Attribute, IAttributeValidator
    {
        public bool IsValid(object value, out string errorMessage)
        {
            var item = value as string;
            errorMessage = string.Empty;
            if (item.Any(char.IsWhiteSpace))
            {
                errorMessage = "строка содержит пробелы";
                return false;
            }
            return true;
        }
    }
}
