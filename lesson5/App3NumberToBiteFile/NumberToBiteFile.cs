// 3. Ввести с клавиатуры произвольный набор чисел (0...255) и записать их в бинарный файл.

using System;
using System.IO;
using System.Security;


namespace App3NumberToBiteFile
{
    class NumberToBiteFile
    {
        
        /// <summary>
        /// Tries to write the incoming byte array <paramref name="arrByte"/> to a byte file <paramref name="path"/>.
        /// Catch the exceptions that occur and outputs them to the console.
        /// </summary>
        /// <param name="path">File path</param>
        /// <param name="arrByte">Array of bytes</param>
        /// <returns>true if write complete without exception, otherwise false</returns>
        static bool MyWriteBytes(string path, byte[] arrByte)
        {
            try
            {
                File.WriteAllBytes(path, arrByte);
                return true;
            }
            catch (Exception ex) when (ex is SecurityException || ex is UnauthorizedAccessException)
            {
                Console.WriteLine("Acces error.");
            }
            catch (DirectoryNotFoundException)
            { 
               Console.WriteLine("One or more directories were not found in the specified path.");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Path or the byte array is null.");
            }
            catch
            {
                Console.WriteLine("Invalid path format.");
            }
            return false;
        }


        /// <summary>
        /// Reads a string, tries to convert it to a byte array.
        /// </summary>
        /// <param name="arrBytes">Returns an array of bytes via the out parameter.</param>
        /// <returns>true if the conversion was successful; otherwise, false.</returns>
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
                bool logic = false;
                while (!logic)
                {
                    Console.WriteLine("Enter file path");
                    string path = Console.ReadLine();
                    logic = MyWriteBytes(path, arrBytes);
                    Console.WriteLine("Do you want to generate bytes.bin file in the executable file folder?\nPress Y (yes) or any key (no).");
                    if (Console.ReadKey().Key == ConsoleKey.Y)
                    {
                        logic = MyWriteBytes("bytes.bin", arrBytes);
                    }
                }
            }
            else Console.WriteLine("Invalid string of bytes.");
        }
    }
}
