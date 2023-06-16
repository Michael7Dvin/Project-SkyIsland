using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI.AnimatedElements
{
    public class AnimatedButton : MonoBehaviour
    {
        private float _bumpScale;
        private float _bumpDuration;
        private Ease _bumpEase;
        private int _bumpLoopsCount;
        private LoopType _bumpLoopType;
        
        [SerializeField] private Button _button;

        public void Construct(AnimatedButtonConfig config)
        {
            _bumpScale = config.BumpScale;
            _bumpDuration = config.BumpDuration;
            _bumpEase = config.BumpEase;
            _bumpLoopsCount = config.BumpLoopsCount;
            _bumpLoopType = config.BumpLoopType;
        }

        public event Action Cliked;
        
        private void OnEnable() => 
            _button.onClick.AddListener(OnButtonClick);

        private void OnDisable() => 
            _button.onClick.RemoveListener(OnButtonClick);

        private async void OnButtonClick()
        {
            await Bump();
            Cliked?.Invoke();
        }

        private async UniTask Bump()
        {
            Tween bump = GetBump();
            await bump.Play();
        }

        private Tween GetBump()
        {
            Tween bump = transform
                .DOScale(_bumpScale, _bumpDuration)
                .SetEase(_bumpEase)
                .SetLoops(_bumpLoopsCount, _bumpLoopType);

            return bump;
        }
    }
}