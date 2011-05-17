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

        public Token SwitchState(ParseState state, TokenType tokenType)
        {
            SetState(state);
            var token = TokenBuilder.Create();
            TokenBuilder = new TokenBuilder(tokenType);
            return token;
        }

        public void SetState(ParseState state)
        {
            State = state;
        }
    }
}