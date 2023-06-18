using System;
using UI.Elements.Buttons.Close;
using UI.Elements.Buttons.SaveSlot;
using UI.Windows.Base.WindowView;
using UnityEngine;

namespace UI.Windows.Implementations.SaveSelection
{
    public class SaveSelectionWindowView : BaseWindowView
    {
        [SerializeField] private CloseButton _closeButton;
        [SerializeField] private SaveSlotButton _saveSlot1;
        [SerializeField] private SaveSlotButton _saveSlot2;
        [SerializeField] private SaveSlotButton _saveSlot3;

        public void Construct(SaveSelectionWindowConfig config)
        {
            _closeButton.Construct(config.CloseButtonConfig);
            
            _saveSlot1.Construct(config.SaveSlotButtonConfig);
            _saveSlot2.Construct(config.SaveSlotButtonConfig);
            _saveSlot3.Construct(config.SaveSlotButtonConfig);
        }

        public event Action CloseButtonClicked;
        public event Action SaveSlotButtonClicked;
        
        protected override void SubscribeControls()
        {
            _closeButton.Cliked += OnCloseSlotButtonClick;
            _saveSlot1.Cliked += OnSaveSlotButtonClick;
            _saveSlot2.Cliked += OnSaveSlotButtonClick;
            _saveSlot3.Cliked += OnSaveSlotButtonClick;
        }

        protected override void UnsubscribeControls()
        {
            _closeButton.Cliked -= OnCloseSlotButtonClick;
            _saveSlot1.Cliked -= OnSaveSlotButtonClick;
            _saveSlot2.Cliked -= OnSaveSlotButtonClick;
            _saveSlot3.Cliked -= OnSaveSlotButtonClick;
        }

        private void OnCloseSlotButtonClick() => 
            CloseButtonClicked?.Invoke();
        
        private void OnSaveSlotButtonClick() => 
            SaveSlotButtonClicked?.Invoke();
    }
}