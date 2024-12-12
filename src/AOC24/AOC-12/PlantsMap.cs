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

		public void CalculateFences(out int price1, out int price2)
		{
			price1 = 0;
			price2 = 0;

			_visitedPlants = new HashSet<(int, int)>();

			for (var j = 1; j < _bounds.X; ++j)
			{
				for (var i = 1; i < _bounds.Y; ++i)
				{
					if (_visitedPlants.Contains((i, j)))
					{
						continue;
					}

					_corners = new HashSet<(int, int, char)>();
					var areaPerimeterAndCorners= CalculateRegion(i, j, _map[j][i]);

					price1 += (int)(areaPerimeterAndCorners.X * areaPerimeterAndCorners.Y);
					price2 += (int)(areaPerimeterAndCorners.X * areaPerimeterAndCorners.Z);
				}
			}
		}

		private Vector3 CalculateRegion(int x, int y, char crop)
		{
			if (_visitedPlants.Contains((x, y)))
			{
				return new Vector3(0, 0, _corners.Count);
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

				if (_map[y - 1][x] != crop)
				{
					_corners.Add((x - 1, y - 1, '3'));
				}

				if (_map[y - 1][x - 1] == crop && _map[y - 1][x] == crop)
				{
					_corners.Add((x - 1, y, '9'));
				}

				if (_map[y + 1][x - 1] == crop && _map[y + 1][x] == crop)
				{
					_corners.Add((x - 1, y, '3'));
				}
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

				if (_map[y + 1][x] != crop)
				{
					_corners.Add((x + 1, y + 1, '7'));
				}

				if (_map[y - 1][x + 1] == crop && _map[y - 1][x] == crop)
				{
					_corners.Add((x + 1, y, '7'));
				}

				if (_map[y + 1][x + 1] == crop && _map[y + 1][x] == crop)
				{
					_corners.Add((x + 1, y, '1'));
				}
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

				if (_map[y][x + 1] != crop)
				{
					_corners.Add((x + 1, y - 1, '1'));
				}

				if (_map[y - 1][x - 1] == crop && _map[y][x - 1] == crop)
				{
					_corners.Add((x, y - 1, '1'));
				}

				if (_map[y - 1][x + 1] == crop && _map[y][x + 1] == crop)
				{
					_corners.Add((x, y - 1, '3'));
				}
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

				if (_map[y][x - 1] != crop)
				{
					_corners.Add((x - 1, y + 1, '9'));
				}

				if (_map[y + 1][x - 1] == crop && _map[y][x - 1] == crop)
				{
					_corners.Add((x, y + 1, '7'));
				}

				if (_map[y + 1][x + 1] == crop && _map[y][x + 1] == crop)
				{
					_corners.Add((x, y + 1, '9'));
				}
			}

			return new(area, perimeter, _corners.Count);
		}
	}
}
