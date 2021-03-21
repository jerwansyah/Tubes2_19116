using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaconPancakes
{
    public class Search
    {
        private List<string> NodeListToStringList(List<Node> list_node) {
            List<string> list_hasil = new List<string>();
            foreach (Node node in list_node) {
                list_hasil.Add(node.GetNode1());
            }
            return list_hasil;
        }

        private void CopyNodeList(List<Node> list_in, List<Node> list_out) {
            foreach (Node item in list_in) {
                list_out.Add(item);
            }
        }

        public List<string> BFS(UndirectedGraph graph_in, string start, string end)
        {
            // Kalau ga ada, return
            if (!graph_in.IsNodeExist(start) || !graph_in.IsNodeExist(end)) {
                return null;
            }
            
            // Inisialisasi
            Queue<List<Node>> penyimpan_path = new Queue<List<Node>>();
            List<Node> currentPath = new List<Node>();

            Node currentNode = graph_in.GetNodeOf(start);

            currentPath.Add(currentNode);
            penyimpan_path.Enqueue(currentPath);
            
            int i = 0;
            while (penyimpan_path.Count > 0) {
                i += 1;                
                Console.WriteLine("Iteration " + i + " started");
                // CopyNodeList(penyimpan_path.Dequeue(), currentPath);
                currentPath = new List<Node>(penyimpan_path.Dequeue());

                foreach (Node step in currentPath) {
                    Console.WriteLine(step.GetNode1());
                }
                
                currentNode = currentPath.Last();
                Console.WriteLine("Current node : " + currentNode.GetNode1());

                if (currentNode.GetNode1() == end) {
                    return NodeListToStringList(currentPath);
                }

                foreach (Node adjacentNode in graph_in.GetAdjacentNodes(currentNode)) {
                    if (currentNode.GetNode1() == end) {
                        return NodeListToStringList(currentPath);
                    }

                    if (!currentPath.Contains(adjacentNode)) {
                        List<Node> new_path = new List<Node>();
                        new_path.Clear();
                        new_path.AddRange(currentPath);
                        new_path.Add(adjacentNode);

                        Console.WriteLine("\nWhat does the new path contains eh?");
                        foreach (Node step in new_path) {
                            Console.WriteLine(step.GetNode1());
                        }
                        penyimpan_path.Enqueue(new_path);
                    }
                }
                Console.WriteLine("Iteration " + i + " ended");
                Console.WriteLine("penyimpan_path contains " + penyimpan_path.Count + "\n");
            }
            return null;
        }

        public void printStack(Stack<string> haha) {
            foreach(string hehe in haha) {
                Console.Write(hehe + " ");
            }
            Console.WriteLine();
        }

        public void printPath(List<string> bacon) {
            Console.WriteLine("printpath ini");
            foreach (string pancake in bacon) {
                Console.Write(pancake + " ");
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
            bool lastAdded = false;
            stack.Push(start);
            Node currNode = nodeStart;
            while (currNode.GetNode1() != end) {
                // kasus kalo sebelumnya bukan backtrack
                if (!backtrack) {
                    stack.Pop();
                    List<string> adjacentNodes = currNode.GetAdjacentNodes();
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
                    if (currNode.GetNode1() == path[path.Count-1] && currNode.IsAdjacent(stack.Peek())) {
                            currNode = graph_in.GetNodeOf(stack.Peek());
                            stack.Pop();
                    }
                }
                
                // Buat ngecek butuh backtrack atau ga
                if (currNode.IsAdjacent(stack.Peek())) {
                    backtrack = false;
                    path.Add(currNode.GetNode1());
                    currNode = graph_in.GetNodeOf(stack.Peek());
                }
                else {
                    backtrack = true;
                    visited.Add(currNode.GetNode1());
                    path.Remove(currNode.GetNode1());

                    // Kalo currNode ga tetanggaan sama stack.Top(), currNode = last element of path
                    if (!currNode.IsAdjacent(stack.Peek())) {
                        currNode = graph_in.GetNodeOf(path[path.Count-1]);
                    }
                    else {
                        currNode = graph_in.GetNodeOf(stack.Peek());
                    }
                }
                if (path[path.Count-1] == end) {
                    lastAdded = true;
                    break;
                }
            }
            // Sebenernya buat nambah end aja di path
            if (!lastAdded) {
                path.Add(currNode.GetNode1());
            }
            return path;
        }

        public List<friendRec> recFriends(UndirectedGraph graph_in, string start) {
            // Inisialisasi
            Node orang = graph_in.GetNodeOf(start);
            List<Node> temen = graph_in.GetAdjacentNodes(orang);
            List<friendRec> recommended = new List<friendRec>();
            List<string> RecFrens = new List<string>();
            
            // Didata dulu siapa yang bisa jadi recommended friends
            foreach (Node namaTemen in temen) {
                foreach (string name in namaTemen.GetAdjacentNodes()) {
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
        // }
    }
}
