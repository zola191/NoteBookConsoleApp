using System;

namespace NoteBookConsoleApp.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class StringMinLengthAttribute : Attribute, IAttributeValidator
    {
        public int MinLength { get; set; }
        public bool IsNullable { get; set; }

        public StringMinLengthAttribute(int minLength) // не более 5 аргументов в атрибуте
        {
            MinLength = minLength;
        }
        public StringMinLengthAttribute(int minLength, bool isNullable) // не более 5 аргументов в атрибуте
        {
            MinLength = minLength;
            IsNullable = isNullable;
        }
        public bool IsValid(object value, out string errorMessage)
        {
            errorMessage = string.Empty;
            if (IsNullable && string.IsNullOrEmpty((string)value))
            {
                return true;
            }
            
            if (!IsNullable && string.IsNullOrEmpty((string)value))
            {
                errorMessage = $"Строка обязательна для заполнения";
                return false;
            }

            var item = (string)value;
            if (item.Length < MinLength)
            {
                errorMessage = $"Длина строки не должна быть меньше {MinLength} символов";
                return false;
            }
            return true;
        }
    }
}
