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

        public string GetNode1()
        {
            return node;
        }

        public List<string> GetAdjacentNodes()
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
                adjacent_nodes.Sort();
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

        public int GetIndexOf(Node node)
        {
            return nodes.IndexOf(node);
        }

        public Node GetNodeOf(string node1)
        {
            foreach (Node a_node in GetNodes())
            {
                if (a_node.GetNode1() == node1)
                {
                    return a_node;
                }
            }
            return null;
        }

        public List<Node> GetAdjacentNodes(Node node)
        {
            List<Node> adjacentNodes = new List<Node>();
            foreach (string name in node.GetAdjacentNodes())
            {
                adjacentNodes.Add(this.GetNodeOf(name));
            }
            return adjacentNodes;
        }

        public bool IsNodeExist(string node1)
        {
            return GetNodeOf(node1) != null;
        }

        public bool IsEdgeExist(string node1, string node2)
        {
            if (!IsNodeExist(node1) || !GetNodeOf(node1).IsAdjacent(node2))
            {
                return false;
            }

            if (!IsNodeExist(node2) || !GetNodeOf(node2).IsAdjacent(node1))
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
            }
            else
            {
                GetNodeOf(node1).AddAdjacency(node2);
            }

            if (!IsNodeExist(node2))
            {
                nodes.Add(new Node(node2, node1));
            }
            else
            {
                GetNodeOf(node2).AddAdjacency(node1);
            }
        }

        public void AddNode(string node) {
            if (!IsNodeExist(node)) {
                nodes.Add(new Node(node));
            }
        }

        public void RemoveEdge(string node1, string node2)
        {
            if (IsEdgeExist(node1, node2))
            {
                GetNodeOf(node1).RemoveAdjacency(node2);
                GetNodeOf(node2).RemoveAdjacency(node1);
            }
        }

        public void print()
        {
            foreach (Node hasil in GetNodes())
            {
                if (hasil.GetAdjacentNodes().Count == 0) {
                    Console.WriteLine(hasil.GetNode1());
                }
                else {
                    foreach (string hasil_node2 in hasil.GetAdjacentNodes())
                    {
                        Console.WriteLine(hasil.GetNode1() + ", " + hasil_node2);
                    }
                }
            }
        }
    }
}
