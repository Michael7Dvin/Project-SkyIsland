using Cysharp.Threading.Tasks;
using Infrastructure.LevelLoading.WarmUpping;
using Infrastructure.LevelLoading.WorldObjectsSpawning;
using Infrastructure.Progress.Services;

namespace Infrastructure.LevelLoading.LevelServicesProviding
{
    public class LevelServicesProvider : ILevelServicesProvider
    {
        private IWorldObjectsSpawner _currentWorldObjectsSpawner;
        private IWarmUpper _currentWarmUpper;
        private ILevelProgressService _currentProgresService;
        
        public async UniTask<IWorldObjectsSpawner> GetWorldObjectsSpawner()
        {
            await UniTask.WaitUntil(() => _currentWorldObjectsSpawner != null);
            return _currentWorldObjectsSpawner;
        }

        public async UniTask<IWarmUpper> GetWarmUpper()
        {
            await UniTask.WaitUntil(() => _currentWarmUpper != null);
            return _currentWarmUpper;
        }

        public async UniTask<ILevelProgressService> GetProgressService()
        {
            await UniTask.WaitUntil(() => _currentProgresService != null);
            return _currentProgresService;
        }

        public void SetWorldObjectsSpawner(IWorldObjectsSpawner worldObjectsSpawner) => 
            _currentWorldObjectsSpawner = worldObjectsSpawner;

        public void SetWarmUpper(IWarmUpper warmUpper) => 
            _currentWarmUpper = warmUpper;

        public void SetProgressService(ILevelProgressService levelProgressLoader) => 
            _currentProgresService = levelProgressLoader;
    }
}