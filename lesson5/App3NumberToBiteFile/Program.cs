// 3. Ввести с клавиатуры произвольный набор чисел (0...255) и записать их в бинарный файл.

using System;
using System.IO;



namespace App3NumberToBiteFile
{
    class Program
    {
        static void WriteBytes(string path, byte[] numbers)
        {
            
            try
            {
                File.WriteAllBytes(path, numbers);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid path, The file bytes.bin was generated in the executable file folder.");
                File.WriteAllBytes("bytes.bin", numbers);
            }

        }

        static bool EnterByteString(out byte[] arrBytes)
        {
            Console.WriteLine("Enter a string of bytes (0..255) separated by spaces");
            string[] stringBytes = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            arrBytes = new byte[stringBytes.Length];
            for (int i = 0; i < stringBytes.Length; i++)
            {
                if (Byte.TryParse(stringBytes[i], out byte number)) arrBytes[i] = number;
                else return false;

            }
            return true;
        }


        static void Main(string[] args)
        {
            if (EnterByteString(out byte[] arrBytes))
            {
                Console.WriteLine("Enter file path");
                string path = Console.ReadLine();
                WriteBytes(path, arrBytes);
            }
            else Console.WriteLine("Invalid string of bytes.");
            
        }
    }
}
