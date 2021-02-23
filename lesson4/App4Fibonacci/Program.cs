// 4. (*) Написать программу, вычисляющую число Фибоначчи для заданного значения рекурсивным способом. 

using System;


namespace App4Fibonacci
{
    class Program
    {
        static long FibonacciCorrect()
        {
            while (true)
            {
                Console.Write("Enter the ordinal number of the Fibonacci number: ");
                if (Int32.TryParse(Console.ReadLine(), out int number) == true)
                {
                    return Fibonacci(Math.Abs(number)) * Math.Sign(number);
                }
                else Console.WriteLine("The number must be an integer.");
            }
        }

        static long Fibonacci(int n)
        {
            return (n == 0 || n == 1) ? n : Fibonacci(n - 1) + Fibonacci(n - 2);
        }


        static void Main(string[] args)
        {
            Console.WriteLine(FibonacciCorrect());
        }
    }
}