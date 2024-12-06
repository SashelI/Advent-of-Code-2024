using System.Numerics;

namespace AOC_6
{
	public class GuardPathPredictor
	{
		private Vector2 _guardPosition = Vector2.Zero;
		private Vector2 _guardDirection = Vector2.Zero;

		private readonly List<List<int>> _map = new();

		private Vector2 _mapSize = Vector2.Zero;

		private int _stepCount = 0;

		public GuardPathPredictor(string filePath)
		{
			var lines = File.ReadAllLines(filePath);

			foreach (var line in lines)
			{
				List<int> mapLine = new();

				foreach (char tile in line)
				{
					if (tile.Equals('.'))
					{
						mapLine.Add(0);
					}
					else if (tile.Equals('#'))
					{
						mapLine.Add(1);
					}
					else if(tile.Equals('^'))
					{
						mapLine.Add(-1);
					}
				}

				_map.Add(mapLine);
			}

			_mapSize.X = _map.Count;
			_mapSize.Y = _map[0].Count;

			int guardCol = 0;

			int guardRow = _map.FindIndex((line) =>
			{
				if (line.Contains(-1))
				{
					guardCol = line.IndexOf(-1);
					line[guardCol]=0;
					return true;
				}

				return false;
			});

			_guardPosition = new Vector2(guardRow, guardCol);
			_guardDirection = new Vector2(-1, 0); // up
		}

		public int StepsBeforeOut()
		{
			_stepCount = 0;

			var rootNode = new Node()
			{
				Condition = CanMoveForward,

				LeftNode = new Node()
				{
					Action = StepForward
				},
				RightNode = new Node()
				{
					Action = TurnRight
				}
			};

			var decisionTree = new BinaryTree(rootNode);

			while (Step(decisionTree)) { }

			return _stepCount;
		}

		private bool Step(BinaryTree decisionTree)
		{
			decisionTree.Evaluate();
			return !IsOut();
		}

		private bool CanMoveForward()
		{
			var nextPosition = _guardPosition + _guardDirection;

			if (IsOffLimits(nextPosition) || _map[(int)nextPosition.X][(int)nextPosition.Y] is 0 or 7)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		private void TurnRight()
		{
			_guardDirection = (_guardDirection.X, _guardDirection.Y) switch
			{
				(-1, 0) => new Vector2(0, 1),
				(0, 1) => new Vector2(1, 0),
				(1, 0) => new Vector2(0, -1),
				(0, -1) => new Vector2(-1, 0)
			};
		}

		private void StepForward()
		{
			if (_map[(int)_guardPosition.X][(int)_guardPosition.Y] == 0)
			{
				_map[(int)_guardPosition.X][(int)_guardPosition.Y] = 7;
				_stepCount += 1;
			}

			_guardPosition += _guardDirection;
		}

		private bool IsOut()
		{
			if (_guardPosition.X < 0 || _guardPosition.X >= _mapSize.X
			                         || _guardPosition.Y < 0 || _guardPosition.Y >= _mapSize.Y)
			{
				return true;
			}

			return false;
		}

		private bool IsOffLimits(Vector2 nextPosition)
		{
			if (nextPosition.X < 0 || nextPosition.X >= _mapSize.X
			                       || nextPosition.Y < 0 || nextPosition.Y >= _mapSize.Y)
			{
				return true;
			}

			return false;
		}
	}
}
