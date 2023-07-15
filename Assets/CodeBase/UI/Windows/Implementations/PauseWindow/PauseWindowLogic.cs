using Infrastructure.GameFSM;
using Infrastructure.GameFSM.States;
using Infrastructure.Progress.Services;
using UnityEngine;

namespace UI.Windows.Implementations.PauseWindow
{
    public class PauseWindowLogic
    {
        private readonly ILevelProgressService _levelProgressService;
        private readonly IGameStateMachine _gameStateMachine;

        public PauseWindowLogic(ILevelProgressService levelProgressService, IGameStateMachine gameStateMachine)
        {
            _levelProgressService = levelProgressService;
            _gameStateMachine = gameStateMachine;
        }

        public void OpenOptions()
        {
        }

        public void SaveProgess() => 
            _levelProgressService.SaveCurrentProgress();

        public void ReturnToMainMenu() => 
         _gameStateMachine.EnterState<MainMenuState>();
    }
}