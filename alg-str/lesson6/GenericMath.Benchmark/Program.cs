using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;


/*
|            Method |  length |            Mean |         Error |        StdDev |            Median |
|------------------ |-------- |----------------:|--------------:|--------------:|------------------:|
|    Add_NotGeneric |       1 |        67.00 ns |     18.704 ns |     55.149 ns |       100.0000 ns |
|   Add_GenericMath |       1 |       148.00 ns |     19.569 ns |     57.700 ns |       100.0000 ns |
|      Add_MiscUtil |       1 |       130.00 ns |     20.166 ns |     59.459 ns |       100.0000 ns |
| Add_MiscUtil_Net5 |       1 |       114.29 ns |     23.607 ns |     68.864 ns |       100.0000 ns |
|    Add_NotGeneric |      10 |        60.00 ns |     17.381 ns |     51.247 ns |       100.0000 ns |
|   Add_GenericMath |      10 |       177.08 ns |     24.881 ns |     71.788 ns |       200.0000 ns |
|      Add_MiscUtil |      10 |       163.00 ns |     19.673 ns |     58.006 ns |       200.0000 ns |
| Add_MiscUtil_Net5 |      10 |       121.00 ns |     16.903 ns |     49.838 ns |       150.0000 ns |
|    Add_NotGeneric |     100 |        37.37 ns |     17.954 ns |     52.656 ns |         0.0000 ns |
|   Add_GenericMath |     100 |       430.22 ns |     14.284 ns |     40.055 ns |       450.0000 ns |
|      Add_MiscUtil |     100 |       400.00 ns |      0.000 ns |      0.000 ns |       400.0000 ns |
| Add_MiscUtil_Net5 |     100 |       383.67 ns |     21.845 ns |     63.723 ns |       400.0000 ns |
|    Add_NotGeneric |   10000 |     6,916.67 ns |     73.950 ns |     57.735 ns |     6,900.0000 ns |
|   Add_GenericMath |   10000 |    32,607.69 ns |    133.545 ns |    111.516 ns |    32,600.0000 ns |
|      Add_MiscUtil |   10000 |    25,920.00 ns |    172.381 ns |    161.245 ns |    25,900.0000 ns |
| Add_MiscUtil_Net5 |   10000 |    26,061.54 ns |    142.858 ns |    119.293 ns |    26,000.0000 ns |
|    Add_NotGeneric | 1000000 | 2,457,340.74 ns | 48,319.452 ns | 67,737.123 ns | 2,444,100.0000 ns |
|   Add_GenericMath | 1000000 | 3,812,573.33 ns | 58,832.542 ns | 55,031.997 ns | 3,810,800.0000 ns |
|      Add_MiscUtil | 1000000 | 3,482,292.31 ns | 47,607.655 ns | 39,754.548 ns | 3,478,100.0000 ns |
| Add_MiscUtil_Net5 | 1000000 | 3,458,161.54 ns | 39,789.137 ns | 33,225.731 ns | 3,457,300.0000 ns |
*/

namespace GenericMathBenchmark
{
    public class GenMy<T>
    {
        T value1;
        T value2;

        public GenMy(T value1, T value2)
        {
            this.value1 = value1;
            this.value2 = value2;
        }


        public T AddGenericMath()
        {
            return Generic.Math.GenericMath.Add(value1, value2);
        }

        public T AddMiscUtil()
        {
            return MiscUtil.Operator.Add(value1, value2);
        }

        public T AddMiscUtilNet5()
        {
            return InaryMiscUtil.Operator.Add(value1, value2);
        }
    }

    public class My
    {
        int value1;
        int value2;

        public My(int value1, int value2)
        {
            this.value1 = value1;
            this.value2 = value2;
        }

        public int Add()
        {
            return value1 + value2;
        }
    }

    static class RandomExtension
    {
        public static int Int(this Random random) => random.Next(int.MinValue / 2, int.MaxValue / 2);
    }

    public class Benchmark
    {
        [Params(1, 10, 100, 10000, 1_000_000)]
        public int length;

        static Random rnd = new();

        public GenMy<int>[] genMy;
        public My[] my;

        [IterationSetup]
        public void ISetUp()
        {
            genMy = new GenMy<int>[length];
            my = new My[length];
            for (int i = 0; i < genMy.Length; i++)
            {
                var x1 = rnd.Int();
                var x2 = rnd.Int();
                genMy[i] = new(x1, x2);
                my[i] = new(x1, x2);
            }
        }

        [Benchmark]
        public void Add_NotGeneric()
        {
            for (int i = 0; i < my.Length; i++)
                my[i].Add();
        }

        [Benchmark]
        public void Add_GenericMath()
        {
            for (int i = 0; i < genMy.Length; i++)
                genMy[i].AddGenericMath();
        }

        [Benchmark]
        public void Add_MiscUtil()
        {
            for (int i = 0; i < genMy.Length; i++)
                genMy[i].AddMiscUtil();
        }

        [Benchmark]
        public void Add_MiscUtil_Net5()
        {
            for (int i = 0; i < genMy.Length; i++)
                genMy[i].AddMiscUtilNet5();
        }

    }



    class Program
    {
        static void Main(string[] args)
        {
            // debug
           //BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, new DebugInProcessConfig());
            BenchmarkSwitcher.FromAssembly(typeof(Benchmark).Assembly).Run(args);
        }
    }
}
