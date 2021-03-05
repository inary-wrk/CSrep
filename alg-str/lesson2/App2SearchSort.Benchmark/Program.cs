using System;
using System.Collections.Generic;
using App2SearchSort;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

/*
|     Method |       array |         Mean |       Error |      StdDev |
|----------- |------------ |-------------:|------------:|------------:|
|  BubleSort | Int32[1000] | 715,449.6 ns | 6,859.46 ns | 6,416.34 ns |
|   CombSort | Int32[1000] |  40,737.4 ns |   373.80 ns |   349.65 ns |
| DefaulSort | Int32[1000] |   9,862.8 ns |    65.86 ns |    55.00 ns |
|  BubleSort |  Int32[100] |   5,047.4 ns |    60.20 ns |    56.31 ns |
|   CombSort |  Int32[100] |   1,297.0 ns |    10.06 ns |     9.41 ns |
| DefaulSort |  Int32[100] |     727.6 ns |     7.95 ns |     6.64 ns |
|  BubleSort |  Int32[500] | 161,787.9 ns |   564.30 ns |   500.23 ns |
|   CombSort |  Int32[500] |  11,823.5 ns |   233.09 ns |   277.48 ns |
| DefaulSort |  Int32[500] |   4,675.0 ns |    92.70 ns |   185.13 ns |
*/

namespace App2SearchSort.Benchmark
{
    public class Benchmarks
    {
        static Random rnd = new();
        static int[] array100;
        static int[] array500;
        static int[] array1000;

        static Benchmarks()
        {
            array100 = new int[100];
            FillArray(array100);
            array500 = new int[500];
            FillArray(array500);
            array1000 = new int[1000];
            FillArray(array1000);
        }

        public static void FillArray(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rnd.Next(Int32.MinValue, Int32.MaxValue);
            }
        }


        public IEnumerable<object> Arrays()
        {
            yield return array100;
            yield return array500;
            yield return array1000;
        }


        [Benchmark]
        [ArgumentsSource(nameof(Arrays))]
        public void BubleSort(int[] array) => Sorting.BubbleSort((int[])array.Clone());



        [Benchmark]
        [ArgumentsSource(nameof(Arrays))]
        public void CombSort(int[] array) => Sorting.CombSort((int[])array.Clone());


        [Benchmark]
        [ArgumentsSource(nameof(Arrays))]
        public void DefaulSort(int[] array) => Array.Sort((int[])array.Clone());

    }


    class Program
    {
        static void Main()
        {
            var summary = BenchmarkRunner.Run(typeof(Benchmarks).Assembly);
        }
    }
}
