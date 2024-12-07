namespace AOC_7
{
	public class EquationSolver
	{
		private List<List<double>> _equationsList=new();

		public EquationSolver(string filePath)
		{
			var lines = File.ReadAllLines(filePath);
			int k = 0;

			foreach (var line in lines)
			{
				var split = line.Split(':');
				var testValue = double.Parse(split[0]);

				_equationsList.Add([testValue]);

				foreach (var number in split[1].Split(' ', StringSplitOptions.RemoveEmptyEntries))
				{
					_equationsList[k].Add(int.Parse(number));
				}

				k++;
			}
		}

		public double FindOperatorsAndSum(bool partTwo=false)
		{
			double sum = 0;

			foreach (var equation in _equationsList)
			{
				if (IsSolvable(partTwo, equation))
				{
					sum += equation[0];
				}
			}

			return sum;
		}

		private bool IsSolvable(bool partTwo, List<double> equation,  int index=1, double operationsValue=0)
		{
			if (index == equation.Count)
			{
				return operationsValue == equation[0];
			}

			var number = equation[index];

			if (IsSolvable(partTwo, equation, index + 1, operationsValue + number))
			{
				return true;
			}

			if (IsSolvable(partTwo, equation, index + 1, operationsValue * number))
			{
				return true;
			}

			//Part 2
			if (partTwo)
			{
				var concatenatedNumber = double.Parse($"{operationsValue}{number}");

				if (IsSolvable(partTwo, equation, index + 1, concatenatedNumber))
				{
					return true;
				}
			}

			return false;
		}
	}
}
