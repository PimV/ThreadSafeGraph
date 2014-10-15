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
            this.mainGrid.MouseDown += new MouseButtonEventHandler(OnLeftDown);
            this.KeyDown += new KeyEventHandler(KeyboardHandling);

            initiateGraph();


            // setupDFS();

            //printDFSOutput();

            checkThreadSafety();
            //graph.DFS(new Stack<Vertex>());
        }

        public void checkThreadSafety()
        {
            Thread t10 = new Thread(new ThreadStart(() =>
            {
                searchVertexByName("B");
            }));
            Thread t11 = new Thread(new ThreadStart(() =>
            {
                searchVertexByName("B");
            }));
            Thread t12 = new Thread(new ThreadStart(() =>
            {
                searchVertexByName("B");
            }));

            Thread t20 = new Thread(new ThreadStart(() =>
            {
                Vertex bVertex = searchVertexByName("B");
                if (bVertex != null)
                {
                    Console.WriteLine("Setting " + bVertex.Name + "'s weight to 5 (current value: " + bVertex.Edges[0].Weight + ").");
                    bVertex.Edges[0].Weight = 5;
                    Console.WriteLine(bVertex.Name + "'s weight set to 5 (current value: " + bVertex.Edges[0].Weight + ").");
                }
            }));

            Thread t30 = new Thread(new ThreadStart(() =>
            {
                Console.WriteLine("Removing B from the graph (vertex-count: " + graph.Vertices.Count + ").");
                Vertex bVertex = searchVertexByName("B");
                if (bVertex == null)
                {
                    Console.WriteLine("B failed to remove");
                }
                else
                {
                    graph.RemoveVertex(bVertex);
                }

                Console.WriteLine("Removed B from the graph (vertex-count: " + graph.Vertices.Count + ").");
            }));
            //Start threads just DFS'ing
            t10.Start();
            t11.Start();
            t12.Start();

            //Start threads altering vertices/edges
            t20.Start();

            //Start threads removing vertices/edges
            t30.Start();

            printGraph();
        }

        public Vertex searchVertexByName(String name)
        {
            Vertex result = graph.DFS().Where(a => a.Name == name).FirstOrDefault();
            return result;
        }

        public void printGraph()
        {
            for (int x = 0; x < graph.Vertices.Count; x++)
            {
                Console.WriteLine(graph.Vertices[x].Name + "-" + graph.Vertices[x].Edges[0].Weight);
            }
            Console.WriteLine("------------------------");
            Console.WriteLine();
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

        private void RunOnMouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void OnLeftDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                Console.WriteLine("left clicked");
            }
        }

        public void KeyboardHandling(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                Console.WriteLine(graph.Vertices.Count);
            }

            if (e.Key == Key.B)
            {
                printGraph();
            }

            if (e.Key == Key.S)
            {
                Console.WriteLine(searchVertexByName("B").Name);
            }
        }

    }
}
