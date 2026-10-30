using System;
using System.Collections.Generic;

namespace GameLogic
{
    public class BasketModel
    {
        private readonly List<int> _items;

        public int Capacity { get; }
        public int RequiredType { get; }
        public bool IsClosed { get; private set; }
        public IReadOnlyList<int> Items => _items;
        public bool IsFull => _items.Count == Capacity;
        public bool CanAdd => !IsClosed && !IsFull;

        public BasketModel(int capacity, int requiredType)
        {
            if (capacity <= 0) throw new ArgumentOutOfRangeException(nameof(capacity));
            if (requiredType < 0) throw new ArgumentOutOfRangeException(nameof(requiredType));

            Capacity = capacity;
            RequiredType = requiredType;
            _items = new List<int>(capacity);
        }

        public bool Accepts(int itemType)
        {
            return CanAdd && itemType == RequiredType;
        }

        public bool TryAdd(int itemType, out int slotIndex)
        {
            slotIndex = -1;

            if (!Accepts(itemType))
            {
                return false;
            }

            slotIndex = _items.Count;
            _items.Add(itemType);
            IsClosed = IsFull;
            return true;
        }
    }
}
