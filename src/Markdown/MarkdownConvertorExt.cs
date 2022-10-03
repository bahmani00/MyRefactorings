namespace Challenges.Markdown;

public static class MarkdownConvertorExt {
  public static readonly string NewLine = Environment.NewLine;

  public static bool IsNullOrEmpty(this string content) {
    return string.IsNullOrEmpty(content);
  }
  public static bool IsNotNullOrEmpty(this string content) {
    return !content.IsNullOrEmpty();
  }

  public static string ApplyHyperLinksIfAny(this string content) {
    var splits = content.Split("()[]".ToCharArray());
    if(splits.Length < 5) return content;

    var numOfLinkes = (int)Math.Ceiling(splits.Length / 5.0);
    var links = new string[numOfLinkes];
    for (int i = 0; i < numOfLinkes; i++) {
      var offsetIdx = i * 5 - i;
      var beforeLink = i == 0 ? splits[offsetIdx] : string.Empty;
      var linkText = splits[offsetIdx + 1];
      var link = splits[offsetIdx + 3];
      var afterLink = splits[offsetIdx + 4];
      links[i] = $"{beforeLink}<a href=\"{link}\">{linkText}</a>{afterLink}";
    }
    return string.Join("", links);
  }

  public static bool IsHeader(this string content, out int header) {
    header = 0;
    if (IsNullOrEmpty(content) || content.Length < 2) return false;

    for(var i = 0; i < content.Length - 1; ++i) {
      if (content[i] != '#') break;

      header++;
    }

    if (header > 0 && header < 7 && header < content.Length && content[header] == ' ') return true;

    return false;
  }
 
  public static string ApplyHeaders(this string content, int headerNum) {
    return $"<h{headerNum}>{content[(headerNum + 1)..].ApplyHyperLinksIfAny()}</h{headerNum}>";
  }

  public static IEnumerable<string> SplitToLines(this string input) {
    if (input.IsNullOrEmpty()) {
      yield break;
    }

    using var reader = new StringReader(input);
    string line;
    while ((line = reader.ReadLine()) != null) {
      yield return line;
    }
  }
}