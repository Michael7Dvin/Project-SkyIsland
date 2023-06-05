using Cysharp.Threading.Tasks;

namespace Gameplay.Levels
{
    public class WorldObjectsSpawnerProvider : IWorldObjectsSpawnerProvider
    {
        private IWorldObjectsSpawner _currentWorldObjectsSpawner;

        public async UniTask<IWorldObjectsSpawner> Get(LevelType levelType)
        {
            await UniTask.WaitUntil(() => _currentWorldObjectsSpawner.LevelType == levelType);
            return _currentWorldObjectsSpawner;
        }
        
        public void Set(IWorldObjectsSpawner worldObjectsSpawner) => 
            _currentWorldObjectsSpawner = worldObjectsSpawner;
    }
}