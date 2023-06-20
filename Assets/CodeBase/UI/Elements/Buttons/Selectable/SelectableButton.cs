using TMPro;
using UI.Animators;
using UI.Animators.OnClickScaler;
using UnityEngine;

namespace UI.Elements.Buttons.Selectable
{
    public class SelectableButton : BaseButton
    {
        [SerializeField] private TextMeshProUGUI _text;
        
        private OnClickScaler _onClickScaler;
        private OnSelectColorChanger _onSelectColorChangerAnimation;
        
        public void Construct(SelectableButtonConfig config)
        {
            _onClickScaler = new OnClickScaler(transform, Events, config.OnClickScalerConfig);

            _onSelectColorChangerAnimation = new OnSelectColorChanger(_text,
                Events,
                config.SelectedColorGradient,
                config.UnselectedColorGradient);
            
            _onClickScaler.Enable();
            _onSelectColorChangerAnimation.Enable();
        }

        private bool IsTweenAnimatorsCreated => _onClickScaler != null && _onSelectColorChangerAnimation != null;
        
        protected override void OnEnable()
        {
            if (IsTweenAnimatorsCreated == true)
            {
                _onClickScaler.Enable();
                _onSelectColorChangerAnimation.Enable();   
            }
            
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            _onClickScaler.Disable();
            _onSelectColorChangerAnimation.Disable();
            base.OnDisable();
        }
    }
}