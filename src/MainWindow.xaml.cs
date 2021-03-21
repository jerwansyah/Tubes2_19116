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
using Graph = Microsoft.Msagl.Drawing.Graph;
using Edge = Microsoft.Msagl.Drawing.Edge;
using MSAGLNode = Microsoft.Msagl.Drawing.Node;

namespace BaconPancakes
{
    public partial class MainWindow : Window
    {
        // Atribut
        private UndirectedGraph UG;
        private Node Src;
        private Node Dest;

        // Metode
        public System.Windows.Forms.ComboBox.ObjectCollection Items { get; }

        public MainWindow()
        {
            InitializeComponent();
            CenterWindowOnScreen();
            //Node_Src.Text = "Choose a node...";
            //Node_Dest.Text = "Choose a node...";
        }

        private void CenterWindowOnScreen()
        {
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            double windowWidth = Width;
            double windowHeight = Height;
            Left = (screenWidth / 2) - (windowWidth / 2);
            Top = (screenHeight / 2) - (windowHeight / 2);
        }

        //    graph.AddEdge("A", "C").Attr.Color = Microsoft.Msagl.Drawing.Color.Green;
        //    graph.FindNode("A").Attr.FillColor = Microsoft.Msagl.Drawing.Color.Magenta;
        //    graph.FindNode("B").Attr.FillColor = Microsoft.Msagl.Drawing.Color.MistyRose;
        //    Microsoft.Msagl.Drawing.Node c = graph.FindNode("C");
        //    c.Attr.FillColor = Microsoft.Msagl.Drawing.Color.PaleGreen;

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            // Jika tidak ada file yang di-submit
            if (UG == null || Node_Src.SelectedItem == null || Node_Dest.SelectedItem == null)
            {
                Result.Text = "Invalid file or input";
                return;
            }

            // Membuat graf
            Graph G = new Graph("graph");

            // Menambahkan sisi ke graf
            foreach (Node node in UG.GetNodes())
            {
                foreach (string adjacentNode in node.GetAdjacentNodes())
                {
                    {
                        var Edge = G.AddEdge(node.GetNode1(), adjacentNode);
                        Edge.Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None;
                        Edge.Attr.ArrowheadAtSource = Microsoft.Msagl.Drawing.ArrowStyle.None;
                    }
                }
            }

            // Membuang sisi yang ganda antarsimpul
            foreach (Edge E1 in G.Edges)
            {
                var src = E1.Source;
                var dest = E1.Target;

                foreach (Edge E2 in G.Edges)
                {
                    if (E2.Source == dest && E2.Target == src)
                    {
                        G.RemoveEdge(E2);
                    }
                }
            }
            this.gViewer.Graph = G;

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
            Result.Text = "";
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
                    UG = fileParse.ListToUndirectedGraph(fileParse.FilenameToList(fileName));
                }
                catch (FileFormatException err)
                {
                    Console.WriteLine(err.Message);
                }
            }

            if (UG != null)
            {
                Node_Src.Items.Clear();
                Node_Dest.Items.Clear();

                foreach (Node n in UG.GetNodes())
                {
                    Node_Src.Items.Add(n.GetNode1());
                    Node_Dest.Items.Add(n.GetNode1());
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

