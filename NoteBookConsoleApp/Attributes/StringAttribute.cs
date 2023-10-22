using System;
using System.Linq;
using System.Xml.Schema;

namespace NoteBookConsoleApp.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class StringAttribute : Attribute
    {
        public bool IsEmpty {  get; set; }
        public int MinLength { get; set; }
        public int MaxLength { get; set; }
        public char StartWith { get; set; }
        public string FormatString { get; set; }

        //проверка на Null?
        public static bool TryGetCorrectName(string value, out string result, out string errorMessage)
        {
            if (value == null)
            {
                result = null;
                errorMessage = "Строка не должна быть пустой";
                return false;
            }
            else if (value.Length < 2)
            {
                result = null;
                errorMessage = "Длина строки должна быть не менее 2 символов";
                return false;
            }
            else
            {
                result = value;
                errorMessage = null;
                return true;
            }
        }

        public static bool TryGetCorrectPhoneNumber(string value, out string result, out string errorMessage)
        {
            var adjustedValue = value.ToCharArray().Where(f => f != ' ');

            if (!adjustedValue.All(char.IsDigit))
            {
                result = null;
                errorMessage = "Используйте только цифры, начните ввод с 8";
                return false;
            }

            if (adjustedValue.Count() != 11)
            {
                if (adjustedValue.First() != '8')
                {
                    result = null;
                    errorMessage = "Номер должен начинаться с 8. Длина номера не должна превышать 11 символов";
                    return false;
                }
                result = null;
                errorMessage = "Длина номера не должна превышать 11 символов";
                return false;
            }

            if (adjustedValue.First() != '8')
            {
                result = null;
                errorMessage = "Номер должен начинаться с 8.";
                return false;
            }
            result = string.Join("", adjustedValue);
            errorMessage = null;
            return true;
        }
    }
}
