using System.Numerics;

namespace AOC_8
{
	public class AntinodesMap
	{
		readonly Vector2 _mapSize;
		private readonly IEnumerable<IGrouping<char, Vector2>> _antennas;

		public AntinodesMap(string filePath)
		{
			var lines = File.ReadAllLines(filePath);
			_mapSize = new Vector2(lines[0].Length, lines.Length);

			_antennas = lines.Index()
				.SelectMany(row => row.Item.Index(), (row, col) => new { Antenna = col.Item, Position = new Vector2(col.Index, row.Index) })
				.Where(element => element.Antenna != '.')
				.GroupBy(element => element.Antenna, element => element.Position);
		}

		public void FindAntinodes(out int nbAntinodes, out int nbHarmonics)
		{
			nbAntinodes = nbHarmonics = 0;

			var antinodes = new HashSet<Vector2>();
			var harmonics = new HashSet<Vector2>();

			foreach (var (position, delta) in _antennas.SelectMany(Delta))
			{
				var (x, y) = (position.X + delta.X, position.Y + delta.Y);

				for (var i = 0; 0 <= x && x < _mapSize.X && 0 <= y && y < _mapSize.Y; ++i)
				{
					if (i == 1)

					{
						antinodes.Add(new(x, y));
					}

					harmonics.Add(new(x, y));

					(x, y) = (x + delta.X, y + delta.Y);
				}
			}

			nbAntinodes = antinodes.Count;
			nbHarmonics = harmonics.Count;
		}

		private IEnumerable<(Vector2, Vector2)> Delta(IEnumerable<Vector2> positions)
		{
			return positions.SelectMany(p => positions.Except([p]), (p1, p2) => (p1, new Vector2(p2.X - p1.X, p2.Y - p1.Y)));
		}
	}
}
