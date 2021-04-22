using System;
using System.Collections.Generic;
using MyGraph;

namespace App1Graph
{
    class test<T>
       where T: struct, IComparable
    {
        public int? MyProperty { get; set; }
        public  static int[] x = new int[10];

        public static void DefaultT(T? item)
        {
            if (item.Value.CompareTo(default(T)) == 0) Console.WriteLine("default");
            else Console.WriteLine("not default");
        }
    }


    class Program
    {

        static void Main(string[] args)
        {

            var x = new Dictionary<int, string>();
           
        }
    }
}
