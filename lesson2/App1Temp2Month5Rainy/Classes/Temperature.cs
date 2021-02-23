using System;

namespace App1Temp2Month5Rainy.Classes
{
    public class Temperature
    {
        public int maxTemp;
        public int minTemp;
        public float avrgTemp;


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

        public float AverageTemp(int minTemp, int maxTemp)
        {
            return avrgTemp = (float)(minTemp + maxTemp) / 2;
        }

    }
}
