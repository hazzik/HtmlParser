namespace HtmlParser
{
    public class HtmlCommentNode : HtmlNode
    {
        internal HtmlCommentNode(string value) 
            : base(HtmlNodeType.Comment, "#comment", value)
        {
        }
    }
}