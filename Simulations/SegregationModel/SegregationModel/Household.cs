using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegregationModel
{
	public class Household
	{
		public Brush Brush { get; set; }

		public Household(Brush brush)
		{
			Brush = brush;
		}
	}
}
