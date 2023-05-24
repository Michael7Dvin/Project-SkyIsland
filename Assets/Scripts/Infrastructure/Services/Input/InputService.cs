using Common.Observable;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Infrastructure.Services.Input
{
    public class InputService : IInputService
    {
        private readonly PlayerInput _input;
        private readonly Observable<Vector2> _horizontalDirection = new();
        private readonly Observable<float> _horizontalMagnitude = new();

        public InputService()
        {
            _input = new PlayerInput();
            _input.Enable();

            _input.Movement.Horizontal.started += OnHorizontalInput;
            _input.Movement.Horizontal.canceled += OnHorizontalInput;
            _input.Movement.Horizontal.performed += OnHorizontalInput;
        }
        
        public IReadOnlyObservable<Vector2> HorizontalDirection => _horizontalDirection;
        public IReadOnlyObservable<float> HorizontalMagnitude => _horizontalMagnitude;

        private void OnHorizontalInput(InputAction.CallbackContext context)
        {
            Vector2 horizontal = context.ReadValue<Vector2>();
            _horizontalDirection.Value = horizontal;
            _horizontalMagnitude.Value = horizontal.magnitude;
        }
    }
}