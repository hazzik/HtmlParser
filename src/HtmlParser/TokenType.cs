using System;

namespace HtmlParser
{
    internal enum TokenType
    {
        OpenTag,
        CloseTag,
        AttributeName,
        AttributeValue,
        Text,
    }
}