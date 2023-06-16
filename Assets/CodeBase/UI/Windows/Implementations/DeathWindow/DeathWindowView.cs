using System;
using UI.Windows.Base.WindowView;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows.Implementations.DeathWindow
{
    public class DeathWindowView : BaseWindowView
    {
        [SerializeField] private Button _respawnButton;
        [SerializeField] private Button _mainMenuButton;

        public event Action RespawnButtonClicked;
        public event Action MainMenuButtonClicked;

        protected override void SubscribeControls()
        {
            _respawnButton.onClick.AddListener(OnRespawnButtonClick);
            _mainMenuButton.onClick.AddListener(OnMainMenuButtonClick);
        }

        protected override void UnsubscribeControls()
        {
            _respawnButton.onClick.RemoveListener(OnRespawnButtonClick);
            _mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClick);
        }
        
        private void OnRespawnButtonClick() => 
            RespawnButtonClicked?.Invoke();

        public void OnMainMenuButtonClick() =>
            MainMenuButtonClicked?.Invoke();
    }
}