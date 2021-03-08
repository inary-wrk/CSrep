using System;
using System.Collections;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

/*
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
*/
namespace App1Benchmark
{
    public class Benchmark
    {
        public Distance<PointFloatGen, float> floatStruct = new()
        {
            FirstPoint = new PointFloatGen { X = 56, Y = 10 },
            SecondPoint = new PointFloatGen { X = -10, Y = -43 }
        };

        public Distance<PointDoubleGen, double> doubleStruct = new()
        {
            FirstPoint = new PointDoubleGen { X = 56, Y = 10 },
            SecondPoint = new PointDoubleGen { X = -10, Y = -43 }
        };

        public Distance<PointClassGen, float> floatClass = new()
        {
            FirstPoint = new PointClassGen { X = 56, Y = 10 },
            SecondPoint = new PointClassGen { X = -10, Y = -43 }
        };

        [Benchmark]
        public void IStructFloat()
        {
            floatStruct.DistanceCalc(true);
        }
        [Benchmark]
        public void IClassFloat()
        {
            floatClass.DistanceCalc(true);
        }
        [Benchmark]
        public void IStructFloatNoSqrt()
        {
            floatStruct.DistanceCalc(false);
        }
        [Benchmark]
        public void IStructDouble()
        {
            doubleStruct.DistanceCalc(true);
        }



        public PointFloat firstPointFloatStr = new() { X = 56, Y = 10 };
        public PointFloat secondPointFloatStr = new() { X = -10, Y = -43 };

        public PointDouble firstPointDoubleStr = new() { X = 56, Y = 10 };
        public PointDouble secondPointDoubleStr = new() { X = -10, Y = -43 };

        public PointClass firstPointFloatClass = new() { X = 56, Y = 10 };
        public PointClass secondPointFloatClass = new() { X = -10, Y = -43 };

        [Benchmark]
        public void StructFloat()
        {
            DistanceCalc.Distance(firstPointFloatStr, secondPointFloatStr, true);
        }
        [Benchmark]
        public void ClassFloat()
        {
            DistanceCalc.Distance(firstPointFloatClass, secondPointFloatClass, true);
        }
        [Benchmark]
        public void StructFloatNoSqrt()
        {
            DistanceCalc.Distance(firstPointFloatStr, secondPointFloatStr, false);
        }
        [Benchmark]
        public void StructDouble()
        {
            DistanceCalc.Distance(firstPointDoubleStr, secondPointDoubleStr, true);
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
