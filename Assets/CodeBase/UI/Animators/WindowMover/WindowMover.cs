using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace UI.Animators.WindowMover
{
    public class WindowMover : IUIAnimator
    {
        private Tweener _currentMoveTween;

        private readonly Vector2 _defaultAnchoredPosition;
        
        private readonly RectTransform _rectTransform;
        private readonly WindowMoverConfig _config;

        public WindowMover(RectTransform rectTransform, WindowMoverConfig config)
        {
            _defaultAnchoredPosition = rectTransform.anchoredPosition;

            _rectTransform = rectTransform;
            _config = config;
        }
        
        public void Enable()
        {
        }

        public void Disable()
        {
            _currentMoveTween?.Kill();
            _rectTransform.anchoredPosition = _defaultAnchoredPosition;
        }
        
        public async UniTask MoveOnWindowOpen()
        {
            _currentMoveTween?.Kill();

            _rectTransform.anchoredPosition = _config.OnOpenedStartPosition;

            Tweener move = GetAnchorPositionMove(_config.OnOpenedDelay,
                _defaultAnchoredPosition,
                _config.OnOpenedDuration,
                _config.OnOpenedEase);

            _currentMoveTween = move;
            await _currentMoveTween.Play();
        }
        
        public async UniTask MoveOnWindowClosed()
        {
            _currentMoveTween?.Kill();

            Tweener move = GetAnchorPositionMove(_config.OnClosedDelay,
                _config.OnClosedEndPosition,
                _config.OnClosedDuration,
                _config.OnClosedEase);

            _currentMoveTween = move;
            await _currentMoveTween.Play();
        }

        private Tweener GetAnchorPositionMove(float delay, Vector2 endPosition, float duration, Ease ease)
        {
            Tweener move = _rectTransform
                .DOAnchorPos(endPosition, duration)
                .SetDelay(delay)
                .SetEase(ease)
                .SetUpdate(true);

            return move;
        }
    }
}