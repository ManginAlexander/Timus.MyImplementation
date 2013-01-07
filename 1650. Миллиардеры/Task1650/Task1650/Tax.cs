using System.Collections.Generic;
using Task1650.Tools;
using Task1650.World;

namespace Task1650
{
	public class Tax
	{
		private readonly TreeSegment<City> _treeMax; 
		public Tax()
		{
			_treeMax = new TreeSegment<City>(CityBase.GetAll().SortByIndex());
		}

		public void ApplyChanges(IEnumerable<Move> groupChangeByDay)
		{
			foreach (var move in groupChangeByDay)
			{
				var curentCity = move.Man.CurrentCity;
				curentCity.RemoveRichMan(move.Man);

				move.To.AddRichMan(move.Man);
			}
		}

		public City GetTheRichestCity()
		{
			return _treeMax.GetMaxElelements();
		}
	}
}