using Cysharp.Threading.Tasks;
using Infrastructure.LevelLoading.SceneServices.SceneServicesProviding;
using Infrastructure.Progress;
using Infrastructure.Progress.Handling.Heros;
using Infrastructure.Progress.Handling.IslandLevel;
using Infrastructure.Services.SaveLoadService;
using Zenject;

namespace Infrastructure.LevelLoading.SceneServices.ProgressServices
{
    public class IslandLevelProgressService : ILevelProgressService, IInitializable
    {
        private readonly IHeroProgressHandler _heroProgressHandler;
        private readonly IIslandWorldProgressHandler _worldProgressHandler;
        
        private readonly ISceneServicesProvider _sceneServicesProvider;
        private readonly ISaveLoadService _saveLoadService;
        
        public IslandLevelProgressService(IHeroProgressHandler heroProgressHandler,
            IIslandWorldProgressHandler worldProgressHandler,
            ISceneServicesProvider sceneServicesProvider,
            ISaveLoadService saveLoadService)
        {
            _heroProgressHandler = heroProgressHandler;
            _worldProgressHandler = worldProgressHandler;
            
            _sceneServicesProvider = sceneServicesProvider;
            _saveLoadService = saveLoadService;
        }

        public void Initialize() => 
            SetSelfToProvider();

        public AllProgress CurrentProgress { get; private set; }

        public void SetCurrentProgress(AllProgress progress) => 
            CurrentProgress = progress;

        public async UniTask SaveCurrentProgress()
        {
            _heroProgressHandler.WriteProgress(CurrentProgress.HeroProgress);
            _worldProgressHandler.WriteProgress(CurrentProgress.IslandWorldProgress);

            await _saveLoadService.Save(CurrentProgress);
        }

        public void LoadCurrentProgress()
        {
            _heroProgressHandler.LoadProgress(CurrentProgress.HeroProgress);
            _worldProgressHandler.LoadProgress(CurrentProgress.IslandWorldProgress);
        }

        private void SetSelfToProvider() => 
            _sceneServicesProvider.SetProgressService(this);
    }
}