using System;
using UI.Elements.Animated;
using UI.Windows.Base.WindowView;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Windows.Implementations.SaveSelection
{
    public class SaveSelectionWindowView : BaseWindowView
    {
        [SerializeField] private AnimatedCanvasGroup _canvasGroup;
        [SerializeField] private AnimatedButton _closeButton;
        [SerializeField] private AnimatedButton _saveSlot1;
        [SerializeField] private AnimatedButton _saveSlot2;
        [SerializeField] private AnimatedButton _saveSlot3;

        public void Construct(SaveSelectionWindowConfig config)
        {
            _canvasGroup.Construct(config.AnimatedCanvasGroupConfig);
            
            _closeButton.Construct(config.CloseButtonConfig);
            
            _saveSlot1.Construct(config.SaveSlotButtonConfig);
            _saveSlot2.Construct(config.SaveSlotButtonConfig);
            _saveSlot3.Construct(config.SaveSlotButtonConfig);
        }

        public event Action CloseButtonClicked;
        public event Action SaveSlotButtonClicked;

        private void Awake() =>
            _canvasGroup.SetAlphaToMin();
        
        public override async void Open()
        {
            base.Open();
            await _canvasGroup.FadeIn();
        }

        public override async void Close()
        {
            await _canvasGroup.FadeOut();
            base.Close();
        }

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