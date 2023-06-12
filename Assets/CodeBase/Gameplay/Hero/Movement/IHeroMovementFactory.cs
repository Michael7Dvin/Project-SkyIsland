using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Hero.Movement
{
    public interface IHeroMovementFactory
    {
        UniTask WarmUp();

        UniTask<IHeroMovement> Create(Transform parent,
            Animator animator,
            CharacterController characterController,
            Transform camera);
    }
}