using Infrastructure.SceneServices.ProgressServices;
using Infrastructure.SceneServices.SceneServicesProviding;
using Infrastructure.SceneServices.WarmUppers;
using Infrastructure.SceneServices.WorldObjectsSpawners;
using Zenject;

namespace Infrastructure.SceneServices.SceneServicesSetting
{
    public class IslandServicesSetter : IInitializable
    {
        private readonly ISceneServicesProvider _sceneServicesProvider;

        private readonly IWarmUpper _warmUpper;
        private readonly IWorldObjectsSpawner _worldObjectsSpawner;
        private readonly ILevelProgressService _levelProgressService;

        public IslandServicesSetter(ISceneServicesProvider sceneServicesProvider,
            IWarmUpper warmUpper,
            IWorldObjectsSpawner worldObjectsSpawner,
            ILevelProgressService levelProgressService)
        {
            _sceneServicesProvider = sceneServicesProvider;
            _warmUpper = warmUpper;
            _worldObjectsSpawner = worldObjectsSpawner;
            _levelProgressService = levelProgressService;
        }

        public void Initialize()
        {
            _sceneServicesProvider.SetWarmUpper(_warmUpper);
            _sceneServicesProvider.SetWorldObjectsSpawner(_worldObjectsSpawner);
            _sceneServicesProvider.SetProgressService(_levelProgressService);
        }
    }
}