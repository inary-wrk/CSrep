using System;
namespace App2Months
{
    class Months
    {
        public void MonthByNumber(byte byteMonthNumber)
        {
            switch (byteMonthNumber)
            {
                case 1:
                    Console.WriteLine("It's January");
                    break;
                case 2:
                    Console.WriteLine("It's February");
                    break;
                case 3:
                    Console.WriteLine("It's March");
                    break;
                case 4:
                    Console.WriteLine("It's April");
                    break;
                case 5:
                    Console.WriteLine("It's May");
                    break;
                case 6:
                    Console.WriteLine("It's June");
                    break;
                case 7:
                    Console.WriteLine("It's July");
                    break;
                case 8:
                    Console.WriteLine("It's August");
                    break;
                case 9:
                    Console.WriteLine("It's September");
                    break;
                case 10:
                    Console.WriteLine("It's October");
                    break;
                case 11:
                    Console.WriteLine("It's November");
                    break;
                case 12:
                    Console.WriteLine("It's December");
                    break;


            }
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
            months.MonthByNumber(byteMonthNumber);

            Console.ReadLine();
        }
    }
}
