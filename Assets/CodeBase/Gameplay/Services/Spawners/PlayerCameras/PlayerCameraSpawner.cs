using Cysharp.Threading.Tasks;
using Gameplay.PlayerCameras;
using Gameplay.Services.Factories.PlayerCameras;
using Gameplay.Services.Providers.PlayerCameraProviding;
using Infrastructure.Services.Logging;

namespace Gameplay.Services.Spawners.PlayerCameras
{
    public class PlayerCameraSpawner : IPlayerCameraSpawner
    {
        private readonly IPlayerCameraFactory _playerCameraFactory;
        private readonly IPlayerCameraProvider _playerCameraProvider;
        private readonly ICustomLogger _logger;

        public PlayerCameraSpawner(IPlayerCameraFactory playerCameraFactory,
            IPlayerCameraProvider playerCameraProvider,
            ICustomLogger logger)
        {
            _playerCameraFactory = playerCameraFactory;
            _playerCameraProvider = playerCameraProvider;
            _logger = logger;
        }

        public async UniTask<PlayerCamera> Spawn()
        {
            if (_playerCameraProvider.PlayerCamera.Value != null) 
                _logger.LogWarning($"Possible duplication. {nameof(IPlayerCameraProvider)} have another {nameof(PlayerCamera)} instance set");

            PlayerCamera playerCamera = await _playerCameraFactory.Create();
            _playerCameraProvider.Set(playerCamera);

            return playerCamera;
        }
    }
}