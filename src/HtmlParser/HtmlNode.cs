namespace HtmlParser
{
    using System.Collections.Generic;
    using System.Diagnostics;

    [DebuggerDisplay("name:{Name}, value:{Value}")]
    public abstract class HtmlNode
    {
        private readonly ICollection<HtmlNode> attributes = new List<HtmlNode>();
        private readonly ICollection<HtmlNode> childNodes = new List<HtmlNode>();

        protected internal HtmlNode(HtmlNodeType nodeType, string name, string value = null)
        {
            NodeType = nodeType;
            Name = name;
            Value = value;
        }

        public HtmlNodeType NodeType { get; private set; }

        public string Name { get; private set; }

        public string Value { get; set; }

        public IEnumerable<HtmlNode> ChildNodes
        {
            get { return childNodes; }
        }

        public IEnumerable<HtmlNode> Attributes
        {
            get { return attributes; }
        }

        public HtmlNode FirstChild { get; private set; }

        public HtmlNode LastChild { get; private set; }

        public HtmlNode NextSibling { get; private set; }

        public HtmlNode PreviousSibling { get; private set; }

        public HtmlNode ParentNode { get; private set; }

        public void AppendChild(HtmlNode node)
        {
            if (FirstChild == null)
                FirstChild = node;

			node.PreviousSibling = LastChild;

            if (LastChild != null)
                LastChild.NextSibling = node;

			LastChild = node;
            
            node.ParentNode = this;
			
            childNodes.Add(node);
        }

        public void AppendAttribute(HtmlNode attribute)
        {
            attributes.Add(attribute);
        }
    }
}