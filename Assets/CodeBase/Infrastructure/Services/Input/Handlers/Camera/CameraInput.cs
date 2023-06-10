using UnityEngine.InputSystem;

namespace Infrastructure.Services.Input.Handlers.Camera
{
    public class CameraInput : ICameraInput
    {
        private readonly PlayerInput.CameraActions _actions;

        public InputAction OrbitalRotation => _actions.OrbitalRotation;

        public CameraInput(PlayerInput.CameraActions actions)
        {
            _actions = actions;
        }

        public void Enable() => 
            _actions.Enable();

        public void Disable() => 
            _actions.Disable();
    }
}