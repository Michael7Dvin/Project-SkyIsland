using System;
using UI.Elements.Buttons.Close;
using UI.Elements.Buttons.Selectable;
using UI.Windows.Base.WindowView;
using UnityEngine;

namespace UI.Windows.Implementations.PauseWindow
{
    public class PauseWindowView : BaseWindowView
    {
        [SerializeField] private CloseButton _closeButton;
        [SerializeField] private SelectableButton _optionsButton;
        [SerializeField] private SelectableButton _saveButton;
        [SerializeField] private SelectableButton _mainMenuButton;

        public void Construct(PauseWindowConfig config)
        {
            _closeButton.Construct(config.CloseButtonConfig);
            _optionsButton.Construct(config.OptionsButtonConfig);
            _saveButton.Construct(config.SaveButtonConfig);
            _mainMenuButton.Construct(config.MainMenuButtonConfig);
        }
        
        public event Action CloseButtonClicked;
        public event Action OptionsButtonClicked;
        public event Action SaveButtonClicked;
        public event Action MainMenuButtonClicked;

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