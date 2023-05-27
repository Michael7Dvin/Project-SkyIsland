using System.Collections.Generic;
using Common.FSM;
using Gameplay.Movement.GroundTypeTracking;
using UnityEngine;

namespace Gameplay.Movement.States.Base
{
    public abstract class ExitableMovementState : IExitableState
    {
        protected abstract HashSet<GroundType> AllowedGroundTypes { get; }

        public Vector3 MoveVelocity { get; protected set; }
        
        public abstract void Exit();

        public bool IsWorkableWithBodyEnvironmentType(GroundType type) => 
            AllowedGroundTypes.Contains(type);

        public abstract void Dispose();
    }
}