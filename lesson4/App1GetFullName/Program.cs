// 1. Написать метод GetFullName(string firstName, string lastName, string patronymic), принимающий на вход ФИО в разных аргументах и возвращающий объединённую
// строку с ФИО. Используя метод, написать программу, выводящую в консоль 3–4 разных ФИО.


using System;

namespace App1GetFullName
{
    class Program
    {
        static void Main(string[] args)
        {
        Person person1 = new Person();
        Person person2 = new Person("Alexander", "Ivanov", "Mihailovich");
        Person person3 = new Person("Philip", "Elens", "");

            Console.WriteLine(person1.GetFullName());
            Console.WriteLine(person2.GetFullName());
            Console.WriteLine(person3.GetFullName());

            Console.WriteLine(person1.GetFullNameByArgs(person1.Name, person1.LastName, person1.Patronymic));
            Console.WriteLine(person2.GetFullNameByArgs(person2.Name, person2.LastName, person2.Patronymic));
            Console.WriteLine(person3.GetFullNameByArgs(person3.Name, person3.LastName, person3.Patronymic));
        }

    }
}
