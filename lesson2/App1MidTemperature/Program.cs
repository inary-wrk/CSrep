using System;

namespace App1MidTemperature
{
    public class TemperatureMaxMinAvrg
    {

        public int IntCorrectInputCheck(string temperature)
        {
            int number;
            while (Int32.TryParse(temperature, out number) == false)
            {
                Console.Write("The temperature must be an integer, please re-enter: ");
                temperature = Console.ReadLine();
            }
            return number;
        }

        public void AverageTemperature(int minTemperature, int maxTemperature)
        {
            Console.WriteLine($"Average temperature for the day: {(minTemperature + maxTemperature) / 2}");
        }


    }

    class Program
    {

        static void Main(string[] AvrgTemperature)
        {
            TemperatureMaxMinAvrg temperatureCheck_Average = new TemperatureMaxMinAvrg();
            Console.Write("Enter the maximum temperature for the day: ");
            int intMaxT = temperatureCheck_Average.IntCorrectInputCheck(Console.ReadLine());

            Console.Write("Enter the minimum temperature for the day: ");
            int intMinT = temperatureCheck_Average.IntCorrectInputCheck(Console.ReadLine());

            temperatureCheck_Average.AverageTemperature(intMinT, intMaxT);

            Console.ReadLine();

        }

    }
}
