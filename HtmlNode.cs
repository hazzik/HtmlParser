using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ClassLibrary3
{
    [DebuggerDisplay("{Name}")]
    internal class HtmlNode
    {
        private readonly ICollection<HtmlAttribute> attributes = new List<HtmlAttribute>();
        private readonly ICollection<HtmlNode> nodes = new List<HtmlNode>();

        public string Name { get; set; }

        public IEnumerable<HtmlNode> Nodes
        {
            get { return nodes; }
        }

        public IEnumerable<HtmlAttribute> Attributes
        {
            get { return attributes; }
        }

        public void AddChild(HtmlNode node)
        {
            nodes.Add(node);
        }

        public void AddAttribute(HtmlAttribute attribute)
        {
            attributes.Add(attribute);
        }
    }
}
