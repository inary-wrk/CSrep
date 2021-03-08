using System;
using System.Collections;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

/*
* Без создания экземпляра внутри бенчмарка. 
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


* С создания экземпляра внутри бенчмарка. 
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
