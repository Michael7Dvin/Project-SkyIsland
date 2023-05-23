using Gameplay.Player;
using UnityEngine;
using Zenject;

namespace Gameplay.Levels
{
    public class IslandWorldObjectsLoader : IInitializable
    {
        private readonly IPlayerFactory _playerFactory;
        
        private readonly Vector3 _playerSpawnPosition;
        private readonly Quaternion _playerSpawnRotation;

        public IslandWorldObjectsLoader(IPlayerFactory playerFactory,
            Vector3 playerSpawnPosition,
            Quaternion playerSpawnRotation)
        {
            _playerFactory = playerFactory;
            
            _playerSpawnPosition = playerSpawnPosition;
            _playerSpawnRotation = playerSpawnRotation;
        }

        public void Initialize() => 
            Load();

        private void Load() => 
            _playerFactory.Create(_playerSpawnPosition, _playerSpawnRotation);
    }
}