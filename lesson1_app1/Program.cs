using System;

namespace lesson1_app1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Please enter your name: ");
            string userName = Console.ReadLine();

            while (String.IsNullOrEmpty(userName))
            {
                Console.Write($"Name field can't be empty, please re-enter name: ");
                userName = Console.ReadLine();
            }

            Console.WriteLine($"\nHello, {userName}, today is {DateTime.Today.ToString("d")}.");

            Console.ReadLine();
        }
    }
}
