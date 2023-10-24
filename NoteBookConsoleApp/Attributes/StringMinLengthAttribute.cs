using System;

namespace NoteBookConsoleApp.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class StringMinLengthAttribute:Attribute, IAttributeValidator
    {
        public int MinLength { get; set; }

        public StringMinLengthAttribute(int minLength)
        {
            MinLength = minLength;
        }
        public bool IsValid(object value, out string errorMessage)
        {
            if (value is string)
            {
                var item = (string)value;
                errorMessage = string.Empty;
                return item.Length < MinLength;
            }
            errorMessage = $"Длина строки меньше {MinLength}";
            return false;
        }

    }
}
