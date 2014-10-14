using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgGraph
{
    public class VertexList : IEnumerable
    {
        private Hashtable data = new Hashtable();

        public virtual void add(Vertex v)
        {
            data.Add(v.Name, v);
        }

        public virtual void Remove(Vertex v)
        {
            data.Remove(v.Name);
        }

        public virtual bool ContainsKey(String key)
        {
            return data.ContainsKey(key);
        }

        public virtual void Clear()
        {
            this.data.Clear();
        }

        public virtual Vertex this[string key] { get { return (Vertex)data[key]; } }


        public IEnumerator GetEnumerator()
        {
            return data.GetEnumerator();
        }
    }
}
