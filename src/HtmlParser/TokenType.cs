namespace HtmlParser
{
    internal enum TokenType
    {
        OpenTag,
        CloseTag,
        AttributeName,
        AttributeValue,
        Text,
        Comment
    }
}