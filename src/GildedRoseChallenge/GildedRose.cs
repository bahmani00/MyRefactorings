namespace Console.GildedRoseChallenge;

// Item constructor. DO NOT MODIFY OR THE GOBLIN WILL EAT YOU!
public class Item {
  public Item(string name, int sellIn, int quality) {
    Name = name;
    SellIn = sellIn;
    Quality = quality;
  }

  public string Name { get; set; }
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
      if (items[i].Name != "Aged Brie" && items[i].Name != "Backstage passes to a TAFKAL80ETC concert") {
        if (items[i].Quality > 0) {
          if (items[i].Name != "Sulfuras, Hand of Ragnaros") {
            items[i].Quality = items[i].Quality - 1;
          }
        }
      } else {
        if (items[i].Quality < 50) {
          items[i].Quality = items[i].Quality + 1;
          if (items[i].Name == "Backstage passes to a TAFKAL80ETC concert") {
            if (items[i].SellIn < 11) {
              if (items[i].Quality < 50) {
                items[i].Quality = items[i].Quality + 1;
              }
            }
            if (items[i].SellIn < 6) {
              if (items[i].Quality < 50) {
                items[i].Quality = items[i].Quality + 1;
              }
            }
          }
        }
      }
      if (items[i].Name != "Sulfuras, Hand of Ragnaros") {
        items[i].SellIn = items[i].SellIn - 1;
      }
      if (items[i].SellIn < 0) {
        if (items[i].Name != "Aged Brie") {
          if (items[i].Name != "Backstage passes to a TAFKAL80ETC concert") {
            if (items[i].Quality > 0) {
              if (items[i].Name != "Sulfuras, Hand of Ragnaros") {
                items[i].Quality = items[i].Quality - 1;
              }
            }
          } else {
            items[i].Quality = items[i].Quality - items[i].Quality;
          }
        } else {
          if (items[i].Quality < 50) {
            items[i].Quality = items[i].Quality + 1;
          }
        }
      }
    }
  }
}
