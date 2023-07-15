using Common.Observable;
using Gameplay.PlayerCameras;

namespace Gameplay.Services.Providing.PlayerCameraProviding
{
    public class PlayerCameraProvider : IPlayerCameraProvider 
    {
        private readonly Observable<PlayerCamera> _playerCamera = new();
        public IReadOnlyObservable<PlayerCamera> PlayerCamera => _playerCamera;
        
        public void Set(PlayerCamera hero)
        {
            _playerCamera.Value = hero;
            hero.Destroyable.Destroyed += Remove;
        }

        private void Remove()
        {
            if (_playerCamera.Value != null) 
                _playerCamera.Value.Destroyable.Destroyed -= Remove;

            _playerCamera.Value = null;
        }
    }
}