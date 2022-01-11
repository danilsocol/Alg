using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgKruskala_16
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Edge[] arr = { new Edge(1, 2, 1), new Edge(3, 2, 2), new Edge(3, 4, 4), new Edge(5, 4, 3), new Edge(2, 4, 2), };
            List<Edge> l = TreeGraphMethods.Kruskals(arr);
            Console.WriteLine();
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
    public static class TreeGraphMethods
    {
        // Алгоритм Краскала. Обходим все отсортированные по весу ребра, которые не образуют цикл, ребра записываем в образуемые множества. 
        public static List<Edge> Kruskals(ICollection<Edge> edges)
        {
            var nodes = new List<int>();                        // ищем все вершины
            foreach (var edge in edges)
            {
                if (!nodes.Contains(edge.node1)) nodes.Add(edge.node1);
                if (!nodes.Contains(edge.node2)) nodes.Add(edge.node2);
            }

            var sortedByWeight = edges.OrderBy(e => e.weight);
            var nodeSets = new Dictionary<int, int>();          // множества которые хранят номера вершин
            var resultEdges = new List<Edge>();
            foreach (var edge in sortedByWeight)
            {
                int node1 = edge.node1, node2 = edge.node2;
                if (nodeSets.ContainsKey(node1))
                {
                    if (nodeSets.ContainsKey(node2))
                    {
                        if (nodeSets[node1] == nodeSets[node2]) // две вершины из одного множества - будет цикл
                            continue;
                        else
                        {                                       // соединяем два множества в одно
                            var nodesOfSecondSet = nodeSets.Where(x => x.Value == nodeSets[node2]).Select(x => x.Key);
                            foreach (var node in nodesOfSecondSet)
                                nodeSets[node] = nodeSets[node1];
                        }
                    }
                    else
                    {
                        nodeSets.Add(node2, nodeSets[node1]);
                    }
                }
                else
                {
                    if (nodeSets.ContainsKey(node2))
                        nodeSets.Add(node1, nodeSets[node2]);
                    else
                    {
                        int newSet = 0;                         // ни в одном множестве нет этих вершин - создаем новое множество
                        while (nodeSets.ContainsValue(newSet))   // ищем уникальный номер для нового множества
                            newSet++;
                        nodeSets.Add(node1, newSet);
                        nodeSets.Add(node2, newSet);
                    }
                }
                resultEdges.Add(edge);                          // все проверки пройдены и ребро может быть добавлено в ответ

                var sets = nodeSets.GroupBy(x => x.Value);
                foreach (var set in sets)
                    if (set.Count() == nodes.Count)
                        return resultEdges;
            }
            throw new Exception();
        }
    }
}
