using Gameplay.GroundTypeObserving;
using UnityEngine;

namespace Gameplay.Player.Movement
{
    public interface IPlayerMovementFactory
    {
        IPlayerMovement Create(Transform parent, CharacterController characterController,
            IGroundTypeObserver groundTypeObserver, Transform camera);
    }
}