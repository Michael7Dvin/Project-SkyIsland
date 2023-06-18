using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace UI.Animators.WindowScaler
{
    public class WindowScaler : IUIAnimator
    {
        private Tween _currentScaleTween;
        
        private readonly Transform _transform;
        private readonly WindowScalerConfig _config;
        
        public WindowScaler(Transform transform, WindowScalerConfig config)
        {
            _transform = transform;
            _config = config;
        }
        
        public void Enable()
        {
        }

        public void Disable()
        {
            _currentScaleTween?.Kill();
            _transform.localScale = _config.DefaultScale;
        }

        public async UniTask ScaleOnWinodwOpen()
        {
            _currentScaleTween?.Kill();

            _transform.localScale = _config.OnOpenedStartScale;

            Tween scaleAnimation = GetScale(_config.OnOpenedEndScale,
                _config.OnOpenedDuration,
                _config.OnOpenedEase);

            _currentScaleTween = scaleAnimation;

            await UniTask.Delay(TimeSpan.FromSeconds(_config.OnOpenDelay), DelayType.UnscaledDeltaTime);
            await _currentScaleTween.Play();
        }
        
        public async UniTask ScaleOnWindowClosed()
        {
            _currentScaleTween?.Kill();

            Tween scaleAnimation = GetScale(_config.OnClosedEndScale,
                _config.OnClosedDuration,
                _config.OnClosedEase);

            _currentScaleTween = scaleAnimation;
            
            await UniTask.Delay(TimeSpan.FromSeconds(_config.OnCloseDelay), DelayType.UnscaledDeltaTime);
            await _currentScaleTween.Play();
        }
        
        private Tween GetScale(Vector3 endScale, float duration, Ease ease)
        {
            Tween scale = _transform
                .DOScale(endScale, duration)
                .SetEase(ease)
                .SetUpdate(true);

            return scale;
        }
    }
}