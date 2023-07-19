using System;
using Common.Observable;
using Gameplay.Movement.StateMachine.States;
using Gameplay.Movement.StateMachine.States.Base;
using Infrastructure.Services.Logging;
using Infrastructure.Services.Updating;
using UnityEngine;

namespace Gameplay.Heros
{
    public class HeroAnimator : IDisposable
    {
        private Animator _animator;
        private IReadOnlyObservable<ExitableMovementState> _activeMovementState;
        private CharacterController _characterController;

        private const float IntensitySettingDampTime = 0.3f;

        private readonly int _horizontalMotionIntensityFloat = Animator.StringToHash("HorizontalMotionIntensity");
        private readonly int _verticalMotionIntensityFloat = Animator.StringToHash("VerticalMotionIntensity");
        private readonly int _groundTrigger = Animator.StringToHash("Ground");
        private readonly int _fallTrigger = Animator.StringToHash("Fall");
        private readonly int _jumpTrigger = Animator.StringToHash("Jump");
        
        private readonly IUpdater _updater;
        private readonly ICustomLogger _logger;

        public HeroAnimator(IUpdater updater, ICustomLogger logger)
        {
            _updater = updater;
            _logger = logger;
        }

        public void Construct(Animator animator,
            IReadOnlyObservable<ExitableMovementState> activeMovementState,
            CharacterController characterController)
        {
            _animator = animator;
            _activeMovementState = activeMovementState;
            _characterController = characterController;
            
            _updater.Updated += Update;
            _activeMovementState.Changed += OnActiveMovementStateChanged;
        }
        
        private float HorizontalMotionIntensity
        {
            get
            {
                float speed = _activeMovementState.Value.CurrentHorizontalSpeed;

                if (speed <= 0)
                    return 0f;

                Vector3 velocity = _characterController.velocity;
                Vector3 horizontalVelocity = new(velocity.x, 0f, velocity.z);

                float horizontalVelocityMagnitude = horizontalVelocity.magnitude;

                return Mathf.Clamp01(horizontalVelocityMagnitude / speed);
            }
        }
        
        private float VerticalMotionIntensity
        {
            get
            {
                float speed = _activeMovementState.Value.CurrentVerticalSpeed;
                
                if (speed <= 0)
                    return 0f;

                float verticalVelocityMagnitude = _characterController.velocity.y;

                return Mathf.Clamp01(verticalVelocityMagnitude / speed);
            }
        }

        public void Dispose()
        {
            _updater.Updated -= Update;
            _activeMovementState.Changed -= OnActiveMovementStateChanged;
        }
        
        private void Update(float deltaTime) => 
            SetMotionIntensity(deltaTime);

        private void OnActiveMovementStateChanged(ExitableMovementState state)
        {
            switch (state.Type)
            {
                case MovementStateType.Fall:
                    _animator.SetTrigger(_fallTrigger);
                    break;
                case MovementStateType.Jog:
                    _animator.SetTrigger(_groundTrigger);
                    break;
                case MovementStateType.Jump:
                    _animator.SetTrigger(_jumpTrigger);
                    break;
                default:
                    _logger.LogError($"Unsupported {nameof(MovementStateType)}: '{state.Type}'");
                    break;
            }
        }

        private void SetMotionIntensity(float deltaTime)
        {
            _animator.SetFloat(_horizontalMotionIntensityFloat,
                HorizontalMotionIntensity,
                IntensitySettingDampTime,
                deltaTime);
            
            _animator.SetFloat(_verticalMotionIntensityFloat,
                VerticalMotionIntensity,
                IntensitySettingDampTime,
                deltaTime);
        }
    }
}