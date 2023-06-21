using TMPro;
using UI.Elements.EventsNotifier;
using UnityEngine.EventSystems;

namespace UI.Animators
{
    public class OnSelectTextChanger : IUIAnimator
    {
        private readonly TextMeshProUGUI _text;
        private readonly IUIElementEventsNotifier _events;
        private readonly string _selectedText;
        private readonly string _unselectedText;

        public OnSelectTextChanger(TextMeshProUGUI text,
            IUIElementEventsNotifier events,
            string selectedText,
            string unselectedText)
        {
            _text = text;
            _events = events;
            _selectedText = selectedText;
            _unselectedText = unselectedText;
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

            ChangeText(_unselectedText);
        }
        
        private void OnPointerEnter(PointerEventData eventData) => 
            ChangeText(_selectedText);

        private void OnOnPointerExit(PointerEventData eventData) => 
            ChangeText(_unselectedText);

        private void ChangeText(string text) =>
            _text.text = text;
    }
}