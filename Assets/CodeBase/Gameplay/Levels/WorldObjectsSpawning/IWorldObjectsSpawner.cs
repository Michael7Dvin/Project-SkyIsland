using Zenject;

namespace Gameplay.Levels.WorldObjectsSpawning
{
    public interface IWorldObjectsSpawner : IInitializable
    {
        LevelType LevelType { get; }
        void SpawnWorldObjects();
    }
}