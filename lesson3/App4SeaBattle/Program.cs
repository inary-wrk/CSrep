using System;
using App4SeaBattle.Class;
namespace App4SeaBattle
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            PlayingField field = new PlayingField();
            ////field.player1FieldShips[2, 2] = '^';
            ////field.player1FieldShips[1, 2] = '[';
            ////field.player1FieldShips[3, 2] = ']';
            ////field.player1FieldShips[1, 3] = '[';
            ////field.player1FieldShips[2, 3] = 'o';
            ////field.player1FieldShips[3, 3] = ']';
            ////field.player1FieldShips[1, 4] = '[';
            ////field.player1FieldShips[2, 4] = '_';
            ////field.player1FieldShips[3, 4] = ']';
            //;
            //field.player1FieldShips[1, 2] = '│';
            //field.player1FieldShips[2, 2] = '^';
            //field.player1FieldShips[3, 2] = '│';
            //field.player1FieldShips[1, 3] = '│';
            //field.player1FieldShips[2, 3] = 'o';
            //field.player1FieldShips[3, 3] = '│';
            //field.player1FieldShips[1, 4] = '│';
            //field.player1FieldShips[2, 4] = '▄';
            //field.player1FieldShips[3, 4] = '│';

            //field.player1FieldShips[8, 4] = '_';
            //field.player1FieldShips[8, 5] = '>';
            //field.player1FieldShips[8, 6] = '̅';
            //field.player1FieldShips[7, 6] = '̅';
            //field.player1FieldShips[7, 6] = '̅';
            //field.player1FieldShips[7, 6] = '̅';
            //field.player1FieldShips[6, 6] = '̅';
            //field.player1FieldShips[6, 6] = '̅';
            //field.player1FieldShips[6, 6] = '̅';
            //field.player1FieldShips[5, 4] = '_';
            //field.player1FieldShips[5, 5] = 'o';
            //field.player1FieldShips[5, 6] = '̅';
            //field.player1FieldShips[4, 4] = '_';
            //field.player1FieldShips[4, 5] = '▌';
            //field.player1FieldShips[4, 6] = '̅';

            field.GetPlayingField(field.player1FieldShips, field.player1FieldShots);
            Console.ReadLine();
        }
    }
}
