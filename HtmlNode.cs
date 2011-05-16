using System;
using System.Collections.Generic;

namespace ClassLibrary3
{
    internal class HtmlNode
    {
        private readonly ICollection<HtmlNode> nodes = new List<HtmlNode>();

        public string Name { get; set; }

        public IEnumerable<HtmlNode> Nodes
        {
            get { return nodes; }
        }

        public void AddChild(HtmlNode node)
        {
            nodes.Add(node);
        }
    }
}