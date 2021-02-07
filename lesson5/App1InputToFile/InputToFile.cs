// 1. Ввести с клавиатуры произвольный набор данных и сохранить его в текстовый файл.

using System;
using System.IO;
using System.Security;

namespace App1InputToFile
{
    class InputToFile
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter an arbitrary data set");
            string data = Console.ReadLine();
            while (true)
            {
                Console.WriteLine("Enter file path");
                string path = Console.ReadLine();
                try
                {
                    File.WriteAllText(path, data);
                    break;
                }
                catch (DirectoryNotFoundException)
                {
                    Console.WriteLine("One or more directories were not found in the specified path.");
                }
                catch (Exception ex) when (ex is UnauthorizedAccessException || ex is SecurityException)
                {
                    Console.WriteLine("Access error");
                }
                catch
                {
                    Console.WriteLine("Invalid path format");
                }
                Console.WriteLine("Do you want to generate text.txt file in the executable file folder?\nPress Y (yes) or any key (no).");
                if(Console.ReadKey().Key == ConsoleKey.Y)
                {
                    File.WriteAllText("text.txt", data);
                    break;
                }
            }


        }
    }
}
