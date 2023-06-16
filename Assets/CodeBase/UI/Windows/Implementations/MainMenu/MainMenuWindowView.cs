using System;
using UI.Windows.Base.WindowView;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows.Implementations.MainMenu
{
    public class MainMenuWindowView : BaseWindowView
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _optionsButton;
        [SerializeField] private Button _quitButton;
        
        public event Action PlayButtonClicked;
        public event Action OptionsButtonClicked;
        public event Action QuitButtonClicked;
        
        protected override void SubscribeControls()
        {
            _playButton.onClick.AddListener(OnPlayButtonClick);
            _optionsButton.onClick.AddListener(OnOptionsButtonClick);
            _quitButton.onClick.AddListener(OnQuitButtonClick);
        }

        protected override void UnsubscribeControls()
        {
            _playButton.onClick.RemoveListener(OnPlayButtonClick);
            _optionsButton.onClick.RemoveListener(OnOptionsButtonClick);
            _quitButton.onClick.RemoveListener(OnQuitButtonClick);
        }

        private void OnPlayButtonClick() => 
            PlayButtonClicked?.Invoke();

        private void OnOptionsButtonClick() => 
            OptionsButtonClicked?.Invoke();

        private void OnQuitButtonClick() => 
            QuitButtonClicked?.Invoke();
    }
}