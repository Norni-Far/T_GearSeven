using System;
using Configs;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace ItemService
{
    public class Item : MonoBehaviour, IItem
    {
        [field: SerializeField][field: ReadOnly] public SOItem ItemData { get; private set; }
        [field: SerializeField][field: ReadOnly] public int ShelfSlot { get; private set; }

        [Space]
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private float _moveDuration = 0.3f;

        public void Init(SOItem itemData, int shelfSlot)
        {
            ItemData = itemData;
            ShelfSlot = shelfSlot;

            UpdateView();
        }

        private void UpdateView()
        {
            _spriteRenderer.sprite = ItemData.ItemIcon;
        }

        public void PlaceAt(Transform point)
        {
            transform.SetParent(point, false);
            transform.localPosition = Vector3.zero;
        }

        public void MoveTo(Transform point, Action onComplete = null)
        {
            transform.DOKill();
            transform.SetParent(point, true);
            transform.DOLocalJump(Vector3.zero, 1f, 1, _moveDuration)
                .SetEase(Ease.OutQuad)
                .OnComplete(() => onComplete?.Invoke());
        }

        public void Despawn()
        {
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            transform.DOKill();
        }
    }
}
