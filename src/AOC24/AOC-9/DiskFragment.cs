namespace AOC_9
{
	public class DiskFragment
	{

		private List<int?> _blocks = new();

		public DiskFragment(string filePath)
		{
			var input = File.ReadAllText(filePath);

			var fileId = 0;
			var isFreeSpace = false;

			for (int i = 0; i < input.Length; i++)
			{
				for (int j = 0; j < input[i] - '0'; j++)
				{
					_blocks.Add(isFreeSpace ? null : fileId);
				}

				fileId += isFreeSpace ? 0 : 1;
				isFreeSpace = !isFreeSpace;
			}
		}

		public void Checksum(out long part1, out long part2)
		{
			part1 = ChecksumPart1([.. _blocks]);
			part2 = ChecksumPart2([.. _blocks]);
		}

		private long ChecksumPart1(IList<int?> blocks)
		{
			var result = 0L;
		
			for (var i = 0; i < blocks.Count; ++i)
			{
				if (blocks[i] != null)
				{
					continue;
				}

				for (var j = blocks.Count - 1; j > i; --j)
				{
					if (blocks[j] == null)
					{
						continue;
					}

					(blocks[i], blocks[j]) = (blocks[j], null);

					break;
				}
			}

			for (var i = 0; i < blocks.Count; ++i)
			{
				if (blocks[i] is int currentFileId)
				{
					result += i * currentFileId;
				}
			}

			return result;
		}

		private long ChecksumPart2(IList<int?> blocks)
		{
			var result = 0L;

			for (var i = blocks.Count - 1; i > 0; --i)
			{
				if (blocks[i] == null)
				{
					continue;
				}

				var firstIndex = -1;
				var spaceNeeded = blocks.Count(b => b == blocks[i]);

				for (var j = 0; j < i; ++j)
				{
					if (blocks[j] != null)
					{
						firstIndex = -1;
						continue;
					}

					if (firstIndex == -1)
					{
						firstIndex = j;
					}

					if (j - firstIndex + 1 == spaceNeeded)
					{
						for (var k = 0; k < spaceNeeded; ++k)
						{
							(blocks[i - k], blocks[firstIndex + k]) = (blocks[firstIndex + k], blocks[i - k]);
						}

						break;
					}
				}
			}

			for (var i = 0; i < blocks.Count; ++i)
			{
				if (blocks[i] is int currentFileId)
				{
					result += i * currentFileId;
				}
			}

			return result;
		}
	}
}
