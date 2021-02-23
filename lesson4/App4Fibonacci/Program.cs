// 4. (*) Написать программу, вычисляющую число Фибоначчи для заданного значения рекурсивным способом. 

using System;


namespace App4Fibonacci
{
    class Program
    {


        static int Fibonacci(int n)
        {
            if (n == 0 || n == 1)
            {
                return n;
            }
            else return Fibonacci(n - 1) + Fibonacci(n - 2);
        }


        static void Main(string[] args)
        {
            Console.WriteLine(Fibonacci(Int32.Parse(Console.ReadLine())));
        }
    }
}
