using System;
using UI.Elements.Buttons.Selectable;
using UI.Windows.Base.WindowView;
using UnityEngine;

namespace UI.Windows.Implementations.DeathWindow
{
    public class DeathWindowView : BaseWindowView
    {
        [SerializeField] private SelectableButton _respawnButton;
        [SerializeField] private SelectableButton _mainMenuButton;

        public void Construct(DeathWindowConfig config)
        {
            _respawnButton.Construct(config.RespawnButtonConfig);
            _mainMenuButton.Construct(config.MainMenuButtonConfig);
        }
        
        public event Action RespawnButtonClicked;
        public event Action MainMenuButtonClicked;

        protected override void SubscribeControls()
        {
            _respawnButton.Cliked += OnRespawnButtonClick;
            _mainMenuButton.Cliked += OnMainMenuButtonClick;
        }

        protected override void UnsubscribeControls()
        {
            _respawnButton.Cliked -= OnRespawnButtonClick;
            _mainMenuButton.Cliked -= OnMainMenuButtonClick;
        }
        
        private void OnRespawnButtonClick() => 
            RespawnButtonClicked?.Invoke();

        public void OnMainMenuButtonClick() =>
            MainMenuButtonClicked?.Invoke();
    }
}