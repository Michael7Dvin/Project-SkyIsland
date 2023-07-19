using System;
using System.Globalization;
using Cysharp.Threading.Tasks;
using Infrastructure.Progress;
using Infrastructure.Progress.Handling.Heros;
using Infrastructure.Progress.Handling.IslandLevel;
using Infrastructure.Services.SaveLoadService;
using Infrastructure.Services.SceneLoading;

namespace Infrastructure.SceneServices.ProgressServices
{
    public class IslandLevelProgressService : ILevelProgressService
    {
        private readonly IHeroProgressHandler _heroProgressHandler;
        private readonly IIslandWorldProgressHandler _worldProgressHandler;
        
        private readonly ISaveLoadService _saveLoadService;
        private readonly ISceneLoader _sceneLoader;

        public IslandLevelProgressService(IHeroProgressHandler heroProgressHandler,
            IIslandWorldProgressHandler worldProgressHandler,
            ISaveLoadService saveLoadService,
            ISceneLoader sceneLoader)
        {
            _heroProgressHandler = heroProgressHandler;
            _worldProgressHandler = worldProgressHandler;
            _saveLoadService = saveLoadService;
            _sceneLoader = sceneLoader;
        }
        
        public AllProgress CurrentProgress { get; private set; }

        public void SetCurrentProgress(AllProgress progress) => 
            CurrentProgress = progress;

        public async UniTask Save()
        {
            WriteCurrentProgress();
            await _saveLoadService.Save(CurrentProgress);
        }

        public void Load()
        {
            _heroProgressHandler.LoadProgress(CurrentProgress.HeroProgress);
            _worldProgressHandler.LoadProgress(CurrentProgress.IslandWorldProgress);
        }
        
        private void WriteCurrentProgress()
        {
            CurrentProgress.SceneID = _sceneLoader.CurrentSceneID;
            CurrentProgress.LastSaveDateTime = DateTime.Now.ToString(CultureInfo.InvariantCulture);

            _heroProgressHandler.WriteProgress(CurrentProgress.HeroProgress);
            _worldProgressHandler.WriteProgress(CurrentProgress.IslandWorldProgress);
        }
    }
}