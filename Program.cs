using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Module7HomeWork14
{

    class Employee
    {
        static int employeeId;
        public Employee() => employeeId++;
        public DateTime addingTime = DateTime.Now;
        public string fullName;
        public int age;
        public int height;
        public DateTime birthDate;
        public string birthPlace;
    }

    class Program
    {
        public const string OptionCommand1 = "1"; /*Команда вывода информации в консоль*/
        public const string OptionCommand2 = "2"; /*Команда записи информации в файл*/
        public const string OptionCommand3 = "3"; /*Команда изменения записи в файле*/
        public const string OptionCommand4 = "4"; /*Команда удаления записи в файле*/
        public const string OptionCommand5 = "5"; /*Команда вывода записей в диапазоне дат*/
        public const string OptionCommand6 = "6"; /*Команда сортировки записей в файле по возрастанию дат*/
        public const string OptionCommand7 = "7"; /*Команда сортировки записей в файле по убыванию дат*/
        public const string HelpCommand = "help"; /*Команда помощи выбора команды*/

        public const string path = @"Employees.txt";
        public const string path1 = @"Employees1.txt";
        static void Main()
        {
            OptionCommand();
        }
        static void OptionCommand()
        {
            while (true)
            {
                Console.WriteLine("Welcome!\n" +
                                  "Enter comand,comand manager - help");
                var comand = Console.ReadLine();
                if (comand != null)
                    switch (comand)
                    {
                        case OptionCommand1:
                            Print();
                            break;

                        case OptionCommand2:
                            Add();
                            break;

                        case OptionCommand3:
                            ReAdd();
                            break;

                        case OptionCommand4:
                            Delete();
                            break;

                        case OptionCommand5:
                            SortByDateTime();
                            break;

                        case OptionCommand6:
                            AscendingDateTime();
                            break;

                        case OptionCommand7:
                            DescendingDateTime();
                            break;

                        case HelpCommand:
                            Help();
                            break;

                        default:
                            Console.WriteLine("incorrect command");
                            break;
                    }
            }
        }

        static void Add() /*Создание записи о сотруднике*/
        {
            int count = 0;
            if (File.Exists(path))
            { count = System.IO.File.ReadAllLines(path).Length; }
            int employeeId = ++count;
            Console.WriteLine($"Employee's ID: {employeeId}");
            var addingTime = DateTime.Now;
            Console.WriteLine($"Adding time: {addingTime}");
            Console.WriteLine($"Enter employee's full name");
            var fullName = Console.ReadLine();
            Console.WriteLine($"Enter employee's birth date");
            var birthDate = Convert.ToDateTime(Console.ReadLine());
            var age = (addingTime - birthDate).Days / 365;
            Console.WriteLine($"Enter employee's height");
            var height = Console.ReadLine();

            Console.WriteLine($"Enter employee's birth place");
            var birthPlace = Console.ReadLine();

            var employeeString = $"{employeeId}#{addingTime:dd.MM.yyyy HH:mm}#{fullName}#{age}#{height}#{birthDate:dd.MM.yyyy}#{birthPlace}";

            using (StreamWriter writer = (File.Exists(path)) ? File.AppendText(path) : File.CreateText(path))

            {
                writer.WriteLine(employeeString);
            }
        }

        static void Print() /*Вывод записи о сотруднике по Id*/
        {

            if (!File.Exists(path))
            {
                Console.WriteLine("The file don't exist and could't be read.");
            }
            else
            {
                using (StreamReader pr = File.OpenText(path))
                {
                    Console.WriteLine($"Enter employee's id");

                    if (int.TryParse(Console.ReadLine(), out int empId))
                    {
                        string[] readText = File.ReadAllLines(path);

                        if (readText.Length <= (--empId))
                        {
                            Console.WriteLine($"Entered employee's ID not exist");
                            Print();
                        }
                        else
                        {
                            string[] employeeArray = readText[empId].Split('#');
                            if (employeeArray.Length != 7)
                            {
                                Console.WriteLine($"Data of employee n.{empId} is corrupted");
                            }
                            else
                            {
                                Console.WriteLine($"employee's ID: {employeeArray[0]}");
                                Console.WriteLine($"adding time: {employeeArray[1]}");
                                Console.WriteLine($"employee's full name: {employeeArray[2]}");
                                Console.WriteLine($"employee's age: {employeeArray[3]}");
                                Console.WriteLine($"employee's height: {employeeArray[4]}");
                                Console.WriteLine($"employee's birth date: {employeeArray[5]}");
                                Console.WriteLine($"employee's birth place: {employeeArray[6]}");
                                Console.WriteLine("-----");
                            }
                        }
                    }

                    else
                    {
                        Console.WriteLine("no number entered");
                        Print();
                    }
                }
            }
        }

        static void ReAdd() /*Редактирование записи о сотруднике по Id*/
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("The file don't exist and could't be read.");
            }
            else
            {
                using (StreamReader pr = File.OpenText(path))
                {
                    Console.WriteLine($"Enter employee's id");

                    if (int.TryParse(Console.ReadLine(), out int empId))
                    {
                        string[] readText = File.ReadAllLines(path);
                        
                        if (readText.Length < (--empId))
                        {
                            Console.WriteLine($"Entered employee's ID not exist");
                            Print();
                        }
                        else
                        {
                                string[] employeeArray = readText[empId].Split('#');
                                var employeeId = employeeArray[0];
                                var addingTime = employeeArray[1];
                                Console.WriteLine($"Adding time: {addingTime}");
                                Console.WriteLine($"Enter employee's new full name");
                                var fullName = Console.ReadLine();
                                Console.WriteLine($"Enter employee's new birth date");
                                var birthDate = Convert.ToDateTime(Console.ReadLine());
                                var age = (DateTime.Now - birthDate).Days / 365;
                                Console.WriteLine($"Enter employee's new height");
                                var height = Console.ReadLine();
                                Console.WriteLine($"Enter employee's new birth place");
                                var birthPlace = Console.ReadLine();

                                string employeeString = $"{employeeId}#{addingTime:dd.MM.yyyy HH:mm}#{fullName}#{age}#{height}#{birthDate:dd.MM.yyyy}#{birthPlace}";

                                using (StreamWriter writer = (File.Exists(path1)) ? File.AppendText(path1) : File.CreateText(path1))

                                {
                                    writer.WriteLine(employeeString);
                                }
                            foreach (string s in readText)
                            {
                                while (s != readText[empId])
                                {
                                    using (StreamWriter writer = (File.Exists(path1)) ? File.AppendText(path1) : File.CreateText(path1))

                                    {
                                        writer.WriteLine(s);
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        static void Delete() /*Удаление записи о сотруднике по Id*/
        {

            if (!File.Exists(path))
            {
                Console.WriteLine("The file don't exist and could't be read.");
            }
            else
            {
                using (StreamReader pr = File.OpenText(path))
                {
                    Console.WriteLine($"Enter employee's id");

                    if (int.TryParse(Console.ReadLine(), out int empId))
                    {
                        string[] readText = File.ReadAllLines(path);

                        if (readText.Length < (--empId))
                        {
                            Console.WriteLine($"Entered employee's ID not exist");
                            Print();
                        }
                        else
                        {
                            foreach (string s in readText)
                            {
                                while (s != readText[empId])
                                {
                                    using (StreamWriter writer = (File.Exists(path1)) ? File.AppendText(path1) : File.CreateText(path1))

                                    {
                                        writer.WriteLine(s);
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        static void SortByDateTime() /*Выборка записей в диапазоне дат*/
        {
            //var a = Convert.ToDateTime(Console.ReadLine());

            //Console.WriteLine($"Enter 1st date");
            //DateTime.TryParse(Console.ReadLine(), out DateTime a);
            //Console.WriteLine($"Enter 2nd date");
            //DateTime.TryParse(Console.ReadLine(), out DateTime b);
            //string[] readText = File.ReadAllLines(path);
            ////foreach (string s in readText)  
            //for (int i = 0; i < readText.Length; i++)
            //{
            //    //Employee addingTime = readText[i]
            //    if ()
            //    { 
                
            //    }
            //}

        }

        static void AscendingDateTime() /*Сортировка карточек сотрудников по возрастанию даты внесения карточки*/
        {

        }
        static void DescendingDateTime() /*Сортировка карточек сотрудников по убыванию даты внесения карточки*/
        {

        }

        static void Help() /*Команда помощи выбора команды*/
        {
            Console.WriteLine($"enter 1 to display employee's data\n" +
                              $"enter 2 to add new data to the file\n" +
                              $"enter 3 to correct employee's data\n" +
                              $"enter 4 to delete employee's data\n" +
                              $"enter 5 to select data within the range of dates you will be entered\n" +
                              $"enter 6 to sort employee's data by ASC\n" +
                              $"enter 7 to sort employee's data by DESC\n");
        }
    }
}
