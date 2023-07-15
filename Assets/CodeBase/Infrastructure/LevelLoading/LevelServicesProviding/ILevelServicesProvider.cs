using Cysharp.Threading.Tasks;
using Infrastructure.LevelLoading.WarmUpping;
using Infrastructure.LevelLoading.WorldObjectsSpawning;
using Infrastructure.Progress.Services;

namespace Infrastructure.LevelLoading.LevelServicesProviding
{
    public interface ILevelServicesProvider
    {
        UniTask<IWorldObjectsSpawner> GetWorldObjectsSpawner();
        UniTask<IWarmUpper> GetWarmUpper();
        UniTask<ILevelProgressService> GetProgressService();

        void SetWorldObjectsSpawner(IWorldObjectsSpawner worldObjectsSpawner);
        void SetWarmUpper(IWarmUpper warmUpper);
        void SetProgressService(ILevelProgressService levelProgressLoader);
    }
}