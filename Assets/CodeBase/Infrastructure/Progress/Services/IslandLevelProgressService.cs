using Infrastructure.LevelLoading.LevelServicesProviding;
using Infrastructure.Progress.Handling.Heros;
using Infrastructure.Progress.Handling.IslandLevel;
using Infrastructure.Services.SaveLoadService;
using Zenject;

namespace Infrastructure.Progress.Services
{
    public class IslandLevelProgressService : ILevelProgressService, IInitializable
    {
        private AllProgress _currentProgress;

        private readonly IHeroProgressHandler _heroProgressHandler;
        private readonly IIslandWorldProgressHandler _worldProgressHandler;
        
        private readonly ILevelServicesProvider _levelServicesProvider;
        private readonly ISaveLoadService _saveLoadService;

        public IslandLevelProgressService(IHeroProgressHandler heroProgressHandler,
            IIslandWorldProgressHandler worldProgressHandler,
            ILevelServicesProvider levelServicesProvider,
            ISaveLoadService saveLoadService)
        {
            _heroProgressHandler = heroProgressHandler;
            _worldProgressHandler = worldProgressHandler;
            
            _levelServicesProvider = levelServicesProvider;
            _saveLoadService = saveLoadService;
        }

        public void Initialize() => 
            SetSelfToProvider();

        public void SetCurrentProgress(AllProgress progress) => 
            _currentProgress = progress;

        public void SaveCurrentProgress()
        {
            _heroProgressHandler.WriteProgress(_currentProgress.HeroProgress);
            _worldProgressHandler.WriteProgress(_currentProgress.IslandWorldProgress);
            
            _saveLoadService.Save(_currentProgress);
        }

        public void LoadCurrentProgress()
        {
            _heroProgressHandler.LoadProgress(_currentProgress.HeroProgress);
            _worldProgressHandler.LoadProgress(_currentProgress.IslandWorldProgress);
        }

        private void SetSelfToProvider() => 
            _levelServicesProvider.SetProgressService(this);
    }
}