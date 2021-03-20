using System;
using System.Collections.Generic;
using System.Text;


namespace BaconPancakes
{
    public class Node
    {
        private char node;
        private List<char> node_targets;

        public Node(char node1)
        {
            node = node1;
            node_targets = new List<char>();
        }

        public Node(char node1, char node2)
        {
            node = node1;
            node_targets = new List<char>();
            AddAdjacency(node2);
        }

        public char getNode1()
        {
            return node;
        }

        public List<char> getTargetNodes()
        {
            return node_targets;
        }

        public bool IsAdjacent(char node2)
        {
            return !node.Equals(null) && node_targets.Contains(node2);
        }
        public void AddAdjacency(char node2)
        {
            if (!node_targets.Contains(node2))
            {
                node_targets.Add(node2);
            }
        }

        public void RemoveAdjacency(char node2)
        {
            if (node_targets.Contains(node2))
            {
                node_targets.Remove(node2);
            }
        }
    }
    public class UndirectedGraph
    {
        private List<Node> nodes;

        public UndirectedGraph()
        {
            nodes = new List<Node>();
        }

        public List<Node> GetNodes()
        {
            return nodes;
        }

        private Node getNodeOf(char node1)
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
        public bool IsNodeExist(char node1)
        {
            return getNodeOf(node1) != null;
        }

        public bool IsEdgeExist(char node1, char node2)
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

        public void AddEdge(char node1, char node2)
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

        public void RemoveEdge(char node1, char node2)
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
                foreach (char hasil_node2 in hasil.getTargetNodes())
                {
                    Console.WriteLine(hasil.getNode1() + ", " + hasil_node2);
                }
            }
        }
    }
}