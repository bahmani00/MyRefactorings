using System.Diagnostics;
using BenchmarkDotNet.Attributes;

namespace Challenges.Markdown;

[MemoryDiagnoser]
public class MarkdownConvertorSample {
  /*
  |                  Method |      Mean |    Error |   StdDev |
  |------------------------ |----------:|---------:|---------:|
  |                  MarkUp | 231.81 us | 1.780 us | 1.665 us |
  |           Split_BuiltIn |  64.51 us | 0.499 us | 0.442 us |
  | Split_UsingStringReader |  49.53 us | 0.345 us | 0.288 us |
  Mean      : Arithmetic mean of all measurements
  Error     : Half of 99.9% confidence interval
  StdDev    : Standard deviation of all measurements
  Gen0      : GC Generation 0 collects per 1000 operations
  Gen1      : GC Generation 1 collects per 1000 operations
  Allocated : Allocated memory per single operation (managed only, inclusive, 1KB = 1024B)
  1 us      : 1 Microsecond (0.000001 sec)
   */
  private readonly static string markedDown;
  private readonly static string markedUp;

  static MarkdownConvertorSample() {
    markedDown = File.ReadAllText(@"Markdown\marked-down-2.txt");
    markedUp = File.ReadAllText(@"Markdown\marked-up-2.txt");
  }

  public static void Start() {
    var convertor = new MarkdownConvertor(markedDown);

    var actual = convertor.MarkUp();

    Debug.Assert(markedUp == actual);
  }
   

  [Benchmark]
  public void MarkUp() {
    var convertor = new MarkdownConvertor(markedDown);
    var actual = convertor.MarkUp();
    //Debug.Assert(markedUp == actual);
  }

  //[Benchmark]
  public string[] Split_BuiltIn() =>
    markedDown.Split(new[] { MarkdownConvertorExt.NewLine }, StringSplitOptions.None);
  
 // [Benchmark]
  public string[] Split_UsingStringReader() =>
     markedDown.SplitToLines().ToArray();
}