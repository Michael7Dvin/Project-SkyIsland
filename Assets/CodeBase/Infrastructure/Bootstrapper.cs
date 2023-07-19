using Infrastructure.GameFSM;
using Infrastructure.GameFSM.States;
using Zenject;

namespace Infrastructure
{
    public class Bootstrapper : IInitializable
    {
        private readonly IGameStateMachine _gameStateMachine;

        public Bootstrapper(IGameStateMachine gameStateMachine,
            InitializationState initializationState,
            MainMenuState mainMenuState,
            LevelLoadingState levelLoadingState,
            LevelRestartState levelRestartState,
            GameplayState gameplayState,
            QuitState quitState)
        {
            _gameStateMachine = gameStateMachine;
            
            _gameStateMachine.AddState(initializationState);
            _gameStateMachine.AddState(mainMenuState);
            _gameStateMachine.AddState(levelLoadingState);
            _gameStateMachine.AddState(levelRestartState);
            _gameStateMachine.AddState(gameplayState);
            _gameStateMachine.AddState(quitState);
        }
        
        public void Initialize() => 
            _gameStateMachine.EnterState<InitializationState>();
    }
}