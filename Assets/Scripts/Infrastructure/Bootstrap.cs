using Infrastructure.GameFSM;
using Infrastructure.GameFSM.States;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class Bootstrap : MonoBehaviour
    {
        private GameStateMachine _gameStateMachine;

        [Inject]
        public void Construct(GameStateMachine gameStateMachine,
            BootstrapState bootstrapState,
            MainMenuState mainMenuState,
            LoadSceneState loadSceneState,
            GameplayState gameplayState)
        {
            _gameStateMachine = gameStateMachine;
            
            _gameStateMachine.AddState(bootstrapState);
            _gameStateMachine.AddState(mainMenuState);
            _gameStateMachine.AddState(loadSceneState);
            _gameStateMachine.AddState(gameplayState);
        }

        private void Start() => _gameStateMachine.EnterState<BootstrapState>();
    }
}