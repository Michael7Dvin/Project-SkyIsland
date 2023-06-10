using UnityEngine.InputSystem;

namespace Infrastructure.Services.Input.Handlers.Camera
{
    public interface ICameraInput : IInputHandler
    {
        InputAction OrbitalRotation { get; }
    }
}