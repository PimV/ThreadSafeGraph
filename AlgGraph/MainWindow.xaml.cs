using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AlgGraph
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Graph graph;

        public MainWindow()
        {
            InitializeComponent();

            initiateGraph();


            // setupDFS();

            //printDFSOutput();

            checkThreadSafety();
            //graph.DFS(new Stack<Vertex>());
        }

        public void checkThreadSafety()
        {
            Thread t1 = new Thread(new ThreadStart(() =>
            {
                setupDFS();
                printDFSOutput();
            }));

            Thread t2 = new Thread(new ThreadStart(() =>
            {
                //setupDFS();
               // printDFSOutput();
            }));

            Thread t3 = new Thread(new ThreadStart(() =>
            {
                //setupDFS();
                //printDFSOutput();
            }));

            t1.Start();
            t2.Start();
            t3.Start();
        }

        public void setupDFS()
        {
            Stack<Vertex> stack = new Stack<Vertex>();
            foreach (Vertex v in graph.Vertices)
            {
                Console.WriteLine("Pushing " + v.Name);
                stack.Push(v);
                graph.DFS(stack);
            }
        }

        public void printDFSOutput()
        {
            for (int x = 0; x < graph.Output.Count; x++)
            {
                Console.WriteLine(graph.Output[x].Name);
            }
        }


        public void initiateGraph()
        {
            graph = new Graph();

            var a = graph.CreateRoot("A");
            var b = graph.CreateVertex("B");
            var c = graph.CreateVertex("C");
            var d = graph.CreateVertex("D");
            var e = graph.CreateVertex("E");
            var f = graph.CreateVertex("F");
            var g = graph.CreateVertex("G");
            var h = graph.CreateVertex("H");
            var i = graph.CreateVertex("I");
            var j = graph.CreateVertex("J");
            var k = graph.CreateVertex("K");
            var l = graph.CreateVertex("L");
            var m = graph.CreateVertex("M");
            var n = graph.CreateVertex("N");
            var o = graph.CreateVertex("O");
            var p = graph.CreateVertex("P");

            a.addEdge(b, 1)
             .addEdge(c, 1);

            b.addEdge(e, 1)
             .addEdge(d, 3);

            c.addEdge(f, 1)
             .addEdge(d, 3);

            c.addEdge(f, 1)
             .addEdge(d, 3);

            d.addEdge(h, 8);

            e.addEdge(g, 1)
             .addEdge(h, 3);

            f.addEdge(h, 3)
             .addEdge(i, 1);

            g.addEdge(j, 3)
             .addEdge(l, 1);

            h.addEdge(j, 8)
             .addEdge(k, 8)
             .addEdge(m, 3);

            i.addEdge(k, 3)
             .addEdge(n, 1);

            j.addEdge(o, 3);

            k.addEdge(p, 3);

            l.addEdge(o, 1);

            m.addEdge(o, 1)
             .addEdge(p, 1);

            n.addEdge(p, 1);

            // o - Already added

            // p - Already added

            int?[,] adj = graph.CreateAdjMatrix(); // We're going to implement that down below

            PrintMatrix(ref adj, graph.Vertices.Count); // We're going to implement that down below
        }

        private static void PrintMatrix(ref int?[,] matrix, int Count)
        {
            Console.Write("       ");
            for (int i = 0; i < Count; i++)
            {
                Console.Write("{0}  ", (char)('A' + i));
            }

            Console.WriteLine();

            for (int i = 0; i < Count; i++)
            {
                Console.Write("{0} | [ ", (char)('A' + i));

                for (int j = 0; j < Count; j++)
                {
                    if (i == j)
                    {
                        Console.Write(" &,");
                    }
                    else if (matrix[i, j] == null)
                    {
                        Console.Write(" .,");
                    }
                    else
                    {
                        Console.Write(" {0},", matrix[i, j]);
                    }

                }
                Console.Write(" ]\r\n");
            }
            Console.Write("\r\n");
        }

    }
}
