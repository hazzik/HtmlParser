namespace HtmlParser
{
    using System.Collections.Generic;
    using System.Diagnostics;

    [DebuggerDisplay("name:{Name}, value:{Value}")]
    public class HtmlNode
    {
        private readonly ICollection<HtmlNode> attributes = new List<HtmlNode>();
        private readonly ICollection<HtmlNode> nodes = new List<HtmlNode>();

        internal HtmlNode(HtmlNodeType nodeType, string name, string value = null)
        {
            NodeType = nodeType;
            Name = name;
            Value = value;
        }

        public HtmlNodeType NodeType { get; private set; }

        public string Name { get; private set; }

        public string Value { get; set; }

        public IEnumerable<HtmlNode> Nodes
        {
            get { return nodes; }
        }

        public IEnumerable<HtmlNode> Attributes
        {
            get { return attributes; }
        }

        public void AddChild(HtmlNode node)
        {
            nodes.Add(node);
        }

        public void AddAttribute(HtmlNode attribute)
        {
            attributes.Add(attribute);
        }
    }
}