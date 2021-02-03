// 1. Ввести с клавиатуры произвольный набор данных и сохранить его в текстовый файл.

using System;
using System.IO;


namespace App1InputToFile
{
    class Program
    {
        static void Main(string[] args)
        {
            File.WriteAllText("text.txt", Console.ReadLine());

        }
    }
}
