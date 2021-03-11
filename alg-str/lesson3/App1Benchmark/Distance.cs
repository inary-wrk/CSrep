using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1Benchmark
{
    public class Distance<TPoint, T>
       where TPoint : IPoint<T>
    {
        public TPoint FirstPoint { get; set; }
        public TPoint SecondPoint { get; set; }


        public double DistanceCalc(bool sqrt)
        {
            var x = FirstPoint.Sub(FirstPoint.X, SecondPoint.Y);
            var y = FirstPoint.Sub(FirstPoint.Y, SecondPoint.Y);
            double distance = FirstPoint.Mult(x, x) + FirstPoint.Mult(y, y);
            if (sqrt) distance = Math.Sqrt(distance);
            return distance;
        }
    }


    public static class DistanceCalc
    { 

        public static double Distance(PointDouble firstPoint, PointDouble secondPoint, bool sqrt)
        {
            var x = secondPoint.X - firstPoint.X;
            var y = secondPoint.Y - firstPoint.Y;
            double distance = x * x + y * y;
            if (sqrt) distance = Math.Sqrt(distance);
            return distance;
        }


        public static double Distance(PointFloat firstPoint, PointFloat secondPoint, bool sqrt)
        {
            var x = secondPoint.X - firstPoint.X;
            var y = secondPoint.Y - firstPoint.Y;
            double distance = x * x + y * y;
            if (sqrt) distance = Math.Sqrt(distance);
            return distance;
        }

        public static double Distance(PointClass firstPoint, PointClass secondPoint, bool sqrt)
        {
            var x = secondPoint.X - firstPoint.X;
            var y = secondPoint.Y - firstPoint.Y;
            double distance = x * x + y * y;
            if (sqrt) distance = Math.Sqrt(distance);
            return distance;
        }
    }
}
