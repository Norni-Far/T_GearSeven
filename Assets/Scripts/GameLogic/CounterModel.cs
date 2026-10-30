using System;
using System.Collections.Generic;

namespace GameLogic
{
    public class CounterModel
    {
        private readonly List<int> _items;

        public int Capacity { get; }
        public IReadOnlyList<int> Items => _items;
        public bool IsFull => _items.Count == Capacity;

        public CounterModel(int capacity)
        {
            if (capacity <= 0) throw new ArgumentOutOfRangeException(nameof(capacity));

            Capacity = capacity;
            _items = new List<int>(capacity);
        }

        public bool TryAdd(int itemType, out int slotIndex)
        {
            slotIndex = -1;

            if (IsFull)
            {
                return false;
            }

            slotIndex = _items.Count;
            _items.Add(itemType);
            return true;
        }
    }
}
