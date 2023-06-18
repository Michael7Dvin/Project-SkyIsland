using System;
using Cysharp.Threading.Tasks;
using UI.Animators.WindowMover;
using UI.Animators.WindowScaler;
using UI.Elements.Buttons.Close;
using UI.Elements.Buttons.Selectable;
using UI.Windows.Base.WindowView;
using UnityEngine;

namespace UI.Windows.Implementations.PauseWindow
{
    [RequireComponent(typeof(RectTransform))]
    public class PauseWindowView : BaseWindowView
    {
        [SerializeField] private CloseButton _closeButton;
        [SerializeField] private SelectableButton _optionsButton;
        [SerializeField] private SelectableButton _saveButton;
        [SerializeField] private SelectableButton _mainMenuButton;

        private WindowScaler _windowScaler;
        private WindowMover _windowMover;
        
        public void Construct(PauseWindowConfig config)
        {
            _closeButton.Construct(config.CloseButtonConfig);
            
            _optionsButton.Construct(config.OptionsButtonConfig);
            _saveButton.Construct(config.SaveButtonConfig);
            _mainMenuButton.Construct(config.MainMenuButtonConfig);
            
            RectTransform rectTransform = GetComponent<RectTransform>();

            _windowScaler = new WindowScaler(transform, config.WindowScalerConfig);
            _windowMover = new WindowMover(rectTransform, config.WindowMoverConfig);
        }
        
        public event Action CloseButtonClicked;
        public event Action OptionsButtonClicked;
        public event Action SaveButtonClicked;
        public event Action MainMenuButtonClicked;

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
            _closeButton.Cliked += OnCloseButtonClick;
            _optionsButton.Cliked += OnOptionsButtonClick;
            _saveButton.Cliked += OnSaveButtonClick;
            _mainMenuButton.Cliked += OnMainMenuButtonClick;
        }

        protected override void UnsubscribeControls()
        {
            _closeButton.Cliked -= OnCloseButtonClick;
            _optionsButton.Cliked -= OnOptionsButtonClick;
            _saveButton.Cliked -= OnSaveButtonClick;
            _mainMenuButton.Cliked -= OnMainMenuButtonClick;
        }
        
        private void OnCloseButtonClick() => 
            CloseButtonClicked?.Invoke();
        
        private void OnOptionsButtonClick() => 
            OptionsButtonClicked?.Invoke();

        private  void OnSaveButtonClick() => 
            SaveButtonClicked?.Invoke();

        private void OnMainMenuButtonClick() => 
            MainMenuButtonClicked?.Invoke();
    }
}