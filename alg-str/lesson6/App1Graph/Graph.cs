using System;
using System.Collections.Generic;
using System.Collections;

namespace MyGraph
{
    public class Vertex<TVertex, TWeight>
        where TWeight : struct, IComparable
        where TVertex : notnull
    {
        public TVertex Name { get; set; }
        public Dictionary<Vertex<TVertex, TWeight>, TWeight> AdjacentVertexes { get; set; }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }

    public class Graph<TVertex, TWeight>
        where TWeight : struct, IComparable
        where TVertex : notnull
    {
        public Graph(TVertex[] vertexNames, TWeight[,] weights)
        {
            if (weights.GetLength(0) != weights.GetLength(1) || vertexNames.Length != weights.GetLength(0) + 1)
            {
                throw new IndexOutOfRangeException("Invalid arrays size.");
            }

            Vertexes = new(vertexNames.Length);


            for (int i = 0; i < vertexNames.Length; i++)
            {
                var vertex = new Vertex<TVertex, TWeight>
                {
                    Name = vertexNames[i],
                    AdjacentVertexes = new()
                };

                for (int j = 1; j < weights.GetLength(0); i++)
                {
                    if (weights[i, j].CompareTo(default(TWeight)) != 0)
                    {
                        vertex.AdjacentVertexes.Add(vertexNames)
                    }
                }

                Vertexes.Add(vertex);

            }
        }

        public HashSet<Vertex<TVertex, TWeight>> Vertexes { get; set; }
    }
}