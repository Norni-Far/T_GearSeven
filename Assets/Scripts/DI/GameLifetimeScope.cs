using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DI
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private Configs.SOLevel _level;

        [SerializeField] private FactoryService.ItemFactory _itemFactory;
        [SerializeField] private CoreService.ItemTapInput _tapInput;
        [SerializeField] private AudioService.SoundService _soundService;
        [SerializeField] private ViewService.ShelfView _shelfView;
        [SerializeField] private ViewService.BasketView _basketView;
        [SerializeField] private ViewService.CounterView _counterView;
        [SerializeField] private ViewService.LevelCompleteView _levelCompleteView;
        [SerializeField] private CoreService.GameProcess _gameProcess;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_itemFactory).As<FactoryService.IItemFactory>();
            builder.RegisterInstance(_tapInput).As<CoreService.IItemTapInput>();
            builder.RegisterInstance(_soundService).As<AudioService.ISoundService>();

            builder.RegisterInstance(_level);

            builder.RegisterInstance(_shelfView);
            builder.RegisterInstance(_basketView);
            builder.RegisterInstance(_counterView);
            builder.RegisterInstance(_levelCompleteView);
            builder.RegisterComponent(_gameProcess);
        }
    }
}
