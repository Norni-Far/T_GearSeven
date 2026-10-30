using System;
using System.Collections.Generic;
using System.Linq;
using AudioService;
using Configs;
using FactoryService;
using GameLogic;
using ItemService;
using UnityEngine;
using VContainer;
using ViewService;

namespace CoreService
{
    public class GameProcess : MonoBehaviour
    {
        private IItemFactory _itemFactory;
        private IItemTapInput _tapInput;
        private SOLevel _level;
        private ShelfView _shelfView;
        private BasketView _basketView;
        private CounterView _counterView;
        private LevelCompleteView _levelCompleteView;
        private ISoundService _soundService;

        private readonly List<IItem> _items = new List<IItem>();
        private MatchGame _game;

        [Inject]
        public void Construct(IItemFactory itemFactory, IItemTapInput tapInput, SOLevel level,
            ShelfView shelfView, BasketView basketView, CounterView counterView, LevelCompleteView levelCompleteView,
            ISoundService soundService)
        {
            _itemFactory = itemFactory;
            _tapInput = tapInput;
            _level = level;
            _shelfView = shelfView;
            _basketView = basketView;
            _counterView = counterView;
            _levelCompleteView = levelCompleteView;
            _soundService = soundService;
        }

        private void Start()
        {
            StartLevel();
            _tapInput.ItemTapped += OnItemTapped;
            _levelCompleteView.RestartClicked += RestartLevel;
        }

        private void OnDestroy()
        {
            if (_tapInput != null) _tapInput.ItemTapped -= OnItemTapped;
            if (_levelCompleteView != null) _levelCompleteView.RestartClicked -= RestartLevel;
        }

        private void StartLevel()
        {
            if (!_level.TryValidate(out string error))
            {
                throw new InvalidOperationException($"{_level.name}: {error}");
            }

            _game = new MatchGame(_level.ShelfItems.Select(item => item.ItemID).ToArray(), _level.BasketOrder.ItemID);
            _basketView.ResetView();
            _basketView.SetOrder(_level.BasketOrder);
            _levelCompleteView.Hide();

            for (int i = 0; i < _level.ShelfItems.Length; i++)
            {
                var item = _itemFactory.CreateItem();
                item.Init(_level.ShelfItems[i], i);
                item.PlaceAt(_shelfView.GetSlot(i));
                _items.Add(item);
            }
        }

        private void RestartLevel()
        {
            foreach (var item in _items)
            {
                item.Despawn();
            }

            _items.Clear();
            _soundService.StopAll();
            StartLevel();
        }

        private void OnItemTapped(IItem item)
        {
            var result = _game.TryMove(item.ShelfSlot, out int targetSlot);
            if (result == MoveResult.Rejected)
            {
                return;
            }

            _soundService.PlayTap();
            _soundService.PlayMove();

            switch (result)
            {
                case MoveResult.MovedToBasket:
                    item.MoveTo(_basketView.GetSlot(targetSlot), _soundService.PlayBasketPlace);
                    break;
                case MoveResult.BasketClosed:
                    item.MoveTo(_basketView.GetSlot(targetSlot), OnBasketClosed);
                    break;
                case MoveResult.MovedToCounter:
                    item.MoveTo(_counterView.GetSlot(targetSlot), _soundService.PlayCounterPlace);
                    break;
            }
        }

        private void OnBasketClosed()
        {
            _soundService.PlayBasketPlace();
            _soundService.PlayBasketClosed();
            _soundService.PlayLevelComplete();
            _basketView.PlayClose();
            _levelCompleteView.Show();
        }
    }
}
