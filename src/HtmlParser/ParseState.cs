using System;

namespace HtmlParser
{
    internal enum ParseState
    {
        Text,
        Tag,
        AttibuteName,
        AttibuteValueBegin,
        AttibuteValue,
        DoubleQuotedAttibuteValue,
        SingleQuotedAttibuteValue,
        WhaitForTagOrComment,
        WaitForSecondOpenMinus,
        Comment,
        WaitForSecondCloseMinus,
        WaitForGt
    }
}