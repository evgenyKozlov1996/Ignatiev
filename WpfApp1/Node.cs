﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace WpfApp1
{
    public class Node
    {			
        public string Data { get; set; }
        public Node Parent { get; set; }
        public List<Node> Children { get; set; }

		public int Label { get; set; }

        public Node(string data, Node parent)
        {
            this.Data = data;
            this.Parent = parent;
            Children = new List<Node>();
        }

        public Node()
        {
            Children = new List<Node>();
        }

        public void Add(Node item)
        {
            //item.Parent = this.Data;
            this.Children.Add(item);
        }

        public bool IsNonTerminal()
        {
			string[] terminals = new string[] { "EMPTY", "УПЛ", "БП" };
			if (terminals.Contains(Data))
				return false;

			// todo так себе решение. 
			bool hasChildren = Children != null && Children.Count > 0;

			if (hasChildren)
			{				
				if (IsOperation())
				{
					return false;
				}
				else return true;
			}
			else return false;
        }

        public bool IsOperation()
        {
            string[] ops = new string[] { "==", "!=", "+", "-", "*", "/", "<", ">", "<=", ">=", "&&", "||", "=" };
            return ops.Contains(Data);
        }

		/// <summary>
		/// Определяет, содержит ли узел терминал, не несущий никакой семантической нагрузки
		/// </summary>
		/// <returns></returns>
		public bool IsSemanticlessTerminal()
		{
			string[] identifiers = new string[] { "var" };
            string[] terms = new string[] { "(", ")", "{", "}", ";", "function main(input)"};
			return terms.Contains(Data) || identifiers.Contains(Data);
        }

        public bool HasOnlyOneChild()
        {
            return Children.Count == 1;
        }

		public Node GetLeftmostNonTerminal(Node root, bool checkCurrent)
        {
            if (checkCurrent == true && root.IsNonTerminal())
            {
                return root;
            }

            if (root.Children != null && root.Children.Count > 0)
            {
                for (int i = 0; i < root.Children.Count; i++)
                {
                    Node nonTerminalChild = GetLeftmostNonTerminal(root.Children[i], true);
                    if (nonTerminalChild != null)
                    {
                        return nonTerminalChild;
                    }
                }

            }

            return null;
        }

        /// <summary>
        /// Определяет, есть ли у текущего узла дочерний узел, в котором содержится терминал, не несущий никакой семантической нагрузки.
        /// </summary>
        /// <returns></returns>
        public bool HasTerminalChildWithSemanticlessTerminal(out Node nodeWithSemanticlessTerminal)
        {
            nodeWithSemanticlessTerminal = null;
            for (int i = 0; i < Children.Count; i++)
            {
                if (Children[i].IsNonTerminal())
                {
                    continue;
                }
                else
                if (Children[i].IsSemanticlessTerminal())
                {
                    nodeWithSemanticlessTerminal = Children[i];
                    return true;
                }

            }

            return false;
        }

        /// <summary>
        /// Определяет, есть ли среди нижестоящих узлов узел, в котором содержится операция, при том что соседние узлы являются операндами.
        /// </summary>
        /// <param name="indexOfOperationChildNode"></param>
        /// <returns></returns>
        public bool HasOperationChild(out Node operationChildNode)
        {
			bool result = false;
            operationChildNode = null;
            for (int i = 0; i < Children.Count; i++)
            {				
                if (Children[i].IsOperation())
                {
                    operationChildNode = Children[i];
					result = true;
					break;
                }
            }

            return result;
        }

    }
}
