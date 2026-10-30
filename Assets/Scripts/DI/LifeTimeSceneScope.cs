using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DI
{
    public class LifeTimeSceneScope : LifetimeScope
    {
        [SerializeField] private ItemService.ItemHolder _itemHolder;
        [SerializeField] private CoreService.GameProcess _gameProcess;
        [SerializeField] private CoreService.LevelCreator _levelCreator;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_itemHolder).As<IItemHolder>();

            //
            builder.RegisterComponent(_levelCreator);
        }
    }
}