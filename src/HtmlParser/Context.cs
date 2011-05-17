namespace HtmlParser
{
    internal class Context
    {
        public Context()
        {
            State = ParseState.Text;
            TokenBuilder = new TokenBuilder(TokenType.Text);
        }

        public ParseState State { get; private set; }

        public TokenBuilder TokenBuilder { get; private set; }

        public void SwitchState(ParseState state, TokenType tokenType)
        {
            SetState(state);
            TokenBuilder = new TokenBuilder(tokenType);
        }

        public void SetState(ParseState state)
        {
            State = state;
        }
    }
}