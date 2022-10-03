
using Challenges.Markdown;
using static Challenges.Markdown.MarkdownConvertorExt;

namespace Test.Markdown;

public class MarkdownConvertorHeaderWithParaghraphTest {

  [Fact]
  public void Markdown_two_header_one_with_one_line_returns_two_H_One_with_paragraphed_line_markup() {
    var input = $"# Header one{NewLine}test{NewLine}# Another Header one";

    var convertor = new MarkdownConvertor(input);

    var actual = convertor.MarkUp();
    var expected = $"<h1>Header one</h1>{NewLine}<p>test</p>{NewLine}<h1>Another Header one</h1>";

    Assert.Equal(expected, actual);
  }

}