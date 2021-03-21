using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Windows.Forms;
using Microsoft.Msagl;
using Microsoft.Msagl.Splines;
// using BaconPancakes.lib;

namespace BaconPancakes
{
    public partial class MainWindow : Window
    {
        // Atribut
        private UndirectedGraph graph;

        // Metode
        public System.Windows.Forms.ComboBox.ObjectCollection Items { get; }

        public MainWindow()
        {
            InitializeComponent();
            CenterWindowOnScreen();
        }

        private void CenterWindowOnScreen()
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }

        //    graph.AddEdge("A", "B");
        //    graph.AddEdge("B", "C");
        //    graph.AddEdge("A", "C").Attr.Color = Microsoft.Msagl.Drawing.Color.Green;
        //    graph.FindNode("A").Attr.FillColor = Microsoft.Msagl.Drawing.Color.Magenta;
        //    graph.FindNode("B").Attr.FillColor = Microsoft.Msagl.Drawing.Color.MistyRose;
        //    Microsoft.Msagl.Drawing.Node c = graph.FindNode("C");
        //    c.Attr.FillColor = Microsoft.Msagl.Drawing.Color.PaleGreen;
        //    c.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Diamond;

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Msagl.Drawing.Graph g = new Microsoft.Msagl.Drawing.Graph("graph");
            g.AddEdge("D", "A");
            g.AddEdge("C", "B");
            g.AddEdge("F", "C");
            g.AddEdge("B", "D");
            g.AddEdge("D", "A");
            this.gViewer.Graph = g;


            if (DFS.IsChecked == true)
            {
                string nl = "\r\n";
                Result.Text = "to yeet" + nl;
                Result.Text += "or not to yeet";
            }
            else
            {
                Result.Text = "";
            }
        }

        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"",
                Title = "Browse Text Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "txt",
                Filter = "txt files (*.txt)|*.txt",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileName = openFileDialog1.FileName;
                File_Name.Content = openFileDialog1.SafeFileName;
                FileParser fileParse = new FileParser();

                try
                {
                    graph = fileParse.ListToUndirectedGraph(fileParse.FilenameToList(fileName));
                }
                catch (FileFormatException err)
                {
                    Console.WriteLine(err.Message);
                }
            }

            if (graph != null)
            {
                foreach (Node n in graph.GetNodes())
                {
                    Node_Alpha.Items.Add(n.getNode1());
                    Node_Omega.Items.Add(n.getNode1());
                }
                return;
            }
            else
            {
                File_Name.Content = "File not read!";
            }
        }
    }
}

