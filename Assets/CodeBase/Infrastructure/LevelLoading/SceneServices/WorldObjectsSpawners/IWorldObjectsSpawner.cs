using Cysharp.Threading.Tasks;
using Zenject;

namespace Infrastructure.LevelLoading.SceneServices.WorldObjectsSpawners
{
    public interface IWorldObjectsSpawner : IInitializable
    {
        UniTask SpawnWorldObjects();
    }
}