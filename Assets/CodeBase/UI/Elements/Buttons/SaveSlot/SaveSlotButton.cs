using TMPro;
using UI.Elements.Animations;
using UnityEngine;

namespace UI.Elements.Buttons.SaveSlot
{
    public class SaveSlotButton : BaseButton
    {
        [SerializeField] private TextMeshProUGUI _text;
        
        private BumpOnClick _bumpOnClickAnimation;
        private ChangeTextOnSelect _changeTextOnSelectAnimation;
        private ChangeColorOnSelect _changeColorOnSelectAnimation;
        
        public void Construct(SaveSlotButtonConfig config)
        {
            _bumpOnClickAnimation = new BumpOnClick(transform,
                Events,
                config.OnButtonDownScalingConfig,
                config.OnButtonUpScalingConfig);

            _changeTextOnSelectAnimation = new ChangeTextOnSelect(_text,
                Events,
                config.OnSelectedText, 
                config.OnUnelectedText);
            
            _changeColorOnSelectAnimation = new ChangeColorOnSelect(_text,
                Events,
                config.SelectedColorGradient,
                config.UnselectedColorGradient);
            
            _bumpOnClickAnimation.Enable();
            _changeTextOnSelectAnimation.Enable();
            _changeColorOnSelectAnimation.Enable();
        }

        private bool IsAnimationsCreated => _bumpOnClickAnimation != null 
            && _changeTextOnSelectAnimation != null 
            && _changeColorOnSelectAnimation != null;

        protected override void OnEnable()
        {
            if (IsAnimationsCreated == true)
            {
                _bumpOnClickAnimation.Enable();
                _changeTextOnSelectAnimation.Enable();
                _changeColorOnSelectAnimation.Enable();                
            }

            base.OnEnable();
        }

        protected override void OnDisable()
        {
            _bumpOnClickAnimation.Disable();
            _changeTextOnSelectAnimation.Enable();
            _changeColorOnSelectAnimation.Disable();
            base.OnDisable();
        }
    }
}