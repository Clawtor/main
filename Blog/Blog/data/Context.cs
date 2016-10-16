using Blog.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Blog.data
{
	public class Context : DbContext
	{
		public Context() : base("DefaultConnection")
		{
			this.Configuration.LazyLoadingEnabled = false;
			this.Configuration.ProxyCreationEnabled = false;

		}

		public DbSet<Post> Posts { get; set; }

		public DbSet<Tag> Tags { get; set; }

		public DbSet<Category> Categories {get;set;}
	}
}