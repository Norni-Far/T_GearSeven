using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace ViewService
{
    public class LevelCompleteView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Button _restartButton;
        [SerializeField] private float _showDelay = 0.4f;
        [SerializeField] private float _fadeDuration = 0.25f;

        public event Action RestartClicked;

        private void Awake()
        {
            _restartButton.onClick.AddListener(OnRestartClicked);
        }

        public void Show()
        {
            _canvasGroup.DOKill();
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.DOFade(1f, _fadeDuration).SetDelay(_showDelay);
        }

        public void Hide()
        {
            _canvasGroup.DOKill();
            _canvasGroup.alpha = 0f;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }

        private void OnRestartClicked()
        {
            RestartClicked?.Invoke();
        }

        private void OnDestroy()
        {
            if (_restartButton != null) _restartButton.onClick.RemoveListener(OnRestartClicked);
            if (_canvasGroup != null) _canvasGroup.DOKill();
        }
    }
}
