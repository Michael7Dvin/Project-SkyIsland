using Cysharp.Threading.Tasks;
using Infrastructure.GameFSM;
using Infrastructure.GameFSM.States;
using Infrastructure.LevelLoading;
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
            LevelLoadRequest levelLoadRequest = await CreateLevelLoadingRequest(saveSlotID);
            _gameStateMachine.EnterState<LevelLoadingState, LevelLoadRequest>(levelLoadRequest);
        }

        public UniTask<(bool isSuccessful, AllProgress result)> GetProgress(SaveSlotID saveSlotID) =>
            _saveLoadService.TryLoad(saveSlotID);
        
        public void DeleteSaveFile(SaveSlotID saveSlotID) =>
            _saveLoadService.DeleteSaveFile(saveSlotID);

        private async UniTask<LevelLoadRequest> CreateLevelLoadingRequest(SaveSlotID saveSlotID)
        {
            AllProgress currentProgress = await GetAllProgress(saveSlotID);

            SceneID currentLevelScene = currentProgress.SceneID;
            LevelLoadRequest levelLoadRequest = new LevelLoadRequest(currentLevelScene, currentProgress);
            return levelLoadRequest;
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