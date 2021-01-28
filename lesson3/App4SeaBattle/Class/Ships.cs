using System;
using App4SeaBattle.Class;

namespace App4SeaBattle.Class
{
    class Ships
    {
        public void OneDeck(string[,] field, int x, int y)
        {
            while (CheckPlacement(field, x, y))
            {

            }
        }
        public bool CheckPlacement(string[,] field, int x, int y)
        {
            string conc = field[x, y] + field[x, y + 1] + field[x + 1, y] + field[x - 1, y] + field[x, y - 1];
            if (conc == "≈≈≈≈≈") return true;
            return false;
        }

    }
}

