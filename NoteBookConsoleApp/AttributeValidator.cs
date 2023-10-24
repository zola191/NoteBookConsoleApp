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
                                     .Where(f => validationAtt.Any(q => q == f.GetType())).Count() > 0;

            if (shouldBeValidated)
            {
                var propinfo = o.GetType()
                                .GetProperties()
                                .Where(f=>f.CustomAttributes.Any(q=> validationAtt.Contains(q.GetType())));

                foreach (var prop in propinfo)
                {
                    var value = prop.GetValue(o, null);
                }

            }

            return false;
        }
    }
}
