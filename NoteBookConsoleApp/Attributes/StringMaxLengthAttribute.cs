using System;

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
            var item = value as string;
            errorMessage = string.Empty;
            if (item.Length > MaxLength)
            {
                errorMessage = $"Длина строки больше {MaxLength}";
                return false;
            }
            return true;
        }
    }
}
