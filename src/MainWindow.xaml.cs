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
using Color = Microsoft.Msagl.Drawing.Color;

namespace BaconPancakes
{
    public partial class MainWindow : Window
    {
        /*** Atribut ***/
        private UndirectedGraph UG;
        private String Src;
        private String Prev_Src;
        private String Dest;
        private String Prev_Dest;
        private Graph G;

        /*** Metode ***/
        public System.Windows.Forms.ComboBox.ObjectCollection Items { get; }
        string nl = "\r\n";
        string tab = "    ";

        public MainWindow()
        {
            InitializeComponent();
            CenterWindowOnScreen();
        }

        /**
         * Metode untuk meletakkan jendela di tengah layar
         */
        private void CenterWindowOnScreen()
        {
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            double windowWidth = Width;
            double windowHeight = Height;
            Left = (screenWidth / 2) - (windowWidth / 2);
            Top = (screenHeight / 2) - (windowHeight / 2);
        }

        /**
         * Metode (Event Handler) untuk menampilkan hasil searching
         * sesuai dengan semua masukan
         */
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            // Jika tidak ada file yang di-submit maka metode akan berhenti
            if (UG == null || Node_Src.SelectedItem == null)
            {
                Result.Text = "Invalid file or input";
                return;
            }

            // Mencetak hasil
            var Instance = new Search();
            List<String> Res;

            Result.Text = "Selected account(s) ";
            if (Src != Dest)
            {
                Result.Text += Src + " and " + Dest + "." + nl;
            }
            else
            {
                Result.Text += Src + "." + nl;
            }

            if (Src != Dest)
            {
                if (DFS_RB.IsChecked == true && Dest != null)
                {
                    try
                    {
                        Res = Instance.DFS(UG, Src, Dest);
                        PrintingPath(Res);
                        ColoringGraph(Res, G);
                    }
                    catch
                    {
                        Console.WriteLine("DFS broke");
                        Result.Text += tab + "No available connection." + nl;
                    }

                }
                else
                {
                    try
                    {
                        Res = Instance.BFS(UG, Src, Dest);
                        PrintingPath(Res);
                        ColoringGraph(Res, G);
                    }
                    catch
                    {
                        Console.WriteLine("BFS broke");
                        Result.Text += tab + "No available connection." + nl;
                    }
                }
            }

            // Mencetak hasil rekomendasi teman
            if (Dest != null)
            {
                FriendsRecommendation();
            }
        }

        /**
         * Metode untuk mencetak hasil searching
         */
        private void PrintingPath(List<String> Res)
        {
            Result.Text += tab;
            if (Res.Count - 2 == 0)
            {
                Result.Text += "0";
            }
            else if (Res.Count - 2 == 1)
            {
                Result.Text += "1st";
            }
            else if (Res.Count - 2 == 2)
            {
                Result.Text += "2nd";
            }
            else if (Res.Count - 2 == 3)
            {
                Result.Text += "3rd";
            }
            else
            {
                Result.Text += (Res.Count - 2) + "th";
            }
            Result.Text += " degree connection." + nl;
            Result.Text += tab + "Connection(s): ";
            for (int i = 0; i < Res.Count; i++)
            {
                Result.Text += Res[i] + " ";
                if (i != Res.Count - 1)
                {
                    Result.Text += "→ ";
                }
                else
                {
                    Result.Text += nl;
                }
            }
        }

        /**
         * Metode untuk mencetak rekomendasi teman
         */
        private void FriendsRecommendation()
        {
            var Instance = new Search();
            List<friendRec> RecsOfFriends;
            RecsOfFriends = Instance.recFriends(UG, Src);

            Result.Text += nl + "Friend recommendation(s) for account " + Src + ":" + nl;
            if (RecsOfFriends.Count != 0) {
                foreach (friendRec friend in RecsOfFriends)
                {
                    Result.Text += tab + "Account: " + friend.GetName();
                    Result.Text += ", " + friend.GetTotalMutual() + " mutual friend(s): ";
                    foreach (string mutualName in friend.GetMutualFriends)
                    {
                        Result.Text += nl + tab + tab + mutualName;
                    }
                    Result.Text += nl;
                }
                return;
            }
            Result.Text += tab + "No friend recommendation." + nl;
        }

        /**
         * Metode untuk mewarnai graf
         */
        private void ColoringGraph(List<String> SearchResult, Graph G)
        {
            ClearColor();
            
            // Mewarnai sisi
            for (int i = 0; i < SearchResult.Count - 1; i++)
            {
                foreach (Edge E1 in G.Edges)
                {
                    if (E1.Source == SearchResult[i] && E1.Target == SearchResult[i + 1] ||
                        E1.Target == SearchResult[i] && E1.Source == SearchResult[i + 1])
                    {
                        E1.Attr.Color = Color.Peru;
                    }
                }
            }

            // Mewarnai simpul
            for (int i = 1; i < SearchResult.Count - 1; i++)
            {
                foreach (MSAGLNode N1 in G.Nodes)
                {
                    if (N1.Id == SearchResult[i])
                    {
                        N1.Attr.Color = Color.SaddleBrown;
                        N1.Attr.FillColor = Color.Moccasin;
                    }
                }
            }

            ColorNode(Src, "Goldenrod");
            ColorNode(Dest, "Goldenrod");
            this.gViewer.Graph = G;
        }

        /**
         * Metode untuk membersihkan warna pada graph
         */
        private void ClearColor()
        {
            // Membersihkan warna
            foreach (Edge E1 in G.Edges)
            {
                E1.Attr.Color = Color.Black;
            }

            foreach (MSAGLNode N1 in G.Nodes)
            {
                N1.Attr.Color = Color.Black;
                N1.Attr.FillColor = Color.White;
            }
            this.gViewer.Graph = G;
        }

        /**
         * Metode (Event Handler) untuk menelusuri file
         */
        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            // Mengosongkan TextBox Result
            Result.Text = "";

            // Membuka browser
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

            // Parsing
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
                    File_Name.Content = "File not read!";
                    return;
                }
            }

            // Mengisi item ComboBox
            Node_Src.Items.Clear();
            Node_Dest.Items.Clear();

            foreach (Node n in UG.GetNodes())
            {
                Node_Src.Items.Add(n.GetNode1());
                Node_Dest.Items.Add(n.GetNode1());
            }

            G = new Graph("graph");
            MakeGraph();

            Src = null;
            Dest = null;
        }

        /**
         * Metode untuk mewarnai sebuah simpul
         */
        private void ColorNode(String node, string color)
        {
            MSAGLNode Copy = G.FindNode(node);
            if (color == "White")
            {
                Copy.Attr.FillColor = Color.White;
            }
            else if (color == "Goldenrod")
            {
                Copy.Attr.FillColor = Color.Goldenrod;
            }
            this.gViewer.Graph = G;
        }

        /**
         * Metode (Event Handler) untuk memilih simpul asal
         */
        private void Node_Src_Chosen(object sender, EventArgs e)
        {
            if (UG != null)
            {
                ClearColor();
                if (Dest != null)
                {
                    ColorNode(Dest, "Goldenrod");
                }

                if (Src != null) // kalo yang pertama banget
                {
                    Prev_Src = Src;
                }

                Src = (string)Node_Src.SelectedValue;
                if (Prev_Src != null && Prev_Src != Src && Prev_Src != Dest)
                {
                    ColorNode(Prev_Src, "White");
                }

                if (Src != null) // kalo gajadi
                {
                    ColorNode(Src, "Goldenrod");
                }
            }
        }
        
        /**
         * Metode (Event Handler) untuk memilih simpul akhir
         */
        private void Node_Dest_Chosen(object sender, EventArgs e)
        {
            if (UG != null)
            {
                ClearColor();
                if (Src != null)
                {
                    ColorNode(Src, "Goldenrod");
                }
                if (Dest != null) // kalo yang pertama banget
                {
                    Prev_Dest = Dest;
                }

                Dest = (string)Node_Dest.SelectedValue;
                if (Prev_Dest != null && Prev_Dest != Dest && Prev_Dest != Src)
                {
                    ColorNode(Prev_Dest, "White");
                }

                if (Src != null) // kalo gajadi
                {
                    ColorNode(Dest, "Goldenrod");
                }
            }
        }

        /**
         * Metode untuk membuat graph MSAGL
         */
        public void MakeGraph()
        {
            // Menambahkan sisi ke graf
            foreach (Node node in UG.GetNodes())
            {
                if (node.GetAdjacentNodes().Count != 0)
                {
                    foreach (string adjacentNode in node.GetAdjacentNodes())
                    {
                        var Edge = G.AddEdge(node.GetNode1(), adjacentNode);
                        Edge.Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None;
                        Edge.Attr.ArrowheadAtSource = Microsoft.Msagl.Drawing.ArrowStyle.None;
                    }
                }
                else
                {
                    G.AddNode(node.GetNode1());
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
        }
    }
}

