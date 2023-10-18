using System;
using System.Collections.Generic;
using System.Linq;

namespace NoteBookConsoleApp
{
    public class Program
    {
        static string formatOutput = "||{0,-15}||{1,-15}||{2,-20}||{3,-20}||{4,-20}||{5,-15}||{6,-15}||{7,-15}||{8,-15}||";
        static string[] outputArray = new string[9]
        {
            "Имя", "Отчество", "Фамилия", "Номер телефона", "Страна", "Дата рождения", "Организация", "Должность", "Прочее"
        };

        static void Main(string[] args)
        {
            MainMenu();
        }

        static void MainMenu()
        {
            Console.WriteLine("Добро пожаловать в главное меню NoteBook");
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("{0,-15}", "1. Создание новой записи");
            Console.WriteLine("{0,-15}", "2. Редактирование созданных записей");
            Console.WriteLine("{0,-15}", "3. Удаление созданных записей");
            Console.WriteLine("{0,-15}", "4. Просмотр созданных записей, с полной информацией");
            Console.WriteLine("{0,-15}", "5. Просмотр всех созданных учетных записей, с краткой информацией");
            Console.WriteLine("\r\nВведите номер для перехода");

            var isCorrectUserAnswer = TryGetMainMenuNumber(Console.ReadLine(), out string errorMessage);

            while (!isCorrectUserAnswer)
            {
                Console.WriteLine(errorMessage);
                isCorrectUserAnswer = TryGetMainMenuNumber(Console.ReadLine(), out errorMessage);
            }
        }

        public static bool TryGetMainMenuNumber(string value, out string errorMessage)
        {
            switch (value)
            {
                case "1":
                    CreateNewNotebook();
                    errorMessage = null;
                    return true;
                case "2":
                    EditNotebook();
                    errorMessage = null;
                    return true;
                case "3":
                    RemoveNotebook();
                    errorMessage = null;
                    return true;
                case "4":
                    PrintFullInfo();
                    errorMessage = null;
                    return true;
                case "5":
                    PrintShortInfo();
                    errorMessage = null;
                    return true;

            }
            errorMessage = "Введите число от 1 до 5";
            return false;
        }

        static void CreateNewNotebook()
        {
            Console.WriteLine("Введите Имя");
            var inputFirstName = Console.ReadLine();
            Validator.TryGetCorrectStringValue(inputFirstName, out string firstName, out string errorMessage);
            while (!Validator.TryGetCorrectStringValue(inputFirstName, out firstName, out errorMessage))
            {
                Console.WriteLine(errorMessage);
                inputFirstName = Console.ReadLine();
            }

            Console.WriteLine("Введите Отчество, поле не является обязательным, для пропуска нажмите enter");
            var inputMiddleName = Console.ReadLine();
            Validator.TryGetCorrectOptionalStringValue(inputMiddleName, out string middleName, out errorMessage);
            while (!Validator.TryGetCorrectOptionalStringValue(inputMiddleName, out middleName, out errorMessage))
            {
                Console.WriteLine(errorMessage);
                inputMiddleName = Console.ReadLine();
            }

            Console.WriteLine("Введите Фамилию");
            var inputLastName = Console.ReadLine();
            Validator.TryGetCorrectStringValue(inputLastName, out string lastName, out errorMessage);
            while (!Validator.TryGetCorrectStringValue(inputLastName, out lastName, out errorMessage))
            {
                Console.WriteLine(errorMessage);
                inputLastName = Console.ReadLine();
            }

            Console.WriteLine("Введите свой номер телефона, начните ввод с 8");
            var inputPhoneNumber = Console.ReadLine();
            Validator.TryGetCorrectPhoneNumber(inputPhoneNumber, out string phoneNumber, out errorMessage);
            while (!Validator.TryGetCorrectPhoneNumber(inputPhoneNumber, out phoneNumber, out errorMessage))
            {
                Console.WriteLine(errorMessage);
                inputPhoneNumber = Console.ReadLine();
            }

            Console.WriteLine("Введите страну");
            var inputCountry = Console.ReadLine();
            Validator.TryGetCorrectStringValue(inputCountry, out string country, out errorMessage);
            while (!Validator.TryGetCorrectStringValue(inputCountry, out country, out errorMessage))
            {
                Console.WriteLine(errorMessage);
                inputCountry = Console.ReadLine();
            }

            Console.WriteLine("Введите дату рождения в формате ####-##-##, поле не является обязательным, для пропуска нажмите enter");
            var inputBirthDay = Console.ReadLine();
            Validator.TryGetCorrectBirthDate(inputBirthDay, out DateTime birthDay, out errorMessage);
            while (!Validator.TryGetCorrectBirthDate(inputBirthDay, out birthDay, out errorMessage))
            {
                Console.WriteLine(errorMessage);
                inputBirthDay = Console.ReadLine();
            }

            Console.WriteLine("Введите организацию, поле не является обязательным, для пропуска нажмите enter");
            var inputOrganization = Console.ReadLine();
            Validator.TryGetCorrectOptionalStringValue(inputOrganization, out string organization, out errorMessage);
            while (!Validator.TryGetCorrectOptionalStringValue(inputOrganization, out organization, out errorMessage))
            {
                Console.WriteLine(errorMessage);
                inputOrganization = Console.ReadLine();
            }

            Console.WriteLine("Введите должность, поле не является обязательным, для пропуска нажмите enter");
            var inputPosition = Console.ReadLine();
            Validator.TryGetCorrectOptionalStringValue(inputPosition, out string position, out errorMessage);
            while (!Validator.TryGetCorrectOptionalStringValue(inputPosition, out position, out errorMessage))
            {
                Console.WriteLine(errorMessage);
                inputPosition = Console.ReadLine();
            }

            Console.WriteLine("Введите свои примечания, поле не является обязательным, для пропуска нажмите enter");
            var inputOther = Console.ReadLine();
            Validator.TryGetCorrectOptionalStringValue(inputOther, out string other, out errorMessage);
            while (!Validator.TryGetCorrectOptionalStringValue(inputOther, out other, out errorMessage))
            {
                Console.WriteLine(errorMessage);
                inputOther = Console.ReadLine();
            }

            var notebook = new NoteBook(firstName, middleName, lastName, phoneNumber, country, birthDay, organization, position, other);
            NotebookStorage.SaveToFile(notebook);
            MainMenu();
        }

        static void EditNotebook()
        {
            Console.WriteLine("Хотите откорректировать запись? Введите да или нет");
            var isParsedUserAnswer = Validator.TryGetUserAnswer(Console.ReadLine(), out bool isTrueUserAnswer, out string errorMessage);
            while (!isParsedUserAnswer)
            {
                Console.WriteLine(errorMessage);
                isParsedUserAnswer = Validator.TryGetUserAnswer(Console.ReadLine(), out isTrueUserAnswer, out errorMessage);
            }
            if (isTrueUserAnswer)
            {
                var notebooks = NotebookStorage.GetFromFile();
                Print(notebooks);

                Console.WriteLine("Выберите номер записи для внесения корректировок");

                isParsedUserAnswer = TryGetNumberNotebook(Console.ReadLine(), out int numberToEdit, out errorMessage);
                while (!isParsedUserAnswer)
                {
                    Console.WriteLine(errorMessage);
                    isParsedUserAnswer = TryGetNumberNotebook(Console.ReadLine(), out numberToEdit, out errorMessage);
                }
                var notebook = notebooks[numberToEdit - 1];
                var positionDict = new Dictionary<int, Action<NoteBook>>()
                {
                    {1,new Action<NoteBook>(a =>
                    {
                        Console.WriteLine("Введите Имя");
                        var inputFirstName = Console.ReadLine();
                        Validator.TryGetCorrectStringValue(inputFirstName, out string firstName, out errorMessage);
                        while (!Validator.TryGetCorrectStringValue(inputFirstName, out firstName, out errorMessage))
                        {
                            Console.WriteLine(errorMessage);
                            inputFirstName = Console.ReadLine();
                        }
                        a.FirstName = firstName;
                    }) },
                    {2,new Action<NoteBook>(a =>
                    {
                        Console.WriteLine("Введите Отчество, поле не является обязательным, для пропуска нажмите enter");
                        var inputMiddleName = Console.ReadLine();
                        Validator.TryGetCorrectOptionalStringValue(inputMiddleName, out string middleName, out errorMessage);
                        while (!Validator.TryGetCorrectOptionalStringValue(inputMiddleName, out middleName, out errorMessage))
                        {
                            Console.WriteLine(errorMessage);
                            inputMiddleName = Console.ReadLine();
                        }
                        a.MiddleName = middleName;
                    }) },
                    {3,new Action<NoteBook>(a =>
                    {
                        Console.WriteLine("Введите Фамилию");
                        var inputLastName = Console.ReadLine();
                        Validator.TryGetCorrectStringValue(inputLastName, out string lastName, out errorMessage);
                        while (!Validator.TryGetCorrectStringValue(inputLastName, out lastName, out errorMessage))
                        {
                            Console.WriteLine(errorMessage);
                            inputLastName = Console.ReadLine();
                        }
                        a.LastName = lastName;
                    }) },
                    {4,new Action<NoteBook>(a =>
                    {
                        Console.WriteLine("Введите свой номер телефона, начните ввод с 8");
                        var inputPhoneNumber = Console.ReadLine();
                        Validator.TryGetCorrectPhoneNumber(inputPhoneNumber, out string phoneNumber, out errorMessage);
                        while (!Validator.TryGetCorrectPhoneNumber(inputPhoneNumber, out phoneNumber, out errorMessage))
                        {
                            Console.WriteLine(errorMessage);
                            inputPhoneNumber = Console.ReadLine();
                        }
                        a.PhoneNumber = phoneNumber;
                    }) },
                    {5,new Action<NoteBook>(a =>
                    {
                        Console.WriteLine("Введите страну");
                        var inputCountry = Console.ReadLine();
                        Validator.TryGetCorrectStringValue(inputCountry, out string country, out errorMessage);
                        while (!Validator.TryGetCorrectStringValue(inputCountry, out country, out errorMessage))
                        {
                            Console.WriteLine(errorMessage);
                            inputCountry = Console.ReadLine();
                        }
                        a.Country = country;
                    }) },
                    {6,new Action<NoteBook>(a =>
                    {
                        var inputBirthDay = Console.ReadLine();
                        var isCorrectDate = Validator.TryGetCorrectBirthDate(inputBirthDay, out DateTime birthDay, out errorMessage);
                        while (!isCorrectDate)
                        {
                            Console.WriteLine(errorMessage);
                            isCorrectDate = Validator.TryGetCorrectBirthDate(inputBirthDay, out birthDay, out errorMessage);
                        }
                        a.BirthDay = birthDay.ToShortDateString();
                    }) },
                    {7,new Action<NoteBook>(a =>
                    {
                        Console.WriteLine("Введите организацию, поле не является обязательным, для пропуска нажмите enter");
                        var inputOrganization = Console.ReadLine();
                        Validator.TryGetCorrectOptionalStringValue(inputOrganization, out string organization, out errorMessage);
                        while (!Validator.TryGetCorrectOptionalStringValue(inputOrganization, out organization, out errorMessage))
                        {
                            Console.WriteLine(errorMessage);
                            inputOrganization = Console.ReadLine();
                        }
                        a.Organization = organization;
                    }) },
                    {8,new Action<NoteBook>(a =>
                    {
                        Console.WriteLine("Введите должность, поле не является обязательным, для пропуска нажмите enter");
                        var inputPosition = Console.ReadLine();
                        Validator.TryGetCorrectOptionalStringValue(inputPosition, out string position, out errorMessage);
                        while (!Validator.TryGetCorrectOptionalStringValue(inputPosition, out position, out errorMessage))
                        {
                            Console.WriteLine(errorMessage);
                            inputPosition = Console.ReadLine();
                        }
                        a.Position = position;
                    }) },
                    {9,new Action<NoteBook>(a =>
                    {
                        Console.WriteLine("Введите свои примечания, поле не является обязательным, для пропуска нажмите enter");
                        var inputOther = Console.ReadLine();
                        Validator.TryGetCorrectOptionalStringValue(inputOther, out string other, out errorMessage);
                        while (!Validator.TryGetCorrectOptionalStringValue(inputOther, out other, out errorMessage))
                        {
                            Console.WriteLine(errorMessage);
                            inputOther = Console.ReadLine();
                        }
                        a.Other = other;
                    }) },

                };

                Console.WriteLine(formatOutput, outputArray);

                var properties = notebook.GetType().GetProperties();
                var propertiesValues = new List<object>();
                foreach (var prop in properties)
                {
                    var val = prop.GetValue(notebook);
                    propertiesValues.Add(val);
                }

                for (int i=0; i < propertiesValues.Count; i++)
                {
                    Console.WriteLine($"{i+1}-{propertiesValues.ElementAt(i)}");
                }

                Console.WriteLine("Выберите позицию для редактирования");
                isParsedUserAnswer = TryGetPositionNotebook(Console.ReadLine(), out numberToEdit, out errorMessage);
                while (!isParsedUserAnswer)
                {
                    Console.WriteLine(errorMessage);
                    isParsedUserAnswer = TryGetPositionNotebook(Console.ReadLine(), out numberToEdit, out errorMessage);
                }

                positionDict[numberToEdit].Invoke(notebook);
                NotebookStorage.Adjust(notebook, numberToEdit-1);
                MainMenu();
            }
            else
            {
                MainMenu();
            }
        }

        static void RemoveNotebook()
        {
            Console.WriteLine("Хотите удалить запись? введите да или нет");
            var isParsedUserAnswer = Validator.TryGetUserAnswer(Console.ReadLine(), out bool isTrueUserAnswer, out string errorMessage);
            while (!isParsedUserAnswer)
            {
                Console.WriteLine(errorMessage);
                isParsedUserAnswer = Validator.TryGetUserAnswer(Console.ReadLine(), out isTrueUserAnswer, out errorMessage);
            }
            if (isTrueUserAnswer)
            {
                var notebooks = NotebookStorage.GetFromFile();
                Print(notebooks);

                Console.WriteLine("Выберите номер записи для удаления");
                var inputUserAnswer = Console.ReadLine();

                isParsedUserAnswer = TryGetNumberNotebook(inputUserAnswer, out int numberToRemove, out errorMessage);
                while (!isParsedUserAnswer)
                {
                    Console.WriteLine(errorMessage);
                    isParsedUserAnswer = TryGetNumberNotebook(Console.ReadLine(), out numberToRemove, out errorMessage);
                }

                Console.WriteLine("Подтердите свой выбор, введите да или нет");
                isParsedUserAnswer = Validator.TryGetUserAnswer(Console.ReadLine(), out isTrueUserAnswer, out errorMessage);
                while (!isParsedUserAnswer)
                {
                    Console.WriteLine(errorMessage);
                    isParsedUserAnswer = Validator.TryGetUserAnswer(Console.ReadLine(), out isTrueUserAnswer, out errorMessage);
                }

                if (isTrueUserAnswer)
                {
                    NotebookStorage.RemoveAtPosition(numberToRemove-1);
                    MainMenu();
                }
            }

            else
            {
                MainMenu();
            }

        }

        static void PrintShortInfo()
        {
            Console.WriteLine("Хотите посмотреть записи? введите да или нет");
            var isParsedUserAnswer = Validator.TryGetUserAnswer(Console.ReadLine(), out bool isTrueUserAnswer, out string errorMessage);
            while (!isParsedUserAnswer)
            {
                Console.WriteLine(errorMessage);
                isParsedUserAnswer = Validator.TryGetUserAnswer(Console.ReadLine(), out isTrueUserAnswer, out errorMessage);
            }
            if (isTrueUserAnswer)
            {
                var notebooks = NotebookStorage.GetFromFile();
                string formatOutput = "||{0,-5} || {1,-10} || {2,-10} || {3,-10} ||";
                Console.WriteLine(formatOutput, "Поз.", "Фамилия:", "Имя:", "Номер телефона:");
                for (int i = 0; i < notebooks.Count; i++)
                {
                    Console.WriteLine($"{formatOutput}", new string[4] { (i + 1).ToString(), notebooks[i].LastName, notebooks[i].FirstName, notebooks[i].PhoneNumber });
                }
                Console.WriteLine("Для выхода в главное меню введите да или нет");
                isParsedUserAnswer = Validator.TryGetUserAnswer(Console.ReadLine(), out isTrueUserAnswer, out errorMessage);
                while (!isParsedUserAnswer)
                {
                    Console.WriteLine(errorMessage);
                    isParsedUserAnswer = Validator.TryGetUserAnswer(Console.ReadLine(), out isTrueUserAnswer, out errorMessage);
                }
                if (isTrueUserAnswer)
                {
                    MainMenu();
                }
            }
            else
            {
                MainMenu();
            }
        }

        static void PrintFullInfo()
        {
            Console.WriteLine("Хотите посмотреть записи? введите да или нет");
            var isParsedUserAnswer = Validator.TryGetUserAnswer(Console.ReadLine(), out bool isTrueUserAnswer, out string errorMessage);
            while (!isParsedUserAnswer)
            {
                Console.WriteLine(errorMessage);
                isParsedUserAnswer = Validator.TryGetUserAnswer(Console.ReadLine(), out isTrueUserAnswer, out errorMessage);
            }
            if (isTrueUserAnswer)
            {
                var notebooks = NotebookStorage.GetFromFile();
                Console.WriteLine(formatOutput, outputArray);
                for (int i = 0; i < notebooks.Count; i++)
                {
                    Console.WriteLine($"{formatOutput}", ((i + 1).ToString() + " " + notebooks[i].ToString()).Split());
                }
                Console.WriteLine("Для выхода в главное меню введите да или нет");
                isParsedUserAnswer = Validator.TryGetUserAnswer(Console.ReadLine(), out isTrueUserAnswer, out errorMessage);
                while (!isParsedUserAnswer)
                {
                    Console.WriteLine(errorMessage);
                    isParsedUserAnswer = Validator.TryGetUserAnswer(Console.ReadLine(), out isTrueUserAnswer, out errorMessage);
                }
                if (isTrueUserAnswer)
                {
                    MainMenu();
                }
            }
            else
            {
                MainMenu();
            }
        }

        static void Print(List<NoteBook> notebooks)
        {
            Console.WriteLine(formatOutput, outputArray);
            for (int i = 0; i < notebooks.Count; i++)
            {
                Console.WriteLine($"{formatOutput}", ((i + 1).ToString() + " " + notebooks[i].ToString()).Split());
            }
        }

        static bool TryGetNumberNotebook(string value, out int numberToEdit, out string errorMessage)
        {
            var notebooks = NotebookStorage.GetFromFile();
            var notebookCount = notebooks.Count;
            int number;
            try
            {
                number = Convert.ToInt32(value);
            }

            catch (FormatException)
            {
                errorMessage = "Введите число";
                numberToEdit = -1;
                return false;
            }

            catch (OverflowException)
            {
                errorMessage = "Введите согласно таблице";
                numberToEdit = -1;
                return false;
            }
            if (number > notebookCount)
            {
                errorMessage = "Введите согласно таблице";
                numberToEdit = -1;
                return false;
            }
            if (number < 1)
            {
                errorMessage = "Введите согласно таблице";
                numberToEdit = -1;
                return false;
            }
            errorMessage = null;
            numberToEdit = number;
            return true;
        }

        static bool TryGetPositionNotebook(string value, out int result, out string errorMessage)
        {
            while (true)
            {
                try
                {
                    result = Convert.ToInt32(value);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Введите число");
                    result = 0;
                    errorMessage = null;
                    return false;
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Вы ввели слишком большое число");
                    result = 0;
                    errorMessage = null;
                    return false;
                }
                if (result > typeof(NoteBook).GetProperties().Length)
                {
                    errorMessage = "Вы ввели слишком большое число";
                    result = 0;
                    return false;
                }
                else
                {
                    errorMessage = null;
                    return true;
                }
            }

        }
    }
}
