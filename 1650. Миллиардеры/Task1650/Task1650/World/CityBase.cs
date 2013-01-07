namespace Task1650.World
{
	public class CityBase : Base<City>
	{
		private static int _numberPreviousElement;
		public static City Create(string name)
		{

			var newCity = new City
				              {
					              Name = name,
					              Index = _numberPreviousElement++
				              };
			Elements.Add(name, newCity);
			return newCity;
		}
	}
}