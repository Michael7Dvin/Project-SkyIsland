using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace UI.Animators.WindowMover
{
    public class WindowMover : IUIAnimator
    {
        private Tween _currentMoveTween;

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

            _rectTransform.anchoredPosition = _config.OnOpenStartPosition;

            Tween move = GetAnchorPositionMove(_defaultAnchoredPosition,
                _config.OnOpenDuration,
                _config.OnOpenEase);

            _currentMoveTween = move;
            
            await UniTask.Delay(TimeSpan.FromSeconds(_config.OnOpenDelay), DelayType.UnscaledDeltaTime);
            await _currentMoveTween.Play();
        }
        
        public async UniTask MoveOnWindowClosed()
        {
            _currentMoveTween?.Kill();

            Tween move = GetAnchorPositionMove(_config.OnClosedEndPosition,
                _config.OnClosedDuration,
                _config.OnClosedEase);

            _currentMoveTween = move;
            
            await UniTask.Delay(TimeSpan.FromSeconds(_config.OnCloseDelay), DelayType.UnscaledDeltaTime);
            await _currentMoveTween.Play();
        }
        
        private Tween GetAnchorPositionMove(Vector2 endPosition, float duration, Ease ease)
        {
            Tween move = _rectTransform
                .DOAnchorPos(endPosition, duration)
                .SetEase(ease)
                .SetUpdate(true);

            return move;
        }
    }
}