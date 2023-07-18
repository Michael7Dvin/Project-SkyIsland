using TMPro;
using UI.Controls.Events;
using UnityEngine.EventSystems;

namespace UI.Animators
{
    public class OnSelectTextChanger : IUIAnimator
    {
        private readonly TextMeshProUGUI _textField;
        private readonly IControlEvents _events;
        private readonly string _selectedText;
        private readonly string _unselectedText;

        public OnSelectTextChanger(TextMeshProUGUI textField,
            IControlEvents events,
            string selectedText,
            string unselectedText)
        {
            _textField = textField;
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
            _textField.text = text;
    }
}