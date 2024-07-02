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

            List<string> fullNames = new List<string>();
            List<string> jobsTitles = new List<string>();

            fullNames.Add("Азаренко Артемий Романович");
            fullNames.Add("Михновец Александр Романович");
            fullNames.Add("Куватов Арсений Романович");
            fullNames.Add("Коновалов Алексей Романович");

            jobsTitles.Add("С# ментор");

            Dictionary<string, string> dossier = new Dictionary<string, string>();

            for (int i = 0; i < fullNames.Count; i++)
            {
                dossier.Add(fullNames[i], jobsTitles[0]);
            }

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
                        CreateFile(fullNames, jobsTitles, dossier);
                        break;

                    case CommandShowDossiers:
                        ShowDossiers(dossier);
                        break;

                    case CommandDeleteFile:
                        DeleteFile(fullNames, jobsTitles, dossier);
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

        private static void CreateFile(List<string> fullNames, List<string> jobsTitles, Dictionary<string, string> dossier)
        {
            bool isRunning = true;

            while (isRunning)
            {
                string buttonAccept = "1";
                string buttonCansel = "2";

                string name = ReadFullName();
                string jobTitle = ReadJobName();

                Console.WriteLine($"Нового сотрудника зовут:\n" +
                    $"{name}.\n" +
                    $"Он будет работать в должности:\n" +
                    $"{jobTitle}.\n" +
                    $"Если данные верны, нажмите {buttonAccept}.\n" +
                    $"Если вы передумали брать его на работу, нажмите {buttonCansel}");
                string userInput = Console.ReadLine();

                if (userInput == buttonAccept)
                {
                    fullNames.Add(name);
                    jobsTitles.Add(jobTitle);
                    dossier.Add(name, jobTitle);

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

        private static void ShowDossiers(Dictionary<string, string> dossier)
        {
            string separator = "-";

            int serialNumber = 0;

            foreach (var item in dossier)
            {
                serialNumber = serialNumber + 1;
                Console.WriteLine($"{serialNumber}{separator}{item.Key}{separator}{item.Value}");
            }
        }

        private static void ReadMessageError()
        {
            Console.WriteLine("Такого варианта нет, пожалуйста попробуйте ввести команду снова.");
        }

        private static void DeleteFile(List<string> fullNames, List<string> jobsTitles, Dictionary<string, string> dossier)
        {
            ShowDossiers(dossier);

            int index = ReadIndex(fullNames, jobsTitles);

            string fullName = fullNames[index];
            string jobTitle = "";

            fullNames.RemoveAt(index);

            if (dossier.TryGetValue(fullName, out jobTitle))
                jobsTitles.Remove(jobTitle);

            dossier.Remove(fullName);
        }

        private static int ReadIndex(List<string> fullNames, List<string> jobsTitles)
        {
            bool isRunning = true;

            int index = 0;

            while (isRunning)
            {
                Console.WriteLine("Чьё досье хотите удалить? Введите порядковый номер:");
                string userInput = Console.ReadLine();

                bool isSuccess = int.TryParse(userInput, out index);

                index -= 1;

                if (isSuccess && index >= 0 && index < fullNames.Count)
                    isRunning = false;
                else
                    Console.WriteLine("Такого порядкового номера нет.");
            }

            return index;
        }

        private static void SearchLastName(Dictionary<string, string> dossier)
        {
            Console.WriteLine("Введите фамилию:");
            string userInput = Console.ReadLine();

            bool isFound = false;

            char separator = ' ';

            foreach (KeyValuePair<string, string> item in dossier)
            {
                string key = item.Key;

                string[] lastName = key.Split(separator);

                if (userInput == lastName[0])
                {
                    Console.WriteLine($"По вашему запросу найдено:\n" +
                        $"{item.Key} - {item.Value}\n");

                    isFound = true;
                    break;
                }
            }

            if (isFound == false)
                Console.WriteLine("Во вашему запросу ничего не найдено.");
        }
    }
}
