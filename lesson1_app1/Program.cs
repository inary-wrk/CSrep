using System;

namespace lesson1_app1
{
    class Program
    {
        static void Main(string[] args)
        {
            string userName;
            DateTime dateToday = DateTime.Today;
            userName = Console.ReadLine();

            while (String.IsNullOrEmpty(userName))
            {
                Console.WriteLine("epmty name");
                userName = Console.ReadLine();

            }

            Console.WriteLine($"Hello, {userName}, today is {dateToday.ToString("d")}");

            Console.ReadLine();
        }
    }
}
