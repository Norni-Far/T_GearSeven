using DG.Tweening;
using UnityEngine;

namespace AudioService
{
    public class SoundService : MonoBehaviour, ISoundService
    {
        [SerializeField] private AudioSource _source;

        [Space]
        [SerializeField] private AudioClip _tap;
        [SerializeField] private AudioClip _move;
        [SerializeField] private AudioClip _basketPlace;
        [SerializeField] private AudioClip _counterPlace;
        [SerializeField] private AudioClip _basketClosed;
        [SerializeField] private AudioClip _levelComplete;
        [SerializeField] private float _levelCompleteDelay = 0.4f;

        private Tween _levelCompleteTween;

        public void PlayTap() => Play(_tap);
        public void PlayMove() => Play(_move);
        public void PlayBasketPlace() => Play(_basketPlace);
        public void PlayCounterPlace() => Play(_counterPlace);
        public void PlayBasketClosed() => Play(_basketClosed);

        public void PlayLevelComplete()
        {
            _levelCompleteTween?.Kill();
            _levelCompleteTween = DOVirtual.DelayedCall(_levelCompleteDelay, () => Play(_levelComplete));
        }

        public void StopAll()
        {
            _levelCompleteTween?.Kill();
            _source.Stop();
        }

        private void Play(AudioClip clip)
        {
            if (clip != null) _source.PlayOneShot(clip);
        }

        private void OnDestroy()
        {
            _levelCompleteTween?.Kill();
        }
    }
}
