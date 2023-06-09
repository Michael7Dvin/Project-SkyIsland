using System;
using Common.Observable;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Infrastructure.Services.Input.Handlers.Hero
{
    public class HeroInput : IHeroInput, IInputHandler
    {
        private readonly Observable<Vector3> _horizontalDirection = new();
        private readonly PlayerInput.MovementActions _movementActions;

        public HeroInput(PlayerInput.MovementActions movementActions)
        {
            _movementActions = movementActions;
        }

        public InputHandlerType Type => InputHandlerType.Hero;

        public IReadOnlyObservable<Vector3> HorizontalMoveDirection => _horizontalDirection;
        public event Action Jumped;
        
        public void Init()
        {
            _movementActions.Horizontal.started += OnHorizontalInput;
            _movementActions.Horizontal.canceled += OnHorizontalInput;
            _movementActions.Horizontal.performed += OnHorizontalInput;

            _movementActions.Jump.performed += OnJumped;
        }

        public void Enable() =>
            _movementActions.Enable();

        public void Disable() =>
            _movementActions.Disable();
        
        private void OnHorizontalInput(InputAction.CallbackContext context)
        {
            Vector2 direction = context.ReadValue<Vector2>();

            Vector3 directionToVector3 = new(direction.x, 0f, direction.y);
            
            _horizontalDirection.Value = directionToVector3.normalized;
        }

        private void OnJumped(InputAction.CallbackContext context) => 
            Jumped?.Invoke();
    }
}