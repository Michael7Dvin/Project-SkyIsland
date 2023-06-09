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
            BootstrapState bootstrapState,
            InitializationState initializationState,
            MainMenuState mainMenuState,
            LoadLevelState loadLevelState,
            GameplayState gameplayState)
        {
            _gameStateMachine = gameStateMachine;
            
            _gameStateMachine.AddState(bootstrapState);
            _gameStateMachine.AddState(initializationState);
            _gameStateMachine.AddState(mainMenuState);
            _gameStateMachine.AddState(loadLevelState);
            _gameStateMachine.AddState(gameplayState);
        }
        
        public void Initialize() => 
            _gameStateMachine.EnterState<BootstrapState>();
    }
}