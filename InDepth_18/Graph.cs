using System;
using System.Collections.Generic;
using System.Text;

namespace InDepth_18
{
    class Graph
    {
        public Node[] graph;

        public Graph(int countNode)
        {
            graph = new Node[countNode];

            for (int i = 0; i < countNode; i++)
                graph[i] = new Node();
        }
        public void addEdge(int id1, int id2)
        {
            graph[id1].conected.Add(graph[id2]);
        }

        public  Node[] DFS(int start)
        {
            var startNode = graph[start];

            var visited = new List<Node> { startNode };
            var stack = new Stack<Node>();
            stack.Push(startNode);
            while (stack.Count > 0)
            {
                var node = stack.Pop();
                var neighbours = node.conected;
                foreach (var n in neighbours)
                    if (!visited.Contains(n))
                    {
                        stack.Push(n);
                        visited.Add(n);
                    }
            }
            return visited.ToArray();
        }
    }

    class Node
    {
        public List<Node> conected = new List<Node>();
    }
}
