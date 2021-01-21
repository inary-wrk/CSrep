using System;

namespace App3Even
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Parity check, please enter the number: ");
            var number = Console.ReadLine();
            int intNumber;

            while (Int32.TryParse(number, out intNumber) == false)
            {
                Console.Write("The number must be an integer, please re-enter: ");
                number = Console.ReadLine();
            }

            if (intNumber % 2 == 1)

            {
                Console.WriteLine("The number {0} is not even. Method - remainder of division by 2.", number);

            }
            else
            {
                Console.WriteLine("The number {0} is even. Method - remainder of division by 2.", number);
            }

            if ((intNumber & 1) == 1)

            {
                Console.WriteLine("The number {0} is not even. Method - bitwise multiplication.", number);

            }
            else
            {
                Console.WriteLine("The number {0} is even. Method - bitwise multiplication.", number);
            }

            Console.ReadLine();
        }
    }
}
