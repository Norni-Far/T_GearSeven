using System;

namespace CoreService
{
    public interface IItemTapInput
    {
        event Action<ItemService.IItem> ItemTapped;
    }
}
