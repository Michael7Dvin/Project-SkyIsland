using Infrastructure.SceneServices.ProgressServices;
using Infrastructure.SceneServices.WarmUppers;
using Infrastructure.SceneServices.WorldObjectsSpawners;

namespace Infrastructure.SceneServices.SceneServicesProviding
{
    public class SceneServicesProvider : ISceneServicesProvider
    {
        public IWarmUpper WarmUpper { get; private set; }
        public IWorldObjectsSpawner WorldObjectsSpawner { get; private set; }
        public ILevelProgressService ProgressService { get; private set; }

        public void SetWarmUpper(IWarmUpper warmUpper) => 
            WarmUpper = warmUpper;

        public void SetWorldObjectsSpawner(IWorldObjectsSpawner worldObjectsSpawner) => 
            WorldObjectsSpawner = worldObjectsSpawner;

        public void SetProgressService(ILevelProgressService levelProgressLoader) => 
            ProgressService = levelProgressLoader;
    }
}