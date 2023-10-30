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
            typeof(NotNullAttribute)
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
                                var validator =(IAttributeValidator) new object();
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


        public bool CheckValidation(object o, Func<PropertyInfo> func, string value)
        {
            var shouldBeValidated = o.GetType()
                     .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                     .Where(f => f.Name == func.Invoke().Name)
                     .SelectMany(f => f.CustomAttributes)
                     .Any(f => validationAtt.Any(q => q == f.AttributeType));

            if (shouldBeValidated)
            {
                var propinfo = func.Invoke();
                var atts = propinfo.GetCustomAttributes(true);
                if (atts != null)
                {
                    foreach (var att in atts)
                    {
                        if (att is IAttributeValidator)
                        {
                            var validator = (IAttributeValidator)new object();
                            if (validator.IsValid(value, out string errorMessage))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
    }
}
