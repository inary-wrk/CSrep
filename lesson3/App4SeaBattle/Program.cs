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

            field.player1FieldShips[8, 4] = "[]►";
            field.player1FieldShips[7, 4] = "[o]";
            field.player1FieldShips[6, 4] = "[]";
            field.player1FieldShips[4, 5] = "▲";
            field.player1FieldShips[4, 6] = "[o]";
            field.player1FieldShips[4, 7] = "[ ]";
            field.GetPlayingField(field.player1FieldShips, field.player1FieldShots);
            Console.ReadLine();
        }
    }
}
