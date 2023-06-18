using System;
using Cysharp.Threading.Tasks;
using UI.Animators.WindowMover;
using UI.Animators.WindowScaler;
using UI.Elements.Buttons.Close;
using UI.Elements.Buttons.SaveSlot;
using UI.Windows.Base.WindowView;
using UnityEngine;

namespace UI.Windows.Implementations.SaveSelection
{
    [RequireComponent(typeof(RectTransform))]
    public class SaveSelectionWindowView : BaseWindowView
    {
        [SerializeField] private CloseButton _closeButton;
        [SerializeField] private SaveSlotButton _saveSlot1;
        [SerializeField] private SaveSlotButton _saveSlot2;
        [SerializeField] private SaveSlotButton _saveSlot3;
        
        private WindowScaler _windowScaler;
        private WindowMover _windowMover;
        
        public void Construct(SaveSelectionWindowConfig config)
        {
            _closeButton.Construct(config.CloseButtonConfig);
            
            _saveSlot1.Construct(config.SaveSlotButtonConfig);
            _saveSlot2.Construct(config.SaveSlotButtonConfig);
            _saveSlot3.Construct(config.SaveSlotButtonConfig);

            RectTransform rectTransform = GetComponent<RectTransform>();

            _windowScaler = new WindowScaler(transform, config.WindowScalerConfig);
            _windowMover = new WindowMover(rectTransform, config.WindowMoverConfig);
        }

        public event Action CloseButtonClicked;
        public event Action SaveSlotButtonClicked;

        public override async void Open()
        {
            base.Open();
     
            UniTask windowScaleAnimation = _windowScaler.ScaleOnWinodwOpen();
            UniTask windowMoveAnimation = _windowMover.MoveOnWindowOpen();
            await UniTask.WhenAll(windowScaleAnimation, windowMoveAnimation);
        }

        public override async void Close()
        {
            UniTask windowScaleAnimation = _windowScaler.ScaleOnWindowClosed();
            UniTask windowMoveAnimation = _windowMover.MoveOnWindowClosed();
            await UniTask.WhenAll(windowScaleAnimation, windowMoveAnimation);
            
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