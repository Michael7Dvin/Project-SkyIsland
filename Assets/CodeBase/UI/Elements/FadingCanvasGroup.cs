﻿using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace UI.Elements
{
    public class FadingCanvasGroup : MonoBehaviour
    {
        private float _fadeDuration;
        private float _minAlpha;
        private float _maxAlpha;
        
        [SerializeField] private CanvasGroup _canvasGroup;

        public void Construct(FadingCanvasGroupConfig config)
        {
            _fadeDuration = config.FadeDuration;
            _minAlpha = config.MinAlpha;
            _maxAlpha = config.MaxAlpha;
        }

        public void SetAlphaToMin() => 
            _canvasGroup.alpha = _minAlpha;

        public async UniTask FadeIn()
        {
            Tween fadeIn = GetFade(_canvasGroup, _maxAlpha, _fadeDuration);
            await fadeIn.Play();
        }

        public async UniTask FadeOut()
        {
            Tween fadeOut = GetFade(_canvasGroup, _minAlpha, _fadeDuration);
            await fadeOut.Play();
        }
        
        private Tween GetFade(CanvasGroup canvasGroup, float targetAlpha, float duration)
        {
            return canvasGroup
                .DOFade(targetAlpha, duration)
                .SetUpdate(true);
        }
    }
}