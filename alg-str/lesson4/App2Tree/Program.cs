using System;
using BinTree;
using System.Collections.Generic;


namespace App2Tree
{
    class sss
    {
        int x = 24;
        public string str { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var a = new Tree<string>();
            a.Search(null);
            Dictionary<sss, int> s = new();
            s.Add(new sss{ str = "asds" }, 34);
            s.Add(new sss{ str = "2" }, 34);
            s.Add(new sss{ str = "3" }, 34);
        }
    }
}
