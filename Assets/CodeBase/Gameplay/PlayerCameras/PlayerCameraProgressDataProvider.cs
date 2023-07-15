using Cinemachine;

namespace Gameplay.PlayerCameras
{
    public class PlayerCameraProgressDataProvider
    {
        private readonly CinemachineFreeLook _freeLookController;

        public PlayerCameraProgressDataProvider(CinemachineFreeLook freeLookController)
        {
            _freeLookController = freeLookController;
        }
        
        public float XAxisValue
        {
            get => _freeLookController.m_XAxis.Value;
            set => _freeLookController.m_XAxis.Value = value;
        }

        public float YAxisValue
        {
            get => _freeLookController.m_YAxis.Value;
            set => _freeLookController.m_YAxis.Value = value;
        }
    }
}