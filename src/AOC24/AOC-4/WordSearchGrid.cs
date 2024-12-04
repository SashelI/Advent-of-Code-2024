using System.Text.RegularExpressions;

namespace AOC_4
{
	public class WordSearchGrid
	{
		private readonly List<List<char>> _gridMatrix=new();
		private readonly List<string> _grid = new();
		private readonly int _lineLength = 0;

		public WordSearchGrid(string filePath)
		{
			var lines = File.ReadAllLines(filePath);
			_lineLength = lines[0].Length;

			foreach (var line in lines)
			{
				List<char> charLine = line.ToCharArray().ToList();
				_gridMatrix.Add(charLine);

				_grid.Add(line);
			}
		}

		public int FindOccurences(string word, string reversedWord)
		{
			int count = 0;

			foreach (var line in _grid)
			{
				count += Regex.Matches(line, word).Count;
				count += Regex.Matches(line, reversedWord).Count;
			}

			count += FindVerticalOccurences(word);
			count += FindVerticalOccurences(reversedWord);

			count += FindDiagonalOccurencesLeftRight(word.ToCharArray());
			count += FindDiagonalOccurencesRightLeft(word.ToCharArray());

			count += FindDiagonalOccurencesLeftRight(reversedWord.ToCharArray());
			count += FindDiagonalOccurencesRightLeft(reversedWord.ToCharArray());

			return count;
		}

		private int FindDiagonalOccurencesLeftRight(char[] word)
		{
			int count = 0;
			int rows = _grid.Count;

			for (int i = 0; i <= rows - word.Length; i++)
			{
				for (int j = 0; j <= _lineLength - word.Length; j++)
				{
					bool match = !word.Where((t, k) => _gridMatrix[i + k][j + k] != t).Any();


					if (match)
					{
						count++;
					}
				}
			}

			return count;
		}

		private int FindDiagonalOccurencesRightLeft(char[] word)
		{
			int count = 0;
			int rows = _grid.Count;

			for (int i = 0; i <= rows - word.Length; i++)
			{
				for (int j = _lineLength - 1; j >= word.Length - 1; j--)
				{
					bool match = !word.Where((t, k) => _gridMatrix[i + k][j - k] != t).Any();


					if (match)
					{
						count++;
					}
				}
			}

			return count;
		}

		private int FindVerticalOccurences(string word)
		{
			int count = 0;
			int rows = _grid.Count;

			for (int j = 0; j < _lineLength; j++)
			{
				for (int i = 0; i <= rows - word.Length; i++)
				{
					bool match = !word.Where((t, k) => _gridMatrix[i + k][j] != t).Any();

					if (match)
					{
						count++;
					}
				}
			}

			return count;
		}
	}
}
