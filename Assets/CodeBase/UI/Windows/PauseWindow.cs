using Infrastructure.GameFSM;
using Infrastructure.GameFSM.States;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public class PauseWindow : BaseWindow
    {
        [SerializeField] private Button _closeButton;
        
        [SerializeField] private Button _optionsButton;
        [SerializeField] private Button _saveButton;
        [SerializeField] private Button _mainMenuButton;

        private IGameStateMachine _gameStateMachine;

        public void Construct(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        protected override void SubscribeOnButtons()
        {
            _closeButton.onClick.AddListener(CloseWindow);
            
            _optionsButton.onClick.AddListener(OnOptionsButtonClick);
            _saveButton.onClick.AddListener(OnSaveButtonClick);
            _mainMenuButton.onClick.AddListener(OnQuitButtonClick);
        }

        protected override void UnsubscribeFromButtons()
        {
            _closeButton.onClick.RemoveListener(CloseWindow);
            
            _optionsButton.onClick.RemoveListener(OnOptionsButtonClick);
            _saveButton.onClick.RemoveListener(OnSaveButtonClick);
            _mainMenuButton.onClick.RemoveListener(OnQuitButtonClick);
        }
        
        public void OnOptionsButtonClick()
        {
        }

        public void OnSaveButtonClick()
        {
        }

        public void OnQuitButtonClick() =>
            _gameStateMachine.EnterState<MainMenuState>();
    }
}