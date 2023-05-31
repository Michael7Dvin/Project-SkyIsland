using Gameplay.Dying;
using Infrastructure.GameFSM;
using Infrastructure.GameFSM.States;
using Infrastructure.Services.SceneLoading;
using Infrastructure.Services.UtilityDataProviding;

namespace Infrastructure.Services.PlayerDeathService
{
    public class PlayerDeathService : IPlayerDeathService
    {
        private IDeath _playerDeath;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IUtilityDataProvider _utilityDataProvider;

        public PlayerDeathService(IGameStateMachine gameStateMachine, IUtilityDataProvider utilityDataProvider)
        {
            _gameStateMachine = gameStateMachine;
            _utilityDataProvider = utilityDataProvider;
        }

        public void Initialize(IDeath playerDeath)
        {
            _playerDeath = playerDeath;
            _playerDeath.Died += OnPlayerDied;
        }

        private void OnPlayerDied()
        {
            _playerDeath.Died -= OnPlayerDied;
            _playerDeath = null;
                
            SceneLoadRequest request = new(_utilityDataProvider.ScenesInfo.IslandSceneName, OnLevelLoaded);
            _gameStateMachine.EnterState<LoadSceneState, SceneLoadRequest>(request);
        }
        
        private void OnLevelLoaded() => 
            _gameStateMachine.EnterState<GameplayState>();
    }
}