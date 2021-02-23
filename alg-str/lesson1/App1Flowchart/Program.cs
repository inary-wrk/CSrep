using System;

namespace App1Flowchart
{
    class Program
    {
        static void Prime(int number)
        {
            int d = 0, i = 2;

            while(i<number)
            {
                if (number % i == 0) d++;
                i++;
            }
            if (d == 0) Console.WriteLine("Простое");
            else Console.WriteLine("Не простое");
        }

        static void Main(string[] args)
        {

        }
    }
}
