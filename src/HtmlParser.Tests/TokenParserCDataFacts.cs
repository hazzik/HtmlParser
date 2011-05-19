namespace HtmlParser.Tests
{
    using System.Linq;
    using Xunit;

    public class TokenParserCDataFacts
    {
        [Fact]
        public void ScriptContentIsCData()
        {
            const string html = "<script>//<br></script>";
            var tokens = TokenParser.Parse(html);
        	Assert.Equal(3, tokens.Count());
        }
        
		[Fact]
        public void ScriptCDataParsedCorrectly()
        {
            const string html = "<script>//<br></script>";
            var tokens = TokenParser.Parse(html).ToList();
		
			Assert.Equal(TokenType.OpenTag, tokens[0].Type);
			Assert.Equal("script", tokens[0].Value);
			Assert.Equal(TokenType.Text, tokens[1].Type);
			Assert.Equal("//<br>", tokens[1].Value);
			Assert.Equal(TokenType.CloseTag, tokens[2].Type);
			Assert.Equal("script", tokens[2].Value);
        } 
        
		[Fact]
        public void StyleContentIsCData()
        {
			const string html = "<style>//<br></style>";
            var tokens = TokenParser.Parse(html);
        	Assert.Equal(3, tokens.Count());
        }

		[Fact(Skip = "not now")]
		public void ScriptWithAttributes()
		{
			const string html = "<script type=\"text/javascript\">//<br></script>";
			var tokens = TokenParser.Parse(html);
			Assert.Equal(5, tokens.Count());
		} 
	}
}