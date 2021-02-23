using System;
using System.IO;


namespace test
{
    class test
    {


        static bool test1(int x, int y)
        {
            bool logic = false;
            try
            {
                if (x == 0)
                {

                    Console.WriteLine(x / y); return true;
                }

                Console.WriteLine(2);
            }
            catch (Exception)
            {
                logic = true;
            }


            return logic;
        }
        static void Main(string[] args)
        {
            string path = Console.ReadLine();
            DirectoryInfo dir = new DirectoryInfo(path);
            var ndir = dir.GetDirectories();
            if (ndir.Length == 0) Console.WriteLine("000000000000000000000000000");
            foreach(var dir1 in ndir)
            {
                Console.WriteLine(dir1.FullName);
            }
            //DirectoryInfo dirInfo = new DirectoryInfo(path);
            //dirInfo.CreateSubdirectory(Console.ReadLine());
            //try
            //{
            //    File.Create(path);
            //}
            //catch (UnauthorizedAccessException)
            //{
            //    Console.WriteLine("No acces, file hidden or read-only.");
            //}
            //catch (Exception ex) when (ex is NotSupportedException ||
            //                           ex is ArgumentException ||
            //                           ex is ArgumentNullException ||
            //                           ex is PathTooLongException)

            //{
            //    Console.WriteLine("Inadmissible file path.");
            //}

            //catch (DirectoryNotFoundException ex)
            //{
            //    Console.WriteLine("The specified directory was not found.");
            //}
            //catch (IOException)
            //{
            //    Console.WriteLine("An I/O error occurred while creating the file.");
            //}
            //catch (Exception)
            //{

            //    Console.WriteLine("Unknown error.");
            //}


        }
    }
}
