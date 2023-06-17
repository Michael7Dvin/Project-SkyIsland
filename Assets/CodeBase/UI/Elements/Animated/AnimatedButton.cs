using System;
using DG.Tweening;
using UI.Elements.EventsNotifier;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Elements.Animated
{
    [RequireComponent(typeof(IUIElementEventsNotifier))]
    public class AnimatedButton : MonoBehaviour
    {
        private AnimatedButtonConfig _config;
        private IUIElementEventsNotifier _events;

        public void Construct(AnimatedButtonConfig config)
        {
            _config = config;
        }

        public event Action Cliked;

        private void Awake()
        {
            _events = GetComponent<IUIElementEventsNotifier>();
        }

        private void OnEnable()
        {
            _events.PointerClicked += OnPointerClick;
            _events.PointerDowned += OnPointerDown;
            _events.PointerUpped += OnPointerUp;
        }

        private void OnDisable()
        {
            _events.PointerClicked -= OnPointerClick;
            _events.PointerDowned -= OnPointerDown;
            _events.PointerUpped -= OnPointerUp;
        }

        private  void OnPointerClick(PointerEventData eventData) => 
            Cliked?.Invoke();

        private void OnPointerDown(PointerEventData eventData)
        {
            Tween scaleDown = GetScaleDown();
            scaleDown.Play();
        }

        private  void OnPointerUp(PointerEventData eventData)
        {
            Tween scaleUp = GetScaleUp();
            scaleUp.Play();
        }

        private Tween GetScaleDown()
        {
            Tween scaleDown = GetScale(_config.OnDownScale,
                _config.OnDownDuration,
                _config.OnDownEase,
                _config.OnDownLoopsCount,
                _config.OnDownLoopType);

            return scaleDown;
        }
        
        private Tween GetScaleUp()
        {
            Tween scaleUp = GetScale(_config.OnUpScale,
                _config.OnUpDuration,
                _config.OnUpEase,
                _config.OnUpLoopsCount,
                _config.OnUpLoopType);

            return scaleUp;
        }
        
        private Tween GetScale(float endScale, float duration, Ease ease, int loopsCount, LoopType loopType)
        {
            Tween scale = transform
                .DOScale(endScale, duration)
                .SetEase(ease)
                .SetLoops(loopsCount, loopType);

            return scale;
        }
    }
}