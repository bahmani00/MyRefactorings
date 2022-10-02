
using Challenges.Markdown;

namespace Test.Markdown;

public class MarkdownConvertorHyperLinkTest {

  [Theory()]
  [InlineData($"[link](http://shrt.com)", $"<p><a href=\"http://shrt.com\">link</a></p>")]
  [InlineData($"[link ](http://shrt.com)", $"<p><a href=\"http://shrt.com\">link </a></p>")]
  [InlineData($"[link](http://shrt.com) test", $"<p><a href=\"http://shrt.com\">link</a> test</p>")]
  [InlineData($"test [link](http://shrt.com) test", $"<p>test <a href=\"http://shrt.com\">link</a> test</p>")]
  public void Markdown_valid_link_returns_HyperLinked_markup(string input, string expected) {
    var convertor = new MarkdownConvertor(input);

    var actual = convertor.MarkUp();

    Assert.Equal(expected, actual);
  }

  [Theory()]
  [InlineData($"link(http://shrt.com)")]
  [InlineData($"[link]http://shrt.com)")]
  [InlineData($"[link](http://shrt.com")]
  [InlineData($"link](http://shrt.com")]
  public void Markdown_invalid_links_returns_paragraphed_markup(string input) {
    var convertor = new MarkdownConvertor(input);

    var actual = convertor.MarkUp();
    var expected = $"<p>{input}</p>";

    Assert.Equal(expected, actual);
  }

  [Fact]
  public void Markdown_two_links_returns_two_HyperLinked_markup() {
    var input = @"[link1](http://shrt1.com) [link2](http://shrt2.com)";
    var convertor = new MarkdownConvertor(input);

    var actual = convertor.MarkUp();
    var expected = $"<p><a href=\"http://shrt1.com\">link1</a> <a href=\"http://shrt2.com\">link2</a></p>";

    Assert.Equal(expected, actual);
  }

  [Fact]
  public void Markdown_three_links_returns_three_HyperLinked_markup() {
    var input = @"[link1](http://shrt1.com) [link2](http://shrt2.com) [link3](http://shrt3.com)";
    var convertor = new MarkdownConvertor(input);

    var actual = convertor.MarkUp();
    var expected = $"<p><a href=\"http://shrt1.com\">link1</a> <a href=\"http://shrt2.com\">link2</a> <a href=\"http://shrt3.com\">link3</a></p>";

    Assert.Equal(expected, actual);
  }

}