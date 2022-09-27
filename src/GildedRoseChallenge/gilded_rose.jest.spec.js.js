import { Item, update_quality } from './gilded_rose'

describe("update_quality", function() {
  describe('when updating a standard item', () => {
    it("reduces the sell_in by 1", function() {
      const standardItem = new Item('Haunted Shoe', 10, 10);
      update_quality([ standardItem ]);
      expect(standardItem.sell_in).toBe(9);
    });

    it("reduces the quality by 1", function() {
      const standardItem = new Item('Haunted Shoe', 10, 10);
      update_quality([ standardItem ]);
      expect(standardItem.quality).toBe(9);
    });

    it('never reduces the quality below 0', () => {
      const standardItem = new Item('Haunted Shoe', 10, 0);
      update_quality([ standardItem ]);
      expect(standardItem.quality).toBe(0);
    });

    describe('when the sell_in is below 0', () => {
      it('reduces the quality by 2', () => {
        const standardItem = new Item('Haunted Shoe', 0, 10);
        update_quality([ standardItem ]);
        expect(standardItem.quality).toBe(8);
      });
    });
  });

  describe('when updating Aged Brie', () => {
    it('increases the quality by 1', () => {
      const agedBrie = new Item('Aged Brie', 1, 10);
      update_quality([ agedBrie ]);
      expect(agedBrie.quality).toBe(11);
    });

    describe('when the sell_in is below 0', () => {
      it('increases the quality by 2', () => {
        const agedBrie = new Item('Aged Brie', 0, 10);
        update_quality([ agedBrie ]);
        expect(agedBrie.quality).toBe(12);
      });
    });
  });

  describe('when updating Sulfuras', () => {
    it('quality remains at 80', () => {
      const sulfuras = new Item('Sulfuras, Hand of Ragnaros', 0, 80);
      update_quality([ sulfuras ]);
      expect(sulfuras.quality).toBe(80);
    });
  });

  describe('when updating a backstage pass', () => {
    describe('when the sell_in is larger than 10', () => {
      it('increases the quality by 1', () => {
        const backstagePass = new Item('Backstage passes to a TAFKAL80ETC concert', 11, 10);
        update_quality([ backstagePass ]);
        expect(backstagePass.quality).toBe(11);
      });
    });
    describe('when the sell_in is 10 or less', () => {
      it('increases the quality by 2', () => {
        const backstagePass = new Item('Backstage passes to a TAFKAL80ETC concert', 10, 10);
        update_quality([ backstagePass ]);
        expect(backstagePass.quality).toBe(12);
      });
    });
    describe('when the sell_in is 5 or less', () => {
      it('increases the quality by 3', () => {
        const backstagePass = new Item('Backstage passes to a TAFKAL80ETC concert', 5, 10);
        update_quality([ backstagePass ]);
        expect(backstagePass.quality).toBe(13);
      });
    });
    describe('when the sell_in is 0 or less', () => {
      it('reduces the quality to 0', () => {
        const backstagePass = new Item('Backstage passes to a TAFKAL80ETC concert', 0, 10);
        update_quality([ backstagePass ]);
        expect(backstagePass.quality).toBe(0);
      });
    });
  });

  describe('when updating a Conjured item', () => {
    it.skip("always reduces quality by 2", function() {

    });
  });
});