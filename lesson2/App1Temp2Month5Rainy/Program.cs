// 1. Запросить у пользователя минимальную и максимальную температуру за сутки и вывести среднесуточную температуру.
// 2. Запросить у пользователя порядковый номер текущего месяца и вывести его название.
// 5. (*) Если пользователь указал месяц из зимнего периода, а средняя температура > 0, вывести сообщение «Дождливая зима».

using System;
using App1Temp2Month5Rainy.Classes;

namespace App1Temp2Month5Rainy
{
    class Program
    {
        static void Main(string[] args)
        {
            var temp = new Temperature();
            Console.Write("Enter the maximum temperature for the day: ");
            temp.maxTemp = temp.IntCorrectInputCheck(Console.ReadLine());

            Console.Write("Enter the minimum temperature for the day: ");
            temp.minTemp = temp.IntCorrectInputCheck(Console.ReadLine());
            temp.AverageTemp(temp.minTemp, temp.maxTemp);


            var month = new Months();
            Console.Write("Please enter the number of the month: ");
            byte byteMonthNumber = month.MonthCorrectInput(Console.ReadLine());

            Console.WriteLine($"Average temperature: {temp.avrgTemp}.\nMonth is {month.MonthByNumber(byteMonthNumber)}.");
            if (temp.avrgTemp > 0)
            {
                switch (month.stringMonth)
                {
                    case "December":
                    case "January":
                    case "February": Console.WriteLine("Rainy winter."); break;

                }
            }
            Console.ReadLine();
        }
    }
}
