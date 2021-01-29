using System;
using App4SeaBattle.Class;

namespace App4SeaBattle.Class
{
    class Ships
    {
        static Random rnd = new Random();
        static public void SetShips(string[,] field, int deck)
        {
            bool check = false;
            int x;
            int y;
            int z; // 0 - vertical; 1 - horizontal;
            do
            {
                x = rnd.Next(1, 10);
                y = rnd.Next(1, 10);
                z = rnd.Next(0, 1);
                //x = Convert.ToInt32(Console.ReadLine());
                //y = Convert.ToInt32(Console.ReadLine());
                if (((z == 0) && (y + deck > 11)) || ((z == 1) && (x - deck < 1))) continue;
                    for (int i = 0; i < deck; i++)
                    {
                        if (z == 0)
                        {
                            if (check = CheckPlacement(field, x, y + i, z, i)) field[x, y + i] = "[ ]"; else break;
                        }

                        else
                        {
                            if (check = CheckPlacement(field, x - i, y, z, i)) field[x - i, y] = "[ ]"; else break;
                        }
                    }
            } while (check == false);
            field[x, y] = "[ ]";
            PlayingField.GetPlayingFieldShips(field);
            //Move(field, x, y);

        }


        static public bool CheckPlacement(string[,] field, int x, int y, int z, int i)
        {
            bool freeSpace = false;
            if (field[x, y] == "≈")
                if ((y == 10) || (field[x, y + 1] == "≈"))
                    if ((y == 1) || (field[x, y - 1] == "≈") || ((z == 0) && (i > 0)))
                        if ((x == 10) || (field[x + 1, y] == "≈") || ((z == 1) && (i > 0)))
                            if ((x == 1) || (field[x - 1, y] == "≈")) freeSpace = true;
            return freeSpace;
        }


        static public void Move(string[,] field, int x, int y)
        {
            string swap = "";
            bool done = false;
            while (!done)
            {
                var key = Console.ReadKey(true).Key;
                int mX = 0;
                int mY = 0;
                switch (key)
                {
                    case ConsoleKey.UpArrow: mY = -1; break;
                    case ConsoleKey.DownArrow: mY = 1; break;
                    case ConsoleKey.RightArrow: mX = 1; break;
                    case ConsoleKey.LeftArrow: mX = -1; break;
                    case ConsoleKey.Spacebar:
                        break;
                    case ConsoleKey.Enter:
                        done = true;
                        break;
                }
                swap = field[x + mX, y + mY];
                field[x + mX, y + mY] = field[x, y];
                field[x, y] = swap;
                Console.Clear();
                PlayingField.GetPlayingFieldShips(field);
                x += mX;
                y += mY;

            }
        }

    }
}

