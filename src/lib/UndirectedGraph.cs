using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BaconPancakes
{
    public class Node
    {
        private string node;
        private List<string> adjacent_nodes;

        public Node(string node1)
        {
            node = node1;
            adjacent_nodes = new List<string>();
        }

        public Node(string node1, string node2)
        {
            node = node1;
            adjacent_nodes = new List<string>();
            AddAdjacency(node2);
        }

        public string getNode1()
        {
            return node;
        }

        public List<string> getAdjacentNodes()
        {
            return adjacent_nodes;
        }

        public bool IsAdjacent(string node2)
        {
            return !node.Equals(null) && adjacent_nodes.Contains(node2);
        }
        public void AddAdjacency(string node2)
        {
            if (!adjacent_nodes.Contains(node2))
            {
                adjacent_nodes.Add(node2);
            }
        }

        public void RemoveAdjacency(string node2)
        {
            if (adjacent_nodes.Contains(node2))
            {
                adjacent_nodes.Remove(node2);
            }
        }
    }

    public class UndirectedGraph
    {
        private List<Node> nodes;
        public int Count { get { return nodes.Count; } }

        public UndirectedGraph()
        {
            nodes = new List<Node>();
        }

        public List<Node> GetNodes()
        {
            return nodes;
        }

        public int getIndexOf(Node node) {
            return nodes.IndexOf(node);
        }

        public Node getNodeOf(string node1)
        {
            foreach (Node a_node in GetNodes())
            {
                if (a_node.getNode1() == node1)
                {
                    return a_node;
                }
            }
            return null;
        }

        public List<Node> getAdjacentNodes(Node node) {
            List<Node> adjacentNodes= new List<Node>();
            foreach (string name in node.getAdjacentNodes()) {
                adjacentNodes.Add(this.getNodeOf(name));
            }
            return adjacentNodes;
        }

        public bool IsNodeExist(string node1)
        {
            return getNodeOf(node1) != null;
        }

        public bool IsEdgeExist(string node1, string node2)
        {
            if (!IsNodeExist(node1) || !getNodeOf(node1).IsAdjacent(node2))
            {
                return false;
            }

            if (!IsNodeExist(node2) || !getNodeOf(node2).IsAdjacent(node1))
            {
                return false;
            }
            return true;
        }

        public void AddEdge(string node1, string node2)
        {
            if (!IsNodeExist(node1))
            {
                nodes.Add(new Node(node1, node2));
            } else
            {
                getNodeOf(node1).AddAdjacency(node2);
            }
          
            if (!IsNodeExist(node2))
            {
                nodes.Add(new Node(node2, node1));
            } else
            {
                getNodeOf(node2).AddAdjacency(node1);
            }
          
        }

        public void RemoveEdge(string node1, string node2)
        {
            if (IsEdgeExist(node1, node2))
            {
                getNodeOf(node1).RemoveAdjacency(node2);
                getNodeOf(node2).RemoveAdjacency(node1);
            }
        }
        public void print()

        {
            foreach (Node hasil in GetNodes())
            {
                foreach (string hasil_node2 in hasil.getAdjacentNodes())
                {
                    Console.WriteLine(hasil.getNode1() + ", " + hasil_node2);
                }
            }
        }

        public List<string> BFS(string start, string end)
        {
            // Kalau ga ada, return
            if (!IsNodeExist(start) || !IsNodeExist(end)) {
                return null;
            }
            
            // Inisialisasi
            List<string> path = new List<string>();
            Node startNode = getNodeOf(start);
            Queue<Node> antrian_node = new Queue<Node>();
            antrian_node.Enqueue(startNode);
            bool[] dikunjungi_array = new bool[Count];
            
            Node currentNode;
            while (antrian_node.Count > 0) {

                // Pop to path and currentNode
                currentNode = antrian_node.Dequeue();
                // dikunjungi_array[getIndexOf(currentNode)] = true;

                foreach (Node adjacentNode in getAdjacentNodes(currentNode)) {
                    if (!dikunjungi_array[getIndexOf(adjacentNode)]) {
                        List<string> new_path = new List<string>();
                        new_path = path;
                        new_path.Append(adjacentNode.getNode1());
                        antrian_node.Append(adjacentNode);
                    }

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
    }
}