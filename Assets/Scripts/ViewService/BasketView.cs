using Configs;
using DG.Tweening;
using UnityEngine;

namespace ViewService
{
    public class BasketView : MonoBehaviour
    {
        [SerializeField] private Transform _root;
        [SerializeField] private SpriteRenderer _orderIcon;
        [SerializeField] private Transform[] _slots = new Transform[GameLogic.MatchGame.BasketCapacity];

        [Space]
        [SerializeField] private ParticleSystem _closeParticles;
        [SerializeField] private float _inflateScale = 0.25f;
        [SerializeField] private float _inflateDuration = 0.12f;
        [SerializeField] private float _deflateDuration = 0.4f;
        [SerializeField] private float _shakeAngle = 12f;

        [Space]
        [SerializeField] private float _popScale = 0.15f;
        [SerializeField] private float _popDuration = 0.15f;
        [SerializeField] private float _hideDuration = 0.25f;

        private Vector3 _baseScale;
        private Quaternion _baseRotation;
        private Sequence _sequence;

        private void Awake()
        {
            _baseScale = _root.localScale;
            _baseRotation = _root.localRotation;
        }

        public Transform GetSlot(int index)
        {
            return _slots[index];
        }

        public void SetOrder(SOItem order)
        {
            _orderIcon.sprite = order.ItemIcon;
        }

        public void PlayClose()
        {
            ResetView();

            _sequence = DOTween.Sequence()
                .Append(_root.DOScale(_baseScale * (1f + _inflateScale), _inflateDuration).SetEase(Ease.OutQuad))
                .Append(_root.DOScale(_baseScale, _deflateDuration).SetEase(Ease.OutBack))
                .Join(_root.DOShakeRotation(_deflateDuration, new Vector3(0f, 0f, _shakeAngle), 14, 90f, true, ShakeRandomnessMode.Harmonic))
                .Append(_root.DOScale(_baseScale * (1f + _popScale), _popDuration).SetEase(Ease.OutQuad))
                .AppendCallback(() => { if (_closeParticles != null) _closeParticles.Play(); })
                .Append(_root.DOScale(Vector3.zero, _hideDuration).SetEase(Ease.InBack))
                .AppendCallback(() => _root.gameObject.SetActive(false));
        }

        public void ResetView()
        {
            _sequence?.Kill();
            _root.gameObject.SetActive(true);
            _root.localScale = _baseScale;
            _root.localRotation = _baseRotation;
        }

        private void OnDestroy()
        {
            _sequence?.Kill();
        }
    }
}
