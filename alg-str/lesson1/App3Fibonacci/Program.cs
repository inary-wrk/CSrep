using System;


namespace App3Fibonacci
{
    public class Program
    {


        public static int FibonacciRec(int n)
        {
            int sign = Math.Sign(n);
            return (n < 2 & n > -2) ? n : FibonacciRec(n - 1 * sign) + FibonacciRec(n - 2 * sign);
        }


        public static int FibonacciLoop(int number)
        {
            int a = 0, b = 1, temp;

            for (int i = 0; i < Math.Abs(number); i++)
            {
                temp = b;
                b += a;
                a = temp;
            }
            return a * Math.Sign(number);
        }


        static void Main(string[] args)
        {

            Console.WriteLine("rec: " + FibonacciRec(10));
            Console.WriteLine("loop: " + FibonacciLoop(10));

        }
    }
}

