using System;
using UnityEngine.InputSystem;

namespace Infrastructure.Services.Input.Handlers.Utility
{
    public class UtilityInput : IUtilityInput, IInputHandler
    {
        private bool _enabled;
        private readonly PlayerInput.UtilityActions _actions;

        public UtilityInput(PlayerInput.UtilityActions actions)
        {
            _actions = actions;
        }

        public event Action Paused;
        public InputHandlerType Type => InputHandlerType.Utility;
        
        public void Init() => 
            _actions.Pause.performed += OnPaused;

        private void OnPaused(InputAction.CallbackContext callbackContext)
        {
            if (_enabled == true) 
                Paused?.Invoke();
        }

        public void Enable() => 
            _enabled = true;

        public void Disable() => 
            _enabled = false;
    }
}