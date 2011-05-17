namespace HtmlParser
{
    public class HtmlTextNode : HtmlNode
    {
        internal HtmlTextNode(string value) 
            : base(HtmlNodeType.Text, "#text", value)
        {
        }
    }
}