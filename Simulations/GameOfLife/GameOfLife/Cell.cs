using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
	public class Cell
	{
		public int X { get; set; }
		public int Y { get; set; }
		private int Size { get; set; }
		public Rectangle Rectangle { get; private set;}
		public double Temperature { get; set; }
		public Flower Flower { get; set; }

		public Cell(int x, int y, int size, double temp, Flower flower)
		{
			X = x; Y = y; Size = size;
			Rectangle = new Rectangle(X, Y, Size, Size);
			Temperature = temp;
			Flower = flower;
		}

		public void Draw(Graphics g, Pen pen)
		{
			if (Flower != null)
				Flower.Draw(g, new Rectangle(X * Size, Y * Size, Size, Size));
			g.DrawRectangle(pen, X * Size, Y * Size, Size, Size);
		}
	}
}
