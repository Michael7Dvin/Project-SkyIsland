using System;
using UI.Windows.Base.WindowView;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows.Implementations.PauseWindow
{
    public class PauseWindowView : BaseWindowView
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _optionsButton;
        [SerializeField] private Button _saveButton;
        [SerializeField] private Button _mainMenuButton;

        public event Action CloseButtonClicked;
        public event Action OptionsButtonClicked;
        public event Action SaveButtonClicked;
        public event Action MainMenuButtonClicked;

        protected override void SubscribeControls()
        {
            _closeButton.onClick.AddListener(OnCloseButtonClick);
            _optionsButton.onClick.AddListener(OnOptionsButtonClick);
            _saveButton.onClick.AddListener(OnSaveButtonClick);
            _mainMenuButton.onClick.AddListener(OnMainMenuButtonClick);
        }

        protected override void UnsubscribeControls()
        {
            _closeButton.onClick.RemoveListener(OnCloseButtonClick);
            _optionsButton.onClick.RemoveListener(OnOptionsButtonClick);
            _saveButton.onClick.RemoveListener(OnSaveButtonClick);
            _mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClick);
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