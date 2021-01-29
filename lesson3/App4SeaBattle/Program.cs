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

            //field.player1FieldShips[8, 4] = "[]►";
            //field.player1FieldShips[7, 4] = "[o]";
            //field.player1FieldShips[6, 4] = "[]";
            //field.player1FieldShips[4, 5] = "▲";
            //field.player1FieldShips[4, 6] = "[o]";
            //field.player1FieldShips[4, 7] = "[ ]";

            Ships.SetShips(field.player1FieldShips, 4);
            Ships.SetShips(field.player1FieldShips, 3);
            Ships.SetShips(field.player1FieldShips, 3);
            Ships.SetShips(field.player1FieldShips, 2);
            Ships.SetShips(field.player1FieldShips, 2);
            Ships.SetShips(field.player1FieldShips, 2);
            Ships.SetShips(field.player1FieldShips, 1);
            Ships.SetShips(field.player1FieldShips, 1);
            Ships.SetShips(field.player1FieldShips, 1);
            Ships.SetShips(field.player1FieldShips, 1);
            //PlayingField.GetPlayingFieldShips(field.player1FieldShips);
            Console.ReadLine();
        }
    }
}
