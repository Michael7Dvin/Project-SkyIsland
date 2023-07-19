using Infrastructure.SceneServices.ProgressServices;
using Infrastructure.SceneServices.WarmUppers;
using Infrastructure.SceneServices.WorldObjectsSpawners;

namespace Infrastructure.SceneServices.SceneServicesProviding
{
    public interface ISceneServicesProvider
    {
        IWarmUpper WarmUpper { get; }
        IWorldObjectsSpawner WorldObjectsSpawner { get; }
        ILevelProgressService ProgressService { get; }

        void SetWorldObjectsSpawner(IWorldObjectsSpawner worldObjectsSpawner);
        void SetWarmUpper(IWarmUpper warmUpper);
        void SetProgressService(ILevelProgressService levelProgressLoader);
    }
}