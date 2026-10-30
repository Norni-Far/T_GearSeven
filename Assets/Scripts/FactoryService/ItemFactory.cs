using UnityEngine;

namespace FactoryService
{
    public class ItemFactory : MonoBehaviour, IItemFactory
    {
        [SerializeField] private ItemService.Item _itemPrefab;

        public ItemService.IItem CreateItem()
        {
            return Instantiate(_itemPrefab);
        }
    }
}
