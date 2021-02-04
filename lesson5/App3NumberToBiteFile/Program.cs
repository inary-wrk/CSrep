// 3. Ввести с клавиатуры произвольный набор чисел (0...255) и записать их в бинарный файл.

using System;
using System.IO;



namespace App3NumberToBiteFile
{
    class Program
    {
        static void EnterPath(byte[] numbers)
        {
            //while (true)
            //{
            //    Console.WriteLine("Enter the path to the file, if the file does not exist it will be created:");
            //   // Directory.Exists()
            //}
            try
            {
                File.WriteAllBytes(Console.ReadLine(), numbers);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid path, The file bytes.bin was generated in the executable file folder.");
                File.WriteAllBytes("bytes.bin", numbers);
            }

        }

        static byte EnterNumber()
        {
            do
            {
                Console.Write("Enter an integer nuber in the range 0..255: ");
                if (Byte.TryParse(Console.ReadLine(), out byte number)) return number;
            } while (true);
        }


        static void Main(string[] args)
        {
            //Console.WriteLine(EnterNumber());
            Console.WriteLine(System.IO.Path.GetInvalidPathChars());
        }
    }
}
