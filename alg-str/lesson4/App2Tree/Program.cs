using System;
using BinTree;
using System.Collections.Generic;
using System.Collections;


namespace App2Tree
{
    class Program
    {
        static void Main()
        {
            Tree<int> t = new(10, 20, 60, 40, 5, 6, 4, 3, -1, -2, 15, 100);
            List<int> l = new();
            List<int> l1 = new();
            List<int> l2 = new();

            t.Print(t.Root);
            t.Clear();
            //Console.WriteLine();
            //Console.WriteLine(1/2);
        }
    }
}
