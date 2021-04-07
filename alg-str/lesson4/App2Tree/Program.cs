using System;
using BinTree;
using System.Collections.Generic;
using System.Collections;
using App1Search;


namespace App2Tree
{
    class Program
    {
        static void Main()
        {

            Tree<int> tree = new(true);
            Tree<string> treeString = new();

            while (tree.Count != 20)
            {
                treeString.Add(Benchmark.RandomString(Benchmark.rnd.Next(0, 10)));
                tree.Add(Benchmark.rnd.Next(-100, 100));
            }

            tree.Print(showHeight: true);
            Console.Write("\nDFS: ");
            var en = tree.DFS();
            while (en.MoveNext())
                Console.Write($"{{{en.Current.Item}}} ");

            Console.Write("\nBFS: ");
            en = tree.BFS();
            while (en.MoveNext())
                Console.Write($"{{{en.Current.Item}}} ");

            Console.WriteLine(Environment.NewLine);
            treeString.Print(showHeight: true);


        }
    }
}
