using UnityEngine;

namespace Gameplay.Player.Movement
{
    public interface IPlayerMovementFactory
    {
        IPlayerMovement Create(Transform parent, CharacterController characterController, Transform camera);
    }
}