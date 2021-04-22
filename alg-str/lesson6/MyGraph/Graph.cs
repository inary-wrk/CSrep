using System;
using System.Collections.Generic;


namespace MyGraph
{
    public class Graph<TVertex, TWeight>
        where TWeight : struct, IComparable
        where TVertex : notnull
    {
        public HashSet<Vertex<TVertex, TWeight>> Vertexes { get; set; }

        #region EXCEPTIONS MESSAGES
        internal const string ARGUMENT_NULL = "The argument cannot be null.";
        internal const string VERTEX_ALREADY_EXISTS = "An vertex already exists in the graph.";
        internal const string VERTEX_NOT_EXISTS = "The specified vertex does not exist.";
        internal const string EDGE_ALREADY_EXISTS = "An edge already exists in the graph.";

        #endregion

        #region CTOR
        public Graph()
        {

        }

        public Graph(TVertex[] vertexNames, TWeight?[,] weights)
        {
            if (weights.GetLength(0) != weights.GetLength(1) || vertexNames.Length + 1 != weights.GetLength(0))
            {
                throw new IndexOutOfRangeException("Invalid arrays size.");
            }

            Vertexes = new(vertexNames.Length);


            for (int i = 0; i < vertexNames.Length; i++)
            {
                var vertex = new Vertex<TVertex, TWeight>
                {
                    Name = vertexNames[i],
                    ToThis = new()
                };

                for (int j = 1; j < weights.GetLength(0); i++)
                {
                    if (weights[i + 1, j] is null)
                    {
                        //vertex.From.Add(vertexNames);
                    }
                }
                bool x = false;
                if (x is false)

                    Vertexes.Add(vertex);

            }
        }
        #endregion

        #region ADD
        /// <summary>
        /// Adds a <paramref name="vertex"/> to the graph.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void VertexAdd(TVertex vertex) => VertexAdd(vertex, false);
        /// <summary>
        /// Adds a <paramref name="vertex"/> to the graph.
        /// </summary>
        /// <returns><see langword="true"/> if added, otherwise <see langword="false"/></returns>
        public bool TryVertexAdd(TVertex vertex) => VertexAdd(vertex, true);
        internal bool VertexAdd(TVertex vertex, bool tryAdd)
        {
            if (vertex is null)
            {
                if (tryAdd) return false;
                else throw new ArgumentNullException(nameof(vertex), ARGUMENT_NULL);
            }

            bool success = Vertexes.Add(new Vertex<TVertex, TWeight> { Name = vertex });

            if (!success)
                if (!tryAdd) throw new ArgumentException(VERTEX_ALREADY_EXISTS);

            return success;
        }


        /// <summary>
        /// Adds an edge from vertex <paramref name="from"/> to vertex <paramref name="to"/>, with a weight of <paramref name="weight"/>.
        /// </summary>
        /// <returns><see langword="true"/> if added, otherwise <see langword="false"/></returns>
        public bool TryEdgeAdd(Vertex<TVertex, TWeight> from, Vertex<TVertex, TWeight> to, TWeight weight) =>
            EdgeAdd(from, to, weight, true);

        /// <summary>
        /// Adds an edge from vertex <paramref name="from"/> to vertex <paramref name="to"/>, with a weight of <paramref name="weight"/>.
        /// </summary>
        ///<exception cref="ArgumentNullException"></exception>
        ///<exception cref="ArgumentException"></exception>
        public void EdgeAdd(Vertex<TVertex, TWeight> from, Vertex<TVertex, TWeight> to, TWeight weight) =>
            EdgeAdd(from, to, weight, false);

        internal bool EdgeAdd(Vertex<TVertex, TWeight> from, Vertex<TVertex, TWeight> to, TWeight weight, bool tryAdd)
        {
            if (from is null)
                if (tryAdd) return false;
                else throw new ArgumentNullException(nameof(from), ARGUMENT_NULL);

            if (to is null)
                if (tryAdd) return false;
                else throw new ArgumentNullException(nameof(to), ARGUMENT_NULL);

            if (Vertexes.Contains(from) is false)
                if (tryAdd) return false;
                else throw new ArgumentException(VERTEX_NOT_EXISTS, nameof(from));

            if (Vertexes.Contains(to) is false)
                if (tryAdd) return false;
                else throw new ArgumentException(VERTEX_NOT_EXISTS, nameof(to));


            if (from.FromThis is null) from.FromThis = new();
            bool success = from.FromThis.TryAdd(to, weight);

            if (success)
            {
                if (to.ToThis is null) to.ToThis = new();
                to.ToThis.Add(from);
            }
            else
            {
                if (!tryAdd) throw new ArgumentException(EDGE_ALREADY_EXISTS);
            }

            return success;
        }
        #endregion
        // TODO: check
        public bool ChahgeWeight(Vertex<TVertex, TWeight> from, Vertex<TVertex, TWeight> to, TWeight weight)
        {
            if (from is null || to is null) return false;
            bool logic = from.FromThis?.ContainsKey(to) ?? false;
            if (logic)
            {
                from.FromThis[to] = weight;
            }
            return logic;
        }

        public bool VertexRemove(TVertex vertex)
        {
            if (vertex is null) throw new ArgumentNullException(nameof(vertex), ARGUMENT_NULL);
            return false;
        }

    }
}