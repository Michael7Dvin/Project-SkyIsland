using Infrastructure.SceneServices.SceneServicesProviding;
using Infrastructure.SceneServices.WarmUppers;
using Infrastructure.SceneServices.WorldObjectsSpawners;
using Zenject;

namespace Infrastructure.SceneServices.SceneServicesSetting
{
    public class MainMenuServicesSetter : IInitializable
    {
        private readonly ISceneServicesProvider _sceneServicesProvider;

        private readonly IWarmUpper _warmUpper;
        private readonly IWorldObjectsSpawner _worldObjectsSpawner;

        public MainMenuServicesSetter(ISceneServicesProvider sceneServicesProvider,
            IWarmUpper warmUpper,
            IWorldObjectsSpawner worldObjectsSpawner)
        {
            _sceneServicesProvider = sceneServicesProvider;
            _warmUpper = warmUpper;
            _worldObjectsSpawner = worldObjectsSpawner;
        }

        public void Initialize()
        {
            _sceneServicesProvider.SetWarmUpper(_warmUpper);
            _sceneServicesProvider.SetWorldObjectsSpawner(_worldObjectsSpawner);
        }
    }
}