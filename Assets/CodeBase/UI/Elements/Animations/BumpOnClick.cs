using DG.Tweening;
using TweenAnimations;
using UI.Elements.EventsNotifier;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Elements.Animations
{
    public class BumpOnClick : IUIAnimation
    {
        private Tween _currentScaleAnimation;
        
        private readonly Transform _transform;
        private readonly IUIElementEventsNotifier _events;

        private readonly ScalingConfig _onButtonDownScalingConfig;
        private readonly ScalingConfig _onButtonUpScalingConfig;
        
        public BumpOnClick(Transform transform,
            IUIElementEventsNotifier events,
            ScalingConfig onButtonDownScalingConfig,
            ScalingConfig onButtonUpScalingConfig)
        {
            _transform = transform;
            _events = events;
            _onButtonDownScalingConfig = onButtonDownScalingConfig;
            _onButtonUpScalingConfig = onButtonUpScalingConfig;
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
            _currentScaleAnimation?.Kill();
        }
        
        private void OnPointerDown(PointerEventData eventData) => 
            AnimateScaling(_onButtonDownScalingConfig);

        private  void OnPointerUp(PointerEventData eventData) => 
            AnimateScaling(_onButtonUpScalingConfig);

        private void AnimateScaling(ScalingConfig config)
        {
            _currentScaleAnimation?.Kill();
            
            Tween scaleAnimation = GetScale(config.EndScale,
                config.Duration,
                config.Ease,
                config.LoopsCount,
                config.LoopType);

            _currentScaleAnimation = scaleAnimation;
            _currentScaleAnimation.Play();
        }

        private Tween GetScale(float endScale, float duration, Ease ease, int loopsCount, LoopType loopType)
        {
            Tween scale = _transform
                .DOScale(endScale, duration)
                .SetEase(ease)
                .SetLoops(loopsCount, loopType)
                .SetUpdate(true);

            return scale;
        }
    }
}