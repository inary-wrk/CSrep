// 3. Написать метод по определению времени года. На вход подаётся число – порядковый номер месяца. На выходе — значение из перечисления (enum) — 
// Winter, Spring, Summer, Autumn. Написать метод, принимающий на вход значение из этого перечисления и возвращающий название времени года 
// (зима, весна, лето, осень). Используя эти методы, ввести с клавиатуры номер месяца и вывести название времени года. Если введено некорректное число, 
// вывести в консоль текст «Ошибка: введите число от 1 до 12».Написать метод по определению времени года. На вход подаётся число – порядковый номер месяца. 
// На выходе — значение из перечисления (enum) — Winter, Spring, Summer, Autumn. Написать метод, принимающий на вход значение из этого перечисления и возвращающий
// название времени года (зима, весна, лето, осень). Используя эти методы, ввести с клавиатуры номер месяца и вывести название времени года. Если введено 
// некорректное число, вывести в консоль текст «Ошибка: введите число от 1 до 12».


using System;


namespace App3Seasons
{
    [Flags]
    enum Months : int
    {
        empty = 0b_0_000_000_000_00,
        January = 0b_0_000_000_000_01,
        February = 0b_0_000_000_000_10,
        March = 0b_0_000_000_001_00,
        April = 0b_0_000_000_010_00,
        May = 0b_0_000_000_100_00,
        June = 0b_0_000_001_000_00,
        July = 0b_0_000_010_000_00,
        August = 0b_0_000_100_000_00,
        September = 0b_0_001_000_000_00,
        October = 0b_0_010_000_000_00,
        November = 0b_0_100_000_000_00,
        December = 0b_1_000_000_000_00,
        Winter = December | January | February,
        Spring = March | April | May,
        Summer = June | July | August,
        Autumn = September | October | November
    }


    class Program
    {

        static int SetMonth()
        {
            while (true)
            {
                Console.Write("Введите номер месяца: ");
                if (Int32.TryParse(Console.ReadLine(), out int number) == true)
                    if (number >= 1 && number <= 12) return number;          // return the month number
                    else Console.WriteLine("Ошибка: введите число от 1 до 12.");
                else Console.WriteLine("Номер месяца должен быть числом.");
            }
        }


        static Months GetSeason(int number)
        {
            var season = Months.empty;
            var month = (Months)Enum.GetValues(typeof(Months)).GetValue(number);

            if ((month & Months.Winter) == month) season = Months.Winter;
            if ((month & Months.Spring) == month) season = Months.Spring;
            if ((month & Months.Summer) == month) season = Months.Summer;
            if ((month & Months.Autumn) == month) season = Months.Autumn;
            return season;
        }

        static string GetSeasonRu (Months season)
        {
            string ruSeason = "empty";
            switch (season)
            {
                case Months.Winter: ruSeason = "Зима";
                    break;
                case Months.Spring: ruSeason = "Весна";
                    break;
                case Months.Summer: ruSeason = "Лето";
                    break;
                case Months.Autumn: ruSeason = "Осень";
                    break;
            }
            return ruSeason;
        }

        static void Main(string[] args)
        {
            Console.WriteLine(GetSeasonRu(GetSeason(SetMonth())));
        }
    }
}
