using System;
using System.Globalization;
using System.Linq;

namespace NoteBookConsoleApp
{
    public static class Validator
    {
        public static bool TryGetCorrectStringValue(string value, out string result, out string errorMessage)
        {
            if (!value.All(char.IsLetter) || string.IsNullOrEmpty(value))
            {
                result = null;
                errorMessage = "Используйте только буквы";
                return false;
            }

            else
            {
                result = char.ToUpper(value[0])+value.Substring(1);
                errorMessage = null;
                return true;
            }
        }

        public static bool TryGetCorrectOptionalStringValue(string value, out string result, out string errorMessage)
        {
            if (string.IsNullOrEmpty(value))
            {
                result = null;
                errorMessage = null;
                return true;
            }

            else
            {
                if (!value.All(char.IsLetter))
                {
                    result = null;
                    errorMessage = "Используйте только буквы, поле не является обязательным, для пропуска нажмите enter";
                    return false;
                }

                else
                {
                    result = char.ToUpper(value[0]) + value.Substring(1);
                    errorMessage = null;
                    return true;
                }
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
                result = null;
                errorMessage = "Длина номера не должна превышать 11 символов";
                return false;
            }

            if (adjustedValue.First() != '8')
            {
                result = null;
                errorMessage = "Номер не начинается с 8";
                return false;
            }
            result = string.Join("", adjustedValue);
            errorMessage = null;
            return true;
        }

        public static bool TryGetCorrectBirthDate(string value, out DateTime result, out string errorMessage)
        {
            if (string.IsNullOrEmpty(value))
            {
                errorMessage = null;
                result = default(DateTime);
                return true;
            }

            else
            {
                if (value.Length != 10)
                {
                    errorMessage = "Введите дату рождения в формате ####-##-##, поле не является обязательным, для пропуска нажмите enter";
                    result = default(DateTime);
                    return false;
                }

                else
                {
                    try
                    {
                        errorMessage = null;
                        result = DateTime.ParseExact(value, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                        return true;
                    }
                    catch (FormatException)
                    {
                        errorMessage = "Введите дату рождения в формате ####-##-##, поле не является обязательным, для пропуска нажмите enter";
                        result = default(DateTime);
                        return false;
                    }
                }
            }
        }

        public static bool TryGetUserAnswer(string value,out bool result, out string errorMessage)
        {
            while (true)
            {
                if (value.ToLower() == "да")
                {
                    errorMessage = null;
                    result = true;
                    return true;
                }

                if (value.ToLower() == "нет")
                {
                    errorMessage = null;
                    result = false;
                    return true;
                }

                errorMessage = "Введите да или нет";
                result = false;
                return false;
            }
        }

    }
}
