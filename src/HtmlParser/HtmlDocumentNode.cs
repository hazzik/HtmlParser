namespace HtmlParser
{
    public class HtmlDocumentNode : HtmlNode
    {
        internal HtmlDocumentNode() 
            : base(HtmlNodeType.Document, "#document")
        {
        }
    }
}