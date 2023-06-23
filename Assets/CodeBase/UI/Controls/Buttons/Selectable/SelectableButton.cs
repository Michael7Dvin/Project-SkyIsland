using TMPro;
using UI.Animators;
using UI.Animators.OnClickScaler;
using UI.Animators.OnSelectScaler;
using UnityEngine;

namespace UI.Controls.Buttons.Selectable
{
    public class SelectableButton : BaseButton
    {
        [SerializeField] private TextMeshProUGUI _text;

        private OnSelectScaler _onSelectScaler;
        private OnClickScaler _onClickScaler;
        private OnSelectColorChanger _onSelectColorChangerAnimation;
        
        public void Construct(SelectableButtonConfig config)
        {
            Transform selfTransform = transform;
            
            _onSelectScaler = new OnSelectScaler(selfTransform, Events, config.OnSelectScalerConfig);
            _onClickScaler = new OnClickScaler(selfTransform, Events, config.OnClickScalerConfig);

            _onSelectColorChangerAnimation = new OnSelectColorChanger(_text,
                Events,
                config.OnSelectColorGradient,
                config.OnUnselectColorGradient);
            
            _onSelectScaler.Enable();
            _onClickScaler.Enable();
            _onSelectColorChangerAnimation.Enable();
        }

        protected override void OnEnable()
        {
            _onSelectScaler?.Enable();
            _onClickScaler?.Enable();
            _onSelectColorChangerAnimation?.Enable();   
            
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            _onSelectScaler.Disable();
            _onClickScaler.Disable();
            _onSelectColorChangerAnimation.Disable();
            base.OnDisable();
        }
    }
}