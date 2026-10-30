using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "Level", menuName = "Configs/Level", order = 0)]
    public class SOLevel : ScriptableObject
    {
        [field: SerializeField] public SOItem BasketOrder { get; private set; }
        [field: SerializeField] public SOItem[] ShelfItems { get; private set; } = new SOItem[GameLogic.MatchGame.ShelfCapacity];

        public bool TryValidate(out string error)
        {
            var itemsById = new Dictionary<int, SOItem>();

            if (!TryRegister(BasketOrder, itemsById, out error))
            {
                return false;
            }

            foreach (var item in ShelfItems)
            {
                if (!TryRegister(item, itemsById, out error))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool TryRegister(SOItem item, Dictionary<int, SOItem> itemsById, out string error)
        {
            if (item == null)
            {
                error = "Level has an empty item reference";
                return false;
            }

            if (itemsById.TryGetValue(item.ItemID, out var other) && other != item)
            {
                error = $"'{item.name}' and '{other.name}' share ItemID {item.ItemID}";
                return false;
            }

            itemsById[item.ItemID] = item;
            error = null;
            return true;
        }

        private void OnValidate()
        {
            if (!TryValidate(out string error))
            {
                Debug.LogWarning($"{name}: {error}", this);
            }
        }
    }
}
