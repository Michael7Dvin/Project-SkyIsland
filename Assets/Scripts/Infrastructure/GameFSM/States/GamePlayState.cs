
using Infrastructure.Configuration;
using PlayerCamera;
using UnityEngine;

namespace Infrastructure.GameFSM.States
{
    public class GameplayState : IState
    {
        private readonly ConfigProvider _configProvider;
        private readonly PlayerCameraFactory _playerCameraFactory;

        public GameplayState(ConfigProvider configProvider, PlayerCameraFactory playerCameraFactory)
        {
            _configProvider = configProvider;
            _playerCameraFactory = playerCameraFactory;
        }

        public void Enter()
        {
            SpawnCameraForDebug();
        }

        public void Exit()
        {
        }

        public void SpawnCameraForDebug()
        {
            Vector3 cameraParentPosition = new Vector3(28.2298698f, 2.5940001f, 14.3753576f);
            GameObject cameraParent = new GameObject("Camera Parent");
            cameraParent.transform.position = cameraParentPosition;

            _playerCameraFactory.Create(_configProvider.GetForPlayerCamera(), cameraParent.transform);
        }
    }
}