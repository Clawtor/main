using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
	public class Tag
	{
		public int Id { get; set; }
		public List<Category> Categories { get; set; }
		public string Name { get; set; }
	}
}