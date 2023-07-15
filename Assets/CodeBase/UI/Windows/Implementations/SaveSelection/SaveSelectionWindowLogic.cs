using Infrastructure.GameFSM;
using Infrastructure.GameFSM.States;
using Infrastructure.LevelLoading.Data;
using Infrastructure.Progress;
using Infrastructure.Services.SaveLoadService;

namespace UI.Windows.Implementations.SaveSelection
{
    public class SaveSelectionWindowLogic
    {
        private readonly ISaveLoadService _saveLoadService;
        private readonly IGameStateMachine _gameStateMachine;

        public SaveSelectionWindowLogic(ISaveLoadService saveLoadService, IGameStateMachine gameStateMachine)
        {
            _saveLoadService = saveLoadService;
            _gameStateMachine = gameStateMachine;
        }

        public void StartGame(SaveSlot saveSlot)
        {
            AllProgress currentProgress = GetAllProgress(saveSlot);

            LevelData currentLevelData = currentProgress.CurrentLevel;
            LevelLoadingRequest levelLoadingRequest = new LevelLoadingRequest(currentLevelData, currentProgress);
            
            _gameStateMachine.EnterState<LevelLoadingState, LevelLoadingRequest>(levelLoadingRequest);
        }

        private AllProgress GetAllProgress(SaveSlot saveSlot)
        {
            if (_saveLoadService.TryLoad(saveSlot, out AllProgress progress) == true)
                return progress;

            return new AllProgress(saveSlot);
        }
    }
}