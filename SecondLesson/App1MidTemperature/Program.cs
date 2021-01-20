using System;

namespace App1MidTemperature
{
    public class TemperatureMaxMinAvrg
    {

        public int intCheck_ReEnter(string temperature)
        {
            int number;
            while (Int32.TryParse(temperature, out number) == false)
            {
                Console.Write("The temperature must be an integer, please re-enter: ");
                temperature = Console.ReadLine();
            }
            return number;
        }

        public int averageTemperature(int minTemperature, int maxTemperature)
        {
            return (minTemperature + maxTemperature) / 2;
        }


    }

    class Program
    {

        static void Main(string[] AvrgTemperature)
        {
            TemperatureMaxMinAvrg temperatureCheck_Average = new TemperatureMaxMinAvrg();
            
            Console.Write("Enter the maximum temperature for the day: ");
            string maxTemperature = Console.ReadLine();
            int intMaxT = temperatureCheck_Average.intCheck_ReEnter(maxTemperature);
            
            Console.Write("Enter the minimum temperature for the day: ");
            string minTemperature = Console.ReadLine();
            int intMinT = temperatureCheck_Average.intCheck_ReEnter(minTemperature);


            Console.WriteLine($"Average temperature for the day: {temperatureCheck_Average.averageTemperature(intMaxT, intMinT)}");

            Console.ReadLine();

        }

    }
}
