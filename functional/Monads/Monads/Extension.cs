using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monads
{
	static class Extension
	{
		public static Identity<B> Bind<A, B>(this Identity<A> a, Func<A, Identity<B>> func)
		{
			return func(a.Value);
		}

		public static Identity<T> ToIdentity<T>(this T value)
		{
			return new Identity<T>(value);
		}

		public static Identity<C> SelectMany<A, B, C>(this Identity<A> a, Func<A, Identity<B>> func, Func<A, B, C> select)
		{
			return select(a.Value, func(a.Value).Value).ToIdentity();
		}

		public static Maybe<T> ToMaybe<T>(this T value)
		{
			return new Just<T>(value);
		}

		public static Maybe<B> Bind<A, B>(this Maybe<A> a, Func<A, Maybe<B>> func)
		{
			var justa = a as Just<A>;
			return justa == null ?
				new Nothing<B>() :
				func(justa.Value);
		}
		public static Maybe<C> SelectMany<A, B, C>(this Maybe<A> a, Func<A, Maybe<B>> func, Func<A, B, C> select)
		{
			return a.Bind(aval =>
				  func(aval).Bind(bval =>
			  select(aval, bval).ToMaybe()));
		}

		public static Maybe<int> Div(this int numerator, int denominator)
		{
			return denominator == 0
					   ? (Maybe<int>)new Nothing<int>()
					   : new Just<int>(numerator / denominator);
		}
	}
}

	
