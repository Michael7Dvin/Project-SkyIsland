using UnityEngine.InputSystem;

namespace Infrastructure.Services.Input.Handlers.Cameras
{
    public interface ICameraInput : IInputHandler
    {
        InputAction OrbitalRotation { get; }
    }
}