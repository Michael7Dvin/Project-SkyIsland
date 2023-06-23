using UI.Animators.OnClickScaler;
using UI.Animators.OnSelectScaler;
using UnityEngine;

namespace UI.Controls.Buttons.Close
{
    public class CloseButton : BaseButton
    {
        private OnSelectScaler _onSelectScaler;
        private OnClickScaler _onClickScaler;

        public void Construct(CloseButtonConfig config)
        {
            Transform selfTransform = transform;
            
            _onSelectScaler = new OnSelectScaler(selfTransform, Events, config.OnSelectScalerConfig);
            _onClickScaler = new OnClickScaler(selfTransform, Events, config.OnClickScalerConfig);
            
            _onSelectScaler.Enable();
            _onClickScaler.Enable();
        }

        protected override void OnEnable()
        {
            _onSelectScaler?.Enable();
            _onClickScaler?.Enable();
            
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            _onSelectScaler.Disable();
            _onClickScaler.Disable();
            base.OnDisable();
        }
    }
}