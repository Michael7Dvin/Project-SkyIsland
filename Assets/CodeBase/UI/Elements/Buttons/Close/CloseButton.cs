using UI.Animators.OnClickScaler;

namespace UI.Elements.Buttons.Close
{
    public class CloseButton : BaseButton
    {
        private OnClickScaler _onClickScalerAnimation;

        public void Construct(CloseButtonConfig config)
        {
            _onClickScalerAnimation = new OnClickScaler(transform, Events, config.OnClickScalerConfig);
            
            _onClickScalerAnimation.Enable();
        }

        private bool IsAnimationsCreated => _onClickScalerAnimation != null;
        
        protected override void OnEnable()
        {
            if (IsAnimationsCreated == true) 
                _onClickScalerAnimation.Enable();
            
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            _onClickScalerAnimation.Disable();
            base.OnDisable();
        }
    }
}