using System;
using System.Collections.Generic;
using System.Text;

namespace BaconPancakes
{
    public class FileFormatException : Exception
    {
        public FileFormatException(string message) : base(message)
        {
            //NOP
        }
    }
    public class FileParser
    {
        public List<string[]> FilenameToList(String filename)
        {
            Console.WriteLine("Reading " + filename);

            string[] text;
            try
            {
                text = System.IO.File.ReadAllLines(filename);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            int numberOfEdge;
            bool isNumeric = int.TryParse(text[0], out numberOfEdge);
            if (!isNumeric || numberOfEdge == 0)
            {
                throw new FileFormatException("[" + (1) + "]Zero edge or no number of edge declared.\n" +
                    "Please declare the number of edge (> 0) in the first line of input file.");
            }

            List<string[]> list_hasil = new List<string[]>();
            for (int i = 1; i <= numberOfEdge && text[i] != null; i += 1)
            {
                string[] x = text[i].Split(' ');
                Console.WriteLine(x[0]);
                if (x.Length > 2)
                {
                    throw new FileFormatException("[" + (i + 1) + "] An edge connects more than two nodes.");
                }
                else
                {
                    list_hasil.Add(x);
                }
            }


            return list_hasil;
        }

        public UndirectedGraph ListToUndirectedGraph(List<string[]> list_hasil)
        {
            UndirectedGraph undirected_graph = new UndirectedGraph();
            foreach (string[] x in list_hasil)
            {
                if (x.Length == 1) {
                    undirected_graph.AddNode(x[0]);
                } else {
                    undirected_graph.AddEdge(x[0], x[1]);
                }
            }
            return undirected_graph;
        }

        // TO SEE THE FileParser x UndirectedGraph UNCOMMENT THIS (Block Them, Ctrl + Shift + /)
        // static void Main(String[] args)
        // {
        //     FileParser fileParse = new FileParser();
        //     UndirectedGraph undirectedGraph = new UndirectedGraph();

        //     string filename = "..\\..\\test\\test1.txt";
        //     try
        //     {
        //         undirectedGraph = fileParse.ListToUndirectedGraph(fileParse.FilenameToList(filename));
        //     }
        //     catch (FileFormatException e)
        //     {
        //         Console.WriteLine(e.Message);
        //         return;
        //     }
        //     finally
        //     {
        //         undirectedGraph.print();
        //         List<string> hasil1 = new List<string>();
        //         List<string> hasil2 = new List<string>();
        //         Search search = new Search();
        //         hasil1 = search.DFS(undirectedGraph, "A", "F");
        //         hasil2 = search.BFS(undirectedGraph, "A", "F");

        //         if (hasil1 == null || hasil2 == null)
        //         {
        //             Console.WriteLine("No path found");

        //         } else {
        //             Console.WriteLine("\n Hasil BFS");
        //             foreach (string au in hasil1)
        //             {
        //                 Console.WriteLine(au);
        //             }

        //             Console.WriteLine("\n Hasil DFS");

        //             foreach (string au in hasil2)
        //             {
        //                 Console.WriteLine(au);
        //             }
        //         }
        //     }
        // }
    }
}
