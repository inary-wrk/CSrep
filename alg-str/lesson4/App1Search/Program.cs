using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Collections.Generic;
using System.Linq;

/*
|             Method | searchCount | notFoundStrings |             Mean |          Error |         StdDev |
|------------------- |------------ |---------------- |-----------------:|---------------:|---------------:|
|      HashSetSearch |           1 |               0 |         21.17 ns |       0.142 ns |       0.133 ns |
| ArrayStringsSearch |           1 |               0 |      1,520.02 ns |       4.322 ns |       3.831 ns |
|      HashSetSearch |           1 |              50 |      1,269.51 ns |      17.147 ns |      15.200 ns |
| ArrayStringsSearch |           1 |              50 |  1,435,779.99 ns |   4,870.396 ns |   4,317.479 ns |
|      HashSetSearch |           1 |             200 |      5,554.97 ns |      50.160 ns |      46.920 ns |
| ArrayStringsSearch |           1 |             200 |  5,755,897.97 ns |  41,988.607 ns |  39,276.169 ns |
|      HashSetSearch |         100 |               0 |      1,965.06 ns |      32.095 ns |      30.022 ns |
| ArrayStringsSearch |         100 |               0 |  1,508,331.69 ns |   3,067.680 ns |   2,869.510 ns |
|      HashSetSearch |         100 |              50 |      3,393.54 ns |      24.313 ns |      22.742 ns |
| ArrayStringsSearch |         100 |              50 |  2,954,826.77 ns |  37,040.616 ns |  34,647.815 ns |
|      HashSetSearch |         100 |             200 |      7,741.04 ns |     105.405 ns |      98.596 ns |
| ArrayStringsSearch |         100 |             200 |  7,252,502.08 ns |  47,465.182 ns |  44,398.960 ns |
|      HashSetSearch |        1000 |               0 |     26,023.07 ns |     290.592 ns |     242.657 ns |
| ArrayStringsSearch |        1000 |               0 | 15,912,466.46 ns | 180,353.196 ns | 168,702.492 ns |
|      HashSetSearch |        1000 |              50 |     27,342.91 ns |      98.797 ns |      82.500 ns |
| ArrayStringsSearch |        1000 |              50 | 15,598,422.40 ns |  53,589.964 ns |  50,128.086 ns |
|      HashSetSearch |        1000 |             200 |     32,075.66 ns |     465.425 ns |     435.359 ns |
| ArrayStringsSearch |        1000 |             200 | 19,325,410.58 ns | 192,990.999 ns | 161,156.225 ns |
*/


namespace App1Search
{
    public class Benchmark
    {
        public static Random rnd = new();

        public static readonly int stringsCount = 10000;

        [Params(1, 100, 1000)]
        public int searchCount = 1;
        [Params(0, 50, 200)]
        public int notFoundStrings;

        public static string[] arrStrings;
        public static HashSet<string> setStrings;
        public static string[] searchStrings;

        [GlobalSetup]
        public void SetUp()
        {
            setStrings = new();
            while (setStrings.Count != stringsCount)
                setStrings.Add(RandomString(rnd.Next(50)));
            arrStrings = setStrings.ToArray<string>();

            searchStrings = new string[searchCount + notFoundStrings];
            for (int i = 0; i < searchCount; i++)
                searchStrings[i] = arrStrings[rnd.Next(arrStrings.Length)];

            for (int i = searchCount; i < searchCount + notFoundStrings; i++)
                searchStrings[i] = RandomString(60);
        }

        public static string RandomString(int length)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[rnd.Next(s.Length)]).ToArray());
        }

        [Benchmark]
        public void HashSetSearch()
        {
            foreach (var item in searchStrings)
                setStrings.Contains(item);
        }

        [Benchmark]
        public void ArrayStringsSearch()
        {
            foreach (var item in searchStrings)
                StringSearch(arrStrings, item);
        }

        public static bool StringSearch(string[] array, string sought)
        {
            for (int i = 0; i < array.Length; i++)
                if (sought == array[i]) return true;

            return false;
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
           BenchmarkSwitcher.FromAssembly(typeof(Benchmark).Assembly).Run(args);
        }
    }
}
