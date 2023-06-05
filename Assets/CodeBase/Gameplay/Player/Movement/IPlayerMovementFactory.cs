using UnityEngine;

namespace Gameplay.Player.Movement
{
    public interface IPlayerMovementFactory
    {
        IPlayerMovement Create(Transform parent, Animator animator, CharacterController characterController, Transform camera);
    }
}