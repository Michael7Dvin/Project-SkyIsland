using Cysharp.Threading.Tasks;
using Zenject;

namespace Gameplay.Levels.WorldObjectsSpawning
{
    public interface IWorldObjectsSpawner : IInitializable
    {
        LevelType LevelType { get; }
        UniTask SpawnWorldObjects();
    }
}