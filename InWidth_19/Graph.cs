using System;
using System.Collections.Generic;
using System.Text;

namespace InWidth_19
{
    class Graph
    {
        int countNode;
        public Node[] graph;
        bool[] visited;

        public Graph(int countNode)
        {
            visited = new bool[countNode];
            graph = new Node[countNode];
            this.countNode = countNode;

            for (int i = 0; i < countNode; i++)
                graph[i] = new Node();
        }
        public void addEdge(int id1, int id2)
        {
            graph[id1].conected.Add(graph[id2]);
        }

       

        public Node[] BFS(int start)
        {
            var startNode = graph[start];

            var visited = new List<Node> { startNode };
            var queue = new Queue<Node>();
            queue.Enqueue(startNode);
            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                var neighbours = node.conected;
                foreach (var n in neighbours)
                    if (!visited.Contains(n))
                    {
                        queue.Enqueue(n);
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
