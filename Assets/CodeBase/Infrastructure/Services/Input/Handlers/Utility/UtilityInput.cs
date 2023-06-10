using System;
using UnityEngine.InputSystem;

namespace Infrastructure.Services.Input.Handlers.Utility
{
    public class UtilityInput : IUtilityInput
    {
        private readonly PlayerInput.UtilityActions _actions;

        public UtilityInput(PlayerInput.UtilityActions actions)
        {
            _actions = actions;
        }

        public event Action Paused;

        public void Init() => 
            _actions.Pause.performed += OnPaused;

        private void OnPaused(InputAction.CallbackContext callbackContext) => 
            Paused?.Invoke();

        public void Enable() =>
            _actions.Enable();

        public void Disable() => 
            _actions.Disable();
    }
}