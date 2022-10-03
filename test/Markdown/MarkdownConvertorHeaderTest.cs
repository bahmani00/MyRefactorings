
using Challenges.Markdown;
using static Challenges.Markdown.MarkdownConvertorExt;

namespace Test.Markdown;

public class MarkdownConvertorHeaderTest {

  [Theory()]
  [InlineData($"# Header 1", $"<h1>Header 1</h1>")]
  [InlineData($"# #Header 1", $"<h1>#Header 1</h1>")]
  [InlineData($"## Header 2", $"<h2>Header 2</h2>")]
  [InlineData($"## #Header 2", $"<h2>#Header 2</h2>")]
  [InlineData($"### Header 3", $"<h3>Header 3</h3>")]
  [InlineData($"### #Header 3", $"<h3>#Header 3</h3>")]
  [InlineData($"#### Header 4", $"<h4>Header 4</h4>")]
  [InlineData($"##### Header 5", $"<h5>Header 5</h5>")]
  [InlineData($"###### Header 6", $"<h6>Header 6</h6>")]
  public void Markdown_valid_header_returns_Headered_markup(string input, string expected) {
    var convertor = new MarkdownConvertor(input);

    var actual = convertor.MarkUp();

    Assert.Equal(expected, actual);
  }

  [Theory()]
  [InlineData($"####### Header 7")]
  [InlineData($"######## Header 8")]
  [InlineData($"######### Header 9")]
  [InlineData($"########## Header 10")]
  public void Markdown_invalid_headers_returns_paragraphed_markup(string input) {
    var convertor = new MarkdownConvertor(input);

    var actual = convertor.MarkUp();
    var expected = $"<p>{input}</p>";

    Assert.Equal(expected, actual);
  }

  [Fact]
  public void Markdown_two_header_one_returns_two_H_One_markup() {
    var input = $"# Header one{NewLine}# Another Header one";

    var convertor = new MarkdownConvertor(input);

    var actual = convertor.MarkUp();
    var expected = $"<h1>Header one</h1>{NewLine}<h1>Another Header one</h1>";

    Assert.Equal(expected, actual);
  }

  [Fact]
  public void Markdown_one_header1_and_one_header2_returns_header1_and_one_header2_markup() {
    var input = $"# Header one{NewLine}## Header two";

    var convertor = new MarkdownConvertor(input);

    var actual = convertor.MarkUp();
    var expected = $"<h1>Header one</h1>{NewLine}<h2>Header two</h2>";

    Assert.Equal(expected, actual);
  }
}