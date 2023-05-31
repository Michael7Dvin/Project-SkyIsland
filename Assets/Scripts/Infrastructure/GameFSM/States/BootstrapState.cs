using Common.FSM;
using Infrastructure.Services.SceneLoading;
using Infrastructure.Services.UtilityDataProviding;

namespace Infrastructure.GameFSM.States
{
    public class BootstrapState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IUtilityDataProvider _utilityDataProvider;
        
        public BootstrapState(IGameStateMachine gameStateMachine, IUtilityDataProvider utilityDataProvider)
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

        private void OnMainMenuLoaded() => 
            _gameStateMachine.EnterState<MainMenuState>();
    }
}