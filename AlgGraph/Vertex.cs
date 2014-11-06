using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgGraph
{
    public class Vertex
    {
        public String Name { get; set; }
        public List<Edge> Edges = new List<Edge>();
        public Boolean Visited { get; set; }

        public Vertex(String name)
        {
            this.Visited = false;
            this.Name = name;
        }

        public Vertex addEdge(Vertex child, int w)
        {
            lock (this)
            {
                Edges.Add(new Edge
                {
                    Parent = this,
                    Child = child,
                    Weight = w
                });
                lock (child)
                {
                    if (!child.Edges.Exists(a => a.Parent == child && a.Child == this))
                    {
                        child.addEdge(this, w);
                    }
                }
            }
            return this;
        }

        public void setEdgeWeight(Vertex child, int weight)
        {
            lock (this)
            {
                foreach (Edge e in this.Edges)
                {
                    if (e.Child == child)
                    {
                        Console.WriteLine("Prev weight and new weight: " + e.Weight + "-" + weight);
                        e.Weight = weight;
                        foreach (Edge e2 in e.Child.Edges)
                        {
                            if (e2.Child == this)
                            {
                                e2.Weight = weight;
                            }
                        }
                    }
                }
            }
        }
    }
}
