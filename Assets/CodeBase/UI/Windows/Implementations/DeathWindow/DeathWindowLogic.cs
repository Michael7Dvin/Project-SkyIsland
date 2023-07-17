using Infrastructure.GameFSM;
using Infrastructure.GameFSM.States;
using Infrastructure.LevelLoading.SceneServices.ProgressServices;
using Infrastructure.Progress;
using UI.Services.Operating;

namespace UI.Windows.Implementations.DeathWindow
{
    public class DeathWindowLogic
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ILevelProgressService _levelProgressService;
        private readonly IWindowsOperator _windowsOperator;

        public DeathWindowLogic(IGameStateMachine gameStateMachine, ILevelProgressService levelProgressService, IWindowsOperator windowsOperator)
        {
            _gameStateMachine = gameStateMachine;
            _levelProgressService = levelProgressService;
            _windowsOperator = windowsOperator;
        }

        public void LoadLastSavedProgress()
        {
            AllProgress currentProgress = _levelProgressService.CurrentProgress;
            LevelLoadingRequest request = new LevelLoadingRequest(currentProgress.CurrentScene, currentProgress);
            
            _windowsOperator.CloseWindow(WindowType.Death);
            _gameStateMachine.EnterState<LevelLoadingState, LevelLoadingRequest>(request);
        }
        
        public void ReturnToMainMenu() => 
            _gameStateMachine.EnterState<MainMenuState>();
    }
}