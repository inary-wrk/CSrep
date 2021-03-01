﻿using System;
using GeekBrainsTests;
using System.Collections.Generic;

namespace App1RefList
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = new MyList(10,20,20);
            n.AddNode(130);
            n.AddNode(140);
            n.AddNode(160);
            n.AddNode(110);
            var l = new MyList();
            l.AddNode(50);
            l.AddNode(60);
            l.AddNode(41);
            l.AddNode(81);
            var x = l.FindNode(60);
            l.AddNodeAfter(x, 3);
            int r = l.GetCount();
            MyList empty = new MyList(1);           
            
            foreach (var item in empty)
            {
                Console.WriteLine(item.Value);
            }
        }
    }
}
