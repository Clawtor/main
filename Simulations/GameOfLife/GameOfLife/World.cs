using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
	public class World
	{
		public int Width { get; private set; }
		public int Height { get; private set; }
		public int Size { get; private set; }

		private Cell[][] Cells { get; set;}
		private List<Flower> Flowers { get; set; }
		private double TempScale { get; set; }
		public double Temperature { get; set; }

		public World(int width, int height, int size)
		{
			this.Width = width;
			this.Height = height;
			this.Size = size;

			Cells = CreateCells(width, height, size);

			Flowers = new List<Flower>();

			double startingTemp = 0.5;

			TempScale = 1/(Width * Height / ((double)Size * (double)Size));
			Random r = new Random();
			int startingFlowers = 20;
			for(int i = 0; i < startingFlowers; i++)
			{
				var x = (int)(r.NextDouble() * (Width / Size));
				var y = (int)(r.NextDouble() * (Height / Size));
                Cells[x][y].Flower = new Flower(1, 0.1, 0.01);

				x = (int)(r.NextDouble() * (Width / Size));
				y = (int)(r.NextDouble() * (Height / Size));
				Cells[x][y].Flower = new Flower(0, 0.1, 0.01);
			}
		}

		private Cell[][] CreateCells(int width, int height, int size)
		{
			var cells = new Cell[Width / Size][];
			foreach (var x in Enumerable.Range(0, Width / Size))
			{
				cells[x] = new Cell[Height / Size];
				foreach (var y in Enumerable.Range(0, Height / Size))
				{
					cells[x][y] = new Cell(x, y, Size, 0.5, null);
				}
			}
			return cells;
		}

		public void Draw(Graphics graphics)
		{
			Pen pen = Pens.Black;
			for(int i=0;i<Cells.Length;i++)
			{
				for(int j = 0; j < Cells[i].Length; j++)
				{
					Cells[i][j].Draw(graphics, pen);
				}
			}
		}

		public void Update()
		{
			var cellsCopy = CopyCells(Cells);
			var temp = 0.0;
			Random random = new Random();
			for (int i = 0; i < Cells.Length; i++)
			{
				for (int j = 0; j < Cells[i].Length; j++)
				{
					if (Cells[i][j].Flower != null)
					{
						Cells[i][j].Flower.Action(cellsCopy, Cells[i][j], Temperature, random);
						//	0 will be negative. 1 will be...1 :)
						temp += (Cells[i][j].Flower.Albedo == 1 ? -1 : 1) * TempScale;

						if (random.NextDouble() <= Cells[i][j].Flower.DeathChance)
						{
							cellsCopy[i][j].Flower = null;
						}
					}
				}
			}
			Temperature = temp;
			Cells = cellsCopy;
		}

		public Cell[][] CopyCells(Cell[][] cells)
		{
			var cellsCopy = new Cell[cells.Length][];
			for (int i = 0; i < Cells.Length; i++)
			{
				cellsCopy[i] = new Cell[Cells[i].Length];
				for (int j = 0; j < Cells[i].Length; j++)
				{
					cellsCopy[i][j] = new Cell(i, j, Size, Cells[i][j].Temperature, Cells[i][j].Flower);
				}
			}
			return cellsCopy;
		}
	}
}
