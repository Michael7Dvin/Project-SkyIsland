using DG.Tweening;
using UI.Elements.EventsNotifier;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Animators.OnSelectScaler
{
    public class OnSelectScaler : IUIAnimator
    {
        private Tweener _currentScaleTweener;
        
        private readonly Transform _transform;
        private readonly IUIElementEventsNotifier _events;
        
        private readonly OnSelectScalerConfig _config;
        
        public OnSelectScaler(Transform transform, IUIElementEventsNotifier events, OnSelectScalerConfig config)
        {
            _transform = transform;
            _events = events;
            _config = config;
        }
        
        public void Enable()
        {
            _events.PointerEntered += OnPointerEnter;
            _events.PointerExited += OnPointerExit;
        }

        public void Disable()
        {
            _events.PointerEntered -= OnPointerEnter;
            _events.PointerExited -= OnPointerExit;
            
            _currentScaleTweener?.Kill();
            _transform.localScale = _config.DefaultScale;
        }
        
        private void OnPointerEnter(PointerEventData eventData) => 
            ScaleOnPointerEnter();

        private void OnPointerExit(PointerEventData eventData) => 
            ScaleOnPointerExit();

        private void ScaleOnPointerEnter()
        {
            _currentScaleTweener?.Kill();

            Tweener scaleAnimation = GetScale(_config.OnEnterEndScale,
                _config.OnEnterDuration,
                _config.OnEnterEase);

            _currentScaleTweener = scaleAnimation;
            _currentScaleTweener.Play();
        }
        
        private void ScaleOnPointerExit()
        {
            _currentScaleTweener?.Kill();

            Tweener scaleAnimation = GetScale(_config.OnExitEndScale,
                _config.OnExitDuration,
                _config.OnExitEase);

            _currentScaleTweener = scaleAnimation;
            _currentScaleTweener.Play();
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