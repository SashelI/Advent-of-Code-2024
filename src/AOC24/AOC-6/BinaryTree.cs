namespace AOC_6
{
	public class BinaryTree
	{
		private Node _root=null!;

		public BinaryTree(Node root = null!)
		{
			_root = root;
		}

		public void Add(Node leftNode, Node rightNode = null!)
		{
			if (_root == null!)
			{
				_root = leftNode;
			}
			else
			{
				_root.Add(leftNode);
				_root.Add(rightNode, false);
			}
		}

		public void Evaluate()
		{
			_root.Evaluate();
		}
	}

	public class Node
	{
		public Func<bool> Condition { get; set; } = null;
		public Action Action { get; set; } = null;
		public Node LeftNode { get; set; } = null!;
		public Node RightNode { get; set; } = null!;

		public void Evaluate()
		{
			if (Condition != null)
			{
				if (Condition())
				{
					LeftNode?.Evaluate();
				}
				else
				{
					RightNode?.Evaluate();
				}
			}
			else
			{
				Action?.Invoke();
			}
		}

		public void Add(Node node, bool addLeft=true)
		{
			if (addLeft)
			{
				if (LeftNode == null!)
				{
					LeftNode = node;
				}
				else
				{
					LeftNode.Add(node);
				}
			}
			else
			{
				if (RightNode == null!)
				{
					RightNode = node;
				}
				else
				{
					RightNode.Add(node, false);
				}
			}
		}
	}
}
