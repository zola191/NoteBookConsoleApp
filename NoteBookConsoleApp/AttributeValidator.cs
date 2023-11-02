using NoteBookConsoleApp.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NoteBookConsoleApp
{
    public class AttributeValidator
    {
        private List<Type> validationAtt = new List<Type>()
        {
            typeof(StringMinLengthAttribute),
            typeof(StringMaxLengthAttribute),
            typeof(NotNullAttribute),
            typeof(DateCorrectBirthDateAttribute)
        };

        public bool CheckValidation(object o)
        {
            var shouldBeValidated = o.GetType()
                     .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                     .SelectMany(f => f.CustomAttributes)
                     .Any(f => validationAtt.Any(q => q == f.AttributeType));

            if (shouldBeValidated)
            {
                var propinfo = o.GetType()
                                .GetProperties()
                                .Where(f => f.CustomAttributes.Any(q => validationAtt.Any(w => w == q.AttributeType)));

                foreach (var prop in propinfo)
                {
                    var value = prop.GetValue(o, null);
                    var atts = prop.GetCustomAttributes(true);
                    if (atts != null)
                    {
                        foreach (var att in atts)
                        {
                            if (att is IAttributeValidator)
                            {
                                var validator = (IAttributeValidator)att;
                                if (validator.IsValid(value, out string errorMessage))
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }


        public bool CheckValidation(Func<PropertyInfo> func, string value,out List<string> errors)
        {
            errors = new List<string>();

            var shouldBeValidated = func?.Invoke()
                                        .CustomAttributes
                                        .Any(f => validationAtt.Any(q => q == f.AttributeType));

            if (shouldBeValidated.Value)
            {
                var propinfo = func.Invoke();
                var atts = propinfo.GetCustomAttributes(true);
                if (atts != null)
                {
                    foreach (var att in atts)
                    {
                        if (att is IAttributeValidator)
                        {
                            var validator = (IAttributeValidator)att;
                            if (!validator.IsValid(value, out string errorMessage))
                            {
                                errors.Add(errorMessage);
                            }
                        }
                    }
                }
            }
            return errors.Count() == 0;
        }
    }
}
