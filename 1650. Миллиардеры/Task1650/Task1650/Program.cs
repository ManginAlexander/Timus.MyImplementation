using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Task1650.Tools;
using Task1650.World;

namespace Task1650
{
	public class Program
	{
		private static void ReadRichManInfo(TextReader stream)
		{
			var inputString = stream.ReadLine();
			if (inputString == null)
				throw new Exception();
			var countRichPeople = Convert.ToInt32(inputString);

			for (int i = 0; i < countRichPeople; i++)
			{
				inputString = stream.ReadLine();
				if (inputString == null)
					continue;
				var whoWhereHowMany = inputString.Split(' ');
				var city = !CityBase.Exists(whoWhereHowMany[1]) ? CityBase.Create(whoWhereHowMany[1]) : CityBase.Find(whoWhereHowMany[1]);
				var man = RichManBase.CreateRichMan(whoWhereHowMany[0], Convert.ToInt64(whoWhereHowMany[2]), city);
				city.AddRichMan(man);
			}
		}
		private static MovePackage ReadMoves(TextReader stream)
		{
			var movePackage = new MovePackage
			{
				StartTime = 0,
				Moves = new List<Move>()
			};
			var inputString = stream.ReadLine();
			if (inputString == null)
				throw new Exception();
			var finishTimeAndCountMove = inputString.Split(' ');
			movePackage.FinishTime = Convert.ToInt32(finishTimeAndCountMove[0]);

			var countMove = Convert.ToInt32(finishTimeAndCountMove[1]);


			for (int i = 0; i < countMove; i++)
			{
				inputString = stream.ReadLine();
				if (inputString == null)
					continue;
				var timeWhoTo = inputString.Split(' ');
				var time = Convert.ToInt32(timeWhoTo[0]);
				var richMan = RichManBase.Find(timeWhoTo[1]);
				if (!CityBase.Exists(timeWhoTo[2]))
				{
					CityBase.Create(timeWhoTo[2]);
				}
				var to = CityBase.Find(timeWhoTo[2]);
				movePackage.Moves.Add(new Move
				{
					Man = richMan,
					Time = time,
					To = to
				});
			}
			return movePackage;
		}
		private static MovePackage ReadTask()
		{
			MovePackage movePackage;
			using (var stream = Console.In)//(TextReader)new StreamReader("input.txt"))
			{
				ReadRichManInfo(stream);
				movePackage = ReadMoves(stream);
			}
			return movePackage;
		}
		private static void WriteAnswer(Dictionary<City, int> liderCities)
		{
			using (var stream = Console.Out)
			{
				foreach (var res in liderCities.ToList().SortByNameCity())
					stream.WriteLine("{0} {1}", res.Key.Name, res.Value);
			}
		}

		public static void Main()
		{
			MovePackage movePackage = ReadTask();

			var tax = new Tax();
			var currentTime = movePackage.StartTime;
			var liderCities = new Dictionary<City, int>();
			City richestCity;
			foreach (var groupMoveByTime in movePackage.Moves.GroupMoveByTime())
			{
				richestCity = tax.GetTheRichestCity();
				var lostTime = groupMoveByTime[0].Time - currentTime;
				if (richestCity != null)
				{
					
					if (liderCities.ContainsKey(richestCity))
						liderCities[richestCity] += lostTime;
					else
						liderCities.Add(richestCity, lostTime);
				}
				currentTime += lostTime;

				tax.ApplyChanges(groupMoveByTime);
			}
			//Сколько лидирует город после последнего измения базы
			richestCity = tax.GetTheRichestCity();
			if (richestCity != null)
			{
				var leftTime = movePackage.FinishTime - currentTime;
				richestCity = tax.GetTheRichestCity();
				if (liderCities.ContainsKey(richestCity))
					liderCities[richestCity] += leftTime;
				else
					liderCities.Add(richestCity, leftTime);

			}
			WriteAnswer(liderCities);
		}
	}
}
