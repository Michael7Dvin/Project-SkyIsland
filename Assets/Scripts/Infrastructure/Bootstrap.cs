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
            LoadLevelState loadLevelState,
            GamePlayState gamePlayState)
        {
            _gameStateMachine = gameStateMachine;
            
            _gameStateMachine.AddState(bootstrapState);
            _gameStateMachine.AddState(mainMenuState);
            _gameStateMachine.AddState(loadLevelState);
            _gameStateMachine.AddState(gamePlayState);
        }

        private void Awake() => _gameStateMachine.EnterState<BootstrapState>();
    }
}