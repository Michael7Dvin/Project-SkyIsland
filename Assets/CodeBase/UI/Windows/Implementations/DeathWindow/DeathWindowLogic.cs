using Infrastructure.GameFSM;
using Infrastructure.GameFSM.States;

namespace UI.Windows.Implementations.DeathWindow
{
    public class DeathWindowLogic
    {
        private readonly IGameStateMachine _gameStateMachine;

        public DeathWindowLogic(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Respawn()
        {
        }
        
        public void ReturnToMainMenu() => 
            _gameStateMachine.EnterState<MainMenuState>();
    }
}