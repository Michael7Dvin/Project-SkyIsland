using Cysharp.Threading.Tasks;
using Infrastructure.GameFSM;
using Infrastructure.GameFSM.States;
using Infrastructure.Progress;
using Infrastructure.Services.SaveLoadService;
using Infrastructure.Services.SceneLoading;

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

        public async void StartGame(SaveSlotID saveSlotID)
        {
            LevelLoadingRequest levelLoadingRequest = await CreateLevelLoadingRequest(saveSlotID);
            _gameStateMachine.EnterState<LevelLoadingState, LevelLoadingRequest>(levelLoadingRequest);
        }

        public UniTask<(bool isSuccessful, AllProgress result)> GetProgress(SaveSlotID saveSlotID) =>
            _saveLoadService.TryLoad(saveSlotID);
        
        public void DeleteSaveFile(SaveSlotID saveSlotID) =>
            _saveLoadService.DeleteSaveFile(saveSlotID);

        private async UniTask<LevelLoadingRequest> CreateLevelLoadingRequest(SaveSlotID saveSlotID)
        {
            AllProgress currentProgress = await GetAllProgress(saveSlotID);

            SceneType currentLevelScene = currentProgress.CurrentScene;
            LevelLoadingRequest levelLoadingRequest = new LevelLoadingRequest(currentLevelScene, currentProgress);
            return levelLoadingRequest;
        }

        private async UniTask<AllProgress> GetAllProgress(SaveSlotID saveSlotID)
        {
            (bool isSuccessful, AllProgress result) progressLoading = await _saveLoadService.TryLoad(saveSlotID);

            if (progressLoading.isSuccessful == true)
                return progressLoading.result;

            return new AllProgress(saveSlotID);
        }
    }
}