using Cysharp.Threading.Tasks;
using Gameplay.Heros.Movement;
using UnityEngine;

namespace Gameplay.Services.Creation.HeroMoving
{
    public interface IHeroMovementFactory
    {
        UniTask WarmUp();

        UniTask<IMovement> Create(Transform parent,
            Animator animator,
            CharacterController characterController);
    }
}