namespace HtmlParser
{
    using System.Collections.Generic;
    using System.Diagnostics;

    [DebuggerDisplay("{Name}")]
    internal class HtmlNode
    {
        private readonly ICollection<HtmlAttribute> attributes = new List<HtmlAttribute>();
        private readonly ICollection<HtmlNode> nodes = new List<HtmlNode>();

        public HtmlNode(HtmlNodeType nodeType, string name)
        {
            NodeType = nodeType;
            Name = name;
        }

        public HtmlNodeType NodeType { get; private set; }

        public string Name { get; private set; }

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