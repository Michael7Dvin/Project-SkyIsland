using TMPro;
using UI.Animators;
using UI.Animators.OnClickScaler;
using UnityEngine;

namespace UI.Controls.Buttons.SaveSlots
{
    public class SaveSlotButton : BaseButton
    {
        [SerializeField] private TextMeshProUGUI _textField;
        
        private OnClickScaler _onClickScaler;
        private OnSelectColorChanger _onSelectColorChanger;

        public void Construct(SaveSlotButtonConfig config)
        {            
            Transform selfTransform = transform;

            _onClickScaler = new OnClickScaler(selfTransform, Events, config.OnClickScalerConfig);
            
            _onSelectColorChanger = new OnSelectColorChanger(_textField,
                Events,
                config.OnSelectColorGradient,
                config.OnUnselectColorGradient);
            
            _onClickScaler.Enable();
            _onSelectColorChanger.Enable();
        }
        
        protected override void OnEnable()
        {
            _onClickScaler?.Enable();
            _onSelectColorChanger?.Enable();      
            
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            _onClickScaler.Disable();
            _onSelectColorChanger.Disable();
            base.OnDisable();
        }
    }
}