using System;
using System.Linq;

namespace NoteBookApp.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class StringOfDigitAttribute : Attribute, IAttributeValidator
    {
        public bool IsAllCharDigit { get; set; }

        public StringOfDigitAttribute(bool isAllCharDigit)
        {
            IsAllCharDigit = isAllCharDigit;
        }

        public bool IsValid(object value, out string errorMessage)
        {
            var item = value as string;
            errorMessage = string.Empty;
            if (item != null && item.All(char.IsDigit))
            {
                return true;
            }
            errorMessage = "Строка должна состоять из цифр";
            return false;
        }
    }
}
