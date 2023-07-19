using Infrastructure.GameFSM;
using Infrastructure.GameFSM.States;
using Infrastructure.LevelLoading;
using Infrastructure.Progress;
using Infrastructure.SceneServices.ProgressServices;
using Infrastructure.Services.SceneLoading;
using UI.Services.Operating;

namespace UI.Windows.Implementations.DeathWindow
{
    public class DeathWindowLogic
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ILevelProgressService _levelProgressService;
        private readonly IWindowsOperator _windowsOperator;
        private readonly ISceneLoader _sceneLoader;

        public DeathWindowLogic(IGameStateMachine gameStateMachine,
            ILevelProgressService levelProgressService,
            IWindowsOperator windowsOperator,
            ISceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _levelProgressService = levelProgressService;
            _windowsOperator = windowsOperator;
            _sceneLoader = sceneLoader;
        }

        public void LoadLastSavedProgress()
        {
            AllProgress currentProgress = _levelProgressService.CurrentProgress;
            
            SceneID saveSceneId = currentProgress.SceneID;
            SceneID currentSceneId = _sceneLoader.CurrentSceneID;

            if (saveSceneId == currentSceneId)
            {
                _gameStateMachine.EnterState<LevelRestartState, AllProgress>(currentProgress);
                _windowsOperator.CloseWindow(WindowType.Death);
            }
            else
            {
                LevelLoadRequest request = new LevelLoadRequest(currentProgress.SceneID, currentProgress);
                _gameStateMachine.EnterState<LevelLoadingState, LevelLoadRequest>(request);
            }
        }

        public void ReturnToMainMenu() => 
            _gameStateMachine.EnterState<MainMenuState>();
    }
}