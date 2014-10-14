using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgGraph
{
    public class Edge
    {
        public int Weight { get; set; }
        public Vertex Parent { get; set; }
        public Vertex Child { get; set; }

    }
}
