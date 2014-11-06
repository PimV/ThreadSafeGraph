using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgGraph
{
    public class Graph
    {
        public Vertex Root { get; set; }
        public List<Vertex> Vertices { get; set; }
        public VertexList VertexList { get; set; }
        public List<Vertex> Output { get; set; }

        //private Object lockObj = new Object();



        public Graph()
        {
            Output = new List<Vertex>();
            Vertices = new List<Vertex>();
        }

        public Vertex CreateRoot(String name)
        {
            this.Root = CreateVertex(name);
            return this.Root;
        }

        public Vertex CreateVertex(String name)
        {
            var n = new Vertex(name);
            // this.Vertices.Add(n);
            return n;
        }

        public void InsertLast(Vertex v)
        {
            lock (this.Vertices)
            {
                if (this.Vertices.Count == 0)
                {
                    this.Root = v;
                }
                this.Vertices.Add(v);
            }

        }

        public void LinkVertex(Vertex source, Vertex target, int weight)
        {
            lock (source)
            {
                source.addEdge(target, weight);
            }
        }

        public void RemoveLinksFromVertex(Vertex v)
        {
            if (v == null)
            {
                return;
            }

            lock (v)
            {
                v.Edges.Clear();
            }
        }

        public void RemoveVertex(Vertex v)
        {
            if (v == null)
            {
                return;
            }
            lock (v)
            {
                foreach (Edge e in v.Edges)
                {
                    lock (e)
                    {
                        for (int i = 0; i < e.Child.Edges.Count; i++)
                        {
                            if (e.Child.Edges[i].Child == v)
                            {
                                e.Child.Edges.RemoveAt(i);
                            }
                        }
                    }
                }
            }

            RemoveLinksFromVertex(v);
            lock (this.Vertices)
            {
                this.Vertices.Remove(v);
            }
        }

        public int?[,] CreateAdjMatrix()
        {
            int?[,] adj = new int?[Vertices.Count, Vertices.Count];
            for (int i = 0; i < Vertices.Count; i++)
            {
                Vertex v1 = Vertices[i];
                for (int j = 0; j < Vertices.Count; j++)
                {
                    Vertex v2 = Vertices[j];
                    var edge = v1.Edges.FirstOrDefault(a => a.Child == v2);
                    if (edge != null)
                    {
                        adj[i, j] = edge.Weight;
                    }
                }
            }
            return adj;
        }


        public IEnumerable<Vertex> DFS()
        {
            var stack = new Stack<Vertex>();
            var visited = new HashSet<Vertex>();
            stack.Push(this.Root);

            while (stack.Count != 0)
            {
                var current = stack.Pop();

                if (!visited.Add(current))
                {
                    continue;
                }

                yield return current;

                current.Visited = true;
                foreach (Edge e in current.Edges)
                {
                    if (!visited.Contains(e.Parent))
                    {
                        stack.Push(e.Parent);
                    }

                    if (!visited.Contains(e.Child))
                    {
                        stack.Push(e.Child);
                    }
                }
            }
        }

        public void actionBefore(Vertex v)
        {

        }

        public void actionAfter(Vertex v)
        {

        }




    }
}
