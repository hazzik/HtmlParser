namespace HtmlParser.Tests
{
    using System.Linq;
    using Xunit;

    public class TokenParserCDataFacts
    {
        [Fact]
        public void ScriptContentIsCData()
        {
            const string html = "<script>//<br/></script>";
            var tokens = TokenParser.Parse(html);
            Assert.Equal(3, tokens.Count());
        } 
    }
}