
using Challenges.GildedRoseChallenge;

namespace Test.GildedRose;

public class UpdateQualityTest {
  [Fact]
  public void Update_reduces_SellIn_by_One() {
    var item = new Item("Haunted Shoe", 10, 10);
    var inventoryItems = new InventoryItems();

    inventoryItems.UpdateQuality(new[] { item });
    Assert.Equal(9, item.SellIn);
  }

  [Fact]
  public void Update_reduces_quality_by_1() {
    var item = new Item("Haunted Shoe", 10, 10);
    var inventoryItems = new InventoryItems();

    inventoryItems.UpdateQuality(new[] { item });
    Assert.Equal(9, item.Quality);
  }

  [Fact]
  public void Update_never_reduces_quality_below_0() {
    var item = new Item("Haunted Shoe", 10, 0);
    var inventoryItems = new InventoryItems();

    inventoryItems.UpdateQuality(new[] { item });
    Assert.Equal(0, item.Quality);
  }


  [Fact]
  public void Update_SellIn_is_below_0_reduces_the_quality_by_2() {
    var item = new Item("Haunted Shoe", 0, 10);
    var inventoryItems = new InventoryItems();

    inventoryItems.UpdateQuality(new[] { item });
    Assert.Equal(8, item.Quality);
  }

  [Fact]
  public void Update_Aged_Brie_increases_the_quality_by_1() {
    var item = new Item("Aged Brie", 1, 10);
    var inventoryItems = new InventoryItems();

    inventoryItems.UpdateQuality(new[] { item });
    Assert.Equal(11, item.Quality);
  }

  [Fact]
  public void Update_Aged_Brie_SellIn_is_below_0_increases_the_quality_by_2() {
    var item = new Item("Aged Brie", 0, 10);
    var inventoryItems = new InventoryItems();

    inventoryItems.UpdateQuality(new[] { item });
    Assert.Equal(12, item.Quality);
  }

  [Fact]
  public void Update_Sulfuras_quality_remains_at_80() {
    var item = new Item("Sulfuras, Hand of Ragnaros", 0, 80);
    var inventoryItems = new InventoryItems();

    inventoryItems.UpdateQuality(new[] { item });
    Assert.Equal(80, item.Quality);
  }

  [Fact]
  public void Update_Backstage_Pass_when_the_SellIn_is_larger_than_10_increases_the_quality_by_1() {
    var item = new Item("Backstage passes to a TAFKAL80ETC concert", 11, 10);
    var inventoryItems = new InventoryItems();

    inventoryItems.UpdateQuality(new[] { item });
    Assert.Equal(11, item.Quality);
  }

  [Fact]
  public void Update_Backstage_Pass_when_the_SellIn_is_10_or_less_increases_the_quality_by_2() {
    var item = new Item("Backstage passes to a TAFKAL80ETC concert", 10, 10);
    var inventoryItems = new InventoryItems();

    inventoryItems.UpdateQuality(new[] { item });
    Assert.Equal(12, item.Quality);
  }

  [Fact]
  public void Update_BackstagePass_not_passes_50() {
    var item = new Item("Backstage passes to a TAFKAL80ETC concert", 5, 48);
    var inventoryItems = new InventoryItems();

    inventoryItems.UpdateQuality(new[] { item });
    Assert.Equal(50, item.Quality);
  }

  [Fact]
  public void Update_Backstage_Pass_when_the_SellIn_is_5_or_less_increases_the_quality_by_3() {
    var item = new Item("Backstage passes to a TAFKAL80ETC concert", 5, 10);
    var inventoryItems = new InventoryItems();

    inventoryItems.UpdateQuality(new[] { item });
    Assert.Equal(13, item.Quality);
  }

  [Fact]
  public void Update_Backstage_Pass_when_the_SellIn_is_0_or_less_reduces_the_quality_to_0() {
    var item = new Item("Backstage passes to a TAFKAL80ETC concert", 0, 10);
    var inventoryItems = new InventoryItems();

    inventoryItems.UpdateQuality(new[] { item });
    Assert.Equal(0, item.Quality);
  }


  [Fact]
  public void Update_Conjured_reduces_quality_by_2() {
    var item = new Item("Conjured", 10, 10);
    var inventoryItems = new InventoryItems();

    inventoryItems.UpdateQuality(new[] { item });
    Assert.Equal(8, item.Quality);
  }


  [Fact]
  public void Update_Conjured_SellIn_is_below_0_reduces_the_quality_by_4() {
    var item = new Item("Conjured", 0, 10);
    var inventoryItems = new InventoryItems();

    inventoryItems.UpdateQuality(new[] { item });
    Assert.Equal(6, item.Quality);
  }
}