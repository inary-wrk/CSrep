using System;
using App4SeaBattle.Class;
namespace App4SeaBattle
{
    class Program
    {
        static void Main(string[] args)
        {
            PlayingField field = new PlayingField();
            field.GetPlayingField(field.player1FieldShips);
            Console.ReadLine();
        }
    }
}
