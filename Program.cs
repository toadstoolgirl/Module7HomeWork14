using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Module7HomeWork14
{

    class Employee
    {
        public int employeeId;
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
                            SearchById();
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

        private static List<Employee> ReadFile() /*Метод считывания данных из файла*/
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("The file don't exist and could't be read.");
                return null;
            }
            else
            {
                using (StreamReader pr = File.OpenText(path))
                {
                    string[] readText = File.ReadAllLines(path);
                    var employeeCollection = new List<Employee>();
                    foreach (var rdTxt in readText)
                    {
                        string[] employeeArray = rdTxt.Split('#');
                        var employee = new Employee();
                        employee.employeeId = int.Parse(employeeArray[0]);
                        employee.addingTime = DateTime.Parse(employeeArray[1]);
                        employee.fullName = employeeArray[2];
                        employee.age = int.Parse(employeeArray[3]);
                        employee.height = int.Parse(employeeArray[4]);
                        employee.birthDate = DateTime.Parse(employeeArray[5]);
                        employee.birthPlace = employeeArray[6];
                        employeeCollection.Add(employee);
                    }
                    return employeeCollection;
                }
            }
        }
        static void Add() /*Создание записи о сотруднике*/
        {
            var employeeCollection = ReadFile();
            var employee = new Employee();
            int count = employeeCollection.Count;
            employee.employeeId = ++count;
            Console.WriteLine($"Employee's ID: {employee.employeeId}");
            Console.WriteLine($"Adding time: {employee.addingTime}");
            Console.WriteLine($"Enter employee's full name");
            employee.fullName = Console.ReadLine();
            Console.WriteLine($"Enter employee's birth date");
            employee.birthDate = DateTime.Parse(Console.ReadLine());
            employee.age = (employee.addingTime - employee.birthDate).Days / 365;
            Console.WriteLine($"Enter employee's height");
            employee.height = int.Parse(Console.ReadLine());
            Console.WriteLine($"Enter employee's birth place");
            employee.birthPlace = Console.ReadLine();
            employeeCollection.Add(employee);

            var employeeString =
                $"{employee.employeeId}" +
                $"#{employee.addingTime:dd.MM.yyyy HH:mm}" +
                $"#{employee.fullName}" +
                $"#{employee.age}" +
                $"#{employee.height}" +
                $"#{employee.birthDate:dd.MM.yyyy}" +
                $"#{employee.birthPlace}";

            using (StreamWriter writer = (File.Exists(path)) ? File.AppendText(path) : File.CreateText(path))

            {
                writer.WriteLine(employeeString);
            }
        }

        static void SearchById() /*Вывод записи о сотруднике по Id*/
        {
            var employeeCollection = ReadFile();
            Console.WriteLine($"Enter employee's id to print his data");

            if (int.TryParse(Console.ReadLine(), out int empId))
            {
                foreach (var employee in employeeCollection)
                {
                    if (employee.employeeId == empId)
                    {
                        Print(new List<Employee> { employee });
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("no number entered");
            }
        }

        private static void Print(List<Employee> employees) /*Вывод записи*/
        {
            foreach (var employee in employees)
            {

                Console.WriteLine($"employee's ID: {employee.employeeId}");
                Console.WriteLine($"adding time: {employee.addingTime}");
                Console.WriteLine($"employee's full name: {employee.fullName}");
                Console.WriteLine($"employee's age: {employee.age}");
                Console.WriteLine($"employee's height: {employee.height}");
                Console.WriteLine($"employee's birth date: {employee.birthDate}");
                Console.WriteLine($"employee's birth place: {employee.birthPlace}");
                Console.WriteLine("-----");

            }
        }

        static void ReAdd() /*Редактирование записи о сотруднике по Id*/
        {
            Console.WriteLine($"Enter employee's id to correct his data");

            if (int.TryParse(Console.ReadLine(), out int empId))
            {
                var employeeCollection = ReadFile();

                if (empId < 1 || employeeCollection.Count < empId)
                {
                    Console.WriteLine($"Entered employee's ID not exist");
                }
                else
                {
                    foreach (var employee in employeeCollection)
                    {
                        if (employee.employeeId != empId)
                        {
                            var employeeString =
                                            $"{employee.employeeId}" +
                                            $"#{employee.addingTime:dd.MM.yyyy HH:mm}" +
                                            $"#{employee.fullName}" +
                                            $"#{employee.age}" +
                                            $"#{employee.height}" +
                                            $"#{employee.birthDate:dd.MM.yyyy}" +
                                            $"#{employee.birthPlace}";
                            using (StreamWriter writer = (File.Exists(path)) ? File.AppendText(path) : File.CreateText(path))

                            {
                                writer.WriteLine(employeeString);
                            }
                        }

                        else
                        {
                            Console.WriteLine($"Employee's ID: {employee.employeeId}");
                            Console.WriteLine($"Adding time: {employee.addingTime}");
                            Console.WriteLine($"Enter employee's new full name");
                            employee.fullName = Console.ReadLine();
                            Console.WriteLine($"Enter employee's new birth date");
                            employee.birthDate = DateTime.Parse(Console.ReadLine());
                            employee.age = (employee.addingTime - employee.birthDate).Days / 365;
                            Console.WriteLine($"Enter employee's new height");
                            employee.height = int.Parse(Console.ReadLine());
                            Console.WriteLine($"Enter employee's new birth place");
                            employee.birthPlace = Console.ReadLine();

                            var employeeString =
                                            $"{employee.employeeId}" +
                                            $"#{employee.addingTime:dd.MM.yyyy HH:mm}" +
                                            $"#{employee.fullName}" +
                                            $"#{employee.age}" +
                                            $"#{employee.height}" +
                                            $"#{employee.birthDate:dd.MM.yyyy}" +
                                            $"#{employee.birthPlace}";

                            using (StreamWriter writer = (File.Exists(path)) ? File.AppendText(path) : File.CreateText(path))

                            {
                                writer.WriteLine(employeeString);
                            }
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("no number entered");
            }
        }

        static void Delete() /*Удаление записи о сотруднике по Id*/
        {
            var employeeCollection = ReadFile();

            Console.WriteLine($"Enter employee's id to delete");

            if (int.TryParse(Console.ReadLine(), out int empId))
            {
                if (empId < 1 | employeeCollection.Count < empId)
                {
                    Console.WriteLine($"Entered employee's ID not exist");
                    Delete();
                }
                else
                {
                    foreach (var employee in employeeCollection)
                    {
                        if (empId != employee.employeeId)
                        {
                            Console.WriteLine($"Employee's ID: {employee.employeeId}");
                            Console.WriteLine($"Adding time: {employee.addingTime}");
                            Console.WriteLine($"Employee's full name:{employee.fullName}");
                            Console.WriteLine($"Employee's age: {employee.age}");
                            Console.WriteLine($"Employee's height: {employee.height}");
                            Console.WriteLine($"employee's birth date: {employee.birthDate}");
                            Console.WriteLine($"employee's birth place: {employee.birthPlace}");
                            Console.WriteLine("-----");

                            var employeeString =
                                $"{employee.employeeId}" +
                                $"#{employee.addingTime:dd.MM.yyyy HH:mm}" +
                                $"#{employee.fullName}" +
                                $"#{employee.age}" +
                                $"#{employee.height}" +
                                $"#{employee.birthDate:dd.MM.yyyy}" +
                                $"#{employee.birthPlace}";

                            using (StreamWriter writer = (File.Exists(path)) ? File.AppendText(path) : File.CreateText(path))

                            {
                                writer.WriteLine(employeeString);
                            }
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine($"This is not employee's ID");
            }
        }

        static void SortByDateTime() /*Выборка записей в диапазоне дат*/
        {
            Console.WriteLine($"Enter 1st date");
            var correctFirstDate = DateTime.TryParse(Console.ReadLine(), out DateTime a);
            Console.WriteLine($"Enter 2nd date");
            var correctSecondDate = DateTime.TryParse(Console.ReadLine(), out DateTime b);

            if (!correctFirstDate)
            { Console.WriteLine($"Incorrect 1st date"); }

            else if (!correctSecondDate)
            { Console.WriteLine($"Incorrect 2nd date"); }

            else
            {
                var employeeCollection = ReadFile();
                var sortedEmpl = new List<Employee>();
                foreach (var employee in employeeCollection)
                {
                    if (a <= employee.addingTime && employee.addingTime <= b)
                    {
                        sortedEmpl.Add(employee); 
                    }
                }
                Print(sortedEmpl);
            }
        }

        static void AscendingDateTime() /*Сортировка карточек сотрудников по возрастанию даты внесения карточки*/
        {
            var employeeCollection = ReadFile();
            var newCollection = employeeCollection.OrderBy(e => e.addingTime).ToList();
            Print(newCollection);
        }
        static void DescendingDateTime() /*Сортировка карточек сотрудников по убыванию даты внесения карточки*/
        {
            var employeeCollection = ReadFile();
            var newCollection = employeeCollection.OrderByDescending(e => e.addingTime).ToList();
            Print(newCollection);
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
