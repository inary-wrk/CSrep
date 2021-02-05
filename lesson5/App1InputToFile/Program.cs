// 1. Ввести с клавиатуры произвольный набор данных и сохранить его в текстовый файл.

using System;
using System.IO;


namespace App1InputToFile
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter an arbitrary data set");
            string data = Console.ReadLine();
            Console.WriteLine("Enter file path");
            string path = Console.ReadLine();
            try
            {
                File.WriteAllText(path, data);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid path, The file text.txt was generated in the executable file folder.");
                File.WriteAllText("text.txt", data);
            }
            

        }
    }
}
