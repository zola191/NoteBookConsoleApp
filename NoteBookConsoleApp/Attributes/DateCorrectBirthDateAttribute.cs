using System.Globalization;

namespace NoteBookApp.Attributes
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
                errorMessage = "Длина строки отличается от требуемой";
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
                errorMessage = "Требуется задать строку согласно требуемому формату";
                return false;
            }
        }
    }
}
