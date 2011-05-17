using System;

namespace HtmlParser
{
    internal enum ParseState
    {
        Default,
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