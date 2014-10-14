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


        public void DFS(Stack<Vertex> stack)
        {
            while (stack.Count > 0)
            {
                Vertex v = stack.Pop();

                //Actie voor             
                if (!v.Visited)
                {
                    Console.WriteLine("Visiting " + v.Name);
                    Output.Add(v);
                    v.Visited = true;
                    foreach (Edge e in v.Edges)
                    {
                        Console.WriteLine("Pushed " + e.Parent.Name);
                        Console.WriteLine("Pushed " + e.Child.Name);
                        stack.Push(e.Parent);
                        stack.Push(e.Child);
                        DFS(stack);
                    }
                }
                else
                {
                    Console.WriteLine("Skipping " + v.Name);
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
