using TMPro;
using UI.Animators;
using UI.Animators.OnClickScaler;
using UnityEngine;

namespace UI.Elements.Buttons.Selectable
{
    public class SelectableButton : BaseButton
    {
        [SerializeField] private TextMeshProUGUI _text;
        
        private OnClickScaler _onClickScalerAnimation;
        private ChangeColorOnSelect _changeColorOnSelectAnimation;
        
        public void Construct(SelectableButtonConfig config)
        {
            _onClickScalerAnimation = new OnClickScaler(transform, Events, config.OnClickScalerConfig);

            _changeColorOnSelectAnimation = new ChangeColorOnSelect(_text,
                Events,
                config.SelectedColorGradient,
                config.UnselectedColorGradient);
            
            _onClickScalerAnimation.Enable();
            _changeColorOnSelectAnimation.Enable();
        }

        private bool IsTweenAnimatorsCreated => _onClickScalerAnimation != null && _changeColorOnSelectAnimation != null;
        
        protected override void OnEnable()
        {
            if (IsTweenAnimatorsCreated == true)
            {
                _onClickScalerAnimation.Enable();
                _changeColorOnSelectAnimation.Enable();   
            }
            
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            _onClickScalerAnimation.Disable();
            _changeColorOnSelectAnimation.Disable();
            base.OnDisable();
        }
    }
}