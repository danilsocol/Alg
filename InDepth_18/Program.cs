﻿using System;

namespace InDepth_18
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph g = new Graph(4);

            g.addEdge(0, 1);
            g.addEdge(0, 2);
            g.addEdge(1, 2);
            g.addEdge(2, 3);

            g.DFS(0);
        }
    }
}
