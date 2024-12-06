using System.Numerics;
using System.Reflection.Metadata.Ecma335;

namespace AOC_6
{
	public class GuardPathPredictor
	{
		private Vector2 _guardPosition = Vector2.Zero;
		private Vector2 _guardDirection = Vector2.Zero;

		private readonly List<List<int>> _map = new();

		private Vector2 _mapSize = Vector2.Zero;

		private int _stepCount = 0;

		//Part 2
		private Vector2 _originalGuardPosition = Vector2.Zero;
		private Vector2 _originalGuardDirection = Vector2.Zero;
		private Dictionary<Vector2, List<Vector2>> _orientationsAtPosition = new ();
		private bool _isObstacleChecking=false;
		private Vector2 _currentObstacle=Vector2.Zero;

		private BinaryTree _decisionTree;

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

			_guardPosition = _originalGuardPosition = new Vector2(guardRow, guardCol);
			_guardDirection = _originalGuardDirection = new Vector2(-1, 0); // up

			_orientationsAtPosition[_guardPosition] = [_guardDirection];

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

			_decisionTree = new BinaryTree(rootNode);
		}

		//Part 2
		public int ObstaclesOptions()
		{
			_stepCount = 0;
			var obstaclesCount = 0;
			_isObstacleChecking = true;

			for (int i = 0; i < _mapSize.X; i++)
			{
				for (int j = 0; j < _mapSize.Y; j++)
				{
					_currentObstacle = new Vector2(i,j);

					_guardPosition = _originalGuardPosition;
					_guardDirection = _originalGuardDirection;
					_orientationsAtPosition.Clear();

					while (Step())
					{
						if (IsLooping())
						{
							obstaclesCount++;
							break;
						}
					}
				}
			}

			return obstaclesCount;
		}

		public int StepsBeforeOut()
		{
			_isObstacleChecking = false;
			_stepCount = 0;

			while (Step()) { }

			return _stepCount;
		}

		private bool Step()
		{
			_decisionTree.Evaluate();
			return !IsOut();
		}

		private bool CanMoveForward()
		{
			var nextPosition = _guardPosition + _guardDirection;

			if (IsOffLimits(nextPosition) || _map[(int)nextPosition.X][(int)nextPosition.Y] is 0 or 7)
			{
				if (_isObstacleChecking)
				{
					if (_currentObstacle == nextPosition)
					{
						return false;
					}
				}
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
			if (!_isObstacleChecking)
			{
				if (_map[(int)_guardPosition.X][(int)_guardPosition.Y] == 0)
				{
					_map[(int)_guardPosition.X][(int)_guardPosition.Y] = 7;
					_stepCount += 1;
				}
			}

			else
			{
				if (!_orientationsAtPosition.ContainsKey(_guardPosition))
				{
					_orientationsAtPosition[_guardPosition] = [_guardDirection];
					_stepCount += 1;
				}
				else
				{
					if (!_orientationsAtPosition[_guardPosition].Contains(_guardDirection))
					{
						_orientationsAtPosition[_guardPosition].Add(_guardDirection);
					}
				}
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

		private bool IsLooping()
		{
			var nextPosition = _guardPosition + _guardDirection;

			if (_orientationsAtPosition.ContainsKey(nextPosition))
			{
				if (_orientationsAtPosition[nextPosition].Contains(_guardDirection))
				{
					return true;
				}
			}
			return false;
		}
	}
}
