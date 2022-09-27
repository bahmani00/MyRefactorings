﻿namespace Console.GildedRoseChallenge;

// Item constructor. DO NOT MODIFY OR THE GOBLIN WILL EAT YOU!
public class Item {
  public Item(string name, int sellIn, int quality) {
    Name = name;
    SellIn = sellIn;
    Quality = quality;
  }

  public string Name { get; }
  public int SellIn { get; set; }
  public int Quality { get; set; }
}

/*
* Update inventory
* @param {Item[]} items - an array of Items representing the inventory to be updated
* Example usage:

const items = [
  new Item('+5 Dexterity Vest', 10, 20),
  new Item('Aged Brie', 2, 0),
  new Item('Elixir of the Mongoose', 5, 7),
  new Item('Sulfuras, Hand of Ragnaros', 0, 80),
  new Item('Backstage passes to a TAFKAL80ETC concert', 15, 20),
  new Item('Conjured Mana Cake', 3, 6),
];

update_Quality(items);
*/
public class InventoryItems {
  public void UpdateQuality(Item[] items) {
    for (var i = 0; i < items.Length; i++) {
      UpdateQuality(items[i]);      
    }
  }

  public void UpdateQuality(Item item) {
    if (IsLegendary(item)) return;

    //TODO: coordinate with GOBLIN to have a new method(DecrementSellIn) whithing Item class
    item.SellIn--;

    if (IsAgedBrie(item)) {
      UpdateAgedBrie(item);
      return;
    }

    if (IsBackstagePass(item)) {
      UpdateBackstagePass(item);
      return;
    }

    var newQuality = -1;
    if (item.SellIn < 0)
      newQuality--;

    if (IsConjured(item)) {
      newQuality *= 2;
    }

    UpdateQuality(item, newQuality);
  }

  private void UpdateBackstagePass(Item item) {
    var newQuality = 1;
    if (item.SellIn < 0) {
      newQuality = -item.Quality;
    } else {
      var todaySellIn = item.SellIn + 1;
      newQuality += todaySellIn < 11 ? 1 : 0;
      newQuality += todaySellIn < 6 ? 1 : 0;
    }
    UpdateQuality(item, newQuality);
  }

  private void UpdateAgedBrie(Item item) {
    var newQuality = 1;
    if (item.SellIn < 0)
      newQuality++;
    UpdateQuality(item, newQuality);
  }

  private void UpdateQuality(Item item, int newQuality) =>
    //TODO: coordinate with GOBLIN to have a new method(UpdateQuality) whithing Item class
    item.Quality = Math.Min(50, Math.Max(0, item.Quality + newQuality));

  private static bool IsLegendary(Item item) => 
    item.Name == "Sulfuras, Hand of Ragnaros";

  private static bool IsAgedBrie(Item item) => 
    item.Name == "Aged Brie";

  private static bool IsBackstagePass(Item item) =>
    item.Name == "Backstage passes to a TAFKAL80ETC concert";

  private static bool IsConjured(Item item) =>
    item.Name == "Conjured";
}
