using System;
using Common.Observable;
using Gameplay.PlayerCameras;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Infrastructure.Services.Input.Handlers.Hero
{
    public class HeroInput : IHeroInput
    {
        private PlayerCamera _horizontalDirectionAligningCamera;
        
        private readonly Observable<Vector3> _horizontalDirection = new();
        private readonly PlayerInput.MovementActions _actions;
        private Vector3 _currentHorizontalDirection;

        public HeroInput(PlayerInput.MovementActions actions)
        {
            _actions = actions;
        }

        public IReadOnlyObservable<Vector3> HorizontalMoveDirection => _horizontalDirection;
        public event Action Jumped;

        public void SetHorizontalDirectionAligningCamera(PlayerCamera playerCamera)
        {
            if (_horizontalDirectionAligningCamera != null)
                _horizontalDirectionAligningCamera.PlayerCameraController.XAxisValue.Changed -= OnCameraXAxisValueChanged;

            _horizontalDirectionAligningCamera = playerCamera;
            _horizontalDirectionAligningCamera.PlayerCameraController.XAxisValue.Changed += OnCameraXAxisValueChanged;
        }

        public void Init()
        {
            _actions.Horizontal.started += OnHorizontalInput;
            _actions.Horizontal.canceled += OnHorizontalInput;
            _actions.Horizontal.performed += OnHorizontalInput;

            _actions.Jump.performed += OnJumped;
        }

        public void Dispose()
        {
            _actions.Horizontal.started -= OnHorizontalInput;
            _actions.Horizontal.canceled -= OnHorizontalInput;
            _actions.Horizontal.performed -= OnHorizontalInput;

            _actions.Jump.performed -= OnJumped;
        }

        public void Enable() =>
            _actions.Enable();

        public void Disable() =>
            _actions.Disable();

        private void OnHorizontalInput(InputAction.CallbackContext context)
        {
            if (_horizontalDirectionAligningCamera != null)
            {
                Vector2 direction = context.ReadValue<Vector2>();

                Vector3 directionToVector3 = new(direction.x, 0f, direction.y);

                _currentHorizontalDirection = directionToVector3.normalized;
                
                AlignHorizontalInputToCamera(_horizontalDirectionAligningCamera.Camera.transform);
            }
        }

        private void OnCameraXAxisValueChanged(float value) => 
            AlignHorizontalInputToCamera(_horizontalDirectionAligningCamera.Camera.transform);

        private void AlignHorizontalInputToCamera(Transform camera)
        {
            Vector3 cameraAlignedDirection = 
                    Quaternion.AngleAxis(camera.rotation.eulerAngles.y, Vector3.up) * _currentHorizontalDirection;

            _horizontalDirection.Value = cameraAlignedDirection;
        }

        private void OnJumped(InputAction.CallbackContext context) => 
            Jumped?.Invoke();
    }
}