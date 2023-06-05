using Cysharp.Threading.Tasks;

namespace Gameplay.Levels
{
    public interface IWorldObjectsSpawnerProvider
    {
        UniTask<IWorldObjectsSpawner> Get(LevelType levelType);
        void Set(IWorldObjectsSpawner worldObjectsSpawner);
    }
}