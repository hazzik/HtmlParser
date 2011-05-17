namespace HtmlParser
{
    internal class Context
    {
        public Context()
        {
            State = ParseState.Text;
            CurrentToken = new Token(TokenType.Text);
        }

        public ParseState State { get; private set; }

        public Token CurrentToken { get; private set; }

        public void SwitchState(ParseState state, TokenType tokenType)
        {
            SetState(state);
            CurrentToken = new Token(tokenType);
        }

        public void SetState(ParseState state)
        {
            State = state;
        }
    }
}