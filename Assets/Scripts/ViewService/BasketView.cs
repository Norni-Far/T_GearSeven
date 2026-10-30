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
        [SerializeField] private float _punchScale = 0.3f;
        [SerializeField] private float _punchDuration = 0.4f;

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
            _root.DOKill(true);
            _root.DOPunchScale(Vector3.one * _punchScale, _punchDuration, 8, 0.6f);

            if (_closeParticles != null) _closeParticles.Play();
        }

        public void ResetView()
        {
            _root.DOKill(true);
        }

        private void OnDestroy()
        {
            if (_root != null) _root.DOKill();
        }
    }
}
