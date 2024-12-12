using System.ComponentModel;
using System.Numerics;

namespace AOC_12
{
	public class PlantsMap
	{
		private List<string> _map = new();
		private Vector2 _bounds = new();

		private HashSet<(int, int)> _visitedPlants;
		private HashSet<(int, int, char)> _corners;

		public PlantsMap(string filePath)
		{
			var tmpMap = File.ReadAllLines(filePath).Select(r => $"|{r}|").ToList();
			var borderRow = new string('-', tmpMap[0].Length);

			_map = [borderRow, .. tmpMap, borderRow];
			_bounds = new(_map.Count - 1, borderRow.Length - 1);
		}

		public void CalculateFences(out int price1)
		{
			price1 = 0;
			_visitedPlants = new HashSet<(int, int)>();

			for (var j = 1; j < _bounds.X; ++j)
			{
				for (var i = 1; i < _bounds.Y; ++i)
				{
					if (_visitedPlants.Contains((i, j)))
					{
						continue;
					}

					var areaAndPerimeter = CalculateRegion(i, j, _map[j][i]);

					price1 += (int)(areaAndPerimeter.X * areaAndPerimeter.Y);
				}
			}
		}

		private Vector2 CalculateRegion(int x, int y, char crop)
		{
			if (_visitedPlants.Contains((x, y)))
			{
				return Vector2.Zero;
			}

			var area = 1;
			var perimeter = 0;

			_visitedPlants.Add((x, y));

			if (_map[y][x - 1] == crop)
			{
				var result = CalculateRegion(x - 1, y, crop);
				area += (int)result.X;
				perimeter += (int)result.Y;
			}
			else
			{
				perimeter++;
			}

			if (_map[y][x + 1] == crop)
			{
				var result = CalculateRegion(x + 1, y, crop);
				area += (int)result.X;
				perimeter += (int)result.Y;
			}
			else
			{
				perimeter++;
			}

			if (_map[y - 1][x] == crop)
			{
				var result = CalculateRegion(x, y - 1, crop);
				area += (int)result.X;
				perimeter += (int)result.Y;
			}
			else
			{
				perimeter++;
			}

			if (_map[y + 1][x] == crop)
			{
				var result = CalculateRegion(x, y + 1, crop);
				area += (int)result.X;
				perimeter += (int)result.Y;
			}
			else
			{
				perimeter++;
			}

			return new(area, perimeter);
		}
	}
}
