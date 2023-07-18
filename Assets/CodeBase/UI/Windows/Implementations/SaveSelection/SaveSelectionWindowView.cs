using System;
using Cysharp.Threading.Tasks;
using Infrastructure.Services.SaveLoadService;
using UI.Animators.WindowMover;
using UI.Animators.WindowScaler;
using UI.Controls.Buttons.Close;
using UI.Windows.Base.WindowView;
using UnityEngine;

namespace UI.Windows.Implementations.SaveSelection
{
    [RequireComponent(typeof(RectTransform))]
    public class SaveSelectionWindowView : BaseWindowView
    {
        [SerializeField] private CloseButton _closeButton;
        [SerializeField] private SaveSlot _saveSlot1;
        [SerializeField] private SaveSlot _saveSlot2;
        [SerializeField] private SaveSlot _saveSlot3;
        
        private bool _isWindowClosing;

        private WindowScaler _windowScaler;
        private WindowMover _windowMover;
        
        public void Construct(SaveSelectionWindowConfig config)
        {
            _closeButton.Construct(config.CloseButtonConfig);
            
            _saveSlot1.Construct(SaveSlotID.First, config.SaveSlotButtonConfig, config.DeleteSaveButtonConfig);
            _saveSlot2.Construct(SaveSlotID.Second, config.SaveSlotButtonConfig, config.DeleteSaveButtonConfig);
            _saveSlot3.Construct(SaveSlotID.Third, config.SaveSlotButtonConfig, config.DeleteSaveButtonConfig);

            RectTransform rectTransform = GetComponent<RectTransform>();

            _windowScaler = new WindowScaler(transform, config.WindowScalerConfig);
            _windowMover = new WindowMover(rectTransform, config.WindowMoverConfig);
            
            EnableAnimators();
        }

        public SaveSlot SaveSlot1 => _saveSlot1;
        public SaveSlot SaveSlot2 => _saveSlot2;
        public SaveSlot SaveSlot3 => _saveSlot3;
        
        public event Action CloseButtonClicked;
        public event Action<SaveSlotID> SaveSlotButtonClicked;
        public event Action<SaveSlotID> DeleteSaveButtonClicked;

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
            
            _saveSlot1.SaveSlotButton.Cliked += OnFirstSaveSlotButtonClick;
            _saveSlot2.SaveSlotButton.Cliked += OnSecondSaveSlotButtonClick;
            _saveSlot3.SaveSlotButton.Cliked += OnThirdSaveSlotButtonClick;
            
            _saveSlot1.DeleteSaveButton.Cliked += OnFirstDeleteSaveButtonClick;
            _saveSlot2.DeleteSaveButton.Cliked += OnSecondDeleteSaveButtonClick;
            _saveSlot3.DeleteSaveButton.Cliked += OnThirdDeleteSaveButtonClick;
        }

        protected override void UnsubscribeControls()
        {
            _closeButton.Cliked -= OnCloseSlotButtonClick;
            
            _saveSlot1.SaveSlotButton.Cliked -= OnFirstSaveSlotButtonClick;
            _saveSlot2.SaveSlotButton.Cliked -= OnSecondSaveSlotButtonClick;
            _saveSlot3.SaveSlotButton.Cliked -= OnThirdSaveSlotButtonClick;
            
            _saveSlot1.DeleteSaveButton.Cliked -= OnFirstDeleteSaveButtonClick;
            _saveSlot2.DeleteSaveButton.Cliked -= OnSecondDeleteSaveButtonClick;
            _saveSlot3.DeleteSaveButton.Cliked -= OnThirdDeleteSaveButtonClick;
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
            SaveSlotButtonClicked?.Invoke(SaveSlotID.First);
        private void OnSecondSaveSlotButtonClick() => 
            SaveSlotButtonClicked?.Invoke(SaveSlotID.Second);
        private void OnThirdSaveSlotButtonClick() => 
            SaveSlotButtonClicked?.Invoke(SaveSlotID.Third);
        
        private void OnFirstDeleteSaveButtonClick() => 
            DeleteSaveButtonClicked?.Invoke(SaveSlotID.First);
        private void OnSecondDeleteSaveButtonClick() => 
            DeleteSaveButtonClicked?.Invoke(SaveSlotID.Second);
        private void OnThirdDeleteSaveButtonClick() => 
            DeleteSaveButtonClicked?.Invoke(SaveSlotID.Third);
    }
}