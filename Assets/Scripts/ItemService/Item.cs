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
        [SerializeField] private float _jumpPower = 1f;

        [Space]
        [SerializeField] private float _tapSquash = 0.25f;
        [SerializeField] private float _tapSquashDuration = 0.2f;
        [SerializeField] private Vector2 _landSquash = new Vector2(0.2f, 0.25f);
        [SerializeField] private float _landSquashDuration = 0.25f;

        [Space]
        [SerializeField] private float _flightTilt = 20f;
        [SerializeField] private int _flightSortingBoost = 10;

        private Vector3 _baseScale;
        private int _baseSortingOrder;
        private Sequence _sequence;

        private void Awake()
        {
            _baseScale = transform.localScale;
            _baseSortingOrder = _spriteRenderer.sortingOrder;
        }

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
            KillAnimation();
            transform.SetParent(point, false);
            transform.localPosition = Vector3.zero;
        }

        public void MoveTo(Transform point, Action onComplete = null)
        {
            KillAnimation();
            transform.SetParent(point, true);

            float direction = point.position.x >= transform.position.x ? 1f : -1f;
            float tiltTime = _moveDuration * 0.4f;
            _spriteRenderer.sortingOrder = _baseSortingOrder + _flightSortingBoost;

            _sequence = DOTween.Sequence()
                .Join(CreateSquash(Vector3.one * (1f - _tapSquash), _tapSquashDuration))
                .Join(transform.DOLocalJump(Vector3.zero, _jumpPower, 1, _moveDuration).SetEase(Ease.OutQuad))
                .Join(transform.DOLocalRotate(new Vector3(0f, 0f, -direction * _flightTilt), tiltTime).SetEase(Ease.OutQuad))
                .Insert(tiltTime, transform.DOLocalRotate(Vector3.zero, _moveDuration - tiltTime).SetEase(Ease.InOutQuad))
                .AppendCallback(() =>
                {
                    _spriteRenderer.sortingOrder = _baseSortingOrder;
                    onComplete?.Invoke();
                })
                .Append(CreateSquash(new Vector3(1f + _landSquash.x, 1f - _landSquash.y, 1f), _landSquashDuration));
        }

        public void Despawn()
        {
            Destroy(gameObject);
        }

        private Sequence CreateSquash(Vector3 squash, float duration)
        {
            return DOTween.Sequence()
                .Append(transform.DOScale(Vector3.Scale(_baseScale, squash), duration * 0.3f).SetEase(Ease.OutQuad))
                .Append(transform.DOScale(_baseScale, duration * 0.7f).SetEase(Ease.OutBack, 3f));
        }

        private void KillAnimation()
        {
            _sequence?.Kill();
            transform.localScale = _baseScale;
            transform.localRotation = Quaternion.identity;
            _spriteRenderer.sortingOrder = _baseSortingOrder;
        }

        private void OnDestroy()
        {
            _sequence?.Kill();
        }
    }
}
