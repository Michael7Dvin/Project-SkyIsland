using TMPro;
using UI.Animators;
using UI.Animators.OnClickScaler;
using UI.Animators.OnSelectScaler;
using UnityEngine;

namespace UI.Controls.Buttons.SaveSlot
{
    public class SaveSlotButton : BaseButton
    {
        [SerializeField] private TextMeshProUGUI _text;

        private OnSelectScaler _onSelectScaler;
        private OnClickScaler _onClickScaler;
        private OnSelectTextChanger _onSelectTextChanger;
        private OnSelectColorChanger _onSelectColorChanger;
        
        public void Construct(SaveSlotButtonConfig config)
        {            
            Transform selfTransform = transform;

            _onSelectScaler = new OnSelectScaler(selfTransform, Events, config.OnSelectScalerConfig);
            _onClickScaler = new OnClickScaler(selfTransform, Events, config.OnClickScalerConfig);

            _onSelectTextChanger = new OnSelectTextChanger(_text,
                Events,
                config.OnSelectText, 
                config.OnUnelectText);
            
            _onSelectColorChanger = new OnSelectColorChanger(_text,
                Events,
                config.OnSelectColorGradient,
                config.OnUnselectColorGradient);
            
            _onSelectScaler.Enable();
            _onClickScaler.Enable();
            _onSelectTextChanger.Enable();
            _onSelectColorChanger.Enable();
        }

        protected override void OnEnable()
        {
            _onSelectScaler?.Enable();
            _onClickScaler?.Enable();
            _onSelectTextChanger?.Enable();
            _onSelectColorChanger?.Enable();      
            
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            _onSelectScaler.Disable();
            _onClickScaler.Disable();
            _onSelectTextChanger.Enable();
            _onSelectColorChanger.Disable();
            base.OnDisable();
        }
    }
}