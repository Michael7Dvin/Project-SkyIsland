using Cysharp.Threading.Tasks;
using Zenject;

namespace Infrastructure.LevelLoading.WorldObjectsSpawning
{
    public interface IWorldObjectsSpawner : IInitializable
    {
        UniTask SpawnWorldObjects();
    }
}