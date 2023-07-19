using Cysharp.Threading.Tasks;

namespace Infrastructure.SceneServices.WorldObjectsSpawners
{
    public interface IWorldObjectsSpawner
    {
        UniTask SpawnWorldObjects();
    }
}