using Infrastructure.Progress.Handling.Heros;
using Infrastructure.Progress.Handling.IslandLevel;
using Infrastructure.Services.LevelLoading;
using Infrastructure.Services.LevelLoading.Data;
using Infrastructure.Services.LevelLoading.LevelServicesProviding;

namespace Infrastructure.Progress.LevelProgressLoading
{
    public class IslandProgressLoader : IProgressLoader
    {
        private readonly HeroProgressHandler _heroProgressHandler;
        private readonly IslandLevelProgressHandler _islandLevelProgressHandler;
        private readonly IGameProgressService _gameProgressService;
        private readonly ILevelServicesProvider _levelServicesProvider;

        public IslandProgressLoader(HeroProgressHandler heroProgressHandler,
            IslandLevelProgressHandler islandLevelProgressHandler,
            IGameProgressService gameProgressService,
            ILevelServicesProvider levelServicesProvider)
        {
            _heroProgressHandler = heroProgressHandler;
            _islandLevelProgressHandler = islandLevelProgressHandler;
            _gameProgressService = gameProgressService;
            _levelServicesProvider = levelServicesProvider;
        }

        public LevelType LevelType => LevelType.Island;

        public void Initialize() => 
            SetSelfToProvider();

        private void SetSelfToProvider() => 
            _levelServicesProvider.SetProgressInitializer(this);

        public void InitializeProgressHandlers(LevelData levelData)
        {
            _gameProgressService.CurrentProgress.CurrentLevel = levelData;
            _heroProgressHandler.Init(_gameProgressService.CurrentProgress.HeroProgress);
            _islandLevelProgressHandler.Init(_gameProgressService.CurrentProgress.IslandLevelProgress);
        }

        public void RegisterProgressHandlers()
        {
            _gameProgressService.RegisterProgressHandler(_heroProgressHandler);
            _gameProgressService.RegisterProgressHandler(_islandLevelProgressHandler);
        }
    }
}