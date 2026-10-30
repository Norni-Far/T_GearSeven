using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CoreService
{
    public class ItemTapInput : MonoBehaviour, IItemTapInput
    {
        [SerializeField] private Camera _camera;

        public event Action<ItemService.IItem> ItemTapped;

        private void Update()
        {
            var pointer = Pointer.current;
            if (pointer == null || !pointer.press.wasPressedThisFrame)
            {
                return;
            }

            Vector2 worldPoint = _camera.ScreenToWorldPoint(pointer.position.ReadValue());
            var hit = Physics2D.OverlapPoint(worldPoint);

            if (hit != null && hit.TryGetComponent(out ItemService.IItem item))
            {
                ItemTapped?.Invoke(item);
            }
        }
    }
}
