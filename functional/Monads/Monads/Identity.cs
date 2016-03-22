using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monads
{
	public class Identity<T>
	{
		public T Value { get; private set; }

		public Identity(T value)
		{
			Value = value;
		}
	}

	public interface Maybe<T> { }

	public class Nothing<T> : Maybe<T>
	{
		public override string ToString()
		{
			return "Nothing";
		}
	}

	public class Just<T> : Maybe<T>
	{
		public T Value { get; private set; }
		public Just(T value)
		{
			Value = value;
		}
		public override string ToString()
		{
			return Value.ToString();
		}
	}
}
