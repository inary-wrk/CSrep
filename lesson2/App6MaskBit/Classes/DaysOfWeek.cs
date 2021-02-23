using System;

namespace App6MaskBit.Classes
{
    [Flags]
    public enum MaskWeekDays : byte
    {
        Monday = 0b_00_00001, // 1
        Tuesday = 0b_00_00010, // 2
        Wednesday = 0b_00_00100, // 4
        Thursday = 0b_00_01000, // 8
        Friday = 0b_00_10000, // 16
        Saturday = 0b_01_00000, // 32
        Sunday = 0b_10_00000,  // 64
        Weekend = Saturday | Sunday
    }
    public class DaysOfWeek
    {

        // Checking for correct entry, re-entry. Return byte Number of Day of the Week.
        public byte WeekDaysCorrect(string number)
        {
            byte byteDayNumber;
            while ((Byte.TryParse(number, out byteDayNumber) == false) || byteDayNumber > 7 || byteDayNumber < 1)
            {
                Console.Write("The day of the week must be an integer from 1 to 7, please re - enter: ");
                number = Console.ReadLine();

            }
            return byteDayNumber;
        }

    }
}
