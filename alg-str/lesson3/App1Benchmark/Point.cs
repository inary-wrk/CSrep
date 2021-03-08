using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1Benchmark
{
    public class PointClass
    {
        public float X { get; set; }
        public float Y { get; set; }
    }

    public struct PointFloat
    {
        public float X { get; set; }
        public float Y { get; set; }
    }

    public struct PointDouble
    {
        public double X { get; set; }
        public double Y { get; set; }
    }

}
