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
    enum Months
    {
        empty,
        Winter,
        Spring,
        Summer,
        Autumn
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

            switch (number)
            {
                case 12:
                case 1:
                case 2: season = Months.Winter; break;
                case 3:
                case 4:
                case 5: season = Months.Spring; break;
                case 6:
                case 7:
                case 8: season = Months.Summer; break;
                case 9:
                case 10:
                case 11: season = Months.Autumn; break;
            }

            return season;
        }

        static string GetSeasonRu(Months season)
        {
            string ruSeason = "empty";
            switch (season)
            {
                case Months.Winter:
                    ruSeason = "Зима";
                    break;
                case Months.Spring:
                    ruSeason = "Весна";
                    break;
                case Months.Summer:
                    ruSeason = "Лето";
                    break;
                case Months.Autumn:
                    ruSeason = "Осень";
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
