using Infrastructure.GameFSM;
using Infrastructure.GameFSM.States;
using Infrastructure.LevelLoading.SceneServices.ProgressServices;
using Infrastructure.Progress;
using UI.Services.Operating;

namespace UI.Windows.Implementations.PauseWindow
{
    public class PauseWindowLogic
    {
        private readonly ILevelProgressService _levelProgressService;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IWindowsOperator _windowsOperator;

        public PauseWindowLogic(ILevelProgressService levelProgressService,
            IGameStateMachine gameStateMachine,
            IWindowsOperator windowsOperator)
        {
            _levelProgressService = levelProgressService;
            _gameStateMachine = gameStateMachine;
            _windowsOperator = windowsOperator;
        }

        public void OpenOptions()
        {
        }

        public void LoadLastSavedProgress()
        {
            AllProgress currentProgress = _levelProgressService.CurrentProgress;
            LevelLoadingRequest request = new LevelLoadingRequest(currentProgress.CurrentScene, currentProgress);
            
            _windowsOperator.CloseWindow(WindowType.Pause);
            _gameStateMachine.EnterState<LevelLoadingState, LevelLoadingRequest>(request);
        }

        public void SaveProgess() => 
            _levelProgressService.SaveCurrentProgress();

        public void ReturnToMainMenu() => 
         _gameStateMachine.EnterState<MainMenuState>();
    }
}