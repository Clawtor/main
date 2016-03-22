using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphAlgorithms
{
    class Program
    {
        static void Main(string[] args)
        {
			//         Graph tree = new Graph();
			//tree.AddVertex("A");
			//tree.AddVertex("B");
			//tree.AddVertex("C");
			//tree.AddVertex("D");
			//tree.AddVertex("E");

			//tree.AddEdge("A", "B");
			//tree.AddEdge("A", "C");
			//tree.AddEdge("B", "D");
			//tree.AddEdge("D", "E");

			//var square = new Graph();
			//square.AddVertex("A");
			//square.AddVertex("B");
			//square.AddVertex("C");
			//square.AddVertex("D");

			//square.AddEdge("A", "B");
			//square.AddEdge("B", "C");
			//square.AddEdge("C", "D");
			//square.AddEdge("D", "A");

			//var triangle = new Graph();
			//triangle.AddVertex("A");
			//triangle.AddVertex("B");
			//triangle.AddVertex("C");

			//triangle.AddEdge("A", "B");
			//triangle.AddEdge("B", "C");
			//triangle.AddEdge("C", "A");

			//var treeIsBipartite = tree.TestBipartite("A");
			//var squareIsBipartite = square.TestBipartite("A");
			//var triangleIsBipartite = triangle.TestBipartite("A");

			//Console.WriteLine($"Tree is bipartite: {treeIsBipartite}");
			//Console.WriteLine($"Square is bipartite: {treeIsBipartite}");
			//Console.WriteLine($"Triangle is bipartite: {triangleIsBipartite}");
			//Console.ReadLine();

			var cycle = new DirectedGraph();
			cycle.AddVertex("A");
            cycle.AddVertex("B");
			cycle.AddVertex("C");
			cycle.AddVertex("D");

			cycle.AddEdge("A", "B");
			cycle.AddEdge("B", "C");
			cycle.AddEdge("C", "D");
			cycle.AddEdge("D", "A");
			
			var wheel = new DirectedGraph();
			wheel.AddVertex("A");
			wheel.AddVertex("B");
			wheel.AddVertex("C");
			wheel.AddVertex("D");

			wheel.AddEdge("A", "B");
			wheel.AddEdge("A", "C");
			wheel.AddEdge("A", "D");

			Console.WriteLine(cycle.IsStronglyConnected());

			//foreach(var n in )
			Console.ReadLine();
		}
    }

    public class Graph
    {
        Dictionary<string, List<string>> adjacencyList;

        public Graph()
        {
            adjacencyList = new Dictionary<string, List<string>>();
        }

        public bool AddVertex(string id)
        {
            if(adjacencyList.Keys.Contains(id))
            {
                return false;
            }
            adjacencyList.Add(id, new List<string>());
            return true;
        }

        public bool AddEdge(string nodeA, string nodeB)
        {
            if(adjacencyList.Keys.Contains(nodeA) && adjacencyList.Keys.Contains(nodeB))
            {
                if(adjacencyList[nodeA].Contains(nodeB) == false && adjacencyList[nodeB].Contains(nodeA) == false)
                {
                    adjacencyList[nodeA].Add(nodeB);
                    adjacencyList[nodeB].Add(nodeA);
                    return true;
                }
            }
            return false;
        }

        public bool ShareEdge(string nodeA, string nodeB)
        {
            return adjacencyList[nodeA].Contains(nodeB) && adjacencyList[nodeB].Contains(nodeA);
        }

        public void PrintGraph()
        {
            foreach(var list in adjacencyList)
            {
                Console.WriteLine("Vertex " + list.Key + ": " + String.Join(", ", list.Value.ToArray()));
            }
        }

        public List<string> BFS(string id)
        {
            var discovered = new List<string>();
            discovered.Add(id);
            var explored = new List<string>();

            //  for each in discovered
            while(discovered.Count > 0)
            {
                var fringe = new List<string>();
                foreach (var d in discovered)
                {
                    var neighbours = adjacencyList[d];
                    foreach (var n in neighbours)
                    {
                        if (discovered.Contains(n) == false && explored.Contains(n) == false)
                        {
                            fringe.Add(n);
                        }
                    }
                }
                explored.AddRange(discovered);
                discovered.Clear();
                discovered.AddRange(fringe);
            }
            return explored;
        }
        public List<string> DFS(string node, List<string> exploredNodes)
        {
            exploredNodes.Add(node);
            foreach(var neighbour in adjacencyList[node])
            {
                if (exploredNodes.Contains(neighbour) == false)
                {
                    DFS(neighbour, exploredNodes);
                }
            }
            return exploredNodes;
        }
		
		public bool TestBipartite(string id)
		{
			var discovered = new List<BPTestingNode>();
			var layerId = 0;
			discovered.Add(new BPTestingNode() { LayerId = layerId, NodeId = id });
			var explored = new List<BPTestingNode>();
			
			//  for each in discovered
			while (discovered.Count > 0)
			{
				layerId++;
				var fringe = new List<BPTestingNode>();
				foreach (var d in discovered)
				{
					var neighbours = adjacencyList[d.NodeId];
					foreach (var n in neighbours)
					{
						if (discovered.Select(x => x.NodeId).Contains(n) == false && explored.Select(x => x.NodeId).Contains(n) == false)
						{
							fringe.Add(new BPTestingNode() { LayerId = layerId, NodeId = n });
						}
					}
				}
                explored.AddRange(discovered);
				discovered.Clear();
				discovered.AddRange(fringe);
			}
			//	Iterate through layers and test connections?
			foreach (var layer in explored.GroupBy(x => x.LayerId).ToList())
			{
				//	Get all nodes in layer.
				var nodes = layer.Select(x => x.NodeId).ToList();
				//	Get all links in layer.
				var links = nodes.Select(x => adjacencyList[x]);
				var distinctLinks = new List<string>();
				foreach (var l in links)
				{
					foreach (string s in l)
					{
						if (distinctLinks.Contains(s))
							continue;
						distinctLinks.Add(s);
					}
				}
				//	Test if any nodes are contained in any links.
				foreach (var l in distinctLinks)
				{
					foreach (var node in nodes)
					{
						if (l.Equals(node))
						{
							return false;
						}
					}
				}
			}
			return true;
		}
	}

	public class BPTestingNode
	{
		public string NodeId { get; set; }
		public int LayerId { get; set; }
	}

    public class Node
    {
        public string ID { get; set; }
        public bool Explored { get; set; }
    }
}
