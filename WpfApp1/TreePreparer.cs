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
			Node ifStatementNode = FindNode(root, "if");
			if (ifStatementNode != null)
			{
				RebaseNodeWithIfStatement(ifStatementNode.Parent);
			}

			Node forNode = FindNode(root, "for");
			if (forNode != null)
			{
				RebaseNodeWithForStatement(forNode.Parent);
			}
			

			return new Node()
			{
				Data = root.Data,
				Children = root.Children
			};
		}

		private static Node FindNode(Node root, string dataToSeek)
		{
			if (root.Data.Equals(dataToSeek))
			{
				return root;
			}

			if (root.Children != null && root.Children.Count > 0)
			{
				for (int i = 0; i < root.Children.Count; i++)
				{
					Node node = FindNode(root.Children[i], dataToSeek);
					if (node != null)
					{
						return node;
					}
				}

			}

			return null;
		}


		private static void RebaseNodeWithForStatement(Node rootOfForStatement)
		{
			RemoveNodesWithSemicolons(rootOfForStatement);
			RemoveNodesWithParanthesis(rootOfForStatement);

			rootOfForStatement.Data = "EMPTY";

			Node forNode = rootOfForStatement.Children.First(n => n.Data.Equals("for"));
			rootOfForStatement.Children.Remove(forNode);

			Node conditionNode = rootOfForStatement.Children.First(n => n.Data.Equals("For Condition Opt"));			
			conditionNode.Data = "УПЛ";
			conditionNode.Children.Add(new Node($"m{secondLabelIndex}", conditionNode));

			// перетасовка итератора и тела цикла
			Node iteratorOpt = rootOfForStatement.Children.First(n => n.Data.Equals("For Iterator Opt"));
			Node statementOpt = rootOfForStatement.Children.First(n => n.Data.Equals("Statement"));

			int indexOfIterator = rootOfForStatement.Children.IndexOf(iteratorOpt);
			int indexOfStatement = rootOfForStatement.Children.IndexOf(statementOpt);

			var buf = iteratorOpt;
			rootOfForStatement.Children[indexOfIterator] = statementOpt;
			rootOfForStatement.Children[indexOfStatement] = buf;

			// добавляем БП в конец цикла
			Node bpNode = new Node($"БП", rootOfForStatement);
			bpNode.Children.Add(new Node($"m{firstLabelIndex}", bpNode));
			rootOfForStatement.Children.Add(bpNode);

			firstLabelIndex += 2;
			secondLabelIndex += 2;

		}
		private static void RebaseNodeWithIfStatement(Node rootForIfStatement)
		{
			RemoveNodesWithParanthesis(rootForIfStatement);

			rootForIfStatement.Data = "EMPTY";
			rootForIfStatement.Label = secondLabelIndex;

			Node ifNode = rootForIfStatement.Children.First(n => n.Data.Equals("if"));
			Node expressionNode = rootForIfStatement.Children.First(n => n.Data.Equals("Expression"));

			ifNode.Data = "УПЛ";
			expressionNode.Parent = ifNode;
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

		private static void RemoveNodesWithParanthesis(Node root)
		{
			Node paranStartNode = root.Children.First(i => i.Data == "(");
			Node paranEndNode = root.Children.First(i => i.Data == ")");

			root.Children.Remove(paranStartNode);
			root.Children.Remove(paranEndNode);
		}

		private static void RemoveNodesWithSemicolons (Node root)
		{
			var nodesWithSemicolon = root.Children.Where(i => i.Data.Equals(";")).ToList();
			for (int i = 0; i < nodesWithSemicolon.Count; i++)
			{
				root.Children.Remove(nodesWithSemicolon[i]);
			}
		}
	}
}
