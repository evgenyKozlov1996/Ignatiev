using System;
using System.Collections.Generic;
using System.Linq;

namespace WpfApp1
{
    public class Node
    {
        public string Data { get; set; }
        public Node Parent { get; set; }
        public List<Node> Children { get; set; }

        public Node(string data)
        {
            this.Data = data;
            Children = new List<Node>();
        }

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
            return (Children != null && Children.Count > 0);
        }

        public bool IsOperation()
        {
            string[] ops = new string[] { "==", "!=", "+", "-", "*", "/", "<", ">", "<=", ">=", "&&", "||" };
            return ops.Contains(Data);
        }

        /// <summary>
        /// Определяет, содержит ли узел терминал, не несущий никакой семантической нагрузки
        /// </summary>
        /// <returns></returns>
        public bool IsSemanticlessTerminal()
        {
            string[] terms = new string[] { "(", ")", "{", "}" };
            return terms.Contains(Data);
        }

        public bool HasOnlyOneChild()
        {
            return Children.Count == 1;
        }

        public bool HasNonTerminals()
        {
            foreach (Node node in Children)
            {
                if (node.IsNonTerminal())
                {
                    return true;
                }
            }
            return false;
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
            operationChildNode = null;
            for (int i = 0; i < Children.Count; i++)
            {
                if (Children[i].IsOperation())
                {
                    operationChildNode = Children[i];
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Возвращает самый левый нетерминальный дочерний узел. или <see langword="null"/>.
        /// </summary>
        /// <returns></returns>
        public Node GetLeftmostNonTerminalChild()
        {
            for (int i = 0; i < Children.Count; i++)
            {
                if (Children[i].IsNonTerminal())
                {
                    return Children[i];
                }
            }

            return null;
        }
    }
}
