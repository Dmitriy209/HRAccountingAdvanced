using System;
using System.Collections.Generic;
using System.Linq;

namespace HRAccountingAdvanced
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CommandCreateFile = "1";
            const string CommandShowDossiers = "2";
            const string CommandDeleteFile = "3";
            const string CommandSearchLastName = "4";
            const string CommandExit = "exit";

            Dictionary<string, List<string>> dossiers = CreateStartDossiers();

            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine($"Введите {CommandCreateFile}, чтобы создать запись.\n" +
                    $"Введите {CommandShowDossiers}, чтобы просмотреть досье.\n" +
                    $"Введите {CommandDeleteFile}, чтобы удалить досье.\n" +
                    $"Введите {CommandSearchLastName}, чтобы найти досье.\n" +
                    $"Введите {CommandExit}, чтобы выйти.");
                string userInput = Console.ReadLine();

                Console.Clear();

                switch (userInput)
                {
                    case CommandCreateFile:
                        CreateFile(dossiers);
                        break;

                    case CommandShowDossiers:
                        ShowDossiers(dossiers);
                        break;

                    case CommandDeleteFile:
                        DeleteFile(dossiers);
                        break;

                    case CommandSearchLastName:
                        SearchLastName(dossiers);
                        break;

                    case CommandExit:
                        isRunning = false;
                        break;

                    default:
                        ReadMessageError();
                        break;
                }
            }

            Console.Clear();

            Console.WriteLine("Вы вышли из программы. Спасибо, что воспользовались нашей программой учёта досье.");
        }

        private static void CreateFile(Dictionary<string, List<string>> dossiers)
        {
            bool isRunning = true;

            while (isRunning)
            {
                string buttonAccept = "1";
                string buttonCansel = "2";

                string name = ReadFullName();
                string jobTitle = ReadJobName();

                List<string> tempName = new List<string>();

                Console.WriteLine($"Нового сотрудника зовут:\n" +
                    $"{name}.\n" +
                    $"Он будет работать в должности:\n" +
                    $"{jobTitle}.\n" +
                    $"Если данные верны, нажмите {buttonAccept}.\n" +
                    $"Если вы передумали брать его на работу, нажмите {buttonCansel}");
                string userInput = Console.ReadLine();

                if (userInput == buttonAccept)
                {
                    if (dossiers.ContainsKey(jobTitle))
                    {
                        tempName = dossiers[jobTitle];
                        tempName.Add(name);
                        dossiers[jobTitle] = tempName;
                    }
                    else
                    {
                        tempName.Add(name);
                        dossiers.Add(jobTitle, tempName);
                    }

                    isRunning = false;
                }
                else if (userInput == buttonCansel)
                {
                    isRunning = false;
                }
                else
                {
                    Console.WriteLine($"Введите всё заново.");
                }
            }
        }

        private static string ReadFullName()
        {
            string lastNameInput = null;
            string firstNameInput = null;
            string surnameInput = null;
            string separator = " ";

            Console.WriteLine("Введите фамилию:");
            lastNameInput = Console.ReadLine();
            Console.Clear();

            Console.WriteLine("Введите имя:");
            firstNameInput = Console.ReadLine();
            Console.Clear();

            Console.WriteLine("Введите отчество:");
            surnameInput = Console.ReadLine();
            Console.Clear();

            string name = $"{lastNameInput}{separator}{firstNameInput}{separator}{surnameInput}";

            return name;
        }

        private static string ReadJobName()
        {
            Console.WriteLine("Введите должность:");
            string jobTitle = Console.ReadLine();

            return jobTitle;
        }

        private static void ShowDossiers(Dictionary<string, List<string>> dossiers)
        {
            string separator = "-";

            int serialNumber = 0;

            foreach (var dossier in dossiers)
            {
                Console.Write($"{dossier.Key}");

                foreach (var fullName in dossier.Value)
                {
                    serialNumber++;
                    Console.Write($"{separator}{serialNumber}{separator}{fullName}");
                }

                Console.WriteLine();
            }
        }

        private static void ReadMessageError()
        {
            Console.WriteLine("Такого варианта нет, пожалуйста попробуйте ввести команду снова.");
        }

        private static void DeleteFile(Dictionary<string, List<string>> dossiers)
        {
            ShowDossiers(dossiers);

            List<string> value;

            string jobTitle = ReadJobTitle(dossiers, out value);

            string fullName = ReadLastName(value);

            value.Remove(fullName);

            if (value.Count == 0)
                dossiers.Remove(jobTitle);
        }

        private static string ReadLastName(List<string> value)
        {
            string fullNameDelete = "";

            bool isRunning = true;
            bool isFound = false;

            while (isRunning)
            {
                foreach (string fullName in value)
                    Console.WriteLine($"{fullName}");

                Console.WriteLine("Чьё досье хотите удалить? Введите ФИО:");
                string userInput = Console.ReadLine();

                foreach (string fullName in value)
                {
                    if (userInput == fullName)
                    {
                        fullNameDelete = fullName;
                        isFound = true;
                        break;
                    }
                }

                if (isFound)
                    isRunning = false;
                else
                    Console.WriteLine("Такой фамилии нет.");
            }

            return fullNameDelete;
        }

        private static void SearchLastName(Dictionary<string, List<string>> dossiers)
        {
            Console.WriteLine("Введите фамилию:");
            string userInput = Console.ReadLine();

            bool isFound = false;

            char separator = ' ';

            foreach (KeyValuePair<string, List<string>> item in dossiers)
            {
                string key = item.Key;

                List<string> fullNames = item.Value;

                foreach (var fullName in fullNames)
                {
                    string[] lastName = fullName.Split(separator);

                    if (userInput == lastName[0])
                    {
                        Console.WriteLine($"По вашему запросу найдено:\n" +
                            $"{item.Key} - {fullName}\n");

                        isFound = true;
                        break;
                    }
                }
            }

            if (isFound == false)
                Console.WriteLine("Во вашему запросу ничего не найдено.");
        }

        private static Dictionary<string, List<string>> CreateStartDossiers()
        {
            Dictionary<string, List<string>> dossier = new Dictionary<string, List<string>>();

            List<string> fullNamesCSharp = new List<string>();
            List<string> fullNamesUnity = new List<string>();

            string jobCSharp = "C# ментор";
            string jobUnity = "Unity ментор";

            fullNamesCSharp.Add("Азаренко Артемий Романович");
            fullNamesCSharp.Add("Михновец Александр Романович");
            fullNamesCSharp.Add("Куватов Арсений Романович");
            fullNamesCSharp.Add("Коновалов Алексей Романович");

            fullNamesUnity.Add("Прокопьев Дмитрий Романович");

            dossier.Add(jobCSharp, fullNamesCSharp);
            dossier.Add(jobUnity, fullNamesUnity);

            return dossier;
        }

        private static string ReadJobTitle(Dictionary<string, List<string>> dossiers, out List<string> value)
        {
            bool isRunning = true;

            value = new List<string>();

            string jobTitle = "";

            while (isRunning)
            {
                Console.WriteLine("Введите должность:");
                string userInput = Console.ReadLine();

                if (dossiers.TryGetValue(userInput, out value))
                {
                    jobTitle = userInput;

                    isRunning = false;
                }
                else
                {
                    Console.WriteLine("Такой должности нет.");
                }
            }

            return jobTitle;
        }
    }
}