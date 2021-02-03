using System;
using System.IO;

namespace App2TimeStartup
{
    class Time
    {
        private static DateTime startUpTime = DateTime.UtcNow;

        public static DateTime StartUpTime { get => startUpTime;}

   

        public static void TimeToFile(DateTime time)
        {
            File.AppendAllText("StartUpTime.txt", $"{time.ToString()} UTC {time.ToLocalTime().ToString()} Local time\n");
        }
    }
}
