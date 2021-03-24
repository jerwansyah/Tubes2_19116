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
        private String Dest;

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
            if (UG == null || Node_Src.SelectedItem == null || Node_Dest.SelectedItem == null)
            {
                Result.Text = "Invalid file or input";
                return;
            }

            // Mengambil nilai-nilai simpul sumber dan tujuan
            Src = (string)Node_Src.SelectedValue;
            Dest = (string)Node_Dest.SelectedValue;

            // Membuat graf
            Graph G = new Graph("graph");

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

            // Mewarnai simpul awal dan akhir
            MSAGLNode Src_Copy = G.FindNode(Src);
            MSAGLNode Dest_Copy = G.FindNode(Dest);

            Src_Copy.Attr.FillColor = Color.Goldenrod;
            Dest_Copy.Attr.FillColor = Color.Goldenrod;

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

            if (DFS_RB.IsChecked == true)
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

            // Mencetak hasil rekomendasi teman
            FriendsRecommendation();
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
                        Result.Text += mutualName+ " ";
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
                }
            }

            // Mengisi item ComboBox
            if (UG != null)
            {
                Node_Src.Items.Clear();
                Node_Dest.Items.Clear();

                foreach (Node n in UG.GetNodes())
                {
                    Node_Src.Items.Add(n.GetNode1());
                    Node_Dest.Items.Add(n.GetNode1());
                }
            }
            else
            {
                File_Name.Content = "File not read!";
            }
        }

        private void WFH_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }
    }
}

