using System.Collections.Generic;
using Common.Observable;
using Gameplay.Movement.GroundTypeTracking;
using Gameplay.Movement.Rotator;
using Gameplay.Movement.SlopeMovement;
using Gameplay.Movement.StateMachine.States.Base;
using UnityEngine;

namespace Gameplay.Movement.StateMachine.States.Implementations
{
    public class JogState : MovementState
    {
        private readonly float _jogSpeed;
        private readonly float _antiBumpSpeed;
        
        private readonly ISlopeSlideMovement _slopeSlideMovement;
        private readonly IRotator _rotator;

        private readonly IReadOnlyObservable<Vector3> _moveDirection;

        public JogState(float jogSpeed,
            float antiBumpSpeed,
            ISlopeSlideMovement slopeSlideMovement,
            IRotator rotator,
            IReadOnlyObservable<Vector3> moveDirection)
        {
            _jogSpeed = jogSpeed;
            _antiBumpSpeed = antiBumpSpeed;
            
            _slopeSlideMovement = slopeSlideMovement;
            _rotator = rotator;

            _moveDirection = moveDirection;
        }

        public override MovementStateType Type => MovementStateType.Jog;
        public override float CurrentHorizontalSpeed => _jogSpeed;
        public override float CurrentVerticalSpeed => _antiBumpSpeed;

        protected override HashSet<GroundType> CanStartWithGroundTypes { get; } = new()
        {
            GroundType.Ground,
        };
        
        protected override HashSet<GroundType> CanWorkWithGroundTypes { get; } = new()
        {
            GroundType.Ground,
        };
        
        public override void Enter()
        {
        }

        public override void Exit()
        {
        }

        public override Vector3 GetMoveVelocty(float deltaTime)
        {
            Vector3 velocity = GetJogVelocity(deltaTime) + GetAntiBumpVelocity(deltaTime);
            
            if (_slopeSlideMovement.IsSteepSlope == true)
                velocity += _slopeSlideMovement.GetSlideDownSlopeVelocity(deltaTime);

            return velocity;
        }
        
        public override Quaternion GetRotation(Quaternion currentRotation, float deltaTime) => 
            _rotator.GetRotationToDirection(GetMoveVelocty(deltaTime), currentRotation, deltaTime);

        private Vector3 GetJogVelocity(float deltaTime)
        {
            Vector3 velocity = new();
            
            if (_moveDirection.Value != Vector3.zero) 
                velocity = _moveDirection.Value * _jogSpeed * deltaTime;

            return velocity;
        }

        private Vector3 GetAntiBumpVelocity(float deltaTime) => 
            Vector3.down * _antiBumpSpeed * deltaTime;

        
    }
}