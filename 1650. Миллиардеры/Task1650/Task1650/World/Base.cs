using System.Collections.Generic;
using System.Linq;

namespace Task1650.World
{
	public class Base<T>
	{
		protected static readonly Dictionary<string, T> Elements = new Dictionary<string, T>();
		public static bool Exists(string name)
		{
			return Elements.ContainsKey(name);
		}

		public static T Find(string name)
		{
			return Elements[name];
		}
		public static T[] GetAll()
		{
			return Elements.Values.ToArray();
		}
	}
}