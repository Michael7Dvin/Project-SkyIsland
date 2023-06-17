using TMPro;
using UI.Elements.EventsNotifier;
using UnityEngine.EventSystems;

namespace UI.Elements.Animations
{
    public class ChangeColorOnSelect : IUIAnimation
    {
        private readonly TextMeshProUGUI _text;
        private readonly IUIElementEventsNotifier _events;
        private readonly TMP_ColorGradient _selectedColorGradient;
        private readonly TMP_ColorGradient _unselectedColorGradient;

        public ChangeColorOnSelect(TextMeshProUGUI text,
            IUIElementEventsNotifier events,
            TMP_ColorGradient selectedColorGradient,
            TMP_ColorGradient unselectedColorGradient)
        {
            _text = text;
            _events = events;
            _selectedColorGradient = selectedColorGradient;
            _unselectedColorGradient = unselectedColorGradient;
        }

        public void Enable()
        {
            _events.PointerEntered += OnPointerEnter;
            _events.PointerExited += OnOnPointerExit;
        }

        public void Disable()
        {
            _events.PointerEntered -= OnPointerEnter;
            _events.PointerExited -= OnOnPointerExit;

            ChangeTextColor(_unselectedColorGradient);
        }
        
        private void OnPointerEnter(PointerEventData eventData) => 
            ChangeTextColor(_selectedColorGradient);

        private void OnOnPointerExit(PointerEventData eventData) => 
            ChangeTextColor(_unselectedColorGradient);
        
        private void ChangeTextColor(TMP_ColorGradient colorGradient) => 
            _text.colorGradientPreset = colorGradient;
    }
}