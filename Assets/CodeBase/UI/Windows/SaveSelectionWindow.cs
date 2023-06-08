using Gameplay.Levels;
using Infrastructure.GameFSM;
using Infrastructure.GameFSM.States;
using Infrastructure.Services.SceneLoading;
using Infrastructure.Services.StaticDataProviding;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public class SaveSelectionWindow : BaseWindow
    {
        [SerializeField] private Button _closeButton;
        
        [SerializeField] private Button _saveSlot1Button;
        [SerializeField] private Button _saveSlot2Button;
        [SerializeField] private Button _saveSlot3Button;

        private LevelData _tempNewGameLevelData;
        
        private IGameStateMachine _gameStateMachine;

        public void Construct(IGameStateMachine gameStateMachine, IStaticDataProvider staticDataProvider)
        {
            _gameStateMachine = gameStateMachine;
            ScenesData scenesData = staticDataProvider.GetScenesData();

            _tempNewGameLevelData = new LevelData(LevelType.Island, scenesData.IslandSceneName);
        }
        
        protected override void SubscribeOnButtons()
        {
            _closeButton.onClick.AddListener(CloseWindow);   
            
            _saveSlot1Button.onClick.AddListener(StartNewGame);
            _saveSlot2Button.onClick.AddListener(StartNewGame);
            _saveSlot3Button.onClick.AddListener(StartNewGame);
        }

        protected override void UnsubscribeFromButtons()
        {
            _closeButton.onClick.RemoveListener(CloseWindow);
            
            _saveSlot1Button.onClick.RemoveListener(StartNewGame);
            _saveSlot2Button.onClick.RemoveListener(StartNewGame);
            _saveSlot3Button.onClick.RemoveListener(StartNewGame);
        }

        private void StartNewGame() => 
            _gameStateMachine.EnterState<LoadLevelState, LevelData>(_tempNewGameLevelData);
    }
}