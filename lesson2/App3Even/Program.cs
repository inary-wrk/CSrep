// 3.Определить, является ли введённое пользователем число чётным.
using System;

namespace App3Even
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Parity check, please enter the number: ");
            var number = Console.ReadLine();
            long longNumber;

            while (long.TryParse(number, out longNumber) == false)
            {
                Console.Write("The number must be an integer, please re-enter: ");
                number = Console.ReadLine();
            }

            if (longNumber % 2 == 1)
                Console.WriteLine("The number {0} is not even. Method - remainder of division by 2.", number);
            else
                Console.WriteLine("The number {0} is even. Method - remainder of division by 2.", number);


            if ((longNumber & 1) == 1)
                Console.WriteLine("The number {0} is not even. Method - bitwise multiplication.", number);

            else
                Console.WriteLine("The number {0} is even. Method - bitwise multiplication.", number);

            Console.ReadLine();
        }
    }
}
