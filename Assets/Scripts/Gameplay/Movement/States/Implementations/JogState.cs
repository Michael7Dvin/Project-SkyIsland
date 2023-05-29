using System.Collections.Generic;
using Gameplay.Movement.GroundTypeTracking;
using Gameplay.Movement.SlopeMovement;
using Gameplay.Movement.States.Base;
using Infrastructure.Services.Input;
using UnityEngine;

namespace Gameplay.Movement.States.Implementations
{
    public class JogState : MovementState
    {
        private readonly float _jogSpeed;
        private readonly float _antiBumpSpeed;
        
        private readonly ISlopeSlideMovement _slopeSlideMovement;
        private readonly Transform _camera;

        private readonly IInputService _input;

        public JogState(float jogSpeed,
            float antiBumpSpeed,
            ISlopeSlideMovement slopeSlideMovement,
            Transform camera,
            IInputService input)
        {
            _jogSpeed = jogSpeed;
            _antiBumpSpeed = antiBumpSpeed;
            
            _slopeSlideMovement = slopeSlideMovement;
            _camera = camera;

            _input = input;
        }

        protected override HashSet<GroundType> CanStartWithGroundTypes { get; } = new()
        {
            GroundType.Ground,
        };
        
        protected override HashSet<GroundType> CanWorkWithGroundTypes { get; } = new()
        {
            GroundType.Ground,
        };

        public override void Dispose()
        {
        }

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
 
        private Vector3 GetJogVelocity(float deltaTime)
        {
            Vector3 velocity = new();
            
            if (_input.HorizontalDirection.Value != Vector3.zero)
            {
                Vector3 cameraAlignedDirection = AlignDirectionToCameraView(_input.HorizontalDirection.Value);
                velocity = cameraAlignedDirection * _jogSpeed * deltaTime;
            }
            
            return velocity;
        }

        private Vector3 GetAntiBumpVelocity(float deltaTime) => 
            Vector3.down * _antiBumpSpeed * deltaTime;

        private Vector3 AlignDirectionToCameraView(Vector3 direction) => 
            Quaternion.AngleAxis(_camera.rotation.eulerAngles.y, Vector3.up) * direction;
    }
}