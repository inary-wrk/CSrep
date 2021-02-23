using System;

namespace App1GetFullName
{
    public class Person
    {
        private string name;
        private string lastName;
        private string patronymic;

        public string Name
        {
            get => name;
            set
            {
                if (String.IsNullOrWhiteSpace(value)) name = "-";
                else name = value;
            }
        }
        public string LastName
        {
            get => lastName;
            set
            {
                if (String.IsNullOrWhiteSpace(value)) lastName = "-";
                else lastName = value;
            }
        }
        public string Patronymic
        {
            get => patronymic;
            set
            {
                if (String.IsNullOrWhiteSpace(value)) patronymic = "-";
                else patronymic = value;
            }
        }


        public Person(string name, string lastName, string patronymic)
        {
            Name = name;
            LastName = lastName;
            Patronymic = patronymic;
        }


        public Person()
        {
            Name = EnterName("Name");
            LastName = EnterName("Last Name");
            Patronymic = EnterName("Patronymic");
        }


        public string EnterName(string whatName)
        {
            bool logic = true;
            string newName = "";
            while (logic)
            {
                Console.Write("Enter the {0}: ", whatName);
                newName = Console.ReadLine();

                if (String.IsNullOrWhiteSpace(newName))
                {
                    Console.Write("If you want to keep the {0} field empty, press Y: ", whatName);
                    logic = Console.ReadKey().Key == ConsoleKey.Y ? false : true;
                    Console.WriteLine();
                }
                else logic = false;
            }
            return newName;
        }


        public string GetFullName()
        {
            return $"Name: {Name}, Last Name: {LastName}, Patronymic: {Patronymic}";
        }

        public string GetFullNameByArgs (string name, string lastName, string patronymic)
        {
            return $"{name} {lastName} {patronymic}";
        }
    }
}
