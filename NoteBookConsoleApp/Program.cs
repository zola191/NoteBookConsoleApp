using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace NoteBookConsoleApp
{
    public class Program
    {
        static string fullFormatOutput = "||{0,-15}||{1,-15}||{2,-20}||{3,-20}||{4,-20}||{5,-20}||{6,-15}||{7,-20}||{8,-25}||{9,-35}||";
        static string fullFormatOutput2 = "||{0,-15}||{1,-20}||{2,-20}||{3,-20}||{4,-20}||{5,-15}||{6,-20}||{7,-25}||{8,-35}||";
        static string shortFormatOutput = "||{0,-5} || {1,-10} || {2,-10} || {3,-15} ||";
        static string[] fulloutputArray = new string[10]
        {
            "№","Имя", "Отчество", "Фамилия", "Номер телефона", "Страна", "Дата рождения", "Организация", "Должность", "Прочее"
        };
        static string[] shortoutputArray = new string[4]
        {
            "Поз.", "Фамилия:", "Имя:", "Номер телефона:"
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
        static Dictionary<string, Func<PropertyInfo>> ValidationDict = new Dictionary<string, Func<PropertyInfo>>()
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
        static Dictionary<string, string> PrintDict = new Dictionary<string, string>()
        {
            { "FirstName","Введите Имя" },
            { "MiddleName","Введите Отчество, поле не является обязательным, для пропуска нажмите enter" },
            { "LastName","Введите Фамилию" },
            { "PhoneNumber","Введите свой номер телефона, начните ввод с 8" },
            { "Country","Введите страну" },
            { "BirthDay","Введите дату рождения в формате ####-##-##, поле не является обязательным, для пропуска нажмите enter" },
            { "Organization","Введите организацию, поле не является обязательным, для пропуска нажмите enter" },
            { "Position","Введите должность, поле не является обязательным, для пропуска нажмите enter" },
            { "Other","Введите свои примечания, поле не является обязательным, для пропуска нажмите enter" },
        };

        private static void Main(string[] args)
        {
            //Console.OutputEncoding = System.Text.Encoding.UTF8;
            //Console.InputEncoding = System.Text.Encoding.UTF8;
            MainMenu();
        }
        //цикличность при выборе? // обнуление ататического поля index
        private static void MainMenu()
        {
            while (true)
            {
                var selectedMenuItem = MenuChose(menuLabels);
                if (selectedMenuItem == (int)Menu.CreateNewNote)
                {
                    index = 0;
                    Console.Clear();
                    Console.WriteLine("Создание новой записи");
                    CreateNewNotebook();
                }
                else if (selectedMenuItem == (int)Menu.EditNote)
                {
                    index = 0;
                    Console.Clear();
                    Console.WriteLine("Редактирование записей");
                    EditNotebookMenu();
                }
                else if (selectedMenuItem == (int)Menu.RemoveNote)
                {
                    index = 0;
                    Console.Clear();
                    Console.WriteLine("Удаление записей");
                    RemoveNotebook();
                }
                else if (selectedMenuItem == (int)Menu.ShowFullNote)
                {
                    index = 0;
                    Console.Clear();
                    Console.WriteLine("Полная запись");
                    var notebooks = NotebookStorage.GetFromFile();
                    PrintFullInfo(notebooks);
                    Console.WriteLine("Для возврата в главное меню нажмите Backspace, для выхода нажмите Esq");
                    MenuKeyPress();
                }
                else if (selectedMenuItem == (int)Menu.ShowShortNote)
                {
                    index = 0;
                    Console.Clear();
                    Console.WriteLine("Короткая запись");
                    var notebooks = NotebookStorage.GetFromFile();
                    PrintShortInfo(notebooks);
                    Console.WriteLine("Для возврата в главное меню нажмите Backspace, для выхода нажмите Esq");
                    MenuKeyPress();
                }
                else if (selectedMenuItem == (int)Menu.Exit)
                {
                    Console.Clear();
                    break;
                }
            }
        }

        private static int MenuChose(List<string> menuLabels)
        {
            for (int i = 0; i < menuLabels.Count; i++)
            {
                if (i == index)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("{0,-15}", menuLabels[i]); //Console.WriteLine("{0,-15}", menuLabels[i]);
                }
                else
                {
                    Console.WriteLine("{0,-15}", menuLabels[i]);
                }
                Console.ResetColor();
            }
            var key = Console.ReadKey();
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

        private static int MenuChose(List<NoteBook> notebooks, out bool flag)
        {
            for (int i = 0; i < notebooks.Count(); i++)
            {
                if (i == index)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(fullFormatOutput2, PrintNotebook(notebooks[i]));
                }
                else
                {
                    Console.WriteLine(fullFormatOutput2, PrintNotebook(notebooks[i]));
                }
                Console.ResetColor();
            }
            var key = Console.ReadKey();
            if (key.Key == ConsoleKey.DownArrow)
            {
                if (index == notebooks.Count() - 1)
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
                    index = notebooks.Count() - 1;
                }
                else
                {
                    index--;
                }
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                flag = false;
                return index;
            }
            else if (key.Key == ConsoleKey.Backspace)
            {
                index = 0;
                Console.Clear();
                MainMenu();
            }
            else if (key.Key == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }
            Console.Clear();

            flag = true;
            return -1;
        }

        private static int MenuChose(NoteBook noteBook, out bool flag)
        {
            var properties = noteBook.GetType().GetProperties();
            var propNames = properties.Select(q => q.Name).ToArray();
            var propValues = properties.Select(q => q.GetValue(noteBook, null)).ToArray();

            for (int i = 0; i < properties.Count(); i++)
            {
                if (i == index)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine($"№{i + 1} - {propNames[i]} - {propValues[i]}");
                }
                else
                {
                    Console.WriteLine($"№{i + 1} - {propNames[i]} - {propValues[i]}");
                }
                Console.ResetColor();
            }
            var key = Console.ReadKey();
            if (key.Key == ConsoleKey.DownArrow)
            {
                if (index == properties.Count() - 1)
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
                    index = properties.Count() - 1;
                }
                else
                {
                    index--;
                }
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                flag = false;
                return index;
            }
            else if (key.Key == ConsoleKey.Backspace)
            {
                index = 0;
                Console.Clear();
                //MenuChose(menuLabels);
                EditNotebookMenu();
                //Environment.Exit(0);
            }
            else if (key.Key == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }
            Console.Clear();
            flag = true;
            return -1;
        }

        private static void CreateNewNotebook()
        {
            var noteBook = new NoteBook()
            {
                FirstName = (string)GetInput("Введите Имя", "FirstName"),
                MiddleName = (string)GetInput("Введите Отчество, поле не является обязательным, для пропуска нажмите enter", "MiddleName"),
                LastName = (string)GetInput("Введите Фамилию", "LastName"),
                PhoneNumber = (string)GetInput("Введите свой номер телефона, начните ввод с 8", "PhoneNumber"),
                Country = (string)GetInput("Введите страну", "Country"),
                BirthDay = Convert.ToDateTime(GetInput("Введите дату рождения в формате ####-##-##, поле не является обязательным, для пропуска нажмите enter", "BirthDay")),
                Organization = (string)GetInput("Введите организацию, поле не является обязательным, для пропуска нажмите enter", "Organization"),
                Position = (string)GetInput("Введите должность, поле не является обязательным, для пропуска нажмите enter", "Position"),
                Other = (string)GetInput("Введите свои примечания, поле не является обязательным, для пропуска нажмите enter", "Other")
            };

            NotebookStorage.SaveToFile(noteBook);
            Console.WriteLine("Запись успешно создана");
            for (int i = 3; i >= 0; i--)
            {
                Console.WriteLine($"Вы перейдете в главное меню через {i}");
                Thread.Sleep(1000);
            }
            Console.Clear();
            MenuChose(menuLabels);
        }

        private static void PrintErrors(List<string> errors)
        {
            Console.WriteLine("Список допущенных ошибок при вводе");
            for (int i = 0; i < errors.Count; i++)
            {
                Console.WriteLine($"Ошибка №{i + 1} - {errors[i]}");
            }
        }

        private static object GetInput(string title, string fieldName)
        {
            Console.WriteLine(title);
            var attributeValidator = new AttributeValidator();
            var inputFirstName = Console.ReadLine();
            var valid = attributeValidator.CheckValidation(ValidationDict[fieldName], inputFirstName, out List<string> errors);
            while (!valid)
            {
                PrintErrors(errors);
                Console.WriteLine(title);
                inputFirstName = Console.ReadLine();
                valid = attributeValidator.CheckValidation(ValidationDict[fieldName], inputFirstName, out errors);
            }
            return inputFirstName;
        }

        private static void ChangePropValue(NoteBook noteBook, string propName, string printInfo)
        {
            var newValue = (string)GetInput(printInfo, propName);
            typeof(NoteBook).GetProperty(propName).SetValue(noteBook, newValue, null);
        }

        private static void EditNotebookMenu()
        {
            var notebooks = NotebookStorage.GetFromFile();
            var indexToEditNotebook = MenuChose(notebooks, out bool flag);
            while (flag)
            {
                Console.WriteLine("Редактирование записей");
                indexToEditNotebook = MenuChose(notebooks, out flag);
            }
            Console.Clear();
            var notebook = notebooks[indexToEditNotebook];
            //Вопрос как избавится от индексов?
            while (true)
            {
                //esq выход в прошлое меню
                var indexToEditProperty = MenuChose(notebook, out flag);
                while (flag)
                {
                    indexToEditProperty = MenuChose(notebook, out flag);
                }
                var properties = notebook.GetType().GetProperties();
                var propName = properties[indexToEditProperty].Name;
                ChangePropValue(notebook, propName, PrintDict[propName]);
                var x = notebook;
                NotebookStorage.SaveByIndex(notebook, indexToEditNotebook);
                Console.Clear();
            }
        }

        private static void MenuKeyPress()
        {
            var key = Console.ReadKey();
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

        private static void RemoveNotebook()
        {
            var notebooks = NotebookStorage.GetFromFile();
            if (notebooks.Count() == 0)
            {
                Console.WriteLine("нет записей для удаления");
                Console.WriteLine("Для возврата в главное меню нажмите Backspace, для выхода нажмите Esq");
                MenuKeyPress();
            }

            else
            {
                var indexToRemoveNotebook = MenuChose(notebooks, out bool flag);
                while (flag)
                {
                    Console.WriteLine("Удаление записей");
                    indexToRemoveNotebook = MenuChose(notebooks, out flag);
                }

                Console.WriteLine("Вы действительно хотите удалить выбранную запись? введите да или нет");
                var isParsedUserAnswer = TryGetUserAnswer(Console.ReadLine(), out bool isTrueUserAnswer, out string errorMessage);
                while (!isParsedUserAnswer)
                {
                    Console.WriteLine(errorMessage);
                    isParsedUserAnswer = TryGetUserAnswer(Console.ReadLine(), out isTrueUserAnswer, out errorMessage);
                }

                if (isTrueUserAnswer)
                {
                    NotebookStorage.RemoveAtPosition(indexToRemoveNotebook);
                }
                Console.Clear();
            }
        }

        static void PrintShortInfo(List<NoteBook> notebooks)
        {
            Console.WriteLine(shortFormatOutput, shortoutputArray);
            for (int i = 0; i < notebooks.Count; i++)
            {
                Console.WriteLine($"{shortFormatOutput}", new string[4]
                {
                    (i + 1).ToString(),
                    notebooks[i].LastName,
                    notebooks[i].FirstName,
                    notebooks[i].PhoneNumber
                });
            }
        }

        static void PrintFullInfo(List<NoteBook> notebooks)
        {
            Console.WriteLine(fullFormatOutput, fulloutputArray);
            for (int i = 0; i < notebooks.Count; i++)
            {
                Console.WriteLine($"{fullFormatOutput}", new string[10]
                {
                    (i+1).ToString(),

                    notebooks[i].FirstName.ToString(),
                    notebooks[i].MiddleName?.ToString(),
                    notebooks[i].LastName.ToString(),
                    notebooks[i].PhoneNumber.ToString(),
                    notebooks[i].Country.ToString(),
                    notebooks[i].BirthDay.ToString("dd:MM:yyyy")??"",
                    notebooks[i].Organization?.ToString(),
                    notebooks[i].Position?.ToString(),
                    notebooks[i].Other?.ToString(),
                });
            }
        }

        private static string[] PrintNotebook(NoteBook notebook)
        {
            return new List<string>()
            {
                notebook.FirstName.ToString(),
                notebook.MiddleName?.ToString(),
                notebook.LastName.ToString(),
                notebook.PhoneNumber.ToString(),
                notebook.Country.ToString(),
                notebook.BirthDay.ToString("dd:MM:yyyy")??"",
                notebook.Organization?.ToString(),
                notebook.Position?.ToString(),
                notebook.Other?.ToString(),
            }.ToArray();
        }

        private static bool TryGetUserAnswer(string userAnswer, out bool isTrueUserAnswer, out string errorMessage)
        {

            if (userAnswer.ToLower() == "да")
            {
                isTrueUserAnswer = true;
                errorMessage = string.Empty;
                return true;
            }
            else if (userAnswer.ToLower() == "нет")
            {
                isTrueUserAnswer = false;
                errorMessage = string.Empty;
                return true;
            }
            errorMessage = "Необходимо ввести да или нет";
            isTrueUserAnswer = false;
            return false;
        }

    }
}
