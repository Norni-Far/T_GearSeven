using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace ItemService
{
    public class ItemHolder : MonoBehaviour, IItemHolder
    {
        [SerializeField] private string _itemsResourcesPath = "SO/Items";
        [SerializeField] private List<SOItem> _items = new List<SOItem>();

        [Button]
        public void UpdateItemList()
        {
            _items.Clear();
            _items.AddRange(Resources.LoadAll<SOItem>(_itemsResourcesPath));

            int index = 0;
            foreach (var item in _items)
            {
                Debug.Log(item.ItemID);
                item.SetItemID(index);
                index++;
            }

            if (_items.Count > 0)
            {
                Debug.Log("Items loaded successfully = " + _items.Count);
            }
            else
            {
                Debug.LogError("No items found");
            }
        }

        public List<SOItem> GetItems()
        {
            return _items;
        }
    }
}