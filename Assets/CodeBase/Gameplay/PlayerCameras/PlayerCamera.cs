using System;
using Gameplay.MonoBehaviours;
using UnityEngine;

namespace Gameplay.PlayerCameras
{
    public class PlayerCamera : IDisposable
    {
        public PlayerCamera(Camera camera,
            PlayerCameraController playerCameraController,
            Destroyable destroyable,
            PlayerCameraProgressDataProvider progressDataDataProvider)
        {
            Camera = camera;
            PlayerCameraController = playerCameraController;
            Destroyable = destroyable;
            ProgressDataDataProvider = progressDataDataProvider;
            
            Destroyable.Destroyed += Dispose;
        }

        public Camera Camera { get; }
        public PlayerCameraController PlayerCameraController { get; }
        public Destroyable Destroyable { get; }
        public PlayerCameraProgressDataProvider ProgressDataDataProvider { get; }

        public void Dispose()
        {
            Destroyable.Destroyed -= Dispose;
            
            PlayerCameraController.Dispose();            
        }
    }
}