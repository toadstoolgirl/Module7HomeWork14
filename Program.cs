using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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
                Console.WriteLine("Welcome! \nenter comand,comand manager - help");
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

                        case HelpCommand:
                            Help();
                            break;

                        default:
                            Console.WriteLine("incorrect command");
                            break;
                    }
            }
        }

        static void Add() /*Добавление информации в файл*/
        {

            int count = 0;
            if (File.Exists(path))
            { count = System.IO.File.ReadAllLines(path).Length; }
            int employeeId = ++count;
            /*new Guid(0xA,0xB,0xC,new Byte[] {0,1,2,3,4,5,6,7});*/
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

        static void Print() /*Вывод информации в файле*/
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
                    string empId = Console.ReadLine(); /*Enter employee's id*/
                    string employeeString = string.Empty;
                    var counter = 1;
                    while ((employeeString = pr.ReadLine()) != null)
                    {
                        var employeeArray = employeeString.Split('#');
                        if (employeeArray.Length != 7)
                        {
                            Console.WriteLine($"Data of employee n.{counter} is corrupted");
                        }
                        else
                        {
                            while (true)
                            {
                                if (employeeArray[0] == empId)
                                {
                                    Console.WriteLine($"employee's ID: {employeeArray[0]}");
                                    Console.WriteLine($"adding time: {employeeArray[1]}");
                                    Console.WriteLine($"employee's full name: {employeeArray[2]}");
                                    Console.WriteLine($"employee's age: {employeeArray[3]}");
                                    Console.WriteLine($"employee's height: {employeeArray[4]}");
                                    Console.WriteLine($"employee's birth date: {employeeArray[5]}");
                                    Console.WriteLine($"employee's birth place: {employeeArray[6]}");
                                }
                            

                                else
                                {
                                    Console.WriteLine($"Data is corrupted, enter correct employee's ID ");
                                    Console.WriteLine($"Enter employee's id");
                                    Console.ReadLine(); 
                                }
                            }
                        }

                        Console.WriteLine("-----");
                        counter++;
                    }
                }

            }

        }


        static void Help() /*Команда помощи выбора команды*/
        {
            Console.WriteLine($"enter 1 to for data output or enter 2 adding new data to the file");
        }
    }
}
