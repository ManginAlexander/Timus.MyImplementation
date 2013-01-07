using System;

namespace Task1650.World
{
	public class RichManBase : Base<RichMan>
	{
		public static RichMan CreateRichMan(string name, Int64 money, City currentCity)
		{
			var newRichMan = new RichMan
				                 {
					                 Name = name,
					                 CurrentCity = currentCity,
					                 Money = money
				                 };
			Elements.Add(name, newRichMan);
			return newRichMan;
		}
	}
}