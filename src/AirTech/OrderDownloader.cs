using Newtonsoft.Json.Linq;

namespace Challenges.AirTech;

public interface IOrderDownloader {
  List<Order> Fetch();
}

//public class OrderAmazonS3Downloader : IOrderDownloader {}

public class OrderFileDownloader : IOrderDownloader {
  private readonly string filePath;

  public OrderFileDownloader(string filePath) {
    this.filePath = filePath;
  }

  public List<Order> Fetch() {
    var result = new List<Order>();
    //size of the file? too much on GC(Large object heap (LOH))
    var allOrders = File.ReadAllText(filePath);
    var jObject = JObject.Parse(allOrders);
    int i = 1;
    foreach (JProperty e in jObject.Properties()) {
      var orderNum = e.Name;
      var destination = e.Value["destination"].ToString();

      result.Add(new(orderNum, destination, i++));
    }
    return result;
  }
}
