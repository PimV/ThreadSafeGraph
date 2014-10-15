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
            this.Vertices.Add(n);
            return n;
        }

        public void RemoveVertex(Vertex v)
        {
            v = this.Vertices[this.Vertices.IndexOf(v)];

            foreach (Edge e in v.Edges)
            {
                if (e.Parent == v)
                {
                    Console.WriteLine("Removing parent");
                    e.Parent = null;
                }

                if (e.Child == v)
                {
                    Console.WriteLine("Removing Child");
                    e.Child = null;
                }
            }

            this.Vertices.Remove(v);
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
