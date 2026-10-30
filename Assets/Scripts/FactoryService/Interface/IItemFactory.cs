namespace FactoryService
{
    public interface IItemFactory
    {
        ItemService.IItem CreateItem();
    }
}
