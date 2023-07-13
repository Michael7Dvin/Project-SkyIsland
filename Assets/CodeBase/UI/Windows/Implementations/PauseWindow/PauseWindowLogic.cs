using Infrastructure.GameFSM;
using Infrastructure.GameFSM.States;
using Infrastructure.Progress;

namespace UI.Windows.Implementations.PauseWindow
{
    public class PauseWindowLogic
    {
        private readonly IGameProgressService _gameProgressService;
        private readonly IGameStateMachine _gameStateMachine;

        public PauseWindowLogic(IGameProgressService gameProgressService, IGameStateMachine gameStateMachine)
        {
            _gameProgressService = gameProgressService;
            _gameStateMachine = gameStateMachine;
        }

        public void OpenOptions()
        {
        }

        public void SaveProgess() => 
            _gameProgressService.SaveCurrentProgress();

        public void ReturnToMainMenu() => 
         _gameStateMachine.EnterState<MainMenuState>();
    }
}