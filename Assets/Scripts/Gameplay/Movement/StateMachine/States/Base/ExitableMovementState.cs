using System;
using System.Collections.Generic;
using Common.FSM;
using Gameplay.Movement.GroundTypeTracking;
using UnityEngine;

namespace Gameplay.Movement.StateMachine.States.Base
{
    public abstract class ExitableMovementState : IExitableState
    {
        public abstract MovementStateType Type { get; }

        public abstract float CurrentHorizontalSpeed { get; }
        public abstract float CurrentVerticalSpeed { get; }
        
        protected abstract HashSet<GroundType> CanStartWithGroundTypes { get; }
        protected abstract HashSet<GroundType> CanWorkWithGroundTypes { get; }

        public event Action MovementPerformed;
        
        public abstract void Exit();
        public abstract Vector3 GetMoveVelocty(float deltaTime);

        public virtual Quaternion GetRotation(Quaternion currentRotation, float deltaTime) => 
            currentRotation;

        public virtual bool CanStart(GroundType currentGroundType) => 
            CanStartWithGroundTypes.Contains(currentGroundType);
        
        public bool CanWorkWithGroundType(GroundType type) => 
            CanWorkWithGroundTypes.Contains(type);

        protected void NotifyMovementPerforemed() => 
            MovementPerformed?.Invoke();
    }
}