using System;
using System.Globalization;

namespace NoteBookConsoleApp.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class DateCorrectBirthDateAttribute : Attribute, IAttributeValidator
    {
        public bool IsValid(object value, out string errorMessage)
        {
            errorMessage = string.Empty;
            var item = (string)value;
            if (string.IsNullOrEmpty(item))
            {
                return true;
            }

            if (item.Length < 10 || item.Length > 10)
            {
                errorMessage = "Длина строки отличается от требуемой, введите дату рождения в формате ####-##-##";
                return false;
            }
            try
            {
                DateTime.ParseExact(item, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                return true;
            }
            catch (ArgumentNullException)
            {
                return true;
            }
            catch (FormatException)
            {
                errorMessage = "Неправильный формат, введите дату рождения в формате ####-##-##";
                return false;
            }
        }
    }
}
