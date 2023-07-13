using System;
using Cysharp.Threading.Tasks;
using Infrastructure.Services.SaveLoadService;
using UI.Animators.WindowMover;
using UI.Animators.WindowScaler;
using UI.Controls.Buttons.Close;
using UI.Controls.Buttons.SaveSlots;
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
        
        private bool _isWindowClosing;

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
            
            EnableAnimators();
        }

        public event Action CloseButtonClicked;
        public event Action<SaveSlot> SaveSlotButtonClicked;

        public override async void Open()
        {
            _isWindowClosing = false;
            base.Open();
     
            UniTask windowScaleAnimation = _windowScaler.ScaleOnWindowOpen();
            UniTask windowMoveAnimation = _windowMover.MoveOnWindowOpen();
            await UniTask.WhenAll(windowScaleAnimation, windowMoveAnimation);
        }

        public override async void Close()
        {
            _isWindowClosing = true;
            
            UniTask windowScaleAnimation = _windowScaler.ScaleOnWindowClosed();
            UniTask windowMoveAnimation = _windowMover.MoveOnWindowClosed();
            await UniTask.WhenAll(windowScaleAnimation, windowMoveAnimation);
            
            if (_isWindowClosing == true)
            {
                base.Close();
                _isWindowClosing = false;
            }
        }
        
        protected override void SubscribeControls()
        {
            _closeButton.Cliked += OnCloseSlotButtonClick;
            
            _saveSlot1.Cliked += OnFirstSaveSlotButtonClick;
            _saveSlot2.Cliked += OnSecondSaveSlotButtonClick;
            _saveSlot3.Cliked += OnThirdSaveSlotButtonClick;
        }

        protected override void UnsubscribeControls()
        {
            _closeButton.Cliked -= OnCloseSlotButtonClick;
            _saveSlot1.Cliked -= OnFirstSaveSlotButtonClick;
            _saveSlot2.Cliked -= OnSecondSaveSlotButtonClick;
            _saveSlot3.Cliked -= OnThirdSaveSlotButtonClick;
        }

        protected override void EnableAnimators()
        {
            _windowMover?.Enable();
            _windowScaler?.Enable();
        }

        protected override void DisableAnimators()
        {
            _windowMover?.Disable();
            _windowScaler?.Disable();
        }

        private void OnCloseSlotButtonClick() => 
            CloseButtonClicked?.Invoke();
        
        private void OnFirstSaveSlotButtonClick() => 
            SaveSlotButtonClicked?.Invoke(SaveSlot.First);
        private void OnSecondSaveSlotButtonClick() => 
            SaveSlotButtonClicked?.Invoke(SaveSlot.Second);
        private void OnThirdSaveSlotButtonClick() => 
            SaveSlotButtonClicked?.Invoke(SaveSlot.Third);
    }
}