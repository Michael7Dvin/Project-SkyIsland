using Gameplay.BodyEnvironmentObserving;
using UnityEngine;

namespace Gameplay.Player.Movement
{
    public interface IPlayerMovementFactory
    {
        IPlayerMovement Create(CharacterController characterController, IBodyEnvironmentObserver bodyEnvironmentObserver);
    }
}