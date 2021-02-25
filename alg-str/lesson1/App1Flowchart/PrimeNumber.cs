using System;

namespace App1Flowchart
{
    public class PrimeNumber
    {
        public static string Prime(int number)
        {
            int d = 0, i = 2;

            while (i < number)
            {
                if (number % i == 0) d++;
                i++;
            }
            return d == 0 ? "Простое" : "Не простое";
        }

        public static string PrimeWorked(int number)
        {
            int d = 0, i = 2;

            while (i < number)
            {
                if (number % i == 0) d++;
                i++;
            }
            return d == 0 ? "Простое" : "Не простое";
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            int number;
            do
            {
                Console.Write("Enter the integer number: ");
            }
            while (!Int32.TryParse(Console.ReadLine(), out number));
            Console.WriteLine(PrimeNumber.Prime(number));
        }
    }
}
