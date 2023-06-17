using UI.Elements.Animations;

namespace UI.Elements.Buttons.Close
{
    public class CloseButton : BaseButton
    {
        private BumpOnClick _bumpOnClickAnimation;

        public void Construct(CloseButtonConfig config)
        {
            _bumpOnClickAnimation = new BumpOnClick(transform,
                Events,
                config.OnButtonDownScalingConfig,
                config.OnButtonUpScalingConfig);
            
            _bumpOnClickAnimation.Enable();
        }

        private bool IsAnimationsCreated => _bumpOnClickAnimation != null;
        
        protected override void OnEnable()
        {
            if (IsAnimationsCreated == true) 
                _bumpOnClickAnimation.Enable();
            
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            _bumpOnClickAnimation.Disable();
            base.OnDisable();
        }
    }
}