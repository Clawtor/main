using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegregationModel
{
	public class Cell
	{
		public Rectangle Rectangle { get; set; }
		public Household Household { get; set; }

		public Cell(Rectangle rec, Household household)
		{
			Rectangle = rec;
			Household = household;
		}

		public Cell(Cell cell)
		{
			Rectangle = new Rectangle(cell.Rectangle.X, cell.Rectangle.Y, cell.Rectangle.Width, cell.Rectangle.Height);
			if (cell.Household == null)
			{
				this.Household = null;
			}
			else
			{
				Household = new Household(cell.Household.Brush);
			}
		}

		public void Draw(Graphics g)
		{
			if(Household != null)
			{
				g.FillRectangle(Household.Brush, Rectangle);
			}
		}
	}
}
