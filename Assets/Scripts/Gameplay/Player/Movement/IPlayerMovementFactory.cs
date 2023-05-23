using Gameplay.BodyEnvironmentObserving;
using UnityEngine;

namespace Gameplay.Player.Movement
{
    public interface IPlayerMovementFactory
    {
        PlayerMovement Create(CharacterController characterController, IBodyEnvironmentObserver bodyEnvironmentObserver);
    }
}