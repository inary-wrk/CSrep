using System;


namespace App3Fibonacci
{
    class Program
    {


        static int FibonacciRec(int n)
        {
            return (n == 1 || n == 2) ? n : FibonacciRec(n - 1) + FibonacciRec(n - 2);
        }


        static int FibonacciLoop(int n)
        {
            int a = 0, b = 1, temp;

            for (int i = 0; i < n; i++)
            {
                temp = b;
                b += a;
                a = temp;
            }
            return b;
        }


        static void Main(string[] args)
        {

            Console.WriteLine("rec: " + FibonacciRec(10));
            Console.WriteLine("loop: " + FibonacciLoop(10));

        }
    }
}

