using Cysharp.Threading.Tasks;
using Infrastructure.LevelLoading.WarmUpping;
using Infrastructure.LevelLoading.WorldObjectsSpawning;

namespace Infrastructure.LevelLoading.LevelServicesProviding
{
    public interface ILevelServicesProvider
    {
        UniTask<IWorldObjectsSpawner> GetWorldObjectsSpawner(LevelType levelType);
        UniTask<IWarmUpper> GetWarmUpper(LevelType levelType);
        void SetWorldObjectsSpawner(IWorldObjectsSpawner worldObjectsSpawner);
        void SetWarmUpper(IWarmUpper warmUpper);
    }
}