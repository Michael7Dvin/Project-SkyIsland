using Infrastructure.GameFSM;
using Infrastructure.GameFSM.States;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class Bootstrapper : MonoBehaviour, IInitializable
    {
        private IGameStateMachine _gameStateMachine;

        [Inject]
        public void Construct(IGameStateMachine gameStateMachine,
            InitializationState initializationState,
            MainMenuState mainMenuState,
            LevelLoadingState levelLoadingState,
            GameplayState gameplayState,
            QuitState quitState)
        {
            _gameStateMachine = gameStateMachine;
            
            _gameStateMachine.AddState(initializationState);
            _gameStateMachine.AddState(mainMenuState);
            _gameStateMachine.AddState(levelLoadingState);
            _gameStateMachine.AddState(gameplayState);
            _gameStateMachine.AddState(quitState);
        }
        
        public void Initialize() => 
            _gameStateMachine.EnterState<InitializationState>();
    }
}