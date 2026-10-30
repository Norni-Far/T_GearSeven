using System;
using System.Collections.Generic;

namespace GameLogic
{
    public class ShelfModel
    {
        public const int EmptySlot = -1;

        private readonly int[] _slots;

        public int Capacity => _slots.Length;

        public ShelfModel(IReadOnlyList<int> itemTypes)
        {
            if (itemTypes == null) throw new ArgumentNullException(nameof(itemTypes));

            _slots = new int[itemTypes.Count];
            for (int i = 0; i < itemTypes.Count; i++)
            {
                if (itemTypes[i] < 0) throw new ArgumentException("Item type must be non-negative", nameof(itemTypes));
                _slots[i] = itemTypes[i];
            }
        }

        public int GetItem(int slotIndex)
        {
            return _slots[slotIndex];
        }

        public bool HasItem(int slotIndex)
        {
            return slotIndex >= 0 && slotIndex < _slots.Length && _slots[slotIndex] != EmptySlot;
        }

        public bool TryTake(int slotIndex, out int itemType)
        {
            itemType = EmptySlot;

            if (!HasItem(slotIndex))
            {
                return false;
            }

            itemType = _slots[slotIndex];
            _slots[slotIndex] = EmptySlot;
            return true;
        }
    }
}
