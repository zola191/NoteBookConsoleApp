using System;

namespace NoteBookApp.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class NotNullAttribute : Attribute, IAttributeValidator
    {
        private bool IsNull { get; set; }

        public NotNullAttribute(bool isNull)
        {
            IsNull = isNull;
        }

        public bool IsValid(object value, out string errorMessage)
        {
            if (value != null)
            {
                errorMessage = string.Empty;
                return true;
            }
            errorMessage = "Пустая строка";
            return false;
        }
    }
}
