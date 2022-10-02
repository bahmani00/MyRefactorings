using System.Text;

namespace Challenges.Markdown;

public class MarkdownConvertor {
  private readonly string markup;

  public MarkdownConvertor(string markup) {
    this.markup = markup;
  }
  public string MarkUp() {
    if(markup.IsNullOrEmpty()) return markup;

    //var lines = markup.Split(new[] { TagHelper.NewLine }, StringSplitOptions.None);
    var lines = markup.SplitToLines().ToArray();
    var sb = new List<string>();
    var queue = new Queue<string>();
    for (var i = 0; i < lines.Length; i++) {
      var line = lines[i];
      if (line.IsHeader(out int headerNum)) {
        ProcessQueue(sb, queue);
        sb.Add(line.ApplyHeaders(headerNum));
      } else if (!line.IsNullOrEmpty()) {
        queue.Enqueue(line);
      }

      if (line.IsNullOrEmpty()) {
        ProcessQueue(sb, queue);
        //sb.Add(String.Empty);
        //HandleNextEmptyLines(lines, sb, ref i);
      }
    }

    ProcessQueue(sb, queue);

    return String.Join(TagHelper.NewLine, sb);
  }

#pragma warning disable IDE0051
  private static void HandleNextEmptyLines(string[] lines, List<string> sb, ref int i) {

    while (i < lines.Length && lines[i].IsNullOrEmpty()) {
      sb.Add(String.Empty);
      i++;
    }
    if(i < lines.Length) {
      sb.RemoveAt(sb.Count - 1);
    }

    i--;
    return;
  }
#pragma warning restore IDE0051

  private static void ProcessQueue(List<string> res, Queue<string> queue) {
    if (queue.Count <= 0) return;

    var sb = new StringBuilder();
    sb.Append($"<p>");
    while (queue.Count > 0) {
      sb.Append(queue.Dequeue().ApplyHyperLinksIfAny());
      if (queue.Count > 0)
        sb.Append(TagHelper.NewLine);
    }
    sb.Append($"</p>");

    res.Add(sb.ToString());
  }
}

public static class TagHelper {
  public static readonly string NewLine = Environment.NewLine;
  public static readonly string EmptyLine = $"{NewLine}{NewLine}";

  public static bool IsNullOrEmpty(this string content) {
    return string.IsNullOrEmpty(content);
  }
  public static bool IsNotNullOrEmpty(this string content) {
    return !content.IsNullOrEmpty();
  }
  public static bool IsEmptyLine(this string content) {
    return content == EmptyLine;
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