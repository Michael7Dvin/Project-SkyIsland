using Cysharp.Threading.Tasks;
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

        public async void StartGame(SaveSlot saveSlot)
        {
            AllProgress currentProgress = await GetAllProgress(saveSlot);

            LevelData currentLevelData = currentProgress.CurrentLevel;
            LevelLoadingRequest levelLoadingRequest = new LevelLoadingRequest(currentLevelData, currentProgress);
            
            _gameStateMachine.EnterState<LevelLoadingState, LevelLoadingRequest>(levelLoadingRequest);
        }

        private async UniTask<AllProgress> GetAllProgress(SaveSlot saveSlot)
        {
            (bool isSuccessful, AllProgress result) progressLoading = await _saveLoadService.TryLoad(saveSlot);

            if (progressLoading.isSuccessful == true)
                return progressLoading.result;

            return new AllProgress(saveSlot);
        }
    }
}