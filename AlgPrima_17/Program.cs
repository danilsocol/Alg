using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgPrima_17
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Edge[] arr = { new Edge(1, 2, 1), new Edge(3, 2, 2), new Edge(3, 4, 4), new Edge(5, 4, 3), new Edge(2, 4, 2), };
            List<Edge> l = TreeGraphMethods.Prima(arr);
            Console.WriteLine();
        }
    }
    public static class TreeGraphMethods
    {
        // Алгоритм Эль Примо. Выбираем случайно начальную вершину, затем идем по наименьшему по весу ребру, которое не образует цикл, до того как не обойдем все ноды
        public static List<Edge> Prima(Edge[] edges)
        {
            var nodes = new List<int>();                        // ищем все вершины
            foreach (var edge in edges)
            {
                if (!nodes.Contains(edge.node1)) nodes.Add(edge.node1);
                if (!nodes.Contains(edge.node2)) nodes.Add(edge.node2);
            }

            List<int> visitedNodes = new List<int>();
            visitedNodes.Add(edges[0].node1);
            IOrderedEnumerable<Edge> toVisit = edges.Where(x => x.node1 == visitedNodes[0] || x.node2 == visitedNodes[0]).OrderBy(x => x.weight);
            List<Edge> resultEdges = new List<Edge>();
            while (visitedNodes.Count < nodes.Count)
            {
                if (toVisit.FirstOrDefault() == null) throw new Exception();
                Edge minEdge = toVisit.First();
                resultEdges.Add(minEdge);
                if (visitedNodes.Contains(minEdge.node1))
                    visitedNodes.Add(minEdge.node2);
                else
                    visitedNodes.Add(minEdge.node1);
                toVisit = edges.Where(x => visitedNodes.Contains(x.node1) ^ visitedNodes.Contains(x.node2)).OrderBy(x => x.weight);
            }
            return resultEdges;
        }
    }
    public class Edge
    {
        public int node1, node2, weight;
        public Edge(int node1, int node2, int weight)
        {
            this.node1 = node1;
            this.node2 = node2;
            this.weight = weight;
        }
        public override string ToString() => $"{node1} {node2}, {weight}";
    }
}
