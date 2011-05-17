namespace HtmlParser.Tests
{
    using Xunit;

    public class HtmlNodeFacts
    {
        [Fact]
        public void FirstChildOfEmptyElementIsNull()
        {
            var doc = new HtmlDocumentNode();
            var firstChild = doc.FirstChild;
            Assert.Null(firstChild);
        }

        [Fact]
        public void FirstChildIsBr()
        {
            var doc = new HtmlDocumentNode();
            doc.AppendChild(new HtmlElementNode("br"));
            var firstChild = doc.FirstChild;
            Assert.Equal("br", firstChild.Name);
        }

        [Fact]
        public void LastChildOfEmptyElementIsNull()
        {
            var doc = new HtmlDocumentNode();
            var lastChild = doc.LastChild;
            Assert.Null(lastChild);
        }

        [Fact]
        public void LastChildIsBr()
        {
            var doc = new HtmlDocumentNode();
            doc.AppendChild(new HtmlElementNode("br"));
            var lastChild = doc.LastChild;
            Assert.Equal("br", lastChild.Name);
        }

        [Fact]
        public void NextSiblingIsNull()
        {
            var doc = new HtmlDocumentNode();
            var a = new HtmlElementNode("a");
            doc.AppendChild(a);
            var nextSibling = a.NextSibling;
            Assert.Null(nextSibling);
        }

        [Fact]
        public void NextSiblingIsBr()
        {
            var doc = new HtmlDocumentNode();
            var a = new HtmlElementNode("a");
            doc.AppendChild(a);
            doc.AppendChild(new HtmlElementNode("br"));
            var nextSibling = a.NextSibling;
            Assert.Equal("br", nextSibling.Name);
        }

        [Fact]
        public void PreviousSiblingIsNull()
        {
            var doc = new HtmlDocumentNode();
            var a = new HtmlElementNode("a");
            doc.AppendChild(a);
            var previousSibling = a.PreviousSibling;
            Assert.Null(previousSibling);
        }

        [Fact]
        public void PreviousSiblingIsBr()
        {
            var doc = new HtmlDocumentNode();
            var a = new HtmlElementNode("a");
            doc.AppendChild(new HtmlElementNode("br"));
            doc.AppendChild(a);
            var previousSibling = a.PreviousSibling;
            Assert.Equal("br", previousSibling.Name);
        }

        [Fact]
        public void ParentNodeOfRootElementIsNull()
        {
            var doc = new HtmlDocumentNode();
            Assert.Null(doc.ParentNode);
        }

        [Fact]
        public void ParentNodeOfNotRootElementIsNotNull()
        {
            var doc = new HtmlDocumentNode();
            var a = new HtmlElementNode("a");
            doc.AppendChild(new HtmlElementNode("br"));
            Assert.Equal(doc, a.ParentNode);
        }
    }
}