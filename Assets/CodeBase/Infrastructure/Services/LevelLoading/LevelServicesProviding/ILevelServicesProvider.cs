using Cysharp.Threading.Tasks;
using Infrastructure.Services.LevelLoading.WarmUpping;
using Infrastructure.Services.LevelLoading.WorldObjectsSpawning;

namespace Infrastructure.Services.LevelLoading.LevelServicesProviding
{
    public interface ILevelServicesProvider
    {
        UniTask<IWorldObjectsSpawner> GetWorldObjectsSpawner(LevelType levelType);
        UniTask<IWarmUpper> GetWarmUpper(LevelType levelType);
        void SetWorldObjectsSpawner(IWorldObjectsSpawner worldObjectsSpawner);
        void SetWarmUpper(IWarmUpper warmUpper);
    }
}