using Cysharp.Threading.Tasks;
using Zenject;

namespace Infrastructure.Services.LevelLoading.WorldObjectsSpawning
{
    public interface IWorldObjectsSpawner : IInitializable
    {
        LevelType LevelType { get; }
        UniTask SpawnWorldObjects();
    }
}