using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1Benchmark
{
    public interface IPoint<T>
    {
        public T X { get; set; }
        public T Y { get; set; }
        public T Add(T minuend, T subtrahend);
        public T Sub(T a, T b);
        public double Mult(T a, T b);
    }

    public struct PointDoubleGen : IPoint<double>
    {
        public double X { get; set; }
        public double Y { get; set; }

        public double Add(double a, double b)
        {
            return a + b;
        }

        public double Mult(double a, double b)
        {
            return a * b;
        }

        public double Sub(double minuend, double subtrahend)
        {
            return minuend - subtrahend;
        }


    }

    public struct PointFloatGen : IPoint<float>
    {
        public float X { get; set; }
        public float Y { get; set; }

        public float Add(float a, float b)
        {
            return a + b;
        }

        public double Mult(float a, float b)
        {
            return a * b;
        }

        public float Sub(float minuend, float subtrahend)
        {
            return minuend - subtrahend;
        }
    }

    public class PointClassGen : IPoint<float>
    {
        public float X { get; set; }
        public float Y { get; set; }

        public float Add(float a, float b)
        {
            return a + b;
        }

        public double Mult(float a, float b)
        {
            return a * b;
        }

        public float Sub(float minuend, float subtrahend)
        {
            return minuend - subtrahend;
        }
    }
}
