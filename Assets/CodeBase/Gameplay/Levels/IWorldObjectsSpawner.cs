using Zenject;

namespace Gameplay.Levels
{
    public interface IWorldObjectsSpawner : IInitializable
    {
        LevelType LevelType { get; }
        void SpawnWorldObjects();
    }
}