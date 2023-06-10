using Cysharp.Threading.Tasks;

namespace Gameplay.Levels.WorldObjectsSpawning
{
    public interface IWorldObjectsSpawnerProvider
    {
        UniTask<IWorldObjectsSpawner> Get(LevelType levelType);
        void Set(IWorldObjectsSpawner worldObjectsSpawner);
    }
}