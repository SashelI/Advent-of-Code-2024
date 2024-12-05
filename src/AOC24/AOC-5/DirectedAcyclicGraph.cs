using System.Data;
using System.Diagnostics.Metrics;
using System.Reflection;
using System.Text;

namespace AOC_5
{
	public class DirectedAcyclicGraph
	{
		private Dictionary<int, List<Tuple<int,int>>> _rules = new();

		public DirectedAcyclicGraph(string filePath)
		{
			var lines = File.ReadAllLines(filePath);
			foreach (var line in lines)
			{
				if (line.Contains('|'))
				{
					var nodes = line.Split('|', StringSplitOptions.RemoveEmptyEntries);

					if (nodes.Length == 2)
					{
						var firstNode = int.Parse(nodes[0]);
						var secondNode = int.Parse(nodes[1]);

						Tuple<int,int> newRule = new(firstNode, secondNode);

						if (!_rules.ContainsKey(firstNode))
						{
							_rules.Add(firstNode, []);
						}

						_rules[firstNode].Add(newRule);

						if (!_rules.ContainsKey(secondNode))
						{
							_rules.Add(secondNode, []);
						}

						_rules[secondNode].Add(newRule);
					}
				}
			}
		}

		public int SumOfCorrectOrders(Updates updates)
		{
			var sum = 0;
			int i = 0;

			foreach (var update in updates.UpdatesList)
			{
				if (IsCorrectOrder(update))
				{
					var middle = Updates.GetMiddleNumber(update);
					sum += middle;
				}
				i++;
			}

			return sum;
		}

		private bool IsCorrectOrder(List<int> update)
		{

			bool order = true;
			int pageCount = update.Count;

			for (int i = 0; i < pageCount; i++)
			{
				int currentPageNumber = update[i];
				var rules = _rules[currentPageNumber];

				foreach (var rule in rules)
				{
					if (currentPageNumber == rule.Item2)
					{
						for (int j = i + 1; j < pageCount; j++)
						{
							int number = update[j];
							if (rule.Item1 == number)
							{
								order = false;
								break;
							}
						}

					}
					if (!order)
					{
						break;
					}
				}

				if (!order)
				{
					break;
				}
			}

			return order;
		}
	}
}
