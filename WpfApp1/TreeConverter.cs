﻿using System;

namespace WpfApp1
{
	public static class TreeConverter
	{
		static Node rootNode;
		static Node currentNode;

		public static Node ConvertGrammarTreeToOperationTree(Node root)
		{
			rootNode = root;
			currentNode = root;

		step1:
			currentNode = rootNode.GetLeftmostNonTerminal(rootNode, true);
			if (currentNode == null)
			{
				return GetNewRoot();
			}

		step3:
			if (currentNode.HasOnlyOneChild())
			{
				RetargetNode(currentNode);
				goto step1;
			}
			else goto step4;

			step4:
			if (currentNode.HasTerminalChildWithSemanticlessTerminal(out var semanticlessNode))
			{
				currentNode.Children.Remove(semanticlessNode);
				goto step3;
			}
			else goto step5;

			step5:
			if (currentNode.HasOperationChild(out var operationChildNode))
			{
				// todo Здесь может быть проблема, потому что безусловно считаем, что если тут операция, то все остальные символы - операнды
				currentNode.Data = operationChildNode.Data;
				currentNode.Children.Remove(operationChildNode);
				goto step1;
			}
			else goto step6;

			step6:
			currentNode = currentNode.GetLeftmostNonTerminal(currentNode, false);
			if (currentNode == null)
			{
				return GetNewRoot();
			}
			else goto step3;
		}

		private static Node GetNewRoot()
		{
			Node newRoot = new Node();
			newRoot.Data = rootNode.Data;
			newRoot.Children = rootNode.Children;
			return newRoot;
		}

		/// <summary>
		/// Изменяет ссылки на потомков и родителей для текущего узла
		/// </summary>
		/// <param name="node"></param>
		private static void RetargetNode(Node node)
		{
			if (node.Children.Count > 1)
			{
				throw new Exception("Node contains more than one children. Operation is inconsistent!");
			}


			Node currentNodeParent = node.Parent;
			Node childNode = node.Children[0];

			// если это корневая вершина (то есть нет родителя)
			if (currentNodeParent == null)
			{
				// просто спускаемся на уровень ниже
				rootNode = childNode;
				rootNode.Children = childNode.Children;
				rootNode.Parent = null;
				currentNode = rootNode;
			}
			else
			{
				// связать родителя текущего узла с единственным потомком текущего узла
				int index = currentNodeParent.Children.IndexOf(node); // индекс узла в списке узлов его родителя. Важен, потому что надо "подтягивать" узел на то же место
				currentNodeParent.Children[index] = childNode;

				// определить родителем единственного потомка текущего узла родителя текущего узла
				childNode.Parent = currentNodeParent;

				// удалить информацию о текущем узле
				currentNodeParent.Children.Remove(node);

				// сделать текущим родительский узел
				currentNode = currentNodeParent;
			}

		}



	}
}

