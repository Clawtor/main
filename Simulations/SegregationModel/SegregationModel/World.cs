using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegregationModel
{
	public class World
	{
		public int Width { get; private set; }
		public int Height { get; private set; }
		public double Density { get; set; }
		public int CellSize { get; private set; }

		public List<List<Cell>> Cells { get; set; }

		private Random Random { get; set; }

		private double Racism { get; set; }

		private bool IsTorus = false;

		public World(int w, int height, int cellSize, double density, double racism) {
			Width = w;
			Height = height;
			CellSize = cellSize;
			Density = Clamp(density);
			Random = new Random();

			Cells = InitCells();

			Racism = racism;

			ConsoleManager.Show();
        }

		private List<List<Cell>> InitCells()
		{
			var cells = new List<List<Cell>>(); 
			foreach (var i in Enumerable.Range(0, Width / CellSize))
			{
				var list = new List<Cell>();
				foreach (var j in Enumerable.Range(0, Height / CellSize))
				{
					var rect = new Rectangle(i * CellSize, j * CellSize, CellSize, CellSize);
					if (Random.NextDouble() < this.Density)
					{
						var rand = Random.NextDouble();
						if (rand < 0.25)
						{
							list.Add(new Cell(rect, new Household(Brushes.Red)));
						}
						else if(rand < 0.5)
						{
							list.Add(new Cell(rect, new Household(Brushes.Blue)));
						}
						else if(rand < 0.75)
						{
							list.Add(new Cell(rect, new Household(Brushes.Green)));
						}
						else
						{
							list.Add(new Cell(rect, new Household(Brushes.Yellow)));
						}
					}
					else
					{
						list.Add(new Cell(rect, null));
					}
				}
				cells.Add(list);
			}
			return cells;
		}

		public void Update()
		{
			Action();
		}

		public void Action() {
			var worldCopy = CloneCells();
			var emptyCells = new List<Tuple<int, int>>();

			for (int i = 0; i < Cells.Count; i++)
			{
				for (int j = 0; j < Cells[i].Count; j++)
				{
					if (Cells[i][j].Household == null)
					{
						emptyCells.Add(new Tuple<int, int>(i, j));
					}
				}
			}

			var count = 0;

			for(int i=0;i<Cells.Count;i++)
			{
				for(int j=0;j<Cells[i].Count;j++)
				{
					if(Cells[i][j].Household != null)
					{
						var neighbours = GetNeighbours(i, j);
						var similarCount = neighbours.Where(x => x.Household != null && x.Household.Brush == Cells[i][j].Household.Brush).Count();
						var neighbourCount = neighbours.Where(x => x.Household != null).Count();

                        if (neighbourCount > 0 && similarCount / (float)neighbours.Count < this.Racism)
						{
							var randomIndex = Random.Next(0, emptyCells.Count - 1);
							var randomPosition = emptyCells[randomIndex];
							worldCopy[randomPosition.Item1][randomPosition.Item2].Household = new Household(Cells[i][j].Household.Brush);
							emptyCells.RemoveAt(randomIndex);
							emptyCells.Add(new Tuple<int, int>(i, j));
						}
						else
						{
							worldCopy[i][j].Household = Cells[i][j].Household;

							//Console.WriteLine($"Location {i}, {j} set to household.");
							count++;
						}
					}
				}
			}

			Cells = worldCopy;
		}

		public List<List<Cell>> CloneCells()
		{
			var clone = new List<List<Cell>>();

			foreach (var x in this.Cells)
			{
				var cloneList = new List<Cell>();
				foreach(var y in x)
				{
					cloneList.Add(new Cell(y.Rectangle, null));
				}
				clone.Add(cloneList);
			}
			return clone;
		}

		public void Draw(Graphics g) {
			g.DrawRectangle(Pens.Black, 0, 0, this.Width, this.Height);
			Cells.ForEach(x => x.ForEach(y => y.Draw(g)));
		}

		public List<Cell> GetNeighbours(int i, int j)
		{
			var neighbours = new List<Cell>();
			var xLength = this.Width / this.CellSize - 1;
			var yLength = this.Height / this.CellSize - 1;

			int top, bottom, left, right;

			if (IsTorus)
			{

				top = j > 0 ? j - 1 : yLength;
				bottom = j < yLength ? j + 1 : 0;

				left = i > 0 ? i - 1 : xLength;
				right = i < xLength ? i + 1 : 0;

				neighbours.Add(this.Cells[left][top]);
				neighbours.Add(this.Cells[i][top]);
				neighbours.Add(this.Cells[right][top]);

				neighbours.Add(this.Cells[left][j]);
				neighbours.Add(this.Cells[right][j]);

				neighbours.Add(this.Cells[left][bottom]);
				neighbours.Add(this.Cells[i][bottom]);
				neighbours.Add(this.Cells[right][bottom]);

				return neighbours;
			}

			top = j > 0 ? j - 1 : -1;
			bottom = j < yLength ? j + 1 : -1;

			left = i > 0 ? i - 1 : -1;
			right = i < xLength ? i + 1 : -1;

			if(top >= 0)
			{
				if(left >= 0) neighbours.Add(this.Cells[left][top]);
				neighbours.Add(this.Cells[i][top]);
				if (right >= 0) neighbours.Add(this.Cells[right][top]);
			}

			if (left >= 0) neighbours.Add(this.Cells[left][j]);
			if (right >= 0) neighbours.Add(this.Cells[right][j]);

			if(bottom >= 0)
			{
				if(left >= 0) neighbours.Add(this.Cells[left][bottom]);
				neighbours.Add(this.Cells[i][bottom]);
				if (right >= 0) neighbours.Add(this.Cells[right][bottom]);
			}
			return neighbours;
		}

		private double Clamp(double d)
		{
			if (d > 1) return 1;
			if (d < 0) return 0;
			return d;
		}
	}
}
