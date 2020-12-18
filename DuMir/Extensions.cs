using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuMir
{
	static class Extensions
	{
		public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> elements)
		{
			foreach (var item in elements)
			{
				collection.Add(item);
			}
		}

		public static void SetOrAddValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
		{
			if(dictionary.ContainsKey(key))
				dictionary[key] = value;
			
			else
				dictionary.Add(key, value);
		}
	}
}
