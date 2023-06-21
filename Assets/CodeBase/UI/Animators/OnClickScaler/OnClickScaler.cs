using DG.Tweening;
using UI.Elements.EventsNotifier;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Animators.OnClickScaler
{
    public class OnClickScaler : IUIAnimator
    {
        private Tweener _currentScaleTween;
        
        private readonly Transform _transform;
        private readonly IUIElementEventsNotifier _events;
        
        private readonly OnClickScalerConfig _config;
        
        public OnClickScaler(Transform transform, IUIElementEventsNotifier events, OnClickScalerConfig config)
        {
            _transform = transform;
            _events = events;
            _config = config;
        }
        
        public void Enable()
        {
            _events.PointerDowned += OnPointerDown;
            _events.PointerUpped += OnPointerUp;
        }

        public void Disable()
        {
            _events.PointerDowned -= OnPointerDown;
            _events.PointerUpped -= OnPointerUp;
            
            _currentScaleTween?.Kill();
            _transform.localScale = _config.DefaultScale;
        }
        
        private void OnPointerDown(PointerEventData eventData) => 
            ScaleOnPointerDown();

        private  void OnPointerUp(PointerEventData eventData) => 
            ScaleOnPointerUp();

        private void ScaleOnPointerDown()
        {
            _currentScaleTween?.Kill();

            Tweener scaleAnimation = GetScale(_config.OnDownEndScale,
                _config.OnDownDuration,
                _config.OnDownEase);

            _currentScaleTween = scaleAnimation;
            _currentScaleTween.Play();
        }
        
        private void ScaleOnPointerUp()
        {
            _currentScaleTween?.Kill();

            Tweener scaleAnimation = GetScale(_config.OnUpEndScale,
                _config.OnUpDuration,
                _config.OnUpEase);

            _currentScaleTween = scaleAnimation;
            _currentScaleTween.Play();
        }

        private Tweener GetScale(Vector3 endScale, float duration, Ease ease)
        {
            Tweener scale = _transform
                .DOScale(endScale, duration)
                .SetEase(ease)
                .SetUpdate(true);

            return scale;
        }
    }
}