using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Node
    {
        public string Data { get; set; }
        public string Parent { get; set; }
        public List<Node> childs { get; set; }

        public Node(string data)
        {
            this.Data = data;
            childs = new List<Node>();
        }
        public Node()
        {
            childs = new List<Node>();
        }

        public void Add(Node item)
        {
            item.Parent = this.Data;
            this.childs.Add(item);
        }
    }
}
