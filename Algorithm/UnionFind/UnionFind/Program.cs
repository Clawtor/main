using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnionFind
{
	class Program
	{
		double pointDensity = 0.5;
		int width = 60;
		int height = 60;
		Random random = new Random();
		Dictionary<int,Site> Sites = new Dictionary<int, Site>();
		
		static void Main(string[] args)
		{
			var p = new Program();
			
			Console.ReadLine();
		}

		Program()
		{
			for(var i = 0; i< height; i++)
			{
				for(var j = 0; j< width; j++)
				{
					if (random.NextDouble() < pointDensity)
					{
						var id = i * (height) + j;
						Sites.Add(id, new Site() { x = j, y = i, Parent = null});
					}
				}
			}
			
			Print();

			//	Build a list of nodes to union.

			var unionList = new List<Tuple<int, int>>();

			//	for each element. If an element exists to the left or below then add to union list.
			foreach(var site in Sites)
			{
				if((site.Key + 1) % width != 0 && Sites.ContainsKey(site.Key + 1))
				{
					unionList.Add(new Tuple<int, int>(site.Key, site.Key + 1));
				}

				if (Sites.ContainsKey(site.Key + height))
				{
					unionList.Add(new Tuple<int, int>(site.Key, site.Key + height));
				}
			}

			//foreach (var union in unionList)
			//	Console.WriteLine($"Union({union.Item1}, {union.Item2})");

			foreach (var union in unionList)
				Union(union.Item1, union.Item2);

			Console.WriteLine(Percolates());	
        }

		public void Print()
		{
			var builder = new StringBuilder();
			var array = new bool[height,width];

			foreach(var site in Sites)
			{
				array[site.Value.y, site.Value.x] = true;
			}

			for(var i = 0; i < array.GetLength(0); i++)
			{
				for (var j = 0; j < array.GetLength(1); j++)
				{
					builder.Append(array[i, j] ? "X" : " ");
				}
				builder.AppendLine();
			}

			Console.WriteLine(builder.ToString());
		}
		
		public bool Union(Tuple<Site,Site> union)
		{
			if (union == null) return false;

			return Union(union.Item1, union.Item2);
		}

		public bool Union(int a, int b)
		{
			//	Set B's parent to A.
			if (Sites == null || Sites.Count == 0 && Sites.ContainsKey(a) && Sites.ContainsKey(b))
				return false;

			var A = Sites[a];
			var B = Sites[b];

			return Union(A, B);
		}

		public bool Union(Site A, Site B)
		{
			var aRoot = Find(A);
			var bRoot = Find(B);

			//	Dont assign self to self.
			if(aRoot != bRoot)
				aRoot.Parent = bRoot;

			return true;
		}

		public Site Find(int a)
		{
			if (Sites.ContainsKey(a))
				return Find(Sites[a]);

			return null;
		}

		public Site Find(Site A)
		{
			if (A.Parent == null)
			{
				return A;
			}

			return Find(A.Parent);
		}

		public bool Connected(int A, int B, Dictionary<int, Site> sites)
		{
			if (sites == null || sites.Count == 0 || sites.ContainsKey(A) == false || sites.ContainsKey(B) == false)
				return false;

			return Connected(sites[A], sites[B], sites);
		}

		//	Is A connected to B?
		public bool Connected(Site A, Site B, Dictionary<int, Site> sites)
		{
			if (sites == null || sites.Count == 0 || A == null || B == null)
				return false;

			var aRoot = Find(A);
			var bRoot = Find(B);

			return aRoot == bRoot;
		}

		public List<Site> GetRoots()
		{
			var roots = Sites.Where(x => x.Value.Parent == null)
							 .Select(x => x.Value)
							 .ToList();
			return roots;
		}

		public bool Percolates()
		{
			//	Checks if there is a path from the top to the bottom.
			//	Get sites at top.
			//	Get sites at bottom.
			//	Check if there is a connection.
			//	Get groups.
			var sitesAtTop = Sites.Where(x => x.Value.y == 0);
			var sitesAtBottom = Sites.Where(x => x.Value.y == height - 1);
			
			foreach(var topSite in sitesAtTop)
			{
				foreach (var bottomSite in sitesAtBottom)
				{
					if (Find(topSite.Value) == bottomSite.Value)
						return true;
				}
			}
			return false;
		}
	}
	
	class Site
	{
		public int x { get; set; }
		public int y { get; set; }

		public int index { get; set; }

		public Site Parent { get; set; }
	}
}
