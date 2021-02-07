// Написать программу, которая при старте дописывает текущее время в файл «startup.txt».


using System;
using System.IO;


namespace App2TimeStartup
{
    class StartUpTimeDate
    {
        private static readonly DateTime startUpTime = DateTime.UtcNow;


        /// <value>
        /// Get StartUp time/date UTC format.
        /// </value>
        public static DateTime StartUpTime { get => startUpTime; }


        /// <summary>
        /// Append <paramref name="time"/> to <paramref name="file"/> in UTC and local format.
        /// </summary>
        /// <param name="time"></param>
        /// <param name="file"></param>
        public static void TimeToFile(DateTime time, string file)
        {
            File.AppendAllText(file, $"{time.ToUniversalTime()} (UTC) \n{time.ToLocalTime()} Local time {TimeZoneInfo.Local}\n\n");
        }



        static void Main(string[] args)
        {
            TimeToFile(StartUpTime, "startup.txt");
        }
    }
}
