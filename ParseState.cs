using System;

namespace ClassLibrary3
{
    internal enum ParseState
    {
        Default,
        Text,
        Tag,
        AttibuteName
    }
}