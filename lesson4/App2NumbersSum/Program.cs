// 2. Написать программу, принимающую на вход строку — набор чисел, разделенных пробелом, и возвращающую число — сумму всех чисел в строке. Ввести данные с 
// клавиатуры и вывести результат на экран.

using System;

namespace App2NumbersSum
{
    class Program
    {
 
        public static bool StringNumbersSum(string numbers, out int sum)
        {
            sum = 0;
            string[] splitNumbers = numbers.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var item in splitNumbers)
            {
                if (Int32.TryParse(item, out int number)) sum += number;
                else return false;
            }
            return true;
        }


        static void Main(string[] args)
        {
            Console.Write("Enter string of numbers separate by \"space\": ");
            if (StringNumbersSum(Console.ReadLine(), out int sum)) Console.WriteLine("Sum of numbers: {0}", sum);
            else Console.WriteLine("Incorrect string.");
        }
    }
}
