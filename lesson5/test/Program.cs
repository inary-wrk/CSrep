using System;
using System.IO;

namespace test
{
    class Program
    {
        static bool test(int x, int y)
        {
            bool logic = false;
            try
            {
                if (x == 0)
                {

                Console.WriteLine(x/y);  return true;
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

            //Console.WriteLine(test(0,0));
            Console.WriteLine(Directory.Exists(@"new\second\file.txt")); 
        }
    }
}
