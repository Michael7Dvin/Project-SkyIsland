using System;
using Cinemachine;
using Common.Observable;
using Infrastructure.Services.Updating;

namespace Gameplay.PlayerCameras
{
    public class PlayerCameraController : IDisposable
    {
        private readonly IUpdater _updater;
        private CinemachineFreeLook _freeLookController;

        private Observable<float> _xAxisValue = new();
        private IDisposable _disposableImplementation;

        public PlayerCameraController(IUpdater updater)
        {
            _updater = updater;
        }

        public void Construct(CinemachineFreeLook freeLookController)
        {
            _freeLookController = freeLookController;
            _updater.NotPausingUpdated += UpdateXAxisValue;
        }

        public IReadOnlyObservable<float> XAxisValue => _xAxisValue;

        public void Dispose() => 
            _updater.NotPausingUpdated -= UpdateXAxisValue;

        private void UpdateXAxisValue(float deltaTime)
        {
            if (_freeLookController.m_XAxis.Value == _xAxisValue.Value)
                return;

            _xAxisValue.Value = _freeLookController.m_XAxis.Value;
        }
    }
}