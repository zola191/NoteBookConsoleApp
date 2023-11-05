using System;

namespace NoteBookApp.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class StringStartWithAttribute : Attribute, IAttributeValidator
    {
        public string StartWithValue { get; set; }

        public StringStartWithAttribute(string startWith)
        {
            StartWithValue = startWith;
        }

        public bool IsValid(object value, out string errorMessage)
        {
            var item = value as string;
            errorMessage = string.Empty;
            if (item.StartsWith(StartWithValue))
            {
                return true;
            }
            errorMessage = $"Строка не начинается с {StartWithValue}";
            return false;
        }
    }
}
