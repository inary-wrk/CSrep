using System;
using System.Collections.Generic;

namespace MyGraph
{
    public class Vertex<TVertex, TWeight>
         where TWeight : struct, IComparable
         where TVertex : notnull
    {
        public TVertex Name { get; set; }
        public Dictionary<Vertex<TVertex, TWeight>, TWeight> FromThis { get; set; }
        public HashSet<Vertex<TVertex, TWeight>> ToThis { get; set; }


        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Name.Equals(obj);
        }

        public override string ToString()
        {
            return Name.ToString();
        }
    }
}
