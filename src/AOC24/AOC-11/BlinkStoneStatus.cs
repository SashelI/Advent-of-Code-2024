namespace AOC_11
{
	public class BlinkStoneStatus
	{
		private readonly Dictionary<long, long> _stones = new();
		public BlinkStoneStatus(string filePath)
		{
			var input = File.ReadAllText(filePath);
			_stones = input.Split(' ').Select(long.Parse).CountBy(l => l).ToDictionary(x => x.Key, x => (long)x.Value);
		}

		public void CountStones(out long stones1, out long stones2)
		{
			var results = StoneCount(_stones, blinks: [25, 75]);

			stones1 = results[0];
			stones2 = results[1];
		}

		static IList<long> StoneCount(IDictionary<long, long> stones, IList<int> blinks, int blink = 1)
		{
			var newStones = new Dictionary<long, long>();

			foreach (var (stone, count) in stones)
			{
				var digits = CountDigits(stone);

				switch (stone, digits % 2 == 0)
				{
					case (0L, _):
						IncrementValue(newStones, 1L, count);
						break;

					case (_, false):
						IncrementValue(newStones, stone * 2024L, count);
						break;

					case (_, true) when (long)Math.Pow(10.0, digits / 2) is var power:
						IncrementValue(newStones, stone / power, count);
						IncrementValue(newStones, stone % power, count);
						break;
				}
			}

			if (!blinks.Contains(blink))
			{
				return StoneCount(newStones, blinks, blink + 1);
			}

			blinks.Remove(blink);

			return [newStones.Sum(x => x.Value), .. blinks.Count > 0 ? StoneCount(newStones, blinks, blink + 1) : []];
		}

		static int CountDigits(long number) => (number > 0 ? (int)Math.Log10(number) : 0) + 1;

		static void IncrementValue(Dictionary<long, long> dictionary, long key, long increment)
		{
			dictionary[key] = dictionary.GetValueOrDefault(key, 0L) + increment;
		}
}
}
