using System;
using Cysharp.Threading.Tasks;
using UI.Animators.WindowMover;
using UI.Animators.WindowScaler;
using UI.Controls.Buttons.Close;
using UI.Controls.Buttons.Selectable;
using UI.Windows.Base.WindowView;
using UnityEngine;

namespace UI.Windows.Implementations.PauseWindow
{
    [RequireComponent(typeof(RectTransform))]
    public class PauseWindowView : BaseWindowView
    {
        [SerializeField] private CloseButton _closeButton;
        [SerializeField] private SelectableButton _optionsButton;
        [SerializeField] private SelectableButton _loadButton;
        [SerializeField] private SelectableButton _saveButton;
        [SerializeField] private SelectableButton _mainMenuButton;

        private bool _isWindowClosing;

        private GameObject _background;
        private WindowScaler _windowScaler;
        private WindowMover _windowMover;
        
        public void Construct(PauseWindowConfig config, GameObject background)
        {
            _closeButton.Construct(config.CloseButtonConfig);
            
            _optionsButton.Construct(config.OptionsButtonConfig);
            _loadButton.Construct(config.LoadButtonConfig);
            _saveButton.Construct(config.SaveButtonConfig);
            _mainMenuButton.Construct(config.MainMenuButtonConfig);
            
            RectTransform rectTransform = GetComponent<RectTransform>();

            _windowScaler = new WindowScaler(transform, config.WindowScalerConfig);
            _windowMover = new WindowMover(rectTransform, config.WindowMoverConfig);
            
            _background = background;
            EnableAnimators();
        }
        
        public event Action CloseButtonClicked;
        public event Action OptionsButtonClicked;
        public event Action LoadButtonClicked;
        public event Action SaveButtonClicked;
        public event Action MainMenuButtonClicked;

        public override async void Open()
        {
            _background.SetActive(true);
            _isWindowClosing = false;
            base.Open();

            UniTask windowScaleAnimation = _windowScaler.ScaleOnWindowOpen();
            UniTask windowMoveAnimation = _windowMover.MoveOnWindowOpen();
            await UniTask.WhenAll(windowScaleAnimation, windowMoveAnimation);
        }

        public override async void Close()
        {
            _background.SetActive(false);
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
            _closeButton.Cliked += OnCloseButtonClick;
            _optionsButton.Cliked += OnOptionsButtonClick;
            _loadButton.Cliked += OnLoadButtonClick;
            _saveButton.Cliked += OnSaveButtonClick;
            _mainMenuButton.Cliked += OnMainMenuButtonClick;
        }

        protected override void UnsubscribeControls()
        {
            _closeButton.Cliked -= OnCloseButtonClick;
            _optionsButton.Cliked -= OnOptionsButtonClick;
            _loadButton.Cliked -= OnLoadButtonClick;
            _saveButton.Cliked -= OnSaveButtonClick;
            _mainMenuButton.Cliked -= OnMainMenuButtonClick;
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

        private void OnCloseButtonClick() => 
            CloseButtonClicked?.Invoke();
        
        private void OnOptionsButtonClick() => 
            OptionsButtonClicked?.Invoke();

        private void OnLoadButtonClick() => 
            LoadButtonClicked?.Invoke();
        
        private void OnSaveButtonClick() => 
            SaveButtonClicked?.Invoke();

        private void OnMainMenuButtonClick() => 
            MainMenuButtonClicked?.Invoke();
    }
}