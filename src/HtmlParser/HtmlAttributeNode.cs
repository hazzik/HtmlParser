namespace HtmlParser
{
    public class HtmlAttributeNode : HtmlNode
    {
        internal HtmlAttributeNode(string name) 
            : base(HtmlNodeType.Attribute, name)
        {
        }
    }
}