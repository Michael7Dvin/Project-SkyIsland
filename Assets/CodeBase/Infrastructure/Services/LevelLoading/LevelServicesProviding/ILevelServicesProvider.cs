using Cysharp.Threading.Tasks;
using Infrastructure.Progress.LevelProgressLoading;
using Infrastructure.Services.LevelLoading.WarmUpping;
using Infrastructure.Services.LevelLoading.WorldObjectsSpawning;

namespace Infrastructure.Services.LevelLoading.LevelServicesProviding
{
    public interface ILevelServicesProvider
    {
        UniTask<IWorldObjectsSpawner> GetWorldObjectsSpawner(LevelType levelType);
        UniTask<IWarmUpper> GetWarmUpper(LevelType levelType);
        UniTask<IProgressLoader> GetProgressInitializer(LevelType levelType);

        void SetWorldObjectsSpawner(IWorldObjectsSpawner worldObjectsSpawner);
        void SetWarmUpper(IWarmUpper warmUpper);
        void SetProgressInitializer(IProgressLoader progressLoader);
    }
}