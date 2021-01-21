using System;
namespace App2Months
{
    public class Months
    {
        public static string stringMonth;
        public string MonthByNumber(byte byteMonthNumber)
        {
            switch (byteMonthNumber)
            {
                case 1: stringMonth = "January"; break;
                case 2: stringMonth = "February"; break;
                case 3: stringMonth = "March"; break;
                case 4: stringMonth = "April"; break;
                case 5: stringMonth = "May"; break;
                case 6: stringMonth = "June"; break;
                case 7: stringMonth = "July"; break;
                case 8: stringMonth = "August"; break;
                case 9: stringMonth = "September"; break;
                case 10: stringMonth = "October"; break;
                case 11: stringMonth = "November"; break;
                case 12: stringMonth = "December"; break;
            }
            return stringMonth;
        }


        public byte MonthCorrectInput(string number)
        {
            byte byteMonthNumber;
            while ((Byte.TryParse(number, out byteMonthNumber) == false) || byteMonthNumber > 12 || byteMonthNumber < 1)
            {
                Console.Write("The number of the month must be an integer, greater than 0, less than 13, please re - enter: ");
                number = Console.ReadLine();

            }
            return byteMonthNumber;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Months months = new Months();

            Console.Write("Please enter the number of the month: ");
            byte byteMonthNumber = months.MonthCorrectInput(Console.ReadLine());

            Console.WriteLine("It's {0}", months.MonthByNumber(byteMonthNumber));
            Console.ReadLine();
        }
    }
}
