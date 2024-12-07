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

		public double FindOperatorsAndSum()
		{
			double sum = 0;

			foreach (var equation in _equationsList)
			{
				if (IsSolvable(equation))
				{
					sum += equation[0];
				}
			}

			return sum;
		}

		private bool IsSolvable(List<double> equation,  int index=1, double operationsValue=0)
		{
			if (index == equation.Count)
			{
				return operationsValue == equation[0];
			}

			if (IsSolvable(equation, index + 1, operationsValue + equation[index]))
			{
				return true;
			}

			if (IsSolvable(equation, index + 1, operationsValue * equation[index]))
			{
				return true;
			}

			return false;
		}
	}
}
