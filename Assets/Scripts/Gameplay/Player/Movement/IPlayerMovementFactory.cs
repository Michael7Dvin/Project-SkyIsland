using Gameplay.GroundTypeObserving;
using UnityEngine;

namespace Gameplay.Player.Movement
{
    public interface IPlayerMovementFactory
    {
        IPlayerMovement Create(CharacterController characterController,
            IGroundTypeObserver groundTypeObserver,
            Transform camera);
    }
}