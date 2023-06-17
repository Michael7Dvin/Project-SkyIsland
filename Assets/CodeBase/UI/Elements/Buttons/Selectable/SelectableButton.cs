using TMPro;
using UI.Elements.Animations;
using UnityEngine;

namespace UI.Elements.Buttons.Selectable
{
    public class SelectableButton : BaseButton
    {
        [SerializeField] private TextMeshProUGUI _text;
        
        private BumpOnClick _bumpOnClickAnimation;
        private ChangeColorOnSelect _changeColorOnSelectAnimation;
        
        public void Construct(SelectableButtonConfig config)
        {
            _bumpOnClickAnimation = new BumpOnClick(transform,
                Events,
                config.OnButtonDownScalingConfig,
                config.OnButtonUpScalingConfig);

            _changeColorOnSelectAnimation = new ChangeColorOnSelect(_text,
                Events,
                config.SelectedColorGradient,
                config.UnselectedColorGradient);
            
            _bumpOnClickAnimation.Enable();
            _changeColorOnSelectAnimation.Enable();
        }

        private bool IsAnimationsCreated => _bumpOnClickAnimation != null && _changeColorOnSelectAnimation != null;
        
        protected override void OnEnable()
        {
            if (IsAnimationsCreated == true)
            {
                _bumpOnClickAnimation.Enable();
                _changeColorOnSelectAnimation.Enable();   
            }
            
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            _bumpOnClickAnimation.Disable();
            _changeColorOnSelectAnimation.Disable();
            base.OnDisable();
        }
    }
}