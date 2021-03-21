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
            Node startNode = graph_in.getNodeOf(start);
            List<Node> antrian_node = new List<Node>();
            antrian_node.Add(startNode);
            bool[] dikunjungi_array = new bool[graph_in.Count];
            
            Node currentNode;
            while (antrian_node.Count > 0) {

                // Pop to path and currentNode
                currentNode = antrian_node.First();
                path.Add(currentNode.getNode1());

                antrian_node.Remove(antrian_node.First());

                if (currentNode.getNode1() == end) {
                    return path;
                }

                foreach (Node adjacentNode in graph_in.getAdjacentNodes(currentNode)) {
                    List<string> new_path = new List<string>();
                    new_path.Add(adjacentNode.getNode1());

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

        public List<string> DFS(UndirectedGraph graph_in, string start, string end) {
            // Kalau ga ada, gon
            if (!graph_in.IsNodeExist(start) || graph_in.IsNodeExist(end)) {
                return null;
            }

            // Inisialisasi
            List<string> path = new List<string>();
            Node nodeStart = graph_in.getNodeOf(start);
            Stack<string> stack = new Stack<string>();
            List<string> visited = new List<string>();
            // List<Node> listOfNodes = graph_in.GetNodes();

            visited.Add(start);
            bool backtrack = false;
            stack.Push(start);
            Node currNode = nodeStart;
            while (currNode.getNode1() != end) {
                if (!backtrack) {
                    stack.Pop();
                }
                
                // Masukin jalan yang bisa dipake
                List<string> adjacentNodes = currNode.getAdjacentNodes();
                foreach (string nodeName in adjacentNodes) {
                    stack.Push(nodeName);
                }

                // Cek nodenya di ujung atau ga
                if (!currNode.IsAdjacent(stack.Peek())) {
                    backtrack = true;
                    visited.Add(currNode.getNode1());
                }
                else {
                    backtrack = false;
                    if (!visited.Contains(currNode.getNode1())) {
                        path.Add(currNode.getNode1());
                    }
                }

                currNode = graph_in.getNodeOf(stack.Peek());
            }

            return path;
        }

        public List<friendRec> recFriends(UndirectedGraph graph_in, string start) {
            // Inisialisasi
            List<Node> temen = graph_in.getAdjacentNodes(graph_in.getNodeOf(start));
            List<friendRec> recommended = new List<friendRec>();
            List<string> RecFrens = new List<string>();
            
            // Didata dulu siapa yang bisa jadi recommended friends
            foreach (Node namaTemen in temen) {
                string name = namaTemen.getNode1();
                if (!RecFrens.Contains(name)) {
                    RecFrens.Add(name);
                    recommended.Add(new friendRec(name));
                }
            }
            
            // Cari tau mutual friends siapa aja
            foreach (Node namaTemen in temen) {
                foreach (friendRec temenRekomen in recommended) {
                    if (namaTemen.IsAdjacent(temenRekomen.getName())) {
                        temenRekomen.addMutualFriend(namaTemen.getNode1());
                    }
                }
            }

            // Ini urutin berdasarkan mutual friends yeak
            return recommended.OrderByDescending(o => o.getTotalMutual()).ToList();
        }

    }
}
