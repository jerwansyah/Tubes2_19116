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
                currentPath = new List<Node>(penyimpan_path.Dequeue());
                
                currentNode = currentPath.Last();

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

                        penyimpan_path.Enqueue(new_path);
                    }
                }
            }
            return null;
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
                    foreach (string nodeName in adjacentNodes.ToList().AsEnumerable().Reverse()) {
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
                            if (currNode.GetNode1() == end) break;
                            stack.Pop();
                            List<string> adjacentNodes = currNode.GetAdjacentNodes();
                            foreach (string nodeName in adjacentNodes.ToList().AsEnumerable().Reverse()) {
                                if (!visited.Contains(nodeName) && !path.Contains(nodeName)) {
                                stack.Push(nodeName);
                            }
                        }
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
                    currNode = graph_in.GetNodeOf(path[path.Count-1]);
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

    }
}
