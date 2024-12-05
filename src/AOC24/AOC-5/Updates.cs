using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC_5
{
	public class Updates
	{
		public List<List<int>> UpdatesList=new();

		public Updates(string filePath)
		{
			var lines = File.ReadAllLines(filePath);

			foreach (var line in lines)
			{
				if (line.Contains(','))
				{
					var updateList = new List<int>();

					var numbers = line.Split(',', StringSplitOptions.RemoveEmptyEntries);

					foreach (var number in numbers)
					{
						updateList.Add(int.Parse(number));
					}

					UpdatesList.Add(updateList);
				}
			}
		}

		public static int GetMiddleNumber(List<int> update)
		{
			var list = update.ToList();
			return list[(int)Math.Floor(list.Count / 2.0f)];
		}

		public override string ToString()
		{
			StringBuilder builder = new();

			foreach (var update in UpdatesList)
			{
				var enumerator = update.GetEnumerator();

				while (enumerator.MoveNext())
				{
					builder.Append(enumerator.Current + "-->");
				}

				builder.Remove(builder.Length - 3, 3);
				builder.Append("\n");
			}

			return builder.ToString();
		}
	}
}
