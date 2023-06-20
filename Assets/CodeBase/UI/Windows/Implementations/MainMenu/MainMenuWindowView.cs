using System;
using UI.Elements.Buttons.Selectable;
using UI.Windows.Base.WindowView;
using UnityEngine;

namespace UI.Windows.Implementations.MainMenu
{
    public class MainMenuWindowView : BaseWindowView
    {
        [SerializeField] private SelectableButton _playButton;
        [SerializeField] private SelectableButton _optionsButton;
        [SerializeField] private SelectableButton _quitButton;
        
        public event Action PlayButtonClicked;
        public event Action OptionsButtonClicked;
        public event Action QuitButtonClicked;

        public void Construct(MainMenuWindowConfig config)
        {
            _playButton.Construct(config.PlayButtonConfig);
            _optionsButton.Construct(config.OptionsButtonConfig);
            _quitButton.Construct(config.QuitButtonConfig);
        }
        
        protected override void SubscribeControls()
        {
            _playButton.Cliked += OnPlayButtonClick;
            _optionsButton.Cliked += OnOptionsButtonClick;
            _quitButton.Cliked += OnQuitButtonClick;
        }

        protected override void UnsubscribeControls()
        {
            _playButton.Cliked -= OnPlayButtonClick;
            _optionsButton.Cliked -= OnOptionsButtonClick;
            _quitButton.Cliked -= OnQuitButtonClick;
        }

        protected override void EnableAnimators()
        {
        }

        protected override void DisableAnimators()
        {
        }

        private void OnPlayButtonClick() => 
            PlayButtonClicked?.Invoke();

        private void OnOptionsButtonClick() => 
            OptionsButtonClicked?.Invoke();

        private void OnQuitButtonClick() => 
            QuitButtonClicked?.Invoke();
    }
}