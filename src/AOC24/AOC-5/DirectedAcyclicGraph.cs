using System.Data;

namespace AOC_5
{
	public class DirectedAcyclicGraph
	{
		internal class RulesComparer : IComparer<int>
		{
			private readonly IEnumerable<Tuple<int,int>> _rules;

			public RulesComparer(IEnumerable<Tuple<int, int>> rules)
			{
				_rules = rules;
			}

			public int Compare(int first, int second)
			{
				if (first == second)
				{
					return 0;
				}
				foreach (var rule in _rules)
				{
					if (rule.Item2 == first && rule.Item1 == second)
					{
						return 1;

					}
					if (rule.Item2 == second && rule.Item1 == first)
					{
						return -1;
					}

				}
				return 0;
			}
		}

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

		public int SumOfIncorrectOrders(Updates updates)
		{
			var sum = 0;

			foreach (var update in updates.UpdatesList)
			{
				if (!IsCorrectOrder(update))
				{
					HashSet<Tuple<int,int>> allRules = [];

					foreach (var page in update)
					{
						foreach (var rule in _rules[page])
						{
							allRules.Add(rule);
						}
					}

					RulesComparer rulesComparer = new(allRules);
					update.Sort(rulesComparer);

					sum += Updates.GetMiddleNumber(update);
				}
			}

			return sum;
		}

		public int SumOfCorrectOrders(Updates updates)
		{
			var sum = 0;

			foreach (var update in updates.UpdatesList)
			{
				if (IsCorrectOrder(update))
				{
					sum += Updates.GetMiddleNumber(update);
				}
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
