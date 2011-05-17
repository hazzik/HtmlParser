using System;

namespace HtmlParser
{
    internal class Context
    {
        public Context()
        {
            State = ParseState.Default;
            PreviousToken = new Token(TokenType.Text);
            CurrentToken = new Token(TokenType.Text);
        }

        public ParseState State { get; private set; }

        public Token PreviousToken;

        public char PreviousChar { get; set; }

        public Token CurrentToken { get; private set; }

        public void SwitchState(ParseState state, TokenType tokenType)
        {
            State = state;
            PreviousToken = CurrentToken;
            CurrentToken = new Token(tokenType);
        }
    }
}