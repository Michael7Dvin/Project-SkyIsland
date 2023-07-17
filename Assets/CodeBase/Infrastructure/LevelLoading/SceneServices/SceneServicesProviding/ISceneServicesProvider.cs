using Cysharp.Threading.Tasks;
using Infrastructure.LevelLoading.SceneServices.ProgressServices;
using Infrastructure.LevelLoading.SceneServices.WarmUppers;
using Infrastructure.LevelLoading.SceneServices.WorldObjectsSpawners;

namespace Infrastructure.LevelLoading.SceneServices.SceneServicesProviding
{
    public interface ISceneServicesProvider
    {
        UniTask<IWorldObjectsSpawner> GetWorldObjectsSpawner();
        UniTask<IWarmUpper> GetWarmUpper();
        UniTask<ILevelProgressService> GetProgressService();

        void SetWorldObjectsSpawner(IWorldObjectsSpawner worldObjectsSpawner);
        void SetWarmUpper(IWarmUpper warmUpper);
        void SetProgressService(ILevelProgressService levelProgressLoader);
    }
}