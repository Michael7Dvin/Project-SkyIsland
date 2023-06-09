using Infrastructure.GameFSM;
using Infrastructure.GameFSM.States;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public class DeathWindow : BaseWindow
    {
        [SerializeField] private Button _respawnButton;
        [SerializeField] private Button _mainMenuButton;
        
        private IGameStateMachine _gameStateMachine;

        public void Construct(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
        
        protected override void SubscribeOnButtons()
        {
            _respawnButton.onClick.AddListener(OnRespawnButtonClick);
            _mainMenuButton.onClick.AddListener(OnMainMenuButtonClick);
        }

        protected override void UnsubscribeFromButtons()
        {
            _respawnButton.onClick.RemoveListener(OnRespawnButtonClick);
            _mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClick);
        }

        private void OnRespawnButtonClick()
        {
        }

        public void OnMainMenuButtonClick() =>
            _gameStateMachine.EnterState<MainMenuState>();
    }
}