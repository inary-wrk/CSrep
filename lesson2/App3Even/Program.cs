using System;

namespace App3Even
{
    class Program
    {
        static void Main(string[] args)
        {
            int knowledges = 0b011111;
            int back = 0b010111;
            int front = 0b011010;
            int designer = 0b110000;

            Console.WriteLine((knowledges & back) == back);
            Console.WriteLine((knowledges & front) == front);
            Console.WriteLine((knowledges & designer) == designer);
            
            Console.ReadLine();
        }
    }
}
