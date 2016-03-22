using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monads
{
	class Program
	{
		static void Main(string[] args)
		{
			var result =
				from a in "Hello World!".ToIdentity()
				from b in 7.ToIdentity()
				from c in (new DateTime(2010, 1, 11)).ToIdentity()
				select a + ", " + b.ToString() + ", " + c.ToShortDateString();

			Console.WriteLine(result.Value);
			
			Console.ReadLine();
		}
	}
}
