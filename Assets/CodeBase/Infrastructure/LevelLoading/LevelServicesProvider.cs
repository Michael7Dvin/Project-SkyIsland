using Cysharp.Threading.Tasks;
using Infrastructure.LevelLoading.WarmUpping;
using Infrastructure.LevelLoading.WorldObjectsSpawning;

namespace Infrastructure.LevelLoading
{
    public class LevelServicesProvider : ILevelServicesProvider
    {
        private IWorldObjectsSpawner _currentWorldObjectsSpawner;
        private IWarmUpper _currentWarmUpper;
        
        public async UniTask<IWorldObjectsSpawner> GetWorldObjectsSpawner(LevelType levelType)
        {
            await UniTask.WaitUntil(() => _currentWorldObjectsSpawner.LevelType == levelType);
            return _currentWorldObjectsSpawner;
        }

        public async UniTask<IWarmUpper> GetWarmUpper(LevelType levelType)
        {
            await UniTask.WaitUntil(() => _currentWarmUpper.LevelType == levelType);
            return _currentWarmUpper;
        }

        public void SetWorldObjectsSpawner(IWorldObjectsSpawner worldObjectsSpawner) => 
            _currentWorldObjectsSpawner = worldObjectsSpawner;

        public void SetWarmUpper(IWarmUpper warmUpper) => 
            _currentWarmUpper = warmUpper;
    }
}