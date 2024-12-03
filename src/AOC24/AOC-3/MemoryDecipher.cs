using System.Text;
using System.Text.RegularExpressions;

namespace AOC_3
{
	public static class MemoryDecipher
	{
		//Part 1
		public static int DecipherFromFile(string filePath)
		{
			int result = 0;

			var input = File.ReadAllText(filePath);

			Regex reg = new Regex("mul\\([0-9]+,[0-9]+\\)");

			var matches = reg.Matches(input);

			foreach (Match match in matches)
			{
				Regex numbersReg = new Regex("[0-9]+");

				var numbersString = numbersReg.Matches(match.Value);

				var number1 = int.Parse(numbersString[0].Value);
				var number2 = int.Parse(numbersString[1].Value);

				var mul = number1 * number2;

				result += mul;
			}

			return result;
		}

		//Part 2
		public static int DecipherFromFileConditional(string filePath)
		{
			int result = 0;

			var input = File.ReadAllText(filePath);

			Regex reg = new Regex("mul\\([0-9]+,[0-9]+\\)");

			Regex conditionalEnableReg = new Regex("do\\(\\)");
			Regex conditionalDisableReg = new Regex("don't\\(\\)");

			var firstSplit = conditionalEnableReg.Split(input);
			StringBuilder finalSplit = new();

			foreach (string sample in firstSplit)
			{
				var secondSplit = conditionalDisableReg.Split(sample);
				finalSplit.Append(secondSplit[0]);
			}

			var matches = reg.Matches(finalSplit.ToString());

			foreach (Match match in matches)
			{
				Regex numbersReg = new Regex("[0-9]+");

				var numbersString = numbersReg.Matches(match.Value);

				var number1 = int.Parse(numbersString[0].Value);
				var number2 = int.Parse(numbersString[1].Value);

				var mul = number1 * number2;

				result += mul;
			}

			return result;
		}
	}
}
