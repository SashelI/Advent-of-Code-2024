using System.Text;

namespace AOC_1
{
	public class LocationsList
	{
		public  List<int> Locations;
		public LocationsList(string filePath, int columnToRead)
		{
			var lines = File.ReadAllLines(filePath);
			Locations = new();

			foreach (var line in lines)
			{
				var numbers = line.Split("   ");

				if (columnToRead > numbers.Length)
				{
					columnToRead = numbers.Length;
				}

				int number = int.Parse(numbers[columnToRead]);

				Locations.Add(number);
			}
		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();

			builder.Append($"Locations ({Locations.Count}) : ");

			foreach (var location in Locations)
			{
				builder.Append($"{location} , ");
			}

			return builder.ToString();
		}

		public int DistanceToList(LocationsList list)
		{
			if (list.Locations.Count != Locations.Count) { return -1;}

			int distance = 0;
			var tmpList1 = new List<int>(Locations);
			var tmpList2 = new List<int>(list.Locations);

			while (tmpList1.Count > 0)
			{
				var small1 = tmpList1.Min();
				var small2 = tmpList2.Min();

				distance += Math.Abs(small1 - small2);

				tmpList1.Remove(small1);
				tmpList2.Remove(small2);
			}

			return distance;
		}
	}
}