using System;

namespace Task1650.World
{
	public class City : IChange
	{
		public string Name;
		public int Index;
		public Int64 GetValue { get { return _totalMoney; } }

		private Int64 _totalMoney;

		public void AddRichMan(RichMan richMan)
		{
			richMan.CurrentCity = this;
			_totalMoney += richMan.Money;
			if (Update != null)
			{
				Update(Index,_totalMoney);
			}
		}
		public void RemoveRichMan(RichMan richMan)
		{
			_totalMoney -= richMan.Money;
			if (Update != null)
			{
				Update(Index,_totalMoney);
			}
		}
		public override string ToString()
		{
			return String.Format("Name:{0},Money:{1}", Name, _totalMoney);
		}


		public event Action<int,Int64> Update;
	}
}