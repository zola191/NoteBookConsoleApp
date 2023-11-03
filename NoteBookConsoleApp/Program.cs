using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace NoteBookConsoleApp
{
    public class Program
    {
        static string formatOutput = "||{0,-15}||{1,-15}||{2,-20}||{3,-20}||{4,-20}||{5,-20}||{6,-15}||{7,-20}||{8,-25}||{9,-35}||";
        static string[] outputArray = new string[10]
        {
            "№","Имя", "Отчество", "Фамилия", "Номер телефона", "Страна", "Дата рождения", "Организация", "Должность", "Прочее"
        };
        static int index = 0;
        static List<string> menuLabels = new List<string>()
        {
                "1. Создание новой записи",
                "2. Редактирование созданных записей",
                "3. Удаление созданных записей",
                "4. Просмотр всех созданных учетных записей, с краткой информацией",
                "5. Просмотр созданных записей, с полной информацией",
                "6. Выход"
        };
        static Dictionary<string, Func<PropertyInfo>> dict = new Dictionary<string, Func<PropertyInfo>>()
        {
            { "FirstName", ()=>typeof(NoteBook).GetProperty("FirstName") },
            { "MiddleName", ()=>typeof(NoteBook).GetProperty("MiddleName") },
            { "LastName", ()=>typeof(NoteBook).GetProperty("LastName") },
            { "PhoneNumber", ()=>typeof(NoteBook).GetProperty("PhoneNumber") },
            { "Country", ()=>typeof(NoteBook).GetProperty("Country") },
            { "BirthDay", ()=>typeof(NoteBook).GetProperty("BirthDay") },
            { "Organization", ()=>typeof(NoteBook).GetProperty("Organization") },
            { "Position", ()=>typeof(NoteBook).GetProperty("Position") },
            { "Other", ()=>typeof(NoteBook).GetProperty("Other") },
        };

        static void Main(string[] args)
        {
            var notebok = new NoteBook("1", "2", "3", "80535145678", "1", new DateTime(1991, 1, 1), "1", "2", "3");

            while (true)
            {
                var selectedMenuItem = MainMenu(menuLabels);
                if (selectedMenuItem == (int)Menu.CreateNewNote)
                {
                    Console.Clear();
                    Console.WriteLine("Создание новой записи");
                    CreateNewNotebook();
                }
                else if (selectedMenuItem == (int)Menu.EditNote)
                {
                    Console.Clear();
                    Console.WriteLine("Редактирование записей");
                    EditNotebook();
                }
                else if (selectedMenuItem == (int)Menu.RemoveNote)
                {
                    Console.Clear();
                    Console.WriteLine("Удаление записей");
                    RemoveNotebook();
                }
                else if (selectedMenuItem == (int)Menu.ShowFullNote)
                {
                    Console.Clear();
                    Console.WriteLine("Полная запись");
                    PrintFullInfo();
                }
                else if (selectedMenuItem == (int)Menu.ShowShortNote)
                {
                    Console.Clear();
                    Console.WriteLine("Короткая запись");
                    PrintShortInfo();
                }
                else if (selectedMenuItem == (int)Menu.Exit)
                {
                    Console.Clear();
                    break;
                }
            }
        }

        private static int MainMenu(List<string> menuLabels)
        {
            //Console.OutputEncoding = System.Text.Encoding.UTF8;
            //Console.InputEncoding = System.Text.Encoding.UTF8;

            for (int i = 0; i < menuLabels.Count; i++)
            {
                if (i == index)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("{0,-15}", menuLabels[i]);
                }
                else
                {
                    Console.WriteLine("{0,-15}", menuLabels[i]);
                }
                Console.ResetColor();
            }
            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key == ConsoleKey.DownArrow)
            {
                if (index == menuLabels.Count - 1)
                {
                    index = 0;
                }
                else
                {
                    index++;
                }
            }
            else if (key.Key == ConsoleKey.UpArrow)
            {
                if (index <= 0)
                {
                    index = menuLabels.Count - 1;
                }
                else
                {
                    index--;
                }
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                return index;
            }
            else if (key.Key == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }
            Console.Clear();
            return -1;
        }

        static void CreateNewNotebook()
        {
            var attributeValidator = new AttributeValidator();
            //GetInput("Введите свой номер телефона, начните ввод с 8", "PhoneNumber");
            //GetInput("Введите дату рождения в формате ####-##-##, поле не является обязательным, для пропуска нажмите enter", "BirthDay");

            var noteBook = new NoteBook();

            Console.WriteLine("Введите Имя");
            var inputFirstName = Console.ReadLine();
            
            var valid = attributeValidator.CheckValidation(dict["FirstName"], inputFirstName, out List<string> errors);
            while (!valid)
            {
                Console.Clear();
                PrintErrors(errors);
                Console.WriteLine("Введите Имя");
                inputFirstName = Console.ReadLine();
                valid = attributeValidator.CheckValidation(dict["FirstName"], inputFirstName, out errors);
            }

            Console.WriteLine("Введите Отчество, поле не является обязательным, для пропуска нажмите enter");
            var inputMiddleName = Console.ReadLine();

            valid = attributeValidator.CheckValidation(dict["MiddleName"], inputMiddleName, out errors);
            while (!valid)
            {
                Console.Clear();
                PrintErrors(errors);
                Console.WriteLine("Введите Отчество, поле не является обязательным, для пропуска нажмите enter");
                inputFirstName = Console.ReadLine();
                valid = attributeValidator.CheckValidation(dict["MiddleName"], inputMiddleName, out errors);
            }

            Console.WriteLine("Введите Фамилию");
            var inputLastName = Console.ReadLine();
            valid = attributeValidator.CheckValidation(dict["LastName"], inputLastName, out errors);
            while (!valid)
            {
                Console.Clear();
                PrintErrors(errors);
                Console.WriteLine("Введите Фамилию");
                inputLastName = Console.ReadLine();
                valid = attributeValidator.CheckValidation(dict["LastName"], inputLastName, out errors);
            }

            Console.WriteLine("Введите свой номер телефона, начните ввод с 8");
            var inputPhoneNumber = Console.ReadLine();
            valid = attributeValidator.CheckValidation(dict["PhoneNumber"], inputPhoneNumber, out errors);
            while (!valid)
            {
                Console.Clear();
                PrintErrors(errors);
                Console.WriteLine("Введите свой номер телефона, начните ввод с 8");
                inputPhoneNumber = Console.ReadLine();
                valid = attributeValidator.CheckValidation(dict["PhoneNumber"], inputPhoneNumber, out errors);
            }

            Console.WriteLine("Введите страну");
            var inputCountry = Console.ReadLine();
            valid = attributeValidator.CheckValidation(dict["Country"], inputCountry, out errors);
            while (!valid)
            {
                Console.Clear();
                PrintErrors(errors);
                Console.WriteLine("Введите страну");
                inputCountry = Console.ReadLine();
                valid = attributeValidator.CheckValidation(dict["Country"], inputCountry, out errors);
            }

            Console.WriteLine("Введите дату рождения в формате ####-##-##, поле не является обязательным, для пропуска нажмите enter");
            var inputBirthDay = Console.ReadLine();
            valid = attributeValidator.CheckValidation(dict["BirthDay"], inputBirthDay, out errors);
            while (!valid)
            {
                Console.Clear();
                PrintErrors(errors);
                Console.WriteLine("Введите дату рождения в формате ####-##-##, поле не является обязательным, для пропуска нажмите enter");
                inputBirthDay = Console.ReadLine();
                valid = attributeValidator.CheckValidation(dict["BirthDay"], inputBirthDay, out errors);
            }

            Console.WriteLine("Введите организацию, поле не является обязательным, для пропуска нажмите enter");
            var inputOrganization = Console.ReadLine();
            valid = attributeValidator.CheckValidation(dict["Organization"], inputOrganization, out errors);
            while (!valid)
            {
                Console.Clear();
                PrintErrors(errors);
                Console.WriteLine("Введите организацию, поле не является обязательным, для пропуска нажмите enter");
                inputOrganization = Console.ReadLine();
                valid = attributeValidator.CheckValidation(dict["Organization"], inputOrganization, out errors);
            }

            Console.WriteLine("Введите должность, поле не является обязательным, для пропуска нажмите enter");
            var inputPosition = Console.ReadLine();
            valid = attributeValidator.CheckValidation(dict["Position"], inputPosition, out errors);
            while (!valid)
            {
                Console.Clear();
                PrintErrors(errors);
                Console.WriteLine("Введите должность, поле не является обязательным, для пропуска нажмите enter");
                inputPosition = Console.ReadLine();
                valid = attributeValidator.CheckValidation(dict["Position"], inputPosition, out errors);
            }

            Console.WriteLine("Введите свои примечания, поле не является обязательным, для пропуска нажмите enter");
            var inputOther = Console.ReadLine();
            valid = attributeValidator.CheckValidation(dict["Other"], inputOther, out errors);
            while (!valid)
            {
                Console.Clear();
                PrintErrors(errors);
                Console.WriteLine("Введите свои примечания, поле не является обязательным, для пропуска нажмите enter");
                inputOther = Console.ReadLine();
                valid = attributeValidator.CheckValidation(dict["Other"], inputOther, out errors);
            }

            var birthDay = DateTime.ParseExact(inputBirthDay, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            var notebook = new NoteBook(inputFirstName, inputMiddleName, inputLastName,
                                        inputPhoneNumber, inputCountry, birthDay,
                                        inputOrganization, inputPosition, inputOther);

            NotebookStorage.SaveToFile(notebook);
            Console.WriteLine("Запись успешно создана");
            for (int i = 3; i >= 0; i--)
            {
                Console.WriteLine($"Вы перейдете в главное меню через {i}");
                Thread.Sleep(1000);
            }
            Console.Clear();
            MainMenu(menuLabels);
        }

        static void PrintErrors(List<string> errors)
        {
            Console.WriteLine("Список допущенных ошибок при вводе");
            for (int i = 0; i < errors.Count; i++)
            {
                Console.WriteLine($"Ошибка №{i + 1} - {errors[i]}");
            }
        }

        static void GetInput(string input, string fieldName)
        {
            Console.WriteLine(input);
            var attributeValidator = new AttributeValidator();
            var inputFirstName = Console.ReadLine();
            var valid = attributeValidator.CheckValidation(dict[fieldName], inputFirstName, out List<string> errors);
            while (!valid)
            {
                PrintErrors(errors);
                Console.WriteLine(input);
                inputFirstName = Console.ReadLine();
                valid = attributeValidator.CheckValidation(dict[fieldName], inputFirstName, out errors);
            }
        }

        static void EditNotebook()
        {

            var notebooks = NotebookStorage.GetFromFile();
            Print(notebooks);

            Console.WriteLine("Выберите номер записи для внесения корректировок");

            var isParsedUserAnswer = TryGetNumberNotebook(Console.ReadLine(), out int numberNoteToEdit, out string errorMessage);
            while (!isParsedUserAnswer)
            {
                Console.WriteLine(errorMessage);
                isParsedUserAnswer = TryGetNumberNotebook(Console.ReadLine(), out numberNoteToEdit, out errorMessage);
            }
            var notebook = notebooks[numberNoteToEdit - 1];
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
                        a.BirthDay = birthDay;
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
                        a.Other = Console.ReadLine();
                    }) },

                };

            var properties = notebook.GetType().GetProperties();
            var propertiesValues = new List<object>();
            foreach (var prop in properties)
            {
                var val = prop.GetValue(notebook);
                propertiesValues.Add(val);
            }

            for (int i = 0; i < propertiesValues.Count; i++)
            {
                Console.WriteLine($"{i + 1}-{propertiesValues.ElementAt(i)}");
            }

            Console.WriteLine("Выберите позицию для редактирования");
            isParsedUserAnswer = TryGetPositionNotebook(Console.ReadLine(), out int numberToEdit, out errorMessage);
            while (!isParsedUserAnswer)
            {
                Console.WriteLine(errorMessage);
                isParsedUserAnswer = TryGetPositionNotebook(Console.ReadLine(), out numberToEdit, out errorMessage);
            }

            positionDict[numberToEdit].Invoke(notebook);
            NotebookStorage.Adjust(notebook, numberNoteToEdit - 1);
            //добавить продолжение корректировки выбранной записи или дать возможность выбрать другую запись для редактирования
            Console.WriteLine("Для возврата в главное меню нажмите Backspace, для выхода нажмите Esq");
            MenuKey();
        }

        private static void MenuKey()
        {
            ConsoleKeyInfo key = Console.ReadKey();
            while (true)
            {
                if (key.Key == ConsoleKey.Backspace)
                {
                    Console.Clear();
                    return;
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    Console.Clear();
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Для возврата в главное меню нажмите Backspace, для выхода нажмите Esq");
                }
                key = Console.ReadKey();
            }

        }

        static void RemoveNotebook()
        {
            var notebooks = NotebookStorage.GetFromFile();
            if (notebooks.Count() == 0)
            {
                Console.WriteLine("нет записей для удаления");
                Console.WriteLine("Для возврата в главное меню нажмите Backspace, для выхода нажмите Esq");
                MenuKey();
            }

            else
            {
                Print(notebooks);

                Console.WriteLine("Выберите номер записи для удаления");
                var inputUserAnswer = Console.ReadLine();

                var isParsedUserAnswer = TryGetNumberNotebook(inputUserAnswer, out int numberToRemove, out string errorMessage);
                while (!isParsedUserAnswer)
                {
                    Console.WriteLine(errorMessage);
                    isParsedUserAnswer = TryGetNumberNotebook(Console.ReadLine(), out numberToRemove, out errorMessage);
                }

                Console.WriteLine("Вы действительно хотите удалить выбранную запись? введите да или нет");
                isParsedUserAnswer = Validator.TryGetUserAnswer(Console.ReadLine(), out bool isTrueUserAnswer, out errorMessage);
                while (!isParsedUserAnswer)
                {
                    Console.WriteLine(errorMessage);
                    isParsedUserAnswer = Validator.TryGetUserAnswer(Console.ReadLine(), out isTrueUserAnswer, out errorMessage);
                }

                if (isTrueUserAnswer)
                {
                    NotebookStorage.RemoveAtPosition(numberToRemove - 1);
                    MainMenu(menuLabels);
                }
            }
        }

        static void PrintShortInfo()
        {
            var notebooks = NotebookStorage.GetFromFile();
            string formatOutput = "||{0,-5} || {1,-10} || {2,-10} || {3,-15} ||";
            Console.WriteLine(formatOutput, "Поз.", "Фамилия:", "Имя:", "Номер телефона:");
            for (int i = 0; i < notebooks.Count; i++)
            {
                Console.WriteLine($"{formatOutput}", new string[4] { (i + 1).ToString(), notebooks[i].LastName, notebooks[i].FirstName, notebooks[i].PhoneNumber });
            }

            Console.WriteLine("Для возврата в главное меню нажмите Backspace, для выхода нажмите Esq");
            MenuKey();
        }

        static void PrintFullInfo()
        {
            var notebooks = NotebookStorage.GetFromFile();
            Console.WriteLine(formatOutput, outputArray);
            for (int i = 0; i < notebooks.Count; i++)
            {
                var item = notebooks[i];
                Console.WriteLine($"{formatOutput}", new string[10]
                {
                    (i+1).ToString(),
                    notebooks[i].FirstName.ToString(),
                    notebooks[i].MiddleName.ToString(),
                    notebooks[i].LastName.ToString(),
                    notebooks[i].PhoneNumber.ToString(),
                    notebooks[i].Country.ToString(),
                    notebooks[i].BirthDay.ToString("dd:MM:yyyy"),
                    notebooks[i].Organization.ToString(),
                    notebooks[i].Position.ToString(),
                    notebooks[i].Other.ToString(),
                });
            }
            Console.WriteLine("Для возврата в главное меню нажмите Backspace, для выхода нажмите Esq");
            MenuKey();
        }

        static void Print(List<NoteBook> notebooks)
        {
            Console.WriteLine(formatOutput, outputArray);
            for (int i = 0; i < notebooks.Count; i++)
            {
                var item = notebooks[i];
                Console.WriteLine($"{formatOutput}", new string[10]
                {
                    (i+1).ToString(),
                    notebooks[i].FirstName.ToString(),
                    notebooks[i].MiddleName.ToString(),
                    notebooks[i].LastName.ToString(),
                    notebooks[i].PhoneNumber.ToString(),
                    notebooks[i].Country.ToString(),
                    notebooks[i].BirthDay.ToString("dd:MM:yyyy"),
                    notebooks[i].Organization.ToString(),
                    notebooks[i].Position.ToString(),
                    notebooks[i].Other.ToString(),
                });
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
