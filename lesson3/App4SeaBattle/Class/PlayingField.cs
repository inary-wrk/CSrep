using System;

namespace App4SeaBattle.Class
{
    class PlayingField
    {
        public static int x = 11, y = 11;
        public static string water = "≈";
        public string[,] player1FieldShips = new string[x, y];
        public string[,] player1FieldShots = new string[x, y];
        public string[,] player2FieldShips = new string[x, y];
        public string[,] player2FieldShots = new string[x, y];

        public PlayingField()
        {
            FillField(player1FieldShips);
            FillField(player2FieldShips);
            FillField(player1FieldShots);
            FillField(player2FieldShots);
        }


        public void FillField(string[,] field)
        {
            char a = 'A';
            for (int i = 1; i < x; i++)
            {
                for (int j = 1; j < y; j++)
                {
                    field[i, j] = water;
                    field[i, 0] = i.ToString();
                }
                field[0, i] = a.ToString() + "|";
                a++;
            }
        }

        public void GetPlayingFieldShots(string[,] fieldShots)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            for (int i = 3; i < x * 3; i += 3)
            {
                for (int j = 1; j < y; j++)
                {
                    int b = 0;
                    if (fieldShots[i / 3, j].Length > 2) b = 1;
                    Console.SetCursorPosition(i + 40, j);
                    Console.Write(fieldShots[i / 3, j]);
                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 1; i < x * 3; i += 3)
            {
                var j = i / 3;
                Console.SetCursorPosition(i + 39, 0);
                Console.Write(fieldShots[j, 0]);

                Console.SetCursorPosition(0 + 40, j);
                Console.Write(fieldShots[0, j]);
            }


        }
        static public void GetPlayingFieldShips(string[,] fieldShips)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            for (int i = 3; i < x * 3; i += 3)
            {
                for (int j = 1; j < y; j++)
                {
                    int b = 0;
                    if (fieldShips[i / 3, j].Length > 2) b = 1;
                    Console.SetCursorPosition(i - b, j);
                    Console.Write(fieldShips[i / 3, j]);
                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 1; i < x * 3; i += 3)
            {
                var j = i / 3;
                Console.SetCursorPosition(i - 1, 0);
                Console.Write(fieldShips[j, 0]);

                Console.SetCursorPosition(0, j);
                Console.Write(fieldShips[0, j]);
            }
        }
    }
}