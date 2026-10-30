using System;
using System.Collections.Generic;
using System.Linq;

namespace GameLogic
{
    public enum MoveResult
    {
        Rejected,
        MovedToBasket,
        MovedToCounter,
        BasketClosed
    }

    public class MatchGame
    {
        public const int ShelfCapacity = 6;
        public const int BasketCapacity = 3;
        public const int CounterCapacity = 5;
        public const int ItemTypesCount = 3;

        public ShelfModel Shelf { get; }
        public BasketModel Basket { get; }
        public CounterModel Counter { get; }

        public MatchGame(IReadOnlyList<int> shelfItems, int basketOrder)
        {
            if (shelfItems == null) throw new ArgumentNullException(nameof(shelfItems));
            if (shelfItems.Count != ShelfCapacity)
                throw new ArgumentException($"Shelf must contain {ShelfCapacity} items", nameof(shelfItems));
            if (shelfItems.Distinct().Count() != ItemTypesCount)
                throw new ArgumentException($"Shelf must contain {ItemTypesCount} item types", nameof(shelfItems));

            int orderedCount = shelfItems.Count(type => type == basketOrder);
            if (orderedCount < BasketCapacity)
                throw new ArgumentException($"Shelf must contain {BasketCapacity} items of the basket order", nameof(basketOrder));
            if (shelfItems.Count - orderedCount > CounterCapacity)
                throw new ArgumentException($"Counter can hold only {CounterCapacity} wrong items", nameof(shelfItems));

            Shelf = new ShelfModel(shelfItems);
            Basket = new BasketModel(BasketCapacity, basketOrder);
            Counter = new CounterModel(CounterCapacity);
        }

        public MoveResult TryMove(int shelfSlot, out int targetSlot)
        {
            targetSlot = -1;

            if (Basket.IsClosed || !Shelf.TryTake(shelfSlot, out int itemType))
            {
                return MoveResult.Rejected;
            }

            if (Basket.TryAdd(itemType, out targetSlot))
            {
                return Basket.IsClosed ? MoveResult.BasketClosed : MoveResult.MovedToBasket;
            }

            Counter.TryAdd(itemType, out targetSlot);
            return MoveResult.MovedToCounter;
        }
    }
}
