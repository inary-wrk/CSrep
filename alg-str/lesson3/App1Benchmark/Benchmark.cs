using System;
using System.Collections;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

/*
* Без создания экземпляра внутри бенчмарка. 1 дистанция.
* Методы начинающиеся с I - методы класса с использовнием обобщений
|             Method |       Mean |     Error |    StdDev |     Median |
|------------------- |-----------:|----------:|----------:|-----------:|
|       IStructFloat |  1.8605 ns | 0.0413 ns | 0.0387 ns |  1.8516 ns |
|        IClassFloat | 15.4529 ns | 0.3330 ns | 0.3270 ns | 15.3742 ns |
| IStructFloatNoSqrt |  2.0904 ns | 0.0156 ns | 0.0138 ns |  2.0889 ns |
|      IStructDouble |  1.8564 ns | 0.0149 ns | 0.0139 ns |  1.8522 ns |
|        StructFloat |  1.8461 ns | 0.0531 ns | 0.0497 ns |  1.8472 ns |
|         ClassFloat |  0.0003 ns | 0.0012 ns | 0.0010 ns |  0.0000 ns |
|  StructFloatNoSqrt |  2.0293 ns | 0.0204 ns | 0.0171 ns |  2.0268 ns |
|       StructDouble |  0.0397 ns | 0.0055 ns | 0.0049 ns |  0.0397 ns |


* Без создания экземпляра внутри бенчмарка. N = 5000 дистанций.
* Методы начинающиеся с I - методы класса с использовнием обобщений

|             Method |     Mean |    Error |   StdDev |
|------------------- |---------:|---------:|---------:|
|       IStructFloat | 13.09 us | 0.130 us | 0.109 us |
|        IClassFloat | 75.41 us | 0.402 us | 0.376 us |
| IStructFloatNoSqrt | 13.06 us | 0.161 us | 0.143 us |
|      IStructDouble | 13.95 us | 0.149 us | 0.132 us |
|        StructFloat | 12.34 us | 0.052 us | 0.043 us |
|         ClassFloat | 15.40 us | 0.191 us | 0.178 us |
|  StructFloatNoSqrt | 12.35 us | 0.064 us | 0.056 us |
|       StructDouble | 13.69 us | 0.050 us | 0.045 us |



* С создания экземпляра внутри бенчмарка. 1 дистанция.
* Методы начинающиеся с I - методы класса с использовнием обобщений
|             Method |       Mean |     Error |    StdDev |
|------------------- |-----------:|----------:|----------:|
|       IStructFloat |  6.5622 ns | 0.0651 ns | 0.0577 ns |
|        IClassFloat | 27.9750 ns | 0.1616 ns | 0.1512 ns |
| IStructFloatNoSqrt |  6.9724 ns | 0.0422 ns | 0.0394 ns |
|      IStructDouble |  7.7289 ns | 0.0618 ns | 0.0548 ns |
|        StructFloat | 15.5535 ns | 0.0261 ns | 0.0231 ns |
|         ClassFloat |  6.3401 ns | 0.0281 ns | 0.0234 ns |
|  StructFloatNoSqrt | 10.2306 ns | 0.0424 ns | 0.0397 ns |
|       StructDouble |  0.5047 ns | 0.0092 ns | 0.0086 ns |
*/
namespace App1Benchmark
{
    public class Benchmark
    {
        public static Random rnd = new();
        public static readonly int N = 5000;


        public static Distance<PointFloatGen, float>[] floatStructD = new Distance<PointFloatGen, float>[N];
        public static Distance<PointDoubleGen, double>[] doubleStructD = new Distance<PointDoubleGen, double>[N];
        public static Distance<PointClassGen, float>[] floatClassD = new Distance<PointClassGen, float>[N];

        public static PointFloat[,] floatStruct = new PointFloat[N, 2];
        public static PointDouble[,] doubleStruct = new PointDouble[N, 2];
        public static PointClass[,] floatClass = new PointClass[N, 2];


        static Benchmark()
        {
            for (int i = 0; i < N; i++)
            {
                var x1 = rnd.Next(int.MinValue, int.MaxValue);
                var x2 = rnd.Next(int.MinValue, int.MaxValue);
                var y1 = rnd.Next(int.MinValue, int.MaxValue);
                var y2 = rnd.Next(int.MinValue, int.MaxValue);
                floatStructD[i] = new Distance<PointFloatGen, float>
                {
                    FirstPoint = new PointFloatGen { X = x1, Y = y1 },
                    SecondPoint = new PointFloatGen { X = x2, Y = y2 }
                };

                doubleStructD[i] = new Distance<PointDoubleGen, double>
                {
                    FirstPoint = new PointDoubleGen { X = x1, Y = y1 },
                    SecondPoint = new PointDoubleGen { X = x2, Y = y2 }
                };

                floatClassD[i] = new Distance<PointClassGen, float>
                {
                    FirstPoint = new PointClassGen { X = x1, Y = y1 },
                    SecondPoint = new PointClassGen { X = x2, Y = y2 }
                };

                floatStruct[i, 0] = new PointFloat { X = x1, Y = y1 };
                floatStruct[i, 1] = new PointFloat { X = x2, Y = y2 };

                floatClass[i, 0] = new PointClass { X = x1, Y = y1 };
                floatClass[i, 1] = new PointClass { X = x2, Y = y2 };

                doubleStruct[i, 0] = new PointDouble { X = x1, Y = y1 };
                doubleStruct[i, 1] = new PointDouble { X = x2, Y = y2 };
            }
        }



        //public Distance<PointFloatGen, float> floatStruct = new()
        //{
        //    FirstPoint = new PointFloatGen { X = 56, Y = 10 },
        //    SecondPoint = new PointFloatGen { X = -10, Y = -43 }
        //};

        //public Distance<PointDoubleGen, double> doubleStruct = new()
        //{
        //    FirstPoint = new PointDoubleGen { X = 56, Y = 10 },
        //    SecondPoint = new PointDoubleGen { X = -10, Y = -43 }
        //};

        //public Distance<PointClassGen, float> floatClass = new()
        //{
        //    FirstPoint = new PointClassGen { X = 56, Y = 10 },
        //    SecondPoint = new PointClassGen { X = -10, Y = -43 }
        //};

        [Benchmark]
        public void IStructFloat()
        {
            for (int i = 0; i < N; i++)
                floatStructD[i].DistanceCalc(true);
        }
        [Benchmark]
        public void IClassFloat()
        {
            for (int i = 0; i < N; i++)
                floatClassD[i].DistanceCalc(true);
        }
        [Benchmark]
        public void IStructFloatNoSqrt()
        {
            for (int i = 0; i < N; i++)
                floatStructD[i].DistanceCalc(false);
        }
        [Benchmark]
        public void IStructDouble()
        {
            for (int i = 0; i < N; i++)
                doubleStructD[i].DistanceCalc(true);
        }



        //    public PointFloat firstPointFloatStr = new() { X = 56, Y = 10 };
        //    public PointFloat secondPointFloatStr = new() { X = -10, Y = -43 };

        //    public PointDouble firstPointDoubleStr = new() { X = 56, Y = 10 };
        //    public PointDouble secondPointDoubleStr = new() { X = -10, Y = -43 };

        //    public PointClass firstPointFloatClass = new() { X = 56, Y = 10 };
        //    public PointClass secondPointFloatClass = new() { X = -10, Y = -43 };

        [Benchmark]
        public void StructFloat()
        {
            for (int i = 0; i < N; i++)
                DistanceCalc.Distance(floatStruct[i, 0], floatStruct[i, 1], true);
        }
        [Benchmark]
        public void ClassFloat()
        {
            for (int i = 0; i < N; i++)
                DistanceCalc.Distance(floatClass[i, 0], floatClass[i, 1], true);
        }
        [Benchmark]
        public void StructFloatNoSqrt()
        {
            for (int i = 0; i < N; i++)
                DistanceCalc.Distance(floatStruct[i, 0], floatStruct[i, 1], false);
        }
        [Benchmark]
        public void StructDouble()
        {
            for (int i = 0; i < N; i++)
                DistanceCalc.Distance(doubleStruct[i, 0], doubleStruct[i, 1], true);
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
