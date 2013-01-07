using System;
using System.Collections.Generic;
using System.Linq;
using Task1650.World;

namespace Task1650.Tools
{
	public static class Extantions
	{
		public static City[] SortByIndex(this IEnumerable<City> cities )
		{
			var arCities = cities.ToArray();
			Array.Sort(arCities, (a,b) => a.Index.CompareTo(b.Index));
			return arCities;
		}

		public static List<List<Move>> GroupMoveByTime(this IEnumerable<Move> moves)
		{
			var k = -1;
			var groupMove = new List<List<Move>>();
			foreach (var move in moves)
			{
				if (k == -1 || groupMove[k][0].Time != move.Time)
				{
					k++;
					groupMove.Add(new List<Move>());
				}
				groupMove[k].Add(move);

			}
			return groupMove;
		}
		public static IEnumerable<KeyValuePair<City, int>> SortByNameCity(this IEnumerable<KeyValuePair<City, int>> liderCities)
		{
			if (liderCities == null)
				throw new Exception();
			var list = liderCities.ToList();
			list.Sort((a, b) => String.Compare(a.Key.Name, b.Key.Name, StringComparison.Ordinal));
			return list;
		}
	}
}