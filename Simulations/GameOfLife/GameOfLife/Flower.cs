using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
	public class Flower
	{
		public double Albedo { get; set; }
		private Brush brush;
		private double BaseGrowthChance { get; set; }
		public double DeathChance { get; set; }
		public Flower(double albedo, double baseGrowthChance, double deathChance)
		{
			Albedo = albedo > 1 ? 1 : albedo < 0 ? 0 : albedo;
			DeathChance = deathChance > 1 ? 1 : deathChance < 0 ? 0 : deathChance;
			brush = new SolidBrush(
				Color.FromArgb(
					(int)(255 * Albedo),
					0,
					0
				)
			);
			BaseGrowthChance = baseGrowthChance;
		}

		/// <summary>
		/// Purpose here is to create new flowers based off warmth values.
		/// Chance of new flower in neighbouring cells is base + 
		/// </summary>
		/// <param name="cells"></param>
		/// <returns></returns>
		public void Action(Cell[][] cells, Cell cell, double temp, Random random)
		{
			var neighbours = GetNeighbours(cells, cell);
			foreach(var c in neighbours)
			{
				if(c.Flower == null)
				{
					var growthChance = (1 - (Math.Abs(this.Albedo - temp))) * this.BaseGrowthChance;
					if (random.NextDouble() <= growthChance)
					{
						c.Flower = new Flower(this.Albedo, this.BaseGrowthChance, this.DeathChance);
					}
				}
			}
		}

		public void Draw(Graphics g, Rectangle rectangle)
		{
			g.FillRectangle(brush, rectangle);
		}

		//	Torus world.
		//	Von Neumann neighbourhood
		private List<Cell> GetNeighbours(Cell[][] cells, Cell cell)
		{
			List<Cell> neighbours = new List<Cell>();
			var l = cell.X > 0 ? cell.X - 1 : cells.Length - 1;
			var r = cell.X  < cells.Length - 1 ? cell.X + 1 : 0;
			var t = cell.Y > 0 ? cell.Y - 1 : cells[0].Length - 1;
			var b = cell.Y < cells[0].Length - 1 ? cell.Y + 1 : 0;

			var left = cells[l][cell.Y];
			var right = cells[r][cell.Y];
			var top = cells[cell.X][t];
			var bottom = cells[cell.X][b];

			neighbours.Add(left);
			neighbours.Add(right);
			neighbours.Add(top);
			neighbours.Add(bottom);

			return neighbours;
		}
	}
}
