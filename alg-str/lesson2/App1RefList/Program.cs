using System;
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
            l.AddNode(30);
            foreach (var item in l)
            {
                Console.WriteLine(item.Value);
            }
        }
    }
}
