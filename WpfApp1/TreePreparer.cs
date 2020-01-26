using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
	public class TreePreparer
	{
		public static List<int> labels = new List<int>();

		static int firstLabelIndex = 1;
		static int secondLabelIndex = 2;

		public static Node PrepareTree(Node root)
		{
			Node ifStatementNode = FindNodeWithIfStatement(root);
			RebaseNodeWithIfStatement(ifStatementNode.Parent);

			return new Node()
			{
				Data = root.Data,
				Children = root.Children
			};
		}

		public static Node FindNodeWithIfStatement(Node root)
		{
			if (root.Data == "if")
			{
				return root;
			}

			if (root.Children != null && root.Children.Count > 0)
			{
				for (int i = 0; i < root.Children.Count; i++)
				{
					Node ifNode = FindNodeWithIfStatement(root.Children[i]);
					if (ifNode != null)
					{
						return ifNode;
					}
				}

			}

			return null;
		}

		public static void RebaseNodeWithIfStatement(Node rootForIfStatement)
		{
			rootForIfStatement.Data = "EMPTY";
			rootForIfStatement.Label = secondLabelIndex;

			Node ifNode = rootForIfStatement.Children.First(n => n.Data.Equals("if"));
			Node expressionNode = rootForIfStatement.Children.First(n => n.Data.Equals("Expression"));

			ifNode.Data = "УПЛ";
			ifNode.Children.Add(expressionNode);
			ifNode.Children.Add(new Node($"m{firstLabelIndex}", ifNode));
			rootForIfStatement.Children.Remove(expressionNode);

			Node elseNode = rootForIfStatement.Children.First(n => n.Data.Equals("else"));
			elseNode.Data = "БП";
			elseNode.Children.Add(new Node($"m{secondLabelIndex}", elseNode));

			Node statementNode = rootForIfStatement.Children.First(n => n.Data.Equals("Statement"));
			statementNode.Label = firstLabelIndex;

			labels.Add(firstLabelIndex);
			labels.Add(secondLabelIndex);

			firstLabelIndex += 2;
			secondLabelIndex += 2;
		}
	}
}
