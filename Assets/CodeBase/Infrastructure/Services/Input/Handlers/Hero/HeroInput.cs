using System;
using Common.Observable;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Infrastructure.Services.Input.Handlers.Hero
{
    public class HeroInput : IHeroInput, IInputHandler
    {
        private bool _enabled;
        private readonly Observable<Vector3> _horizontalDirection = new();
        private readonly PlayerInput.MovementActions _movement;

        public HeroInput(PlayerInput.MovementActions movement)
        {
            _movement = movement;
        }

        public InputHandlerType Type => InputHandlerType.Hero;

        public IReadOnlyObservable<Vector3> HorizontalMoveDirection => _horizontalDirection;
        public event Action Jumped;
        
        public void Init()
        {
            _movement.Horizontal.started += OnHorizontalInput;
            _movement.Horizontal.canceled += OnHorizontalInput;
            _movement.Horizontal.performed += OnHorizontalInput;

            _movement.Jump.performed += OnJumped;
        }

        public void Enable() => 
            _enabled = true;

        public void Disable() => 
            _enabled = false;
        
        private void OnHorizontalInput(InputAction.CallbackContext context)
        {
            if (_enabled == true)
            {
                Vector2 direction = context.ReadValue<Vector2>();

                Vector3 directionToVector3 = new(direction.x, 0f, direction.y);
            
                _horizontalDirection.Value = directionToVector3.normalized;   
            }
        }

        private void OnJumped(InputAction.CallbackContext context)
        {
            if (_enabled == true) 
                Jumped?.Invoke();
        }
    }
}