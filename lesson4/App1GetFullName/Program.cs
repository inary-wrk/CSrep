using System;

namespace App1GetFullName
{
    class Program
    {
        static void Main(string[] args)
        {
        Person person1 = new Person();
        Person person2 = new Person("Alexander", "Ivanov", "Temurovich");
        Person person3 = new Person("Philip", "Elens", "");

            Console.WriteLine(person1.FullName());
            Console.WriteLine(person2.FullName());
            Console.WriteLine(person3.FullName());
        }

    }
}
