using System.Numerics;

namespace AOC_10
{
	public class TopographicMap
	{
		private List<string> _map=new();
		private Vector2 _bounds;
		public TopographicMap(string filePath)
		{
			var linesToMap = File.ReadAllLines(filePath).Select(r => $"*{r}*").ToList();
			var borderRow = new string('*', linesToMap[0].Length);

			_map = [borderRow, .. linesToMap, borderRow];

			_bounds = new (_map.Count-1, borderRow.Length - 1);
		}

		public void CountScores(out int scorePart1, out int scorePart2)
		{
			(scorePart1, scorePart2) = (0, 0);

			for (var i = 1; i < _bounds.X; ++i)
			{
				for (var j = 1; j < _bounds.Y; ++j)
				{
					switch (_map[i][j])
					{
						case '0':
							scorePart1 += CountTrailheadScore(j, i);
							scorePart2 += CountTrailheadRating(j, i);
							break;
					}
				}
			}
		}

		int CountTrailheadScore(int i, int j)
		{
			var result = 0;
			var visited = new HashSet<(int, int)>();
			var queue = new Queue<(int, int, char)>([(i, j, '0')]);

			while (queue.TryDequeue(out var item))
			{
				var (x, y, height) = item;

				if (visited.Contains((x, y)))
				{
					continue;
				}

				visited.Add((x, y));

				if (height == '9')
				{
					++result;
					continue;
				}

				if (_map[y][x - 1] == height + 1)
				{
					queue.Enqueue((x - 1, y, (char)(height + 1)));
				}
				if (_map[y][x + 1] == height + 1)
				{
					queue.Enqueue((x + 1, y, (char)(height + 1)));
				}
				if (_map[y - 1][x] == height + 1)
				{
					queue.Enqueue((x, y - 1, (char)(height + 1)));
				}
				if (_map[y + 1][x] == height + 1)
				{
					queue.Enqueue((x, y + 1, (char)(height + 1)));
				}
			}

			return result;
		}

		int CountTrailheadRating(int i, int j)
		{
			var result = 0;
			var queue = new Queue<(int, int, char)>([(i, j, '0')]);

			while (queue.TryDequeue(out var item))
			{
				var (x, y, height) = item;

				if (height == '9')
				{
					++result;
					continue;
				}

				if (_map[y][x - 1] == height + 1)
				{
					queue.Enqueue((x - 1, y, (char)(height + 1)));
				}
				if (_map[y][x + 1] == height + 1)
				{
					queue.Enqueue((x + 1, y, (char)(height + 1)));
				}
				if (_map[y - 1][x] == height + 1)
				{
					queue.Enqueue((x, y - 1, (char)(height + 1)));
				}
				if (_map[y + 1][x] == height + 1)
				{
					queue.Enqueue((x, y + 1, (char)(height + 1)));
				}
			}

			return result;
		}
	}
}
