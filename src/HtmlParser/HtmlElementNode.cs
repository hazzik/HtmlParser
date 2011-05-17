namespace HtmlParser
{
    public class HtmlElementNode : HtmlNode
    {
        internal HtmlElementNode(string name) 
            : base(HtmlNodeType.Element, name)
        {
        }
    }
}