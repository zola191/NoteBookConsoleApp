using System;
using System.Xml.Schema;

namespace NoteBookConsoleApp.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class StringMaxLengthAttribute : Attribute, IAttributeValidator
    {
        public int MaxLength { get; set; }

        public StringMaxLengthAttribute(int maxLength)
        {
            MaxLength = maxLength;
        }
        public bool IsValid(object value, out string errorMessage)
        {
            if (value is string)
            {
                var item = (string) value;
                errorMessage = string.Empty;
                return item.Length > MaxLength;
            }
            errorMessage = $"Длина строки больше {MaxLength}";
            return false;
        }
    }
}
