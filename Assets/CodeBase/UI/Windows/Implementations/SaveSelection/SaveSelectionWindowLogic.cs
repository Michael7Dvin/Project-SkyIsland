using System.Threading.Tasks;
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

        public async void StartGame(SaveSlot saveSlot)
        {
            LevelLoadingRequest levelLoadingRequest = await CreateLevelLoadingRequest(saveSlot);
            _gameStateMachine.EnterState<LevelLoadingState, LevelLoadingRequest>(levelLoadingRequest);
        }

        private async UniTask<LevelLoadingRequest> CreateLevelLoadingRequest(SaveSlot saveSlot)
        {
            AllProgress currentProgress = await GetAllProgress(saveSlot);

            SceneType currentLevelScene = currentProgress.CurrentScene;
            LevelLoadingRequest levelLoadingRequest = new LevelLoadingRequest(currentLevelScene, currentProgress);
            return levelLoadingRequest;
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