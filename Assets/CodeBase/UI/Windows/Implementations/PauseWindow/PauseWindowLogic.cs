using Infrastructure.GameFSM;
using Infrastructure.GameFSM.States;
using Infrastructure.LevelLoading;
using Infrastructure.Progress;
using Infrastructure.SceneServices.ProgressServices;
using Infrastructure.Services.SceneLoading;
using UI.Services.Operating;

namespace UI.Windows.Implementations.PauseWindow
{
    public class PauseWindowLogic
    {
        private readonly ILevelProgressService _levelProgressService;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IWindowsOperator _windowsOperator;
        private readonly ISceneLoader _sceneLoader;

        public PauseWindowLogic(ILevelProgressService levelProgressService,
            IGameStateMachine gameStateMachine,
            IWindowsOperator windowsOperator,
            ISceneLoader sceneLoader)
        {
            _levelProgressService = levelProgressService;
            _gameStateMachine = gameStateMachine;
            _windowsOperator = windowsOperator;
            _sceneLoader = sceneLoader;
        }

        public void OpenOptions()
        {
        }

        public void LoadLastSavedProgress()
        {
            AllProgress currentProgress = _levelProgressService.CurrentProgress;
            
            SceneID saveSceneId = currentProgress.SceneID;
            SceneID currentSceneId = _sceneLoader.CurrentSceneID;

            if (saveSceneId == currentSceneId)
            {
                _gameStateMachine.EnterState<LevelRestartState, AllProgress>(currentProgress);
                _windowsOperator.CloseWindow(WindowType.Pause);
            }
            else
            {
                LevelLoadRequest request = new LevelLoadRequest(currentProgress.SceneID, currentProgress);
                _gameStateMachine.EnterState<LevelLoadingState, LevelLoadRequest>(request);
            }
        }

        public void SaveProgess() => 
            _levelProgressService.Save();

        public void ReturnToMainMenu() => 
            _gameStateMachine.EnterState<MainMenuState>();
    }
}