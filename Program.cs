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

            Dictionary<string, List<string>> dossier = CreateStartDossiers();

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
                        CreateFile(dossier);
                        break;

                    case CommandShowDossiers:
                        ShowDossiers(dossier);
                        break;

                    case CommandDeleteFile:
                        DeleteFile(dossier);
                        break;

                    case CommandSearchLastName:
                        SearchLastName(dossier);
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

        private static void CreateFile(Dictionary<string, List<string>> dossier)
        {
            List<string> fullNames = new List<string>();

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
                    if (dossier.ContainsKey(jobTitle))
                    {
                        tempName = dossier[jobTitle];
                        tempName.Add(name);
                        dossier[jobTitle] = tempName;
                    }
                    else
                    {
                        dossier.Add(jobTitle, tempName);
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

        private static void ShowDossiers(Dictionary<string, List<string>> dossier)
        {
            string separator = "-";

            int serialNumber = 0;

            foreach (var itemDossier in dossier)
            {
                foreach (var itemList in itemDossier.Value)
                {
                    serialNumber ++;
                    Console.WriteLine($"{serialNumber}{separator}{itemDossier.Key}{separator}{itemList}");
                }
            }
        }

        private static void ReadMessageError()
        {
            Console.WriteLine("Такого варианта нет, пожалуйста попробуйте ввести команду снова.");
        }

        private static void DeleteFile(Dictionary<string, List<string>> dossier)
        {
            ShowDossiers(dossier);

            int index = ReadIndex(dossier);

            int serialNumber = -1;

            string keyDelete = "";

            bool isValueEmpty = false;

            foreach (var itemDossier in dossier)
            {
                foreach (var itemList in itemDossier.Value)
                {
                    serialNumber++;

                    if (serialNumber == index)
                    {
                        itemDossier.Value.Remove(itemList);

                        if (itemDossier.Value.Count == 0)
                        {
                            isValueEmpty = true;
                            keyDelete = itemDossier.Key;
                        }
                        break;
                    }
                }
            }

            if (isValueEmpty)
                dossier.Remove(keyDelete);
        }

        private static int ReadIndex(Dictionary<string, List<string>> dossier)
        {
            bool isRunning = true;

            int index = 0;

            while (isRunning)
            {
                Console.WriteLine("Чьё досье хотите удалить? Введите порядковый номер:");
                string userInput = Console.ReadLine();

                bool isSuccess = int.TryParse(userInput, out index);

                index -= 1;

                int serialNumber = 0;

                foreach (var itemDossier in dossier)
                {
                    foreach (var itemList in itemDossier.Value)
                        serialNumber++;
                }

                if (isSuccess && index >= 0 && index < serialNumber)
                    isRunning = false;
                else
                    Console.WriteLine("Такого порядкового номера нет.");
            }

            return index;
        }

        private static void SearchLastName(Dictionary<string, List<string>> dossier)
        {
            Console.WriteLine("Введите фамилию:");
            string userInput = Console.ReadLine();

            bool isFound = false;

            char separator = ' ';

            foreach (KeyValuePair<string, List<string>> item in dossier)
            {
                string key = item.Key;

                List<string> fullNames = item.Value;

                foreach (var fullname in fullNames)
                {
                    string[] lastName = fullname.Split(separator);

                    if (userInput == lastName[0])
                    {
                        Console.WriteLine($"По вашему запросу найдено:\n" +
                            $"{item.Key} - {fullname}\n");

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
    }
}