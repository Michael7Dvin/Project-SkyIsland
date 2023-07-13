using Infrastructure.GameFSM;
using Infrastructure.GameFSM.States;
using Infrastructure.Progress;
using Infrastructure.Services.LevelLoading.Data;
using Infrastructure.Services.SaveLoadService;

namespace UI.Windows.Implementations.SaveSelection
{
    public class SaveSelectionWindowLogic
    {
        private readonly ISaveLoadService _saveLoadService;
        private readonly IGameProgressService _gameProgressService;
        private readonly IGameStateMachine _gameStateMachine;

        public SaveSelectionWindowLogic(ISaveLoadService saveLoadService, IGameProgressService gameProgressService, IGameStateMachine gameStateMachine)
        {
            _saveLoadService = saveLoadService;
            _gameProgressService = gameProgressService;
            _gameStateMachine = gameStateMachine;
        }

        public void StartGame(SaveSlot saveSlot)
        {
            SetCurrentProgress(saveSlot);
            LevelData currentLevel = _gameProgressService.CurrentProgress.CurrentLevel;
            
            _gameStateMachine.EnterState<LevelLoadingState, LevelData>(currentLevel);
        }

        private void SetCurrentProgress(SaveSlot saveSlot)
        {
            if (_saveLoadService.TryLoad(saveSlot, out AllProgress progress) == false)
            {
                progress = new AllProgress(saveSlot);
            }

            _gameProgressService.SetCurrentProgress(progress);
        }
    }
}