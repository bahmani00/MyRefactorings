using System.Text;
using static Challenges.Markdown.MarkdownConvertorExt;

namespace Challenges.Markdown;

public class MarkdownConvertor {
  private readonly string markup;

  public MarkdownConvertor(string markup) {
    this.markup = markup;
  }
  public string MarkUp() {
    if(markup.IsNullOrEmpty()) return markup;

    var lines = markup.SplitToLines().ToArray();
    var markedUpLines = new List<string>();
    var queue = new Queue<string>();
    foreach (var line in lines) {
      if (line.IsHeader(out int headerNum)) {
        ApplyParagraphs(markedUpLines, queue);
        markedUpLines.Add(line.ApplyHeaders(headerNum));
      } else if (line.IsNotNullOrEmpty()) {
        queue.Enqueue(line);
      } else {
        ApplyParagraphs(markedUpLines, queue);
      }
    }

    ApplyParagraphs(markedUpLines, queue);

    return String.Join(NewLine, markedUpLines);
  }

  private static void ApplyParagraphs(List<string> markedUpLines, Queue<string> queue) {
    if (queue.Count <= 0) return;

    var sb = new StringBuilder();
    sb.Append($"<p>");
    while (queue.Count > 0) {
      sb.Append(queue.Dequeue().ApplyHyperLinksIfAny());
      if (queue.Count > 0)
        sb.Append(NewLine);
    }
    sb.Append($"</p>");

    markedUpLines.Add(sb.ToString());
  }
}
