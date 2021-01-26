using System;

namespace App4SeaBattle.Class
{
    class PlayingField
    {
        public char[,] player1FieldShips = new char[12, 12];
        public char[,] player1FieldShots = new char[12, 12];
        public char[,] player2FieldShips = new char[12, 12];
        public char[,] player2FieldShots = new char[12, 12];

        public PlayingField()
        {
            for (int i = 2; i <=11; i++)
            {
                for (int j = 2; j < 11; j++)
                {
                    player1FieldShips[i, j] = '~';
                    player1FieldShots[i, j] = '~';
                    player2FieldShips[i, j] = '~';
                    player2FieldShots[i, j] = '~';
                }
            }
        }
        public void GetPlayingField(char[,] field)
        {
            for (int i = 0; i <= 11; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    Console.SetCursorPosition(i, j);
                    Console.Write(field[i,j]);
                }
            }
        }
    }
}
