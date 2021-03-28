﻿using System;
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


            //Tree<int> t = new(10, 9, 20, 60, 40, 15,12,11,13,14,17, 5, 6,7,8, 4, 3, -1, -2, 0, 100);
            // Tree<int> t = new(18, 9, 61, 24, 80, 85, 31, 29, 57, 58);
            Tree<int> t2 = new(true, 18, 9, 61, 24, 80, 85, 31, 29, 57, 58);

            //Tree<int> t = new(18, 57, 58, 90, 100, 110, 120, 130, 140);
            //Tree<int> t2 = new(true, 18, 57, 58, 90, 100, 110, 120, 130, 140);

            //Tree<int> t = new(true, 2122759058, -560528402, -575174685, 333527576, -1410646536, 222703804, -292329913, -866847011, 1461376610, 694080023, -92258931, 1215972922, -498257739, 1360398742, 1018003925, 352846507, 1720116656, 2127386, -1032539648, -676881469, 1496509622, -1000881945, -192997892, -1174143796, -1792659774, 897129923, 185415413, 293897413, 292486519, -127668401);
            Tree<string> t = new(true);
            List<string> l = new();

            while (t.Count != 80)
            {
                string r = Benchmark.RandomString(Benchmark.rnd.Next(0, 30));
                //t.add(benchmark.randomstring(benchmark.rnd.next(0, 10)));
               // int r = Benchmark.rnd.Next(int.MinValue, int.MaxValue);
                t.Add(r);
                l.Add(r);
            }

            var en = t.DFS();

            t.Print(showHeight: true);
            while (en.MoveNext())
            {
                var bf = t.BalanceFactor(en.Current);
                if (Math.Abs(bf) > 1)
                {
                    Console.Write("({0}) bf: {1} ", en.Current.ToString(), bf);
                }

            }

            //Console.WriteLine();
            //foreach (var item in l)
            //{
            //    Console.Write(item + ", ");
            //}

            ////t.Remove(24);
            //Console.WriteLine();
            //t.Remove(80);
            //t.Print();
            //Console.WriteLine();
            //t2.Print();
            //Console.WriteLine();
            //t2.Remove(80);
            //t2.Print();
            //for (int i = 0; i < l.Count; i++)
            //   Console.Write("{0}, ", l[i]);

        }
    }
}
