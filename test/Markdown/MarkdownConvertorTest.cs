
using Challenges.Markdown;

namespace Test.Markdown;

public class MarkdownConvertorTest {

  [Theory]
  [MemberData(nameof(PowerOfTestData))]
  public void Markdown_complex_marked_downs_examples_returns_HTML_markups(string input, string expected) {
    var convertor = new MarkdownConvertor(input);

    var actual = convertor.MarkUp();

    Assert.Equal(expected, actual);
  }

  public static IEnumerable<object[]> PowerOfTestData() {
    yield return new object[] { input1, expected1 };
    yield return new object[] { input2, expected2 };
    yield return new object[] { input3, expected3 };
    yield return new object[] { File.ReadAllText(@"Markdown\marked-down-1.txt"), File.ReadAllText(@"Markdown\marked-up-1.txt") };
  }


  static readonly string input1 = @"# Sample Document
Hello!
This is sample markdown for the [Mailchimp](https://www.mailchimp.com) homework assignment." + Environment.NewLine;
  static readonly string expected1 = @"<h1>Sample Document</h1>
<p>Hello!
This is sample markdown for the <a href=""https://www.mailchimp.com"">Mailchimp</a> homework assignment.</p>";


  static readonly string input2 = @"# Sample Document
Hello!

This is sample markdown for the [Mailchimp](https://www.mailchimp.com) homework assignment." + Environment.NewLine;
  static readonly string expected2 = @"<h1>Sample Document</h1>
<p>Hello!</p>
<p>This is sample markdown for the <a href=""https://www.mailchimp.com"">Mailchimp</a> homework assignment.</p>";


  static readonly string input3 = @"# Header one

Hello there

How are you?
What's going on?

## Another Header

This is a paragraph [with an inline link](http://google.com). Neat, eh?

## This is a header [with a link](http://yahoo.com)" + Environment.NewLine;
  static readonly string expected3 = @"<h1>Header one</h1>
<p>Hello there</p>
<p>How are you?
What's going on?</p>
<h2>Another Header</h2>
<p>This is a paragraph <a href=""http://google.com"">with an inline link</a>. Neat, eh?</p>
<h2>This is a header <a href=""http://yahoo.com"">with a link</a></h2>";

}