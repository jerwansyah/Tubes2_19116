using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaconPancakes
{
    public class Search
    {

        public List<string> BFS(UndirectedGraph graph_in, string start, string end)
        {
            // Kalau ga ada, return
            if (!graph_in.IsNodeExist(start) || graph_in.IsNodeExist(end)) {
                return null;
            }
            
            // Inisialisasi
            List<string> path = new List<string>();
            Node startNode = graph_in.GetNodeOf(start);
            List<Node> antrian_node = new List<Node>();
            antrian_node.Add(startNode);
            bool[] dikunjungi_array = new bool[graph_in.Count];
            
            Node currentNode;
            while (antrian_node.Count > 0) {

                // Pop to path and currentNode
                currentNode = antrian_node.First();
                path.Add(currentNode.GetNode1());

                antrian_node.Remove(antrian_node.First());

                if (currentNode.GetNode1() == end) {
                    return path;
                }

                foreach (Node adjacentNode in graph_in.GetAdjacentNodes(currentNode)) {
                    List<string> new_path = new List<string>();
                    new_path.Add(adjacentNode.GetNode1());

                    antrian_node.Add(adjacentNode);

                    // int nodeIndex = graph_in.getIndexOf(adjacentNode);
                    // // jika node di index tersebut belum dikunjungi
                    // if (!dikunjungi_array[nodeIndex]) {
                    //     // tambahkan tetangga itu ke antrian_Node paling belakang
                    //     antrian_node.Add(adjacentNode);

                    //     // set sudah dikunjungi
                    //     dikunjungi_array[nodeIndex] = true;
                    // }

                }
            }
            return null;
        }

        public void printStack(Stack<string> haha) {
            foreach(string hehe in haha) {
                Console.Write(hehe + " ");
            }
            Console.WriteLine();
        }

        public void printPath(List<string> asu) {
            Console.WriteLine("printpath ini");
            foreach (string babi in asu) {
                Console.Write(babi + " ");
            }
            Console.WriteLine();
        }

        // Use exception handling for no connected friends
        public List<string> DFS(UndirectedGraph graph_in, string start, string end) {
            // Kalau ga ada, gon
            if (!graph_in.IsNodeExist(start) || !graph_in.IsNodeExist(end)) {
                return null;
            }

            // Inisialisasi
            List<string> path = new List<string>();
            Node nodeStart = graph_in.GetNodeOf(start);
            Stack<string> stack = new Stack<string>();
            List<string> visited = new List<string>();

            visited.Add(start);
            bool backtrack = false;
            stack.Push(start);
            Node currNode = nodeStart;
            while (currNode.GetNode1() != end) {
                // kasus kalo sebelumnya bukan backtrack
                if (!backtrack) {
                    stack.Pop();
                    List<string> adjacentNodes = currNode.getAdjacentNodes();
                    adjacentNodes.Reverse();
                    foreach (string nodeName in adjacentNodes) {
                        if (!visited.Contains(nodeName) && !path.Contains(nodeName)) {
                            stack.Push(nodeName);
                        }
                    }
                }
                else {
                    // kalo ternyata currNode = di last element di path + currNode emang tetangga stack.top()
                    // example: path = A B C F
                    //          stack.top() = E (ceritanya F sama E tetangga)
                    //          currNode = F
                    if (currNode.getNode1() == path[path.Count-1] && currNode.IsAdjacent(stack.Peek())) {
                            currNode = graph_in.getNodeOf(stack.Peek());
                            stack.Pop();
                    }
                }
                
                // Buat ngecek butuh backtrack atau ga
                if (currNode.IsAdjacent(stack.Peek())) {
                    backtrack = false;
                    path.Add(currNode.getNode1());
                    currNode = graph_in.getNodeOf(stack.Peek());
                }
                else {
                    backtrack = true;
                    visited.Add(currNode.getNode1());
                    path.Remove(currNode.getNode1());

                    // Kalo currNode ga tetanggaan sama stack.Top(), currNode = last element of path
                    if (!currNode.IsAdjacent(stack.Peek())) {
                        currNode = graph_in.getNodeOf(path[path.Count-1]);
                    }
                    else {
                        currNode = graph_in.getNodeOf(stack.Peek());
                    }
                }
            }
            // Sebenernya buat nambah end aja di path
            path.Add(currNode.getNode1());
            return path;
        }

        public List<friendRec> recFriends(UndirectedGraph graph_in, string start) {
            // Inisialisasi
            Node orang = graph_in.getNodeOf(start);
            List<Node> temen = graph_in.getAdjacentNodes(orang);
            List<friendRec> recommended = new List<friendRec>();
            List<string> RecFrens = new List<string>();
            
            // Didata dulu siapa yang bisa jadi recommended friends
            foreach (Node namaTemen in temen) {
                foreach (string name in namaTemen.getAdjacentNodes()) {
                    if (!RecFrens.Contains(name) && name != start && !orang.IsAdjacent(name)) {
                        RecFrens.Add(name);
                        recommended.Add(new friendRec(name));
                    }
                }
            }
            
            // Cari tau mutual friends siapa aja
            foreach (Node namaTemen in temen) {
                foreach (friendRec temenRekomen in recommended) {
                    if (namaTemen.IsAdjacent(temenRekomen.GetName())) {
                        temenRekomen.AddMutualFriend(namaTemen.GetNode1());
                    }
                }
            }

            // Ini urutin berdasarkan mutual friends yeak
            return recommended.OrderByDescending(o => o.GetTotalMutual()).ToList();
        }

        // static void Main(string[] args) {
        //     // ini buat testing aja
        //     UndirectedGraph g = new UndirectedGraph();
        //     g.AddEdge("A", "B");
        //     g.AddEdge("A", "C");
        //     g.AddEdge("A", "D");
        //     g.AddEdge("B", "C");
        //     g.AddEdge("B", "E");
        //     g.AddEdge("B", "F");
        //     g.AddEdge("C", "F");
        //     g.AddEdge("C", "G");
        //     g.AddEdge("D", "G");
        //     g.AddEdge("D", "F");
        //     g.AddEdge("E", "H");
        //     g.AddEdge("E", "F");
        //     g.AddEdge("F", "H");

        //     var instance = new Search();

            // Node jir = g.getNodeOf("A");
            // List<Node> hehe = g.getAdjacentNodes(jir);
            // foreach (Node anjir in hehe) {
            //     Console.WriteLine(anjir.getNode1());
            // }

            // List<friendRec> tes = instance.recFriends(g, "A");
            // foreach (friendRec abc in tes) {
            //     abc.print();
            // }
            // g.AddEdge("I", "J");
            // try {
            //     List<string> tes = instance.DFS(g, "A", "J");
            //     foreach (string abc in tes) {
            //         Console.WriteLine(abc);
            //     }
            // }
            // catch {
            //     Console.WriteLine("nuh");
            // }
        }
    }

}
