// Написать программу, выводящую введенную пользователем строку в обратном порядке (olleH вместо Hello).
using System;

namespace App3StringReverse
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the string: ");
            string myString = Console.ReadLine();


            char[] arrMyString = myString.ToCharArray();
            Array.Reverse(arrMyString);
            Console.WriteLine("Method Array.Reverse: {0}", new String(arrMyString));


            char[] reversedArrMyString = new char[myString.Length];
            int dim0 = reversedArrMyString.GetUpperBound(0);
            for (int i = 0; i <= dim0; i++)
            {
                reversedArrMyString[dim0 - i] = myString[i];
            }
            Console.WriteLine("Method my reverse, for loop: {0}", new String(reversedArrMyString));
            Console.ReadLine();
        }
    }
}
