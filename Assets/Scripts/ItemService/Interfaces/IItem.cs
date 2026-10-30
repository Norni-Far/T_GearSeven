using System;
using Configs;
using UnityEngine;

namespace ItemService
{
    public interface IItem
    {
        int ShelfSlot { get; }
        void Init(SOItem itemData, int shelfSlot);
        void PlaceAt(Transform point);
        void MoveTo(Transform point, Action onComplete = null);
        void Despawn();
    }
}
