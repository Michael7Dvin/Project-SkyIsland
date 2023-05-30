using System;
using Common.Observable;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Infrastructure.Services.Input
{
    public class InputService : IInputService
    {
        private readonly Observable<Vector3> _horizontalDirection = new();

        public InputService()
        {
            PlayerInput input = new();
            input.Enable();

            input.Movement.Horizontal.started += OnHorizontalInput;
            input.Movement.Horizontal.canceled += OnHorizontalInput;
            input.Movement.Horizontal.performed += OnHorizontalInput;

            input.Movement.Jump.performed += context => Jumped?.Invoke();
        }
        
        public IReadOnlyObservable<Vector3> HorizontalDirection => _horizontalDirection;

        public event Action Jumped;
        
        private void OnHorizontalInput(InputAction.CallbackContext context)
        {
            Vector2 direction = context.ReadValue<Vector2>();

            Vector3 directionToVector3 = new(direction.x, 0f, direction.y);
            
            _horizontalDirection.Value = directionToVector3.normalized;
        }
    }
}