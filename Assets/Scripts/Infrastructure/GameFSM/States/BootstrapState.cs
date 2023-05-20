using Infrastructure.Services;
using Infrastructure.Services.SceneLoading;

namespace Infrastructure.GameFSM.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly UtilityDataProvider _utilityDataProvider;
        
        public BootstrapState(GameStateMachine gameStateMachine, UtilityDataProvider utilityDataProvider)
        {
            _gameStateMachine = gameStateMachine;
            _utilityDataProvider = utilityDataProvider;
        }

        public void Enter()
        {
            SceneLoadRequest request = new(_utilityDataProvider.ScenesInfo.MainMenuSceneName, OnMainMenuLoaded);
            _gameStateMachine.EnterState<LoadSceneState, SceneLoadRequest>(request);
        }

        public void Exit()
        {
        }

        private void OnMainMenuLoaded()
        {
            _gameStateMachine.EnterState<MainMenuState>();
        }
    }
}