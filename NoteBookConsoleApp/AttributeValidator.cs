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

        public void CheckValidation(object o)
        {
            var shouldBeValidated = o.GetType()
                                     .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                     .SelectMany(f => f.CustomAttributes)
                                     .Where(f => validationAtt.Any(q => q.GetType() == f.GetType())).Count() > 0;

            if (shouldBeValidated)
            {
                var validationProp = o.GetType()
                                     .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                     .SelectMany(q => q.CustomAttributes);

                foreach (var prop in validationProp)
                {
                    var value = prop.GetValue(o, null);
                }
            }
        }
    }
}
