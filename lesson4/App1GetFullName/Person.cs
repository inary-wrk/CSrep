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
                if (String.IsNullOrWhiteSpace(value)) name = "";
                else name = value;
            }
        }
        public string LastName
        {
            get => lastName;
            set
            {
                if (String.IsNullOrWhiteSpace(value)) lastName = "";
                else lastName = value;
            }
        }
        public string Patronymic
        {
            get => patronymic;
            set
            {
                if (String.IsNullOrWhiteSpace(value)) patronymic = "";
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
            while (logic)
            {
                Console.Write("Enter the {0}: ", whatName);
                name = Console.ReadLine();

                if (String.IsNullOrWhiteSpace(name))
                {
                    Console.Write("If you want to leave the {0} blank, type Y", whatName);
                    Console.ReadKey();
                   logic = Console.ReadKey() ==  ? false : true;
                }
            }
            return name;
        }


        public string FullName()
        {
            return $"{Name} {LastName} {Patronymic}";
        }

    }
}
