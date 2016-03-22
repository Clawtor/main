using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphAlgorithms
{
	public class DirectedGraph
	{
		Dictionary<string, Links> AdjacencyList;

		public DirectedGraph()
		{
			AdjacencyList = new Dictionary<string, Links>();
		}

		public bool AddVertex(string id)
		{
			if (AdjacencyList.Keys.Contains(id))
			{
				return false;
			}
			AdjacencyList.Add(id, 
							 new Links()
							 {
								From = new List<string>(),
								To = new List<string>()
							 });
			return true;
		}

		public bool AddEdge(string nodeA, string nodeB)
		{
			if (AdjacencyList.Keys.Contains(nodeA) && AdjacencyList.Keys.Contains(nodeB))
			{
				if (AdjacencyList[nodeA].To.Contains(nodeB) == false)
				{
					AdjacencyList[nodeA].To.Add(nodeB);
					AdjacencyList[nodeB].From.Add(nodeA);
					return true;
				}
			}
			return false;
		}

		public override string ToString()
		{
			var builder = new StringBuilder();
			foreach(var node in AdjacencyList)
			{
				builder.AppendLine($"NodeID: {node.Key}");
				foreach(var toLink in node.Value.To)
				{
					builder.AppendLine($"\tLink From {node.Key} To {toLink}");
                }
			}
			return builder.ToString();
		}

		public List<string> BFS(string id)
		{
			var discovered = new List<string>();
			discovered.Add(id);
			var explored = new List<string>();

			//  for each in discovered
			while (discovered.Count > 0)
			{
				var fringe = new List<string>();
				foreach (var d in discovered)
				{
					var neighbours = AdjacencyList[d].To;
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

		public DirectedGraph GetReverse()
		{
			var reverseGraph = new DirectedGraph();
			var adjacencyList = new Dictionary<string, Links>();
			foreach(var node in AdjacencyList)
			{
				adjacencyList.Add(node.Key, new Links() { From = node.Value.To, To = node.Value.From });
			}
			reverseGraph.AdjacencyList = adjacencyList;
			return reverseGraph;
		}

		public bool IsStronglyConnected()
		{
			if (AdjacencyList.Count == 0) return true;

			var reverseGraph = this.GetReverse();
			var bfsResult = BFS(AdjacencyList.Keys.ElementAt(0));
			
			foreach(var node in AdjacencyList)
				if (bfsResult.Contains(node.Key) == false)
					return false;

			var reverseResult = reverseGraph.BFS(AdjacencyList.Keys.ElementAt(0));

			foreach (var node in AdjacencyList)
				if (reverseResult.Contains(node.Key) == false)
					return false;

			return true;
		}

		public bool IsDAG()
		{
			return false;
		}

		public void GetTopologicalOrdering() { }
	}

	public class Links
	{
		public List<string> To { get; set; }
		public List<string> From { get; set; }
	}
}
