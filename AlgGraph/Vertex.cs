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
            Edges.Add(new Edge
            {
                Parent = this,
                Child = child,
                Weight = w
            });
            if (!child.Edges.Exists(a => a.Parent == child && a.Child == this))
            {
                child.addEdge(this, w);
            }
            return this;
        }

    }
}
