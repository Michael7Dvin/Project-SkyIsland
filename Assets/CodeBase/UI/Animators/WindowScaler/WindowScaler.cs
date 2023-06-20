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

        public async UniTask ScaleOnWindowOpen()
        {
            _currentScaleTween?.Kill();

            _transform.localScale = _config.OnOpenedStartScale;

            Tween scaleAnimation = GetScale(_config.OnOpenedDelay,
                _config.DefaultScale,
                _config.OnOpenedDuration,
                _config.OnOpenedEase);

            _currentScaleTween = scaleAnimation;
            
            await _currentScaleTween.Play();
        }
        
        public async UniTask ScaleOnWindowClosed()
        {
            _currentScaleTween?.Kill();

            Tween scaleAnimation = GetScale(_config.OnClosedDelay,
                _config.OnClosedEndScale,
                _config.OnClosedDuration,
                _config.OnClosedEase);

            _currentScaleTween = scaleAnimation;

            await _currentScaleTween.Play();
        }
        
        private Tween GetScale(float delay, Vector3 endScale, float duration, Ease ease)
        {
            Tweener scale = _transform
                .DOScale(endScale, duration)
                .SetDelay(delay)
                .SetEase(ease)
                .SetUpdate(true);

            return scale;
        }
    }
}