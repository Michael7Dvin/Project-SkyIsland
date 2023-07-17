using Gameplay.Heros;
using Gameplay.PlayerCameras;
using Gameplay.Services.Providers.HeroProviding;
using Gameplay.Services.Providers.PlayerCameraProviding;
using Infrastructure.Services.Logging;
using UnityEngine;

namespace Infrastructure.Progress.Handling.IslandLevel
{
    public class IslandWorldProgressHandler : IIslandWorldProgressHandler
    {
        private readonly IHeroProvider _heroProvider;
        private readonly IPlayerCameraProvider _playerCameraProvider;
        private readonly ICustomLogger _logger;

        public IslandWorldProgressHandler(IHeroProvider heroProvider, IPlayerCameraProvider playerCameraProvider, ICustomLogger logger)
        {
            _heroProvider = heroProvider;
            _playerCameraProvider = playerCameraProvider;
            _logger = logger;
        }

        private IHeroProgressDataProvider HeroProgressDataProvider
        {
            get
            {
                if (_heroProvider.Hero == null)
                    _logger.LogError($"Can't perform operation. {nameof(IHeroProvider)} have no {nameof(Hero)} set");

                return _heroProvider.Hero.Value.ProgressDataProvider;
            }
        }

        private PlayerCameraProgressDataProvider PlayerCameraProgressDataProvider
        {
            get
            {
                if (_playerCameraProvider.PlayerCamera == null)
                    _logger.LogError($"Can't perform operation. {nameof(IPlayerCameraProvider)} have no {nameof(PlayerCamera)} set");

                return _playerCameraProvider.PlayerCamera.Value.ProgressDataDataProvider;
            }
        }

        public void WriteProgress(IslandWorldProgress progress)
        {
            progress.IsEmpty = false;

            progress.HeroPosition = HeroProgressDataProvider.Position;
            progress.HeroRotation = HeroProgressDataProvider.Rotation;

            progress.CameraXAxisValue = PlayerCameraProgressDataProvider.XAxisValue;
            progress.CameraYAxisValue = PlayerCameraProgressDataProvider.YAxisValue;
        }

        public void LoadProgress(IslandWorldProgress progress)
        {
            if (progress.IsEmpty == false)
            {
                HeroProgressDataProvider.Position = progress.HeroPosition;
                HeroProgressDataProvider.Rotation = progress.HeroRotation;
                
               PlayerCameraProgressDataProvider.XAxisValue = progress.CameraXAxisValue;
               PlayerCameraProgressDataProvider.YAxisValue = progress.CameraYAxisValue;
            }
        }
    }
}