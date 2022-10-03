
using Challenges.Markdown;
using static Challenges.Markdown.MarkdownConvertorExt;

namespace Test.Markdown;

public class MarkdownConvertorParagraphedTest {

  [Theory()]
  [InlineData(null, null)]
  [InlineData("", "")]
  public void Markdown_empty_returns_empty(string input, string expected) {
    var convertor = new MarkdownConvertor(input);

    var actual = convertor.MarkUp();
    
    Assert.Equal(expected, actual);
  }

  [Theory()]
  [InlineData("abc", "<p>abc</p>")]
  [InlineData(" abc  ", "<p> abc  </p>")]
  [InlineData("abc d", "<p>abc d</p>")]
  public void Markdown_raw_input_returns_paragraphed_markup(string input, string expected) {
    var convertor = new MarkdownConvertor(input);

    var actual = convertor.MarkUp();

    Assert.Equal(expected, actual);
  }

  [Fact]
  public void Markdown_one_empty_line_input_returns_empty_line() {
    var convertor = new MarkdownConvertor($"{NewLine}");
    var actual = convertor.MarkUp();
    Assert.Equal("", actual);

    convertor = new MarkdownConvertor($"{NewLine}{NewLine}");
    actual = convertor.MarkUp();
    Assert.Equal("", actual);

    convertor = new MarkdownConvertor($"{NewLine}{NewLine}{NewLine}");
    actual = convertor.MarkUp();
    Assert.Equal("", actual);

    convertor = new MarkdownConvertor($"{NewLine}{NewLine}{NewLine}{NewLine}");
    actual = convertor.MarkUp();
    Assert.Equal("", actual);
  }

  [Fact]
  public void Markdown_no_empty_lines_raw_input_returns_paragraphed_raw_markup() {
    var input = $"Hey!{NewLine}How are you?";
    var convertor = new MarkdownConvertor(input);

    var actual = convertor.MarkUp();
    var expected = $"<p>{input}</p>";

    Assert.Equal(expected, actual);
  }

  [Fact]
  public void Markdown_three_lines_raw_input_returns_paragraphed_three_lines_raw_markup() {
    var input = $"Hey!{NewLine}How are you?{NewLine}What's going on?";
    var convertor = new MarkdownConvertor(input);

    var actual = convertor.MarkUp();
    var expected = $"<p>{input}</p>";

    Assert.Equal(expected, actual);
  }

  [Fact]
  public void Markdown_two_lines_raw_with_two_empty_line_input_returns_paragraphed_four_lines_raw_markup() {
    var input = $"Hey!{NewLine}How are you?{NewLine}{NewLine}";

    var convertor = new MarkdownConvertor(input);

    var actual = convertor.MarkUp();
    var expected = $"<p>Hey!{NewLine}How are you?</p>";

    Assert.Equal(expected, actual);
  }

  [Fact]
  public void Markdown_three_lines_with_one_empty_line_raw_input_returns_paragraphed_three_lines_raw_markup() {
    var input = $"Hey!{NewLine}{NewLine}How are you?";

    var convertor = new MarkdownConvertor(input);

    var actual = convertor.MarkUp();
    var expected = $"<p>Hey!</p>{NewLine}<p>How are you?</p>";

    Assert.Equal(expected, actual);
  }

  [Fact]
  public void Markdown_four_lines_with_two_empty_line_raw_input_returns_paragraphed_four_lines_raw_markup() {
    var input = $"Hey!{NewLine}{NewLine}How are you?";

    var convertor = new MarkdownConvertor(input);

    var actual = convertor.MarkUp();
    var expected = $"<p>Hey!</p>{NewLine}<p>How are you?</p>";

    Assert.Equal(expected, actual);
  }

  [Fact]
  public void Markdown_two_lines_and_one_empty_line_raw_input_returns_paragraphed_four_lines_raw_markup() {
    var input = $"Hey!{NewLine}How are you?{NewLine}{NewLine}What's going on?";

    var convertor = new MarkdownConvertor(input);

    var actual = convertor.MarkUp();
    var expected = $"<p>Hey!{NewLine}How are you?</p>{NewLine}<p>What's going on?</p>";

    Assert.Equal(expected, actual);
  }
}