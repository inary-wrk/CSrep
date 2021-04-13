using System;
using System.Collections.Generic;
namespace App1Graph
{
    class test
    {
      public  static int[] x = new int[10];
    }
    class Program
    {
        static void TEstM<T>(ICollection<T> col)
        {
            var en = col.GetEnumerator();
            while (en.MoveNext())
            {
                Console.WriteLine(en.Current);
            }
        }

        static void Main(string[] args)
        {
            var en = test.x.GetEnumerator();
            en.MoveNext();
            TEstM(test.x);
            Console.WriteLine("Hello World!");
        }
    }
}
